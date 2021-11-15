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
using FF.WindowsERPClient.General;
using FF.WindowsERPClient.Reports.Finance;
using System.IO;

namespace FF.WindowsERPClient.General
{
    public partial class Document_Check_List : Base
    {
        private static int _recordCNT = 0;
        bool _isDecimalAllow = false;
        private Boolean _isSR = false;
        private Boolean _isRT = false;
        private Boolean _isSE = false;

        private System.Windows.Forms.TextBox[] txtDesc;
        private System.Windows.Forms.TextBox[] txtSR;
        private System.Windows.Forms.TextBox[] txtRT;
        private System.Windows.Forms.TextBox[] txtSE;
        private System.Windows.Forms.TextBox[] txtLine;

        private void RemoveControls()
        {
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                pnlDesc.Controls.Remove(txtDesc[i]);
                pnlSE.Controls.Remove(txtSE[i]);
                pnlSR.Controls.Remove(txtSR[i]);
                pnlRT.Controls.Remove(txtRT[i]);
                pnlLine.Controls.Remove(txtLine[i]);

            }
            //if (_recordCNT > 0)
            //{
            //    for (int i = 1; i < _recordCNT + 2; i++)
            //    {
            //        pnlSE.Controls.Remove(txtSE[i]);
            //        pnlSR.Controls.Remove(txtSR[i]);
            //        pnlRT.Controls.Remove(txtRT[i]);
            //    }
            //}
        }

        private void LoadLine(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtLine", _num);
            int n = 1;
            pnlLine.Height = 25;
            while (n < _num + 1)
            {
                pnlExecutive.Height = pnlExecutive.Height + 31;
                txtLine[n].Tag = n;
                txtLine[n].Width = 50;
                txtLine[n].Height = 21;
                txtLine[n].BackColor = System.Drawing.SystemColors.Info;
                txtLine[n].Left = xPos;
                txtLine[n].Top = yPos;
                yPos = yPos + txtLine[n].Height + 4;
                pnlLine.Controls.Add(txtLine[n]);
                pnlLine.Height = pnlLine.Height + 25;
                n++;
            }
        }

        private void LoadRef(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtDesc", _num);
            int n = 1;
            pnlDesc.Height = 25;
            while (n < _num + 1)
            {
                txtDesc[n].Tag = n;
                txtDesc[n].Width = 560;
                txtDesc[n].Height = 21;
                txtDesc[n].BackColor = System.Drawing.SystemColors.Info;
                txtDesc[n].Enabled = false;
                txtDesc[n].Left = xPos;
                txtDesc[n].Top = yPos;
                yPos = yPos + txtDesc[n].Height + 4;
                pnlDesc.Controls.Add(txtDesc[n]);
                pnlDesc.Height = pnlDesc.Height + 25;
                n++;
            }
        }

