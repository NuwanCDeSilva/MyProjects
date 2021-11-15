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
using FF.WindowsERPClient.Inventory;
using System.IO;
using FF.WindowsERPClient.Reports.Finance;
using Excel = Microsoft.Office.Interop.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using System.Net.Mail;
using System.Net.Mime;
//Written By Dilshan on 30/10/2017
namespace FF.WindowsERPClient.Finance
{
    public partial class Valuation_Upload : Base
    {
        Base bsObj;
        public Valuation_Upload()
        {
            try
            {
                InitializeComponent();
                InitializeEnv();
                GetCompanyDet(null, null);
                GetPCDet(null, null);
                setFormControls(0);
                BindAdminTeam();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindAdminTeam()
        {
            DataTable dt = new DataTable();
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
            dt = CHNLSVC.CommonSearch.GetAdminTeamByCompany(para, null, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    lstAdmin.Items.Add(drow["mso_cd"].ToString());
                }
            }

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }


                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        //paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        //break;
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InventoryDirection:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "CT003" + seperator + "0" + seperator + "" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void setFormControls(Int32 _index)
        {
            pnlAsAtDate.Enabled = false;
            pnlDateRange.Enabled = true;
            pnlLoc.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            pnlLifeSpan.Enabled = false;
            pnlModelIntroduce.Enabled = false;
            pnlLifeSpan.Visible = false;
            pnlModelIntroduce.Visible = false;
            pnlDisConItems.Enabled = false;
            //chkroot.Visible = false;
            //chkroot.Enabled = false;
        }

        protected void GetCompanyDet(object sender, EventArgs e)
        {

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
            if (_masterComp != null)
            {
                txtCompDesc.Text = _masterComp.Mc_desc;
                txtCompAddr.Text = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }
            else
            {
                txtCompDesc.Text = "";
                txtCompAddr.Text = "";
            }
        }

        protected void GetPCDet(object sender, EventArgs e)
        {

            MasterLocation _masterLoc = null;
            _masterLoc = CHNLSVC.General.GetAllLocationByLocCode(txtComp.Text, txtPC.Text, 1);
            if (_masterLoc != null)
            {
                txtPCDesn.Text = _masterLoc.Ml_loc_desc;
            }
            else
            {
                txtPCDesn.Text = "";
            }
        }

        private void InitializeEnv()
        {
            txtComp.Text = BaseCls.GlbUserComCode;
            txtPC.Text = BaseCls.GlbUserDefLoca;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            cmbYear.SelectedIndex = _Year % 2013 + 1;

            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtAsAtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;
        }

        private void update_PC_List()
        {
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            List<string> _listLocs = new List<string>();

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
                _listLocs.Add(txtPC.Text);
            }
            else
            {
                _listLocs = lstPC.Items.Cast<ListViewItem>().Where(item => item.Checked).Select(item => item.Text).ToList();
            }
            if (_listLocs.Count > 0) CHNLSVC.Security.Add_User_Selected_Loc_Pc_DR(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null, _listLocs);

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            Boolean _isChk = false;
            string _adminTeam = "";

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

            foreach (ListViewItem Item in lstAdmin.Items)
            {
                if (Item.Checked == true)
                {
                    _adminTeam = Item.Text;
                    _isChk = true;
                    break;
                }
            }
            if (_isChk == true)
            {
                DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPI"))
                //Add by Chamal 30-Aug-2013
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10045))
                {
                    if (optSales.Checked == true)
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            //MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(txtComp.Text.Trim().ToUpper(), drow["PROFIT_CENTER"].ToString(), 1);
                            //if (_loc != null)
                            //{
                            //    if (_loc.Ml_anal1 == "SCM2")
                            //    {
                            //        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            //    }
                            //    else
                            //    {
                            //        continue;
                            //    }
                            //}
                            //else
                            //{
                            //    continue;
                            //}

                        }
                    }
                    else
                    {
                        DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);

                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                }
                else
                {
                    if (optSales.Checked == true) // add by tharanga 2017/08/29
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            //MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(txtComp.Text.Trim().ToUpper(), drow["PROFIT_CENTER"].ToString(), 1);
                            //if (_loc != null)
                            //{
                            //    if (_loc.Ml_anal1 == "SCM2")
                            //    {
                            //        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            //    }
                            //    else
                            //    {
                            //        continue;
                            //    }
                            //}
                            //else
                            //{
                            //    continue;
                            //}
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                    else
                    {
                        DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                }
            }
        }
        private void txtChanel_LostFocus(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
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

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            int month = cmbMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(cmbYear.Text);

            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);

            txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
            txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
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

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
        }

        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        }

        private void txtAsAtDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                DataTable _result = new DataTable();
                //if (chkroot.Checked == true)// add by tharanga 2017/08/29
                //{
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Route_cd);
                //    _result = CHNLSVC.CommonSearch.Get_route_SearchData(_CommonSearch.SearchParams, null, null);
                //}
                //else
                if (optSales.Checked == true)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();

                load_PCDesc();

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void load_PCDesc()
        {
            DataTable LocDes = new DataTable();
            txtPCDesn.Text = "";
            //if (chkroot.Checked == true) // add by tharanga 2017/08/29
            //{

            //    LocDes = CHNLSVC.Sales.getrootDesc(BaseCls.GlbUserComCode, txtPC.Text);
            //    foreach (DataRow row2 in LocDes.Rows)
            //    {
            //        txtPCDesn.Text = row2["frh_desc"].ToString();
            //    }
            //}
            //else
            if (optSales.Checked == true)
            {
                LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtPC.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtPCDesn.Text = row2["descp"].ToString();
                }
            }
            else
            {
                LocDes = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", txtPC.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtPCDesn.Text = row2["descp"].ToString();
                }
            }

        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            load_PCDesc();
        }

        private void btn_close_admin_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstAdmin.Items)
            {
                Item.Checked = false;
            }
            pnlAdmin.Visible = false;
        }

        private void lstAdmin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            foreach (ListViewItem lstItem in lstAdmin.Items)
            {
                if (lstItem.Text != e.Item.Text)
                {
                    lstItem.Checked = false;
                }
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10140))
            {
                MessageBox.Show("Sorry, You have no permission to view admin team!\n( Advice: Required permission code :10140 )");
                return;
            }
            pnlAdmin.Visible = true;
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < lstGroup.Items.Count; i++)
            //{
            //    if (lstGroup.Items[i].Text == "PC")
            //        return;
            //}
            //lstGroup.Items.Add(("PC").ToString());
        }

        private void chkroot_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkroot.Checked == true)
            //{
            //    label5.Text = "Route";
            //    txtPC.Text = "";
            //    txtPCDesn.Text = "";
            //    lstPC.Clear();
            //}
            //else
            {
                label5.Text = "Location";
                txtPC.Text = "";
                txtPCDesn.Text = "";
                lstPC.Clear();

            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string p_sun_user_id = string.Empty;
                string p_file_path = string.Empty;
                string p_source_path = string.Empty;
                string p_file_name = string.Empty;
                string p_pc_hrchy = "";

                if (txtFromDate.Text == string.Empty)
                {
                    MessageBox.Show("Select the Year/Month", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //if (optScan.Checked == true && ddlWeek.SelectedIndex == -1)
                //{
                //    MessageBox.Show("Select the Week", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (!string.IsNullOrEmpty(txtZone.Text))
                    p_pc_hrchy = txtZone.Text;
                else if (!string.IsNullOrEmpty(txtRegion.Text))
                    p_pc_hrchy = txtRegion.Text;
                else if (!string.IsNullOrEmpty(txtArea.Text))
                    p_pc_hrchy = txtArea.Text;
                else if (!string.IsNullOrEmpty(txtSChanel.Text))
                    p_pc_hrchy = txtSChanel.Text;
                else if (!string.IsNullOrEmpty(txtChanel.Text))
                    p_pc_hrchy = txtChanel.Text;
                else if (!string.IsNullOrEmpty(txtComp.Text))
                    p_pc_hrchy = txtComp.Text;

                this.Cursor = Cursors.WaitCursor;
                //accounting period
                string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtToDate.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtToDate.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");

                
                    //check whether date range is valid
                    Int32 _ok = CHNLSVC.Financial.IsValidWeekDataRange(Convert.ToInt32(cmbYear.Text), cmbMonth.SelectedIndex + 1, Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date, BaseCls.GlbUserComCode);
                    if (_ok == 0)
                    {
                        MessageBox.Show("Invalid Date Range !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //check whether the period is finalized


                    //check whether SUN user ID exist
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {
                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    if (lstPC.Items.Count == 0)
                    {
                        MessageBox.Show("Select the location", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    //file name
                    p_file_name = p_sun_user_id + txtPC.Text + "SCM2" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";

                    //check whether local path is exist
                    p_source_path = @"C:\\SUN";

                    if (!Directory.Exists(p_source_path))
                    {

                        MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    p_source_path = p_source_path + "\\";
                    //local file path to save 
                    p_file_path = p_source_path + p_file_name;

                    MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);  //7/5/2015
                    DataTable X = new DataTable();
                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            if (optInvent.Checked==true)
                            {
                                //DataTable tmpX = CHNLSVC.Financial.ProcessSUNUpload(cmbMonth.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path, mst_com.Mc_anal24);
                                DataTable tmpX = CHNLSVC.Financial.EvaluationSUNUpload(cmbMonth.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, "GRN");//, p_file_path, mst_com.Mc_anal24);
                                X.Merge(tmpX);
                            }
                            if (optSales.Checked == true)
                            {
                                //DataTable FORWARD_SALES_REP = CHNLSVC.Sales.GetForwardSalesDetails(Convert.ToDateTime(txtToDate.Text), BaseCls.GlbUserID, "", "", "", "", "", "", BaseCls.GlbReportExeType, BaseCls.GlbReportDiscRate);
                                DataTable tmpX = CHNLSVC.Financial.GetUploadForwardSalesDetails(Convert.ToDateTime(txtToDate.Text), BaseCls.GlbUserID, "", "", "", "", "", "", "All", 0, BaseCls.GlbUserComCode, pc, "N", "", "", BaseCls.GlbUserID, vAccPeriod, p_sun_user_id);
                                //DataTable tmpX = CHNLSVC.Financial.EvaluationSUNUpload2(cmbMonth.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, "GRN");//, p_file_path, mst_com.Mc_anal24);
                                X.Merge(tmpX);
                            }
                        }
                    }
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any record found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
                            if (X.Rows[i]["STR_15"].ToString() == "")
                            {
                                X.Rows[i]["STR_15"]="000000000000000";
                            }

                            _STR = X.Rows[i]["STR_01"].ToString() +
                                X.Rows[i]["STR_02"].ToString() + X.Rows[i]["STR_03"].ToString() + X.Rows[i]["STR_04"].ToString() +
                                X.Rows[i]["STR_05"].ToString() + X.Rows[i]["STR_06"].ToString() + X.Rows[i]["STR_07"].ToString() +
                                X.Rows[i]["STR_08"].ToString() + X.Rows[i]["STR_09"].ToString() + X.Rows[i]["STR_10"].ToString() +
                                X.Rows[i]["STR_11"].ToString() + X.Rows[i]["STR_12"].ToString() + X.Rows[i]["STR_13"].ToString() +
                                X.Rows[i]["STR_14"].ToString() + X.Rows[i]["STR_15"].ToString() + X.Rows[i]["STR_16"].ToString() +
                                X.Rows[i]["STR_17"].ToString() + X.Rows[i]["STR_18"].ToString() + X.Rows[i]["STR_19"].ToString() +
                                X.Rows[i]["STR_20"].ToString() + X.Rows[i]["STR_21"].ToString() + X.Rows[i]["STR_22"].ToString() +
                                X.Rows[i]["STR_23"].ToString() + X.Rows[i]["STR_24"].ToString() + X.Rows[i]["STR_25"].ToString() +
                                X.Rows[i]["STR_26"].ToString() + X.Rows[i]["STR_27"].ToString() + X.Rows[i]["STR_28"].ToString() +
                                X.Rows[i]["STR_29"].ToString() + X.Rows[i]["STR_30"].ToString() + X.Rows[i]["STR_31"].ToString() +
                                X.Rows[i]["STR_32"].ToString() + X.Rows[i]["STR_33"].ToString() + X.Rows[i]["STR_34"].ToString() +
                                X.Rows[i]["STR_35"].ToString() + X.Rows[i]["STR_36"].ToString() + X.Rows[i]["STR_37"].ToString() +
                                X.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "SOS");
                    //txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void File_Copy(string _fileName, string _sourceFilePath, string _upType)
        {
            try
            {
                if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\sos\\" + _fileName, true);
                else
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\sos\\" + _fileName, true);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Uploading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = true;
            //pnlInvalidReceipts.Visible = false;
           // if (dgvInvalidReceipts.Rows.Count > 0) { dgvInvalidReceipts.Rows.Clear(); }
        }

        private void optInvent_CheckedChanged(object sender, EventArgs e)
        {
            label5.Text = "Location";
            txtPC.Text = "";
            txtPCDesn.Text = "";
        }

        private void optSales_CheckedChanged(object sender, EventArgs e)
        {
            label5.Text = "Profit Center";
            txtPC.Text = "";
            txtPCDesn.Text = "";
        }


    }
}
