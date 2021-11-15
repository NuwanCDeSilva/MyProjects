using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using FF.BusinessObjects;

namespace FF.WindowsERPClient.Sales
{
    public partial class LoyaltyDefinitions : Base
    {

        #region public variables

        List<PriceBookLevelRef> PBList;
        List<CashCommissionDetailRef> ItemBrandCat_List;
        DataTable PCList = null;
        List<LoyaltyPointDefinition> _loyaltyPointList;

        #endregion

        public LoyaltyDefinitions()
        {
            InitializeComponent();

            PBList = new List<PriceBookLevelRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            PCList = new DataTable();
            _loyaltyPointList = new List<LoyaltyPointDefinition>();

            grvPayTp.AutoGenerateColumns = false;
            grvPayTp.DataSource = CHNLSVC.Sales.GetAllRepPriceType();
            //dilshan on 08/11/2018
            grvInvTp.AutoGenerateColumns = false;
            grvInvTp.DataSource = CHNLSVC.Sales.GetAllinvType();

        }

        private void Clear()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();

            //set cursor to wait
            this.Cursor = Cursors.WaitCursor;
           
            //TAB PAGE 01
            dtFrom.Value = _date;
            dtTo.Value = _date;
            txtlotyValidDays.Text = "";
            txtLoyaltyDescription.Text = "";
            txtLoyaltyMemberChg.Text = "";
            txtLoyaltyRenewalChg.Text = "";
            txtLoyaltyType.Text = "";
            chkAllowMultiple.Checked = false;
            chkCompulsory.Checked = false;
            txtLoyaltyType.Text = "";

            txtPriceBook.Text = "";
            txtLevel.Text = "";

            txtHierchCode.Text = "";

            txtPromotion.Text = "";
            txtItemCD.Text = "";
            txtSerial.Text = "";
           
            txtCate1.Text = "";
            txtCate2.Text = "";

            txtBrand.Text = "";


            //TAB PAGE 02
            txtLoyaltyBank.Text = "";
            dtFrom1.Value = _date;
            dtTo1.Value = _date;
            txtLoyaltyType1.Text="";
            txtValueFrom.Text = "";
            txtValueTo.Text = "";
            txtQtyFrom.Text = "";
            txtQtyTo.Text = "";
            txtLoyaltyPoints.Text = "";
            txtValueDiv.Text = "";
            chkIsMultiple.Checked = false;
            grvLoyaltyPoint.DataSource = null;
      

            txtPB1.Text = "";
            txtPlevel1.Text = "";

            txtHierchCode1.Text = "";

            txtPromotion1.Text = "";
            txtItemCD1.Text = "";
            txtSerial1.Text = "";
            txtCate11.Text = "";
            txtCate21.Text = "";

            txtBrand1.Text = "";

            //TAB PAGE 03
            //dtFrom2.Value = _date;
            //dtTo2.Value = _date;
            //txtLoyaltyType2.Text = "";
            //txtPointFrom.Text = "";
            //txtPointTo.Text = "";
            //txtDiscountRate.Text = "";

            //txtPB2.Text = "";
            //txtPlevel2.Text = "";

            //txtHierchCode2.Text = "";

            //txtPromotion2.Text = "";
            //txtItemCD2.Text = "";
            //txtSerial2.Text = "";
            //txtCate12.Text = "";
            //txtCate22.Text = "";
            //txtBrand2.Text = "";

            //TAB PAGE 04
            dtFrom3.Value = _date;
            dtTo3.Value = _date;
            txtLoyaltyType3.Text = "";
            txtRedeemPoint.Text = "";
            txtPointValue.Text = "";

            txtPB3.Text = "";
            txtPlevel3.Text = "";

            txtHierchCode3.Text = "";

            txtPromotion3.Text = "";
            txtItemCD3.Text = "";
            txtSerial3.Text = "";
            txtCate13.Text = "";
            txtCate13.Text = "";
            txtBrand3.Text = "";
            txtCusSpec.Text = "";

            //TAB PAGE 05
            txtLoyaltyType4.Text = "";
            txtsepPointFrom.Text = "";
            txtsepPointTo.Text = "";

            //clear gvs
            grvParty.DataSource = null;
           // grvParty2.DataSource = null;
            grvParty1.DataSource = null;
            grvParty3.DataSource = null;

            dataGridViewItem.DataSource = null;
            dataGridViewItem1.DataSource = null;
           // dataGridViewItem2.DataSource = null;
            dataGridViewItem3.DataSource = null;

            dataGridViewPriceBook.DataSource = null;
            dataGridViewPriceBook1.DataSource = null;
           // dataGridViewPriceBook2.DataSource = null;
            dataGridViewPriceBook3.DataSource = null;

            grvLoyaltyPoint.DataSource = null;

            grvPayTp.Enabled = true;
            grvInvTp.Enabled = true;
            chkAlwDisc.Enabled = true;
            chkAlwSch.Enabled = true;
            grvPayMode.Enabled = true;

            chkAlwDisc.Checked = false;
            chkAlwSch.Checked = false;


            PBList = new List<PriceBookLevelRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            PCList = new DataTable();
            _loyaltyPointList = new List<LoyaltyPointDefinition>();
            GC.Collect();

            //reset cursor
            this.Cursor = Cursors.Default;
        }

        #region common search

        #region item search
        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.ShowDialog();
                txtItemCD.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.txtSearchbyword.Text = txtSerial.Text;
                _CommonSearch.ShowDialog();
                txtSerial.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        private void btnPromation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion;
                _CommonSearch.txtSearchbyword.Text = txtPromotion.Text;
                _CommonSearch.ShowDialog();
                txtPromotion.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        #region item search 1

        private void btnBrand1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand1;
                _CommonSearch.txtSearchbyword.Text = txtBrand1.Text;
                _CommonSearch.ShowDialog();
                txtBrand1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnMainCat1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate11;
                _CommonSearch.txtSearchbyword.Text = txtCate11.Text;
                _CommonSearch.ShowDialog();
                txtCate11.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCat1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate21;
                _CommonSearch.txtSearchbyword.Text = txtCate21.Text;
                _CommonSearch.ShowDialog();
                txtCate21.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        private void btnItem1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD1;
                _CommonSearch.ShowDialog();
                txtItemCD1.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSerial1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.ShowDialog();
                txtSerial.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        private void btnPromation1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion1;
                _CommonSearch.txtSearchbyword.Text = txtPromotion1.Text;
                _CommonSearch.ShowDialog();
                txtPromotion1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region item search 2