        private void LoadSE(int _num, Boolean _confirm)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtSE", _num);
            int n = 1;
            pnlSE.Height = 25;
            while (n < _num + 1)
            {
                txtSE[n].Tag = n;
                txtSE[n].Width = 78;
                txtSE[n].Height = 21;
                txtSE[n].BackColor = System.Drawing.SystemColors.Info;
                //   txtSE[n].Text = "0";
                if (_isSE == true)
                {
                    if (_confirm == true)
                        txtSE[n].Enabled = false;
                    else
                        txtSE[n].Enabled = true;
                }
                else
                    txtSE[n].Enabled = false;

                txtSE[n].Left = xPos;
                txtSE[n].Top = yPos;
                yPos = yPos + txtSE[n].Height + 4;
                pnlSE.Controls.Add(txtSE[n]);
                pnlSE.Height = pnlSE.Height + 25;
                txtSE[n].Leave += new EventHandler(txtSE_Leave);
                n++;
            }
        }

        protected void txtSR_Leave(object sender, EventArgs e)
        {
            try
            {
                foreach (TextBox tx in txtSR)
                {
                    if (tx != null)
                    {
                        if (tx.Text.Trim() != "")
                        {
                            try
                            {
                                if (Convert.ToInt32(tx.Text) < 0)
                                {
                                    MessageBox.Show("Invalid amount", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tx.Text = "0";
                                    tx.Focus();
                                    return;
                                }
                                if (!IsNumeric(tx.Text))
                                {
                                    MessageBox.Show("Invalid amount", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tx.Text = "0";
                                    tx.Focus();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid amount!");
                                tx.Text = "0";
                            }

                        }
                    }


                }

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

        protected void txtRT_Leave(object sender, EventArgs e)
        {
            try
            {
                foreach (TextBox tx in txtRT)
                {
                    if (tx != null)
                    {
                        if (tx.Text.Trim() != "")
                        {
                            try
                            {
                                if (Convert.ToInt32(tx.Text) < 0)
                                {
                                    MessageBox.Show("Invalid amount", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tx.Text = "0";
                                    tx.Focus();
                                    return;
                                }
                                if (!IsNumeric(tx.Text))
                                {
                                    MessageBox.Show("Invalid amount", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tx.Text = "0";
                                    tx.Focus();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid amount!");
                                tx.Text = "0";
                            }

                        }
                    }


                }

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

        protected void txtSE_Leave(object sender, EventArgs e)
        {
            try
            {
                foreach (TextBox tx in txtSE)
                {
                    if (tx != null)
                    {
                        if (tx.Text.Trim() != "")
                        {
                            try
                            {
                                if (Convert.ToInt32(tx.Text) < 0)
                                {
                                    MessageBox.Show("Invalid amount", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tx.Text = "0";
                                    tx.Focus();
                                    return;
                                }
                                if (!IsNumeric(tx.Text))
                                {
                                    MessageBox.Show("Invalid amount", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tx.Text = "0";
                                    tx.Focus();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid amount!");
                                tx.Text = "0";
                            }

                        }
                    }


                }

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
        private void LoadSR(int _num, Boolean _confirm)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtSR", _num);
            int n = 1;
            pnlSR.Height = 25;
            while (n < _num + 1)
            {
                txtSR[n].Tag = n;
                txtSR[n].Width = 78;
                txtSR[n].Height = 21;
                txtSR[n].BackColor = System.Drawing.SystemColors.Info;
                //   txtSR[n].Text = "0";
                if (_isSR == true)
                {
                    if (_confirm == true)
                        txtSR[n].Enabled = false;
                    else
                        txtSR[n].Enabled = true;
                }
                else
                    txtSR[n].Enabled = false;

                txtSR[n].Left = xPos;
                txtSR[n].Top = yPos;
                yPos = yPos + txtSR[n].Height + 4;
                pnlSR.Controls.Add(txtSR[n]);
                pnlSR.Height = pnlSR.Height + 25;
                //txtSR[n].KeyPress += new System.EventHandler(ClickTextBox);
                txtSR[n].Leave += new EventHandler(txtSR_Leave);
                n++;
            }
        }



        private void LoadRT(int _num, Boolean _confirm)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtRT", _num);
            int n = 1;
            pnlRT.Height = 25;
            while (n < _num + 1)
            {
                txtRT[n].Tag = n;
                txtRT[n].Width = 78;
                txtRT[n].Height = 21;
                txtRT[n].BackColor = System.Drawing.SystemColors.Info;
                //  txtRT[n].Text = "0";
                if (_isRT == true)
                {
                    if (_confirm == true)
                        txtRT[n].Enabled = false;
                    else
                        txtRT[n].Enabled = true;
                }
                else
                    txtRT[n].Enabled = false;

                txtRT[n].Left = xPos;
                txtRT[n].Top = yPos;
                yPos = yPos + txtRT[n].Height + 4;
                pnlRT.Controls.Add(txtRT[n]);
                pnlRT.Height = pnlRT.Height + 25;
                txtRT[n].Leave += new EventHandler(txtRT_Leave);
                n++;
            }
        }

        private void AddControls(string anyControl, int cNumber)
        {
            switch (anyControl)
            {
                case "txtLine":
                    {
                        txtLine = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtLine[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtDesc":
                    {
                        txtDesc = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtDesc[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtSR":
                    {
                        txtSR = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSR[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRT":
                    {
                        txtRT = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRT[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtSE":
                    {
                        txtSE = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSE[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
            }
        }


        private void btnProcess_Click(object sender, EventArgs e)
        {
            Boolean _SR = true;
            Boolean _RT = true;
            Boolean _SE = true;

            if (string.IsNullOrEmpty(lblFrmdtWk.Text))
            {
                MessageBox.Show("Select Week", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtPC.Text) || string.IsNullOrEmpty(cmbMonth.Text) || string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Cannot Process. Missing Parameters.", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SR");
            if (_SR == true)
            {
                MessageBox.Show("Cannot Process. Already confirmed.", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Do you want to Delete Previously Processed Data ?", "Check List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            DateTime DTmonth = Convert.ToDateTime(lblMonth.Text).Date;
            Int32 mon = DTmonth.Month;
            Int32 yr = DTmonth.Year;
            Int32 Week = Convert.ToInt32(ddlWeek.SelectedIndex + 1);

            Int32 del_eff = CHNLSVC.General.Delete_Doc_Check_List(BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), DTmonth, Week);
            Int32 eff = CHNLSVC.General.Save_Doc_Check_List(BaseCls.GlbUserID, DateTime.Now.Date, BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), DTmonth.Month, DTmonth.Year, Week, DTmonth);

            DataTable dt = CHNLSVC.General.Get_Doc_Check_List(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1));

            RemoveControls();
            pnlExecutive.Height = 31;
            pnlExecutive.Top = 51;
            _recordCNT = dt.Rows.Count;

            if (_recordCNT > 0)
            {
                LoadLine(_recordCNT);
                LoadRef(_recordCNT);
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10072))
                {
                    _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SR");
                    if (_SR == true)
                    {
                        btnConfirm.Enabled = false;
                        btnPrint.Enabled = true;
                    }
                }
                LoadSR(_recordCNT, _SR);

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10073))
                {
                    _RT = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "RT");
                    if (_RT == true)
                    {
                        btnConfirm.Enabled = false;
                        btnPrint.Enabled = true;
                    }
                }
                LoadRT(_recordCNT, _RT);

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10074))
                {
                    _SE = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SE");
                    if (_SE == true)
                    {
                        btnConfirm.Enabled = false;
                        btnPrint.Enabled = true;
                    }
                }
                LoadSE(_recordCNT, _SE);
            }

            for (int i = 0; i < _recordCNT; i++)
            {

                txtDesc[i + 1].Text = (dt.Rows[i]["DCL_DESC"]).ToString();
                txtSR[i + 1].Text = (dt.Rows[i]["DCL_SR"]).ToString();
                txtRT[i + 1].Text = (dt.Rows[i]["DCL_RT"]).ToString();
                txtSE[i + 1].Text = (dt.Rows[i]["DCL_SE"]).ToString();
                txtLine[i + 1].Text = (dt.Rows[i]["DCL_LINE"]).ToString();
                //_totSE = _totSE + Convert.ToInt32(txtSE[i + 1].Text);
                //_totSR = _totSR + Convert.ToInt32(txtSR[i + 1].Text);
                //_totRT = _totRT + Convert.ToInt32(txtRT[i + 1].Text);

            }
            //txtSE[_recordCNT+1].Text = _totSE.ToString();
            //txtSR[_recordCNT + 1].Text = _totSR.ToString();
            //txtRT[_recordCNT + 1].Text = _totRT.ToString();

            vScrollBar1.Maximum = pnlExecutive.Height - 6 * _recordCNT;

            this.Cursor = Cursors.Default;
            if (eff > 0)
            {
                btnPrint.Enabled = false;
                MessageBox.Show("Successfully Processed.", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not Processed!", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BackDatePermission()
        {
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtRemindDate, lblBackDateInfor, string.Empty);
        }

        public Document_Check_List()
        {
            InitializeComponent();
            InitializeValuesNDefaultValueSet();
        }


        private void InitializeValuesNDefaultValueSet()
        {
            _recordCNT = 0;

            txtPC.Text = BaseCls.GlbUserDefProf;
            txtUser.Text = BaseCls.GlbUserID;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");
            cmbMonth.SelectedIndex = -1;

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

            if (BaseCls.GlbUserDeptID == "SR")
            {
                btnProcess.Enabled = true;
                pnlSREnd.Enabled = true;
                pnlHOEnd.Visible = false;
            }
            else
            {
                pnlSREnd.Enabled = false;
                pnlHOEnd.Visible = true;
            }

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10072))
                _isSR = true;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10073))
                _isRT = true;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10074))
                _isSE = true;
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
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ImgBtnPC_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.txtSearchbyword.Text = txtPC.Text;
            _CommonSearch.ShowDialog();
            txtPC.Focus();

            cmbYear.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
            btnClear_Click(null, null);

        }

        private void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime _from;
            DateTime _to;

            if (string.IsNullOrEmpty(cmbMonth.Text) || string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select Year/Month", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(ddlWeek.Text))
            {
                DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), out _from, out _to, BaseCls.GlbUserComCode, "");
                if (_from != Convert.ToDateTime("31/Dec/9999"))
                {
                    lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                    lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblFrmdtWk.Text = string.Empty;
                    lblTodtWk.Text = string.Empty;
                }
            }

            this.Cursor = Cursors.Default;

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Int32 _totSE = 0;
            Int32 _totSR = 0;
            Int32 _totRT = 0;

            Boolean _SR = false;
            Boolean _RT = false;
            Boolean _SE = false;

            if (string.IsNullOrEmpty(lblFrmdtWk.Text))
            {
                MessageBox.Show("Select week", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(ddlWeek.Text) || string.IsNullOrEmpty(cmbMonth.Text) || string.IsNullOrEmpty(cmbYear.Text))
            {
                return;
            }
            else
            {
                dt = CHNLSVC.General.Get_Doc_Check_List(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1));

                RemoveControls();
                pnlExecutive.Height = 31;
                pnlExecutive.Top = 51;
                _recordCNT = dt.Rows.Count;

                if (_recordCNT > 0)
                {
                    LoadLine(_recordCNT);
                    LoadRef(_recordCNT);

                    //LoadSR(_recordCNT, false);
                    //LoadRT(_recordCNT, false);
                    //LoadSE(_recordCNT, false);

                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10072))
                    {
                        btnProcess.Enabled = true;
                        _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SR");
                        if (_SR == true)
                        {
                            btnConfirm.Enabled = false;
                            btnPrint.Enabled = true;

                        }
                        pnlSE.Visible = false;

                    }
                    LoadSR(_recordCNT, _SR);
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10073))
                    {
                        btnProcess.Enabled = false;
                        _RT = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "RT");
                        if (_RT == true)
                        {
                            btnConfirm.Enabled = false;
                            btnPrint.Enabled = true;

                        }
                        else
                        {
                            _RT = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SR");
                            if (_RT == true)
                            {
                                //btnConfirm.Enabled = true;
                                btnPrint.Enabled = false;
                                _RT = false;

                            }
                        }
                        pnlSE.Visible = true;

                    }
                    LoadRT(_recordCNT, _RT);
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10074))
                    {
                        btnProcess.Enabled = false;
                        _SE = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SE");
                        if (_SE == true)
                        {
                            btnConfirm.Enabled = false;
                            btnPrint.Enabled = true;

                        }
                        else
                        {
                            _SE = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "RT");
                            if (_SE == true)
                            {
                                //btnConfirm.Enabled = true;
                                btnPrint.Enabled = false;
                                _SE = false;

                            }
                        }
                        pnlSE.Visible = true;

                    }
                    LoadSE(_recordCNT, _SE);
                }

                for (int i = 0; i < _recordCNT; i++)
                {

                    txtDesc[i + 1].Text = (dt.Rows[i]["DCL_DESC"]).ToString();
                    txtSR[i + 1].Text = (dt.Rows[i]["DCL_SR"]).ToString();
                    txtRT[i + 1].Text = (dt.Rows[i]["DCL_RT"]).ToString();
                    txtSE[i + 1].Text = (dt.Rows[i]["DCL_SE"]).ToString();
                    txtLine[i + 1].Text = (dt.Rows[i]["DCL_LINE"]).ToString();
                    //_totSE = _totSE + Convert.ToInt32(txtSE[i + 1].Text);
                    //_totSR = _totSR + Convert.ToInt32(txtSR[i + 1].Text);
                    //_totRT = _totRT + Convert.ToInt32(txtRT[i + 1].Text);

                }
                vScrollBar1.Maximum = pnlExecutive.Height - 6 * _recordCNT;
                //txtSE[_recordCNT + 1].Text = (_totSE).ToString();
                //txtSR[_recordCNT + 1].Text = (_totSR).ToString();
                //txtRT[_recordCNT + 1].Text = (_totRT).ToString();
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select Year", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            int month = cmbMonth.SelectedIndex + 1;
            if (month != 0)
            {
                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
                lblMonth.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            }

            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.SelectedIndex = -1;
            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Boolean _SR = false;
            Boolean _isSE = false;
            Boolean _isDiff = false;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10072))
            {
                if (Convert.ToDateTime(dtSR.Value.Date) > DateTime.Now.Date)
                {
                    MessageBox.Show("Invalid POD Date !", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SE");
                if (_SR == true)
                {
                    MessageBox.Show("Already confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "RT");
                if (_SR == true)
                {
                    MessageBox.Show("Already confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10073))
                {
                    if (Convert.ToDateTime(dtHO.Value.Date) > DateTime.Now.Date)
                    {
                        MessageBox.Show("Invalid HO Date !", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SE");
                    if (_SR == true)
                    {
                        MessageBox.Show("Already confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10074))
                    {
                        if (Convert.ToDateTime(dtHO.Value.Date) > DateTime.Now.Date)
                        {
                            MessageBox.Show("Invalid HO Date !", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        _isSE = true;
                        _SR = CHNLSVC.General.IsConfirmCheckList(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), "SE");
                        if (_SR == true)
                        {
                            MessageBox.Show("Already confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Access Denied", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

            if (MessageBox.Show("Are you sure ?", "Check List", MessageBoxButtons.YesNo) == DialogResult.No) return;

            List<DocCheckList> UpdateDocList = new List<DocCheckList>();

            for (int i = 1; i < _recordCNT + 1; i++)
            {
                DocCheckList docChlLst = new DocCheckList();
                docChlLst.Dcl_com = BaseCls.GlbUserComCode;
                docChlLst.Dcl_line = Convert.ToInt32(txtLine[i].Text);
                docChlLst.Dcl_month = Convert.ToDateTime(lblMonth.Text);
                docChlLst.Dcl_pc = txtPC.Text;
                docChlLst.Dcl_rt = Convert.ToInt32(txtRT[i].Text);
                docChlLst.Dcl_se = Convert.ToInt32(txtSE[i].Text);
                docChlLst.Dcl_sr = Convert.ToInt32(txtSR[i].Text);
                docChlLst.Dcl_week = Convert.ToInt32(ddlWeek.SelectedIndex + 1);
                docChlLst.Dcl_cre_by = BaseCls.GlbUserID;
                docChlLst.Dcl_cre_dt = DateTime.Now.Date;
                docChlLst.Dcl_desc = txtDesc[i].Text;
                if (_isSE == true)
                {
                    if (Convert.ToInt32(txtRT[i].Text) != Convert.ToInt32(txtSE[i].Text) && Convert.ToInt32(txtRT[i].Text) > 0)
                    {
                        _isDiff = true;
                    }
                }


                UpdateDocList.Add(docChlLst);
            }

            if (_isSE == true && _isDiff == true)
            {
                if (MessageBox.Show("There are some mismatches. Are you sure ?", "Check List", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            Int32 eff = 0;
            eff = CHNLSVC.General.Update_Doc_Check_List(UpdateDocList);

            if (eff > 0)
            {
                btnPrint.Enabled = false;
                btnConfirm.Enabled = true;
                MessageBox.Show("Successfully Updated..", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not Updated!", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pnlExecutive.Top = -vScrollBar1.Value + 50;
            //if (e.OldValue != e.NewValue)
            //{
            //    if (e.OldValue - e.NewValue < 0)
            //    {
            //        pnlExecutive.Top = pnlExecutive.Top - 50;
            //    }
            //    else
            //    {
            //        pnlExecutive.Top = pnlExecutive.Top + 50;
            //    }
            //}
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                BaseCls.GlbReportComp = _masterComp.Mc_desc;
                BaseCls.GlbReportCompAddr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }

            BaseCls.GlbReportName = "Doc_Check_List.rpt";
            BaseCls.GlbReportProfit = txtPC.Text;
            BaseCls.GlbReportFromDate = Convert.ToDateTime(lblFrmdtWk.Text).Date;
            BaseCls.GlbReportToDate = Convert.ToDateTime(lblTodtWk.Text).Date;
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(lblMonth.Text).Date;
            BaseCls.GlbReportWeek = Convert.ToInt32(ddlWeek.SelectedIndex + 1);
            ReportViewerFinance _view = new ReportViewerFinance();
            _view.GlbReportName = "Doc_Check_List.rpt";
            _view.Show();
            _view = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RemoveControls();
            pnlExecutive.Height = 31;
            pnlExecutive.Top = 51;
            _recordCNT = 0;
            cmbMonth.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
            ddlWeek.SelectedIndex = -1;
            dtSR.Enabled = false;
            chkSR.Checked = false;
            txtPOD.Text = "";
            dtHO.Enabled = false;
            chkHO.Checked = false;
            txtUser.Text = "";

        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnPC_Click(null, null);
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ImgBtnPC_Click(null, null);
        }

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10074)) //SE
            {
                DataTable dt = CHNLSVC.General.Get_Doc_Check_List(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1));
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["DCL_STUS_RT"]) == 0)
                    {
                        MessageBox.Show("Receiving table not finalized !", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBox.Show("Are you sure ?", "Check List", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    Int32 X = CHNLSVC.General.Confirm_Check_List(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), 0, 0, 1, DateTime.Now.Date, "", BaseCls.GlbUserID, Convert.ToDateTime(dtHO.Value.Date));
                    btnView_Click(null, null);
                    MessageBox.Show("Successfully Confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Not processed data found !", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10073))  //RT
                {
                    DataTable dt = CHNLSVC.General.Get_Doc_Check_List(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1));
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["DCL_STUS_SR"]) == 0)
                        {
                            MessageBox.Show("Showroom not finalized !", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (MessageBox.Show("Are you sure ?", "Check List", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        Int32 X = CHNLSVC.General.Confirm_Check_List(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), 0, 1, 0, DateTime.Now.Date, "", BaseCls.GlbUserID, Convert.ToDateTime(dtHO.Value.Date));
                        btnView_Click(null, null);
                        MessageBox.Show("Successfully Confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10072))  //SR
                    {
                        if (chkSR.Checked == false)
                        {
                            MessageBox.Show("Please select the handed over date to courier", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtPOD.Text))
                            {
                                MessageBox.Show("Enter the POD Number", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        if (MessageBox.Show("Are you sure ?", "Check List", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        Int32 X = CHNLSVC.General.Confirm_Check_List(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), 1, 0, 0, Convert.ToDateTime(dtSR.Value.Date), txtPOD.Text, BaseCls.GlbUserID, Convert.ToDateTime(dtHO.Value.Date));
                        btnView_Click(null, null);
                        MessageBox.Show("Successfully Confirmed", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Access Denied. Permission code 10072", "Check List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }



        }

        private void chkSR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSR.Checked == true)
            {
                dtSR.Enabled = true;
                txtPOD.Enabled = true;
            }
            else
            {
                txtPOD.Text = "";
                dtSR.Enabled = false;
                txtPOD.Enabled = false;
            }
        }

        private void chkHO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHO.Checked == true)
                dtHO.Enabled = true;
            else
                dtHO.Enabled = false;

        }
    }
}
