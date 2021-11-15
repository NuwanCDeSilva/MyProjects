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
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.Finance
{
    public partial class SOS_Upload : Base
    {
        Boolean WithCancleentry = false;
        private String doc_type = "";
        public SOS_Upload()
        {
            InitializeComponent();
            pnlLoc.Enabled = true;  //default option is sos upload
            bindData();
        }

        private void setFormControls(Int32 _index)
        {
            txtPC.Text = "";
            pnlLoc.Enabled = false;
            ddlWeek.Enabled = false;
            txtFrom.Enabled = false;
            txtTo.Enabled = false;
            txtPC.Enabled = false;
            cmbMonth.Enabled = false;
            cmbYear.Enabled = false;
            btnAddItem.Enabled = true;
            btnHierachySearch.Enabled = false;
            txtPC.Text = BaseCls.GlbUserDefProf;
           

            switch (_index)
            {
                case 1:     //SOS
                    {
                        //txtPC.Enabled = true;
                        txtPC.Text = "";
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_SOS";
                        break;
                    }
                case 2:     //dealer inv
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type="SUN_Dea_Inv";
                        break;
                    }
                case 3:     //dealer receipt
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_Dea_Rec";
                        break;
                    }
                case 4:     //scan
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        ddlWeek.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_Scn_Doc";
                        break;
                    }
                case 5:     //elite
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        doc_type = "SUN_Elite_Sal";
                        break;
                    }
                case 6:     //loyalty
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        doc_type = "SUN_loyalty";
                        break;
                    }
                case 7:     //return chq
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        ddlWeek.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_Retn_Cheque";
                        break;
                    }
                case 8:     //rtn chq settle
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        ddlWeek.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_Retrn_Chq_Setl";
                        break;
                    }
                case 9:     //voucher entry
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        btnAddItem.Enabled = false;
                        doc_type = "SUN_Int_Pay";
                        break;
                    }
                case 10:     //voucher claim
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        //txtFrom.Enabled = true;
                        //txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        btnAddItem.Enabled = true;
                        ddlWeek.Enabled = true;
                        doc_type = "SUN_Inter_Pay";
                        break;
                    }
                case 11:     //fixed asset
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        break;
                    }
                case 12:    //product bonus
                    {
                        txtPC.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        pnlLoc.Enabled = false;
                        doc_type = "SUN_Prod_Bons";
                        break;
                    }
                case 13:     //Duty Free
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        doc_type = "SUN_Duty_Free";
                        break;
                    }
                case 14:     //AOA
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        doc_type = "SUN_AOA_Sales";
                        break;
                    }
                case 15:     //Cheque Printing
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        txtPC.Enabled = false;
                       // pnlLoc.Enabled = true;
                        txtAccNo.Enabled = true;
                        btnAccNoSearch.Enabled = true;
                        btnAddItem.Enabled = false;
                        doc_type = "SUN_Cheq_Print";

                        break;
                    }
                case 16:     //Fund
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        txtPC.Enabled = false;
                        // pnlLoc.Enabled = true;
                        txtAccNo.Enabled = true;
                        btnAccNoSearch.Enabled = true;
                        btnAddItem.Enabled = false;
                        doc_type = "SUN_Fund_Transer";

                        break;
                    }
                case 17:     //Loyality
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        txtPC.Enabled = true;
                        txtAccNo.Enabled = false;
                        // pnlLoc.Enabled = true;
                        doc_type = "SUN_Loyality";

                        break;
                    }
                case 18:     //bank charges
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        txtPC.Enabled = false;
                        // pnlLoc.Enabled = true;
                        txtAccNo.Enabled = true;
                        btnAccNoSearch.Enabled = true;
                        btnAddItem.Enabled = false;
                        doc_type = "SUN_Bank_Charges";

                        break;
                    }
                case 19:     //service inv
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_Ser_Sales";
                        break;
                    }
                case 20:     //service rec
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type = "SUN_Ser_Receipts";
                        break;
                    }
                case 21:     //JAD
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = false;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = false;
                        doc_type = "SUN_JAD_Sales";
                        break;
                    }
                case 22:     //cedite note
                    {
                        txtPC.Enabled = true;
                        btnHierachySearch.Enabled = true;
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                        txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        pnlLoc.Enabled = true;
                        doc_type ="SUN_Warr_Crdit_Note";
                        break;
                    }

                case 23:     //bank charges
                    {
                        cmbYear.Enabled = true;
                        cmbMonth.Enabled = true;
                      //  txtFrom.Enabled = true;
                        txtTo.Enabled = true;
                        txtPC.Enabled = false;
                        // pnlLoc.Enabled = true;
                        txtAccNo.Enabled = true;
                        btnAccNoSearch.Enabled = true;
                        btnAddItem.Enabled = false;
                        label5.Text = "As at Date";
                        doc_type = "SUN_Bank_Recompilation";

                        break;
                    }
            }
        }
        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnHierachySearch_Click(null, null);

                }
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
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



        protected void bindData()
        {
            
            txtComp.Text = BaseCls.GlbUserComCode;
           // txtPC1.Text = BaseCls.GlbUserDefProf;

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
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string p_sun_user_id = string.Empty;
                string p_file_path = string.Empty;
                string p_source_path = string.Empty;
                string p_file_name = string.Empty;
                string p_err_file_name = string.Empty;
                string p_pc_hrchy = "";

                if (txtFrom.Text == string.Empty)
                {
                    MessageBox.Show("Select the Year/Month", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (optScan.Checked == true && ddlWeek.SelectedIndex == -1)
                {
                    MessageBox.Show("Select the Week", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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
                string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");

                #region Return Cheque Settlement
                if (rbRtnChqSet.Checked == true)
                {
                    if (lstPC.Items.Count == 0)
                    {
                        MessageBox.Show("Select the location", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

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


                    //p_file_name = p_sun_user_id + "CCSHB.txt";
                    p_file_name = p_sun_user_id + "RCHQS" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                          if (Item.Checked == true)
                        {
                            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);
                         }
                    }



                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_RtnChequeSettlement(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/12
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("Records not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "RCHQS");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion

                #region Return Cheque
                if (rbRtnChq.Checked == true)
                {
                    if (lstPC.Items.Count == 0)
                    {
                        MessageBox.Show("Select the location", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

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

                    //return cheque*******************************************************************
                    //p_file_name = p_sun_user_id + "CCSHB.txt";
                    p_file_name = p_sun_user_id + "RCHQ" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                          if (Item.Checked == true)
                        {
                            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);
                         }
                    }



                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_RtnCheque(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/12
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("Records not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "RCHQ");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                #endregion

                #region Scan Docs
                //---------------------------------------------------SCAN DOCUMENTS UPLOAD-------------------------------------------------------------------------------------------------------------------------
                if (optScan.Checked == true)
                {
                    if (lstPC.Items.Count == 0)
                    {
                        MessageBox.Show("Select the location", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);
                        }
                    }

                    //Scan Docs (Cash Bankings)*******************************************************************
                    p_file_name = p_sun_user_id + "CCSHB.txt";

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

                    string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

                    // CASH BANKINGS
                    MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);  //7/5/2015
                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime(lblFrmdtWk.Text), Convert.ToDateTime(lblTodtWk.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, "CB", _strMonth,mst_com.Mc_anal24);
                  
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - X.Rows[i]["STR_16"].ToString().Length);

                            _STR = X.Rows[i]["STR_01"].ToString() + "      " +
                                X.Rows[i]["STR_02"].ToString() + X.Rows[i]["STR_03"].ToString() + "  " + X.Rows[i]["STR_04"].ToString() +
                                X.Rows[i]["STR_05"].ToString() + X.Rows[i]["STR_06"].ToString() + "  " + X.Rows[i]["STR_07"].ToString() +
                                X.Rows[i]["STR_08"].ToString() + X.Rows[i]["STR_09"].ToString() + X.Rows[i]["STR_11"].ToString() + "  " + X.Rows[i]["STR_12"].ToString() + "          " +
                                X.Rows[i]["STR_13"].ToString() + X.Rows[i]["STR_14"].ToString() + "                                                   " + X.Rows[i]["STR_15"].ToString() + "             " +
                                X.Rows[i]["STR_16"].ToString() + str + X.Rows[i]["STR_17"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "SCAN");
                    txtFile.Text = p_file_name;

                    //Scan Docs (Cheques)*******************************************************************
                    p_file_name = p_sun_user_id + "CCHQB.txt";

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


                    // cheque bankings
                    DataTable Y = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime(lblFrmdtWk.Text), Convert.ToDateTime(lblTodtWk.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, "CHQ", _strMonth,mst_com.Mc_anal24);
                    
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < Y.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - Y.Rows[i]["STR_16"].ToString().Length);

                            _STR = Y.Rows[i]["STR_01"].ToString() + "      " +
                                Y.Rows[i]["STR_02"].ToString() + Y.Rows[i]["STR_03"].ToString() + "  " + Y.Rows[i]["STR_04"].ToString() +
                                Y.Rows[i]["STR_05"].ToString() + Y.Rows[i]["STR_06"].ToString() + "  " + Y.Rows[i]["STR_07"].ToString() +
                                Y.Rows[i]["STR_08"].ToString() + Y.Rows[i]["STR_09"].ToString() + Y.Rows[i]["STR_11"].ToString() + "  " + Y.Rows[i]["STR_12"].ToString() + "          " +
                                Y.Rows[i]["STR_13"].ToString() + Y.Rows[i]["STR_14"].ToString() + "                                                   " + Y.Rows[i]["STR_15"].ToString() + "             " +
                                Y.Rows[i]["STR_16"].ToString() + str + Y.Rows[i]["STR_17"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "SCAN");
                    txtFile.Text = txtFile.Text + "  " + p_file_name;

                    //Scan Docs (Credit Cards)*******************************************************************
                    p_file_name = p_sun_user_id + "CCDCB.txt";

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


                    // credit card bankings
                    DataTable Z = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime(lblFrmdtWk.Text), Convert.ToDateTime(lblTodtWk.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, "CRCD", _strMonth,mst_com.Mc_anal24);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < Z.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - Z.Rows[i]["STR_16"].ToString().Length);

                            _STR = Z.Rows[i]["STR_01"].ToString() + "      " +
                                Z.Rows[i]["STR_02"].ToString() + Z.Rows[i]["STR_03"].ToString() + "  " + Z.Rows[i]["STR_04"].ToString() +
                                Z.Rows[i]["STR_05"].ToString() + Z.Rows[i]["STR_06"].ToString() + "  " + Z.Rows[i]["STR_07"].ToString() +
                                Z.Rows[i]["STR_08"].ToString() + Z.Rows[i]["STR_09"].ToString() + Z.Rows[i]["STR_11"].ToString() + "  " + Z.Rows[i]["STR_12"].ToString() + "          " +
                                Z.Rows[i]["STR_13"].ToString() + Z.Rows[i]["STR_14"].ToString() + "                                                   " + Z.Rows[i]["STR_15"].ToString() + "             " +
                                Z.Rows[i]["STR_16"].ToString() + str + Z.Rows[i]["STR_17"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "SCAN");
                    txtFile.Text = txtFile.Text + "  " + p_file_name;


                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            //kapila 18/11/2014 update grdd_is_sun_up
                            Int32 _eff = CHNLSVC.Financial.UpdateIsSUNUpload(txtComp.Text, pc, Convert.ToDateTime(lblFrmdtWk.Text), Convert.ToDateTime(lblTodtWk.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1));
                        }
                    }

                    //Scan Docs (bank charge)*******************************************************************
                    //p_file_name = p_sun_user_id + "CBCHG.txt";

                    ////check whether local path is exist
                    //p_source_path = @"C:\\SUN";

                    //if (!Directory.Exists(p_source_path))
                    //{
                    //    MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}
                    //p_source_path = p_source_path + "\\";
                    ////local file path to save 
                    //p_file_path = p_source_path + p_file_name;

                    //Int32 XX = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

                    //foreach (ListViewItem Item in lstPC.Items)
                    //{
                    //    string pc = Item.Text;

                    //    if (Item.Checked == true)
                    //    {
                    //        Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);
                    //    }
                    //}

                    //// bank charge
                    //DataTable Q = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1),Convert.ToDateTime(lblFrmdtWk.Text), Convert.ToDateTime(lblTodtWk.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, "BCHG", _strMonth);
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    //{
                    //    string _STR = string.Empty;
                    //    for (int i = 0; i < Q.Rows.Count; i++)
                    //    {
                    //        String str = new String(' ', 75 - Q.Rows[i]["STR_16"].ToString().Length);

                    //        _STR = Q.Rows[i]["STR_01"].ToString() + "      " +
                    //            Q.Rows[i]["STR_02"].ToString() + Q.Rows[i]["STR_03"].ToString() + "  " + Q.Rows[i]["STR_04"].ToString() +
                    //            Q.Rows[i]["STR_05"].ToString() + Q.Rows[i]["STR_06"].ToString() + "  " + Q.Rows[i]["STR_07"].ToString() +
                    //            Q.Rows[i]["STR_08"].ToString() + Q.Rows[i]["STR_09"].ToString() + Q.Rows[i]["STR_11"].ToString() + "  " + Q.Rows[i]["STR_12"].ToString() + "          " +
                    //            Q.Rows[i]["STR_13"].ToString() + Q.Rows[i]["STR_14"].ToString() + "                                                   " + Q.Rows[i]["STR_15"].ToString() + "             " +
                    //            Q.Rows[i]["STR_16"].ToString() + str + Q.Rows[i]["STR_17"].ToString();
                    //        file.WriteLine(_STR);
                    //    }

                    //    file.Close();
                    //}

                    //File_Copy(p_file_name, p_file_path, "SCAN");
                    //txtFile.Text = txtFile.Text + "  " + p_file_name;

                    MessageBox.Show("Successfully generated.", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region SOS
                //SOS upload--------------------------------------------------------------------------------------------------------------------------
                if (optSOS.Checked == true)
                {
                    //check whether date range is valid
                    Int32 _ok = CHNLSVC.Financial.IsValidWeekDataRange(Convert.ToInt32(cmbYear.Text), cmbMonth.SelectedIndex + 1, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, BaseCls.GlbUserComCode);
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
                  //create tharanga Erorr txt file
                    p_err_file_name = p_sun_user_id + txtPC.Text + "SCM2" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + "ERORRS" + ".txt";

                    //check whether local path is exist
                   
                    #region SOS Compare tax :: Tharanga 2017/10/05
                    
                    Decimal REM_VAL_FINAL = 0;
                    Decimal satx_itm_tax_amt = 0;
                    Decimal SAR_TOT_SETTLE_AMT=0;
                    var list = new List<Tuple<string, string, string>>();
                    //DataTable table = new DataTable();
                    //table.Columns.Add("PC", typeof(int));
                    //table.Columns.Add("Drug", typeof(string));
                    //table.Columns.Add("Patient", typeof(string));
                    //table.Columns.Add("Date", typeof(DateTime));
                    //foreach (ListViewItem Item in lstPC.Items)
                    //{
                    //    string pc = Item.Text;

                    //    if (Item.Checked == true)
                    //    {
                    //        DataTable _tbl1 = CHNLSVC.Financial.GNT_REM_SUM(pc, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, 0); //rem_is_sos=1
                    //        DataTable _tbl2 = CHNLSVC.Financial.GET_TAX_BY_INV_TYPE(pc, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, "CS");
                    //        DataTable _tbl3 = CHNLSVC.Financial.SAT_RECEIPT_ALL_DET(BaseCls.GlbUserComCode, pc, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date);
                    //        DataTable _tbl4 = CHNLSVC.Financial.SAT_Collec_summery(BaseCls.GlbUserComCode, pc, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, 0);//SAR_IS_OTH_SHOP=0
                    //        DataTable _tbl5 = CHNLSVC.Financial.SAT_Collec_summery(BaseCls.GlbUserComCode, pc, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, 1);//SAR_IS_OTH_SHOP=1
                    //        DataTable _tbl6 = CHNLSVC.Financial.GNT_REM_SUM(pc, Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, 1);// is day end 1

                    //        DataTable tblReceivableMovemnt = CHNLSVC.Sales.ReceivableMovemntReport(Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, BaseCls.GlbUserID, pc, BaseCls.GlbUserComCode, 1);
                    //        DataTable sos = CHNLSVC.Financial.ProcessSOS(Convert.ToDateTime(txtFrom.Text).Date, Convert.ToDateTime(txtTo.Text).Date, BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID);

                    //        var groupedData = from b in tblReceivableMovemnt.AsEnumerable()
                    //                          group b by b.Field<string>("PROFIT") into g
                    //                          select new
                    //                          {
                    //                              ChargeTag = g.Key,
                    //                              List = g.ToList(),
                    //                          } into g
                    //                          select new
                    //                          {
                    //                              g.ChargeTag,
                    //                              Count = g.List.Count,
                    //                              ECD = g.List.Sum(x => x.Field<decimal>("ECD")),
                    //                              HP_INT = g.List.Sum(x => x.Field<decimal>("HP_INT"))

                    //                          };

                    //        //if (_tbl1.Rows.Count > 0 && _tbl2.Rows.Count > 0)
                    //        //{
                    //        #region Cash Sales -Vat


                    //        //DataTable dtcs = _tbl1.AsEnumerable().Where(r => r.Field<string>("REM_CD") == "007" && r.Field<string>("REM_SEC") == "01").CopyToDataTable();
                    //        //_totalLessFinal = Convert.ToDecimal(_tbl1.AsEnumerable().Where(y => y.Field<string>("REM_CD") == "007" && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        //decimal a = dtcs.AsEnumerable().Sum(r => r.Field<decimal>("REM_VAL_FINAL"));

                    //        //DataTable dtcs2 = _tbl2.AsEnumerable().Where(r => r.Field<string>("sah_inv_tp") == "CS").CopyToDataTable();
                    //        //decimal b = dtcs2.AsEnumerable().Sum(r => r.Field<decimal>("satx_itm_tax_amt"));

                    //        REM_VAL_FINAL = Convert.ToDecimal(_tbl1.AsEnumerable().Where(y => y.Field<string>("REM_CD") == "007" && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        satx_itm_tax_amt = Convert.ToDecimal(_tbl2.AsEnumerable().Where(y => y.Field<string>("sah_inv_tp") == "CS").Sum(y => y.Field<decimal>("satx_itm_tax_amt")).ToString());
                    //        if ((REM_VAL_FINAL - satx_itm_tax_amt) < -10 || (REM_VAL_FINAL - satx_itm_tax_amt) > 10)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Cash Sales -Vat", "Cash Sales -Vat deferrent"));
                    //            //MessageBox.Show("Cash Sales -Vat deferrent in " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        #region Credit Sales - Vat
                    //        //DataTable dtcd = _tbl1.AsEnumerable().Where(r => r.Field<string>("REM_CD") == "009" && r.Field<string>("REM_SEC") == "01").CopyToDataTable();
                    //        //decimal aa = dtcd.AsEnumerable().Sum(r => r.Field<decimal>("REM_VAL_FINAL"));

                    //        //DataTable dtcd2 = _tbl2.AsEnumerable().Where(r => r.Field<string>("sah_inv_tp") == "CRED").CopyToDataTable();
                    //        //decimal bb = dtcd2.AsEnumerable().Sum(r => r.Field<decimal>("satx_itm_tax_amt"));
                    //        REM_VAL_FINAL = Convert.ToDecimal(_tbl1.AsEnumerable().Where(y => y.Field<string>("REM_CD") == "009" && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        satx_itm_tax_amt = Convert.ToDecimal(_tbl2.AsEnumerable().Where(y => y.Field<string>("sah_inv_tp") == "CRED").Sum(y => y.Field<decimal>("satx_itm_tax_amt")).ToString());
                    //        if ((REM_VAL_FINAL - satx_itm_tax_amt) < -10 || (REM_VAL_FINAL - satx_itm_tax_amt) > 10)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Credit Sales -Vat", "Credit Sales - Vat deferrent"));
                    //            //MessageBox.Show("Credit Sales - Vat deferrent in " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        #region Hire Sales - Vat
                    //        //DataTable dths = _tbl1.AsEnumerable().Where(r => r.Field<string>("REM_CD") == "008" && r.Field<string>("REM_SEC") == "01").CopyToDataTable();
                    //        //decimal c = dths.AsEnumerable().Sum(r => r.Field<decimal>("REM_VAL_FINAL"));

                    //        //DataTable dths2 = _tbl2.AsEnumerable().Where(r => r.Field<string>("sah_inv_tp") == "HS").CopyToDataTable();
                    //        //decimal d = dths2.AsEnumerable().Sum(r => r.Field<decimal>("satx_itm_tax_amt"));
                    //        REM_VAL_FINAL = Convert.ToDecimal(_tbl1.AsEnumerable().Where(y => y.Field<string>("REM_CD") == "008" && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        satx_itm_tax_amt = Convert.ToDecimal(_tbl2.AsEnumerable().Where(y => y.Field<string>("sah_inv_tp") == "HS").Sum(y => y.Field<decimal>("satx_itm_tax_amt")).ToString());

                    //        if ((REM_VAL_FINAL - satx_itm_tax_amt) < -10 || (REM_VAL_FINAL - satx_itm_tax_amt) > 10)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Hire Sales -Vat", "Hire Sales - Vat deferrentt"));
                    //            //MessageBox.Show("Hire Sales - Vat deferrent in " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        #region Vat/Dp & Ser Charge
                    //        REM_VAL_FINAL = Convert.ToDecimal(_tbl6.AsEnumerable().Where(y => (y.Field<string>("REM_CD") == "003" || y.Field<string>("REM_CD") == "008") && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        SAR_TOT_SETTLE_AMT = Convert.ToDecimal(_tbl3.AsEnumerable().Where(y => y.Field<string>("SAR_RECEIPT_TYPE") == "HPDPM" || y.Field<string>("SAR_RECEIPT_TYPE") == "HPDPS" || y.Field<string>("SAR_RECEIPT_TYPE") == "HPDRV").Sum(y => y.Field<decimal>("SAR_TOT_SETTLE_AMT")).ToString());
                    //        if ((REM_VAL_FINAL - SAR_TOT_SETTLE_AMT) < -10 || (REM_VAL_FINAL - SAR_TOT_SETTLE_AMT) > 10)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Cash Sales -Vat", "Vat/Dp & Ser Charge deferrentt"));
                    //            //MessageBox.Show("Vat/Dp & Ser Charge " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        #region Hp Installments

                    //        REM_VAL_FINAL = Convert.ToDecimal(_tbl6.AsEnumerable().Where(y => y.Field<string>("REM_CD") == "004" && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        if (_tbl4.Rows.Count > 0)
                    //        {
                    //            SAR_TOT_SETTLE_AMT = Convert.ToDecimal(_tbl4.Rows[0][0]);
                    //        }


                    //        //SAR_TOT_SETTLE_AMT = Convert.ToDecimal(_tbl3.AsEnumerable().Where(y => y.Field<string>("SAR_RECEIPT_TYPE") == "HPRM" && y.Field<string>("SAR_RECEIPT_TYPE") == "HPRS" 
                    //        //    && y.Field<string>("SAR_RECEIPT_TYPE") == "HPARM" && y.Field<string>("SAR_RECEIPT_TYPE") == "HPARS" && y.Field<string>("SAR_RECEIPT_TYPE") == "HPREV").Sum(y => y.Field<decimal>("SAR_TOT_SETTLE_AMT")).ToString());
                    //        if ((REM_VAL_FINAL - SAR_TOT_SETTLE_AMT) < -10 || (REM_VAL_FINAL - SAR_TOT_SETTLE_AMT) > 10)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Hp Installments", "Vat defferent"));
                    //            //MessageBox.Show("Hp Installments " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        #region Other Shop Collection
                    //        REM_VAL_FINAL = Convert.ToDecimal(_tbl6.AsEnumerable().Where(y => y.Field<string>("REM_CD") == "005" && y.Field<string>("REM_SEC") == "01").Sum(y => y.Field<decimal>("REM_VAL_FINAL")).ToString());
                    //        if (_tbl5.Rows.Count > 0)
                    //        {
                    //            SAR_TOT_SETTLE_AMT = Convert.ToDecimal(_tbl5.Rows[0][0]);
                    //        }

                    //        //SAR_TOT_SETTLE_AMT = Convert.ToDecimal(_tbl3.AsEnumerable().Where(y => y.Field<string>("SAR_RECEIPT_TYPE") == "HPRM" && y.Field<string>("SAR_RECEIPT_TYPE") == "HPRS" 
                    //        //    && y.Field<string>("SAR_RECEIPT_TYPE") == "HPARM" && y.Field<string>("SAR_RECEIPT_TYPE") == "HPARS" && y.Field<string>("SAR_RECEIPT_TYPE") == "HPREV").Sum(y => y.Field<decimal>("SAR_TOT_SETTLE_AMT")).ToString());

                    //        if ((REM_VAL_FINAL - SAR_TOT_SETTLE_AMT) < -10 || (REM_VAL_FINAL - SAR_TOT_SETTLE_AMT) > 10)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Other Shop Collection", "Collection defferent"));
                    //            //MessageBox.Show("Other Shop Collection " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion

                    //        #region Validate ECD
                    //        decimal ReceivableMovemntAmount = Convert.ToDecimal(groupedData.FirstOrDefault().ECD.ToString());
                    //        // Convert.ToDecimal(tblReceivableMovemnt.Rows[0]["ECD"]);
                    //        decimal ECDnormal = Convert.ToDecimal(sos.AsEnumerable().Where(y => y.Field<string>("SOS_DESC") == "ECD NORMAL" || y.Field<string>("SOS_DESC") == "ECD SPECIAL").Sum(y => y.Field<decimal>("SOS_1S") + y.Field<decimal>("SOS_1V")
                    //            + y.Field<decimal>("SOS_2S") + y.Field<decimal>("SOS_2V")
                    //            + y.Field<decimal>("SOS_3S") + y.Field<decimal>("SOS_3V")
                    //            + y.Field<decimal>("SOS_4S") + y.Field<decimal>("SOS_4V")
                    //            + y.Field<decimal>("SOS_5S") + y.Field<decimal>("SOS_5V")).ToString());
                    //        if (ReceivableMovemntAmount != ECDnormal)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Validate ECD ", "ECD Value Different "));
                    //            //MessageBox.Show("ECD Value Different " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        #region Hire Purchase Interest
                    //        decimal HPINT = Convert.ToDecimal(groupedData.FirstOrDefault().HP_INT.ToString());
                    //        // Convert.ToDecimal(tblReceivableMovemnt.Rows[0]["ECD"]);LESS : INTEREST
                    //        decimal HPINTSOS = Convert.ToDecimal(sos.AsEnumerable().Where(y => y.Field<string>("SOS_DESC") == "LESS : INTEREST").Sum(y => y.Field<decimal>("SOS_1S") + y.Field<decimal>("SOS_1V")
                    //         + y.Field<decimal>("SOS_2S") + y.Field<decimal>("SOS_2V")
                    //         + y.Field<decimal>("SOS_3S") + y.Field<decimal>("SOS_3V")
                    //         + y.Field<decimal>("SOS_4S") + y.Field<decimal>("SOS_4V")
                    //         + y.Field<decimal>("SOS_5S") + y.Field<decimal>("SOS_5V")).ToString());
                    //        if (HPINT != HPINTSOS)
                    //        {
                    //            list.Add(Tuple.Create(pc, "Hire Purchase Interest", "Hire Purchase Interest Different"));
                    //            //MessageBox.Show("Hire Purchase Interest Different " + pc, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            //return;

                    //        }
                    //        #endregion
                    //        // }
                    //    }
                    //}

                    #endregion
                    
                    p_source_path = p_source_path + "\\";

                    #region write eroor txt
                    //if (list.Count > 0)
                    //{
                    //    p_file_path = p_source_path + p_err_file_name;
                    //    grdErrs.DataSource = list;
                    //    this.pnlErr.Size = new System.Drawing.Size(508, 166);
                    //    this.pnlErr.Location = new System.Drawing.Point(20, 28);
                    //    pnlErr.Visible = true;
                    //    using (System.IO.StreamWriter filerr = new System.IO.StreamWriter(p_file_path))
                    //    {
                    //        string _STR = string.Empty;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            _STR = list[i].ToString();
                    //            filerr.WriteLine(_STR);
                    //        }

                    //        filerr.Close();
                    //    }
                    //    File_Copy(p_err_file_name, p_file_path, "SOS");
                    // //   txtFile.Text = p_file_name;
                    //    if (MessageBox.Show("Do you want to save?", "Confirm save", MessageBoxButtons.YesNo) == DialogResult.No)
                    //    {
                    //        return;
                    //    }
                    //    pnlErr.Visible = false;
                    //}
                    #endregion
                    if (!Directory.Exists(p_source_path))
                    {

                        MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //p_source_path = p_source_path + "\\";
                    //local file path to save 
                    p_file_path = p_source_path + p_file_name;

                    MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);  //7/5/2015
                    DataTable X = new DataTable();
                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            DataTable tmpX = CHNLSVC.Financial.ProcessSUNUpload(cmbMonth.Text, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path, mst_com.Mc_anal24);
                            X.Merge(tmpX);
                        }
                    }
                    // add by tharanga 2017/09/13
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
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region Dealer Invoice
                //Dealer invoices upload--------------------------------------------------------------------------------------------------------------------------
                if (optDInv.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                    if (txtPC.Text != "")
                    {
                        p_file_name = txtPC.Text + "INV" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }
                    else//Sanjeewa 2015-07-07
                    {
                        if (txtChanel.Text=="")
                        {
                            MessageBox.Show("Channel or profit center not selected.", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        p_file_name = txtChanel.Text.Substring(1,2) + "INV" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }

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

                    DataTable X = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        X = CHNLSVC.Financial.ProcessSUNUpload_Invoice(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, 1, p_sun_user_id, p_file_path);
                    }
                    else
                    {
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            string pc = Item.Text;

                            if (Item.Checked == true)
                            {
                                DataTable X1 = CHNLSVC.Financial.ProcessSUNUpload_Invoice(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, 1, p_sun_user_id, p_file_path);
                                X.Merge(X1);
                            }
                        }
                    }
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "DINV");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region Dealer Receipt
                //Receipts upload--------------------------------------------------------------------------------------------------------------------------
                if (optDRec.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                     if (txtPC.Text != "")
                    {
                    p_file_name = txtPC.Text + "RC" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }
                     else//Sanjeewa 2015-07-07
                     {
                         if (txtChanel.Text == "")
                         {
                             MessageBox.Show("Channel or profit center not selected.", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                             this.Cursor = Cursors.Default;
                             return;
                         }
                         p_file_name = txtChanel.Text.Substring(1, 2) + "RC" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                     }

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

                    DataTable X = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        X = CHNLSVC.Financial.ProcessSUNUpload_Receipt(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                    }
                    else
                    {
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            string pc = Item.Text;

                            if (Item.Checked == true)
                            {
                                DataTable X1 = CHNLSVC.Financial.ProcessSUNUpload_Receipt(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                                X.Merge(X1);
                            }
                        }
                    }
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                        
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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


                    //int Y = CHNLSVC.Financial.Generate_SOS_Text_File(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);

                    File_Copy(p_file_name, p_file_path, "REC");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region internal voucher entry
                if (optVouEntry.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtChanel.Text))
                    {
                        MessageBox.Show("Select the channel", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "VOUE" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_PayVouEntry(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtChanel.Text, vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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
                                X.Rows[i]["STR_34"].ToString() + X.Rows[i]["STR_36"].ToString() + X.Rows[i]["STR_34"].ToString() +
                                X.Rows[i]["STR_38"].ToString() + X.Rows[i]["STR_34"].ToString() + X.Rows[i]["STR_37"].ToString();
                            file.WriteLine(_STR);
                        }
                        //X.Rows[i]["STR_34"].ToString() is used to get 15 spaces
                        //X.Rows[i]["STR_35"].ToString() is the department code
                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "VOUENRY");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                #endregion


                #region Cheque Printing
                if (rdoCheqPrinting.Checked == true)
                {

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

                    if (string.IsNullOrEmpty(txtAccNo.Text))
                    {
                        MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "CHQ" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_ChequePrnt(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtAccNo.Text.Trim(), vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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
                        //X.Rows[i]["STR_34"].ToString() is used to get 15 spaces
                        //X.Rows[i]["STR_35"].ToString() is the department code
                        file.Close();
                    }

                    //File_Copy(p_file_name, p_file_path, "CHQ");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);





                }
                #endregion

                #region Loyality
                if(rdoLoyalty.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtPC.Text))
                    {
                        MessageBox.Show("Select the profit center", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "LOY" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_Loyalty(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtPC.Text.Trim(), vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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
                        //X.Rows[i]["STR_34"].ToString() is used to get 15 spaces
                        //X.Rows[i]["STR_35"].ToString() is the department code
                        file.Close();
                    }

                    //File_Copy(p_file_name, p_file_path, "CHQ");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                #endregion


                #region Fund

                if (rdoFund.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtAccNo.Text))
                    {
                        MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "FUND" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_Fund(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtAccNo.Text.Trim(), vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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
                        //X.Rows[i]["STR_34"].ToString() is used to get 15 spaces
                        //X.Rows[i]["STR_35"].ToString() is the department code
                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "FUND");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }



                #endregion


                #region internal voucher claim
                if (optVouClaim.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtChanel.Text))
                    {
                        MessageBox.Show("Select the channel", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "VOUC" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);
                        }
                    }

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_PayVouClaim(Convert.ToDateTime(lblFrmdtWk.Text), Convert.ToDateTime(lblTodtWk.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtChanel.Text, vAccPeriod, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "VOUCLM");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                #endregion

                #region fixed asset
                if (opt_FA.Checked == true)
                {
                    clsInventoryRep objInv = new clsInventoryRep();

                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFrom.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtTo.Text);
                    BaseCls.GlbReportName = "FixedAssetForSUN.rpt";
                    objInv.FixedAsset();

                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;

                    objInv._FixedAsset.ExportToDisk(ExportFormatType.Excel, _path + "FixedAssetForSUN" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "FixedAssetForSUN" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);
                }

                #endregion

                #region product bonus
                //--------------------------------------------------------------------------------------------------------------------------
                if (optPB.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                    p_file_name = "PB" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

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

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_PBonus(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, 1, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "PB");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region AOA Invoice
                //AOA invoices upload--------------------------------------------------------------------------------------------------------------------------
                if (optAOA.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                    p_file_name =  txtPC.Text + Convert.ToDateTime(txtFrom.Text).ToString("yyyyMMdd") + ".txt";

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

                    DataTable X = CHNLSVC.Financial.ProcessSUNUpload_AOAInvoice(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, 1, p_sun_user_id, p_file_path);
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "AOAINV");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region bank charges
                if (optBnkChrg.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtAccNo.Text))
                    {
                        MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "BCHG" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

                    string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

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
                    DataTable Q = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, txtAccNo.Text + "|" + p_sun_user_id , "BCHG", _strMonth,mst_com.Mc_anal24);
                    // add by tharanga 2017/09/13
                    if (Q.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < Q.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - Q.Rows[i]["STR_16"].ToString().Length);

                            _STR = Q.Rows[i]["STR_01"].ToString() +
                                Q.Rows[i]["STR_02"].ToString() + Q.Rows[i]["STR_03"].ToString() + Q.Rows[i]["STR_04"].ToString() +
                                Q.Rows[i]["STR_05"].ToString() + Q.Rows[i]["STR_06"].ToString() + Q.Rows[i]["STR_07"].ToString() +
                                Q.Rows[i]["STR_08"].ToString() + Q.Rows[i]["STR_09"].ToString() + Q.Rows[i]["STR_10"].ToString() +
                                Q.Rows[i]["STR_11"].ToString() + Q.Rows[i]["STR_12"].ToString() + Q.Rows[i]["STR_13"].ToString() +
                                Q.Rows[i]["STR_14"].ToString() + Q.Rows[i]["STR_15"].ToString() + Q.Rows[i]["STR_16"].ToString() +
                                Q.Rows[i]["STR_17"].ToString() + Q.Rows[i]["STR_18"].ToString() + Q.Rows[i]["STR_19"].ToString() +
                                Q.Rows[i]["STR_20"].ToString() + Q.Rows[i]["STR_21"].ToString() + Q.Rows[i]["STR_22"].ToString() +
                                Q.Rows[i]["STR_23"].ToString() + Q.Rows[i]["STR_24"].ToString() + Q.Rows[i]["STR_25"].ToString() +
                                Q.Rows[i]["STR_26"].ToString() + Q.Rows[i]["STR_27"].ToString() + Q.Rows[i]["STR_28"].ToString() +
                                Q.Rows[i]["STR_29"].ToString() + Q.Rows[i]["STR_30"].ToString() + Q.Rows[i]["STR_31"].ToString() +
                                Q.Rows[i]["STR_32"].ToString() + Q.Rows[i]["STR_33"].ToString() + Q.Rows[i]["STR_34"].ToString() +
                                Q.Rows[i]["STR_35"].ToString() + Q.Rows[i]["STR_36"].ToString() + Q.Rows[i]["STR_37"].ToString() +
                                Q.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "BNKCHG");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                #endregion

                #region service Invoice
                //service invoices upload--------------------------------------------------------------------------------------------------------------------------
                if (optService.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                    if (txtPC.Text != "")
                    {
                        p_file_name = txtPC.Text + "INV" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }
                    else//Sanjeewa 2015-07-07
                    {
                        p_file_name = p_pc_hrchy.Substring(1, 2) + "INV" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }

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

                    DataTable X = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        X = CHNLSVC.Financial.ProcessSUNUpload_Common(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, "SERVICE","SALES", p_sun_user_id, p_file_path);
                    }
                    else
                    {
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            string pc = Item.Text;

                            if (Item.Checked == true)
                            {
                                DataTable X1 = CHNLSVC.Financial.ProcessSUNUpload_Common(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod,"SERVICE" ,"SALES", p_sun_user_id, p_file_path);
                                X.Merge(X1);
                            }
                        }
                    }
                    // add by tharanga 2017/09/13
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any service invoices record found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "SVCINV");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region service receipts
                //service receipts upload--------------------------------------------------------------------------------------------------------------------------
                if (optServRec.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                    if (txtPC.Text != "")
                    {
                        p_file_name = txtPC.Text + "REC" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }
                    else//Sanjeewa 2015-07-07
                    {
                        p_file_name = p_pc_hrchy.Substring(1, 2) + "REC" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }

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

                    //add by akila 2017/08/17
                    DataTable _invalidReceipts = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        _invalidReceipts = CHNLSVC.Financial.GetInvalidReceiptsForSunUpload(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text));
                    }
                    else
                    {
                        if(lstPC.Items != null  && lstPC.Items.Count > 0)
                        {
                            foreach (ListViewItem _item in lstPC.Items)
                            {
                                if (_item.Checked == true)
                                {
                                    _invalidReceipts.Merge(CHNLSVC.Financial.GetInvalidReceiptsForSunUpload(BaseCls.GlbUserComCode, _item.Text, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text)));
                                }
                            }
                        }
                    }

                    if (_invalidReceipts.Rows.Count > 0) 
                    {
                        btnCreate.Enabled = false;
                        BindInvalidReceipts(_invalidReceipts);
                        pnlInvalidReceipts.Visible = true;
                        return;
                    }
                    

                    DataTable X = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        X = CHNLSVC.Financial.ProcessSUNUpload_Common(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, "SERVICE", "REC", p_sun_user_id, p_file_path);
                    }
                    else
                    {
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            string pc = Item.Text;

                            if (Item.Checked == true)
                            {
                                DataTable X1 = CHNLSVC.Financial.ProcessSUNUpload_Common(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, "SERVICE", "REC", p_sun_user_id, p_file_path);
                                X.Merge(X1);
                            }
                        }
                    }
                    // add by tharanga 2017/09/12
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("Records not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "SVCREC");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                #region JAD Sales   --------------------------------------------------------------------------------------------------------------------------
                if (optJad.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //file name
                    if (txtPC.Text != "")
                    {
                        p_file_name = txtPC.Text + "SA" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }
                    else//Sanjeewa 2015-07-07
                    {
                        if (txtChanel.Text == "")
                        {
                            MessageBox.Show("Channel or profit center not selected.", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        p_file_name = txtChanel.Text.Substring(1, 2) + "RC" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }

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

                    DataTable X = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        X = CHNLSVC.Financial.ProcessSUNUpload_JAD(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), "JAD", txtPC.Text, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                    }
                    else
                    {
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            string pc = Item.Text;

                            if (Item.Checked == true)
                            {
                                DataTable X1 = CHNLSVC.Financial.ProcessSUNUpload_JAD(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                                X.Merge(X1);
                            }
                        }
                    }
                    //ADD bY THARANGA
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("Records not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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


                    //int Y = CHNLSVC.Financial.Generate_SOS_Text_File(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);

                    File_Copy(p_file_name, p_file_path, "JAD");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion
                #region Credit Note
                if (optwrrcredt.Checked == true)
                {
                    SystemUser _systemUser = null;
                    _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                    //string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");

                    //update temporary table
                    update_PC_List();
                    p_sun_user_id = _systemUser.Se_SUN_ID;
                    if (string.IsNullOrEmpty(p_sun_user_id))
                    {

                        //MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //this.Cursor = Cursors.Default;
                        //return;
                    }
                    //file name
                    if (txtPC.Text != "")
                    {
                        p_file_name = txtPC.Text + "SA" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }
                    else//Sanjeewa 2015-07-07
                    {
                        if (txtChanel.Text == "")
                        {
                          //  MessageBox.Show("Channel or profit center not selected.", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                           // this.Cursor = Cursors.Default;
                            //return;
                        }
                        p_file_name = txtChanel.Text.Substring(1, 2) + "RC" + Convert.ToDateTime(txtTo.Text).ToString("yyMMdd") + ".txt";
                    }

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

                    DataTable X = new DataTable();
                    if (txtPC.Text != "")//Sanjeewa 2015-07-07
                    {
                        X = CHNLSVC.Financial.ProcessSUNUpload_Cedit_note(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                    }
                    else
                    {
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            string pc = Item.Text;

                            if (Item.Checked == true)
                            {
                                DataTable X1 = CHNLSVC.Financial.ProcessSUNUpload_Cedit_note(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, pc, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);
                                X.Merge(X1);
                            }
                        }
                    }
                    //ADD bY THARANGA
                    if (X.Rows.Count <= 0)
                    {
                        MessageBox.Show("Records not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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


                    //int Y = CHNLSVC.Financial.Generate_SOS_Text_File(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, vAccPeriod, p_sun_user_id, p_file_path);

                    File_Copy(p_file_name, p_file_path, "WARCRDT");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                #endregion

                #region BANK REC
                if (optBnkRecom.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtAccNo.Text))
                    {
                        MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "BNKREC" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

                    string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

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
                    DataTable U = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime("1900/Jan/01"), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, txtAccNo.Text + "|" + p_sun_user_id, "BNKREC", _strMonth, mst_com.Mc_anal24);
                    // add by tharanga 2017/09/13
                    if (U.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < U.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - U.Rows[i]["STR_16"].ToString().Length);

                            _STR = U.Rows[i]["STR_01"].ToString() +
                                U.Rows[i]["STR_02"].ToString() + U.Rows[i]["STR_03"].ToString() + U.Rows[i]["STR_04"].ToString() +
                                U.Rows[i]["STR_05"].ToString() + U.Rows[i]["STR_06"].ToString() + U.Rows[i]["STR_07"].ToString() +
                                U.Rows[i]["STR_08"].ToString() + U.Rows[i]["STR_09"].ToString() + U.Rows[i]["STR_10"].ToString() +
                                U.Rows[i]["STR_11"].ToString() + U.Rows[i]["STR_12"].ToString() + U.Rows[i]["STR_13"].ToString() +
                                U.Rows[i]["STR_14"].ToString() + U.Rows[i]["STR_15"].ToString() + U.Rows[i]["STR_16"].ToString() +
                                U.Rows[i]["STR_17"].ToString() + U.Rows[i]["STR_18"].ToString() + U.Rows[i]["STR_19"].ToString() +
                                U.Rows[i]["STR_20"].ToString() + U.Rows[i]["STR_21"].ToString() + U.Rows[i]["STR_22"].ToString() +
                                U.Rows[i]["STR_23"].ToString() + U.Rows[i]["STR_24"].ToString() + U.Rows[i]["STR_25"].ToString() +
                                U.Rows[i]["STR_26"].ToString() + U.Rows[i]["STR_27"].ToString() + U.Rows[i]["STR_28"].ToString() +
                                U.Rows[i]["STR_29"].ToString() + U.Rows[i]["STR_30"].ToString() + U.Rows[i]["STR_31"].ToString() +
                                U.Rows[i]["STR_32"].ToString() + U.Rows[i]["STR_33"].ToString() + U.Rows[i]["STR_34"].ToString() +
                                U.Rows[i]["STR_35"].ToString() + U.Rows[i]["STR_36"].ToString() + U.Rows[i]["STR_37"].ToString() +
                                U.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "BNKREC");
                    txtFile.Text = p_file_name;

                    p_file_name = p_sun_user_id + "BNKRECOTH"+ ".txt";

                    //string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

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

                    //MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);  //7/5/2015
                    DataTable Q = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime("1900/Jan/01"), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, txtAccNo.Text + "|" + p_sun_user_id, "BNKRECOTH", _strMonth, mst_com.Mc_anal24);
                    // add by tharanga 2017/09/13
                    //if (Q.Rows.Count <= 0)
                    //{
                    //    MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < Q.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - Q.Rows[i]["STR_16"].ToString().Length);

                            _STR = Q.Rows[i]["STR_01"].ToString() +
                                Q.Rows[i]["STR_02"].ToString() + Q.Rows[i]["STR_03"].ToString() + Q.Rows[i]["STR_04"].ToString() +
                                Q.Rows[i]["STR_05"].ToString() + Q.Rows[i]["STR_06"].ToString() + Q.Rows[i]["STR_07"].ToString() +
                                Q.Rows[i]["STR_08"].ToString() + Q.Rows[i]["STR_09"].ToString() + Q.Rows[i]["STR_10"].ToString() +
                                Q.Rows[i]["STR_11"].ToString() + Q.Rows[i]["STR_12"].ToString() + Q.Rows[i]["STR_13"].ToString() +
                                Q.Rows[i]["STR_14"].ToString() + Q.Rows[i]["STR_15"].ToString() + Q.Rows[i]["STR_16"].ToString() +
                                Q.Rows[i]["STR_17"].ToString() + Q.Rows[i]["STR_18"].ToString() + Q.Rows[i]["STR_19"].ToString() +
                                Q.Rows[i]["STR_20"].ToString() + Q.Rows[i]["STR_21"].ToString() + Q.Rows[i]["STR_22"].ToString() +
                                Q.Rows[i]["STR_23"].ToString() + Q.Rows[i]["STR_24"].ToString() + Q.Rows[i]["STR_25"].ToString() +
                                Q.Rows[i]["STR_26"].ToString() + Q.Rows[i]["STR_27"].ToString() + Q.Rows[i]["STR_28"].ToString() +
                                Q.Rows[i]["STR_29"].ToString() + Q.Rows[i]["STR_30"].ToString() + Q.Rows[i]["STR_31"].ToString() +
                                Q.Rows[i]["STR_32"].ToString() + Q.Rows[i]["STR_33"].ToString() + Q.Rows[i]["STR_34"].ToString() +
                                Q.Rows[i]["STR_35"].ToString() + Q.Rows[i]["STR_36"].ToString() + Q.Rows[i]["STR_37"].ToString() +
                                Q.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "BNKRECOTH");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                #endregion
                #region Credit Card REC

                if (optcredrecon.Checked == true)
                {
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

                    if (string.IsNullOrEmpty(txtAccNo.Text))
                    {
                        MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    p_file_name = p_sun_user_id + "CRCDREC" + ".txt";

                    string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

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
                    DataTable U = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime("1900/Jan/01"), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, txtAccNo.Text + "|" + p_sun_user_id, "CRCDREC", _strMonth, mst_com.Mc_anal24);
                    // add by tharanga 2017/09/13
                    if (U.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < U.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - U.Rows[i]["STR_16"].ToString().Length);

                            _STR = U.Rows[i]["STR_01"].ToString() +
                                U.Rows[i]["STR_02"].ToString() + U.Rows[i]["STR_03"].ToString() + U.Rows[i]["STR_04"].ToString() +
                                U.Rows[i]["STR_05"].ToString() + U.Rows[i]["STR_06"].ToString() + U.Rows[i]["STR_07"].ToString() +
                                U.Rows[i]["STR_08"].ToString() + U.Rows[i]["STR_09"].ToString() + U.Rows[i]["STR_10"].ToString() +
                                U.Rows[i]["STR_11"].ToString() + U.Rows[i]["STR_12"].ToString() + U.Rows[i]["STR_13"].ToString() +
                                U.Rows[i]["STR_14"].ToString() + U.Rows[i]["STR_15"].ToString() + U.Rows[i]["STR_16"].ToString() +
                                U.Rows[i]["STR_17"].ToString() + U.Rows[i]["STR_18"].ToString() + U.Rows[i]["STR_19"].ToString() +
                                U.Rows[i]["STR_20"].ToString() + U.Rows[i]["STR_21"].ToString() + U.Rows[i]["STR_22"].ToString() +
                                U.Rows[i]["STR_23"].ToString() + U.Rows[i]["STR_24"].ToString() + U.Rows[i]["STR_25"].ToString() +
                                U.Rows[i]["STR_26"].ToString() + U.Rows[i]["STR_27"].ToString() + U.Rows[i]["STR_28"].ToString() +
                                U.Rows[i]["STR_29"].ToString() + U.Rows[i]["STR_30"].ToString() + U.Rows[i]["STR_31"].ToString() +
                                U.Rows[i]["STR_32"].ToString() + U.Rows[i]["STR_33"].ToString() + U.Rows[i]["STR_34"].ToString() +
                                U.Rows[i]["STR_35"].ToString() + U.Rows[i]["STR_36"].ToString() + U.Rows[i]["STR_37"].ToString() +
                                U.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "CRCDREC");
                    txtFile.Text = p_file_name;

                    p_file_name = p_sun_user_id + "BNKRECOTH" + ".txt";
                    p_source_path = @"C:\\SUN";

                    if (!Directory.Exists(p_source_path))
                    {
                        MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                  
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                #endregion

                #region MC Donalds GRN Upload

                if (optmacgrn.Checked == true)
                {
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

                    //if (string.IsNullOrEmpty(txtAccNo.Text))
                    //{
                    //    MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}

                    p_file_name = p_sun_user_id + "MCINV" /*+ Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd")*/ + ".txt";

                    string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

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
                    DataTable U = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, txtAccNo.Text + "|" + p_sun_user_id, "MCINV", _strMonth, mst_com.Mc_anal24);
                    // add by tharanga 2017/09/13
                    if (U.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < U.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - U.Rows[i]["STR_16"].ToString().Length);

                            _STR = U.Rows[i]["STR_01"].ToString() +
                                U.Rows[i]["STR_02"].ToString() + U.Rows[i]["STR_03"].ToString() + U.Rows[i]["STR_04"].ToString() +
                                U.Rows[i]["STR_05"].ToString() + U.Rows[i]["STR_06"].ToString() + U.Rows[i]["STR_07"].ToString() +
                                U.Rows[i]["STR_08"].ToString() + U.Rows[i]["STR_09"].ToString() + U.Rows[i]["STR_10"].ToString() +
                                U.Rows[i]["STR_11"].ToString() + U.Rows[i]["STR_12"].ToString() + U.Rows[i]["STR_13"].ToString() +
                                U.Rows[i]["STR_14"].ToString() + U.Rows[i]["STR_15"].ToString() + U.Rows[i]["STR_16"].ToString() +
                                U.Rows[i]["STR_17"].ToString() + U.Rows[i]["STR_18"].ToString() + U.Rows[i]["STR_19"].ToString() +
                                U.Rows[i]["STR_20"].ToString() + U.Rows[i]["STR_21"].ToString() + U.Rows[i]["STR_22"].ToString() +
                                U.Rows[i]["STR_23"].ToString() + U.Rows[i]["STR_24"].ToString() + U.Rows[i]["STR_25"].ToString() +
                                U.Rows[i]["STR_26"].ToString() + U.Rows[i]["STR_27"].ToString() + U.Rows[i]["STR_28"].ToString() +
                                U.Rows[i]["STR_29"].ToString() + U.Rows[i]["STR_30"].ToString() + U.Rows[i]["STR_31"].ToString() +
                                U.Rows[i]["STR_32"].ToString() + U.Rows[i]["STR_33"].ToString() + U.Rows[i]["STR_34"].ToString() +
                                U.Rows[i]["STR_35"].ToString() + U.Rows[i]["STR_36"].ToString() + U.Rows[i]["STR_37"].ToString() +
                                U.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "MCINV");
                    txtFile.Text = p_file_name;

                    p_file_name = p_sun_user_id + "MCINV" + ".txt";
                    p_source_path = @"C:\\SUN";

                    if (!Directory.Exists(p_source_path))
                    {
                        MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                #endregion

                #region MC Donalds Sales Upload 

                if (optmacsales.Checked == true)
                {
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

                    //if (string.IsNullOrEmpty(txtAccNo.Text))
                    //{
                    //    MessageBox.Show("Select the Account No", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    this.Cursor = Cursors.Default;
                    //    return;
                    //}

                    p_file_name = p_sun_user_id + "MCSALE" /*+ Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd")*/ + ".txt";

                    string _strMonth = cmbMonth.Text.Substring(0, 3).ToUpper();

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
                    DataTable U = CHNLSVC.Financial.ProcessSUNUpload_ScanDocs(Convert.ToDateTime(txtFrom.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), Convert.ToDateTime(txtTo.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, BaseCls.GlbUserID, vAccPeriod, txtAccNo.Text + "|" + p_sun_user_id, "MCSALE", _strMonth, mst_com.Mc_anal24);
                    // add by tharanga 2017/09/13
                    if (U.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any records found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < U.Rows.Count; i++)
                        {
                            String str = new String(' ', 75 - U.Rows[i]["STR_16"].ToString().Length);

                            _STR = U.Rows[i]["STR_01"].ToString() +
                                U.Rows[i]["STR_02"].ToString() + U.Rows[i]["STR_03"].ToString() + U.Rows[i]["STR_04"].ToString() +
                                U.Rows[i]["STR_05"].ToString() + U.Rows[i]["STR_06"].ToString() + U.Rows[i]["STR_07"].ToString() +
                                U.Rows[i]["STR_08"].ToString() + U.Rows[i]["STR_09"].ToString() + U.Rows[i]["STR_10"].ToString() +
                                U.Rows[i]["STR_11"].ToString() + U.Rows[i]["STR_12"].ToString() + U.Rows[i]["STR_13"].ToString() +
                                U.Rows[i]["STR_14"].ToString() + U.Rows[i]["STR_15"].ToString() + U.Rows[i]["STR_16"].ToString() +
                                U.Rows[i]["STR_17"].ToString() + U.Rows[i]["STR_18"].ToString() + U.Rows[i]["STR_19"].ToString() +
                                U.Rows[i]["STR_20"].ToString() + U.Rows[i]["STR_21"].ToString() + U.Rows[i]["STR_22"].ToString() +
                                U.Rows[i]["STR_23"].ToString() + U.Rows[i]["STR_24"].ToString() + U.Rows[i]["STR_25"].ToString() +
                                U.Rows[i]["STR_26"].ToString() + U.Rows[i]["STR_27"].ToString() + U.Rows[i]["STR_28"].ToString() +
                                U.Rows[i]["STR_29"].ToString() + U.Rows[i]["STR_30"].ToString() + U.Rows[i]["STR_31"].ToString() +
                                U.Rows[i]["STR_32"].ToString() + U.Rows[i]["STR_33"].ToString() + U.Rows[i]["STR_34"].ToString() +
                                U.Rows[i]["STR_35"].ToString() + U.Rows[i]["STR_36"].ToString() + U.Rows[i]["STR_37"].ToString() +
                                U.Rows[i]["STR_38"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "MCSALE");
                    txtFile.Text = p_file_name;

                    p_file_name = p_sun_user_id + "MCSALE" + ".txt";
                    p_source_path = @"C:\\SUN";

                    if (!Directory.Exists(p_source_path))
                    {
                        MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                #endregion

                if ((optElite.Checked == true)|| (rbdBlkEliteSale.Checked))
                {// Done by Nadeeka 02-July-2013
                    SunUploadElite();
                }
                if (opLoyal.Checked == true)
                {// Done by Nadeeka 10-July-2013
                    SunUploadLoyalityCard();
                }
                if (optDutyFree.Checked == true)
                {// Done by Nadeeka 02-July-2013
                    SunUploadDutyFree();
                }
                this.Cursor = Cursors.Default;
                //add  by tharanga 2017/09/14
                       
                        SendEMail(p_file_path);
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
        //add by tharanga 2017/09/14
        public int SendEMail(string p_file_name)
        {
            int result = 0;
            DataTable pclist = new DataTable();
            DataTable dtuser = new DataTable();
            string copyright = "Sirius Technologies Service (Pvt) Limited";
            string _ccAddress = "";
            string _recipientAddress="";
            try
            {
                dtuser = CHNLSVC.CustService.getServiceJobUser(BaseCls.GlbUserID);
                foreach (DataRow dr in dtuser.Rows)
                {
                    _ccAddress = dr["se_email"].ToString();
                    if (!string.IsNullOrEmpty(_ccAddress))
                    {
                        MailMessage mail = new MailMessage();
                        mail.Bcc.Add(_ccAddress);
                       // mail.CC.Add(_recipientAddress);
                        mail.From = new MailAddress("info@abansgroup.lk");
                        mail.Subject = "SUN UPLOAD";
                        mail.Body = "<html style='width: 590px;'>" +
                "<head> </head>" +
                "<body style='width: 450px; border: 2px solid rgb(156, 38, 204); padding: 15px; color: rgb(156, 38, 204);'>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<div style='color:#07000A'>" +
                            "<div>Dear Sir/Madam,</div><br/>" +
                            "<div>please find SUN upload Text File.</div><br/>" +
                            "<div>** This is an auto generated mail from SCM2 (Abans infor portal). Please don't Reply **</div>" +
                        "</div><br/>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<h4 style='margin-bottom: 0px;'>**" + copyright + "**</h4>" +
                    "<span style='font-family:Arial;font-size:10pt'> </span></body>" +
                "</html>";
                        mail.IsBodyHtml = true;
                        if (!string.IsNullOrEmpty(p_file_name))
                        {
                            Attachment data = new Attachment(p_file_name, MediaTypeNames.Application.Octet);
                            mail.Attachments.Add(data);
                        }
                        string fromAddress = "Admin";
                        SmtpClient client = new SmtpClient("192.168.1.204");
                        client.EnableSsl = false;
                        client.Credentials = new System.Net.NetworkCredential(fromAddress, "cha@123");
                        client.Send(mail);

                    }
                }

                pclist = CHNLSVC.CustService.get_msg_info_MAIL(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, doc_type);
                foreach (DataRow dr in pclist.Rows)
                {
                    _recipientAddress = dr["mmi_email"].ToString();

                    if (!string.IsNullOrEmpty(_recipientAddress))
                    {
                        MailMessage mail = new MailMessage();
                     //   mail.Bcc.Add(_ccAddress);
                        mail.CC.Add(_recipientAddress);
                        mail.From = new MailAddress("info@abansgroup.lk");
                        mail.Subject = "SUN UPLOAD";
                        mail.Body = "<html style='width: 590px;'>" +
                "<head> </head>" +
                "<body style='width: 450px; border: 2px solid rgb(156, 38, 204); padding: 15px; color: rgb(156, 38, 204);'>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<div style='color:#07000A'>" +
                            "<div>Dear Sir/Madam,</div><br/>" +
                            "<div>please find SUN upload Text File.</div><br/>" +
                            "<div>** This is an auto generated mail from SCM2 (Abans infor portal). Please don't Reply **</div>" +
                        "</div><br/>" +
                        "<div style='border-top:1px solid #9C26CC'> </div>" +
                        "<h4 style='margin-bottom: 0px;'>**" + copyright + "**</h4>" +
                    "<span style='font-family:Arial;font-size:10pt'> </span></body>" +
                "</html>"; 
                        mail.IsBodyHtml = true;
                        if (!string.IsNullOrEmpty(p_file_name))
                        {
                            Attachment data = new Attachment(p_file_name, MediaTypeNames.Application.Octet);
                            mail.Attachments.Add(data);
                        }               
                        string fromAddress = "Admin";
                        SmtpClient client = new SmtpClient("192.168.1.204");
                        client.EnableSsl = false;
                        client.Credentials = new System.Net.NetworkCredential(fromAddress, "cha@123");
                        client.Send(mail);
                  
                    }
            }
            }
            catch (Exception ex)
            {
                result = 0;
             //   _errorMessage = "Error occurred while sending the email. " + ex.Message + Environment.NewLine + ex.Source;
            }

            return result;
        }
        protected void File_Copy(string _fileName, string _sourceFilePath, string _upType)
        {
            try
            {
                //IWshNetwork_Class network = new IWshNetwork_Class();
                //network.MapNetworkDrive("O:", @"\\192.168.1.224\aal", Type.Missing, "cc\\kapila", "kap@321");

                //System.IO.File.Delete(@"O:\" + _fileName);
                //System.IO.File.Copy(@"C:\SUN\" + _fileName, @"O:\" + _fileName);
                if (_upType == "REC" || _upType == "DINV")
                {
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\aal\\" + _fileName, true);
                }
                else if (_upType == "SOS")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\sos\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\sos\\" + _fileName, true);
                }
                else if (_upType == "SCAN" || _upType == "PB")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\work\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\work\\" + _fileName, true);
                }
                                
                else if (_upType == "BNKCHG" || _upType == "FUND")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\work\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\work\\" + _fileName, true);
                }
                else if (_upType == "Elite")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\sgsp\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\sun426\\" + _fileName, true);
                }
                else if (_upType == "Loyl")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\sgsp\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\sun426\\" + _fileName, true);
                }
                else if (_upType == "RCHQ")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\upload\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\work\\" + _fileName, true);
                }
                else if (_upType == "RCHQS")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\upload\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\work\\" + _fileName, true);
                }
                else if (_upType == "VOUENRY" || _upType == "VOUCLM")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\work\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\work\\" + _fileName, true);
                }
                else if (_upType == "DutyFree")
                {
                    if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD" || BaseCls.GlbUserComCode == "PNG")
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.136\\sgsp\\" + _fileName, true);
                    else
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\SUN\\" + _fileName, true);
                }
                else if (_upType == "AOAINV")
                {
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.46\\sun\\POSINV\\" + _fileName, true);
                }
                else if (_upType == "CHQ")
                {
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\10.12.10.139\\work\\" + _fileName, true);
                }
                else if (_upType == "SVCINV" || _upType == "SVCREC")
                {
                    if(BaseCls.GlbUserComCode=="ABE" )  //28/3/2016 kapila
                        System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.46\\sun\\sun426\\" + _fileName, true);
                    else
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\sun426\\" + _fileName, true);
                }
                else if (_upType == "JAD" )
                {
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\aal\\" + _fileName, true);
                }
                else if (_upType == "WARCRDT")
                {
                    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\sun426\\" + _fileName, true);
                }
                //network.RemoveNetworkDrive("O:");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Uploading", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            txtTo.Text = lastDay.ToString("dd/MMM/yyyy");

            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
            txtFrom.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");

            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = cmbMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(cmbYear.Text);

            if (month != 0)
            {
                int numberOfDays = DateTime.DaysInMonth(year, month);
                DateTime lastDay = new DateTime(year, month, numberOfDays);

                txtTo.Text = lastDay.ToString("dd/MMM/yyyy");

                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
                txtFrom.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            }
        }

        private void optScan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optScan.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10015))
                    {
                        MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10015)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(4);
                }
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                string com = txtComp.Text;
                string chanel = txtChanel.Text;
                string subChanel = txtSChanel.Text;
                string area = txtArea.Text;
                string region = txtRegion.Text;
                string zone = txtZone.Text;
                string pc = txtPC1.Text;

                lstPC.Clear();
                if (optScan.Checked == true || optSOS.Checked == true || optService.Checked == true || optServRec.Checked == true)
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                    }
                }
                else
                {
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                    else
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                }
                txtPC.Text = "";
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

        private void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime _from;
                DateTime _to;
                if (cmbYear.Text == "")
                {
                    MessageBox.Show("Select Year !", "Sun Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(ddlWeek.Text))
                {
                    DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), out _from, out _to,BaseCls.GlbUserComCode,"");
                    if (_weekDef == null)
                    {
                        MessageBox.Show("Week Definition not set!", "Sun Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_from != Convert.ToDateTime("31/Dec/9999"))
                    {
                        lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                        lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");

                        //lblFrmdtWkSelect
                        //lblTodtWkSelect
                        lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                        lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");
                    }
                    else
                    {
                        lblFrmdtWk.Text = string.Empty;
                        lblTodtWk.Text = string.Empty;

                        lblFrmdtWk.Text = string.Empty;
                        lblTodtWk.Text = string.Empty;
                    }
                }
                this.Cursor = Cursors.Default;
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

        private void SunUploadElite()
        {
            try
            {
                string p_sun_user_id = string.Empty;
                string p_file_path = string.Empty;
                string p_source_path = string.Empty;
                string p_file_name = string.Empty;

                // Done by Nadeeka 02-July-2013


                this.Cursor = Cursors.WaitCursor;
                //accounting period
                string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");



                {
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

                    //file name
                    //  p_file_name = p_sun_user_id + txtPC.Text + "Elite" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";
                    p_file_name = txtPC.Text + "Elt" + Convert.ToDateTime(txtFrom.Text).Date.ToString("ddMMyy") + ".txt";
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
                    
                    DataTable X  = new DataTable();

                    //updated by akila 2018/02/28
                    if (rbdBlkEliteSale.Checked)
                    {
                        string _errorMsg = string.Empty;
                        X = CHNLSVC.Financial.ProcessSunUploadElite_New(BaseCls.GlbUserID, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text, out _errorMsg);
                        if (!string.IsNullOrEmpty(_errorMsg))
                        {
                            MessageBox.Show(_errorMsg, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                    else if (optElite.Checked)
                    {
                        X = CHNLSVC.Financial.ProcessSunUploadElite(BaseCls.GlbUserID, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text);
                    }
                    
 
                    //DataTable X = CHNLSVC.Financial.ProcessSunUploadElite(BaseCls.GlbUserID, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text);

                    

                    if (X.Rows.Count == 0)
                    {
                        MessageBox.Show("No Records found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "Elite");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


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

        private void SunUploadDutyFree()
        {
            try
            {
                string p_sun_user_id = string.Empty;
                string p_file_path = string.Empty;
                string p_source_path = string.Empty;
                string p_file_name = string.Empty;

                // Done by Nadeeka 02-July-2013


                this.Cursor = Cursors.WaitCursor;
                //accounting period
                string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");



                {
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

                    //file name
                    //  p_file_name = p_sun_user_id + txtPC.Text + "Elite" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";
                    p_file_name = txtPC.Text + "Elt" + Convert.ToDateTime(txtFrom.Text).Date.ToString("ddMMyy") + ".txt";
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

                    DataTable X = CHNLSVC.Financial.ProcessSunUploadDutyFree(BaseCls.GlbUserID, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text);
                    if (X.Rows.Count == 0)
                    {
                        MessageBox.Show("No Records found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
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

                    File_Copy(p_file_name, p_file_path, "DutyFree");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


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

        private void SunUploadLoyalityCard()
        {
            try
            {
                string p_sun_user_id = string.Empty;
                string p_file_path = string.Empty;
                string p_source_path = string.Empty;
                string p_file_name = string.Empty;

                // Done by Nadeeka 02-July-2013


                this.Cursor = Cursors.WaitCursor;
                //accounting period
                string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");



                {
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

                    //file name
                    //  p_file_name = p_sun_user_id + txtPC.Text + "Elite" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";
                    p_file_name = "Loyl" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";
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

                    DataTable X = CHNLSVC.Financial.ProcessSunUploadLoyalty(BaseCls.GlbUserID, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text));
                    if (X.Rows.Count == 0)
                    {
                        MessageBox.Show("No Records found. ", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                    {
                        string _STR = string.Empty;
                        for (int i = 0; i < X.Rows.Count; i++)
                        {
                            _STR = X.Rows[i]["STR_01"].ToString();
                            file.WriteLine(_STR);
                        }

                        file.Close();
                    }

                    File_Copy(p_file_name, p_file_path, "Loyl");
                    txtFile.Text = p_file_name;
                    MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



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

        private void optDInv_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optDInv.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10013))
                    {
                        MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10013)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(2);
                }
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

        private void optDRec_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optDRec.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10014))
                    {
                        MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10014)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(3);
                }
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

        private void optSOS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optSOS.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10012))
                    {
                        MessageBox.Show("Sorry, You have no permission!\n( Advice: Reqired permission code :10012)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(1);
                }
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

        private void optElite_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optElite.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10016))
                    {
                        MessageBox.Show("Sorry, You have no permission to Elite Upload!\n( Advice: Reqired permission code :10016)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(5);
                }
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = true;
            pnlInvalidReceipts.Visible = false;
            if (dgvInvalidReceipts.Rows.Count > 0) { dgvInvalidReceipts.Rows.Clear(); }
        }

        private void opLoyal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opLoyal.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10017))
                    {
                        MessageBox.Show("Sorry, You have no permission to Elite Upload!\n( Advice: Reqired permission code :10017)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(6);
                }
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

        private void rbRtnChq_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRtnChq.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10018))
                {
                    MessageBox.Show("Sorry, You have no permission to return cheque Upload!\n( Advice: Reqired permission code :10018)");

                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    btnCreate.Enabled = false;
                    return;
                }
                setFormControls(7);
            }

        }

        private void rbRtnChqSet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRtnChqSet.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10019))
                {
                    MessageBox.Show("Sorry, You have no permission to return cheque settlement Upload!\n( Advice: Reqired permission code :10019)");

                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    btnCreate.Enabled = false;
                    return;
                }
                setFormControls(8);
            }


        }

        private void optVouEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (optVouEntry.Checked == true)
            {

                setFormControls(9);
            }


        }


        private void optVouClaim_CheckedChanged(object sender, EventArgs e)
        {

            setFormControls(10);
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
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
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
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

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            btnHierachySearch_Click(null, null);
        }

        private void optPB_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(12);
        }

        private void optDutyFree_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optDutyFree.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10016))
                    {
                        MessageBox.Show("Sorry, You have no permission to Elite Upload!\n( Advice: Reqired permission code :10016)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(13);
                }
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

        private void optAOA_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(14);
        }

        private void rdoCheqPrinting_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCheqPrinting.Checked == true)
            {

                setFormControls(15);
            }
        }

        private void txtAccNo_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccNo;
            _CommonSearch.ShowDialog();
            txtAccNo.Select();
        }

        private void btnAccNoSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccNo;
            _CommonSearch.ShowDialog();
            txtAccNo.Select();
        }

        private void txtAccNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccNo.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, txtAccNo.Text);
                if (_result == null || _result.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a correct account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccNo.Clear();
                }
                else
                {
                    cmbMonth.Focus();
                   
                }
            }
        }

        private void txtAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAccNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                cmbMonth.Focus();
            }
        }

        private void rdoFund_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFund.Checked == true)
            {

                setFormControls(16);
            }
        }

        private void rdoLoyalty_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLoyalty.Checked == true)
            {

                setFormControls(17);
            }
        }

        private void optBnkChrg_CheckedChanged(object sender, EventArgs e)
        {
            if (optBnkChrg.Checked == true)
            {

                setFormControls(18);
            }
        }

        private void optService_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(19);
        }

        private void optServRec_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(20);
        }

        private void optJad_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(21);
        }

        private void btnClosePnlInvalidReceipt_Click(object sender, EventArgs e)
        {
            pnlInvalidReceipts.Visible = false;
            if (dgvInvalidReceipts.Rows.Count > 0) { dgvInvalidReceipts.DataSource = null; }
            btnCreate.Enabled = true;
        }


        private void BindInvalidReceipts(DataTable _invalidReceipts)
        {
            try
            {
                if (_invalidReceipts.Rows.Count > 0)
                {
                    BindingSource _bind = new BindingSource();
                    _bind.DataSource = _invalidReceipts;
                    dgvInvalidReceipts.DataSource = _bind;
                }
                else { MessageBox.Show("Couldn't upload receipt details", "Sun Upload- Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while upload service reseipt details!" + Environment.NewLine + ex.Message, "Sun Upload- Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkIScancle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIScancle.Checked == true)
            {
                WithCancleentry = true;
            }
            else
            { WithCancleentry = false; }
        }

        private void update_PC_List()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                List<string> tmpList = new List<string>();
                tmpList = Item.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                string pc = null;
                string com = txtComp.Text;
                if ((tmpList != null) && (tmpList.Count > 0))
                {
                    pc = tmpList[0];
                    //com = com;
                }

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

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        private void optwrrcredt_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Added by Chamal - 12-Jan-2018 :: 10162 - Permission - Warranty Credit Note SUN Upload
                if (optwrrcredt.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10162))
                    {
                        MessageBox.Show("Sorry, You have no permission to Warranty Credit Note Upload!\n( Advice: Reqired permission code :10162)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        btnCreate.Enabled = false;
                        return;
                    }
                    setFormControls(22);
                }
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

        private void opt_FA_CheckedChanged(object sender, EventArgs e)
        {
            doc_type = "SUN_FIXED_ASSET";
        }

        private void rbdBlkEliteSale_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(5);
        }

        private void optBnkRecom_CheckedChanged(object sender, EventArgs e)
        {
            if (optBnkRecom.Checked == true)
            {
                setFormControls(23);
            }
        }
     

    }
}