        private void btnBrand2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBrand2;
            _CommonSearch.txtSearchbyword.Text = txtBrand2.Text;
            _CommonSearch.ShowDialog();
            txtBrand2.Focus();
             */
        }

        private void btnMainCat2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate12;
            _CommonSearch.txtSearchbyword.Text = txtCate12.Text;
            _CommonSearch.ShowDialog();
            txtCate12.Focus();
             */
        }

        private void btnCat2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate22;
            _CommonSearch.txtSearchbyword.Text = txtCate22.Text;
            _CommonSearch.ShowDialog();
            txtCate22.Focus();
             */
        }



        private void btnItem2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemCD2;
            _CommonSearch.ShowDialog();
            txtItemCD2.Select();
             */
        }

        private void btnSerial2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
            DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSerial2;
            _CommonSearch.ShowDialog();
            txtSerial2.Select();
             */
        }



        private void btnPromation2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPromotion2;
            _CommonSearch.txtSearchbyword.Text = txtPromotion2.Text;
            _CommonSearch.ShowDialog();
            txtPromotion2.Focus();
             */
        }

        #endregion

        #region itrm search 3

        private void btnBrand3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand3;
                _CommonSearch.txtSearchbyword.Text = txtBrand3.Text;
                _CommonSearch.ShowDialog();
                txtBrand3.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnMainCat3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate13;
                _CommonSearch.txtSearchbyword.Text = txtCate13.Text;
                _CommonSearch.ShowDialog();
                txtCate13.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCat3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate23;
                _CommonSearch.txtSearchbyword.Text = txtCate23.Text;
                _CommonSearch.ShowDialog();
                txtCate23.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD3;
                _CommonSearch.ShowDialog();
                txtItemCD3.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSerial3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial3;
                _CommonSearch.ShowDialog();
                txtSerial3.Select();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPromation3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion3;
                _CommonSearch.txtSearchbyword.Text = txtPromotion3.Text;
                _CommonSearch.ShowDialog();
                txtPromotion3.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        #endregion

        #region price book/price level
        private void btnSearchPriceLvl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceBook;
                _CommonSearch.txtSearchbyword.Text = txtPriceBook.Text;
                _CommonSearch.ShowDialog();
                txtPriceBook.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchPB_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceBook;
                _CommonSearch.txtSearchbyword.Text = txtPriceBook.Text;
                _CommonSearch.ShowDialog();
                txtPriceBook.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion 

        #region price book/price level1 1

        private void btnPB1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPB1;
                _CommonSearch.txtSearchbyword.Text = txtPB1.Text;
                _CommonSearch.ShowDialog();
                txtPB1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPlevel1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPlevel1;
                _CommonSearch.txtSearchbyword.Text = txtPlevel1.Text;
                _CommonSearch.ShowDialog();
                txtPlevel1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region price book/price level 2

        private void btnPB2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPB2;
            _CommonSearch.txtSearchbyword.Text = txtPB2.Text;
            _CommonSearch.ShowDialog();
            txtPB2.Focus();
             */
        }

        private void btnPlevel2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPlevel2;
            _CommonSearch.txtSearchbyword.Text = txtPlevel2.Text;
            _CommonSearch.ShowDialog();
            txtPlevel2.Focus();
             */
        }

        #endregion

        #region price book/price level 3

        private void btnPB3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPB3;
                _CommonSearch.txtSearchbyword.Text = txtPB3.Text;
                _CommonSearch.ShowDialog();
                txtPB3.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPlevel3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPlevel3;
                _CommonSearch.txtSearchbyword.Text = txtPlevel3.Text;
                _CommonSearch.ShowDialog();
                txtPlevel3.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnHierachySearch1_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes1.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode1;
                _CommonSearch.ShowDialog();
                txtHierchCode1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnHierachySearch2_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes2.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes2.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode1;
                _CommonSearch.ShowDialog();
                txtHierchCode2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
             */
        }

        private void btnHierachySearch3_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes3.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode3;
                _CommonSearch.ShowDialog();
                txtHierchCode3.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoyaltyBank;
                _CommonSearch.ShowDialog();
                txtLoyaltyBank.Select();
                BindCardType(txtLoyaltyBank.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        #region loyalty type

        private void btnLoyaltyType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
                DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoyaltyType1;
                _CommonSearch.txtSearchbyword.Text = txtLoyaltyType1.Text;
                _CommonSearch.ShowDialog();
                txtLoyaltyType1.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnLoyaltyType2_Click(object sender, EventArgs e)
        {
            /*
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
            DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtLoyaltyType2;
            _CommonSearch.txtSearchbyword.Text = txtLoyaltyType2.Text;
            _CommonSearch.ShowDialog();
            txtLoyaltyType2.Focus();
             */
        }

        private void btnLoyaltyType3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
                DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoyaltyType3;
                _CommonSearch.txtSearchbyword.Text = txtLoyaltyType3.Text;
                _CommonSearch.ShowDialog();
                txtLoyaltyType3.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnLoyaltyType4_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
                DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoyaltyType4;
                _CommonSearch.txtSearchbyword.Text = txtLoyaltyType4.Text;
                _CommonSearch.ShowDialog();
                txtLoyaltyType4.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        #endregion

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
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        if (tabControl1.SelectedIndex == 0)
                        {

                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                            break;
                        }
                        else if(tabControl1.SelectedIndex==1){
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB1.Text.Trim() + seperator);
                            break; 
                        }
                        //else if (tabControl1.SelectedIndex == 2)
                        //{
                        //    paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB2.Text.Trim() + seperator);
                        //    break;
                        //}
                        else if (tabControl1.SelectedIndex == 2)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB3.Text.Trim() + seperator);
                            break;
                        }
                        else {
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(txtItemCD.Text + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append("" + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtExcecType.Text.Trim().ToUpper());
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashCommissionCircular:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loyalty_Type:
                    {
                        paramsText.Append(BaseCls.GlbUserDefProf + seperator + DateTime.Now.ToString("dd/MMM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
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

        #region price book add

        private void btnAddPB_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPriceBook.AutoGenerateColumns = false;
                List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
                if (txtLevel.Text != "" && txtPriceBook.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPriceBook.Text && _res.Sapl_pb_lvl_cd == txtLevel.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book, level already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (txtPriceBook.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPriceBook.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim().ToUpper(), txtLevel.Text.Trim().ToUpper()));

                pbLIST.RemoveAll(x => x.Sapl_act == false);
                var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                             .Select(g => g.First())
                             .ToList();
                if (distinctList == null || distinctList.Count <= 0)
                {
                    MessageBox.Show("Invalid Price book or Level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PBList.AddRange(distinctList);

                BindingSource source = new BindingSource();
                source.DataSource = PBList;
                dataGridViewPriceBook.DataSource = source;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewPriceBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PBList.RemoveAt(e.RowIndex);
                    BindingSource source = new BindingSource();
                    source.DataSource = PBList;
                    dataGridViewPriceBook.DataSource = source;
                }
            }
        }

        #endregion

        #region price book ADD1

        private void btnAddPB1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPriceBook1.AutoGenerateColumns = false;
                List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
                if (txtPlevel1.Text != "" && txtPB1.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPB1.Text && _res.Sapl_pb_lvl_cd == txtPlevel1.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book, level already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (txtPB1.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPB1.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, txtPB1.Text.Trim().ToUpper(), txtPlevel1.Text.Trim().ToUpper()));

                pbLIST.RemoveAll(x => x.Sapl_act == false);
                var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                             .Select(g => g.First())
                             .ToList();
                if (distinctList == null || distinctList.Count <= 0)
                {
                    MessageBox.Show("Invalid Price book or Level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PBList.AddRange(distinctList);

                BindingSource source = new BindingSource();
                source.DataSource = PBList;
                dataGridViewPriceBook1.DataSource = source;
                txtPB1.Text = "";
                txtPlevel1.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewPriceBook1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PBList.RemoveAt(e.RowIndex);
                    BindingSource source = new BindingSource();
                    source.DataSource = PBList;
                    dataGridViewPriceBook1.DataSource = source;
                }
            }
        }

        #endregion

        #region price book ADD2

        private void btnAddPB2_Click(object sender, EventArgs e)
        {
            /*
            dataGridViewPriceBook2.AutoGenerateColumns = false;
            List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
            if (txtPlevel2.Text != "" && txtPB2.Text != "")
            {
                List<PriceBookLevelRef> tem = (from _res in PBList
                                               where _res.Sapl_pb == txtPB2.Text && _res.Sapl_pb_lvl_cd == txtPlevel2.Text
                                               select _res).ToList<PriceBookLevelRef>();
                if (tem != null && tem.Count > 0)
                {
                    MessageBox.Show("Selected price book, level already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (txtPB2.Text != "")
            {
                List<PriceBookLevelRef> tem = (from _res in PBList
                                               where _res.Sapl_pb == txtPB2.Text
                                               select _res).ToList<PriceBookLevelRef>();
                if (tem != null && tem.Count > 0)
                {
                    MessageBox.Show("Selected price book already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, txtPB2.Text.Trim().ToUpper(), txtPlevel2.Text.Trim().ToUpper()));

            pbLIST.RemoveAll(x => x.Sapl_act == false);
            var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                         .Select(g => g.First())
                         .ToList();
            if (distinctList == null || distinctList.Count <= 0)
            {
                MessageBox.Show("Invalid Price book or Level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PBList.AddRange(distinctList);

            BindingSource source = new BindingSource();
            source.DataSource = PBList;
            dataGridViewPriceBook2.DataSource = source;
            txtPB2.Text = "";
            txtPlevel2.Text = "";
             */
        }

        private void dataGridViewPriceBook2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PBList.RemoveAt(e.RowIndex);
                    BindingSource source = new BindingSource();
                    source.DataSource = PBList;
                    dataGridViewPriceBook2.DataSource = source;
                }
            }
             */
        }

        #endregion

        #region price book ADD3

        private void btnAddPB3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPriceBook3.AutoGenerateColumns = false;
                List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
                if (txtPlevel3.Text != "" && txtPB3.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPB3.Text && _res.Sapl_pb_lvl_cd == txtPlevel3.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book, level already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (txtPB3.Text != "")
                {
                    List<PriceBookLevelRef> tem = (from _res in PBList
                                                   where _res.Sapl_pb == txtPB3.Text
                                                   select _res).ToList<PriceBookLevelRef>();
                    if (tem != null && tem.Count > 0)
                    {
                        MessageBox.Show("Selected price book already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, txtPB3.Text.Trim().ToUpper(), txtPlevel3.Text.Trim().ToUpper()));


                pbLIST.RemoveAll(x => x.Sapl_act == false);
                var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                             .Select(g => g.First())
                             .ToList();

                if (distinctList == null || distinctList.Count <= 0)
                {
                    MessageBox.Show("Invalid Price book or Level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PBList.AddRange(distinctList);

                BindingSource source = new BindingSource();
                source.DataSource = PBList;
                dataGridViewPriceBook3.DataSource = source;
                txtPB3.Text = "";
                txtPlevel3.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewPriceBook3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PBList.RemoveAt(e.RowIndex);
                    BindingSource source = new BindingSource();
                    source.DataSource = PBList;
                    dataGridViewPriceBook3.DataSource = source;
                }
            }
        }

        #endregion

        #region brand/cat/item ADD

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {

                /*
                 
                 BRAND
                 MAIN CATEGORY
                 CATEGORY
                 ITEM
                 SERIAL
                 PROMOTION
                 
                 */

                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                string selection = "";
                if (cmbSelectCat.SelectedItem.ToString() == "BRAND")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "MAIN CATEGORY")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "CATEGORY")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "ITEM")
                {
                    selection = "ITEM";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "SERIAL")
                {
                    selection = "SERIAL";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "PROMOTION")
                {
                    selection = "PROMOTION";
                }
               
                

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), "", txtItemCD.Text.Trim(), txtSerial.Text.Trim(), "", txtPromotion.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.Text;
                }
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    string brand = txtBrand.Text;
                    CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                    if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE3")
                    {
                        obj.Sccd_brd = brand;
                    }
                    else
                    {
                        obj.Sccd_brd = "N/A";
                    }

                    obj.Sccd_itm = code;
                    try
                    {
                        obj.Sccd_ser = dr["descript"].ToString();
                    }
                    catch (Exception)
                    {
                        obj.Sccd_ser = "";
                    }

                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        addList.Add(obj);
                    }

                }
                if (addList == null || addList.Count <= 0) {
                    MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ItemBrandCat_List = new List<CashCommissionDetailRef>();

                dataGridViewItem.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                dataGridViewItem.AutoGenerateColumns = false;
                dataGridViewItem.DataSource = ItemBrandCat_List;
                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.Text;
                }
              
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {

                if (MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ItemBrandCat_List.RemoveAt(e.RowIndex);

                    dataGridViewItem.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = ItemBrandCat_List;
                    dataGridViewItem.DataSource = _source;
                }
            }
        }

        #endregion

        #region brand/cat /item ADD1

        private void btnAddCat1_Click(object sender, EventArgs e)
        {
            try
            {

                /*
                 
                 BRAND
                 MAIN CATEGORY
                 CATEGORY
                 SUB CATEGORY
                 ITEM
                 SERIAL
                 PROMOTION
                 
                 */

                if (cmbSelectCat1.SelectedItem == null || cmbSelectCat1.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selection = "";
                if (cmbSelectCat1.SelectedItem.ToString() == "BRAND")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "MAIN CATEGORY")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "CATEGORY")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "SUB CATEGORY")
                {
                    selection = "CATE3";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "ITEM")
                {
                    selection = "ITEM";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "SERIAL")
                {
                    selection = "SERIAL";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "PROMOTION")
                {
                    selection = "PROMOTION";
                }
                else if (cmbSelectCat1.SelectedItem.ToString() == "CIRCULAR")
                {
                    selection = "CIRCULAR";
                }

                //ItemBrandCat_List = new List<CashCommissionDetailRef>();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand1.Text.Trim(), txtCate11.Text.Trim(), txtCate21.Text.Trim(), "", txtItemCD1.Text.Trim(), txtSerial1.Text.Trim(), "", txtPromotion1.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat1.Text;
                }
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    string brand = txtBrand.Text;
                    CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                    if (cmbSelectCat1.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat1.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat1.SelectedItem.ToString() == "BRAND_CATE3")
                    {
                        obj.Sccd_brd = brand;
                    }
                    else
                    {
                        obj.Sccd_brd = "N/A";
                    }

                    obj.Sccd_itm = code;
                    try
                    {
                        obj.Sccd_ser = dr["descript"].ToString();
                    }
                    catch (Exception)
                    {
                        obj.Sccd_ser = "";
                    }

                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                     select _dup;

                    if (_duplicate.Count() == 0)
                    {
                        addList.Add(obj);
                    }
                    else if(_duplicate.Count()>0) {
                        MessageBox.Show("Duplicate record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (addList == null || addList.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dataGridViewItem1.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                dataGridViewItem1.AutoGenerateColumns = false;
                dataGridViewItem1.DataSource = source;
                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem1.Columns[1].HeaderText = cmbSelectCat1.Text;
                }

                //select all
                foreach (DataGridViewRow grv in dataGridViewItem1.Rows) {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                    cell.Value = "True";
                }
                txtItemCD1.Text = "";
                txtCate11.Text = "";
                txtBrand1.Text = "";
                txtCate21.Text = "";
                txtSerial1.Text = "";
                txtPromotion1.Text = "";

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewItem1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ItemBrandCat_List.RemoveAt(e.RowIndex);

                dataGridViewItem1.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = ItemBrandCat_List;
                dataGridViewItem1.DataSource = _source;
            }
        }
        
        #endregion

        #region brand/cat/item ADD2

        private void btnAddCat2_Click(object sender, EventArgs e)
        {
            /*
            try
            {

                
                 
                 //BRAND
                 //MAIN CATEGORY
                 //CATEGORY
                 //SUB CATEGORY
                 //ITEM
                 //SERIAL
                 //PROMOTION
                 
                 

                if (cmbSelectCat2.SelectedItem == null || cmbSelectCat2.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selection = "";
                if (cmbSelectCat2.SelectedItem.ToString() == "BRAND")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat2.SelectedItem.ToString() == "MAIN CATEGORY")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat2.SelectedItem.ToString() == "CATEGORY")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat2.SelectedItem.ToString() == "ITEM")
                {
                    selection = "ITEM";
                }
                else if (cmbSelectCat2.SelectedItem.ToString() == "SERIAL")
                {
                    selection = "SERIAL";
                }
                else if (cmbSelectCat2.SelectedItem.ToString() == "PROMOTION")
                {
                    selection = "PROMOTION";
                }


                ItemBrandCat_List = new List<CashCommissionDetailRef>();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand2.Text.Trim(), txtCate12.Text.Trim(), txtCate22.Text.Trim(), "", txtItemCD2.Text.Trim(), txtSerial2.Text.Trim(), "", txtPromotion2.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat2.Text;
                }
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    string brand = txtBrand.Text;
                    CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                    if (cmbSelectCat2.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat2.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat2.SelectedItem.ToString() == "BRAND_CATE3")
                    {
                        obj.Sccd_brd = brand;
                    }
                    else
                    {
                        obj.Sccd_brd = "N/A";
                    }

                    obj.Sccd_itm = code;
                    try
                    {
                        obj.Sccd_ser = dr["descript"].ToString();
                    }
                    catch (Exception)
                    {
                        obj.Sccd_ser = "";
                    }

                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                     select _dup;
                    if (addList == null || addList.Count <= 0)
                    {
                        MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_duplicate.Count() == 0)
                    {
                        addList.Add(obj);
                    }

                }

                dataGridViewItem2.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                dataGridViewItem2.AutoGenerateColumns = false;
                dataGridViewItem2.DataSource = ItemBrandCat_List;
                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem2.Columns[1].HeaderText = cmbSelectCat2.Text;
                }

                //select all
                foreach (DataGridViewRow grv in dataGridViewItem2.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                    cell.Value = "True";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
             */
        }

        private void dataGridViewItem2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ItemBrandCat_List.RemoveAt(e.RowIndex);

                dataGridViewItem2.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = ItemBrandCat_List;
                dataGridViewItem2.DataSource = _source;
            }
             */
        }

        #endregion

        #region brand/cat/item ADD3

        private void btnAddCat3_Click(object sender, EventArgs e)
        {
            try
            {

                /*
                 
                 BRAND
                 MAIN CATEGORY
                 CATEGORY
                 SUB CATEGORY
                 ITEM
                 SERIAL
                 PROMOTION
                 
                 */

                if (cmbSelectCat3.SelectedItem == null || cmbSelectCat3.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selection = "";
                if (cmbSelectCat3.SelectedItem.ToString() == "BRAND")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat3.SelectedItem.ToString() == "MAIN CATEGORY")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat3.SelectedItem.ToString() == "CATEGORY")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat3.SelectedItem.ToString() == "ITEM")
                {
                    selection = "ITEM";
                }
                else if (cmbSelectCat3.SelectedItem.ToString() == "SERIAL")
                {
                    selection = "SERIAL";
                }
                else if (cmbSelectCat3.SelectedItem.ToString() == "PROMOTION")
                {
                    selection = "PROMOTION";
                }


                ItemBrandCat_List = new List<CashCommissionDetailRef>();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand3.Text.Trim(), txtCate13.Text.Trim(), txtCate23.Text.Trim(), "", txtItemCD3.Text.Trim(), txtSerial3.Text.Trim(), "", txtPromotion3.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem3.Columns[1].HeaderText = cmbSelectCat3.Text;
                }
                List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    string brand = txtBrand.Text;
                    CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                    if (cmbSelectCat3.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat3.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat3.SelectedItem.ToString() == "BRAND_CATE3")
                    {
                        obj.Sccd_brd = brand;
                    }
                    else
                    {
                        obj.Sccd_brd = "N/A";
                    }

                    obj.Sccd_itm = code;
                    try
                    {
                        obj.Sccd_ser = dr["descript"].ToString();
                    }
                    catch (Exception)
                    {
                        obj.Sccd_ser = "";
                    }

                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                                     select _dup;
                    if (addList == null || addList.Count <= 0)
                    {
                        MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_duplicate.Count() == 0)
                    {
                        addList.Add(obj);
                    }

                }
                dataGridViewItem3.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                dataGridViewItem3.AutoGenerateColumns = false;
                dataGridViewItem3.DataSource = ItemBrandCat_List;
                if (dt.Rows.Count > 0)
                {
                    dataGridViewItem3.Columns[1].HeaderText = cmbSelectCat3.Text;
                }

                //select all
                foreach (DataGridViewRow grv in dataGridViewItem3.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                    cell.Value = "True";
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewItem3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ItemBrandCat_List.RemoveAt(e.RowIndex);

                dataGridViewItem3.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = ItemBrandCat_List;
                dataGridViewItem3.DataSource = _source;
            }
        }


        #endregion

        #region pc ADD

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
                    }
                }
                if (_result == null || _result.Rows.Count <= 0) {
                    MessageBox.Show("Invalid search term, no result found" , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PCList = _result;
                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = _result;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void grvParty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PCList.Rows.RemoveAt(e.RowIndex);

                    grvParty1.AutoGenerateColumns = false;
                    grvParty1.DataSource = PCList;
                }

            }
        }

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
        }

        #endregion

        #region pc ADD1

        private void btnAddPartys1_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes1.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }

                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode1.Text);
                    }
                }
                else if (DropDownListPartyTypes1.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                    if (txtHierchCode1.Text != "")
                    {
                        _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode1.Text, "");
                    }
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                PCList = _result;
                grvParty1.DataSource = null;
                grvParty1.AutoGenerateColumns = false;
                grvParty1.DataSource = _result;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PCList.Rows.RemoveAt(e.RowIndex);

                    grvParty.AutoGenerateColumns = false;
                    grvParty.DataSource = PCList;
                }
            }
        }
    
        #endregion

        #region pc ADD2

        private void btnAddPartys2_Click(object sender, EventArgs e)
        {
            /*
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes2.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }
            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }
            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }

            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }
            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }
            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }
            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode2.Text);
                }
            }
            else if (DropDownListPartyTypes2.SelectedValue.ToString() == "GPC")
            {
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
                if (txtHierchCode2.Text != "")
                {
                    _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode2.Text, "");
                }
            }
            if (_result == null || _result.Rows.Count <= 0)
            {
                MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PCList = _result;
            grvParty2.DataSource = null;
            grvParty2.AutoGenerateColumns = false;
            grvParty2.DataSource = _result;
             */
        }

        private void grvParty2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PCList.Rows.RemoveAt(e.RowIndex);

                    grvParty2.AutoGenerateColumns = false;
                    grvParty2.DataSource = PCList;
                }
            }
             */
        }

        #endregion

        #region pc ADD3

        private void btnAddPartys3_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes3.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }

                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode3.Text);
                    }
                }
                else if (DropDownListPartyTypes3.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                    if (txtHierchCode3.Text != "")
                    {
                        _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode3.Text, "");
                    }
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                PCList = _result;
                grvParty3.DataSource = null;
                grvParty3.AutoGenerateColumns = false;
                grvParty3.DataSource = _result;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvParty3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PCList.Rows.RemoveAt(e.RowIndex);

                    grvParty3.AutoGenerateColumns = false;
                    grvParty3.DataSource = PCList;
                }
            }
        }

        #endregion

        #region save

        private void Process()
        {

            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                //save loyalty type
                if (tabControl1.SelectedIndex == 0)
                {
                    #region validation

                    if (txtLoyaltyType.Text == "") {
                        MessageBox.Show("Please enter loyalty type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtLoyaltyMemberChg.Text == "")
                    {
                        MessageBox.Show("Please enter membership charge", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtLoyaltyRenewalChg.Text == "")
                    {
                        MessageBox.Show("Please enter renewal charge", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtlotyValidDays.Text == "")
                    {
                        MessageBox.Show("Please enter valid period in days", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dtFrom.Value.Date > dtTo.Value.Date)
                    {
                        MessageBox.Show("From date has to be smaller than to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //if (PBList.Count <= 0)
                    //{
                    //    MessageBox.Show("Please add price book level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //if (PCList.Rows.Count <= 0)
                    //{
                    //    MessageBox.Show("Please add pc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(cmbSelectCat.SelectedItem.ToString()))
                    //{
                    //    MessageBox.Show("Please select item category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    #endregion

                    List<string> party = new List<string>();
                    //grvParty.EndEdit();
                    //foreach (DataGridViewRow gvr in grvParty.Rows)
                    //{
                    //    bool duplicate = party.Contains(gvr.Cells[1].Value.ToString());
                    //    if (!duplicate)
                    //    {
                    //        party.Add(gvr.Cells[1].Value.ToString());
                    //    }
                    //}

                    //save loyalty types
                    LoyaltyType _loyal = new LoyaltyType();
                    _loyal.Salt_loty_tp = txtLoyaltyType.Text.ToUpper();
                    _loyal.Salt_desc = txtLoyaltyDescription.Text.ToUpper();
                    _loyal.Salt_frm_dt = dtFrom.Value.Date;
                    _loyal.Salt_to_dt = dtTo.Value.Date;
                    _loyal.Salt_is_comp = chkCompulsory.Checked;
                    _loyal.Salt_alw_multi_cdpnt = chkAllowMultiple.Checked;
                    _loyal.Salt_memb_chg = Convert.ToDecimal(txtLoyaltyMemberChg.Text);
                    _loyal.Salt_renew_chg = Convert.ToDecimal(txtLoyaltyRenewalChg.Text);
                    _loyal.Salt_valid = Convert.ToInt32(txtlotyValidDays.Text);
                    
                    _loyal.Salt_pt_tp = DropDownListPartyTypes.SelectedValue.ToString();
                   


                  int result=  CHNLSVC.Sales.SaveLoyaltyType(_loyal);
                  if (result > 0)
                  {
                      MessageBox.Show("Successfully insert records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      Clear();
                  }
                  else {
                      MessageBox.Show("Insert unsuccessfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }
                }
                //save loyalty point definition
                else if (tabControl1.SelectedIndex == 1) {
                    #region validation

                    if (txtLoyaltyType1.Text == "")
                    {
                        MessageBox.Show("Please select loyalty type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dtFrom1.Value.Date > dtTo1.Value.Date)
                    {
                        MessageBox.Show("From date has to be smaller than to date");
                        return;
                    }
                    if (_loyaltyPointList==null ||  _loyaltyPointList.Count <= 0)
                    {
                        if (txtValueFrom.Text == "")
                        {
                            MessageBox.Show("Please enter from value");
                            return;
                        }
                        if (txtValueTo.Text == "")
                        {
                            MessageBox.Show("Please enter to value");
                            return;
                        }
                        if (Convert.ToDecimal(txtValueFrom.Text) > Convert.ToDecimal(txtValueTo.Text))
                        {
                            MessageBox.Show("From value has to be smaller than to value");
                            return;
                        }
                        if (txtQtyFrom.Text == "")
                        {
                            MessageBox.Show("Please enter qty from");
                            return;
                        }
                        if (txtQtyTo.Text == "")
                        {
                            MessageBox.Show("Please enter qty to");
                            return;
                        }
                        if (Convert.ToInt32(txtQtyFrom.Text) > Convert.ToInt32(txtQtyTo.Text))
                        {
                            MessageBox.Show("From qty has to be smaller than to qty");
                            return;
                        }
                        if (txtLoyaltyPoints.Text == "")
                        {
                            MessageBox.Show("Please enter point value");
                            return;
                        }
                    }

                    if (PBList.Count <= 0)
                    {
                        MessageBox.Show("Please add price book level");
                        return;
                    }
                    if (PCList.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please add pc");
                        return;
                    }

                    #endregion

                    List<string> party = new List<string>();
                    grvParty.EndEdit();
                    foreach (DataGridViewRow gvr in grvParty1.Rows)
                    {
                        bool duplicate = party.Contains(gvr.Cells[1].Value.ToString());
                        if (!duplicate)
                        {
                            party.Add(gvr.Cells[1].Value.ToString());
                        }
                    }

                    List<CashCommissionDetailRef> _itemList = new List<CashCommissionDetailRef>();
                    //dataGridViewItem1.EndEdit();
                    for(int i=0;i<dataGridViewItem1.Rows.Count;i++) {
                        dataGridViewItem1.EndEdit();
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridViewItem1.Rows[i].Cells[0];
                        if (cell.Value.ToString().ToUpper() == "TRUE") {
                            _itemList.Add(ItemBrandCat_List[i]);
                        }

                    }



                    LoyaltyPointDefinition _loyalty = new LoyaltyPointDefinition();
                    _loyalty.Saldf_is_multi = chkIsMultiple.Checked;
                    _loyalty.Saldf_loty_tp = txtLoyaltyType1.Text;
                    _loyalty.Saldf_alw_prt_tp = DropDownListPartyTypes1.SelectedValue.ToString();
                    _loyalty.Saldf_val_frm = dtFrom1.Value.Date;
                    _loyalty.Saldf_val_to = dtTo1.Value.Date;
                    _loyalty.Saldf_cre_by = BaseCls.GlbUserID;
                    _loyalty.Saldf_cre_dt = _date;
                    try
                    {
                        if (_loyaltyPointList == null || _loyaltyPointList.Count <= 0)
                        {
                            _loyalty.Saldf_value_frm = Convert.ToInt32(txtValueFrom.Text);
                            _loyalty.Saldf_value_to = Convert.ToInt32(txtValueTo.Text);
                            _loyalty.Saldf_qt_frm = Convert.ToInt32(txtQtyFrom.Text);
                            _loyalty.Saldf_qt_to = Convert.ToInt32(txtQtyTo.Text);
                            _loyalty.Saldf_pt = Convert.ToDecimal(txtLoyaltyPoints.Text);
                            if (cmbCustomerSpec.SelectedValue != null)
                                _loyalty.Saldf_cus_spec = cmbCustomerSpec.SelectedValue.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Value range and qty range has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                   // if (cmbPayType.SelectedValue != null)
                   // _loyalty.Saldf_pmod = cmbPayType.SelectedValue.ToString();
                  //  _loyalty.Saldf_bank = txtLoyaltyBank.Text;
                    //if(cmbCardType.SelectedItem!=null)
                    //_loyalty.Saldf_cd_tp = cmbCardType.SelectedItem.ToString();
                    //if (cmbCustomerSpec.SelectedValue != null)
                    //    _loyalty.Saldf_cus_spec = cmbCustomerSpec.SelectedValue.ToString();

                    string _itemType = "";
                    if(cmbSelectCat1.SelectedItem!=null)
                        _itemType=cmbSelectCat1.SelectedItem.ToString();


                    int result = CHNLSVC.Sales.SaveLoyaltyPointDefinition(_loyalty, _loyaltyPointList, party, PBList, _itemList, _itemType);
                   if (result > 0)
                   {
                       MessageBox.Show("Successfully insert records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       Clear();
                   }
                   else
                   {
                       MessageBox.Show("Insert unsuccessfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   }
                
                }
                /*
                else if (tabControl1.SelectedIndex == 2) {
                    #region validation

                    if (txtLoyaltyType2.Text == "")
                    {
                        MessageBox.Show("Please select loyalty type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dtFrom2.Value.Date > dtTo2.Value.Date)
                    {
                        MessageBox.Show( "From date has tobe smaller than to date","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtPointFrom.Text == "")
                    {
                        MessageBox.Show("Please enter point from value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtPointTo.Text == "")
                    {
                        MessageBox.Show("Please enter point from value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Convert.ToInt32(txtPointFrom.Text) > Convert.ToInt32(txtPointTo.Text))
                    {
                        MessageBox.Show("From point has to be smaller than to point", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (PBList.Count <= 0)
                    {
                        MessageBox.Show("Please add price book level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (PCList.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please add pc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    #endregion        

                    List<string> party = new List<string>();
                    grvParty.EndEdit();
                    foreach (DataGridViewRow gvr in grvParty2.Rows)
                    {
                        bool duplicate = party.Contains(gvr.Cells[1].Value.ToString());
                        if (!duplicate)
                        {
                            party.Add(gvr.Cells[1].Value.ToString());
                        }
                    }
                    List<CashCommissionDetailRef> _itemList = new List<CashCommissionDetailRef>();
                    //dataGridViewItem1.EndEdit();
                    for (int i = 0; i < dataGridViewItem2.Rows.Count; i++)
                    {
                        dataGridViewItem2.EndEdit();
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridViewItem2.Rows[i].Cells[0];
                        if (cell.Value.ToString().ToUpper() == "TRUE")
                        {
                            _itemList.Add(ItemBrandCat_List[i]);
                        }

                    }

                    LoyaltyPointDiscount _loyal = new LoyaltyPointDiscount();
                    _loyal.Saldi_loty_tp = txtLoyaltyType2.Text;
                    _loyal.Saldi_frm = dtFrom2.Value.Date;
                    _loyal.Saldi_to = dtTo2.Value.Date;
                    _loyal.Saldi_pt_frm = Convert.ToInt32(txtPointFrom.Text);
                    _loyal.Saldi_pt_to = Convert.ToInt32(txtPointTo.Text);
                    _loyal.Saldi_dis_rt = Convert.ToDecimal(txtDiscountRate.Text);
                    _loyal.Saldi_prt_tp = DropDownListPartyTypes2.SelectedValue.ToString();

                    string _itemType = "";
                    if (cmbSelectCat2.SelectedItem != null)
                        _itemType = cmbSelectCat2.SelectedItem.ToString();

                    int result = CHNLSVC.Sales.SaveLoyaltyDiscountDefinition(_loyal, _itemList, party, PBList, _itemType);
                  if (result > 0)
                  {
                      MessageBox.Show("Successfully insert records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      Clear();
                  }
                  else
                  {
                      MessageBox.Show("Insert unsuccessfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }
                
                }
                 */
                else if (tabControl1.SelectedIndex == 2) {

                    if (txtLoyaltyType3.Text == "")
                    {
                        MessageBox.Show("Please select loyalty type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (PCList.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please add pc", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtRedeemPoint.Text == "") {
                        MessageBox.Show("Please add redeem point", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txtPointValue.Text == "") {
                        MessageBox.Show("Please add redeem value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    List<string> party = new List<string>();
                    grvParty.EndEdit();
                    foreach (DataGridViewRow gvr in grvParty3.Rows)
                    {
                        bool duplicate = party.Contains(gvr.Cells[1].Value.ToString());
                        if (!duplicate)
                        {
                            party.Add(gvr.Cells[1].Value.ToString());
                        }
                    }


                    LoyaltyPointRedeemDefinition _loyal = new LoyaltyPointRedeemDefinition();
                    _loyal.Salre_loty_tp = txtLoyaltyType3.Text;
                    _loyal.Salre_frm_dt = dtFrom3.Value.Date;
                    _loyal.Salre_to_dt =dtTo3.Value.Date;
                    _loyal.Salre_red_pt = Convert.ToInt32(txtRedeemPoint.Text);
                    _loyal.Salre_pt_value = Convert.ToInt32(txtPointValue.Text);
                    _loyal.Salre_alw_prt_tp  = DropDownListPartyTypes3.SelectedValue.ToString();

                    string _itemType = "";
                    if (cmbSelectCat3.SelectedItem != null)
                        _itemType = cmbSelectCat3.SelectedItem.ToString();

                    int result = CHNLSVC.Sales.SaveLoyaltyRedeemDefinition(_loyal, party);
                   if (result > 0)
                   {
                       MessageBox.Show("Successfully insert records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       Clear();
                   }
                   else
                   {
                       MessageBox.Show("Insert unsuccessfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   }
                }
                else if (tabControl1.SelectedIndex == 3) {

                    if (txtLoyaltyType4.Text == "")
                    {
                        MessageBox.Show("Please select loyalty type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    LoyaltyCustomerSpecification _loyal = new LoyaltyCustomerSpecification();
                    _loyal.Salcs_loty_tp = txtLoyaltyType4.Text;
                    _loyal.Salcs_po_from = Convert.ToInt32(txtsepPointFrom.Text);
                    _loyal.Salcs_po_to = Convert.ToInt32(txtsepPointTo.Text);
                    _loyal.Salcs_spec = txtCusSpec.Text;
                    _loyal.Salcs_cre_by = BaseCls.GlbUserID;
                    _loyal.Salcs_cre_dt = DateTime.Now;

                   int result= CHNLSVC.Sales.SaveLoyaltyCustomerSpecification(_loyal);
                   if (result > 0)
                   {
                       MessageBox.Show("Successfully insert records", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       Clear();
                   }
                   else
                   {
                       MessageBox.Show("Insert unsuccessfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        #region data bind
        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("COM", "Company");
            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        public void BindPartyType1()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("COM", "Company");
            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes1.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes1.DisplayMember = "Value";
            DropDownListPartyTypes1.ValueMember = "Key";
        }

        public void BindPartyType2()
        {
            /*
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes2.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes2.DisplayMember = "Value";
            DropDownListPartyTypes2.ValueMember = "Key";
             */
        }

        public void BindPartyType3()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("COM", "Company");
            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes3.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes3.DisplayMember = "Value";
            DropDownListPartyTypes3.ValueMember = "Key";
        }

        private void BindPayType()
        {
            List<PaymentTypeRef> _ref =CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            PaymentTypeRef _payment = new PaymentTypeRef();
            _payment.Sapt_cd = "";
            _payment.Sapt_desc = "";
            _ref.Add(_payment);
            grvPayMode.AutoGenerateColumns = false;
            grvPayMode.DataSource = _ref;
     
        }

        private void BindCustomerSpecification()
        {

            DataTable _dt = CHNLSVC.Sales.GetLoyaltyCustomerSpecifications(txtLoyaltyType1.Text);
             DataRow _dr= _dt.NewRow();
            _dr["SALCS_SPEC"]="";
            _dt.Rows.Add(_dr);
            cmbCustomerSpec.DataSource = _dt;
            cmbCustomerSpec.DisplayMember = "SALCS_SPEC";
            cmbCustomerSpec.ValueMember = "SALCS_SPEC";
            //cmbCustomerSpec.Items.Clear();
            //DataTable _dt = CHNLSVC.Sales.GetLoyaltyCustomerSpecifications(txtLoyaltyType4.Text);
            //foreach (DataRow dr in _dt.Rows) {
            //    cmbCustomerSpec.Items.Add(dr["SALCS_SPEC"].ToString());
            //}
        }

        private void BindCardType(string p)
        {
            //DataTable _dt = CHNLSVC.Sales.GetBankCC(p);
            //if (_dt.Rows.Count > 0)
            //{
            //    cmbCardType.DataSource = _dt;
            //    cmbCardType.DisplayMember = "mbct_cc_tp";
            //    cmbCardType.ValueMember = "mbct_cc_tp";
            //}
            //else
            //{
            //    cmbCardType.DataSource = null;
            //}
        }

        #endregion

        #region button event
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //set cursor to wait
            this.Cursor = Cursors.WaitCursor;

            Process();

            //set cursor to default
            this.Cursor = Cursors.Default;
        } 
        #endregion

        private void LoyaltyDefinitions_Load(object sender, EventArgs e)
        {
            BindPartyType();
            BindPartyType1();
            BindPartyType2();
            BindPartyType3();
            BindPayType();
            BindCustomerSpecification();
            txtLoyaltyType.Select();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtLoyaltyType4_Leave(object sender, EventArgs e)
        {
            if (txtLoyaltyType3.Text != "")
            {
                LoyaltyType _type = CHNLSVC.Sales.GetLoyaltyType(txtLoyaltyType3.Text);
                if (_type == null)
                {
                    MessageBox.Show("Invalid Loyalty Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLoyaltyType3.Text = "";
                    return;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Boolean _isSelect = false;
            Boolean _isSelectInv = false;
            if (txtValueFrom.Text == "")
            {
                MessageBox.Show("Please enter from value");
                return;
            }
            if (txtValueTo.Text == "")
            {
                MessageBox.Show("Please enter to value");
                return;
            }
            if (Convert.ToDecimal(txtValueFrom.Text) > Convert.ToDecimal(txtValueTo.Text))
            {
                MessageBox.Show("From value has to be smaller than to value");
                return;
            }
            if (txtQtyFrom.Text == "")
            {
                MessageBox.Show("Please enter qty from");
                return;
            }
            if (txtQtyTo.Text == "")
            {
                MessageBox.Show("Please enter qty to");
                return;
            }
            if (Convert.ToInt32(txtQtyFrom.Text) > Convert.ToInt32(txtQtyTo.Text))
            {
                MessageBox.Show("From qty has to be smaller than to qty");
                return;
            }
            if (txtLoyaltyPoints.Text == "")
            {
                MessageBox.Show("Please enter point value");
                return;
            }
            foreach (DataGridViewRow gvr in grvPayTp.Rows)
            {
                if (Convert.ToBoolean(gvr.Cells[0].Value) == true)
                    _isSelect = true;
            }
            if (_isSelect == false)
            {
                MessageBox.Show("Please select price type");
                return;
            }
            //dilshan on 08/11/2018 ****
            foreach (DataGridViewRow invtp in grvInvTp.Rows)
            {
                if (Convert.ToBoolean(invtp.Cells[0].Value) == true)
                    _isSelectInv = true;
            }
            if (_isSelectInv == false)
            {
                MessageBox.Show("Please select invoice type");
                return;
            }
            //**************************
            try
            {
                foreach (DataGridViewRow gvr in grvPayTp.Rows)
                {
                    if (Convert.ToBoolean( gvr.Cells[0].Value) == true)
                    {
                        foreach (DataGridViewRow gvr1 in grvPayMode.Rows)
                        {
                            if (Convert.ToBoolean(gvr1.Cells[0].Value) == true)
                            {
                                foreach (DataGridViewRow invtp in grvInvTp.Rows)//by dilshan on 08/11/2018
                                {
                                    if (Convert.ToBoolean(invtp.Cells[0].Value) == true)
                                    {
                                        LoyaltyPointDefinition _loyalty = new LoyaltyPointDefinition();

                                        _loyalty.Saldf_value_frm = Convert.ToInt32(txtValueFrom.Text);
                                        _loyalty.Saldf_value_to = Convert.ToInt32(txtValueTo.Text);
                                        _loyalty.Saldf_qt_frm = Convert.ToInt32(txtQtyFrom.Text);
                                        _loyalty.Saldf_qt_to = Convert.ToInt32(txtQtyTo.Text);
                                        _loyalty.Saldf_pt = Convert.ToInt32(txtLoyaltyPoints.Text);
                                        _loyalty.Saldf_alw_dis = chkAlwDisc.Checked;
                                        _loyalty.Saldf_alw_ins = chkAlwSch.Checked;
                                        _loyalty.Saldf_alw_ptp = Convert.ToInt32(gvr.Cells[3].Value);
                                        _loyalty.Saldf_minv_tp = invtp.Cells[1].Value.ToString();//dilshan
                                        _loyalty.Saldf_pmod = gvr1.Cells[2].Value.ToString();
                                        _loyalty.Saldf_bank = txtLoyaltyBank.Text;
                                        if (cmbCardType.SelectedItem != null)
                                            _loyalty.Saldf_cd_tp = cmbCardType.SelectedItem.ToString();
                                        if (cmbCustomerSpec.SelectedValue != null)
                                            _loyalty.Saldf_cus_spec = cmbCustomerSpec.SelectedValue.ToString();
                                        _loyaltyPointList.Add(_loyalty);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Value from,to Qty from,t has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if(cmbPayType.SelectedValue!=null)
            //_loyalty.Saldf_pmod = cmbPayType.SelectedValue.ToString();
            //_loyalty.Saldf_bank = txtLoyaltyBank.Text;
            //if(cmbCardType.SelectedValue!=null)
            //_loyalty.Saldf_cd_tp = cmbCardType.SelectedValue.ToString();
            //if (cmbCustomerSpec.SelectedValue != null)
            //    _loyalty.Saldf_cus_spec = cmbCustomerSpec.SelectedValue.ToString();
            //_loyaltyPointList.Add(_loyalty);  kapila 4/8/2014

            grvLoyaltyPoint.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _loyaltyPointList;
            grvLoyaltyPoint.DataSource = _source;

            txtValueFrom.Text = "";
            txtValueTo.Text = "";
            txtQtyTo.Text = "";
            txtLoyaltyPoints.Text = "";
            txtQtyFrom.Text = "";
            grvPayTp.Enabled = false;
            grvInvTp.Enabled = false;
            chkAlwDisc.Enabled = false;
            chkAlwSch.Enabled = false;
            grvPayMode.Enabled = false;
        }

        private void chkIsMultiple_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsMultiple.Checked)
            {
               // btnAdd.Enabled = false;
                grvLoyaltyPoint.AutoGenerateColumns = false;
                _loyaltyPointList = new List<LoyaltyPointDefinition>();
                BindingSource _source = new BindingSource();
                _source.DataSource = _loyaltyPointList;
                grvLoyaltyPoint.DataSource = _source;
            }
            else
            {
                //btnAdd.Enabled = true;
            }
        }

        private void txtLoyaltyRenewalChg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtlotyValidDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtLoyaltyMemberChg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtValueFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtValueTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtQtyFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }
        }

        private void txtQtyTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLoyaltyPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPointFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPointTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtRedeemPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPointValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtsepPointFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtsepPointTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        #region tab 01 search
        private void txtPriceBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPB_Click(null, null);
        }

        private void LoyaltyDefinitions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchPB_Click(null, null);
        }

        private void txtLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPriceLvl_Click(null, null);
        }

        private void txtLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchPriceLvl_Click(null, null);
        }

        private void txtHierchCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnHierachySearch_Click(null, null);
        }

        private void txtHierchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch_Click(null, null);
        }

        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand_Click(null, null);
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand_Click(null, null);
        }

        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat_Click(null, null);
        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnMainCat_Click(null, null);
        }

        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat_Click(null, null);
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCat_Click(null, null);
        }

        private void txtItemCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem_Click(null, null);
        }

        private void txtItemCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItem_Click(null, null);
        }

        private void txtSerial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial_Click(null, null);
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSerial_Click(null, null);
        }

        private void txtPromotion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPromation_Click(null, null);
        }

        private void txtPromotion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPromation_Click(null, null);
        } 
        #endregion

        #region tab 02 search
        private void txtPB1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPB1_Click(null, null);
        }

        private void txtPB1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPB1_Click(null, null);
        }

        private void txtPlevel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPlevel1_Click(null, null);
        }

        private void txtPlevel1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPlevel1_Click(null, null);
        }

        private void txtHierchCode1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnHierachySearch1_Click(null, null);
        }

        private void txtHierchCode1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch1_Click(null, null);
        }

        private void txtBrand1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand1_Click(null, null);
        }

        private void txtBrand1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand1_Click(null, null);
        }

        private void txtCate11_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat1_Click(null, null);
        }

        private void txtCate11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnMainCat1_Click(null, null);
        }

        private void txtCate21_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat1_Click(null, null);
        }

        private void txtCate21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCat1_Click(null, null);
        }

        private void txtItemCD1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem1_Click(null, null);
        }

        private void txtItemCD1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItem1_Click(null, null);
        }

        private void txtSerial1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial1_Click(null, null);
        }

        private void txtSerial1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSerial1_Click(null, null);
        }

        private void txtPromotion1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPromation1_Click(null, null);
        }

        private void txtPromotion1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPromation1_Click(null, null);

        }

        private void txtLoyaltyType1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
            btnLoyaltyType_Click(null, null);
        }

        private void txtLoyaltyType1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnLoyaltyType_Click(null, null);
        }
        #endregion

        #region tab 03 search

        private void txtLoyaltyType2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnLoyaltyType2_Click(null, null);
        }

        private void txtLoyaltyType2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnLoyaltyType2_Click(null, null);
        }

        private void txtPB2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPB2_Click(null, null);
        }

        private void txtPB2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPB2_Click(null, null);
        }

        private void txtPlevel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPlevel2_Click(null, null);
        }

        private void txtPlevel2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPlevel2_Click(null, null);
        }

        private void txtHierchCode2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnHierachySearch2_Click(null, null);
        }

        private void txtHierchCode2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch2_Click(null, null);
        }

        private void txtBrand2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand2_Click(null, null);
        }

        private void txtBrand2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand2_Click(null, null);
        }

        private void txtCate12_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat2_Click(null, null);
        }

        private void txtCate12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnMainCat2_Click(null, null);
        }

        private void txtCate22_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat2_Click(null, null);
        }

        private void txtCate22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCat2_Click(null, null);
        }

        private void txtItemCD2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem2_Click(null, null);
        }

        private void txtItemCD2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItem2_Click(null, null);
        }

        private void txtSerial2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial2_Click(null, null);
        }

        private void txtSerial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSerial2_Click(null, null);
        }

        private void txtPromotion2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPlevel2_Click(null, null);
        }

        private void txtPromotion2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPromation2_Click(null, null);
        } 
        #endregion

        #region tab 04 search
        private void txtLoyaltyType3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnLoyaltyType3_Click(null, null);
        }

        private void txtLoyaltyType3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnLoyaltyType3_Click(null, null);
        }

        private void txtPB3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPB3_Click(null, null);
        }

        private void txtPB3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPB3_Click(null, null);
        }

        private void txtPlevel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPlevel3_Click(null, null);
        }

        private void txtPlevel3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPlevel3_Click(null, null);
        }

        private void txtHierchCode3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnHierachySearch3_Click(null, null);
        }

        private void txtHierchCode3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch3_Click(null, null);
        }

        private void txtBrand3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand3_Click(null, null);
        }

        private void txtBrand3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand3_Click(null, null);
        }

        private void txtCate13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat3_Click(null, null);
        }

        private void txtCate13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnMainCat3_Click(null, null);
        }

        private void txtCate23_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat3_Click(null, null);
        }

        private void txtCate23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCat3_Click(null, null);
        }

        private void txtItemCD3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button5_Click(null, null);
        }

        private void txtItemCD3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                button5_Click(null, null);
        }

        private void txtSerial3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial3_Click(null, null);
        }

        private void txtSerial3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSerial3_Click(null, null);
        }

        private void txtPromotion3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPromation3_Click(null, null);
        }

        private void txtPromotion3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPromation3_Click(null, null);
        } 
        #endregion

        private void txtLoyaltyType4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnLoyaltyType4_Click(null, null);
        }

        private void txtLoyaltyType4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnLoyaltyType4_Click(null, null);
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (m.Msg == 0x0100 && (int)m.WParam == 13)
            {
                this.ProcessTabKey(true);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void btnAll_Hirchy_Click(object sender, EventArgs e)
        {

        }

        private void btnItemAll_Click(object sender, EventArgs e)
        {

        }

        private void txtLoyaltyType1_Leave(object sender, EventArgs e)
        {
            if (txtLoyaltyType1.Text != "")
            {
                LoyaltyType _type = CHNLSVC.Sales.GetLoyaltyType(txtLoyaltyType1.Text);
                if (_type == null)
                {
                    MessageBox.Show("Invalid Loyalty Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLoyaltyType1.Text = "";
                    return;
                }
                else {
                    BindCustomerSpecification();
                }
            }
        }

        private void txtLoyaltyBank_Leave(object sender, EventArgs e)
        {
            if (txtLoyaltyBank.Text != "")
                if (!CheckBank(txtLoyaltyBank.Text))
                    txtLoyaltyBank.Text = "";
        }

        private bool CheckBank(string bank)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetails(bank, "BANK");

                if (_bankAccounts.Mbi_cd != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Please select the valid bank.");
                    return false;
                }
            }
            return false;


        }

        private void txtLoyaltyType2_Leave(object sender, EventArgs e)
        {
            /*
            if (txtLoyaltyType2.Text != "")
            {
                LoyaltyType _type = CHNLSVC.Sales.GetLoyaltyType(txtLoyaltyType2.Text);
                if (_type == null)
                {
                    MessageBox.Show("Invalid Loyalty Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLoyaltyType2.Text = "";
                    return;
                }
            }
             */
        }

        private void txtLoyaltyType3_Leave(object sender, EventArgs e)
        {
            if (txtLoyaltyType3.Text != "")
            {
                LoyaltyType _type = CHNLSVC.Sales.GetLoyaltyType(txtLoyaltyType3.Text);
                if (_type == null)
                {
                    MessageBox.Show("Invalid Loyalty Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLoyaltyType3.Text = "";
                    return;
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            /*
            //select all
            foreach (DataGridViewRow grv in dataGridViewItem2.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "True";
            }
             */
        }

        private void button22_Click(object sender, EventArgs e)
        {
            /*
            //select all
            foreach (DataGridViewRow grv in dataGridViewItem2.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "False";
            }
             */
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grv in dataGridViewItem1.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "True";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grv in dataGridViewItem1.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "False";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grv in dataGridViewItem3.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "True";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow grv in dataGridViewItem3.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                cell.Value = "False";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridViewItem1.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void cmbSelectCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCD1.Text = "";
            txtCate11.Text = "";
            txtBrand1.Text = "";
            txtCate21.Text = "";
            txtSerial1.Text = "";
            txtPromotion1.Text = "";
            dataGridViewItem1.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void grvLoyaltyPoint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0) {
                try
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _loyaltyPointList.RemoveAt(e.RowIndex);
                        BindingSource _source = new BindingSource();
                        _source.DataSource = _loyaltyPointList;
                        grvLoyaltyPoint.DataSource = _source;
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Error occurred while processing.\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CHNLSVC.CloseChannel(); 
                    return;
                }
            }
        }

        private void chkAllPayMode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAllPayMode.Checked == true)
                {
                    foreach (DataGridViewRow row in grvPayMode.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvPayMode.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvPayMode.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvPayMode.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        
    }
}
