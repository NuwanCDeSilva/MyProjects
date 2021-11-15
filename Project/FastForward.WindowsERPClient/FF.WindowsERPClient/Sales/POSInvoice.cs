using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Sales
{
    public partial class POSInvoice : Base
    {
        private Int32 _srchCriteria = 2;

        public POSInvoice()
        {
            InitializeComponent();
            setEnv();
        }

        private void setEnv()
        {
            pnlItem.Hide();
            pnlItem.Size = new System.Drawing.Size(1076, 474);
            pnlItem.Location = new System.Drawing.Point(12, 5);

            pnlPay.Hide();
            pnlPay.Size = new System.Drawing.Size(1076, 266);
            pnlPay.Location = new System.Drawing.Point(12, 5);

            pnlDisc.Hide();
            pnlDisc.Size = new System.Drawing.Size(1076, 164);
            pnlDisc.Location = new System.Drawing.Point(12, 5);

            pnlCust.Hide();
            pnlCust.Size = new System.Drawing.Size(1076, 474);
            pnlCust.Location = new System.Drawing.Point(12, 5);

            grvItems.ColumnHeadersHeight = 40;
            grvItems.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 18F, FontStyle.Regular);

            grvItemSrch.ColumnHeadersHeight = 40;
            grvItemSrch.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 18F, FontStyle.Regular);

            grvPayments.ColumnHeadersHeight = 30;
            grvPayments.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, FontStyle.Regular);

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "7,8,11" + seperator);
                        break;
                    }

                    break;
            }

            return paramsText.ToString();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            pnlItem.Show();
            pnlItem.Size = new System.Drawing.Size(1076, 164);
        }

        private void POSInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pnlItem.Hide();
                pnlPay.Hide();
                pnlCust.Hide();
                pnlDisc.Hide();
            }
        }

        private void txtSrchItem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSrchItem.Text))
                pnlItem.Size = new System.Drawing.Size(1076, 474);
            else
                pnlItem.Size = new System.Drawing.Size(1076, 164);
        }

        private void addItem()
        {

            pnlItem.Size = new System.Drawing.Size(1076, 164);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                addItem();
            }
        }

        private void txtSrchItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F5)
            {
                if (_srchCriteria == 1)
                {
                    lblSrchDesc.BackColor = Color.DeepSkyBlue;
                    lblSrchCode.BackColor = Color.LightSteelBlue;
                    lblSrchModel.BackColor = Color.LightSteelBlue;
                    lblSrchBrand.BackColor = Color.LightSteelBlue;
                    _srchCriteria = 2;
                }
                if (_srchCriteria == 2)
                {
                    lblSrchModel.BackColor = Color.DeepSkyBlue;
                    lblSrchCode.BackColor = Color.LightSteelBlue;
                    lblSrchDesc.BackColor = Color.LightSteelBlue;
                    lblSrchBrand.BackColor = Color.LightSteelBlue;
                    _srchCriteria = 3;
                }
                if (_srchCriteria == 3)
                {
                    lblSrchBrand.BackColor = Color.DeepSkyBlue;
                    lblSrchCode.BackColor = Color.LightSteelBlue;
                    lblSrchModel.BackColor = Color.LightSteelBlue;
                    lblSrchDesc.BackColor = Color.LightSteelBlue;
                    _srchCriteria = 4;
                }
                if (_srchCriteria == 4)
                {
                    lblSrchCode.BackColor = Color.DeepSkyBlue;
                    lblSrchDesc.BackColor = Color.LightSteelBlue;
                    lblSrchModel.BackColor = Color.LightSteelBlue;
                    lblSrchBrand.BackColor = Color.LightSteelBlue;
                    _srchCriteria = 1;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                this.Close(); 
        }

        private void btnClosePay_Click(object sender, EventArgs e)
        {
            pnlPay.Hide();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {

            pnlPay.Show();
        }

        private void btnCloseItm_Click(object sender, EventArgs e)
        {
            pnlItem.Hide();
        }

        private void btnDisc_Click(object sender, EventArgs e)
        {
            pnlDisc.Show();
        }

        private void btnCloseDisc_Click(object sender, EventArgs e)
        {
            pnlDisc.Hide();
        }

        private void btnCloseCust_Click(object sender, EventArgs e)
        {
            pnlCust.Hide();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            pnlCust.Show();
        }

    }
}