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
using System.IO;

namespace FF.WindowsERPClient.General
{

    public partial class Credit_Sale_Docs : Base
    {
        Int32 Seq_No;
        string RecieptNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox textBox = new TextBox();

        }

        private static int _recordCNT = 0;
        private static int _recordFound = 0;

        private System.Windows.Forms.TextBox[] txtLine;
        private System.Windows.Forms.TextBox[] txtDesc;

        private System.Windows.Forms.CheckBox[] chkSend;
        private System.Windows.Forms.DateTimePicker[] dtSDate;
        private System.Windows.Forms.TextBox[] txtSRem;

        private System.Windows.Forms.CheckBox[] chkRec;
        private System.Windows.Forms.DateTimePicker[] dtRDate;
        private System.Windows.Forms.TextBox[] txtRRem;

        private System.Windows.Forms.CheckBox[] chkRet;
        private System.Windows.Forms.DateTimePicker[] dtRetDate;
        private System.Windows.Forms.TextBox[] txtRetRem;

        private System.Windows.Forms.CheckBox[] chkRerec;
        private System.Windows.Forms.DateTimePicker[] dtRerec;

        Dictionary<string, Decimal> BindSumList = new Dictionary<string, Decimal>();

        public void ClickTextBox(Object sender, System.EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Edit me, then Click Label which has same number.");
        }

        private void LoadReRec(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkRerec", _num);
            int n = 1;
            pnlrerec.Height = 25;
            while (n < _num + 1)
            {
                chkRerec[n].Tag = n;
                chkRerec[n].Width = 15;
                chkRerec[n].Height = 14;
                chkRerec[n].Left = xPos;
                chkRerec[n].Top = yPos;
                yPos = yPos + chkRerec[n].Height + 11;
                pnlrerec.Controls.Add(chkRerec[n]);
                pnlrerec.Height = pnlrerec.Height + 25;
                chkRerec[n].Enabled = false;
                n++;
            }
        }

