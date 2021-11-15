using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.General;


//Written By kapila on 31/3/2014
namespace FF.WindowsERPClient.General
{
    public partial class GiftVoucherSettle : Base
    {
        public GiftVoucherSettle()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            lblAdd1.Text = "";
            lblBook.Text = "";
            lblCd.Text = "";
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblMobile.Text = "";
            lblPrefix.Text = "";
            txtGiftVoucher.Text = "";
            textBoxAmount.Text = "";
            txtRef.Text = "";

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.PartyType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(null + seperator + 0 + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void LoadGiftVoucher(string p)
        {
            Int32 val;

            if (!int.TryParse(p, out val))
                return;

            List<GiftVoucherPages> _gift = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(p));
            if (_gift != null)
            {
                if (_gift.Count == 1)
                {
                    lblAdd1.Text = _gift[0].Gvp_cus_add1;

                    lblCusCode.Text = _gift[0].Gvp_cus_cd;
                    lblCusName.Text = _gift[0].Gvp_cus_name;
                    lblMobile.Text = _gift[0].Gvp_cus_mob;
                    textBoxAmount.Text = _gift[0].Gvp_bal_amt.ToString();

                    lblBook.Text = _gift[0].Gvp_book.ToString();
                    lblPrefix.Text = _gift[0].Gvp_gv_cd;
                    lblCd.Text = _gift[0].Gvp_gv_prefix;

                }
                else
                {
                    gvMultipleItem.AutoGenerateColumns = false;
                    gvMultipleItem.Visible = true;
                    gvMultipleItem.DataSource = _gift;
                }
            }
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucher);
                DataTable _result = CHNLSVC.Inventory.SearchGiftVoucher(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGiftVoucher;
                _CommonSearch.ShowDialog();
                txtGiftVoucher.Select();
                if (txtGiftVoucher.Text != "")
                    LoadGiftVoucher(txtGiftVoucher.Text);
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

        private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    int book = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[1].Value);
                    int page = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[2].Value);
                    string code = gvMultipleItem.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string prefix = gvMultipleItem.Rows[e.RowIndex].Cells[5].Value.ToString();


                    GiftVoucherPages _gift = CHNLSVC.Inventory.GetGiftVoucherPage(BaseCls.GlbUserComCode, "%", code, book, page, prefix);
                    if (_gift != null)
                    {
                        //validation
                        //DateTime _date = _base.CHNLSVC.Security.GetServerDateTime();
                        if (_gift.Gvp_stus != "A")
                        {
                            MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_gift.Gvp_gv_tp != "VALUE")
                        {
                            MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!(_gift.Gvp_valid_from <= DateTime.Now.Date && _gift.Gvp_valid_to >= DateTime.Now.Date))
                        {
                            MessageBox.Show("Gift voucher From and To dates not in range\nFrom Date - " + _gift.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift.Gvp_valid_to.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        txtGiftVoucher.Text = _gift.Gvp_page.ToString();

                        lblCusCode.Text = _gift.Gvp_cus_cd;
                        lblCusName.Text = lblCusName.Text;
                        lblAdd1.Text = _gift.Gvp_cus_add1;

                        lblBook.Text = _gift.Gvp_book.ToString();
                        lblPrefix.Text = _gift.Gvp_gv_cd;
                        lblCd.Text = _gift.Gvp_gv_prefix;
                        textBoxAmount.Text = _gift.Gvp_bal_amt.ToString();
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

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGiftVoucher.Text))
            {
                MessageBox.Show("Select gift voucher nnumber", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are You Sure?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Int32 X = CHNLSVC.Sales.UpdateVouSettlement(BaseCls.GlbUserComCode,BaseCls.GlbUserDefProf,lblBook.Text,txtGiftVoucher.Text,lblPrefix.Text,lblCd.Text,BaseCls.GlbUserID,txtRef.Text,0,Convert.ToDateTime(txtDate.Text));
                btnClose_Click(null, null);
                MessageBox.Show("Successfully Updated", "Gift voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}


