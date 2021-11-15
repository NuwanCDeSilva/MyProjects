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
using FF.WindowsERPClient.Finance;
using System.IO;

namespace FF.WindowsERPClient.Finance
{
    //sp_update_GNT_RCV_DSK_DOC =UPDATE
    //get_GNT_RCV_DSK_DOC =UPDATE 
    //sp_getShortBankDocs =UPDATE
    //get_GNT_RCV_DSK_DOC_FORDate =NEW
    //sp_save_Gnt_rcv_dsk_doc  =UPDATE

    //SP_Delete_GNT_RCV_DSK_DOC_NEW= new
    //sp_Process_Docs =update
    //sp_RemDet =update
    //sp_get_BNK_ON_PC =update
    //getSunUploaded_gnt_rcv_dsk_doc =new
    //sp_update_GNT_RCV_DSK_DOC =update
    //sp_getShortBankDocs =update

    public partial class Scan_Physical_Docs : Base
    {
        //private System.Windows.Forms.Button btnAddText;
        //private System.Windows.Forms.DateTimePicker btnAddDate;
        private Boolean _noRec = false;
        private Boolean _isScreenChanged = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox textBox = new TextBox();
            btnProcess.Enabled = false;
            //  txtPhyRecVal[0].TextChanged += new EventHandler(textBox_TextChanged);
        }
        protected void sunUploadCheck_Changed(object sender, EventArgs e)
        {

            MessageBox.Show("Cannot change. Sun Uploaded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;


            //DataTable dt_CHECK_FOR_available = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null);

            //foreach (CheckBox chekbox in  chkSun)
            //{   
            //    if (chekbox!=null)
            //    {
            //        CheckState CHST = CheckState.Checked;                    
            //        Int32 ischecked = 0;
            //        if (CheckState.Checked == chekbox.CheckState)
            //        {
            //            ischecked = 1;
            //        }
            //        foreach (DataRow drr in dt_CHECK_FOR_available.Rows)
            //        {
            //            Int32 sunUplaod_by = Convert.ToInt32(drr["grdd_sun_upload"].ToString());
            //            if (sunUplaod_by != ischecked)
            //            {
            //                MessageBox.Show("Cannot change. Sun Uploaded!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //        }
            //    }               
            //}            
        }

        protected void textBox_TextChanged(object sender, EventArgs e)
        {
            // Your code here
            try
            {
                foreach (TextBox tx in txtPhyRecVal)
                {
                    if (tx != null)
                    {
                        if (tx.Text.Trim() != "")
                        {
                            try
                            {
                                Convert.ToDecimal(tx.Text.Trim());

                                //---------------------
                                Decimal phyVal = 0;
                                //Int32 _arrIndex = Convert.ToInt32(((System.Windows.Forms.CheckBox)sender).Tag);
                                lblSelTot.Text = "0.00";
                                for (int i = 1; i < _recordCNT_Rec + 1; i++)
                                {
                                    if (chkPhyRec[i].Checked == true)
                                    {
                                        if (!string.IsNullOrEmpty((txtPhyRecVal[i].Text)))
                                        {
                                            phyVal = Convert.ToDecimal(txtPhyRecVal[i].Text);
                                        }
                                        if (phyVal <= 0)
                                        {
                                            return;
                                        }
                                        string docTp = txtRDocTp[i].Text;
                                        int count = BindSumList.Count(D => D.Key.StartsWith(docTp));
                                        if (count == 0)
                                        {
                                            //BindSumList.Add(docTp, phyVal);
                                            lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) + phyVal).ToString());
                                        }
                                        else if (count > 0)
                                        {
                                            Decimal val = BindSumList[docTp];
                                            if (chkPhyRec[i].Checked)
                                            {
                                                val = val + phyVal;
                                                lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) + phyVal).ToString());
                                            }
                                            else
                                            {
                                                val = val - phyVal;
                                                lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) - phyVal).ToString());
                                            }

                                        }
                                    }
                                }

                                //---------------------
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid amount!");
                                tx.Text = "0.00";
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
        protected void textBoxPhyRecValSun_TextChanged(object sender, EventArgs e)
        {
            // Your code here
            try
            {
                foreach (TextBox tx in txtPhyRecValSun)
                {
                    if (tx != null)
                    {
                        if (tx.Text.Trim() != "")
                        {
                            try
                            {
                                Convert.ToDecimal(tx.Text.Trim());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Invalid amount!");
                                tx.Text = "0.00";
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

        private static int _TabIndex = 0;
        private static int _recordCNT = 0;
        private static int _recordCNT_Rec = 0;
        private static int _recordCNT_Sun = 0;
        private static int IS_SCAN = 0;
        private static int IS_SUN = 0;
        private static int IS_RECEIVE = 0;

        private System.Windows.Forms.TextBox[] txtLine;
        private System.Windows.Forms.DateTimePicker[] dtDate;
        private System.Windows.Forms.TextBox[] txtDesc;
        private System.Windows.Forms.TextBox[] txtRef;
        private System.Windows.Forms.TextBox[] txtBank;
        private System.Windows.Forms.TextBox[] txtAmount;
        private System.Windows.Forms.CheckBox[] chkScan;
        private System.Windows.Forms.CheckBox[] chkRelz;
        private System.Windows.Forms.DateTimePicker[] dtRelzDate;
        private System.Windows.Forms.TextBox[] txtDocTp;
        private System.Windows.Forms.TextBox[] txtDocRemarks;
        // private System.Windows.Forms.TextBox[] txtIsShortSet;

        private System.Windows.Forms.TextBox[] txtRLine;
        private System.Windows.Forms.DateTimePicker[] dtRDate;
        private System.Windows.Forms.TextBox[] txtRDesc;
        private System.Windows.Forms.TextBox[] txtRRef;
        private System.Windows.Forms.TextBox[] txtRBank;
        private System.Windows.Forms.TextBox[] txtRAmount;
        private System.Windows.Forms.CheckBox[] chkPhyRec;
        private System.Windows.Forms.TextBox[] txtPhyRecVal;
        private System.Windows.Forms.ComboBox[] cmbBank;
        private System.Windows.Forms.TextBox[] txtBankCode;
        private System.Windows.Forms.TextBox[] txtBranch;
        private System.Windows.Forms.TextBox[] txtRem;
        private System.Windows.Forms.TextBox[] txtRDocTp;
        private System.Windows.Forms.TextBox[] txtPhyRDate;

        private System.Windows.Forms.TextBox[] txtSLine;
        private System.Windows.Forms.DateTimePicker[] dtSDate;
        private System.Windows.Forms.TextBox[] txtSDesc;
        private System.Windows.Forms.TextBox[] txtSRef;
        private System.Windows.Forms.TextBox[] txtSBank;
        private System.Windows.Forms.TextBox[] txtSAmount;
        private System.Windows.Forms.TextBox[] txtPhyRecValSun;
        private System.Windows.Forms.CheckBox[] chkSun;
        private System.Windows.Forms.TextBox[] txtSDocTp;
        private System.Windows.Forms.TextBox[] cmbSBank;



        public class RemDet
        {
            private string date;
            private Decimal prevExcessRem;
            private Decimal excessRem;
            private Decimal cashIH;
            private Decimal amtRemited;
            private Decimal difference;

            public string Date
            {
                get { return date; }
                set { date = value; }
            }

            public Decimal PrevExcessRem
            {
                get { return prevExcessRem; }
                set { prevExcessRem = value; }
            }

            public Decimal ExcessRem
            {
                get { return excessRem; }
                set { excessRem = value; }
            }

            public Decimal CashIH
            {
                get { return cashIH; }
                set { cashIH = value; }
            }

            public Decimal AmtRemited
            {
                get { return amtRemited; }
                set { amtRemited = value; }
            }
            public Decimal Difference
            {
                get { return difference; }
                set { difference = value; }
            }
        }
        private void bind_Combo_ddlDocTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("ALL", "ALL");
            PartyTypes.Add("CHEQUE", "CHEQUE");
            PartyTypes.Add("CS_CHEQUE", "CS SETTLEMENT-CHEQUES");
            PartyTypes.Add("CS_CASH", "CS SETTLEMENT-CASH");
            PartyTypes.Add("CRCD", "CREDIT CARD");
            PartyTypes.Add("ADVAN", "ADVANCE RECEIPTS");
            PartyTypes.Add("DEPOSIT", "BANK DEPOSIT SLIP");
            PartyTypes.Add("COLL-BONUS", "COLLECTION BONUS");
            PartyTypes.Add("PROD-BONUS", "PRODUCT BONUS");//ZM-VOUCHER
            PartyTypes.Add("ZM-VOUCHER", "ZONE MANAGER VOUCHER");
            PartyTypes.Add("GV", "GIFT VOUCHER");
            PartyTypes.Add("GVO", "OTHER GIFT VOUCHER");
            PartyTypes.Add("SB", "SHORT BANK");
            PartyTypes.Add("BANK_SLIP", "BANK SLIP");   //20/2/2016
            ddlDocTypes.DataSource = new BindingSource(PartyTypes, null);
            ddlDocTypes.DisplayMember = "Value";
            ddlDocTypes.ValueMember = "Key";
        }

        Dictionary<string, Decimal> BindSumList = new Dictionary<string, Decimal>();

        public void ClickTextBox(Object sender, System.EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Edit me, then Click Label which has same number.");
        }

        private void RemoveControls()
        {
            for (int i = 1; i < _recordCNT + 1; i++)
            {
                pnlDate.Controls.Remove(dtDate[i]);
                pnlLine.Controls.Remove(txtLine[i]);
                pnlRef.Controls.Remove(txtRef[i]);
                pnlBank.Controls.Remove(txtBank[i]);
                pnlAmount.Controls.Remove(txtAmount[i]);
                pnlDesc.Controls.Remove(txtDesc[i]);

                pnlRealise.Controls.Remove(chkRelz[i]);
                pnlRealiseDate.Controls.Remove(dtRelzDate[i]);
                pnlScan.Controls.Remove(chkScan[i]);
                pnlDocTp.Controls.Remove(txtDocTp[i]);
                pnlDocRemarks.Controls.Remove(txtDocRemarks[i]);
                // pnlIsShortSet.Controls.Remove(txtIsShortSet[i]);
            }
        }

        private void RemoveControls_RecDesk()
        {
            for (int i = 1; i < _recordCNT_Rec + 1; i++)
            {
                pnlRDate.Controls.Remove(dtRDate[i]);
                pnlRLine.Controls.Remove(txtRLine[i]);
                pnlRRef.Controls.Remove(txtRRef[i]);
                pnlRBank.Controls.Remove(txtRBank[i]);
                pnlRAmount.Controls.Remove(txtRAmount[i]);
                pnlRDesc.Controls.Remove(txtRDesc[i]);
                pnlPhyRec.Controls.Remove(chkPhyRec[i]);
                pnlPhyRecVal.Controls.Remove(txtPhyRecVal[i]);
                pnlPhyRDate.Controls.Remove(txtPhyRDate[i]);
                pnlBankR.Controls.Remove(cmbBank[i]);
                pnlBankCode.Controls.Remove(txtBankCode[i]);
                pnlBranch.Controls.Remove(txtBranch[i]);
                pnlRem.Controls.Remove(txtRem[i]);
                pnlRDocTp.Controls.Remove(txtRDocTp[i]);

            }
        }

        private void RemoveControls_Sun()
        {
            for (int i = 1; i < _recordCNT_Sun + 1; i++)
            {
                pnlSDate.Controls.Remove(dtSDate[i]);
                pnlSLine.Controls.Remove(txtSLine[i]);
                pnlSRef.Controls.Remove(txtSRef[i]);
                pnlSBank.Controls.Remove(txtSBank[i]);
                pnlSAmount.Controls.Remove(txtSAmount[i]);
                pnlSDesc.Controls.Remove(txtSDesc[i]);
                pnlPhyRecValSun.Controls.Remove(txtPhyRecValSun[i]);
                pnlSUN.Controls.Remove(chkSun[i]);
                pnlSDocTp.Controls.Remove(txtSDocTp[i]);
                pnlSBankCode.Controls.Remove(cmbSBank[i]);
            }
        }

        private void LoadSDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("dtSDate", _num);
            int n = 1;
            pnlSDate.Height = 25;
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
                //dtSDate[n].Enabled = false;
                pnlSDate.Controls.Add(dtSDate[n]);
                pnlSDate.Height = pnlSDate.Height + 25;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadSLine(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtSLine", _num);
            int n = 1;
            pnlSLine.Height = 25;
            while (n < _num + 1)
            {
                pnlSunUser.Height = pnlSunUser.Height + 31;
                txtSLine[n].Tag = n;
                txtSLine[n].Width = 50;
                txtSLine[n].Height = 21;
                txtSLine[n].BackColor = System.Drawing.SystemColors.Info;
                txtSLine[n].Left = xPos;
                txtSLine[n].Top = yPos;
                yPos = yPos + txtSLine[n].Height + 4;
                pnlSLine.Controls.Add(txtSLine[n]);
                pnlSLine.Height = pnlSLine.Height + 25;
                n++;
            }
        }

        private void LoadSDesc(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtSDesc", _num);
            int n = 1;
            pnlSDesc.Height = 25;
            while (n < _num + 1)
            {
                txtSDesc[n].Tag = n;
                txtSDesc[n].Width = 187;
                txtSDesc[n].Height = 21;
                txtSDesc[n].BackColor = System.Drawing.SystemColors.Info;
                txtSDesc[n].Enabled = false;
                txtSDesc[n].Left = xPos;
                txtSDesc[n].Top = yPos;
                yPos = yPos + txtSDesc[n].Height + 4;
                pnlSDesc.Controls.Add(txtSDesc[n]);
                pnlSDesc.Height = pnlSDesc.Height + 25;
                n++;
            }
        }

        private void LoadSRef(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtSRef", _num);
            int n = 1;
            pnlSRef.Height = 25;
            while (n < _num + 1)
            {
                txtSRef[n].Tag = n;
                txtSRef[n].Width = 95;
                txtSRef[n].Height = 21;
                txtSRef[n].BackColor = System.Drawing.SystemColors.Info;
                // txtSRef[n].Enabled = false;
                txtSRef[n].Left = xPos;
                txtSRef[n].Top = yPos;
                yPos = yPos + txtSRef[n].Height + 4;
                pnlSRef.Controls.Add(txtSRef[n]);
                pnlSRef.Height = pnlSRef.Height + 25;
                n++;
            }
        }

        private void LoadSBank(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtSBank", _num);
            int n = 1;
            pnlSBank.Height = 25;
            while (n < _num + 1)
            {
                txtSBank[n].Tag = n;
                txtSBank[n].Width = 52;
                txtSBank[n].Height = 21;
                txtSBank[n].BackColor = System.Drawing.SystemColors.Info;
                txtSBank[n].Enabled = false;
                txtSBank[n].Left = xPos;
                txtSBank[n].Top = yPos;
                yPos = yPos + txtSBank[n].Height + 4;
                pnlSBank.Controls.Add(txtSBank[n]);
                pnlSBank.Height = pnlSBank.Height + 25;
                n++;
            }
        }



        private void LoadPhyRecValSun(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtPhyRecValSun", _num);
            int n = 1;
            pnlPhyRecValSun.Height = 25;
            while (n < _num + 1)
            {
                txtPhyRecValSun[n].Tag = n;
                txtPhyRecValSun[n].Width = 75;
                txtPhyRecValSun[n].Height = 21;
                txtPhyRecValSun[n].BackColor = System.Drawing.SystemColors.Info;
                txtPhyRecValSun[n].TextAlign = HorizontalAlignment.Right;
                txtPhyRecValSun[n].Left = xPos;
                txtPhyRecValSun[n].Top = yPos;
                yPos = yPos + txtPhyRecValSun[n].Height + 4;
                pnlPhyRecValSun.Controls.Add(txtPhyRecValSun[n]);
                pnlPhyRecValSun.Height = pnlPhyRecValSun.Height + 25;
                n++;
            }
        }

        private void LoadSunUpload(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("chkSun", _num);
            int n = 1;
            pnlSUN.Height = 25;
            while (n < _num + 1)
            {
                chkSun[n].Tag = n;
                chkSun[n].Width = 15;
                chkSun[n].Height = 14;
                chkSun[n].Left = xPos;
                chkSun[n].Top = yPos;
                yPos = yPos + chkSun[n].Height + 10;
                pnlSUN.Controls.Add(chkSun[n]);
                pnlSUN.Height = pnlSUN.Height + 25;
                //if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                chkSun[n].Enabled = false;

                // chkSun[n].CheckedChanged+= sunUploadCheck_Changed(null, null);
                //chkSun[n].CheckedChanged += new System.EventHandler(sunUploadCheck_Changed(null,null));
                n++;
            }
        }

        private void LoadSDocTp(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtSDocTp", _num);
            int n = 1;
            pnlSDocTp.Height = 25;
            while (n < _num + 1)
            {
                txtSDocTp[n].Tag = n;
                txtSDocTp[n].Width = 75;
                txtSDocTp[n].Height = 21;
                txtSDocTp[n].BackColor = System.Drawing.SystemColors.Info;
                txtSDocTp[n].TextAlign = HorizontalAlignment.Right;
                txtSDocTp[n].Left = xPos;
                txtSDocTp[n].Top = yPos;
                yPos = yPos + txtSDocTp[n].Height + 4;
                pnlSDocTp.Controls.Add(txtSDocTp[n]);
                pnlSDocTp.Height = pnlSDocTp.Height + 25;
                n++;
            }
        }

        private void LoadRDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("dtRDate", _num);
            int n = 1;
            pnlRDate.Height = 25;
            while (n < _num + 1)
            {
                dtRDate[n].Tag = n;
                dtRDate[n].Width = 104;
                dtRDate[n].Height = 21;
                dtRDate[n].Left = xPos;
                dtRDate[n].Top = yPos;
                yPos = yPos + dtRDate[n].Height + 4;
                dtRDate[n].Format = DateTimePickerFormat.Custom;
                dtRDate[n].CustomFormat = "dd/MMM/yyyy";
                dtRDate[n].Enabled = false;
                pnlRDate.Controls.Add(dtRDate[n]);
                pnlRDate.Height = pnlRDate.Height + 25;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadRLine(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRLine", _num);
            int n = 1;
            pnlRLine.Height = 25;
            while (n < _num + 1)
            {
                pnlRec.Height = pnlRec.Height + 31;
                txtRLine[n].Tag = n;
                txtRLine[n].Width = 50;
                txtRLine[n].Height = 21;
                txtRLine[n].BackColor = System.Drawing.SystemColors.Info;
                txtRLine[n].Left = xPos;
                txtRLine[n].Top = yPos;
                yPos = yPos + txtRLine[n].Height + 4;
                pnlRLine.Controls.Add(txtRLine[n]);
                pnlRLine.Height = pnlRLine.Height + 25;
                n++;
            }
        }

        private void LoadRDesc(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRDesc", _num);
            int n = 1;
            pnlRDesc.Height = 25;
            while (n < _num + 1)
            {
                txtRDesc[n].Tag = n;
                txtRDesc[n].Width = 177;
                txtRDesc[n].Height = 21;
                txtRDesc[n].BackColor = System.Drawing.SystemColors.Info;
                txtRDesc[n].Enabled = false;
                txtRDesc[n].Left = xPos;
                txtRDesc[n].Top = yPos;
                yPos = yPos + txtRDesc[n].Height + 4;
                pnlRDesc.Controls.Add(txtRDesc[n]);
                pnlRDesc.Height = pnlRDesc.Height + 25;
                n++;
            }
        }

        private void LoadRRef(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRRef", _num);
            int n = 1;
            pnlRRef.Height = 25;
            while (n < _num + 1)
            {
                txtRRef[n].Tag = n;
                txtRRef[n].Width = 105;
                txtRRef[n].Height = 21;
                txtRRef[n].BackColor = System.Drawing.SystemColors.Info;
                //  txtRRef[n].Enabled = false;
                txtRRef[n].Left = xPos;
                txtRRef[n].Top = yPos;
                yPos = yPos + txtRRef[n].Height + 4;
                pnlRRef.Controls.Add(txtRRef[n]);
                pnlRRef.Height = pnlRRef.Height + 25;
                //  if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                txtRRef[n].Enabled = false;

                n++;
            }
        }

        private void LoadRBank(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRBank", _num);
            int n = 1;
            pnlRBank.Height = 25;
            while (n < _num + 1)
            {
                txtRBank[n].Tag = n;
                txtRBank[n].Width = 52;
                txtRBank[n].Height = 21;
                txtRBank[n].BackColor = System.Drawing.SystemColors.Info;
                txtRBank[n].Enabled = false;
                txtRBank[n].Left = xPos;
                txtRBank[n].Top = yPos;
                yPos = yPos + txtRBank[n].Height + 4;
                pnlRBank.Controls.Add(txtRBank[n]);
                pnlRBank.Height = pnlRBank.Height + 25;
                n++;
            }
        }

        private void LoadRAmount(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRAmount", _num);
            int n = 1;
            pnlRAmount.Height = 25;
            while (n < _num + 1)
            {
                txtRAmount[n].Tag = n;
                txtRAmount[n].Width = 75;
                txtRAmount[n].Height = 21;
                txtRAmount[n].BackColor = System.Drawing.SystemColors.Info;
                txtRAmount[n].TextAlign = HorizontalAlignment.Right;
                txtRAmount[n].Enabled = false;
                txtRAmount[n].Left = xPos;
                txtRAmount[n].Top = yPos;
                yPos = yPos + txtRAmount[n].Height + 4;
                pnlRAmount.Controls.Add(txtRAmount[n]);
                pnlRAmount.Height = pnlRAmount.Height + 25;
                n++;
            }
        }

        private void LoadPhyRec(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("chkPhyRec", _num);
            int n = 1;
            pnlPhyRec.Height = 25;
            while (n < _num + 1)
            {
                chkPhyRec[n].Tag = n;
                chkPhyRec[n].Width = 15;
                chkPhyRec[n].Height = 14;
                chkPhyRec[n].Left = xPos;
                chkPhyRec[n].Top = yPos;
                yPos = yPos + chkPhyRec[n].Height + 10;
                pnlPhyRec.Controls.Add(chkPhyRec[n]);
                pnlPhyRec.Height = pnlPhyRec.Height + 25;
                chkPhyRec[n].CheckedChanged += new System.EventHandler(chkPhyRec_CheckedChanged);
                n++;
            }
        }

        private void LoadPhyRecVal(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtPhyRecVal", _num);
            int n = 1;
            pnlPhyRecVal.Height = 25;
            while (n < _num + 1)
            {
                txtPhyRecVal[n].Tag = n;
                txtPhyRecVal[n].Width = 75;
                txtPhyRecVal[n].Height = 21;
                txtPhyRecVal[n].BackColor = System.Drawing.SystemColors.Info;
                txtPhyRecVal[n].TextAlign = HorizontalAlignment.Right;
                txtPhyRecVal[n].Left = xPos;
                txtPhyRecVal[n].Top = yPos;
                yPos = yPos + txtPhyRecVal[n].Height + 4;
                pnlPhyRecVal.Controls.Add(txtPhyRecVal[n]);
                pnlPhyRecVal.Height = pnlPhyRecVal.Height + 25;
                // if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                txtPhyRecVal[n].Enabled = false;
                n++;
            }
        }

        private void LoadPhyRDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtPhyRDate", _num);
            int n = 1;
            pnlPhyRDate.Height = 25;
            while (n < _num + 1)
            {
                txtPhyRDate[n].Tag = n;
                txtPhyRDate[n].Width = 75;
                txtPhyRDate[n].Height = 21;
                txtPhyRDate[n].BackColor = System.Drawing.SystemColors.Info;
                txtPhyRDate[n].TextAlign = HorizontalAlignment.Right;
                txtPhyRDate[n].Left = xPos;
                txtPhyRDate[n].Top = yPos;
                txtPhyRDate[n].Enabled = false;
                //txtPhyRDate[n].Font = new Font("Tahoma", 7);
                yPos = yPos + txtPhyRDate[n].Height + 4;
                pnlPhyRDate.Controls.Add(txtPhyRDate[n]);
                pnlPhyRDate.Height = pnlPhyRDate.Height + 25;
                n++;
            }
        }

        private void LoadBankR(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("cmbBank", _num);
            int n = 1;
            pnlBankR.Height = 25;
            while (n < _num + 1)
            {
                cmbBank[n].Tag = n;
                cmbBank[n].Width = 52;
                cmbBank[n].Height = 21;
                cmbBank[n].Left = xPos;
                cmbBank[n].Top = yPos;
                yPos = yPos + cmbBank[n].Height + 3;
                pnlBankR.Controls.Add(cmbBank[n]);
                pnlBankR.Height = pnlBankR.Height + 25;
                //if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                cmbBank[n].Enabled = false;
                n++;
            }
        }

        private void LoadBankS(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("cmbSBank", _num);
            int n = 1;
            pnlSBankCode.Height = 25;
            while (n < _num + 1)
            {
                cmbSBank[n].Tag = n;
                cmbSBank[n].Width = 52;
                cmbSBank[n].Height = 21;
                cmbSBank[n].Left = xPos;
                cmbSBank[n].Top = yPos;
                yPos = yPos + cmbSBank[n].Height + 4;
                pnlSBankCode.Controls.Add(cmbSBank[n]);
                pnlSBankCode.Height = pnlSBankCode.Height + 25;
                n++;
            }
        }

        private void LoadSAmount(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_Sun("txtSAmount", _num);
            int n = 1;
            pnlSAmount.Height = 25;
            while (n < _num + 1)
            {
                txtSAmount[n].Tag = n;
                txtSAmount[n].Width = 75;
                txtSAmount[n].Height = 21;
                txtSAmount[n].BackColor = System.Drawing.SystemColors.Info;
                txtSAmount[n].TextAlign = HorizontalAlignment.Right;
                txtSAmount[n].Enabled = false;
                txtSAmount[n].Left = xPos;
                txtSAmount[n].Top = yPos;
                yPos = yPos + txtSAmount[n].Height + 4;
                pnlSAmount.Controls.Add(txtSAmount[n]);
                pnlSAmount.Height = pnlSAmount.Height + 25;
                n++;
            }
        }

        private void LoadBankCode(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtBankCode", _num);
            int n = 1;
            pnlBankCode.Height = 25;
            while (n < _num + 1)
            {
                txtBankCode[n].Tag = n;
                txtBankCode[n].Width = 62;
                txtBankCode[n].Height = 21;
                txtBankCode[n].BackColor = System.Drawing.SystemColors.Info;
                //txtBankCode[n].Enabled = false;
                txtBankCode[n].Left = xPos;
                txtBankCode[n].Top = yPos;
                yPos = yPos + txtBankCode[n].Height + 4;
                pnlBankCode.Controls.Add(txtBankCode[n]);
                pnlBankCode.Height = pnlBankCode.Height + 25;
                // if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                txtBankCode[n].Enabled = false;
                n++;
            }
        }

        private void LoadBranch(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtBranch", _num);
            int n = 1;
            pnlBranch.Height = 25;
            while (n < _num + 1)
            {
                txtBranch[n].Tag = n;
                txtBranch[n].Width = 62;
                txtBranch[n].Height = 21;
                txtBranch[n].BackColor = System.Drawing.SystemColors.Info;
                //txtBranch[n].Enabled = false;
                txtBranch[n].Left = xPos;
                txtBranch[n].Top = yPos;
                yPos = yPos + txtBranch[n].Height + 4;
                pnlBranch.Controls.Add(txtBranch[n]);
                pnlBranch.Height = pnlBranch.Height + 25;
                // if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                txtBranch[n].Enabled = false;
                n++;
            }
        }

        private void LoadRem(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRem", _num);
            int n = 1;
            pnlRem.Height = 25;
            while (n < _num + 1)
            {
                txtRem[n].Tag = n;
                txtRem[n].Width = 125;
                txtRem[n].Height = 21;
                txtRem[n].BackColor = System.Drawing.SystemColors.Info;
                //txtRem[n].Enabled = false;
                txtRem[n].Left = xPos;
                txtRem[n].Top = yPos;
                yPos = yPos + txtRem[n].Height + 4;
                pnlRem.Controls.Add(txtRem[n]);
                pnlRem.Height = pnlRem.Height + 25;
                n++;
            }
        }

        private void LoadRDocTp(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls_RecDesk("txtRDocTp", _num);
            int n = 1;
            pnlRDocTp.Height = 25;
            while (n < _num + 1)
            {
                txtRDocTp[n].Tag = n;
                txtRDocTp[n].Width = 75;
                txtRDocTp[n].Height = 21;
                txtRDocTp[n].BackColor = System.Drawing.SystemColors.Info;
                txtRDocTp[n].TextAlign = HorizontalAlignment.Right;
                txtRDocTp[n].Left = xPos;
                txtRDocTp[n].Top = yPos;
                yPos = yPos + txtRDocTp[n].Height + 4;
                pnlRDocTp.Controls.Add(txtRDocTp[n]);
                pnlRDocTp.Height = pnlRDocTp.Height + 25;
                n++;
            }
            pnlRec.Height = pnlRec.Height + 250;
            pnlSettle.Visible = true;
            pnlSettle.Top = yPos + 10;
        }

        private void LoadDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtDate", _num);
            int n = 1;
            pnlDate.Height = 25;
            while (n < _num + 1)
            {
                dtDate[n].Tag = n;
                dtDate[n].Width = 104;
                dtDate[n].Height = 21;
                dtDate[n].Left = xPos;
                dtDate[n].Top = yPos;
                yPos = yPos + dtDate[n].Height + 4;
                dtDate[n].Format = DateTimePickerFormat.Custom;
                dtDate[n].CustomFormat = "dd/MMM/yyyy";
                // dtDate[n].Enabled = false;
                pnlDate.Controls.Add(dtDate[n]);
                pnlDate.Height = pnlDate.Height + 25;
                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
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
                txtDesc[n].Width = 235;
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

        private void LoadRef(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtRef", _num);
            int n = 1;
            pnlRef.Height = 25;
            while (n < _num + 1)
            {
                txtRef[n].Tag = n;
                txtRef[n].Width = 113;
                txtRef[n].Height = 21;
                txtRef[n].BackColor = System.Drawing.SystemColors.Info;
                txtRef[n].Enabled = false;
                txtRef[n].Left = xPos;
                txtRef[n].Top = yPos;
                yPos = yPos + txtRef[n].Height + 4;
                pnlRef.Controls.Add(txtRef[n]);
                pnlRef.Height = pnlRef.Height + 25;
                n++;
            }
        }

        //private void LoadIsShortSet(int _num)
        //{
        //    int xPos = 2;
        //    int yPos = 2;
        //    AddControls("txtIsShortSet", _num);
        //    int n = 1;
        //    pnlIsShortSet.Height = 25;
        //    while (n < _num + 1)
        //    {
        //        txtIsShortSet[n].Tag = n;
        //        txtIsShortSet[n].Width = 113;
        //        txtIsShortSet[n].Height = 21;
        //        txtIsShortSet[n].BackColor = System.Drawing.SystemColors.Info;
        //        txtIsShortSet[n].Enabled = false;
        //        txtIsShortSet[n].Left = xPos;
        //        txtIsShortSet[n].Top = yPos;
        //        yPos = yPos + txtIsShortSet[n].Height + 4;
        //        pnlIsShortSet.Controls.Add(txtIsShortSet[n]);
        //        pnlIsShortSet.Height = pnlIsShortSet.Height + 25;
        //        n++;
        //    }
        //}

        private void LoadBank(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtBank", _num);
            int n = 1;
            pnlBank.Height = 25;
            while (n < _num + 1)
            {
                txtBank[n].Tag = n;
                txtBank[n].Width = 62;
                txtBank[n].Height = 21;
                txtBank[n].BackColor = System.Drawing.SystemColors.Info;
                txtBank[n].Enabled = false;
                txtBank[n].Left = xPos;
                txtBank[n].Top = yPos;
                yPos = yPos + txtBank[n].Height + 4;
                pnlBank.Controls.Add(txtBank[n]);
                pnlBank.Height = pnlBank.Height + 25;
                n++;
            }
        }

        private void LoadAmount(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtAmount", _num);
            int n = 1;
            pnlAmount.Height = 25;
            while (n < _num + 1)
            {
                txtAmount[n].Tag = n;
                txtAmount[n].Width = 75;
                txtAmount[n].Height = 21;
                txtAmount[n].BackColor = System.Drawing.SystemColors.Info;
                txtAmount[n].TextAlign = HorizontalAlignment.Right;
                txtAmount[n].Enabled = false;
                txtAmount[n].Left = xPos;
                txtAmount[n].Top = yPos;
                yPos = yPos + txtAmount[n].Height + 4;
                pnlAmount.Controls.Add(txtAmount[n]);
                pnlAmount.Height = pnlAmount.Height + 25;
                n++;
            }
        }

        private void LoadScan(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkScan", _num);
            int n = 1;
            pnlScan.Height = 25;
            while (n < _num + 1)
            {
                chkScan[n].Tag = n;
                chkScan[n].Width = 15;
                chkScan[n].Height = 14;
                chkScan[n].Left = xPos;
                chkScan[n].Top = yPos;
                yPos = yPos + chkScan[n].Height + 10;
                pnlScan.Controls.Add(chkScan[n]);
                pnlScan.Height = pnlScan.Height + 25;
                n++;
            }
        }

        private void LoadRelz(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("chkRelz", _num);
            int n = 1;
            pnlRealise.Height = 25;
            while (n < _num + 1)
            {
                chkRelz[n].Tag = n;
                chkRelz[n].Width = 15;
                chkRelz[n].Height = 14;
                chkRelz[n].Left = xPos;
                chkRelz[n].Top = yPos;
                yPos = yPos + chkRelz[n].Height + 10;
                pnlRealise.Controls.Add(chkRelz[n]);
                pnlRealise.Height = pnlRealise.Height + 25;
                // if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                chkRelz[n].Enabled = false;
                //chkRelz[n].CheckedChanged += new System.EventHandler(chkRelz_CheckedChanged);
                n++;
            }
        }

        private void LoadRelzDate(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("dtRelzDate", _num);
            int n = 1;
            pnlRealiseDate.Height = 25;
            while (n < _num + 1)
            {
                dtRelzDate[n].Tag = n;
                dtRelzDate[n].Width = 104;
                dtRelzDate[n].Height = 21;
                dtRelzDate[n].Left = xPos;
                dtRelzDate[n].Top = yPos;
                yPos = yPos + dtDate[n].Height + 3;
                dtRelzDate[n].Format = DateTimePickerFormat.Custom;
                dtRelzDate[n].CustomFormat = "dd/MMM/yyyy";
                pnlRealiseDate.Controls.Add(dtRelzDate[n]);
                pnlRealiseDate.Height = pnlRealiseDate.Height + 25;
                // if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                dtRelzDate[n].Enabled = false;

                // the Event of click Button
                //txtLine[n].Click += new System.EventHandler(ClickTextBox);
                n++;
            }
        }

        private void LoadDocTp(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtDocTp", _num);
            int n = 1;
            pnlDocTp.Height = 25;
            while (n < _num + 1)
            {
                txtDocTp[n].Tag = n;
                txtDocTp[n].Width = 75;
                txtDocTp[n].Height = 21;
                txtDocTp[n].BackColor = System.Drawing.SystemColors.Info;
                txtDocTp[n].TextAlign = HorizontalAlignment.Right;
                txtDocTp[n].Left = xPos;
                txtDocTp[n].Top = yPos;
                yPos = yPos + txtDocTp[n].Height + 4;
                pnlDocTp.Controls.Add(txtDocTp[n]);
                pnlDocTp.Height = pnlDocTp.Height + 25;
                n++;
            }
        }

        private void LoadDocRemarks(int _num)
        {
            int xPos = 2;
            int yPos = 2;
            AddControls("txtDocRemarks", _num);
            int n = 1;
            pnlDocRemarks.Height = 25;
            while (n < _num + 1)
            {
                txtDocRemarks[n].Tag = n;
                txtDocRemarks[n].Width = 125;
                txtDocRemarks[n].Height = 21;
                txtDocRemarks[n].BackColor = System.Drawing.SystemColors.Info;
                txtDocRemarks[n].TextAlign = HorizontalAlignment.Left;
                txtDocRemarks[n].Left = xPos;
                txtDocRemarks[n].Top = yPos;
                yPos = yPos + txtDocRemarks[n].Height + 4;
                pnlDocRemarks.Controls.Add(txtDocRemarks[n]);
                pnlDocRemarks.Height = pnlDocRemarks.Height + 25;
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
                case "dtDate":
                    {
                        dtDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtDate[i] = new System.Windows.Forms.DateTimePicker();
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
                case "txtRef":
                    {
                        txtRef = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRef[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtBank":
                    {
                        txtBank = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtBank[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtAmount":
                    {
                        txtAmount = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtAmount[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "chkScan":
                    {
                        chkScan = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkScan[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "chkRelz":
                    {
                        chkRelz = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkRelz[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "dtRelzDate":
                    {
                        dtRelzDate = new System.Windows.Forms.DateTimePicker[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            dtRelzDate[i] = new System.Windows.Forms.DateTimePicker();
                        }
                        break;
                    }
                case "txtDocTp":
                    {
                        txtDocTp = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtDocTp[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtDocRemarks":
                    {
                        txtDocRemarks = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtDocRemarks[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                //case "txtIsShortSet":
                //    {
                //        txtIsShortSet = new System.Windows.Forms.TextBox[cNumber + 1];
                //        for (int i = 1; i < cNumber + 1; i++)
                //        {
                //            txtIsShortSet[i] = new System.Windows.Forms.TextBox();
                //        }
                //        break;
                //    }


            }
        }

        private void AddControls_RecDesk(string anyControl, int cNumber)
        {
            switch (anyControl)
            {
                case "txtRLine":
                    {
                        txtRLine = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRLine[i] = new System.Windows.Forms.TextBox();
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
                case "txtRDesc":
                    {
                        txtRDesc = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRDesc[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRRef":
                    {
                        txtRRef = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRRef[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRBank":
                    {
                        txtRBank = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRBank[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRAmount":
                    {
                        txtRAmount = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRAmount[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "chkPhyRec":
                    {
                        chkPhyRec = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkPhyRec[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "txtPhyRecVal":
                    {
                        txtPhyRecVal = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtPhyRecVal[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtPhyRDate":
                    {
                        txtPhyRDate = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtPhyRDate[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "cmbBank":
                    {
                        cmbBank = new System.Windows.Forms.ComboBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            cmbBank[i] = new System.Windows.Forms.ComboBox();
                        }
                        break;
                    }
                case "txtBankCode":
                    {
                        txtBankCode = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtBankCode[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtBranch":
                    {
                        txtBranch = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtBranch[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRem":
                    {
                        txtRem = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRem[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtRDocTp":
                    {
                        txtRDocTp = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtRDocTp[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
            }
        }

        private void AddControls_Sun(string anyControl, int cNumber)
        {
            switch (anyControl)
            {
                case "txtSLine":
                    {
                        txtSLine = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSLine[i] = new System.Windows.Forms.TextBox();
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
                case "txtSDesc":
                    {
                        txtSDesc = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSDesc[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtSRef":
                    {
                        txtSRef = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSRef[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtSBank":
                    {
                        txtSBank = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSBank[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtSAmount":
                    {
                        txtSAmount = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSAmount[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "chkSun":
                    {
                        chkSun = new System.Windows.Forms.CheckBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            chkSun[i] = new System.Windows.Forms.CheckBox();
                        }
                        break;
                    }
                case "txtPhyRecValSun":
                    {
                        txtPhyRecValSun = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtPhyRecValSun[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "txtSDocTp":
                    {
                        txtSDocTp = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            txtSDocTp[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }
                case "cmbSBank":
                    {
                        cmbSBank = new System.Windows.Forms.TextBox[cNumber + 1];
                        for (int i = 1; i < cNumber + 1; i++)
                        {
                            cmbSBank[i] = new System.Windows.Forms.TextBox();
                        }
                        break;
                    }

            }
        }

        public void chkRelz_CheckedChanged(Object sender, System.EventArgs e)
        {
            Int32 _arrIndex = Convert.ToInt32(((System.Windows.Forms.CheckBox)sender).Tag);
            if (chkRelz[_arrIndex].Checked)
                dtRelzDate[_arrIndex].Visible = true;
            else
                dtRelzDate[_arrIndex].Visible = false;
        }

        public void chkPhyRec_CheckedChanged(Object sender, System.EventArgs e)
        {
            try
            {
                Decimal phyVal = 0;
                Int32 _arrIndex = Convert.ToInt32(((System.Windows.Forms.CheckBox)sender).Tag);

                //5/6/2015
                //if (txtPhyRDate[_arrIndex].Visible == false)
                //{
                //    if (chkPhyRec[_arrIndex].Checked == true)
                //    {
                //        if (MessageBox.Show("Not Realize Transaction. Are You Sure ?", "Scan/Physical Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                //        {
                //            chkPhyRec[_arrIndex].Checked = false;
                //            return;
                //        }
                //    }
                    
                //}

                if (!string.IsNullOrEmpty((txtPhyRecVal[_arrIndex].Text)))
                {
                    phyVal = Convert.ToDecimal(txtPhyRecVal[_arrIndex].Text);
                }//GRDD_DOC_VAL
                if (phyVal <= 0)
                {
                    return;
                }

                //string docTp = grvDocDetails.Rows[i].Cells[1].Text;
                string docTp = txtRDocTp[_arrIndex].Text;

                //------------------------------------
                // Dictionary<string, Decimal> bindSumList = (Dictionary<string, Decimal>)grvSellectTool.DataSource;

                int count = BindSumList.Count(D => D.Key.StartsWith(docTp));
                if (count == 0)
                {
                    BindSumList.Add(docTp, phyVal);
                    lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) + phyVal).ToString());
                }
                else if (count > 0)
                {
                    Decimal val = BindSumList[docTp];
                    if (chkPhyRec[_arrIndex].Checked)
                    {
                        val = val + phyVal;
                        lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) + phyVal).ToString());
                    }
                    else
                    {
                        val = val - phyVal;
                        lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) - phyVal).ToString());
                    }


                    //bindSumList[docTp] = val;
                    BindSumList[docTp] = val;

                }
                //grvSellectTool.DataSource = BindSumList;
                BindingSource _x = new BindingSource();
                var _lst = BindSumList.ToList();
                _x.DataSource = _lst;
                grvSellectTool.DataSource = _x;
                grvSellectTool.Columns[0].Width = 170;
                grvSellectTool.Columns[0].HeaderText = "DOC TYPE";
                grvSellectTool.Columns[1].Width = 100;
                grvSellectTool.Columns[1].HeaderText = "VALUE";

                _isScreenChanged = true;
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

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                //------------------------------------------------------------------------------------------
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "ACCEXE") == false)
                //{
                //    //Only A/C Executive   can process.
                //    MessageBox.Show("No permission for 'Process' task!\n(Advice: Reqired permission code :ACCEXE)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                if (Convert.ToString(ddlDocTypes.SelectedValue) == null)
                {
                    MessageBox.Show("Please select the document type", "Document Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Add by Chamal 21-Sep-2013
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10058))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Sorry, You have not permission for Accounts Executive tasks!\n( Advice: Required permission code :10058)", "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                DateTime FromDate;
                DateTime ToDate;
                try
                {
                    //lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                    FromDate = Convert.ToDateTime(lblFrmdtWk.Text);
                    //lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");
                    ToDate = Convert.ToDateTime(lblTodtWk.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please select From and To dates!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //3/7/2014
                Boolean _SR = CHNLSVC.General.IsDayEndDone4ScanDocs(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(ToDate).Date);
                if (_SR == false)
                {
                    MessageBox.Show("Dayend is not done from " + Convert.ToDateTime(ToDate).Date, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ddlWeek.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select week!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (lblFrmdtWkSelect.Text == "")
                {
                    MessageBox.Show("Week definition not set for the selected week!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDateTime(lblFrmdtWkSelect.Text) > FromDate)
                {
                    MessageBox.Show("Please select a 'From' date within the week", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDateTime(lblTodtWkSelect.Text) < ToDate)
                {
                    MessageBox.Show("Please select a 'To' date within the week", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //------------------------------------------------------------------------------------------
                DataTable dt_CHECK_FOR_available = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null, 0);

                foreach (DataRow drr in dt_CHECK_FOR_available.Rows)
                {
                    // string itmcd = drr["mi_cd"].ToString();
                    string sunUplaod_by = drr["grdd_sun_up_by"].ToString();
                    string reciveBy = drr["grdd_rcv_by"].ToString();
                    Int32 _is_extra_doc = Convert.ToInt32(drr["grdd_is_extra"].ToString());
                    if (sunUplaod_by != "")
                    {
                        MessageBox.Show("Cannot process. This week is updated by Sun Upload tasks!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (reciveBy != "")  //|| _is_extra_doc == 1
                    {
                        MessageBox.Show("Cannot process. This week is updated by Receive Desk tasks!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                }

                //---------------------------------------------------------------------------------------------

                if (string.IsNullOrEmpty(txtPC.Text) || string.IsNullOrEmpty(cmbMonth.Text) || string.IsNullOrEmpty(cmbYear.Text))
                {
                    MessageBox.Show("Cannot Process. Missing Parameters.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show("Do you want to Delete Previously Processed Data ?", "Scan Docs", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                BindSumList.Clear();
                //grvSellectTool.DataSource = BindSumList;

                DateTime DTmonth = Convert.ToDateTime(lblMonth.Text).Date;
                Int32 mon = DTmonth.Month;
                Int32 yr = DTmonth.Year;
                Int32 Week = Convert.ToInt32(ddlWeek.SelectedIndex + 1);

                // Int32 del_eff = CHNLSVC.Financial.Delete_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), DTmonth, Week);

                Int32 del_eff = CHNLSVC.Financial.Delete_GNT_RCV_DSK_DOC_new(BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), FromDate, ToDate);

                //Int32 eff = CHNLSVC.Financial.Save_GNT_RCV_DSK_DOC(BaseCls.GlbUserID, DateTime.Now.Date, BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), DTmonth.Month, DTmonth.Year, Week, DTmonth);
                Int32 eff = CHNLSVC.Financial.new_Save_GNT_RCV_DSK_DOC(BaseCls.GlbUserID, DateTime.Now.Date, BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), Week, lblFrmdtWk.Value.Date, lblTodtWk.Value.Date, Convert.ToString(ddlDocTypes.SelectedValue), Convert.ToDateTime(lblMonth.Text).Date);

                string _msg = string.Empty;
                int _effect = CHNLSVC.Financial.UpdateSalesSummary(BaseCls.GlbUserComCode, txtPC.Text.Trim(), lblFrmdtWk.Value.Date, lblTodtWk.Value.Date, BaseCls.GlbUserID, out _msg);
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Error generated while processing. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //**   DataTable dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null);
                DataTable dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_onDateRange(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblFrmdtWk.Value).Date, Convert.ToDateTime(lblTodtWk.Value).Date, null, 0);


                //DataTable _tmpdt = CHNLSVC.Financial.getDocSettle(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblFrmdtWk.Value).Date, Convert.ToDateTime(lblTodtWk.Value).Date, null);
                //dt.Merge(_tmpdt);


                //  Get_GNT_RCV_DSK_DOC_onDateRange(string com, string pc, DateTime fromDt, DateTime toDt, string doc_tp)
                //save short bankings
                if (!string.IsNullOrEmpty(lblFrmdtWk.Text))
                {
                    DateTime START_DT = Convert.ToDateTime(lblFrmdtWk.Text);
                    DateTime END_DT = Convert.ToDateTime(lblTodtWk.Text);
                    Int32 COUNT_DAYS = (END_DT.Date - START_DT.Date).Days;
                    for (int i = 0; i < COUNT_DAYS; i++)
                    {

                        Dictionary<string, Decimal> dayOne = CHNLSVC.Financial.Get_RemDet(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblFrmdtWk.Text).AddDays(i));
                        Short_Banking _shortBank = new Short_Banking();
                        _shortBank.SB_DT = Convert.ToDateTime(lblFrmdtWk.Text).AddDays(i);
                        _shortBank.SB_PRV_EXS_REM = dayOne["PrvExcess"];
                        _shortBank.SB_EXS_REM = dayOne["ExcessRem"];
                        _shortBank.SB_CIH = dayOne["CashInHand"];
                        _shortBank.SB_REMITED = dayOne["AmtRemited"];
                        _shortBank.SB_DIFF = dayOne["Defference"];
                        _shortBank.SB_COM = BaseCls.GlbUserComCode;
                        _shortBank.SB_PC = txtPC.Text;
                        _shortBank.SB_MONTH = Convert.ToDateTime(lblMonth.Text);
                        _shortBank.SB_WEEK = Convert.ToInt32(ddlWeek.SelectedIndex + 1);

                        if (dayOne["Defference"] > 0)
                        {
                            int Z = CHNLSVC.Financial.SaveShortBanking(_shortBank);
                        }
                    }

                }

                if (_TabIndex == 0)
                {
                    RemoveControls();
                    pnlExecutive.Height = 31;
                    pnlExecutive.Top = 51;
                    _recordCNT = dt.Rows.Count;


                    if (_recordCNT > 0)
                    {
                        LoadLine(_recordCNT);
                        LoadDate(_recordCNT);
                        LoadDesc(_recordCNT);
                        LoadRef(_recordCNT);
                        LoadBank(_recordCNT);
                        LoadAmount(_recordCNT);
                        LoadScan(_recordCNT);
                        LoadRelz(_recordCNT);
                        LoadRelzDate(_recordCNT);
                        LoadDocTp(_recordCNT);
                        LoadDocRemarks(_recordCNT);
                        // LoadIsShortSet(_recordCNT);
                    }

                    for (int i = 0; i < _recordCNT; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["grdd_is_short_set"]) == 0)     //not consider short settlements
                        {
                            txtLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                            dtDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                            txtDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                            txtRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref"]).ToString();
                            txtBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                            txtAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();
                            chkScan[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GRDD_SCAN_RCV"]);
                            chkRelz[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GRDD_IS_REALIZED"]);
                            if (dtRelzDate[i + 1].Text != ("31/Dec/2999").ToString())
                                dtRelzDate[i + 1].Text = (dt.Rows[i]["GRDD_REALIZED_DT"]).ToString();
                            if (dtRelzDate[i + 1].Text != ("01/Jan/0001").ToString())
                                dtRelzDate[i + 1].Text = (dt.Rows[i]["GRDD_REALIZED_DT"]).ToString();
                            txtDocTp[i + 1].Text = (dt.Rows[i]["grdd_doc_tp"]).ToString();
                            //  txtIsShortSet[i+1].Text=(dt.Rows[i]["grdd_is_short_set"]).ToString();
                            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["grdd_ttl"])))
                                if (Convert.ToDecimal(dt.Rows[i]["grdd_ttl"]) > 0)
                                    txtDesc[i + 1].BackColor = Color.LightSkyBlue;
                        }


                    }
                    vScrollBar1.Maximum = pnlExecutive.Height - 6 * _recordCNT;

                    //------------------- //add by shani--------------------------------------
                    this.Cursor = Cursors.Default;
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully Processed.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No data to process!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;

                    //--------------------------------------------------------------------------
                }
                else if (_TabIndex == 1)
                {
                    //------------------- //add by shani--------------------------------------
                    this.Cursor = Cursors.Default;
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully Processed.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No data to process!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;

                    //--------------------------------------------------------------------------

                    RemoveControls_RecDesk();
                    pnlRec.Height = 31;
                    pnlRec.Top = 51;

                    _recordCNT_Rec = dt.Rows.Count;

                    if (_recordCNT_Rec > 0)
                    {
                        LoadRLine(_recordCNT_Rec);
                        LoadRDate(_recordCNT_Rec);
                        LoadRDesc(_recordCNT_Rec);
                        LoadRRef(_recordCNT_Rec);
                        LoadRBank(_recordCNT_Rec);
                        LoadRAmount(_recordCNT_Rec);
                        LoadPhyRec(_recordCNT_Rec);
                        LoadPhyRecVal(_recordCNT_Rec);
                        LoadPhyRDate(_recordCNT_Rec);
                        LoadBankR(_recordCNT_Rec);
                        LoadBankCode(_recordCNT_Rec);
                        LoadBranch(_recordCNT_Rec);
                        LoadRem(_recordCNT_Rec);
                        LoadRDocTp(_recordCNT_Rec);
                    }

                    for (int i = 0; i < _recordCNT_Rec; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["grdd_is_short_set"]) == 0)     //not consider short settlements
                        {
                            txtRLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                            dtRDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                            txtRDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                            txtRRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref"]).ToString();
                            //if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                            //{
                            if (txtRDesc[i + 1].Text == "CHEQUE" || txtRDesc[i + 1].Text == "BANK DEPOSIT SLIP")
                                txtRRef[i + 1].Enabled = false;
                            else
                                txtRRef[i + 1].Enabled = true;
                            //}
                            txtRBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                            txtRAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();
                            chkPhyRec[i + 1].Checked = Convert.ToBoolean((dt.Rows[i]["GRDD_DOC_RCV"]));
                            txtPhyRecVal[i + 1].Text = (dt.Rows[i]["GRDD_DOC_VAL"]).ToString();
                            //if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                            //{
                            if (txtRDesc[i + 1].Text == "CHEQUE" || txtRDesc[i + 1].Text == "BANK DEPOSIT SLIP")
                                txtPhyRecVal[i + 1].Enabled = false;
                            else
                                txtPhyRecVal[i + 1].Enabled = true;
                            //}
                            cmbBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                            //txtBankCode[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK_CD"]).ToString();
                            txtBankCode[i + 1].Text = (dt.Rows[i]["grdd_deposit_bank"]).ToString();
                            txtBranch[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK_BRANCH"]).ToString();
                            txtRem[i + 1].Text = (dt.Rows[i]["GRDD_RMK"]).ToString();
                            txtRDocTp[i + 1].Text = (dt.Rows[i]["grdd_doc_tp"]).ToString();
                        }
                    }



                    vScrollBar2.Maximum = pnlRec.Height - 6 * _recordCNT_Rec;
                }
                else if (_TabIndex == 2)
                {
                    //------------------- //add by shani--------------------------------------
                    this.Cursor = Cursors.Default;
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully Processed.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No data to process!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;

                    //--------------------------------------------------------------------------

                    RemoveControls_Sun();
                    pnlSunUser.Height = 31;
                    pnlSunUser.Top = 51;
                    _recordCNT_Sun = dt.Rows.Count;



                    if (_recordCNT_Sun > 0)
                    {
                        LoadSLine(_recordCNT_Sun);
                        LoadSDate(_recordCNT_Sun);
                        LoadSDesc(_recordCNT_Sun);
                        LoadSRef(_recordCNT_Sun);
                        LoadSBank(_recordCNT_Sun);
                        LoadSAmount(_recordCNT_Sun);
                        LoadPhyRecValSun(_recordCNT_Sun);
                        LoadSunUpload(_recordCNT_Sun);
                        LoadSDocTp(_recordCNT_Sun);
                        LoadBankS(_recordCNT_Sun);
                    }

                    for (int i = 0; i < _recordCNT_Sun; i++)
                    {
                        txtSLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                        dtSDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                        txtSDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                        txtSRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref"]).ToString();
                        txtSBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                        txtSAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();
                        chkSun[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GRDD_SUN_UPLOAD"]);
                        txtPhyRecValSun[i + 1].Text = (dt.Rows[i]["GRDD_DOC_VAL"]).ToString();
                        txtSDocTp[i + 1].Text = (dt.Rows[i]["grdd_doc_tp"]).ToString();
                        cmbSBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                    }

                    vScrollBar3.Maximum = pnlSunUser.Height - 6 * _recordCNT_Sun;
                }
                this.Cursor = Cursors.Default;
                if (eff > 0)
                {
                    MessageBox.Show("Successfully Processed.", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No data to process!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void BackDatePermission()
        {
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtRemindDate, lblBackDateInfor, string.Empty);
        }

        public Scan_Physical_Docs()
        {
            try
            {
                InitializeComponent();
                bind_Combo_ddlDocTypes();//add by shani
                InitializeValuesNDefaultValueSet();

                if (tabControl1.SelectedIndex == 0)
                {
                    //Edit by Chamal 21-Sep-2013
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "ACCEXE") == true)
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10058))
                    {
                        lblFrmdtWk.Enabled = true;
                        lblTodtWk.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }


        private void InitializeValuesNDefaultValueSet()
        {
            _recordCNT = 0;
            _recordCNT_Rec = 0;
            _recordCNT_Sun = 0;

            txtPC.Text = BaseCls.GlbUserDefProf;

            grvRem.AutoGenerateColumns = false;
            //grvSellectTool.AutoGenerateColumns = false;
            // grvSellectTool.ColumnHeadersVisible = false;


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

            ddlDocTypes.SelectedIndex = 0;
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
            try
            {
                txtPC.Text = "";
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

                //cmbYear.SelectedIndex = -1;
                //cmbMonth.SelectedIndex = -1;
                //ddlWeek.SelectedIndex = -1;
                //lblFrmdtWk.Text = "";
                //lblTodtWk.Text = "";
                if (_TabIndex == 0)    //executive
                {
                    RemoveControls();
                    pnlExecutive.Height = 31;
                    pnlExecutive.Top = 51;
                }
                if (_TabIndex == 1)    //receiving desk
                {
                    if (_isScreenChanged == true)
                        if (MessageBox.Show("You have do some changes to the screen. Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                    RemoveControls_RecDesk();
                    pnlRec.Height = 31;
                    pnlRec.Top = 51;
                    
                }
                if (_TabIndex == 2)    //sun upload user
                {
                    RemoveControls_Sun();
                    pnlSunUser.Height = 31;
                    pnlSunUser.Top = 51;
                }

                grvRem.DataSource = null;
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

        private void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime _from;
                DateTime _to;
                if (cmbYear.Text == "")
                {
                    MessageBox.Show("Select Year !", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(ddlWeek.Text))
                {
                    DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(ddlWeek.SelectedIndex + 1), out _from, out _to, BaseCls.GlbUserComCode, "");
                    if (_weekDef == null)
                    {
                        MessageBox.Show("Week Definition not set!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_from != Convert.ToDateTime("31/Dec/9999"))
                    {
                        lblFrmdtWk.Text = _from.Date.ToString("dd/MMM/yyyy");
                        lblTodtWk.Text = _to.Date.ToString("dd/MMM/yyyy");

                        //lblFrmdtWkSelect
                        //lblTodtWkSelect
                        lblFrmdtWkSelect.Text = _from.Date.ToString("dd/MMM/yyyy");
                        lblTodtWkSelect.Text = _to.Date.ToString("dd/MMM/yyyy");
                    }
                    else
                    {
                        lblFrmdtWk.Text = string.Empty;
                        lblTodtWk.Text = string.Empty;

                        lblFrmdtWkSelect.Text = string.Empty;
                        lblTodtWkSelect.Text = string.Empty;
                    }
                }

                List<RemDet> remList = new List<RemDet>();

                if (!string.IsNullOrEmpty(lblFrmdtWk.Text))
                {
                    DateTime START_DT = Convert.ToDateTime(lblFrmdtWk.Text);
                    DateTime END_DT = Convert.ToDateTime(lblTodtWk.Text);
                    Int32 COUNT_DAYS = (END_DT.Date - START_DT.Date).Days + 1;
                    for (int i = 0; i < COUNT_DAYS; i++)
                    {

                        Dictionary<string, Decimal> dayOne = CHNLSVC.Financial.Get_RemDet(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblFrmdtWk.Text).AddDays(i));
                        RemDet remdt = new RemDet();
                        remdt.Date = Convert.ToDateTime(lblFrmdtWk.Text).AddDays(i).ToShortDateString();
                        remdt.PrevExcessRem = dayOne["PrvExcess"];
                        remdt.ExcessRem = dayOne["ExcessRem"];
                        remdt.CashIH = dayOne["CashInHand"];
                        remdt.AmtRemited = dayOne["AmtRemited"];
                        remdt.Difference = dayOne["Defference"];
                        remList.Add(remdt);
                    }
                    grvRem.DataSource = remList;
                }

                this.Cursor = Cursors.Default;
                //TODO: fill the grvRem
                if (ddlWeek.SelectedIndex == -1)
                {
                    btnExtraDocs.Enabled = false;
                }
                else
                {
                    btnExtraDocs.Enabled = true;
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

        private void calc()
        {
            //CIH (06,001)

            //Tot remitance (99,001)

            //rem to b bank (99,003)

            //diference (99,002)

            //credit note



        }

        public void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                //------------------------------------------------------------------------------------------------------------------
                if (tabControl1.SelectedIndex == 0)
                {
                    //Add by Chamal 21-Sep-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10058))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Sorry, You have not permission for Accounts Executive tasks!\n( Advice: Required permission code :10058)", "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    //Add by Chamal 21-Sep-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10059))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Sorry, You have not permission for Receiving Desk tasks!\n( Advice: Required permission code :10059)", "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    //Add by Chamal 21-Sep-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10060))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Sorry, You have not permission for SUN System Upload tasks!\n( Advice: Required permission code :10060)", "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (ddlWeek.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select week first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (lblFrmdtWkSelect.Text == "" || lblFrmdtWkSelect.Text == string.Empty)
                {
                    MessageBox.Show("Please select week first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //-------------------------------------------------------------------------------------------------------------------
                string SELECT_DOC_TP = ddlDocTypes.SelectedValue.ToString();
                if (SELECT_DOC_TP == "")
                {
                    MessageBox.Show("Select Document Type", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                BindSumList.Clear();
                grvSellectTool.DataSource = BindSumList;

                DataTable dt = new DataTable();
                if (SELECT_DOC_TP == "ALL")
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_onDateRange(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblFrmdtWk.Value).Date, Convert.ToDateTime(lblTodtWk.Value).Date, null, 0);
                    }
                    else if (tabControl1.SelectedIndex == 1)
                    {
                        dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null, 0);
                    }
                    else
                    {
                        dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null, 1);
                    }
                }
                else
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC_onDateRange(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblFrmdtWk.Value).Date, Convert.ToDateTime(lblTodtWk.Value).Date, SELECT_DOC_TP, 0);
                    }
                    else if (tabControl1.SelectedIndex == 1)
                    {
                        dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), SELECT_DOC_TP, 0);
                    }
                    else
                    {
                        dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), SELECT_DOC_TP, 1);
                    }
                    //dt = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), SELECT_DOC_TP);
                }
                if (dt == null)
                {
                    MessageBox.Show("No records found!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }

                Decimal _totSettle = 0;
                int S = CHNLSVC.Financial.GetTotalSettlements(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), out _totSettle);
                lblTotSettlement.Text = FormatToCurrency(_totSettle.ToString());

                Decimal _remTot = 0;
                Decimal _cashRef = 0;
                Decimal _remTotFin = 0;
                int Z = CHNLSVC.Financial.GetRemSumDet(Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, "99", "003", txtPC.Text, BaseCls.GlbUserComCode, out  _remTot, out _remTotFin);

                
                decimal _valX = 0;
                decimal _valFinX = 0;
                int P = CHNLSVC.Financial.GetRemSumDet(Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date.AddDays(-1), "06", "001", txtPC.Text, BaseCls.GlbUserComCode, out  _valX, out _valFinX);

                lbl_banked.Text = FormatToCurrency((_remTot -_valX).ToString());

                int Y = CHNLSVC.Financial.GetRemSumDet(Convert.ToDateTime(lblTodtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, "06", "001", txtPC.Text, BaseCls.GlbUserComCode, out  _remTot, out _remTotFin);
                lbl_CIH.Text = FormatToCurrency(_remTot.ToString());

                int X = CHNLSVC.Financial.GetRemSumDet(Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, "99", "001", txtPC.Text, BaseCls.GlbUserComCode, out  _remTot, out _remTotFin);
                lbl_TotRem.Text = FormatToCurrency(_remTot.ToString());

                int A = CHNLSVC.Financial.GetRemSumDet(Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, "99", "002", txtPC.Text, BaseCls.GlbUserComCode, out  _remTot, out _remTotFin);
                lbl_Diff.Text = FormatToCurrency(_remTot.ToString());

                int B = CHNLSVC.Financial.GetCreditNoteTot(Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, txtPC.Text, BaseCls.GlbUserComCode, out  _remTot);
                int C = CHNLSVC.Financial.GetCashRefundTot(Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToDateTime(lblTodtWk.Text).Date, txtPC.Text, BaseCls.GlbUserComCode, out  _cashRef);
                lbl_CN.Text = FormatToCurrency((_remTot+_cashRef).ToString());

                if (_TabIndex == 0)     //acc executive
                {
                    RemoveControls();
                    pnlExecutive.Height = 31;
                    pnlExecutive.Top = 51;
                    _recordCNT = dt.Rows.Count;


                    if (_recordCNT > 0)
                    {
                        LoadLine(_recordCNT);
                        LoadDate(_recordCNT);
                        LoadDesc(_recordCNT);
                        LoadRef(_recordCNT);
                        LoadBank(_recordCNT);
                        LoadAmount(_recordCNT);
                        LoadScan(_recordCNT);
                        LoadRelz(_recordCNT);
                        LoadRelzDate(_recordCNT);
                        LoadDocTp(_recordCNT);
                        LoadDocRemarks(_recordCNT);
                        //  LoadIsShortSet(_recordCNT);
                    }

                    for (int i = 0; i < _recordCNT; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["grdd_is_short_set"]) == 0)
                        {
                            txtLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                            dtDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                            txtDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                            txtRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref"]).ToString();
                            txtBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                            txtAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();
                            chkScan[i + 1].Checked = Convert.ToBoolean((dt.Rows[i]["GRDD_SCAN_RCV"]));
                            chkRelz[i + 1].Checked = Convert.ToBoolean((dt.Rows[i]["GRDD_IS_REALIZED"]));

                            if (Convert.ToInt32(dt.Rows[i]["grdd_is_realized"]) == 0)
                                dtRelzDate[i + 1].Visible = false;
                            else
                                dtRelzDate[i + 1].Text = (dt.Rows[i]["GRDD_REALIZED_DT"]).ToString();

                            txtDocTp[i + 1].Text = (dt.Rows[i]["grdd_doc_tp"]).ToString();
                            txtDocRemarks[i + 1].Text = (dt.Rows[i]["grdd_Remarks"]).ToString();
                            // txtIsShortSet[i + 1].Text = (dt.Rows[i]["grdd_is_short_set"]).ToString();
                            if (dtRelzDate[i + 1].Text == "31/Dec/2999")
                            {
                                dtRelzDate[i + 1].CalendarMonthBackground = Color.Red;
                            }
                        }

                    }
                    vScrollBar1.Maximum = pnlExecutive.Height - 6 * _recordCNT;
                }
                else if (_TabIndex == 1)    //receiving desk
                {
                    DataTable _dtSet = CHNLSVC.Financial.getShortSetle(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1));
                    grvSettle.AutoGenerateColumns = false;
                    grvSettle.DataSource = _dtSet;

                    RemoveControls_RecDesk();
                    pnlRec.Height = 31;
                    pnlRec.Top = 51;
                    _recordCNT_Rec = dt.Rows.Count;

                    if (_recordCNT_Rec > 0)
                    {
                        _noRec = false;
                        LoadRLine(_recordCNT_Rec);
                        LoadRDate(_recordCNT_Rec);
                        LoadRDesc(_recordCNT_Rec);
                        LoadRRef(_recordCNT_Rec);
                        LoadRBank(_recordCNT_Rec);
                        LoadRAmount(_recordCNT_Rec);
                        LoadPhyRec(_recordCNT_Rec);
                        LoadPhyRecVal(_recordCNT_Rec);
                        LoadPhyRDate(_recordCNT_Rec);
                        LoadBankR(_recordCNT_Rec);
                        LoadBankCode(_recordCNT_Rec);
                        LoadBranch(_recordCNT_Rec);
                        LoadRem(_recordCNT_Rec);
                        LoadRDocTp(_recordCNT_Rec);
                        //  LoadIsShortSet(_recordCNT_Rec);

                    }
                    else
                    {
                        _noRec = true;
                        _recordCNT_Rec = 1;
                        LoadRLine(1);
                        LoadRDate(1);
                        LoadRDesc(1);
                        LoadRRef(1);
                        LoadRBank(1);
                        LoadRAmount(1);
                        LoadPhyRec(1);
                        LoadPhyRecVal(1);
                        LoadPhyRDate(1);
                        LoadBankR(1);
                        LoadBankCode(1);
                        LoadBranch(1);
                        LoadRem(1);
                        LoadRDocTp(1);

                    }

                    if (_noRec == false)
                    {
                        for (int i = 0; i < _recordCNT_Rec; i++)
                        {
                            if (Convert.ToInt32(dt.Rows[i]["grdd_is_short_set"]) == 0)
                            {
                                txtRLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                                dtRDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                                txtRDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                                txtRRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref_NEW"]).ToString();
                                //  if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                                //  {
                                if (txtRDesc[i + 1].Text == "CHEQUE" || txtRDesc[i + 1].Text == "BANK DEPOSIT SLIP")
                                    txtRRef[i + 1].Enabled = false;
                                else
                                    txtRRef[i + 1].Enabled = true;
                                // }
                                txtRBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString(); //--------------------
                                txtRAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();
                                chkPhyRec[i + 1].Checked = Convert.ToBoolean((dt.Rows[i]["GRDD_DOC_RCV"]));
                                txtPhyRecVal[i + 1].Text = (dt.Rows[i]["GRDD_DOC_VAL"]).ToString();

                                // if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                                // {
                                if (txtRDesc[i + 1].Text == "CHEQUE" || txtRDesc[i + 1].Text == "BANK DEPOSIT SLIP")
                                    txtPhyRecVal[i + 1].Enabled = false;
                                else
                                    txtPhyRecVal[i + 1].Enabled = true;
                                // }

                                if (Convert.ToInt32(dt.Rows[i]["grdd_is_realized"]) == 0)
                                    txtPhyRDate[i + 1].Visible = false;
                                else
                                    txtPhyRDate[i + 1].Text = (dt.Rows[i]["grdd_realized_dt"]).ToString();

                                cmbBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString(); //*******************
                                txtRem[i + 1].Text = (dt.Rows[i]["grdd_Rmk"]).ToString();  //16/9/2014

                                //cmbBank[i + 1].DataSource = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                                //----------------------------------------------------------
                                DataTable DT = CHNLSVC.Financial.GET_BANKS_of_PC_on_docType(BaseCls.GlbUserComCode, txtPC.Text, (dt.Rows[i]["grdd_doc_tp"]).ToString());
                                if (DT != null)
                                {
                                    //ddlPopUpBank.Items.Clear();
                                    foreach (DataRow drow in DT.Rows)
                                    {
                                        cmbBank[i + 1].Items.Add(drow["grsa_bank_id"].ToString());
                                        if (dt.Rows[i]["GRDD_DOC_BANK"].ToString() == drow["grsa_bank_id"].ToString())
                                        {
                                            cmbBank[i + 1].SelectedValue = drow["grsa_bank_id"].ToString();

                                        }

                                    }
                                }

                                //----------------------------------------------------------                    

                                txtBankCode[i + 1].Text = (dt.Rows[i]["grdd_deposit_bank"]).ToString();
                                txtBranch[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK_BRANCH"]).ToString();
                                txtRem[i + 1].Text = (dt.Rows[i]["GRDD_RMK"]).ToString();
                                txtRDocTp[i + 1].Text = (dt.Rows[i]["grdd_doc_tp"]).ToString();

                                txtPhyRecVal[i + 1].TextChanged += new EventHandler(textBox_TextChanged);
                            }

                        }
                    }
                    vScrollBar2.Maximum = pnlRec.Height - 6 * _recordCNT_Rec;
                }
                else if (_TabIndex == 2)    //sun upload user
                {
                    RemoveControls_Sun();
                    pnlSunUser.Height = 31;
                    pnlSunUser.Top = 51;

                    _recordCNT_Sun = dt.Rows.Count;

                    DataTable _tmpdt = CHNLSVC.Financial.getDocSettle(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(lblMonth.Text).Date, Convert.ToDateTime(lblTodtWk.Value).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), SELECT_DOC_TP);
                    dt.Merge(_tmpdt);
                    _recordCNT_Sun = _recordCNT_Sun + _tmpdt.Rows.Count;

                    if (_recordCNT_Sun > 0)
                    {
                        LoadSLine(_recordCNT_Sun);
                        LoadSDate(_recordCNT_Sun);
                        LoadSDesc(_recordCNT_Sun);
                        LoadSRef(_recordCNT_Sun);
                        LoadSBank(_recordCNT_Sun);
                        LoadSAmount(_recordCNT_Sun);
                        LoadPhyRecValSun(_recordCNT_Sun);
                        LoadSunUpload(_recordCNT_Sun);
                        LoadSDocTp(_recordCNT_Sun);
                        LoadBankS(_recordCNT_Sun);

                    }

                    for (int i = 0; i < _recordCNT_Sun; i++)
                    {
                        txtSLine[i + 1].Text = (dt.Rows[i]["Grdd_seq"]).ToString();
                        dtSDate[i + 1].Text = (dt.Rows[i]["grdd_dt"]).ToString();
                        txtSDesc[i + 1].Text = (dt.Rows[i]["grdd_doc_desc"]).ToString();
                        txtSRef[i + 1].Text = (dt.Rows[i]["grdd_doc_ref"]).ToString();
                        txtSBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();
                        txtSAmount[i + 1].Text = (dt.Rows[i]["GRDD_SYS_VAL"]).ToString();
                        chkSun[i + 1].Checked = Convert.ToBoolean(dt.Rows[i]["GRDD_SUN_UPLOAD"]);
                        txtPhyRecValSun[i + 1].Text = (dt.Rows[i]["GRDD_DOC_VAL"]).ToString();
                        txtSDocTp[i + 1].Text = (dt.Rows[i]["grdd_doc_tp"]).ToString();
                        cmbSBank[i + 1].Text = (dt.Rows[i]["GRDD_DOC_BANK"]).ToString();


                        if (chkSun[i + 1].Checked == true)
                        {
                            chkSun[i + 1].Enabled = false;
                            // chkSun[i + 1].CheckedChanged += new EventHandler(sunUploadCheck_Changed);
                        }
                        txtPhyRecValSun[i + 1].TextChanged += new EventHandler(textBoxPhyRecValSun_TextChanged);
                    }


                    vScrollBar3.Maximum = pnlSunUser.Height - 6 * _recordCNT_Sun;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No records found!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //Dictionary<string, Decimal> bindSumList = new Dictionary<string, decimal>();

                lblSelTot.Text = "0.00";
                foreach (DataRow dr in dt.Rows)
                {
                    string docTp = dr["grdd_doc_tp"].ToString();
                    string isCheked = dr["GRDD_DOC_RCV"].ToString();
                    // Decimal phyVal = Convert.ToDecimal(dr["GRDD_DOC_VAL"]);
                    Decimal phyVal = 0;
                    //TextBox PH_VAL = (TextBox)grvDocDetails.FindControl("txtgrvPhyVal");
                    //Decimal phyVal = Convert.ToDecimal(PH_VAL.Text);//GRDD_DOC_VAL

                    if (isCheked == "1")
                    {
                        phyVal = Convert.ToDecimal(dr["GRDD_DOC_VAL"]);
                        int count = BindSumList.Count(D => D.Key.StartsWith(docTp));
                        if (count == 0)
                        {
                            BindSumList.Add(docTp, phyVal);
                            lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) + phyVal).ToString());
                        }
                        else if (count > 0)
                        {
                            Decimal val = BindSumList[docTp];
                            val = val + phyVal;
                            //bindSumList[docTp] = val;
                            BindSumList[docTp] = val;
                            lblSelTot.Text = FormatToCurrency((Convert.ToDecimal(lblSelTot.Text) + phyVal).ToString());
                        }
                    }
                }
                // grvSellectTool.DataSource = bindSumList;//BindSumList
                BindingSource _x = new BindingSource();
                var _lst = BindSumList.ToList();
                _x.DataSource = _lst;
                grvSellectTool.DataSource = _x;
                grvSellectTool.Columns[0].Width = 170;
                grvSellectTool.Columns[0].HeaderText = "DOC TYPE";
                grvSellectTool.Columns[1].Width = 100;
                grvSellectTool.Columns[1].HeaderText = "VALUE";
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

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYear.Text != "")
            {
                int month = cmbMonth.SelectedIndex + 1;
                if (month > 0)
                {
                    DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
                    lblMonth.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
                }
            }

            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
            grvRem.DataSource = null;
        }

        private void btnExtraDocs_Click(object sender, EventArgs e)
        {
            try
            {
                //Convert.ToDateTime(lblMonth.Text).Date
                //Convert.ToInt32(ddlWeek.SelectedIndex + 1)
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RCDEXE") == false)
                //{
                //    //ACCEXE,RCDEXE,SUNEXE
                //    MessageBox.Show("No permission for Receiving Desk tasks!\n(Advice: Reqired permission code :RCDEXE)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //Add Chamal 21-Sep-2013
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10059))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Sorry, You have not permission for Receiving Desk tasks!\n( Advice: Required permission code :10059)", "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                DataTable sunUp = CHNLSVC.Financial.Get_SunUploaded_gnt_rcv_dsk_doc(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null);
                if (sunUp.Rows.Count > 0)
                {
                    MessageBox.Show("Cannot add. Week is Sun uploaded!", "Sun uploaded", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Finance.Scan_Physical_Docs_Extra _ExtraDocs = new Finance.Scan_Physical_Docs_Extra(this, Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), BaseCls.GlbUserComCode, txtPC.Text.Trim());
                _ExtraDocs.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.SelectedIndex = -1;
            ddlWeek.SelectedIndex = -1;
            lblFrmdtWk.Text = "";
            lblTodtWk.Text = "";
            grvRem.DataSource = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMonth.SelectedIndex == -1 || ddlWeek.SelectedIndex == -1 || cmbYear.SelectedIndex == -1)
                {
                    MessageBox.Show("Enter week details", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //DataTable sunUp = CHNLSVC.Financial.Get_SunUploaded_gnt_rcv_dsk_doc(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null);
                //if (sunUp.Rows.Count > 0)
                //{
                //    MessageBox.Show("Cannot save. Week is Sun uploaded!", "Sun uploaded", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "BLKSCAN"))
                //{
                //        MessageBox.Show("Access Denied !", "Sun uploaded", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //}

                List<ScanPhysicalDocReceiveDet> UpdateDocList = new List<ScanPhysicalDocReceiveDet>();
                if (_TabIndex == 0)
                {
                    IS_SCAN = 1;
                    if (_recordCNT == 0)
                    {
                        MessageBox.Show("Cannot Save. Details not found !", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    for (int i = 1; i < _recordCNT + 1; i++)
                    {
                        ScanPhysicalDocReceiveDet dicObj = new ScanPhysicalDocReceiveDet();
                        dicObj.Grdd_com = BaseCls.GlbUserComCode;
                        dicObj.Grdd_cre_by = BaseCls.GlbUserID;
                        dicObj.Grdd_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now.Date;
                        dicObj.Grdd_seq = Convert.ToInt32(txtLine[i].Text);

                        //dicObj.Grdd_doc_ref = txtRef[i].Text;
                        dicObj.Grdd_is_realized = chkRelz[i].Checked;
                        dicObj.Grdd_realized_dt = Convert.ToDateTime(dtRelzDate[i].Text).Date;
                        dicObj.Grdd_scan_rcv = chkScan[i].Checked;
                        dicObj.Grdd_dt = Convert.ToDateTime(dtDate[i].Text).Date;//DateTime.Now.Date;
                        dicObj.Grdd_remarks = txtDocRemarks[i].Text;
                        // dicObj.grdd_is_short_set = Convert.ToInt32(txtIsShortSet[i].Text);

                        UpdateDocList.Add(dicObj);
                    }
                }
                else if (_TabIndex == 1)
                {
                    IS_RECEIVE = 1;
                    if (_recordCNT_Rec == 0)
                    {
                        MessageBox.Show("Cannot Save. Details not found !", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (_noRec == false)
                    {
                        for (int i = 1; i < _recordCNT_Rec + 1; i++)
                        {
                            ScanPhysicalDocReceiveDet dicObj = new ScanPhysicalDocReceiveDet();
                            dicObj.Grdd_com = BaseCls.GlbUserComCode;
                            dicObj.Grdd_cre_by = BaseCls.GlbUserID;
                            dicObj.Grdd_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;// DateTime.Now.Date;
                            dicObj.Grdd_seq = Convert.ToInt32(txtRLine[i].Text);

                            dicObj.Grdd_doc_rcv = chkPhyRec[i].Checked;
                            dicObj.Grdd_doc_val = Convert.ToDecimal(txtPhyRecVal[i].Text);
                            dicObj.Grdd_doc_bank = cmbBank[i].Text;
                            //dicObj.Grdd_doc_bank_cd = txtBankCode[i].Text;
                            dicObj.Grdd_deposit_bank = txtBankCode[i].Text;
                            dicObj.Grdd_doc_bank_branch = txtBranch[i].Text;
                            dicObj.Grdd_rmk = txtRem[i].Text;
                            dicObj.Grdd_dt = Convert.ToDateTime(dtRDate[i].Text);

                            //pnlRRef
                            dicObj.Grdd_doc_ref = txtRRef[i].Text;
                            UpdateDocList.Add(dicObj);
                        }
                    }
                }
                else if (_TabIndex == 2)
                {
                    IS_SUN = 1;
                    if (_recordCNT_Sun == 0)
                    {
                        MessageBox.Show("Cannot Save. Details not found !", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    for (int i = 1; i < _recordCNT_Sun + 1; i++)
                    {
                        ////------------------------------------------------------------------------------------------
                        //DataTable dt_CHECK_FOR_available = CHNLSVC.Financial.Get_GNT_RCV_DSK_DOC(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), Convert.ToDateTime(lblMonth.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), null);

                        //foreach (DataRow drr in dt_CHECK_FOR_available.Rows)
                        //{                       
                        //    Int32 sunUplaod_by = Convert.ToInt32(drr["grdd_sun_upload"].ToString());
                        //    if (sunUplaod_by == 1)
                        //    {
                        //        MessageBox.Show("Cannot process. This week is updated by Sun Upload tasks!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        return;
                        //    }

                        //}

                        ////---------------------------------------------------------------------------------------------

                        ScanPhysicalDocReceiveDet dicObj = new ScanPhysicalDocReceiveDet();
                        dicObj.Grdd_com = BaseCls.GlbUserComCode;
                        dicObj.Grdd_cre_by = BaseCls.GlbUserID;
                        dicObj.Grdd_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now.Date;
                        dicObj.Grdd_seq = Convert.ToInt32(txtSLine[i].Text);

                        dicObj.Grdd_sun_upload = chkSun[i].Checked;
                        dicObj.Grdd_doc_val = Convert.ToDecimal(txtPhyRecValSun[i].Text);
                        dicObj.Grdd_dt = Convert.ToDateTime(dtSDate[i].Text);
                        //pnlSRef
                        dicObj.Grdd_doc_ref = txtSRef[i].Text;
                        dicObj.Grdd_doc_bank = cmbSBank[i].Text;
                        UpdateDocList.Add(dicObj);
                    }
                }
                //Int32 IS_SCAN = ((CheckBox)grvDocDetails.Rows[0].FindControl("chekgrvScnRecive")).Enabled == true ? 1 : 0;
                //Int32 IS_SUN = ((CheckBox)grvDocDetails.Rows[0].FindControl("chekgrvSunUp")).Enabled == true ? 1 : 0;
                //Int32 IS_RECEIVE = ((CheckBox)grvDocDetails.Rows[0].FindControl("chekPhyReceive")).Enabled == true ? 1 : 0;
                if (MessageBox.Show("Are you sure to update?", "Confirm update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = 0;
                if (_TabIndex == 1)   //save short settlements
                {
                    Int32 Week = Convert.ToInt32(ddlWeek.SelectedIndex + 1);
                    eff = CHNLSVC.Financial.Save_Short_Settlements(BaseCls.GlbUserID, DateTime.Now.Date, BaseCls.GlbUserComCode, txtPC.Text.ToUpper(), Week, Convert.ToDateTime(lblMonth.Text).Date, lblTodtWk.Value.Date, Convert.ToString(ddlDocTypes.SelectedValue));
                }

                eff = CHNLSVC.Financial.Update_GNT_RCV_DSK_DOC(UpdateDocList, IS_SCAN, IS_SUN, IS_RECEIVE);//update upon permissions

                if (eff > 0)
                {
                    MessageBox.Show("Successfully Updated..", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isScreenChanged = false;
                }
                else
                {
                    MessageBox.Show("Not Updated!", "Scan Docs", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IS_SCAN = 0;
            IS_RECEIVE = 0;
            IS_SUN = 0;
            switch ((sender as TabControl).SelectedIndex)
            {

                case 0:
                    _TabIndex = 0;
                    IS_SCAN = 1;
                    //  if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                    // {
                    btnProcess.Enabled = false;
                    btnSave.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnProcess.Enabled = true;
                    //}
                    break;
                case 1:
                    _TabIndex = 1;
                    IS_RECEIVE = 1;
                    //if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                    //{
                    btnProcess.Enabled = true;
                    btnSave.Enabled = true;
                    //}
                    //else
                    //{
                    //    btnProcess.Enabled = true;
                    //}
                    break;
                case 2:
                    _TabIndex = 2;
                    IS_SUN = 1;
                    //if (BaseCls.GlbUserComCode != "AAL" && BaseCls.GlbUserComCode != "SGL" && BaseCls.GlbUserComCode != "SGD")
                    //{
                    btnProcess.Enabled = false;
                    btnSave.Enabled = false;
                    //}
                    //else
                    //{
                    //    btnProcess.Enabled = false;
                    //}
                    break;
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            pnlExecutive.Top = -vScrollBar1.Value + 50;
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            pnlRec.Top = -vScrollBar2.Value + 50;
        }

        private void vScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            pnlSunUser.Top = -vScrollBar3.Value + 50;
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                ImgBtnPC_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtPC_Leave(null, null);
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ImgBtnPC_Click(null, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //RemoveControls();
            //RemoveControls_RecDesk();
            //RemoveControls_Sun();

            //grvRem.DataSource = null;
            //grvSellectTool.DataSource = null;

            //_recordCNT_Sun = 0;
            //_recordCNT_Rec = 0;
            //_recordCNT = 0;

            //cmbMonth.SelectedIndex = -1;
            //cmbYear.SelectedIndex = -1;

            //ddlDocTypes.SelectedIndex = -1;
            //ddlWeek.SelectedIndex = -1;

            //lblFrmdtWk.Text = "-";
            //lblTodtWk.Text = "-";

            try
            {
                Scan_Physical_Docs formnew = new Scan_Physical_Docs();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
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

        private void txtPC_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPC.Text.Trim() == "")
                {
                    MessageBox.Show("Enter profit center first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (_isScreenChanged == true)
                        if (MessageBox.Show("You have do some changes to the screen. Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;

                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result = CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, txtPC.Text.Trim());
                    if (_result == null)
                    {
                        MessageBox.Show("Enter valid profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPC.Focus();
                        return;
                    }
                    if (_result.Rows.Count <= 0)
                    {
                        MessageBox.Show("Enter valid profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPC.Focus();
                        return;
                    }
                    //Int16 is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                    //if (is_Access != 1)
                    //{
                    //    MessageBox.Show("Access Denied !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtPC.Focus();
                    //    return;
                    //}
                }

                //cmbMonth.SelectedIndex = -1;
                //ddlWeek.SelectedIndex = -1;
                //lblFrmdtWk.Text = "";
                //lblTodtWk.Text = "";

                if (_TabIndex == 0)    //executive
                {
                    RemoveControls();
                    pnlExecutive.Height = 31;
                    pnlExecutive.Top = 51;
                }
                if (_TabIndex == 1)    //receiving desk
                {
                    RemoveControls_RecDesk();
                    pnlRec.Height = 31;
                    pnlRec.Top = 51;
                }
                if (_TabIndex == 2)    //sun upload user
                {
                    RemoveControls_Sun();
                    pnlSunUser.Height = 31;
                    pnlSunUser.Top = 51;
                }

                ddlWeek_SelectedIndexChanged(null, null);
                // grvRem.DataSource = null;

                //RemoveControls();
                //RemoveControls_RecDesk();
                //RemoveControls_Sun();

                //grvRem.DataSource = null;
                //grvSellectTool.DataSource = null;

                //_recordCNT_Sun = 0;
                //_recordCNT_Rec = 0;
                //_recordCNT = 0;
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

        private void txtPC_TextChanged(object sender, EventArgs e)
        {
            //cmbMonth.SelectedIndex = -1;
            //ddlWeek.SelectedIndex = -1;
            //lblFrmdtWk.Text = "";
            //lblTodtWk.Text = "";
            //grvRem.DataSource = null;
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            DateTime FromDate;
            DateTime ToDate;
            try
            {
                FromDate = Convert.ToDateTime(lblFrmdtWk.Text);
                ToDate = Convert.ToDateTime(lblTodtWk.Text);

                if (ddlWeek.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select week!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (lblFrmdtWkSelect.Text == "")
                {
                    MessageBox.Show("Week definition not set for the selected week!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDateTime(lblFrmdtWkSelect.Text) > FromDate)
                {
                    MessageBox.Show("Please select a 'From' date within the week", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDateTime(lblTodtWkSelect.Text) < ToDate)
                {
                    MessageBox.Show("Please select a 'To' date within the week", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10059))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Sorry, You have not permission for short settlement!\n( Advice: Required permission code :10059)", "Unauthorised Access", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Finance.Scan_Physical_Docs_Settle _settle = new Finance.Scan_Physical_Docs_Settle(this, Convert.ToDateTime(lblFrmdtWk.Text).Date, Convert.ToInt32(ddlWeek.SelectedIndex + 1), BaseCls.GlbUserComCode, txtPC.Text.Trim());
                _settle.Todate = lblTodtWk.Value.Date;
                _settle.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Scan_Physical_Docs_Deactivate(object sender, EventArgs e)
        {

        }

        private void Scan_Physical_Docs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isScreenChanged == true)
                if (MessageBox.Show("You have do some changes to the screen. Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel=true;
        }

    }

    public static class PanelExtension
    {
        public static void ScrollDown(this Panel p, int pos)
        {
            //pos passed in should be positive
            using (Control c = new Control() { Parent = p, Height = 1, Top = p.ClientSize.Height + pos })
            {
                p.ScrollControlIntoView(c);
            }
        }
        public static void ScrollUp(this Panel p, int pos)
        {
            //pos passed in should be negative
            using (Control c = new Control() { Parent = p, Height = 1, Top = pos })
            {
                p.ScrollControlIntoView(c);
            }
        }
    }
}