        private void LoadReRecDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtRerec", _num);
            int n = 1;
            pnlrerecdate.Height = 25;
            while (n < _num + 1)
            {
                dtRerec[n].Tag = n;
                dtRerec[n].Width = 104;
                dtRerec[n].Height = 21;
                dtRerec[n].Left = xPos;
                dtRerec[n].Top = yPos;
                yPos = yPos + dtSDate[n].Height + 3;
                dtRerec[n].Format = DateTimePickerFormat.Custom;
                dtRerec[n].CustomFormat = "dd/MMM/yyyy";
                pnlrerecdate.Controls.Add(dtRerec[n]);
                pnlrerecdate.Height = pnlrerecdate.Height + 25;
                dtRerec[n].Enabled = false;
                //pnlRDate.Visible = false;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }


        private void LoadRet(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkRet", _num);
            int n = 1;
            pnlRet.Height = 25;
            while (n < _num + 1)
            {
                chkRet[n].Tag = n;
                chkRet[n].Width = 15;
                chkRet[n].Height = 14;
                chkRet[n].Left = xPos;
                chkRet[n].Top = yPos;
                yPos = yPos + chkRet[n].Height + 11;
                pnlRet.Controls.Add(chkRet[n]);
                pnlRet.Height = pnlRet.Height + 25;
                chkRet[n].Enabled = false;
                n++;
            }
        }

        private void LoadRetDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtRetDate", _num);
            int n = 1;
            pnlRetDate.Height = 25;
            while (n < _num + 1)
            {
                dtRetDate[n].Tag = n;
                dtRetDate[n].Width = 104;
                dtRetDate[n].Height = 21;
                dtRetDate[n].Left = xPos;
                dtRetDate[n].Top = yPos;
                yPos = yPos + dtSDate[n].Height + 3;
                dtRetDate[n].Format = DateTimePickerFormat.Custom;
                dtRetDate[n].CustomFormat = "dd/MMM/yyyy";
                pnlRetDate.Controls.Add(dtRetDate[n]);
                pnlRetDate.Height = pnlRetDate.Height + 25;
                dtRetDate[n].Enabled = false;
                // dtRetDate[n].Visible = false;
                //pnlRDate.Visible = false;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadRetRem(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtRetRem", _num);
            int n = 1;
            pnlRetRem.Height = 25;
            while (n < _num + 1)
            {
                txtRetRem[n].Tag = n;
                txtRetRem[n].Width = 109;
                txtRetRem[n].Height = 21;
                txtRetRem[n].BackColor = System.Drawing.SystemColors.Info;
                txtRetRem[n].Left = xPos;
                txtRetRem[n].Top = yPos;
                yPos = yPos + txtRetRem[n].Height + 4;
                pnlRetRem.Controls.Add(txtRetRem[n]);
                pnlRetRem.Height = pnlRetRem.Height + 25;
                txtRetRem[n].MaxLength = 200;
                txtRetRem[n].Enabled = false;
                n++;
            }
        }

        public Credit_Sale_Docs()
        {
            InitializeComponent();
            _recordCNT = 0;
            _recordFound = 0;
        }

        private void RemoveControls()
        {
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                pnlLine.Controls.Remove(txtLine[i]);
                pnlDesc.Controls.Remove(txtDesc[i]);

                pnlReceive.Controls.Remove(chkRec[i]);
                pnlRDate.Controls.Remove(dtRDate[i]);
                pnlRecRem.Controls.Remove(txtRRem[i]);

                pnlSend.Controls.Remove(chkSend[i]);
                pnlSDate.Controls.Remove(dtSDate[i]);
                pnlSendRem.Controls.Remove(txtSRem[i]);

                pnlRet.Controls.Remove(chkRet[i]);
                pnlRetDate.Controls.Remove(dtRetDate[i]);
                pnlRetRem.Controls.Remove(txtRetRem[i]);

                pnlrerec.Controls.Remove(chkRerec[i]);
                pnlrerecdate.Controls.Remove(dtRerec[i]);


            }
            _recordCNT = 0;
            _recordFound = 0;
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

        private void LoadDesc(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtDesc", _num);
            int n = 1;
            pnlDesc.Height = 25;
            while (n < _num + 1)
            {
                txtDesc[n].Tag = n;
                txtDesc[n].Width = 110;
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

        private void LoadSDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtSDate", _num);
            int n = 1;
            pnlSDate.Height = 25;

            if (dtpDate.Enabled == true)
            {
                pnlSDate.Enabled = true;
            }
            else
            {
                pnlSDate.Enabled = false;
            }
            while (n < _num + 1)
            {
                dtSDate[n].Tag = n;
                dtSDate[n].Width = 104;
                dtSDate[n].Height = 21;
                dtSDate[n].Left = xPos;
                dtSDate[n].Top = yPos;
                yPos = yPos + dtSDate[n].Height + 4;
                dtSDate[n].Format = DateTimePickerFormat.Custom;
                dtSDate[n].CustomFormat = "dd/MMM/yyyy";
                // dtSDate[n].Enabled = false;
                pnlSDate.Controls.Add(dtSDate[n]);
                pnlSDate.Height = pnlSDate.Height + 25;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadSend(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkSend", _num);
            int n = 1;
            pnlSend.Height = 25;
            while (n < _num + 1)
            {
                chkSend[n].Tag = n;
                chkSend[n].Width = 15;
                chkSend[n].Height = 14;
                chkSend[n].Left = xPos;
                chkSend[n].Top = yPos;
                yPos = yPos + chkSend[n].Height + 11;
                pnlSend.Controls.Add(chkSend[n]);
                pnlSend.Height = pnlSend.Height + 25;
                n++;
            }
        }


        private void LoadSRem(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtSRem", _num);
            int n = 1;
            pnlSendRem.Height = 25;
            while (n < _num + 1)
            {
                txtSRem[n].Tag = n;
                txtSRem[n].Width = 109;
                txtSRem[n].Height = 21;
                txtSRem[n].BackColor = System.Drawing.SystemColors.Info;
                txtSRem[n].Left = xPos;
                txtSRem[n].Top = yPos;
                yPos = yPos + txtSRem[n].Height + 4;
                pnlSendRem.Controls.Add(txtSRem[n]);
                pnlSendRem.Height = pnlSendRem.Height + 25;
                txtSRem[n].MaxLength = 200;
                n++;
            }
        }

        private void LoadReceive(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkRec", _num);
            int n = 1;
            pnlReceive.Height = 25;
            while (n < _num + 1)
            {
                chkRec[n].Tag = n;
                chkRec[n].Width = 15;
                chkRec[n].Height = 14;
                chkRec[n].Left = xPos;
                chkRec[n].Top = yPos;
                yPos = yPos + chkRec[n].Height + 11;
                pnlReceive.Controls.Add(chkRec[n]);
                pnlReceive.Height = pnlReceive.Height + 25;
                chkRec[n].Enabled = false;
                // pnlReceive.Visible = false;
                n++;
            }
        }

        private void LoadRDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtRDate", _num);
            int n = 1;
            pnlRDate.Height = 25;
            while (n < _num + 1)
            {
                dtRDate[n].Tag = n;
                dtRDate[n].Width = 104;
                dtRDate[n].Height = 21;
                dtRDate[n].Left = xPos;
                dtRDate[n].Top = yPos;
                yPos = yPos + dtSDate[n].Height + 3;
                dtRDate[n].Format = DateTimePickerFormat.Custom;
                dtRDate[n].CustomFormat = "dd/MMM/yyyy";
                pnlRDate.Controls.Add(dtRDate[n]);
                pnlRDate.Height = pnlRDate.Height + 25;
                dtRDate[n].Enabled = false;
                //pnlRDate.Visible = false;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadRRem(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtRRem", _num);
            int n = 1;
            pnlRecRem.Height = 25;
            while (n < _num + 1)
            {
                txtRRem[n].Tag = n;
                txtRRem[n].Width = 109;
                txtRRem[n].Height = 21;
                txtRRem[n].BackColor = System.Drawing.SystemColors.Info;
                txtRRem[n].Enabled = false;
                txtRRem[n].Left = xPos;
                txtRRem[n].Top = yPos;
                yPos = yPos + txtRRem[n].Height + 4;
                pnlRecRem.Controls.Add(txtRRem[n]);
                pnlRecRem.Height = pnlRecRem.Height + 25;
                txtRRem[n].MaxLength = 200;
                txtRRem[n].Enabled = false;
                //pnlRecRem.Visible = false;
                n++;
            }
        }

        private void Load_Saved_Docs()
        {
            DataTable dt = CHNLSVC.General.GetCrdSaleDocsSavedData(Seq_No);

            RemoveControls();
            pnlExecutive.Height = 31;
            pnlExecutive.Top = 3;
            _recordCNT = dt.Rows.Count;

            if (_recordCNT > 0)
            {
                LoadLine(_recordCNT);
                LoadDesc(_recordCNT);

                LoadSend(_recordCNT);
                LoadSDate(_recordCNT);
                LoadSRem(_recordCNT);

                LoadReceive(_recordCNT);
                LoadRDate(_recordCNT);
                LoadRRem(_recordCNT);

                LoadRet(_recordCNT);
                LoadRetDate(_recordCNT);
                LoadRetRem(_recordCNT);

                LoadReRec(_recordCNT);
                LoadReRecDate(_recordCNT);

                chkSendReg.Checked = Convert.ToBoolean(dt.Rows[1]["gdh_send_reg"]);
                chkCRRec.Checked = Convert.ToBoolean(dt.Rows[1]["gdh_cr_rec"]);
                chkSendPay.Checked = Convert.ToBoolean(dt.Rows[1]["gdh_send_pay"]);
                chkChqRec.Checked = Convert.ToBoolean(dt.Rows[1]["gdh_coll_cheq"]);

                dtReg.Visible = false;
                dtCR.Visible = false;
                dtPay.Visible = false;
                dtChq.Visible = false;

                if (System.DBNull.Value != (dt.Rows[1]["gdh_reg_send_dt"]))
                {
                    dtReg.Text = (dt.Rows[1]["gdh_reg_send_dt"]).ToString();
                    dtReg.Visible = true;
                }

                if (System.DBNull.Value != (dt.Rows[1]["gdh_cr_dt"]))
                {
                    dtCR.Text = (dt.Rows[1]["gdh_cr_dt"]).ToString();
                    dtCR.Visible = true;
                }

                if (System.DBNull.Value != (dt.Rows[1]["gdh_pay_send_dt"]))
                {
                    dtPay.Text = (dt.Rows[1]["gdh_pay_send_dt"]).ToString();
                    dtPay.Visible = true;
                }

                if (System.DBNull.Value != (dt.Rows[1]["gdh_coll_dt"]))
                {
                    dtChq.Text = (dt.Rows[1]["gdh_coll_dt"]).ToString();
                    dtChq.Visible = true;
                }


                txtRegRem.Text = (dt.Rows[1]["gdh_reg_rmks"]).ToString();
                txtCRRem.Text = (dt.Rows[1]["gdh_cr_rmks"]).ToString();
                txtCR.Text = (dt.Rows[1]["gdh_cr_no"]).ToString();
                txtChq.Text = (dt.Rows[1]["gdh_chq_no"]).ToString();

            }

            for (int i = 0; i < _recordCNT; i++)
            {
                txtLine[i + 1].Text = (dt.Rows[i]["GDD_DOC"]).ToString();
                txtDesc[i + 1].Text = (dt.Rows[i]["hpd_desc"]).ToString();

                chkSend[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_ISSU"]);
                dtSDate[i + 1].Text = (dt.Rows[i]["GDD_ISS_DT"]).ToString();
                txtSRem[i + 1].Text = (dt.Rows[i]["GDD_ISS_RMKS"]).ToString();

                if (chkSend[i + 1].Checked == true)
                {
                    chkSend[i + 1].Enabled = false;
                    dtSDate[i + 1].Enabled = false;
                    txtSRem[i + 1].Enabled = false;
                }

                //chkRec[i + 1].Visible = false;
                //dtRDate[i + 1].Visible = false;
                //txtRRem[i + 1].Visible = false;

                //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10130))
                //{  comented on 9/6/2016 req by dilanda
                    chkRec[i + 1].Enabled = true;
                    dtRDate[i + 1].Enabled = true;
                    txtRRem[i + 1].Enabled = true;
                    chkRerec[i + 1].Enabled = true;
                    chkRet[i + 1].Enabled = true;
                    dtRetDate[i + 1].Enabled = true;
                    dtRerec[i + 1].Enabled = true;
                    txtRetRem[i + 1].Enabled = true;

              //  }

                chkRec[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_REC"]);
                dtRDate[i + 1].Text = (dt.Rows[i]["GDD_REC_DT"]).ToString();
                txtRRem[i + 1].Text = (dt.Rows[i]["GDD_REC_RMKS"]).ToString();

                chkRet[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_RET"]);
                dtRetDate[i + 1].Text = (dt.Rows[i]["GDD_RET_DT"]).ToString();
                txtRetRem[i + 1].Text = (dt.Rows[i]["GDD_RET_REM"]).ToString();

                chkRerec[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_REREC"]);
                dtRerec[i + 1].Text = (dt.Rows[i]["GDD_REREC_DT"]).ToString();
            }
        }

        private void Load_Docs(string _saleType)
        {
            DataTable dt = CHNLSVC.General.GetCrdSaleDocsData(_saleType);

            RemoveControls();
            pnlExecutive.Height = 31;
            pnlExecutive.Top = 3;
            _recordCNT = dt.Rows.Count;

            if (_recordCNT > 0)
            {
                LoadLine(_recordCNT);
                LoadDesc(_recordCNT);

                LoadSend(_recordCNT);
                LoadSDate(_recordCNT);
                LoadSRem(_recordCNT);

                LoadReceive(_recordCNT);
                LoadRDate(_recordCNT);
                LoadRRem(_recordCNT);

                LoadRet(_recordCNT);
                LoadRetDate(_recordCNT);
                LoadRetRem(_recordCNT);

                LoadReRec(_recordCNT);
                LoadReRecDate(_recordCNT);

            }

            for (int i = 0; i < _recordCNT; i++)
            {
                txtLine[i + 1].Text = (dt.Rows[i]["GDD_DOC"]).ToString();
                txtDesc[i + 1].Text = (dt.Rows[i]["hpd_desc"]).ToString();

                chkSend[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_ISSU"]);
                dtSDate[i + 1].Text = (dt.Rows[i]["GDD_ISS_DT"]).ToString();
                txtSRem[i + 1].Text = (dt.Rows[i]["GDD_ISS_RMKS"]).ToString();

                chkRec[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_REC"]);
                dtRDate[i + 1].Text = (dt.Rows[i]["GDD_REC_DT"]).ToString();
                txtRRem[i + 1].Text = (dt.Rows[i]["GDD_REC_RMKS"]).ToString();

                chkRet[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_RET"]);
                dtRetDate[i + 1].Text = (dt.Rows[i]["GDD_RET_DT"]).ToString();
                txtRetRem[i + 1].Text = (dt.Rows[i]["GDD_RET_REM"]).ToString();

                chkRerec[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GDD_IS_REREC"]);
                dtRerec[i + 1].Text = (dt.Rows[i]["GDD_REREC_DT"]).ToString();
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
                case "dtSDate":
                    {
                        dtSDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtSDate[i] = new System.Windows.Forms.DateTimePicker();
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

                case "chkSend":
                    {
                        chkSend = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkSend[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "chkRec":
                    {
                        chkRec = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkRec[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "dtRDate":
                    {
                        dtRDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtRDate[i] = new System.Windows.Forms.DateTimePicker();
                        }
                        break;
                    }
                case "txtSRem":
                    {
                        txtSRem = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSRem[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRRem":
                    {
                        txtRRem = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRRem[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "chkRet":
                    {
                        chkRet = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkRet[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "dtRetDate":
                    {
                        dtRetDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtRetDate[i] = new System.Windows.Forms.DateTimePicker();
                        }
                        break;
                    }
                case "txtRetRem":
                    {
                        txtRetRem = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRetRem[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "chkRerec":
                    {
                        chkRerec = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkRerec[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "dtRerec":
                    {
                        dtRerec = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtRerec[i] = new System.Windows.Forms.DateTimePicker();
                        }
                        break;
                    }
            }
        }

        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
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
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehRegTxn:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
            }

            return paramsText.ToString();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pnlExecutive.Top = -vScrollBar1.Value + 2;
        }

        private void btnADReciept_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VehRegTxn);
            DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtADReciept;
            _CommonSearch.txtSearchbyword.Text = txtADReciept.Text;
            txtADEngine.Text = "";
            txtADChassis.Text = "";
            _CommonSearch.ShowDialog();
            txtADReciept.Focus();
            Cursor.Current = Cursors.Default;
        }



        private void btnADAccount_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtADAccount;
            _CommonSearch.txtSearchbyword.Text = txtADAccount.Text;
            _CommonSearch.ShowDialog();
            txtADAccount.Focus();
        }

        //private void btnADProfitCenter_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //    _CommonSearch.ReturnIndex = 0;
        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
        //    DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = txtADProfitCenter;
        //    _CommonSearch.txtSearchbyword.Text = txtADProfitCenter.Text;
        //    _CommonSearch.ShowDialog();
        //    txtADProfitCenter.Focus();
        //}

        private void btnsearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            dataGridViewADSearchResult.AutoGenerateColumns = false;
            DataTable dt = CHNLSVC.General.GetVehicalSearch(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "Registration", txtADVehNo.Text, txtADChassis.Text, txtADEngine.Text, txtADInvoice.Text, txtADReciept.Text, txtADAccount.Text, DateTime.Today, DateTime.Today, null);
            dataGridViewADSearchResult.DataSource = dt;
            //cmbIDType.Focus();
            dataGridViewADSearchResult.Focus();
            Cursor.Current = Cursors.Default;
        }

        private void dataGridViewADSearchResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 _tmpSeqNo;
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                chkSendAll.Checked = false;
                RecieptNo = dataGridViewADSearchResult.Rows[e.RowIndex].Cells[1].Value.ToString();
                //Seq_No =Convert.ToInt32(dataGridViewADSearchResult.Rows[e.RowIndex].Cells[5].Value);

                LoadApplicationDetails();

                //check whether already save for this engine number
                if (CHNLSVC.General.checkCredSaleDocs(txtADEngine.Text, out _tmpSeqNo) == true)
                {
                    Seq_No = _tmpSeqNo;
                    Load_Saved_Docs();

                    _recordFound = 1;
                }
                else
                {
                    Load_Docs(dataGridViewADSearchResult.Rows[e.RowIndex].Cells[7].Value.ToString());
                    _recordFound = 0;
                }
            }
        }

        private void LoadApplicationDetails()
        {
            List<FF.BusinessObjects.VehicalRegistration> list = CHNLSVC.General.GetVehicalRegistrations(RecieptNo);
            if (list != null)
            {
                txtADReciept.Text = list[0].P_srvt_ref_no;
                txtADRegAmt.Text = list[0].P_svrt_reg_val.ToString();
                txtADClaimAmt.Text = list[0].P_svrt_claim_val.ToString();
                dtRecDate.Value = list[0].P_svrt_dt;

                List<InvoiceItem> Inolist = CHNLSVC.Sales.GetInvoiceDetailByInvoice(list[0].P_svrt_inv_no);
                txtADInvNo.Text = list[0].P_svrt_inv_no;
                if (Inolist != null)
                    txtADSaleAmt.Text = Inolist[0].Sad_unit_amt.ToString();
                dtInvDate.Value = list[0].P_svrt_dt;
                txtADSalesType.Text = list[0].P_svrt_sales_tp;

                //cus details
                txtLastName.Text = list[0].P_svrt_last_name;
                txtFullAnme.Text = list[0].P_svrt_full_name;
                txtInitials.Text = list[0].P_svrt_initial;

                txtADEngine.Text = list[0].P_svrt_engine;
                txtADChassis.Text = list[0].P_svrt_chassis;


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Int32 _seq_no;
            Boolean _isNonIssueDocFound = false;
            Boolean _isAllDocRec = false;
            Int32 _stus = 0;
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                if (Convert.ToDateTime(dtSDate[i].Text).Date > DateTime.Now.Date)
                {
                    MessageBox.Show("Cannot Save. Invalid date found !", "Registration Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (chkRec[i].Checked == true && chkRet[i].Checked == false && chkRerec[i].Checked == true)
                {
                    MessageBox.Show("Cannot Save. Invalid re-receive found !", "Registration Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (chkRec[i].Checked == false && chkRet[i].Checked == false && chkRerec[i].Checked == true)
                {
                    MessageBox.Show("Cannot Save. Invalid re-receive found !", "Registration Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtSDate[i], lblBackDateInfor, dtSDate[i].Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        //if (dtSDate[i].Value.Date != DateTime.Now.Date)
                        //{
                        //    dtSDate[i].Enabled = true;
                        //    MessageBox.Show("Back date not allow for selected date!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    dtSDate[i].Focus();
                        //    return;
                        //}
                    }
                    else
                    {
                        dtSDate[i].Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtSDate[i].Focus();
                        return;

                    }
                }

            }
            List<CreditSaleDocsDetail> CreditSaleDocList = new List<CreditSaleDocsDetail>();
            if (MessageBox.Show("Are you sure ?", "Credit Docs", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            if (_recordCNT == 0)
            {
                MessageBox.Show("Cannot Save. Details not found !", "Registration Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_recordFound == 1)
            {
                for (int i = 1; i < _recordCNT + 1; i++)
                {
                    CreditSaleDocsDetail _crdSaleDocDet = new CreditSaleDocsDetail();
                    _crdSaleDocDet.Gdd_seq = Seq_No;
                    _crdSaleDocDet.Gdd_doc = txtLine[i].Text;
                    _crdSaleDocDet.Gdd_is_issu = chkSend[i].Checked;
                    _crdSaleDocDet.Gdd_iss_dt = Convert.ToDateTime(dtSDate[i].Text).Date;
                    _crdSaleDocDet.Gdd_iss_rmks = txtSRem[i].Text;
                    _crdSaleDocDet.Gdd_iss_by = BaseCls.GlbUserID;
                    _crdSaleDocDet.Gdd_rec_dt = Convert.ToDateTime(dtRDate[i].Text).Date;

                    _crdSaleDocDet.Gdd_is_rec = chkRec[i].Checked;
                    _crdSaleDocDet.Gdd_rec_rmks = txtRRem[i].Text;

                    _crdSaleDocDet.Gdd_is_ret = chkRet[i].Checked;
                    _crdSaleDocDet.Gdd_ret_dt = Convert.ToDateTime(dtRetDate[i].Text).Date;
                    _crdSaleDocDet.Gdd_ret_rmks = txtRetRem[i].Text;

                    _crdSaleDocDet.Gdd_is_rerec = chkRerec[i].Checked;
                    _crdSaleDocDet.Gdd_rerec_dt = Convert.ToDateTime(dtRerec[i].Text).Date;

                    CreditSaleDocList.Add(_crdSaleDocDet);

                    if (chkSend[i].Checked == false)
                    {
                        _isNonIssueDocFound = true;
                    }
                    if (chkRec[i].Checked == true && chkRet[i].Checked == true && chkRerec[i].Checked == false)
                        _isAllDocRec = true;
                    if (chkRec[i].Checked == false && chkRet[i].Checked == true && chkRerec[i].Checked == false)
                        _isAllDocRec = true;
                    if (chkRec[i].Checked == false && chkRet[i].Checked == false && chkRerec[i].Checked == false)
                        _isAllDocRec = true;
                }

                if (_isNonIssueDocFound == false)
                {
                    Int32 Z = CHNLSVC.General.UpdateCredSaleDocAllIssue(Convert.ToDateTime(DateTime.Now).Date, Seq_No);
                }
                //kapila
                if (_isAllDocRec == false)
                    _stus = 1;
                else
                    _stus = 0;

                Int32 D = CHNLSVC.Sales.Update_veh_DocReg_stus(Seq_No, Convert.ToDateTime(DateTime.Now).Date, "", "", _stus);

                Int32 Y = CHNLSVC.General.SaveCredSaleDocDetail(CreditSaleDocList);
            }

            else
            {

                CreditSaleDocsHeader _crdSaleDocHdr = new CreditSaleDocsHeader();

                _crdSaleDocHdr.Gdh_com = BaseCls.GlbUserComCode;
                _crdSaleDocHdr.Gdh_pc = BaseCls.GlbUserDefProf;
                _crdSaleDocHdr.Gdh_inv = txtADInvNo.Text;
                _crdSaleDocHdr.Gdh_inv_dt = Convert.ToDateTime(dtInvDate.Text).Date;
                _crdSaleDocHdr.Gdh_rec = txtADReciept.Text;
                _crdSaleDocHdr.Gdh_recipt_dt = Convert.ToDateTime(dtRecDate.Text).Date;
                _crdSaleDocHdr.Gdh_engine = txtADEngine.Text;
                _crdSaleDocHdr.Gdh_chassis = txtADChassis.Text;
                _crdSaleDocHdr.Gdh_isse_doc = false;
                _crdSaleDocHdr.Gdh_rec_doc = false;
                _crdSaleDocHdr.Gdh_amt = Convert.ToDecimal(txtADSaleAmt.Text);

                Int32 X = CHNLSVC.General.SaveCredSaleDocHeader(_crdSaleDocHdr, out _seq_no);

                for (int i = 1; i < _recordCNT + 1; i++)
                {
                    CreditSaleDocsDetail _crdSaleDocDet = new CreditSaleDocsDetail();
                    _crdSaleDocDet.Gdd_seq = _seq_no;
                    _crdSaleDocDet.Gdd_line = i;
                    _crdSaleDocDet.Gdd_doc = txtLine[i].Text;
                    _crdSaleDocDet.Gdd_is_issu = chkSend[i].Checked;
                    _crdSaleDocDet.Gdd_iss_dt = Convert.ToDateTime(dtSDate[i].Text).Date;
                    _crdSaleDocDet.Gdd_iss_rmks = txtSRem[i].Text;
                    _crdSaleDocDet.Gdd_iss_by = BaseCls.GlbUserID;

                    _crdSaleDocDet.Gdd_is_ret = chkRet[i].Checked;
                    _crdSaleDocDet.Gdd_ret_dt = Convert.ToDateTime(dtRetDate[i].Text).Date;
                    _crdSaleDocDet.Gdd_ret_rmks = txtRetRem[i].Text;

                    _crdSaleDocDet.Gdd_is_rerec = chkRerec[i].Checked;
                    _crdSaleDocDet.Gdd_rerec_dt = Convert.ToDateTime(dtRerec[i].Text).Date;

                    CreditSaleDocList.Add(_crdSaleDocDet);

                    if (chkSend[i].Checked == false)
                    {
                        _isNonIssueDocFound = true;
                    }
                    if (chkRec[i].Checked == true && chkRet[i].Checked == true && chkRerec[i].Checked == false)
                        _isAllDocRec = true;
                    if (chkRec[i].Checked == false && chkRet[i].Checked == true && chkRerec[i].Checked == false)
                        _isAllDocRec = true;
                    if (chkRec[i].Checked == false && chkRet[i].Checked == false && chkRerec[i].Checked == false)
                        _isAllDocRec = true;

                }
                if (_isNonIssueDocFound == false)
                {
                    Int32 Z = CHNLSVC.General.UpdateCredSaleDocAllIssue(Convert.ToDateTime(DateTime.Now).Date, _seq_no);
                }
                //kapila
                if (_isAllDocRec == false)
                    _stus = 1;
                else
                    _stus = 0;

                Int32 D = CHNLSVC.Sales.Update_veh_DocReg_stus(Seq_No, Convert.ToDateTime(DateTime.Now).Date, "", "", _stus);

                Int32 Y = CHNLSVC.General.SaveCredSaleDocDetail(CreditSaleDocList);
            }
            //RemoveControls();
            //_recordCNT = 0;
            //_recordFound = 0;
            btnClear_Click(null, null);
            MessageBox.Show("Successfully Saved", "Registration Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            RemoveControls();
            chkChqRec.Checked = false;
            chkCRRec.Checked = false;
            chkPending.Checked = false;
            chkSendPay.Checked = false;

            dtChq.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtCR.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtInvDate.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtPay.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtRecDate.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtReg.Value = Convert.ToDateTime(DateTime.Now).Date;

            txtRegRem.Text = "";
            txtCR.Text = "";
            txtChq.Text = "";
            txtCRRem.Text = "";
            txtADReciept.Text = "";

            txtFullAnme.Text = "";
            txtInitials.Text = "";
            txtLastName.Text = "";
            dataGridViewADSearchResult.DataSource = null;
            txtADAccount.Text = "";
            txtADChassis.Text = "";
            txtADClaimAmt.Text = "";

            txtADEngine.Text = "";
            txtADInvNo.Text = "";
            txtADInvoice.Text = "";


            txtADRegAmt.Text = "";
            txtADSaleAmt.Text = "";
            txtADSalesType.Text = "";
            txtADVehNo.Text = "";
            chkSendAll.Checked = false;

        }

        private void txtADReciept_TextChanged(object sender, EventArgs e)
        {
            txtADEngine.Text = "";
            txtADChassis.Text = "";

            RemoveControls();
            chkChqRec.Checked = false;
            chkCRRec.Checked = false;
            chkPending.Checked = false;
            chkSendPay.Checked = false;

            dtChq.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtCR.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtInvDate.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtPay.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtRecDate.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtReg.Value = Convert.ToDateTime(DateTime.Now).Date;

            txtRegRem.Text = "";
            txtCR.Text = "";
            txtChq.Text = "";
            txtCRRem.Text = "";

            txtFullAnme.Text = "";
            txtInitials.Text = "";
            txtLastName.Text = "";
            //dataGridViewADSearchResult.DataSource = null;
            txtADAccount.Text = "";
            txtADChassis.Text = "";
            txtADClaimAmt.Text = "";

            txtADEngine.Text = "";
            txtADInvNo.Text = "";
            txtADInvoice.Text = "";


            txtADRegAmt.Text = "";
            txtADSaleAmt.Text = "";
            txtADSalesType.Text = "";
            txtADVehNo.Text = "";
        }

        private void txtADAccount_DoubleClick(object sender, EventArgs e)
        {
            btnADAccount_Click(null, null);
        }

        private void txtADAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnADAccount_Click(null, null);
            }
            else
                if (e.KeyCode == Keys.Enter)
                    btnsearch.Focus();
        }

        private void txtADReciept_DoubleClick(object sender, EventArgs e)
        {
            btnADReciept_Click(null, null);
        }

        private void txtADReciept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnADReciept_Click(null, null);
            }
            else
                if (e.KeyCode == Keys.Enter)
                    btnsearch.Focus();
        }

        private void chkSendAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSendAll.Checked == true)
            {
                for (int i = 0; i < _recordCNT; i++)
                {
                    if (chkSend[i + 1].Enabled == true)
                    {
                        chkSend[i + 1].Checked = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _recordCNT; i++)
                {
                    if (chkSend[i + 1].Enabled == true)
                    {
                        chkSend[i + 1].Checked = false;
                    }
                }
            }
        }

        private void Credit_Sale_Docs_Load(object sender, EventArgs e)
        {
            BackDatePermission();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view1 = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = string.Empty;
            _view1.GlbReportName = string.Empty;
            _view1.GlbReportName = "VehicleRegistrationSlip.rpt";
            BaseCls.GlbReportName = "VehicleRegistrationSlip.rpt";
            _view1.GlbReportDoc = RecieptNo;
            BaseCls.GlbReportDoc = RecieptNo;
            _view1.Show();
            _view1 = null;
        }

    }
}
