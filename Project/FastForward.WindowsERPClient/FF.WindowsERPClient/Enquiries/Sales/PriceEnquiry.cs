using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Globalization;
using System.Linq;

namespace FF.WindowsERPClient.Enquiries.Sales
{
    public partial class PriceEnquiry : FF.WindowsERPClient.Base
    {
        #region Variables
        MasterProfitCenter _masterProfitCenter = null;
        List<PriceDefinitionRef> _priceDefinitionRef = null;
        MasterItem _itemdetail = null;
        private bool IsSuperUser = false;

        private int PageSize = 50;
        private int CurrentPageSize = 0;
        private Boolean _isStrucBaseTax = false;
        #endregion

        #region Rooting for Initialize Form
        private void LoadCachedObjects()
        {
            _masterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _priceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            pnlBalance.Size = new Size(403, 112);
        }
        private void LoadDefaultBookNLevel()
        {
            if (_priceDefinitionRef != null) if (_priceDefinitionRef.Count > 0)
                {
                    var _defaultValue = _priceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                    if (_defaultValue != null)
                        if (_defaultValue.Count > 0)
                        {
                            txtPriceBook.Text = _defaultValue[0].Sadd_pb;
                            txtLevel.Text = _defaultValue[0].Sadd_p_lvl;
                        }
                }

        }
        private void UserPermissionforSuperUser()
        {
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, CommonUIDefiniton.UserPermissionType.PRICENQ.ToString()))
            {
                IsSuperUser = true;
                btnSearch_Pc.Enabled = true;
            }
            else
            {
                IsSuperUser = false;
                btnSearch_Pc.Enabled = false;
            }
        }
        private void ClearGridView()
        {
            gvCombine.DataSource = null;
            gvPriceSerial.DataSource = null;
            grvPriceDetail.DataSource = null;
            grvScheme.DataSource = null;
        }
        public PriceEnquiry()
        {
            InitializeComponent();
            grvPriceDetail.AutoGenerateColumns = false;
            gvCombine.AutoGenerateColumns = false;
            grvScheme.AutoGenerateColumns = false;
            gvPriceSerial.AutoGenerateColumns = false;
            gvBalance.AutoGenerateColumns = false;
            gvPromotionDiscount.AutoGenerateColumns = false;
            dgvPricedet.AutoGenerateColumns = false; //Tharindu
            panel6.Visible = false;
            UserPermissionforSuperUser();
            LoadCachedObjects(); //LoadDefaultBookNLevel();
            pnlCombine.Size = new Size(857, 195);
            cmbStatus.SelectedIndex = 0;
            grvPriceDetail.Columns["pr_vatExPrice"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_vatInPrice"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_qtyFrom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_qtyTo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_Times"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_used"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_vatrate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            grvPriceDetail.Columns["pr_pbseq"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;

            gvPriceSerial.Columns["prs_pricewovat"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            gvPriceSerial.Columns["prs_wvat"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            gvPriceSerial.Columns["prs_pbseq"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            gvPriceSerial.Columns["prs_vatrate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            txtPc.Text = BaseCls.GlbUserDefProf;
            if (!IsSuperUser) txtAsAtDt.Enabled = false;

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

        }
        #endregion

        #region Rooting for Common Search

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Scheme:
                    {
                        paramsText.Append(txtSchemeCD.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceType:
                    {
                        paramsText.Append("%" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circualr:
                    {
                        paramsText.Append("%" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RawPriceBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RawPriceLevel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Book_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSuperUser == false)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPriceBook;
                    _CommonSearch.ShowDialog();
                    txtPriceBook.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetRawPriceBook(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPriceBook;
                    _CommonSearch.ShowDialog();
                    txtPriceBook.Select();
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
        private void txtPriceBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Book_Click(null, null);
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

        private void btnSearch_Level_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPriceBook.Clear();
                    txtPriceBook.Focus();
                    return;
                }

                if (IsSuperUser == false)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtLevel;
                    _CommonSearch.ShowDialog();
                    txtLevel.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RawPriceLevel);
                    DataTable _result = CHNLSVC.CommonSearch.GetRawPriceLevel(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtLevel;
                    _CommonSearch.ShowDialog();
                    txtLevel.Select();
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
        private void txtLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Level_Click(null, null);
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

        private void btnSearch_Circular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.ShowDialog();
                txtCircular.Select();
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
        private void txtCircular_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Circular_Click(null, null);
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

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
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
        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Item_Click(null, null);
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

        private void btnSearch_Cat1_Click(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
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
        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Cat1_Click(null, null);
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

        private void btnSearch_Cat2_Click(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
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
        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Cat2_Click(null, null);
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

        private void btnSearch_Cat3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate3;
                _CommonSearch.ShowDialog();
                txtCate3.Focus();
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
        private void txtCate3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Cat3_Click(null, null);
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

        private void btnSearch_Type_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtType;
                _CommonSearch.ShowDialog();
                txtType.Select();
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
        private void txtType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Type_Click(null, null);
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

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomer;
                _CommonSearch.ShowDialog();
                txtCustomer.Select();
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
        private void txtCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Customer_Click(null, null);
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

        private void btnSearch_Scheme_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
                DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSchemeCD;
                _CommonSearch.ShowDialog();
                txtSchemeCD.Select();
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
        private void txtSchemeCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (chkScheme.Checked) btnSearch_Scheme_Click(null, null);
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

        private void btnSearch_Promotion_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionCode);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion;
                _CommonSearch.ShowDialog();
                txtPromotion.Select();
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
        private void txtPromotion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Promotion_Click(null, null);
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
        private void txtPromotion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Promotion_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtItem.Focus();
        }

        #endregion

        #region Rooting for Navigate Screen
        private void txtPriceBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Book_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtLevel.Focus();
        }
        private void txtLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Level_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCircular.Focus();
        }
        private void txtCircular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Circular_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtPromotion.Focus();
        }
        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
            {
                txtCate1.Focus();
                btnGetDetail_Click(null, null);
            }
        }
        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Cat1_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCate2.Focus();

        }
        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Cat2_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCate3.Focus();
        }
        private void txtCate3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Cat3_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtType.Focus();

        }
        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Type_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtCustomer.Focus();
        }
        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Customer_Click(null, null);
        }
        private void rdoDateRange_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void txtFromDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtToDt.Focus();
        }
        private void txtToDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rdoAsAt.Focus();
        }
        private void rdoWithHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAsAtDt.Focus();
        }
        private void txtAsAtDt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSchemeCD.Focus();
        }
        private void txtSchemeCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                if (chkScheme.Checked) btnSearch_Scheme_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetDetail.Focus();
        }
        #endregion

        #region Rooting for Check Price Book
        private void txtPriceBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please check the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPriceBook.Clear();
                    txtPriceBook.Focus();
                }
                txtLevel.Clear();
                ClearGridView();
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
        #endregion

        #region Rooting for Check Price Level
        private void txtLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLevel.Text)) return;
                if (string.IsNullOrEmpty(txtPriceBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPriceBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim(), txtLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please check the price level.", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); txtLevel.Clear(); txtLevel.Focus(); return; }
                ClearGridView();
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
        #endregion

        #region Rooting for Check Circular
        private void txtCircular_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCircular.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circualr);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchData(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CIRCULAR") == txtCircular.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid circular no", "Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Clear();
                    txtCircular.Focus();
                    return;
                }
                ClearGridView();
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
        #endregion

        #region Rooting for Load Item Detail
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            Decimal VAT_RATE = 0;
            Boolean _isTaxFound = false;
            try
            {
                lblItemDescription.Text = "Description : ";
                lblItemModel.Text = "Model : ";
                lblItemBrand.Text = "Brand : ";
                lblItemSubStatus.Text = "Serial Status : ";
                lblVatRate.Text = "Imported VAT Rt. : ";
                _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the item code", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    _isValid = false;
                    return _isValid;
                }
                if (_itemdetail != null)
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "None";

                        lblItemDescription.Text = "Description : "; //+ _description; 
                        txtItemDescription.Text = _description; //Edit by Chamal 22/Oct/2013
                        lblItemModel.Text = "Model : ";// +_model;
                        txtItemModel.Text = _model; //Edit by Chamal 22/Oct/2013
                        lblItemBrand.Text = "Brand : " + _brand;
                        txtBrand.Text = _brand;
                        lblItemSubStatus.Text = "Serial Status : " + _serialstatus;

                        //kapila 21/4/2017
                        Boolean _isStrucBaseTax = false;
                        _isTaxFound = false;
                        MasterCompany _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                        if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

                        if (_isStrucBaseTax == true)    //kapila  26/11/2015
                        {
                            List<MasterItemTax> _itmTax = new List<MasterItemTax>();
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemdetail.Mi_cd);
                            _itmTax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _itemdetail.Mi_cd, "GOD", string.Empty, string.Empty, _mstItem.Mi_anal1);
                            if (_itmTax.Count > 0)
                            {
                                _isTaxFound = true;
                                foreach (MasterItemTax _one in _itmTax)
                                {
                                    if (_one.Mict_tax_cd=="VAT")
                                    {
                                        VAT_RATE = _one.Mict_tax_rate; 
                                    }
                                   
                                }
                            }
                            else
                            {
                                _itmTax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _itemdetail.Mi_cd, "GDLP", string.Empty, string.Empty, _mstItem.Mi_anal1);
                                if (_itmTax.Count > 0)
                                {
                                    _isTaxFound = true;
                                    foreach (MasterItemTax _one in _itmTax)
                                    {
                                        if (_one.Mict_tax_cd == "VAT")
                                        {
                                            VAT_RATE = _one.Mict_tax_rate;
                                        }
                                    }
                                }
                            }

                            if (_isTaxFound == false)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Cannot find valid tax definition.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        else
                        {
                            VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(BaseCls.GlbUserComCode, _itemdetail.Mi_cd, "VAT");
                        }
                    }

                if (_isTaxFound == true)
                {
                    lblVatRate.Text = "Imported VAT Rt. : " + VAT_RATE.ToString() + "%";
                }
                else
                {
                    lblVatRate.Text = "Imported VAT Rt. : Definition Error";
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return _isValid;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
            return _isValid;
        }
        #endregion

        #region Rooting for Check Item
        protected void CheckItem()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                LoadItemDetail(txtItem.Text.Trim());
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally { this.Cursor = Cursors.Default; }

        }
        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (BaseCls.GlbUserComCode == "AST")
                {
                    string _item = "";
                    //kapila 18/11/2013
                    if (txtItem.Text.Length == 16)
                        _item = txtItem.Text.Substring(1, 7);
                    else if (txtItem.Text.Length == 15)
                        _item = txtItem.Text.Substring(0, 7);
                    else if (txtItem.Text.Length == 8)
                        _item = txtItem.Text.Substring(1, 7);
                    else if (txtItem.Text.Length == 20)
                        _item = txtItem.Text.Substring(0, 12);
                    else
                        _item = txtItem.Text;

                    txtItem.Text = _item;
                }
                panel6.Visible = false;

                LoadItemDetail(txtItem.Text.Trim());
                ClearGridView();
                if (chkWithInv.Checked) LoadBalance();
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
        #endregion

        #region Rooting for Check Category 1
        private void txtCate1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCate1.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCate1.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCate1.Clear();
                    txtCate1.Focus();
                    return;
                }
                txtCate2.Clear();
                txtCate3.Clear();
                ClearGridView();
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
        #endregion

        #region Rooting for Check Category 2
        private void txtCate2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCate2.Text)) return;
                if (string.IsNullOrEmpty(txtCate1.Text)) { MessageBox.Show("Please select the main category first", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCate2.Clear(); txtCate1.Focus(); return; }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCate2.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid category code", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCate2.Clear();
                    txtCate2.Focus();
                    return;
                }
                txtCate3.Clear();
                ClearGridView();
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
        #endregion

        #region Rooting for Check Category 3
        private void txtCate3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCate3.Text)) return;
                if (string.IsNullOrEmpty(txtCate2.Text)) { MessageBox.Show("Please select the main category first", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCate3.Clear(); txtCate2.Focus(); return; }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCate2.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid sub category code", "Sub Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCate2.Clear();
                    txtCate2.Focus();
                    return;
                }
                ClearGridView();
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
        #endregion

        #region Rooting for Check Type
        private void txtType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
                DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, null, null);
                var _isExist = _result.AsEnumerable().Where(x => x.Field<string>("TypeCode") == txtType.Text.Trim());
                if (_isExist == null || _isExist.Count() <= 0)
                { MessageBox.Show("Please select the valid type", "Type", MessageBoxButtons.OK, MessageBoxIcon.Information); txtType.Clear(); txtType.Focus(); return; }
                ClearGridView();
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
        #endregion

        #region Rooting for Check Customer
        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomer.Text)) return;
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_masterBusinessCompany == null || string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCustomer.Clear(); txtCustomer.Focus(); return; }
                ClearGridView();
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
        #endregion

        #region Rooting for Check Scheme
        private void txtSchemeCD_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSchemeCD.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetSchemes("CODE", txtSchemeCD.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                { MessageBox.Show("Please select a valid scheme code", "Scheme Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSchemeCD.Clear(); txtSchemeCD.Focus(); return; }
                ClearGridView();
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
        #endregion

        private static DataTable SampleData(string _name, string _secondValue)
        {

            DataTable sampleDataTable = new DataTable(_name);

            sampleDataTable.Columns.Add("FirstColumn", typeof(string));
            sampleDataTable.Columns.Add("SecondColumn", typeof(string));
            DataRow sampleDataRow;
            for (int i = 1; i <= 49; i++)
            {
                sampleDataRow = sampleDataTable.NewRow();
                sampleDataRow["FirstColumn"] = "Cell1: " + i.ToString(CultureInfo.CurrentCulture);
                sampleDataRow["SecondColumn"] = _secondValue + ": " + i.ToString(CultureInfo.CurrentCulture);
                sampleDataTable.Rows.Add(sampleDataRow);
            }

            return sampleDataTable;
        }

        //protected void Search()
        //{
        //    if (txtPriceBook.Text == "" && txtLevel.Text == "" && txtItem.Text == "")
        //    {
        //        MessageBox.Show("Please enter item code or price book or level", "Critical Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    #region search

        //    string entered_PriceBook = null;
        //    if (txtPriceBook.Text.Trim() != "")
        //        entered_PriceBook = txtPriceBook.Text.Trim();

        //    string entered_Level = null;
        //    if (txtLevel.Text.Trim() != "")
        //        entered_Level = txtLevel.Text.Trim();

        //    string entered_Circular = null;
        //    if (txtCircular.Text.Trim() != "")
        //        entered_Circular = txtCircular.Text.Trim();

        //    string entered_ItemCD = txtItem.Text.Trim();
        //    if (txtItem.Text.Trim() != "")
        //        entered_ItemCD = txtItem.Text.Trim();

        //    string entered_Customer = null;
        //    if (txtCustomer.Text.Trim() != "")
        //        entered_Customer = txtCustomer.Text.Trim();


        //    string entered_Type = null;
        //    if (txtType.Text.Trim() != "")
        //        entered_Type = txtType.Text.Trim();



        //    Int32 _Type = -1;




        //    if (txtType.Text.Trim() != "")
        //        try
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
        //            DataTable _result = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(SearchParams, null, null);
        //            var _no = _result.AsEnumerable().Where(x => x.Field<string>("TYPECODE") == txtType.Text.Trim()).Select(y => y.Field<Int16>("SEQ")).ToList();
        //            if (_no != null)
        //                if (_no.Count > 0)
        //                    _Type = Convert.ToInt32(_no[0].ToString());
        //        }
        //        catch
        //        {
        //            CHNLSVC.CloseChannel(); 
        //            MessageBox.Show("Please enter valid Type", "Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }


        //    string entered_cate1 = null;
        //    if (txtCate1.Text.Trim() != "")
        //        entered_cate1 = txtCate1.Text.Trim();

        //    string entered_cate2 = null;
        //    if (txtCate2.Text.Trim() != "")
        //        entered_cate2 = txtCate2.Text.Trim();

        //    string entered_cate3 = null;
        //    if (txtCate3.Text.Trim() != "")
        //        entered_cate3 = txtCate3.Text.Trim();

        //    DateTime fdt = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
        //    DateTime tdt = Convert.ToDateTime(txtToDt.Text.Trim()).Date;

        //    try
        //    {
        //        DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, entered_PriceBook, entered_Level, entered_ItemCD, entered_Customer, entered_Type, fdt, tdt, entered_Circular, entered_cate1, entered_cate2, entered_cate3, _Type);
        //        //DataTable _promo = ShowCombineItems();

        //        //DataTable _t1 = SampleData("tbl1", "tbl1");
        //        //DataTable _t2 = SampleData("tbl2", "tbl2");


        //        //DataSet dsResults = new DataSet("Results");
        //        //dsResults.Tables.Add(_t1);
        //        //dsResults.Tables.Add(_t2);


        //        //DataRelation _rel = new DataRelation("rel1", _t1.Columns[0], _t2.Columns[0]);
        //        //dsResults.Relations.Add(_rel);

        //        BindingSource _source = new BindingSource();
        //        _source.DataSource = EnqTbl;
        //        grvPriceDetail.DataSource = _source;
        //    }
        //    catch
        //    {
        //        CHNLSVC.CloseChannel(); 
        //        MessageBox.Show("Please enter another searching parameter.", "Searching...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //CHNLSVC.CloseChannel(); 
        //        return;
        //    }
        //    #endregion
        //}
        //protected void Search_AsAt()
        //{
        //    if (txtPriceBook.Text == "" && txtLevel.Text == "" && txtItem.Text == "")
        //    {
        //        CHNLSVC.CloseChannel(); 
        //        MessageBox.Show("Enter item or price book or level", "Critical Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    #region search
        //    string entered_PriceBook = null;
        //    if (txtPriceBook.Text.Trim() != "")
        //        entered_PriceBook = txtPriceBook.Text.Trim();

        //    string entered_Level = null;
        //    if (txtLevel.Text.Trim() != "")
        //        entered_Level = txtLevel.Text.Trim();

        //    string entered_Circular = null;
        //    if (txtCircular.Text.Trim() != "")
        //        entered_Circular = txtCircular.Text.Trim();

        //    string entered_ItemCD = txtItem.Text.Trim();
        //    if (txtItem.Text.Trim() != "")
        //        entered_ItemCD = txtItem.Text.Trim();

        //    string entered_Customer = null;
        //    if (txtCustomer.Text.Trim() != "")
        //        entered_Customer = txtCustomer.Text.Trim();


        //    string entered_Type = null;
        //    if (txtType.Text.Trim() != "")
        //        entered_Type = txtType.Text.Trim();

        //    Int32 _Type = -1;
        //    if (txtType.Text.Trim() != "")
        //        _Type = Convert.ToInt32(txtType.Text.Trim());

        //    string entered_cate1 = null;
        //    if (txtCate1.Text.Trim() != "")
        //        entered_cate1 = txtCate1.Text.Trim();

        //    string entered_cate2 = null;
        //    if (txtCate2.Text.Trim() != "")
        //        entered_cate2 = txtCate2.Text.Trim();

        //    string entered_cate3 = null;
        //    if (txtCate3.Text.Trim() != "")
        //        entered_cate3 = txtCate3.Text.Trim();


        //    DateTime asatDate = Convert.ToDateTime(txtAsAtDt.Text.Trim()).Date;

        //    try
        //    {
        //        //EnquirePriceDetails_forAsAtDate
        //        DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails_forAsAtDate(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, entered_PriceBook, entered_Level, entered_ItemCD, entered_Customer, entered_Type, asatDate, entered_Circular, entered_cate1, entered_cate2, entered_cate3, _Type);

        //        BindingSource _source = new BindingSource();
        //        _source.DataSource = EnqTbl;
        //        grvPriceDetail.DataSource = _source;
        //    }
        //    catch (Exception ex)
        //    {
        //        CHNLSVC.CloseChannel(); 
        //        MessageBox.Show("Enter another searching parameter!", "Other Parameter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }

        //    #endregion
        //}

        private void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == tabPage4.Name)
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text)) { MessageBox.Show("Please select the price book.", "Book", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtLevel.Text)) { MessageBox.Show("Please select the price level.", "Level", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtItem.Text)) { MessageBox.Show("Please select the item.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (IsSuperUser == false) txtPc.Text = BaseCls.GlbUserDefProf;
                if (string.IsNullOrEmpty(txtPc.Text)) { MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                Int32 _isPromotionBase = chkIsPromotionBase.Checked ? 1 : 0;
                Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                try
                {
                    List<CashPromotionDiscountDetail> _table = new List<CashPromotionDiscountDetail>();

                    if (!chkCancelDis.Checked)
                        _table = CHNLSVC.Sales.GetPromotionalDiscount(txtAsAtDt.Value.Date, _timeno, Convert.ToDateTime(txtAsAtDt.Text.Trim()).DayOfWeek.ToString().ToUpper(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtItem.Text.Trim(), BaseCls.GlbUserComCode, txtPc.Text.Trim(), _isPromotionBase, IsSuperUser ? 1 : 0);
                    else if (chkCancelDis.Checked && IsSuperUser)
                        _table = CHNLSVC.Sales.GetPromotionalDiscountCacnel(txtAsAtDt.Value.Date, _timeno, Convert.ToDateTime(txtAsAtDt.Text.Trim()).DayOfWeek.ToString().ToUpper(), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtItem.Text.Trim(), BaseCls.GlbUserComCode, txtPc.Text.Trim(), _isPromotionBase, IsSuperUser ? 1 : 0);

                    if (_table == null || _table.Count <= 0)
                        MessageBox.Show("There is no promotion discount found for the selected criteria!", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _table.Where(X => X.Spdd_alw_cc_pro).ToList().ForEach(X => X.Spdd_cre_by = "Yes");
                    _table.Where(X => X.Spdd_alw_cc_pro == false).ToList().ForEach(X => X.Spdd_cre_by = "No");
                    _table.Where(x => x.Spdd_stus == 1).ToList().ForEach(x => x.Spdd_mod_by = "Active");
                    _table.Where(x => x.Spdd_stus == 0).ToList().ForEach(x => x.Spdd_mod_by = "Inactive");
                    _table.Where(x => x.Spdi_act).ToList().ForEach(x => x.Spdi_mod_by = "Active");
                    _table.Where(x => x.Spdi_act == false).ToList().ForEach(x => x.Spdi_mod_by = "Inactive");
                    _table = _table.OrderByDescending(x => x.Spdd_seq).ToList();     //kapila 28/3/2017
                    gvPromotionDiscount.DataSource = _table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    CHNLSVC.CloseChannel();
                    this.Cursor = Cursors.Default;
                }
                return;
            }

            if (tabControl1.SelectedTab.Name != tabPage5.Name)
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                if (!IsSuperUser && string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("Please enter price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (chkScheme.Checked)
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MessageBox.Show("You need to select the item if you need to search for schemes!", "Schemes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            this.Cursor = Cursors.WaitCursor;
            DateTime _fromdate = new DateTime();
            DateTime _todate = new DateTime();
            bool _isHistory = false;
            bool _isAsAtHistory = false;
            string _itemStatus = string.Empty;
            string _taxStrucCode = string.Empty;
            _itemStatus = cmbStatus.Text == "GOOD" ? "GOD" : "GDLP";

            if (rdoDateRange.Checked)
            {
                _fromdate = txtFromDt.Value.Date;
                _todate = txtToDt.Value.Date;
                _isHistory = true;
            }
            else if (rdoAsAt.Checked)
            {
                _fromdate = txtAsAtDt.Value.Date;
                _todate = txtAsAtDt.Value.Date;
                _isHistory = false;
                if (chkWithHistory.Checked)
                    _isAsAtHistory = true;
                else
                    _isAsAtHistory = false;
            }

            try
            {
                if (sender != null)
                {
                    CurrentPageSize = 0;
                    txtPage.Text = "1";
                }

                #region Get Prices
                if (tabControl1.SelectedTab.Name == tabPage1.Name)
                {
                    if (!string.IsNullOrEmpty(txtItem.Text))
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());

                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isStrucBaseTax == true)       //kapila 21/9/2015
                        {

                            _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), null, string.Empty, string.Empty, _mstItem.Mi_anal1);
                            _taxStrucCode = _mstItem.Mi_anal1;
                        }
                        else
                        {
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), null, string.Empty, string.Empty);
                            _taxStrucCode = null;
                        }


                        //  DataTable _tax = CHNLSVC.Inventory.GetItemTaxData(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                        if (_taxs.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Tax definitions not setup.Please contact inventory dept.", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtItem.Focus();
                            return;
                        }
                    }
                    List<PriceDetailRef> _ref = CHNLSVC.Sales.GetPriceEnquiryDetailNew(txtPc.Text.Trim(), CurrentPageSize, CurrentPageSize + PageSize, BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text.Trim(), chkAllStatus.Checked ? string.Empty : _itemStatus, txtType.Text.Trim(), txtCircular.Text.Trim(), _fromdate.Date, _todate.Date, _isHistory, _isAsAtHistory, chkAllStatus.Checked, IsSuperUser, txtPromotion.Text.Trim(), _taxStrucCode);
                    if (_ref == null || _ref.Count <= 0)
                        MessageBox.Show("There is no price found for the selected criteria!", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);





                    // Nadeeka 17-10-2015 
                    foreach (PriceDetailRef j in _ref)
                    {
                        string expression;
                        decimal _tax = 0;
                        //_tax = Convert.ToDecimal("12.5");
                        _tax = Convert.ToDecimal("11");

                        //kapila 20/7/2016
                        HpSystemParameters _SystemPara = new HpSystemParameters();
                        _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", "ALL", "WREXTXRT", DateTime.Now.Date);

                        if (_SystemPara.Hsy_cd != null)
                        {
                            _tax = _SystemPara.Hsy_val;
                        }

                        expression = "sapl_is_sales = 0";
                        DataRow[] foundRows;

                        DataTable _extPrice = CHNLSVC.Sales.GetPriceLevelTable(BaseCls.GlbUserComCode, j.Sapd_pb_tp_cd, j.Sapd_pbk_lvl_cd);
                        foundRows = _extPrice.Select(expression);

                        if (foundRows.Length > 0)
                        {
                            j.Sapd_with_tax = j.Sapd_itm_price + (j.Sapd_itm_price * _tax / 100);
                        }

                    }


                    BindingSource _source1 = new BindingSource();
                    DataTable _tbl = _ref.ToDataTable();
                    _source1.DataSource = _tbl;
                    grvPriceDetail.DataSource = _source1;
                    this.Cursor = Cursors.Default;
                }
                #endregion

                #region Get Serialized Prices
                if (tabControl1.SelectedTab.Name == tabPage3.Name)
                {
                    List<PriceSerialRef> _ref1 = new List<PriceSerialRef>();
                    if (checkwithcom.Checked == true) //Add by tharanga 2017/08/09
                    {
                        _ref1 = CHNLSVC.Sales.GetEnquirySerialDetailwithLoc(txtPc.Text.Trim(), CurrentPageSize, CurrentPageSize + PageSize, BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text.Trim(), chkAllStatus.Checked ? string.Empty : _itemStatus, txtType.Text.Trim(), txtCircular.Text.Trim(), _fromdate.Date, _todate.Date, _isHistory, _isAsAtHistory, true, IsSuperUser,null);
                    }
                    else
                    {
                        _ref1 = CHNLSVC.Sales.GetEnquirySerialDetailwithLoc(txtPc.Text.Trim(), CurrentPageSize, CurrentPageSize + PageSize, BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text.Trim(), chkAllStatus.Checked ? string.Empty : _itemStatus, txtType.Text.Trim(), txtCircular.Text.Trim(), _fromdate.Date, _todate.Date, _isHistory, _isAsAtHistory, true, IsSuperUser, BaseCls.GlbUserDefLoca);
                    }

                    if (!string.IsNullOrEmpty(txtitmser.Text.Trim().ToString())) //Add by tharanga 2017/08/10
                    {
                        string ser = txtitmser.Text.Trim().ToString();
                        _ref1 = _ref1.Where(p => p.Sars_ser_no.Contains(ser)).ToList();
                    }

                        if (_ref1 == null || _ref1.Count <= 0)
                        MessageBox.Show("There is no serialized price found for the selected criteria!", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindingSource _source2 = new BindingSource();
                    DataTable _tbl1 = _ref1.ToDataTable();
                    _source2.DataSource = _tbl1;
                    gvPriceSerial.DataSource = _source2;
                    this.Cursor = Cursors.Default;
                }
                #endregion

                #region Get Scheme
                if (tabControl1.SelectedTab.Name == tabPage2.Name)
                {
                    if (chkScheme.Checked)
                    {
                        if (txtItem.Text == "")
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please enter item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtItem.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(txtPc.Text))
                        {
                            MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPc.Clear();
                            txtPc.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(txtPriceBook.Text)) { MessageBox.Show("Please select the price book", "Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPriceBook.Clear(); txtPriceBook.Focus(); return; }
                        if (string.IsNullOrEmpty(txtLevel.Text)) { MessageBox.Show("Please select the price level", "Level", MessageBoxButtons.OK, MessageBoxIcon.Information); txtLevel.Clear(); txtLevel.Focus(); return; }


                        try { Convert.ToDateTime(txtAsAtDt.Text); }
                        catch (Exception ex)
                        {
                            this.Cursor = Cursors.Default;
                            CHNLSVC.CloseChannel();
                            MessageBox.Show("Please enter an as at date", "As At Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        string entered_schemeCD = null;
                        if (txtSchemeCD.Text.Trim() != "")
                            entered_schemeCD = txtSchemeCD.Text.Trim();

                        string entered_PriceBook = null;
                        if (txtPriceBook.Text.Trim() != "")
                            entered_PriceBook = txtPriceBook.Text.Trim();

                        string entered_Level = null;
                        if (txtLevel.Text.Trim() != "")
                            entered_Level = txtLevel.Text.Trim();

                        DataTable pcHerachyTB = new DataTable();
                        List<HpSchemeDefinition> Final_schemaList = new List<HpSchemeDefinition>();

                        pcHerachyTB = CHNLSVC.Sales.Get_hpHierachy(txtPc.Text.Trim());

                        if (pcHerachyTB.Rows.Count > 0)
                        {
                            MasterItem mstItm = new MasterItem();
                            mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                            string _item = mstItm.Mi_cd;
                            string _brand = mstItm.Mi_brand;
                            string _mainCat = mstItm.Mi_cate_1;
                            string _subCat = mstItm.Mi_cate_2;
                            foreach (DataRow pcH in pcHerachyTB.Rows)
                            {
                                string party_tp = Convert.ToString(pcH["MPI_CD"]);
                                string party_cd = Convert.ToString(pcH["MPI_VAL"]);
                                //List<HpSchemeDefinition> schemsList = new List<HpSchemeDefinition>();
                                //schemsList = CHNLSVC.Sales.get_HP_Schemes(Convert.ToDateTime(txtAsAtDt.Text), txtItem.Text.Trim(), party_tp, party_cd, mstItm.Mi_brand, mstItm.Mi_cate_1, mstItm.Mi_cate_2, entered_schemeCD, entered_PriceBook, entered_Level);
                                //Final_schemaList.AddRange(schemsList);


                                //get details from item
                                List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, _item, null, null, null, null, null);
                                if (_def != null)
                                {
                                    Final_schemaList.AddRange(_def);
                                }

                                //get details according to main category
                                List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, _brand, _mainCat, null, null, null);
                                if (_def1 != null)
                                {
                                    Final_schemaList.AddRange(_def1);
                                }

                                //get details according to sub category
                                List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, _brand, null, _subCat, null, null);
                                if (_def2 != null)
                                {
                                    Final_schemaList.AddRange(_def2);
                                }

                                //get details according to price book and level
                                List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, null, null, null, null, null);
                                if (_def3 != null)
                                {
                                    Final_schemaList.AddRange(_def3);
                                }
                            }
                        }

                        if (txtPriceBook.Text.Trim() != "")
                            Final_schemaList.RemoveAll(x => x.Hpc_pb != txtPriceBook.Text.Trim());

                        if (txtLevel.Text.Trim() != "")
                            Final_schemaList.RemoveAll(x => x.Hpc_pb_lvl != txtLevel.Text.Trim());

                        if (txtCircular.Text.Trim() != "")
                            Final_schemaList.RemoveAll(x => x.Hpc_price_cir_no != txtCircular.Text.Trim());


                        var _record = (from _lst in Final_schemaList
                                       where _lst.Hpc_is_alw == false
                                       select _lst).ToList().Distinct();

                        foreach (HpSchemeDefinition j in _record)
                        {
                            Final_schemaList.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd);
                        }
                        if (Final_schemaList == null || Final_schemaList.Count <= 0)
                            MessageBox.Show("There is no scheme found for the selected criteria!", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        BindingSource _source = new BindingSource();
                        _source.DataSource = Final_schemaList;
                        grvScheme.DataSource = _source;

                    }
                }
                #endregion

                #region Get Pay Types
                if (tabControl1.SelectedTab.Name == tabPage5.Name)
                {

                    DataTable _tbl = CHNLSVC.Sales.TrPayTpDefEnquiry(BaseCls.GlbUserComCode, txtCircular.Text.Trim(), txtPromotion.Text.Trim(), null, rdoAsAt.Checked ? "Y" : "N", _fromdate.Date, _todate.Date, txtAsAtDt.Value.Date, null, txtPriceBook.Text, txtLevel.Text, txtCate1.Text, txtCate2.Text, txtPc.Text, txtItem.Text);
                    if (_tbl == null || _tbl.Rows.Count <= 0)
                        MessageBox.Show("There is no pay type definition for the selected criteria!", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindingSource _source1 = new BindingSource();
                    dgvPayType.AutoGenerateColumns = false;
                    _source1.DataSource = _tbl;
                    dgvPayType.DataSource = _source1;
                    this.Cursor = Cursors.Default;
                }
                #endregion

            }
            catch (Exception ex)
            {
                gvCombine.DataSource = null;
                gvPriceSerial.DataSource = null;
                grvPriceDetail.DataSource = null;
                grvScheme.DataSource = null;
                CHNLSVC.CloseChannel();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PriceEnquiry _priceDetail = new PriceEnquiry();
            _priceDetail.MdiParent = this.MdiParent;
            _priceDetail.Location = this.Location;
            _priceDetail.Show();
            this.Close();
        }

        private void grvPriceDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                

                DataGridView _wall = sender as DataGridView;
               

                if (_wall.RowCount > 0)
                    if (e.RowIndex != -1)
                        if (e.ColumnIndex == 0)
                        {
                            string _item = string.Empty;
                            Int32 _pbseq = -1;
                            Int32 _pblineseq = -1;
                            Int32 _type = -1;
                            string _IsComStr = string.Empty;

                            string _book = string.Empty;
                            string _level = string.Empty;
                            string _customer = string.Empty;

                            if (_wall.Name == "grvPriceDetail")
                            {
                                _item = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_item"].Value);
                                _pbseq = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_pbseq"].Value);
                                _pblineseq = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_pblineseq"].Value);
                                _type = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_typeseq"].Value);
                                _IsComStr = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_iscom"].Value);

                                _book = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_book"].Value);
                                _level = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_level"].Value);
                                _customer = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_activeFor"].Value);
                                com_serial.Visible = false;
                            }
                            else if (_wall.Name == "gvPriceSerial")
                            {
                                _item = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_item"].Value);
                                _pbseq = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["prs_pbseq"].Value);
                                _pblineseq = 1;// Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_pblineseq"].Value);
                                _type = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["prs_prtype"].Value);
                                _IsComStr = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_iscom"].Value);

                                _book = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_book"].Value);
                                _level = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_level"].Value);
                                _customer = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_customer"].Value);
                                com_serial.Visible = true;
                            }

                            DateTime _effectDate = new DateTime();
                            if (rdoDateRange.Checked)
                            {
                                _effectDate = txtFromDt.Value;
                            }
                            if (rdoAsAt.Checked)
                            {
                                _effectDate = txtAsAtDt.Value;
                            }

                            Int32 _isCom = 0;

                            if (!string.IsNullOrEmpty(_IsComStr))
                                if (_wall.Name == "grvPriceDetail")
                                { _isCom = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_iscom"].Value); }
                                else if (_wall.Name == "gvPriceSerial")
                                { _isCom = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["prs_iscom"].Value); }
                            int row_id = e.RowIndex;

                            if (_type != 0)
                            {
                                BindingSource _source = new BindingSource();
                                List<PriceCombinedItemRef> _lst = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbseq, _pblineseq, _item, string.Empty);
                                if (_lst == null || _lst.Count <= 0)
                                { gvCombine.DataSource = _source; grvPriceDetail.Cursor = Cursors.Default; pnlCombine.Visible = false; return; }
                                pnlCombine.Visible = true;
                                _source.DataSource = _lst;
                                gvCombine.DataSource = _source;
                                _wall.Cursor = Cursors.Hand;
                            }
                        }
                //Pc
                if (e.ColumnIndex == 1)
                {
                    if (!IsSuperUser)
                    {
                        MessageBox.Show("You don't have permission to see the price allocation.", "Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    string _item = string.Empty;
                    Int32 _type = -1;
                    string _book = string.Empty;
                    string _level = string.Empty;
                    string _promotion = string.Empty;
                    if (tabControl1.SelectedTab.Name == tabPage3.Name)
                    {

                        _item = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_item"].Value);
                        _type = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["sapd_price_type"].Value);
                        _book = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_book"].Value);
                        _level = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_level"].Value);
                        _promotion = Convert.ToString(_wall.Rows[e.RowIndex].Cells["sapd_promo_cd"].Value);
                    }
                    else
                    {
                    _item = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_item"].Value);
                    _type = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_typeseq"].Value);
                    _book = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_book"].Value);
                    _level = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_level"].Value);
                    _promotion = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_promotioncd"].Value);
                     }

                    gvPc.AutoGenerateColumns = false;
                    gvPc.DataSource = CHNLSVC.Sales.GetProfitCenterDetail(BaseCls.GlbUserComCode, _type, _book, _level, _promotion);
                    pnlPc.Visible = true;
                    pnlStatus.Visible = false;
                    pnlCombine.Visible = false;
                }
                //status
                if (e.ColumnIndex == 2)
                {
                    string _item = string.Empty;
                    Int32 _type = -1;
                    string _book = string.Empty;
                    string _level = string.Empty;
                    decimal _price = 0;
                    if (tabControl1.SelectedTab.Name == tabPage3.Name)
                    {

                        _item = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_item"].Value);
                        _type = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["sapd_price_type"].Value);
                        _book = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_book"].Value);
                        _level = Convert.ToString(_wall.Rows[e.RowIndex].Cells["prs_level"].Value);
                        _price = Convert.ToDecimal(_wall.Rows[e.RowIndex].Cells["prs_pricewovat"].Value);
                    }
                    else
                    {
                        _item = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_item"].Value);
                        _type = Convert.ToInt32(_wall.Rows[e.RowIndex].Cells["pr_typeseq"].Value);
                        _book = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_book"].Value);
                        _level = Convert.ToString(_wall.Rows[e.RowIndex].Cells["pr_level"].Value);
                        _price = Convert.ToDecimal(_wall.Rows[e.RowIndex].Cells["pr_vatExPrice"].Value);
                    }
                    DataTable _PriceStatus = CHNLSVC.Sales.GetPriceStatus(_item, _price, BaseCls.GlbUserComCode, _book, _level);
                    //gvStatus.AutoGenerateColumns = false;
                    gvStatus.Rows.Clear();

               //     DataTable warr_dt = CHNLSVC.Sales.GetItemStatusWiseWarrantyPeriods(_item, string.Empty);

                    if (_PriceStatus.Rows.Count > 0)
                    {
                        foreach (DataRow _row in _PriceStatus.Rows)
                        {
                            gvStatus.Rows.Add();
                            gvStatus["Column22", gvStatus.Rows.Count - 1].Value = _row["MICT_STUS"].ToString();
                            gvStatus["Column23", gvStatus.Rows.Count - 1].Value = _row["MICT_TAXRATE_CD"].ToString();
                            gvStatus["Column24", gvStatus.Rows.Count - 1].Value = _row["MICT_TAX_RATE"].ToString();
                            gvStatus["Column25", gvStatus.Rows.Count - 1].Value = _row["SAPD_WITH_TAX"].ToString();
                            gvStatus["Column28", gvStatus.Rows.Count - 1].Value = _row["MWP_RMK"].ToString();


                            if (Convert.ToInt16(_row["sapl_set_warr"]) == 1)
                            {
                                gvStatus["Column27", gvStatus.Rows.Count - 1].Value = _row["sapl_warr_period"].ToString();
                                gvStatus["Column28", gvStatus.Rows.Count - 1].Value = "N/A";//_row["MWP_RMK"].ToString();
                            }
                            else
                            {
                                gvStatus["Column27", gvStatus.Rows.Count - 1].Value = _row["MWP_VAL"].ToString();
                                gvStatus["Column28", gvStatus.Rows.Count - 1].Value = _row["MWP_RMK"].ToString();
                            }
                        }
                    }

                    //gvStatus.AutoGenerateColumns = false;
                    //gvStatus.DataSource = CHNLSVC.Sales.GetPriceStatus(_item, _price, BaseCls.GlbUserComCode, _book, _level);
                    pnlStatus.Visible = true;
                    pnlPc.Visible = false;
                    pnlCombine.Visible = false;
                }

                //Tharindu
                if (e.ColumnIndex == 3)
                {
                    #region Get Scheme
                   // if (tabControl1.SelectedTab.Name == tabPage2.Name)
                   // {
                      //  if (chkScheme.Checked)
                      //  {
                            //if (txtItem.Text == "")
                            //{
                            //    this.Cursor = Cursors.Default;
                            //    MessageBox.Show("Please enter item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtItem.Focus();
                            //    return;
                            //}

                            //if (string.IsNullOrEmpty(txtPc.Text))
                            //{
                            //    MessageBox.Show("Please select the profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    txtPc.Clear();
                            //    txtPc.Focus();
                            //    return;
                            //}

                            if (string.IsNullOrEmpty(txtPriceBook.Text)) { MessageBox.Show("Please select the price book", "Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPriceBook.Clear(); txtPriceBook.Focus(); return; }
                            if (string.IsNullOrEmpty(txtLevel.Text)) { MessageBox.Show("Please select the price level", "Level", MessageBoxButtons.OK, MessageBoxIcon.Information); txtLevel.Clear(); txtLevel.Focus(); return; }


                            try { Convert.ToDateTime(txtAsAtDt.Text); }
                            catch (Exception ex)
                            {
                                this.Cursor = Cursors.Default;
                                CHNLSVC.CloseChannel();
                                MessageBox.Show("Please enter an as at date", "As At Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            string entered_schemeCD = null;
                            if (txtSchemeCD.Text.Trim() != "")
                                entered_schemeCD = txtSchemeCD.Text.Trim();

                            string entered_PriceBook = null;
                            if (txtPriceBook.Text.Trim() != "")
                                entered_PriceBook = txtPriceBook.Text.Trim();

                            string entered_Level = null;
                            if (txtLevel.Text.Trim() != "")
                                entered_Level = txtLevel.Text.Trim();

                            DataTable pcHerachyTB = new DataTable();
                            List<HpSchemeDefinition> Final_schemaList = new List<HpSchemeDefinition>();

                            pcHerachyTB = CHNLSVC.Sales.Get_hpHierachy(txtPc.Text.Trim());

                            if (pcHerachyTB.Rows.Count > 0)
                            {
                                MasterItem mstItm = new MasterItem();
                                mstItm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                                string _item = mstItm.Mi_cd;
                                string _brand = mstItm.Mi_brand;
                                string _mainCat = mstItm.Mi_cate_1;
                                string _subCat = mstItm.Mi_cate_2;
                                foreach (DataRow pcH in pcHerachyTB.Rows)
                                {
                                    string party_tp = Convert.ToString(pcH["MPI_CD"]);
                                    string party_cd = Convert.ToString(pcH["MPI_VAL"]);
                                    //List<HpSchemeDefinition> schemsList = new List<HpSchemeDefinition>();
                                    //schemsList = CHNLSVC.Sales.get_HP_Schemes(Convert.ToDateTime(txtAsAtDt.Text), txtItem.Text.Trim(), party_tp, party_cd, mstItm.Mi_brand, mstItm.Mi_cate_1, mstItm.Mi_cate_2, entered_schemeCD, entered_PriceBook, entered_Level);
                                    //Final_schemaList.AddRange(schemsList);


                                    //get details from item
                                    List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, _item, null, null, null, null, null);
                                    if (_def != null)
                                    {
                                        Final_schemaList.AddRange(_def);
                                    }

                                    //get details according to main category
                                    List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, _brand, _mainCat, null, null, null);
                                    if (_def1 != null)
                                    {
                                        Final_schemaList.AddRange(_def1);
                                    }

                                    //get details according to sub category
                                    List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, _brand, null, _subCat, null, null);
                                    if (_def2 != null)
                                    {
                                        Final_schemaList.AddRange(_def2);
                                    }

                                    //get details according to price book and level
                                    List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(party_tp, party_cd, entered_PriceBook, entered_Level, Convert.ToDateTime(txtAsAtDt.Text).Date, null, null, null, null, null, null);
                                    if (_def3 != null)
                                    {
                                        Final_schemaList.AddRange(_def3);
                                    }
                                }
                            }

                            if (txtPriceBook.Text.Trim() != "")
                                Final_schemaList.RemoveAll(x => x.Hpc_pb != txtPriceBook.Text.Trim());

                            if (txtLevel.Text.Trim() != "")
                                Final_schemaList.RemoveAll(x => x.Hpc_pb_lvl != txtLevel.Text.Trim());

                            if (txtCircular.Text.Trim() != "")
                                Final_schemaList.RemoveAll(x => x.Hpc_price_cir_no != txtCircular.Text.Trim());


                            var _record = (from _lst in Final_schemaList
                                           where _lst.Hpc_is_alw == false
                                           select _lst).ToList().Distinct();

                            foreach (HpSchemeDefinition j in _record)
                            {
                                Final_schemaList.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd);
                            }
                            if (Final_schemaList == null || Final_schemaList.Count <= 0)
                                MessageBox.Show("There is no scheme found for the selected criteria!", "Price Enquiry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            BindingSource _source = new BindingSource();
                            _source.DataSource = Final_schemaList; 
                         //   dgvPricedet.AutoGenerateColumns = false;
                            dgvPricedet.DataSource = _source;
                            panel6.Visible = true;
                           

                    

                       // }
                  //  }
                    #endregion

                }

            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void pnlCombine_Click(object sender, EventArgs e)
        {
            grvPriceDetail.Cursor = Cursors.Default;
            pnlCombine.Visible = false;
            grvPriceDetail.Refresh();
        }

        private void gvCombine_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            grvPriceDetail.Cursor = Cursors.Default;
            pnlCombine.Visible = false;
            grvPriceDetail.Refresh();
        }

        private void gvCombine_Click(object sender, EventArgs e)
        {
            grvPriceDetail.Cursor = Cursors.Default;
            pnlCombine.Visible = false;
            grvPriceDetail.Refresh();
        }

        private void PriceDetail_Shown(object sender, EventArgs e)
        {
            txtToDt.Value = txtToDt.MaxDate.Date;
        }

        private void chkScheme_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ClearGridView();
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
            pnlCombine.Visible = false;
            gvCombine.DataSource = null;
            if (MessageBox.Show("Do you need to search again?", "Search " + tabControl1.SelectedTab.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                btnGetDetail_Click(sender, null);
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void chkAllStatus_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void rdoDateRange_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void txtFromDt_Leave(object sender, EventArgs e)
        {
            ClearGridView();
            if (!IsSuperUser && txtFromDt.Value.Date > DateTime.Now.Date)
            { MessageBox.Show("You can not see future prices.", "Price", MessageBoxButtons.OK, MessageBoxIcon.Information); txtFromDt.Value = DateTime.Now.Date; return; }
        }

        private void txtToDt_Leave(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void rdoAsAt_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void txtAsAtDt_Leave(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void chkWithHistory_CheckedChanged(object sender, EventArgs e)
        {
            ClearGridView();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPageSize == 0) return;
            if (CurrentPageSize / PageSize == 1) CurrentPageSize = 0;
            else CurrentPageSize = CurrentPageSize - PageSize;
            btnGetDetail_Click(null, null);
            int _page = 0;
            if (CurrentPageSize == 0)
                _page = 1;
            else
                _page = CurrentPageSize / PageSize + 1;
            txtPage.Text = _page.ToString();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab.Name == tabPage1.Name)
                    if (grvPriceDetail.Rows.Count <= 0) return;

                if (tabControl1.SelectedTab.Name == tabPage3.Name)
                    if (gvPriceSerial.Rows.Count <= 0) return;

                if (tabControl1.SelectedTab.Name == tabPage2.Name)
                    if (grvScheme.Rows.Count <= 0) return;


                CurrentPageSize = CurrentPageSize + PageSize;
                btnGetDetail_Click(null, null);
                int _page = 0;

                if (tabControl1.SelectedTab.Name == tabPage1.Name)
                    if (grvPriceDetail.Rows.Count <= 0) { CurrentPageSize = CurrentPageSize - PageSize; btnGetDetail_Click(null, null); }

                if (tabControl1.SelectedTab.Name == tabPage3.Name)
                    if (gvPriceSerial.Rows.Count <= 0) { CurrentPageSize = CurrentPageSize - PageSize; btnGetDetail_Click(null, null); }

                if (tabControl1.SelectedTab.Name == tabPage2.Name)
                    if (grvScheme.Rows.Count <= 0) { CurrentPageSize = CurrentPageSize - PageSize; btnGetDetail_Click(null, null); }



                _page = CurrentPageSize / PageSize + 1;
                txtPage.Text = _page.ToString();
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

        private void btnSearch_Pc_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSuperUser == false) { txtPc.Clear(); return; }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPc;
                _CommonSearch.ShowDialog();
                txtPc.Select();
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

        private void txtPc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Pc_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtPriceBook.Focus();
        }

        private void txtPc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (IsSuperUser == false) { txtPc.Clear(); return; }

                if (string.IsNullOrEmpty(txtPc.Text)) return;
                MasterProfitCenter _pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, txtPc.Text.Trim());
                if (_pc == null || string.IsNullOrEmpty(_pc.Mpc_com))
                {
                    MessageBox.Show("Please select the valid profit center", "Invalid Profir Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPc.Clear();
                    txtPc.Focus();
                    return;
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
        private void txtPc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Pc_Click(null, null);
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

        private void LoadBalance()
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                gvBalance.DataSource = null;
                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0) { MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                gvBalance.DataSource = _inventoryLocation;
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
        private void chkWithInv_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (pnlBalance.Visible) pnlBalance.Visible = false;
                else
                {
                    pnlBalance.Visible = true;
                    LoadBalance();
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

        private void chkDiscountCal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDiscountCal.Checked) pnlDiscountCal.Visible = true;
            else pnlDiscountCal.Visible = false;
            txtDisRate.Clear();
            txtQty.Clear();
            radNormal.Checked = true;
            lblDisVal.Text = "0.00";
            lblTotalValue.Text = "Value";
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction, bool _isVAT)
        {
            try
            {

                bool _isVATInvoice = false;
                if (_isVAT) _isVATInvoice = true;
                else _isVATInvoice = false;

                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                if (_isTaxfaction == false)
                    if (_isStrucBaseTax == true)       //kapila  22/4/2016
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                else
                    if (_isStrucBaseTax == true)       //kapila  22/4/2016
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                var _Tax = from _itm in _taxs
                           select _itm;
                foreach (MasterItemTax _one in _Tax)
                {

                    if (_isTaxfaction == false)
                        _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                    else
                        if (_isVATInvoice)
                        {
                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                        }
                        else
                            _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;

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

            return _pbUnitPrice;
        }
        private void CalculateItem(string _item, string _status, decimal _unitprice, decimal _qty, decimal _disAmount, decimal _disRate, bool _isTaxInvoice, out string _invoiceamount, out string _discountamount)
        {
            decimal _unitAmount = _unitprice * _qty;

            decimal _vatPortion = TaxCalculation(_item, _status, _qty, null, _unitprice, _disAmount, _disRate, true, _isTaxInvoice);
            decimal _taxAmount = _vatPortion;

            decimal _totalAmount = _qty * _unitprice;
            decimal _disAmt = 0;

            if (!string.IsNullOrEmpty(txtDisRate.Text))
            {
                bool _isVATInvoice = false;
                if (_isTaxInvoice) _isVATInvoice = true;
                else _isVATInvoice = false;

                if (_isVATInvoice)
                    _disAmt = _totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100);
                else
                    _disAmt = (_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100);
            }
            _totalAmount = _totalAmount + _taxAmount - _disAmt;

            _invoiceamount = FormatToCurrency(Convert.ToString(_totalAmount));
            _discountamount = FormatToCurrency(Convert.ToString(_disAmt));
        }


        private void lblTotalValue_Click(object sender, EventArgs e)
        {
            lblTotalValue.Text = string.Empty;
            lblDisVal.Text = "0.00";
            if (string.IsNullOrEmpty(txtDisRate.Text))
            {            // Summer1021
                MessageBox.Show("Please select the discount rate.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsNumeric(txtDisRate.Text) == false)
            {
                MessageBox.Show("Please select the valid discount rate", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (IsNumeric(txtQty.Text) == false)
            {
                MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (grvPriceDetail.Rows.Count <= 0)
            {
                MessageBox.Show("Please select the item from the price detail table", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string _invoiceamount = string.Empty;
            string _discountamount = string.Empty;
            string _item = Convert.ToString(grvPriceDetail.SelectedRows[0].Cells["pr_item"].Value);
            string _status = "GOD";//Convert.ToString(grvPriceDetail.SelectedRows[0].Cells["pr_itemstatus"].Value);
            decimal _unitprice = Convert.ToDecimal(grvPriceDetail.SelectedRows[0].Cells["pr_vatExPrice"].Value);
            decimal _qty = Convert.ToDecimal(txtQty.Text);
            decimal _disAmt = 0;
            decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
            bool _taxInvoice = radTaxInvoice.Checked ? true : radNormal.Checked ? false : true;

            CalculateItem(_item, _status, _unitprice, _qty, _disAmt, _disRate, _taxInvoice, out _invoiceamount, out _discountamount);

            lblTotalValue.Text = _invoiceamount;
            lblDisVal.Text = _discountamount;
        }

        private void btnClosePC_Click(object sender, EventArgs e)
        {
            pnlPc.Visible = false;
            gvPc.DataSource = new DataTable();
        }

        private void btnCloseStatus_Click(object sender, EventArgs e)
        {
            pnlStatus.Visible = false;
            //gvStatus.DataSource = new DataTable();
        }

        private void chkCancelDis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCancelDis.Checked)
                if (!IsSuperUser)
                {
                    MessageBox.Show("You are not an authorized for enquire the cancel discount promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkCancelDis.Checked = false;
                }
        }

        private void txtitmser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate1.Focus();
                btnGetDetail_Click(null, null);
            }
        }

        private void PriceEnquiry_Load(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16103))
            {

                grvScheme.Columns[4].Visible = false;
                grvScheme.Columns[5].Visible = false;
            }
            else
            {
                grvScheme.Columns[4].Visible = true;
                grvScheme.Columns[5].Visible = true;
            }


        }

        private void btnpricegvcls_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

    }
}
