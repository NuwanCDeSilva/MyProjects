using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.General
{
    public partial class CashDiscountDefinition : Base
    {

        #region public variables

        List<PriceBookLevelRef> PBList;
        List<PaymentTypeRef> PayType;
        List<CashCommissionDetailRef> ItemBrandCat_List;
        List<MasterInvoiceType> SalesType;
        DataTable PCList = null;
        private List<string> _itemLst = null;

        #endregion

        public CashDiscountDefinition()
        {
            InitializeComponent();
            PBList = new List<PriceBookLevelRef>();
            PayType = new List<PaymentTypeRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            SalesType = new List<MasterInvoiceType>();
            PCList = new DataTable();
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
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

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSer4Itm:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtItemCD.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loyalty_Type:
                    {
                        paramsText.Append(BaseCls.GlbUserDefProf + seperator + DateTime.Now.ToString("dd/MMM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionalCircular:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DiscountCircularPending:
                    {
                        paramsText.Append(seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                if (chkAppPendings.Checked)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DiscountCircularPending);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionalCircular);
                }
                DataTable _resultTemp = CHNLSVC.CommonSearch.SearchPromotinalCircular(_CommonSearch.SearchParams, null, null);

                DataTable _result = _resultTemp.Clone();
                if (chkAppPendings.Checked)
                {
                    _result = _resultTemp.Select("STATUS = '2'").CopyToDataTable();
                }
                else
                {
                    _result.Merge(_resultTemp);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.txtSearchbyword.Text = txtCircular.Text;
                _CommonSearch.ShowDialog();
                txtCircular.Focus();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchPriceLvl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLevel;
                _CommonSearch.txtSearchbyword.Text = txtLevel.Text;
                _CommonSearch.ShowDialog();
                txtLevel.Focus();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtItemCD;
            //_CommonSearch.txtSearchbyword.Text = txtItemCD.Text;
            //_CommonSearch.ShowDialog();
            //txtItemCD.Focus();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnSerial_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
            //DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtSerial;
            //_CommonSearch.txtSearchbyword.Text = txtSerial.Text;
            //_CommonSearch.ShowDialog();
            //txtSerial.Focus();
            try
            {
                if (string.IsNullOrEmpty(txtItemCD.Text))
                {
                    MessageBox.Show("Please select item code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCD.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSer4Itm);
                DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Item(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.ShowDialog();
                txtSerial.Select();
                txtSerial.Select();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dataGridViewPriceBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



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
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtHierchCode.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    //}
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
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtHierchCode.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    //}
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtHierchCode.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    //}
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtHierchCode.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    //}
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
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    //_result = CHNLSVC.General.Get_GET_GPC("", "");
                    //if (txtHierchCode.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
                    //}
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PCList.Merge(_result);
                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = PCList;
                // chkPCAll.Checked = true;
                //AllClick();
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

        private void CashDiscountDefinition_Load(object sender, EventArgs e)
        {
            try
            {
                //data bindings
                BindPartyType();
                BindPayType();
                BindCategoryTypes();
                BindSalesTypes();
                BindPartyTypeEdit();
                BindCategoryTypesEdit();

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11051))
                {
                    btnUpdate.Enabled = true;
                }
                else
                {
                    btnUpdate.Enabled = false;
                }
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10088))
                {
                    btnApprove.Enabled = true;
                }
                else
                {
                    btnApprove.Enabled = false;
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



        private void btnAll_Hirchy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvParty.EndEdit();
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

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;

        }

        private void btnNon_Hierachy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearHirchy_Click(null, null);
            //this.btnAddPartys_Click(null, null);
            PCList = new DataTable();
        }

        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
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

        public void BindPartyTypeEdit()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            cmbEditBussinssHir.DataSource = new BindingSource(PartyTypes, null);
            cmbEditBussinssHir.DisplayMember = "Value";
            cmbEditBussinssHir.ValueMember = "Key";
        }

        private void BindPayType()
        {
            cmbPayType.DataSource = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            cmbPayType.DisplayMember = "SAPT_DESC";
            cmbPayType.ValueMember = "SAPT_CD";
        }

        private void BindCategoryTypes()
        {

            Dictionary<int, string> categoryType = new Dictionary<int, string>();
            categoryType.Add(1, "Serial");
            categoryType.Add(2, "Promotion");
            categoryType.Add(3, "Item");
            categoryType.Add(4, "Brand & Sub cat");
            categoryType.Add(5, "Brand & Cat");
            categoryType.Add(6, "Brand & main cat");
            categoryType.Add(7, "Brand");
            categoryType.Add(8, "Sub cat");
            categoryType.Add(9, "Cat");
            categoryType.Add(10, "Main cat");


            cmbSelectCat.DataSource = new BindingSource(categoryType, null);
            cmbSelectCat.DisplayMember = "Value";
            cmbSelectCat.ValueMember = "Key";

        }

        private void BindCategoryTypesEdit()
        {

            Dictionary<int, string> categoryType = new Dictionary<int, string>();
            //categoryType.Add(1, "Serial");
            //categoryType.Add(2, "Promotion");
            categoryType.Add(3, "Item");
            categoryType.Add(4, "Brand & Sub cat");
            categoryType.Add(5, "Brand & Cat");
            categoryType.Add(6, "Brand & main cat");
            categoryType.Add(7, "Brand");
            categoryType.Add(8, "Sub cat");
            categoryType.Add(9, "Cat");
            categoryType.Add(10, "Main cat");


            cmbEditItemType.DataSource = new BindingSource(categoryType, null);
            cmbEditItemType.DisplayMember = "Value";
            cmbEditItemType.ValueMember = "Key";

        }

        private void btnSalesTypeAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (cmbPayType.SelectedValue != null) ? cmbPayType.SelectedValue.ToString() : null;

                List<PaymentTypeRef> _payMode = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, code);
                foreach (PaymentTypeRef pay in _payMode)
                {
                    List<PaymentTypeRef> _duplicat = PayType.Where(x => x.Sapt_cd == pay.Sapt_cd && x.Sapt_act == txtPromo.Text && x.Sapt_cre_by == txtBank.Text && x.Sapt_mod_by == cmbCardType.Text).ToList<PaymentTypeRef>();
                    if (_duplicat == null || _duplicat.Count <= 0)
                    {
                        PaymentTypeRef tem = new PaymentTypeRef();
                        tem.Sapt_cd = pay.Sapt_cd;
                        tem.Sapt_desc = pay.Sapt_desc;
                        if (pay.Sapt_cd == "CRCD")
                        {
                            if (txtPromo.Text != "0" && txtPromo.Text != "")
                            {
                                tem.Sapt_act = txtPromo.Text;
                            }
                            tem.Sapt_cre_by = txtBank.Text;
                            tem.Sapt_mod_by = cmbCardType.Text;

                        }
                        if (pay.Sapt_cd == "LORE")
                        {
                            {
                                tem.Sapt_cre_by = txtLotyType.Text;
                                tem.Sapt_mod_by = cmbCusSpec.Text;
                            }
                        }
                        //kapila 7/4/2015
                        if (pay.Sapt_cd == "DEBT")
                            tem.Sapt_cre_by = txtBank.Text;

                        PayType.Add(tem);
                        txtPromo.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Already contain " + pay.Sapt_cd + " type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                grvPayType.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = PayType;
                grvPayType.DataSource = _source;
                pnlBank.Visible = false;
                pnlLoyalty.Visible = false;
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
        private void chkSalesAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPayAll.Checked)
                {
                    grvPayType.AutoGenerateColumns = false;
                    PayType = new List<PaymentTypeRef>();
                    List<PaymentTypeRef> _payMode = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
                    foreach (PaymentTypeRef _refTem in _payMode)
                    {
                        PaymentTypeRef tem = new PaymentTypeRef();
                        tem.Sapt_cd = _refTem.Sapt_cd;
                        tem.Sapt_desc = _refTem.Sapt_desc;
                        PayType.Add(tem);
                    }
                    BindingSource _source = new BindingSource();
                    _source.DataSource = PayType;
                    grvPayType.DataSource = _source;
                }
                else
                {
                    grvPayType.AutoGenerateColumns = false;
                    PayType = new List<PaymentTypeRef>();
                    BindingSource _source = new BindingSource();
                    _source.DataSource = PayType;
                    grvPayType.DataSource = _source;
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


        private void grvSalesType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            PayType.RemoveAt(e.RowIndex);
                            grvPayType.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = PayType;
                            grvPayType.DataSource = _source;
                        }
                        catch (Exception) { }

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

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                chkSunday.Checked = true;
                chkMonday.Checked = true;
                chkTuesday.Checked = true;
                chkWednesday.Checked = true;
                chkThursday.Checked = true;
                chkFriday.Checked = true;
                chkSaturday.Checked = true;
            }
            else
            {
                chkSunday.Checked = false;
                chkMonday.Checked = false;
                chkTuesday.Checked = false;
                chkWednesday.Checked = false;
                chkThursday.Checked = false;
                chkFriday.Checked = false;
                chkSaturday.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            /*
             
            Serial=1
            Promotion=2
            Item=3
            Brand & Sub cat=4
            Brand & Cat=5
            Brand & main cat=6
            Brand=7
            Sub cat=8
            Cat=9
            Main cat=10
           
             */

            try
            {

                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2")
                {
                    if (txtBrand.Text == string.Empty)
                    {
                        MessageBox.Show("Specify brand also!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string selection = "";
                if (cmbSelectCat.SelectedValue.ToString() == "10")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "9")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "8")
                {
                    selection = "CATE3";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "7")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    selection = "BRAND_CATE1";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    selection = "BRAND_CATE2";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    selection = "BRAND_CATE3";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "3")
                {
                    selection = "ITEM";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "2")
                {
                    selection = "PROMOTION";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "1")
                {
                    selection = "SERIAL";
                }
                //ItemBrandCat_List = new List<CashCommissionDetailRef>();

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text, txtItemCD.Text.Trim(), txtSerial.Text.Trim(), txtCircular.Text.Trim(), txtPromotion.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
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
                    if (_duplicate.Count() > 0)
                    {
                        MessageBox.Show("Duplicate record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (addList == null || addList.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                grvSalesTypes.AutoGenerateColumns = false;
                ItemBrandCat_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = ItemBrandCat_List;
                grvSalesTypes.AutoGenerateColumns = false;
                grvSalesTypes.DataSource = source;
                if (dt.Rows.Count > 0)
                {
                    grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                }

                foreach (DataGridViewRow grv in grvSalesTypes.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grv.Cells[0];
                    cell.Value = "True";
                }

                txtItemCD.Text = "";
                txtCate1.Text = "";
                txtCate2.Text = "";
                txtCate3.Text = "";
                txtSerial.Text = "";
                txtPromotion.Text = "";

                //foreach (DataGridViewRow gr in grvSalesTypes.Rows)
                //{
                //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                //    chk.Value = "true";
                //}
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

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in grvSalesTypes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                chk.Value = "True";
            }
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in grvSalesTypes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                chk.Value = "False";
            }
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            grvSalesTypes.DataSource = null;
        }

        private void cmbPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCardType.Visible = true;
            txtPromo.Visible = true;
            label13.Visible = true;
            label73.Visible = true;

            if (cmbPayType.SelectedValue != null && cmbPayType.SelectedValue.ToString() == "CRCD")
            {
                pnlBank.Visible = true;
                pnlLoyalty.Visible = false;
            }

            else if (cmbPayType.SelectedValue != null && cmbPayType.SelectedValue.ToString() == "LORE")
            {
                pnlLoyalty.Visible = true;
                lblLoyalty.Text = "Point Selection";
                pnlBank.Visible = false;
            }
            //kapila 7/4/2015
            else if (cmbPayType.SelectedValue != null && cmbPayType.SelectedValue.ToString() == "DEBT")
            {
                pnlBank.Visible = true;
                pnlLoyalty.Visible = false;
                cmbCardType.Visible = false;
                txtPromo.Visible = false;
                label13.Visible = false;
                label73.Visible = false;
            }
            else
            {
                lblLoyalty.Text = "Quantity Selection";
                pnlLoyalty.Visible = false;
                pnlBank.Visible = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            try
            {
                chkApplyForTotalInvoice.Checked = false; //added by akila 2018/03/05
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();
                PBList = new List<PriceBookLevelRef>();
                ItemBrandCat_List = new List<CashCommissionDetailRef>();
                PCList = new DataTable();
                PayType = new List<PaymentTypeRef>();
                SalesType = new List<MasterInvoiceType>();
                BindPartyType();
                BindPayType();
                BindCategoryTypes();
                BindSalesTypes();
                lblLoyalty.Text = "Quantity Selection";
                this.Cursor = Cursors.Default;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                tabControl1.SelectedIndex = 0;
                lstPc.Items.Clear();
                lstItem.Items.Clear();
                BindCategoryTypesEdit();
                BindPartyTypeEdit();
                chkAppPendings.Checked = false;
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

        private void btnBank_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBank;
                _CommonSearch.ShowDialog();
                txtBank.Select();
                LoadCardType(txtBank.Text);
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

        private void LoadCardType(string p)
        {
            DataTable _dt = CHNLSVC.Sales.GetBankCC(p);
            if (_dt.Rows.Count > 0)
            {
                cmbCardType.DataSource = _dt;
                cmbCardType.DisplayMember = "mbct_cc_tp";
                cmbCardType.ValueMember = "mbct_cc_tp";
            }
            else
            {
                cmbCardType.DataSource = null;
            }
        }


        private void BindSalesTypes()
        {
            //cmbSalesType.DataSource = CHNLSVC.General.GetSalesTypes("", null, null);
            //cmbSalesType.DisplayMember = "srtp_desc";
            //cmbSalesType.ValueMember = "srtp_cd";
        }

        private void btnAddSt_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (!string.IsNullOrEmpty(txtMInvType.Text)) ? txtMInvType.Text : "";
                DataTable _dt = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", code);
                if (_dt.Rows.Count > 0)
                {

                    MasterInvoiceType _duplicate = SalesType.Find(x => x.Srtp_cd == _dt.Rows[0]["Srtp_cd"].ToString());
                    if (_duplicate == null)
                    {
                        MasterInvoiceType _invType = new MasterInvoiceType();
                        _invType.Srtp_cd = _dt.Rows[0]["Srtp_cd"].ToString();
                        _invType.Srtp_desc = _dt.Rows[0]["SRTP_DESC"].ToString();
                        SalesType.Add(_invType);

                        grvSalesType.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = SalesType;
                        grvSalesType.DataSource = _source;
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

        private void grvSalesType_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SalesType.RemoveAt(e.RowIndex);

                        grvSalesType.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = SalesType;
                        grvSalesType.DataSource = _source;
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

        private void chkSalesAll_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (chkSalesAll.Checked)
                {
                    DataTable _dt = CHNLSVC.General.GetSalesTypes("", null, null);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        MasterInvoiceType _invoType = new MasterInvoiceType();
                        _invoType.Srtp_cd = dr["SRTP_CD"].ToString();
                        _invoType.Srtp_desc = dr["SRTP_DESC"].ToString();
                        SalesType.Add(_invoType);
                    }
                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;

                }
                else
                {
                    SalesType = new List<MasterInvoiceType>();
                    grvSalesType.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = SalesType;
                    grvSalesType.DataSource = _source;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                Cursor = Cursors.WaitCursor;
                if (chkupload.Checked)
                {
                    ExcelUploadProcess();  // Tharanga 2017/05/15
                }
                else
                {
                    Process();
                }
               
                Cursor = Cursors.Default;
                btnSave.Enabled = true;
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

        private void Process()
        {
            //validation
            try
            {
                if (txtCircular.Text == "")
                {
                    MessageBox.Show("Please select circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtDiscount.Text == "")
                {
                    MessageBox.Show("Please enter discount amount/value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal val;
                if (!decimal.TryParse(txtDiscount.Text, out val))
                {
                    MessageBox.Show("Please enter discount amount/value in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (val < 0)
                {
                    MessageBox.Show("Please enter discount amount/value can not be minus", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (PBList.Count <= 0)
                {
                    MessageBox.Show("Please select price book details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (grvParty.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select location details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ItemBrandCat_List.Count <= 0)
                {
                    MessageBox.Show("Please select item details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (SalesType.Count <= 0)
                {
                    MessageBox.Show("Please select sales type details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (PayType == null || PayType.Count <= 0)
                {
                    MessageBox.Show("Please select Payment type details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<CashPromotionDiscountHeader> _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtCircular.Text);
                if (_hdr != null && _hdr.Count > 0)
                {
                    MessageBox.Show("Circular with same name exists.\nPlease select another name.");
                    return;
                }


                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                CashPromotionDiscountHeader _discount = new CashPromotionDiscountHeader();
                CashPromotionDiscountDetail _detail = new CashPromotionDiscountDetail();
                _discount.Spdh_circular = txtCircular.Text;
                _discount.Spdh_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
                _discount.Spdh_from_dt = dateTimePickerFrom.Value.Date;
                _discount.Spdh_to_dt = dateTimePickerTo.Value.Date;
                _discount.Spdh_stus = 1;
                _discount.Spdh_cre_by = BaseCls.GlbUserID;
                _discount.Spdh_cre_dt = _date;
                _discount.Spdh_mod_by = BaseCls.GlbUserID;
                _discount.Spdh_mod_dt = _date;
                _discount.Spdh_no_times = Convert.ToInt32(txtNoTimes.Text);
                _discount.Spdh_is_alw_normal = chkAlwProcessNormal.Checked;
                _detail.Spdd_pay_tp = DropDownListPartyTypes.SelectedValue.ToString();
                _detail.Spdd_from_dt = dateTimePickerFrom.Value.Date;
                _detail.Spdd_to_dt = dateTimePickerTo.Value.Date;
                _detail.Spdd_from_time = Convert.ToInt32(nuFromHH.Value * 10000 + nuFromMM.Value * 100);
                _detail.Spdd_to_time = Convert.ToInt32(nuToHH.Value * 10000 + nuToMM.Value * 100);
                _detail.Spdd_stus = 2; // 2: temp save 
                _discount.ApplyForTotalInvoice = chkApplyForTotalInvoice.Checked ? 1 : 0;

                int alwMulty;
                if (chkAlwMultiply.Checked)
                    alwMulty = 1;
                else
                    alwMulty = 0;
                _detail.Spdd_alw_mult = alwMulty;
                try
                {
                    _detail.Spdd_from_val = Convert.ToDecimal(txtValueFrom.Text);
                }
                catch (Exception)
                {
                    _detail.Spdd_from_val = 0;
                }
                try
                {
                    _detail.Spdd_to_val = Convert.ToDecimal(txtValueTo.Text);
                }
                catch (Exception)
                {
                    _detail.Spdd_to_val = 9999999;
                }
                try
                {
                    _detail.Spdd_from_qty = Convert.ToInt32(txtQtyFrom.Text);
                }
                catch (Exception)
                {
                    _detail.Spdd_from_qty = 0;
                }
                try
                {
                    _detail.Spdd_to_qty = Convert.ToInt32(txtQtyTo.Text);
                }
                catch (Exception)
                {
                    _detail.Spdd_to_qty = 999999;
                }

                if (cmbSelectCat.SelectedValue.ToString() == "2")
                    _detail.Spdd_alw_pro = true;
                if (cmbSelectCat.SelectedValue.ToString() == "1")
                    _detail.Spdd_alw_ser = true;

                if(BaseCls.GlbUserComCode=="AST")   //kapila 11/12/2015
                    _detail.Spdd_alw_pro = true;

                string days = "";// "SUNDAY,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY";
                if (chkSunday.Checked)
                    days = days + chkSunday.Text.ToUpper() + ",";
                if (chkMonday.Checked)
                    days = days + chkMonday.Text + ",";
                if (chkTuesday.Checked)
                    days = days + chkTuesday.Text + ",";
                if (chkWednesday.Checked)
                    days = days + chkWednesday.Text + ",";
                if (chkThursday.Checked)
                    days = days + chkThursday.Text + ",";
                if (chkFriday.Checked)
                    days = days + chkFriday.Text + ",";
                if (chkSaturday.Checked)
                    days = days + chkSaturday.Text;
                if (chkAll.Checked)
                {
                    days = "SUNDAY,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY";
                }
                _detail.Spdd_day = days.ToUpper();
                _detail.Spdd_cust_cd = txtCustomer.Text;
                if (rdoAmo.Checked)
                    _detail.Spdd_disc_val = Convert.ToDecimal(txtDiscount.Text);
                else
                    _detail.Spdd_disc_rt = Convert.ToDecimal(txtDiscount.Text);
                _detail.Spdd_cre_by = BaseCls.GlbUserID;
                _detail.Spdd_cre_dt = _date;
                _detail.Spdd_mod_by = BaseCls.GlbUserID;
                _detail.Spdd_mod_dt = _date;

                List<string> PartyList = new List<string>();
                grvParty.EndEdit();
                foreach (DataGridViewRow gvr in grvParty.Rows)
                {

                    bool duplicate = PartyList.Contains(gvr.Cells[1].Value.ToString());
                    if (!duplicate)
                    {
                        PartyList.Add(gvr.Cells[1].Value.ToString());
                    }

                }

                List<CashCommissionDetailRef> _itemList = new List<CashCommissionDetailRef>();
                //dataGridViewItem1.EndEdit();
                DataTable _itemTable = new DataTable();
                _itemTable.TableName = "aaa";
                _itemTable.Columns.Add("Sccd_itm");
                _itemTable.Columns.Add("Sccd_brd");
                _itemTable.Columns.Add("Sccd_ser");
                for (int i = 0; i < grvSalesTypes.Rows.Count; i++)
                {
                    grvSalesTypes.EndEdit();
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[i].Cells[0];

                    if (cell.Value != null && cell.Value.ToString().ToUpper() == "TRUE")
                    {
                        DataRow _dr = _itemTable.NewRow();
                        _dr[0] = ItemBrandCat_List[i].Sccd_itm;
                        _dr[1] = ItemBrandCat_List[i].Sccd_brd;
                        _dr[2] = ItemBrandCat_List[i].Sccd_ser;
                        _itemTable.Rows.Add(_dr);
                        // _itemList.Add(ItemBrandCat_List[i]);
                    }

                }

                //validation
                if (_itemTable.Rows.Count <= 0)
                {
                    MessageBox.Show("Item Details not found\nPlease make sure item details enterd and selected");
                    return;
                }

                List<string> temSalesType = new List<string>();
                foreach (MasterInvoiceType _sales in SalesType)
                {
                    temSalesType.Add(_sales.Srtp_cd);
                }
                int hdrCou;
                int detCou;
                int itmCou;
                int locCou;
                string err;
                int times = 0;
                try
                {
                    times = Convert.ToInt32(txtNoTimes.Text);
                }
                catch (Exception) { times = 0; }
                int effect = CHNLSVC.Sales.SaveDiscountDefinition(_discount, _detail, PartyList, _itemTable, temSalesType, PBList, PayType, Convert.ToInt32(cmbSelectCat.SelectedValue), out hdrCou, out detCou, out itmCou, out locCou, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out err, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                int StatusResult = CHNLSVC.Sales.DiscountDefinitionChangeStatus(txtCircular.Text, 2);

                if (err == "")
                {
                    MessageBox.Show("Successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Save Unsuccessful\n" + err, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //  MessageBox.Show("Header -" + hdrCou+"\n"+"Detail "+detCou+"\n"+"Item"+itmCou+"\n"+"Location"+locCou);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }
         

        // Tharanga 2017/05/15
        private void ExcelUploadProcess()
        {
            int count = 1;
            #region para
            int alwMulty;
            int hdrCou;
           
            int detCou;
            int itmCou;
            int locCou;
            string err;
            int times = 0;
            string days = "";
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            #endregion
            #region Validation
            //validation
            try
            {
                if (string.IsNullOrEmpty(txtExcelUpload.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtExcelUpload.Clear();
                    txtExcelUpload.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtExcelUpload.Text);
                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnGvBrowse.Focus();
                    return;
                }
                if (txtCircular.Text == "")
                {
                    MessageBox.Show("Please select circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
               
               
                if (PBList.Count <= 0)
                {
                    MessageBox.Show("Please select price book details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (grvParty.Rows.Count <= 0)
                {
                    MessageBox.Show("Please select location details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if (SalesType.Count <= 0)
                {
                    MessageBox.Show("Please select sales type details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (PayType == null || PayType.Count <= 0)
                {
                    MessageBox.Show("Please select Payment type details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<CashPromotionDiscountHeader> _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtCircular.Text);
                if (_hdr != null && _hdr.Count > 0)
                {
                    MessageBox.Show("Circular with same name exists.\nPlease select another name.");
                    return;
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension.ToUpper() == ".XLS")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension.ToUpper() == ".XLSX")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }
                else
                {
                    MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
   
                }
                
            
                string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;

                _excelConnectionString = String.Format(conStr, txtExcelUpload.Text, "YES");
                OleDbConnection connExcel = new OleDbConnection(_excelConnectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                cmdExcel.Connection = connExcel;

                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "] ";
                oda.SelectCommand = cmdExcel;
                oda.Fill(_dt);
                connExcel.Close();
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            #endregion
                List<SaleDisDefinition> _lsit = new List<SaleDisDefinition>();
                if (_dt.Rows.Count > 0)
                {                      
                    foreach (DataRow _row in _dt.Rows)
                    {
                        SaleDisDefinition _saledes = new SaleDisDefinition();
                        _saledes.itemcd = _row[0] == DBNull.Value ? string.Empty : _row[0].ToString().Trim();
                        
                        _saledes.frmDate = _row[3] == DBNull.Value ? DateTime.Today.Date : Convert.ToDateTime( _row[3]).Date;
                        _saledes.toDate = _row[4] == DBNull.Value ? DateTime.Today.Date : Convert.ToDateTime(_row[4]).Date;
                        decimal val;

                        double myNum = 0;
                       

                        if (Double.TryParse(_row[1].ToString(), out myNum) || Double.TryParse(_row[2].ToString(), out myNum))
                        {
                            _saledes.pre = _row[1] == DBNull.Value ? 0 : Convert.ToDecimal(_row[1].ToString());
                            _saledes.value = _row[2] == DBNull.Value ? 0 : Convert.ToDecimal(_row[2].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Please enter numeric to the Discount Rate or Discount Value ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _saledes.itemcd);
                        if (_item == null)
                        {
                            MessageBox.Show("Invalid Item - " + _row[0].ToString() + "\nPlease check the item.", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        if (_saledes.frmDate > _saledes.toDate)
                        {
                            MessageBox.Show("Please Enter From Date less than To Date.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_saledes.pre < 0  || _saledes.pre >= 100)
                        {
                            MessageBox.Show("Please enter Discount Rate greater than 0 or less than 100.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_saledes.value < 0)
                        {
                            MessageBox.Show("Please enter Discount Rate greater than 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //MessageBox.Show("Both Discount Rate & Discount Amount empty recodes found \nPlease check the excel sheet.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        _lsit.Add(_saledes);
                    }                      
                }
                CashPromotionDiscountDetail _detail = new CashPromotionDiscountDetail();
                CashPromotionDiscountHeader _discount = new CashPromotionDiscountHeader();
                int effect = 0;

                var _presentageList = _lsit.Where(x => x.value == 0 && x.pre >= 0).GroupBy(x => new { x.pre, x.frmDate, x.toDate }).Select(x => x.Key).OrderBy(x => x.pre).ToList();
                //var _presentageList = _lsit.Where(x => x.value == 0 && x.pre > 0).GroupBy(x => new {x.itemcd,x.pre, x.frmDate, x.toDate }).Select(x => x.Key).OrderBy(x=>x.pre).ToList();
                //var _ValueList = _lsit.Where(x => x.pre == 0 && x.value > 0).GroupBy(x => new { x.value, x.frmDate, x.toDate }).Select(x => x.Key).OrderBy(x => x.value).ToList(); ;
                #region Discount presentage
                foreach (var item in _presentageList)
                {
                    _discount = new CashPromotionDiscountHeader();
                    _detail = new CashPromotionDiscountDetail();
                    _discount.Spdh_circular = Convert.ToString( txtCircular.Text +"_"+count);
                    _discount.Spdh_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
                    _discount.Spdh_from_dt = item.frmDate;
                    _discount.Spdh_to_dt = item.toDate;
                    _discount.Spdh_stus = 2;
                    _discount.Spdh_cre_by = BaseCls.GlbUserID;
                    _discount.Spdh_cre_dt = _date;
                    _discount.Spdh_mod_by = BaseCls.GlbUserID;
                    _discount.Spdh_mod_dt = _date;
                    _discount.Spdh_no_times = Convert.ToInt32(txtNoTimes.Text);
                    _discount.Spdh_is_alw_normal = chkAlwProcessNormal.Checked;
                    decimal rate=item.pre;
                    _detail.Spdd_disc_rt = item.pre;
                    _detail.Spdd_pay_tp = DropDownListPartyTypes.SelectedValue.ToString();
                    _detail.Spdd_from_dt = item.frmDate;
                    _detail.Spdd_to_dt = item.toDate;
                    _detail.Spdd_from_time = Convert.ToInt32(nuFromHH.Value * 10000 + nuFromMM.Value * 100);
                    _detail.Spdd_to_time = Convert.ToInt32(nuToHH.Value * 10000 + nuToMM.Value * 100);
                    _detail.Spdd_stus = 2; // 2: temp save 

#region 001
                     
                    if (chkAlwMultiply.Checked)
                        alwMulty = 1;
                    else
                        alwMulty = 0;
                    _detail.Spdd_alw_mult = alwMulty;
                    try
                    {
                        _detail.Spdd_from_val = Convert.ToDecimal(txtValueFrom.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_from_val = 0;
                    }
                      try
                    {
                        _detail.Spdd_to_val = Convert.ToDecimal(txtValueTo.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_to_val = 9999999;
                    }
                    try
                    {
                        _detail.Spdd_from_qty = Convert.ToInt32(txtQtyFrom.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_from_qty = 0;
                    }
                    try
                    {
                        _detail.Spdd_to_qty = Convert.ToInt32(txtQtyTo.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_to_qty = 999999;
                    }

                    if (cmbSelectCat.SelectedValue.ToString() == "2")
                        _detail.Spdd_alw_pro = true;
                    if (cmbSelectCat.SelectedValue.ToString() == "1")
                        _detail.Spdd_alw_ser = true;

                    if (BaseCls.GlbUserComCode == "AST")   //kapila 11/12/2015
                        _detail.Spdd_alw_pro = true;

#endregion

#region 002
                     //string days = "";// "SUNDAY,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY";
                    if (chkSunday.Checked)
                        days = days + chkSunday.Text.ToUpper() + ",";
                    if (chkMonday.Checked)
                        days = days + chkMonday.Text + ",";
                    if (chkTuesday.Checked)
                        days = days + chkTuesday.Text + ",";
                    if (chkWednesday.Checked)
                        days = days + chkWednesday.Text + ",";
                    if (chkThursday.Checked)
                        days = days + chkThursday.Text + ",";
                    if (chkFriday.Checked)
                        days = days + chkFriday.Text + ",";
                    if (chkSaturday.Checked)
                        days = days + chkSaturday.Text;
                    if (chkAll.Checked)
                    {
                        days = "SUNDAY,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY";
                    }
                     _detail.Spdd_day = days.ToUpper();
                    _detail.Spdd_cust_cd = txtCustomer.Text;
                    
                    _detail.Spdd_cre_by = BaseCls.GlbUserID;
                    _detail.Spdd_cre_dt = _date;
                    _detail.Spdd_mod_by = BaseCls.GlbUserID;
                    _detail.Spdd_mod_dt = _date;
#endregion
                    var _item = _lsit.Where(x => x.pre == rate && x.frmDate == item.frmDate && x.toDate == item.toDate).Select(x => x.itemcd).ToList();
                    DataTable _itemTable = new DataTable();
                    List<string> _itemList = new List<string>();
                    _itemTable.TableName = "aaa";
                    _itemTable.Columns.Add("Sccd_itm");
                    for (int i = 0; i < _item.Count; i++)
                
                   // foreach (var items in _item)
                    {
                           DataRow _dr = _itemTable.NewRow();
                           _dr[0] = _item[i].ToString();
                                    _itemTable.Rows.Add(_dr);
                                    _itemList.Add(_item[i].ToString());
                    }

                    List<string> PartyList = new List<string>();
                    grvParty.EndEdit();
                    foreach (DataGridViewRow gvr in grvParty.Rows)
                    {

                        bool duplicate = PartyList.Contains(gvr.Cells[1].Value.ToString());
                        if (!duplicate)
                        {
                            PartyList.Add(gvr.Cells[1].Value.ToString());
                        }

                    }

                    List<string> temSalesType = new List<string>();
                    foreach (MasterInvoiceType _sales in SalesType)
                    {
                        temSalesType.Add(_sales.Srtp_cd);
                    }
                    //int hdrCou;
                    //int detCou;
                    //int itmCou;
                    //int locCou;
                   
                    //int times = 0;
                    try
                    {

                        times = Convert.ToInt32(txtNoTimes.Text);
                    }
                    catch (Exception) { times = 0; }
                    effect = CHNLSVC.Sales.SaveDiscountDefinition(_discount, _detail, PartyList, _itemTable, temSalesType, PBList, PayType, Convert.ToInt32(cmbSelectCat.SelectedValue), out hdrCou, out detCou, out itmCou, out locCou, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out err, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                    count++;
                    //int StatusResult = CHNLSVC.Sales.DiscountDefinitionChangeStatus(txtCircular.Text, 2);


                }
                #endregion
                //var _ValueList = _lsit.Where(x => x.pre == 0 && x.value > 0).GroupBy(x => new {x.itemcd, x.value, x.frmDate, x.toDate }).Select(x => x.Key).OrderBy(x => x.value).ToList(); ;
                var _ValueList = _lsit.Where(x => x.pre == 0 && x.value > 0).GroupBy(x => new {  x.value, x.frmDate, x.toDate }).Select(x => x.Key).OrderBy(x => x.value).ToList(); ;
                #region Discount Value
               
                foreach (var value in _ValueList)
                {
                    _discount = new CashPromotionDiscountHeader();
                    _detail = new CashPromotionDiscountDetail();
                    _discount.Spdh_circular = Convert.ToString(txtCircular.Text + "_" + count);
                    _discount.Spdh_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
                    _discount.Spdh_from_dt = value.frmDate;
                    _discount.Spdh_to_dt = value.toDate;
                    _discount.Spdh_stus = 2;
                    _discount.Spdh_cre_by = BaseCls.GlbUserID;
                    _discount.Spdh_cre_dt = _date;
                    _discount.Spdh_mod_by = BaseCls.GlbUserID;
                    _discount.Spdh_mod_dt = _date;
                    _discount.Spdh_no_times = Convert.ToInt32(txtNoTimes.Text);
                    _discount.Spdh_is_alw_normal = chkAlwProcessNormal.Checked;
                    decimal vale = value.value;
                    _detail.Spdd_pay_tp = DropDownListPartyTypes.SelectedValue.ToString();
                    _detail.Spdd_from_dt = value.frmDate;
                    _detail.Spdd_to_dt = value.toDate;
                    _detail.Spdd_disc_val = value.value;
                    _detail.Spdd_from_time = Convert.ToInt32(nuFromHH.Value * 10000 + nuFromMM.Value * 100);
                    _detail.Spdd_to_time = Convert.ToInt32(nuToHH.Value * 10000 + nuToMM.Value * 100);
                    _detail.Spdd_stus = 2; // 2: temp save 

                    #region 001

                    if (chkAlwMultiply.Checked)
                        alwMulty = 1;
                    else
                        alwMulty = 0;
                    _detail.Spdd_alw_mult = alwMulty;
                    try
                    {
                        _detail.Spdd_from_val = Convert.ToDecimal(txtValueFrom.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_from_val = 0;
                    }
                    try
                    {
                        _detail.Spdd_to_val = Convert.ToDecimal(txtValueTo.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_to_val = 9999999;
                    }
                    try
                    {
                        _detail.Spdd_from_qty = Convert.ToInt32(txtQtyFrom.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_from_qty = 0;
                    }
                    try
                    {
                        _detail.Spdd_to_qty = Convert.ToInt32(txtQtyTo.Text);
                    }
                    catch (Exception)
                    {
                        _detail.Spdd_to_qty = 999999;
                    }

                    if (cmbSelectCat.SelectedValue.ToString() == "2")
                        _detail.Spdd_alw_pro = true;
                    if (cmbSelectCat.SelectedValue.ToString() == "1")
                        _detail.Spdd_alw_ser = true;

                    if (BaseCls.GlbUserComCode == "AST")   //kapila 11/12/2015
                        _detail.Spdd_alw_pro = true;

                    #endregion

                    #region 002
                    // "SUNDAY,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY";
                    if (chkSunday.Checked)
                        days = days + chkSunday.Text.ToUpper() + ",";
                    if (chkMonday.Checked)
                        days = days + chkMonday.Text + ",";
                    if (chkTuesday.Checked)
                        days = days + chkTuesday.Text + ",";
                    if (chkWednesday.Checked)
                        days = days + chkWednesday.Text + ",";
                    if (chkThursday.Checked)
                        days = days + chkThursday.Text + ",";
                    if (chkFriday.Checked)
                        days = days + chkFriday.Text + ",";
                    if (chkSaturday.Checked)
                        days = days + chkSaturday.Text;
                    if (chkAll.Checked)
                    {
                        days = "SUNDAY,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY";
                    }
                    _detail.Spdd_day = days.ToUpper();
                    _detail.Spdd_cust_cd = txtCustomer.Text;

                   

                    _detail.Spdd_cre_by = BaseCls.GlbUserID;
                    _detail.Spdd_cre_dt = _date;
                    _detail.Spdd_mod_by = BaseCls.GlbUserID;
                    _detail.Spdd_mod_dt = _date;
                    
                    #endregion
                    var _valeitem = _lsit.Where(x => x.value == vale && x.frmDate == value.frmDate && x.toDate == value.toDate).Select(x => x.itemcd).ToList();
                    DataTable _valeitemTable = new DataTable();
                    List<string> _valueitemList = new List<string>();
                    _valeitemTable.TableName = "aaa";
                    _valeitemTable.Columns.Add("Sccd_itm");
                    for (int i = 0; i < _valeitem.Count; i++)

                    // foreach (var items in _item)
                    {
                        DataRow _dr = _valeitemTable.NewRow();
                        _dr[0] = _valeitem[i].ToString();
                        _valeitemTable.Rows.Add(_dr);
                        _valueitemList.Add(_valeitem[i].ToString());
                    }

                    List<string> valuePartyList = new List<string>();
                    grvParty.EndEdit();
                    foreach (DataGridViewRow gvr in grvParty.Rows)
                    {

                        bool duplicate = valuePartyList.Contains(gvr.Cells[1].Value.ToString());
                        if (!duplicate)
                        {
                            valuePartyList.Add(gvr.Cells[1].Value.ToString());
                        }

                    }
                  
                    List<string> valuetemSalesType = new List<string>();
                    foreach (MasterInvoiceType _sales in SalesType)
                    {
                        valuetemSalesType.Add(_sales.Srtp_cd);
                    }

                    try
                    {

                        times = Convert.ToInt32(txtNoTimes.Text);
                    }
                    catch (Exception) { times = 0; }
                    
                        effect = CHNLSVC.Sales.SaveDiscountDefinition(_discount, _detail, valuePartyList, _valeitemTable, valuetemSalesType, PBList, PayType, Convert.ToInt32(cmbSelectCat.SelectedValue), out hdrCou, out detCou, out itmCou, out locCou, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out err, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                        count++;
                   
                    
                }
            
                #endregion
               
               
               
                StringBuilder _errorLst = new StringBuilder();


                if (effect == 1)
                {
                    MessageBox.Show("Successfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Save Unsuccessful\n" , "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    

                
        }
                    //  MessageBox.Show("Header -" + hdrCou+"\n"+"Detail "+detCou+"\n"+"Item"+itmCou+"\n"+"Location"+locCou);
          catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }
        
        
        private void btnCustomer_Click(object sender, EventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            pnlCancel.Visible = true;
            pnlMain.Enabled = false;
        }

        private void btnCancelPop_Click(object sender, EventArgs e)
        {
            pnlCancel.Visible = false;
            pnlMain.Enabled = true;
            txtCancelCircualr.Text = "";
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCancelCircualr.Text = "";
        }

        private void linkLabelProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to cancel this circular", "Wairning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                if (txtCancelCircualr.Text != "")
                {

                    int result = CHNLSVC.Sales.UpdatePromotionlDiscountStatus(txtCancelCircualr.Text, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                    int StatusResult = CHNLSVC.Sales.DiscountDefinitionChangeStatus(txtCircular.Text, 0);
                    //process
                    if (result > 0)
                    {
                        MessageBox.Show("Successfully Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        pnlCancel.Visible = false;
                        pnlMain.Enabled = true;
                        txtCancelCircualr.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Update Unsuccessful", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("Please select circular no", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancelCir_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                //DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtCancelCircualr;
                //_CommonSearch.txtSearchbyword.Text = txtCancelCircualr.Text;
                //_CommonSearch.ShowDialog();
                //txtCancelCircualr.Focus();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionalCircular);
                DataTable _result = CHNLSVC.CommonSearch.SearchPromotinalCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCancelCircualr;
                _CommonSearch.txtSearchbyword.Text = txtCancelCircualr.Text;
                _CommonSearch.ShowDialog();
                txtCancelCircualr.Focus();
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

        private void grvSalesTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grvParty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
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

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void chkPCAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPCAll.Checked == true)
            {
                this.AllClick();
            }
            else
            {
                this.NonClick();
            }
        }

        private void AllClick()
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void NonClick()
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvParty.EndEdit();



            }
            catch (Exception ex)
            {

            }
        }

        #region key down evevts
        private void txtCircular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiscount.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnCircular_Click(null, null);
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCustomer.Focus();
            }
        }

        private void txtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerFrom.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnCustomer_Click(null, null);
        }

        private void dateTimePickerFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerTo.Focus();
            }
        }

        private void dateTimePickerTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuFromHH.Focus();
            }
        }

        private void nuFromHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuFromMM.Focus();
            }
        }

        private void nuFromMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuToHH.Focus();
            }
        }

        private void nuToHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nuToMM.Focus();
            }
        }

        private void nuToMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtValueFrom.Focus();
            }
        }

        private void txtValueFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtValueTo.Focus();
            }
        }

        private void txtValueTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQtyFrom.Focus();
            }
        }

        private void txtQtyFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQtyTo.Focus();
            }
        }

        private void txtQtyTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMInvType.Focus();
            }
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate1.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnBrand_Click(null, null);
        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                txtCate2.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnMainCat_Click(null, null);
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                txtCate3.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnCat_Click(null, null);
        }

        private void txtCate3_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                txtItemCD.Focus();
            }
            if (e.KeyCode == Keys.F2)
                button5_Click(null, null);
        }

        private void txtItemCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerial.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnItem_Click(null, null);
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerial.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnSerial_Click(null, null);
        }

        private void txtPromotion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
            if (e.KeyCode == Keys.F2)
                btnPromation_Click(null, null);
        }
        #endregion

        #region txt box mouse double click
        private void txtCircular_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCircular_Click(null, null);
        }

        private void txtCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCustomer_Click(null, null);
        }

        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand_Click(null, null);
        }

        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat_Click(null, null);
        }

        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat_Click(null, null);
        }

        private void txtCate3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button5_Click(null, null);
        }

        private void txtItemCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem_Click(null, null);
        }

        private void txtSerial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial_Click(null, null);
        }

        private void txtPromotion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPromation_Click(null, null);
        }

        private void txtPriceBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPB_Click(null, null);
        }

        private void txtLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPriceLvl_Click(null, null);
        }
        #endregion

        private void grvParty_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnLotyType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
                DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLotyType;
                _CommonSearch.txtSearchbyword.Text = txtLotyType.Text;
                _CommonSearch.ShowDialog();
                txtLotyType.Focus();
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

        private void txtCircular_Leave(object sender, EventArgs e)
        {
            //if (txtCircular.Text != "") {
            //    DataTable _det = CHNLSVC.Sales.GetCircularNo(txtCircular.Text);
            //    if (_det.Rows.Count<=0) {
            //        MessageBox.Show("Invalid Circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtCircular.Text = "";
            //        return;

            //    }
            //}

            List<CashPromotionDiscountHeader> _hdr = null;
            try
            {
                if (txtCircular.Text != "")
                {
                    _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtCircular.Text);
                    if (_hdr == null || _hdr.Count <= 0)
                    {
                        //MessageBox.Show("Entered circular number  in not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {

                        SalesType = new List<MasterInvoiceType>();
                        PayType = new List<PaymentTypeRef>();
                        PBList = new List<PriceBookLevelRef>();
                        List<CashPromotionDiscountDetail> _detail = CHNLSVC.Sales.GetPromotinalDiscountDetail(_hdr[0].Spdh_seq);
                        List<CashPromotionDiscountLocation> _location = CHNLSVC.Sales.GetPromotinalDiscountLoc(_hdr[0].Spdh_seq);
                        List<CashPromotionDiscountItem> _item = CHNLSVC.Sales.GetPromotinalDiscountItem(_hdr[0].Spdh_seq);

                        //_detail = (from _res in _detail
                        //           where _res.Spdd_stus == true
                        //           select _res).ToList<CashPromotionDiscountDetail>();

                        _location = (from _res in _location
                                     where _res.Spdl_act == true
                                     select _res).ToList<CashPromotionDiscountLocation>();

                        if (_item != null)
                        {
                            _item = (from _res in _item
                                     where _res.Spdi_act == true
                                     select _res).ToList<CashPromotionDiscountItem>();
                        }

                        txtQtyFrom.Text = _detail[0].Spdd_from_qty.ToString();
                        txtQtyTo.Text = _detail[0].Spdd_to_qty.ToString();
                        chkAlwMultiply.Checked = _detail[0].Spdd_alw_mult == 1 ? true : false;

                        if (_hdr[0].Spdh_stus == 1)
                        {
                            lblStatus.Text = "Active";
                        }
                        else if (_hdr[0].Spdh_stus == 0)
                        {
                            lblStatus.Text = "Inactive";
                        }
                        else if (_hdr[0].Spdh_stus == 2)
                        {
                            lblStatus.Text = "Pending";
                        }

                        chkAlwProcessNormal.Checked = _hdr[0].Spdh_is_alw_normal;

                        dateTimePickerFrom.Value = _detail[0].Spdd_from_dt;
                        dateTimePickerTo.Value = _detail[0].Spdd_to_dt;

                        txtValueFrom.Text = _detail[0].Spdd_from_val.ToString();
                        txtValueTo.Text = _detail[0].Spdd_to_val.ToString();
                        if (_detail[0].Spdd_disc_rt > 0)
                        {
                            txtDiscount.Text = _detail[0].Spdd_disc_rt.ToString();
                        }
                        else
                            txtDiscount.Text = _detail[0].Spdd_disc_val.ToString();
                        //load data
                        //sales types
                        DataTable _dt = new DataTable();
                        _dt.Columns.Add("Code");
                        _dt.Columns.Add("Description");
                        foreach (CashPromotionDiscountDetail hdr in _detail)
                        {
                            DataTable _tem = CHNLSVC.General.GetSalesTypes("", "SRTP_CD", hdr.Spdd_sale_tp);
                            foreach (DataRow dr in _tem.Rows)
                            {

                                List<MasterInvoiceType> dup = (from _res in SalesType
                                                               where _res.Srtp_cd == dr["SRTP_CD"].ToString()
                                                               select _res).ToList<MasterInvoiceType>();
                                if (dup != null && dup.Count > 0)
                                {
                                    continue;
                                }

                                MasterInvoiceType _invoType = new MasterInvoiceType();
                                _invoType.Srtp_cd = dr["SRTP_CD"].ToString();
                                _invoType.Srtp_desc = dr["SRTP_DESC"].ToString();
                                SalesType.Add(_invoType);
                            }
                        }

                        //load pay type/load price book/level
                        foreach (CashPromotionDiscountDetail _det in _detail)
                        {
                            List<PaymentTypeRef> dup = (from _res in PayType
                                                        where _res.Sapt_cd == _det.Spdd_pay_tp && _res.Sapt_cre_by == _det.Spdd_bank
                                                        && _res.Sapt_mod_by == _det.Spdd_cc_tp && Convert.ToInt32(_res.Sapt_act) == _det.Spdd_cc_pd
                                                        select _res).ToList<PaymentTypeRef>();
                            if (dup != null && dup.Count > 0)
                            {
                                continue;
                            }

                            PaymentTypeRef _pay = new PaymentTypeRef();
                            _pay.Sapt_cd = _det.Spdd_pay_tp;
                            _pay.Sapt_cre_by = _det.Spdd_bank;
                            _pay.Sapt_mod_by = _det.Spdd_cc_tp;
                            _pay.Sapt_act = _det.Spdd_cc_pd.ToString();
                            PayType.Add(_pay);


                            dataGridViewPriceBook.AutoGenerateColumns = false;
                            List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
                            if (_det.Spdd_pb == null || _det.Spdd_pb_lvl == null)
                                continue;
                            List<PriceBookLevelRef> tem = (from _res in PBList
                                                           where _res.Sapl_pb == _det.Spdd_pb && _res.Sapl_pb_lvl_cd == _det.Spdd_pb_lvl
                                                           select _res).ToList<PriceBookLevelRef>();
                            if (tem != null && tem.Count > 0)
                            {

                            }
                            else
                            {

                                pbLIST = (CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _det.Spdd_pb, _det.Spdd_pb_lvl.ToUpper()));
                                pbLIST.RemoveAll(x => x.Sapl_act == false);
                                var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                                             .Select(g => g.First())
                                             .ToList();
                                if (distinctList == null || distinctList.Count <= 0)
                                {

                                }

                                PBList.AddRange(distinctList);
                            }

                        }
                        //load loc list
                        foreach (CashPromotionDiscountLocation _loc in _location)
                        {
                            MasterProfitCenter pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, _loc.Spdl_pc);
                            if (pc != null)
                            {
                                DataRow _dr = _dt.NewRow();
                                _dr[0] = pc.Mpc_cd;
                                _dr[1] = pc.Mpc_desc;
                                _dt.Rows.Add(_dr);
                            }
                        }
                        List<CashCommissionDetailRef> _itemList = new List<CashCommissionDetailRef>();

                        if (_item != null)
                        {
                            foreach (CashPromotionDiscountItem _itm in _item)
                            {

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

                                obj.Sccd_itm = _itm.Spdi_itm;
                                try
                                {
                                    obj.Sccd_ser = "";
                                }
                                catch (Exception)
                                {
                                    obj.Sccd_ser = "";
                                }
                                _itemList.Add(obj);
                            }
                        }
                        else
                        {
                            foreach (CashPromotionDiscountHeader _ser in _hdr)
                            {
                                CashCommissionDetailRef obj1 = new CashCommissionDetailRef();
                                obj1.Sccd_itm = _ser.Spdh_ser;
                                obj1.Sccd_ser = _ser.Spdh_itm;
                                _itemList.Add(obj1);
                            }

                        }

                        DropDownListPartyTypes.SelectedValue = "PC";//_hdr[0].Spdh_pty_tp;
                        cmbSelectCat.SelectedValue = 3;


                        grvSalesType.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = SalesType;
                        grvSalesType.DataSource = _source;

                        grvPayType.AutoGenerateColumns = false;
                        BindingSource _source1 = new BindingSource();
                        _source1.DataSource = PayType;
                        grvPayType.DataSource = _source1;

                        BindingSource source2 = new BindingSource();
                        source2.DataSource = PBList;
                        dataGridViewPriceBook.DataSource = source2;

                        BindingSource source3 = new BindingSource();
                        source3.DataSource = _itemList;
                        grvSalesTypes.DataSource = source3;
                        ItemBrandCat_List = _itemList;

                        grvParty.AutoGenerateColumns = false;
                        grvParty.DataSource = _dt;

                        PCList = _dt;
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

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCD.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtSerial.Text = "";
            txtPromotion.Text = "";
            grvSalesTypes.DataSource = null;
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
        }

        private void txtLotyType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLotyType.Text != "")
                {
                    LoyaltyType _type = CHNLSVC.Sales.GetLoyaltyType(txtLotyType.Text);
                    if (_type == null)
                    {
                        MessageBox.Show("Invalid Loyalty Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLotyType.Text = "";
                        return;
                    }
                    else
                    {
                        BindCustomerSpecification();
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

        private void BindCustomerSpecification()
        {

            DataTable _dt = CHNLSVC.Sales.GetLoyaltyCustomerSpecifications(txtLotyType.Text);
            DataRow _dr = _dt.NewRow();
            _dr["SALCS_SPEC"] = "";
            _dt.Rows.Add(_dr);
            cmbCusSpec.DataSource = _dt;
            cmbCusSpec.DisplayMember = "SALCS_SPEC";
            cmbCusSpec.ValueMember = "SALCS_SPEC";

            //}
        }

        private void btnEditCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionalCircular);
                DataTable _result = CHNLSVC.CommonSearch.SearchPromotinalCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditCircular;
                _CommonSearch.txtSearchbyword.Text = txtEditCircular.Text;
                _CommonSearch.ShowDialog();
                txtEditCircular.Focus();
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

        private void txtEditCircular_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtEditCircular.Text == "")
                    return;
                SalesType = new List<MasterInvoiceType>();
                PayType = new List<PaymentTypeRef>();
                PBList = new List<PriceBookLevelRef>();
                List<CashPromotionDiscountHeader> _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtEditCircular.Text);
                if (_hdr != null && _hdr.Count > 0)
                {
                    List<CashPromotionDiscountDetail> _detail = CHNLSVC.Sales.GetPromotinalDiscountDetail(_hdr[0].Spdh_seq);
                    List<CashPromotionDiscountLocation> _location = CHNLSVC.Sales.GetPromotinalDiscountLoc(_hdr[0].Spdh_seq);
                    List<CashPromotionDiscountItem> _item = CHNLSVC.Sales.GetPromotinalDiscountItem(_hdr[0].Spdh_seq);

                    lstPc.Items.Clear();
                    lstItem.Items.Clear();
                    //load data
                    dtEditFrom.Value = _detail[0].Spdd_from_dt;
                    dtEditTo.Value = _detail[0].Spdd_to_dt;
                    if (_detail[0].Spdd_disc_rt > 0)
                    {
                        txtEditDiscount.Text = _detail[0].Spdd_disc_rt.ToString();
                        rdoEditRate.Checked = true;
                    }
                    else
                    {

                        txtEditDiscount.Text = _detail[0].Spdd_disc_val.ToString();
                        rdoEditValue.Checked = true;

                    }
                    if (_hdr[0].Spdh_stus == 1)
                    {
                        lblStatus.Text = "Active";
                    }
                    else if (_hdr[0].Spdh_stus == 0)
                    {
                        lblStatus.Text = "Inactive";
                    }
                    else if (_hdr[0].Spdh_stus == 2)
                    {
                        lblStatus.Text = "Pending";
                    }
                    //get distince location
                    var lo = _location.GroupBy(x => x.Spdl_pc).Select(group => group.First());

                    //load loc list
                    foreach (CashPromotionDiscountLocation _loc in lo)
                    {
                        if (_loc.Spdl_act)
                        {
                            lstPc.Items.Add(_loc.Spdl_pc);
                            lstPc.Items[lstPc.Items.Count - 1].Checked = true;
                        }
                        else
                        {
                            lstPc.Items.Add(_loc.Spdl_pc);
                        }


                    }
                    //get distance item
                    var it = _item.GroupBy(x => x.Spdi_itm).Select(group => group.First());

                    foreach (CashPromotionDiscountItem _itm in it)
                    {
                        if (_itm.Spdi_act)
                        {
                            lstItem.Items.Add(_itm.Spdi_itm);
                            lstItem.Items[lstItem.Items.Count - 1].Checked = true;
                        }
                        else
                        {
                            lstItem.Items.Add(_itm.Spdi_itm);
                        }
                    }



                }
                else
                {
                    //MessageBox.Show("Invalid circular no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void lnkEditItemAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem itm in lstItem.Items)
            {
                itm.Checked = true;
            }
        }

        private void lnkEditItemNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem itm in lstItem.Items)
            {
                itm.Checked = false;
            }
        }

        private void lnkEditPcAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem pc in lstPc.Items)
            {
                pc.Checked = true;
            }
        }

        private void lnkEditPcNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem pc in lstPc.Items)
            {
                pc.Checked = false;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                btnSave.Enabled = true;
                btnEdit.Enabled = false;
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10088))
                {
                    btnApprove.Enabled = true;
                }
            }
            else
            {
                btnEdit.Enabled = true;
                btnSave.Enabled = false;
                btnApprove.Enabled = false;
            }

            lblStatus.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btnEdit.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                Edit();

                this.Cursor = Cursors.Default;
                btnEdit.Enabled = true;
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

        private void Edit()
        {
            try
            {
                #region validation
                List<CashPromotionDiscountHeader> _thdr = CHNLSVC.Sales.GetPromotionalHeader(txtEditCircular.Text);
                if (_thdr == null || _thdr.Count <= 0)
                {
                    MessageBox.Show("Entered circular number is not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (dtEditFrom.Value < DateTime.Now.Date)
                //{
                //    MessageBox.Show("Can not edit from date less than current date records", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (dtEditTo.Value < DateTime.Now.Date)
                {
                    MessageBox.Show("Can not edit to date less than current date records", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtEditTo.Value < dtEditFrom.Value)
                {
                    MessageBox.Show("Can not edit to date has to be greater than to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                #endregion
                DataTable _itemList = new DataTable();
                _itemList.TableName = "dt";
                _itemList.Columns.Add("item");
                List<string> _locationList = new List<string>();
                foreach (ListViewItem _itm in lstItem.Items)
                {
                    if (_itm.Checked)
                    {
                        DataRow _dr = _itemList.NewRow();
                        _dr[0] = _itm.Text;
                        _itemList.Rows.Add(_dr);
                    }
                }
                foreach (ListViewItem _itm in lstPc.Items)
                {
                    if (_itm.Checked)
                    {
                        _locationList.Add(_itm.Text);
                    }
                }
                List<CashPromotionDiscountHeader> _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtEditCircular.Text);



                CHNLSVC.Sales.UpdatePromotionalDiscountDefinition(_itemList, _locationList, _hdr[0].Spdh_seq, BaseCls.GlbUserID, DateTime.Now, BaseCls.GlbUserComCode);

                decimal _disRate = 0;
                decimal _disVal = 0;
                if (rdoEditRate.Checked)
                {
                    _disRate = Convert.ToDecimal(txtEditDiscount.Text);
                }
                if (rdoEditValue.Checked)
                {
                    _disVal = Convert.ToDecimal(txtEditDiscount.Text);
                }
                if (_disRate < 0 || _disVal < 0)
                {
                    MessageBox.Show("Discount amount/value can not be minus", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CHNLSVC.General.UpdateCashPromotionalDiscountHdr(_hdr[0].Spdh_seq, dtEditFrom.Value, dtEditTo.Value, _disVal, _disRate, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                int StatusResult = CHNLSVC.Sales.DiscountDefinitionChangeStatus(txtCircular.Text, 2);

                MessageBox.Show("Successfully Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClear_Click(null, null);
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

        private void btnEditPc_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (cmbEditBussinssHir.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (cmbEditBussinssHir.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditPc;
                _CommonSearch.ShowDialog();
                txtEditPc.Focus();
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

        private void btnEditAddPc_Click(object sender, EventArgs e)
        {
            try
            {

                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (cmbEditBussinssHir.SelectedValue.ToString() == "COM")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtEditPc.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    //}
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtEditPc.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    }
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtEditPc.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    }
                }

                else if (cmbEditBussinssHir.SelectedValue.ToString() == "AREA")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtEditPc.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    //}
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "REGION")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtEditPc.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    //}
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "ZONE")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    //_result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    //if (txtEditPc.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    //}
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtEditPc.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtEditPc.Text);
                    }
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "GPC")
                {
                    MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    //_result = CHNLSVC.General.Get_GET_GPC("", "");
                    //if (txtEditPc.Text != "")
                    //{
                    //    _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtEditPc.Text, "");
                    //}
                }
                /*
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbEditBussinssHir.SelectedValue.ToString() == "COM") {
                    
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "CHNL") { 
                
                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "SCHNL")
                {

                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "AREA")
                {

                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "REGION")
                {

                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "PC")
                {

                }
                else if (cmbEditBussinssHir.SelectedValue.ToString() == "GPC")
                {

                }
                */
                //kapila 4/5/2017
                if (cmbEditBussinssHir.SelectedValue.ToString() == "PC")
                {
                    foreach (DataRow _dr in _result.Rows)
                    {
                        DataTable _loc = CHNLSVC.Sales.GetPcFromHirarchey(_dr["Code"].ToString(), cmbEditBussinssHir.SelectedValue.ToString());
                        foreach (DataRow _dr1 in _loc.Rows)
                        {
                            bool valid = true;
                            foreach (ListViewItem _itm in lstPc.Items)
                            {
                                if (_itm.Text == _dr1["MPI_PC_CD"].ToString())
                                {
                                    MessageBox.Show("PC " + _dr1["MPI_PC_CD"].ToString() + " already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    valid = false;
                                }
                            }
                            if (valid)
                                lstPc.Items.Add(_dr1["MPI_PC_CD"].ToString());
                        }

                    }
                }
                else
                {
                    bool valid = true;
                    foreach (ListViewItem _itm in lstPc.Items)
                    {
                        if (_itm.Text == txtEditPc.Text)
                        {
                            MessageBox.Show(cmbEditBussinssHir.SelectedValue.ToString() + " " + txtEditPc.Text +  " already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            valid = false;
                        }
                    }
                    if (valid)
                        lstPc.Items.Add(txtEditPc.Text);
                }
                if (_result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnEditBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditBrand;
                _CommonSearch.txtSearchbyword.Text = txtEditBrand.Text;
                _CommonSearch.ShowDialog();
                txtEditBrand.Focus();
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

        private void btnEditCat1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditCat1;
                _CommonSearch.txtSearchbyword.Text = txtEditCat1.Text;
                _CommonSearch.ShowDialog();
                txtEditCat1.Focus();
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

        private void btnEditCat2_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditCat2;
                _CommonSearch.txtSearchbyword.Text = txtEditCat2.Text;
                _CommonSearch.ShowDialog();
                txtEditCat2.Focus();
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

        private void btnEditCat3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditCat3;
                _CommonSearch.ShowDialog();
                txtEditCat3.Focus();
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

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditItem;
                _CommonSearch.ShowDialog();
                txtEditItem.Select();
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

        private void btnEditSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditSerial;
                _CommonSearch.ShowDialog();
                txtEditSerial.Select();
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

        private void btnEditPromotion_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtEditPromotion;
                _CommonSearch.txtSearchbyword.Text = txtEditPromotion.Text;
                _CommonSearch.ShowDialog();
                txtEditPromotion.Focus();
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

        private void btnEditAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                //categoryType.Add(3, "Item");
                //categoryType.Add(4, "Brand & Sub cat");
                //categoryType.Add(5, "Brand & Cat");
                //categoryType.Add(6, "Brand & main cat");
                //categoryType.Add(7, "Brand");
                //categoryType.Add(8, "Sub cat");
                //categoryType.Add(9, "Cat");
                //categoryType.Add(10, "Main cat");

                if (txtEditBrand.Text == "" && txtEditCat1.Text == "" && txtEditCat2.Text == "" && txtEditCat3.Text == "" && txtEditItem.Text == "")
                {
                    MessageBox.Show("Please select search term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbEditItemType.SelectedItem == null || cmbEditItemType.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbEditItemType.SelectedItem.ToString() == "BRAND_CATE1" || cmbEditItemType.SelectedItem.ToString() == "BRAND_CATE2")
                {
                    if (txtBrand.Text == string.Empty)
                    {
                        MessageBox.Show("Specify brand also!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string selection = "";
                if (cmbEditItemType.SelectedValue.ToString() == "10")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll("%", txtEditCat1.Text, "%", "%");
                    // lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "9")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll("%", "%", txtEditCat2.Text, "%");
                    //lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {

                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "8")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll("%", "%", "%", txtEditCat3.Text);
                    //lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {

                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "7")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll(txtEditBrand.Text, "%", "%", "%");
                    // lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {

                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "6")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll(txtEditBrand.Text, txtEditCat1.Text, "%", "%");
                    //lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //ListViewItem[] lst = lstItem.Items.Find(dr["MI_CD"].ToString(), true);
                        //if (lst != null && lst.Count() > 0)
                        //{
                        //    MessageBox.Show("Item " + dr["MI_CD"].ToString() + " already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //}
                        //else
                        //{
                        //    lstItem.Items.Add(dr["MI_CD"].ToString());
                        //}
                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "5")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll(txtEditBrand.Text, "%", txtEditCat2.Text, "%");
                    //lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {

                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "4")
                {
                    DataTable _dt = CHNLSVC.Inventory.GetItemByAll(txtEditBrand.Text, "%", "%", txtEditCat3.Text);
                    //lstItem.Items.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        ListViewItem _lst = lstItem.FindItemWithText(dr["MI_CD"].ToString());
                        if (_lst == null)
                            lstItem.Items.Add(dr["MI_CD"].ToString());
                        else
                        {
                            MessageBox.Show("Item - " + dr["MI_CD"].ToString() + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    if (_dt.Rows.Count <= 0)
                        MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "3")
                {
                    if (txtEditItem.Text == "")
                    {

                    }
                    else
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtEditItem.Text);
                        if (_item != null)
                        {
                            ListViewItem _lst = lstItem.FindItemWithText(txtEditItem.Text);
                            if (_lst == null)
                                lstItem.Items.Add(txtEditItem.Text);
                            else
                            {
                                MessageBox.Show("Item - " + txtEditItem.Text + " in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }


                        }
                        else
                        {
                            MessageBox.Show("Invalid search term,no records found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "2")
                {
                    selection = "PROMOTION";
                }
                else if (cmbEditItemType.SelectedValue.ToString() == "1")
                {
                    selection = "SERIAL";
                }


                txtItemCD.Text = "";
                txtCate1.Text = "";
                txtCate2.Text = "";
                txtCate3.Text = "";
                txtSerial.Text = "";
                txtPromotion.Text = "";


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

        private void btnEditDt_Click(object sender, EventArgs e)
        {
            try
            {

                /*
                 * get old circular no
                 * get change details
                 * insert new records
                 */
                #region validation
                List<CashPromotionDiscountHeader> _thdr = CHNLSVC.Sales.GetPromotionalHeader(txtEditCircular.Text.ToUpper());
                if (_thdr == null || _thdr.Count <= 0)
                {
                    MessageBox.Show("Entered circular number is not valid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (dtEditFrom.Value < DateTime.Now.Date)
                //{
                //    MessageBox.Show("Can not edit from date less than current date records", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (dtEditTo.Value < DateTime.Now.Date)
                {
                    MessageBox.Show("Can not edit to date less than current date records", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtEditTo.Value < dtEditFrom.Value)
                {
                    MessageBox.Show("Can not edit to date has to be greater than to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                #endregion
                List<string> _itemList = new List<string>();
                List<string> _locationList = new List<string>();
                foreach (ListViewItem _itm in lstItem.Items)
                {
                    if (_itm.Checked)
                    {
                        _itemList.Add(_itm.Text);
                    }
                }
                foreach (ListViewItem _itm in lstPc.Items)
                {
                    if (_itm.Checked)
                    {
                        _locationList.Add(_itm.Text);
                    }
                }
                decimal _disRate = 0;
                decimal _disVal = 0;
                if (rdoEditRate.Checked)
                {
                    _disRate = Convert.ToDecimal(txtEditDiscount.Text);
                }
                if (rdoEditValue.Checked)
                {
                    _disVal = Convert.ToDecimal(txtEditDiscount.Text);
                }
                if (_disRate < 0 || _disVal < 0)
                {
                    MessageBox.Show("Discount amount/value can not be minus", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string _error;
                int effect = CHNLSVC.General.DuplicateCashDiscount(txtEditCircular.Text, txtNewCircular.Text, dtEditFrom.Value, dtEditTo.Value, _disRate, _disVal, _itemList, _locationList, BaseCls.GlbUserID, DateTime.Now, BaseCls.GlbUserComCode, out _error);
                if (effect == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Error");
                }
                else
                {
                    MessageBox.Show("Records Duplicate successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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





            /*
            try
            {
                #region validation
                if (dtEditFrom.Value < DateTime.Now.Date)
                {
                    MessageBox.Show("Can not edit from date less than current date records", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtEditFrom.Value <= DateTime.Now.Date)
                {
                    MessageBox.Show("Can not edit from date less than or equal current  date records", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                List<CashPromotionDiscountHeader> _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtEditCircular.Text);
                if (_hdr != null && _hdr.Count > 0)
                {
                    decimal _disRate = 0;
                    decimal _disVal = 0;
                    if (rdoEditRate.Checked)
                    {
                        _disRate = Convert.ToDecimal(txtEditDiscount.Text);
                    }
                    if (rdoEditValue.Checked)
                    {
                        _disVal = Convert.ToDecimal(txtEditDiscount.Text);
                    }
                    CHNLSVC.Sales.UpdateCashPromotionalDiscountHdr(_hdr[0].Spdh_seq,dtEditFrom.Value,dtEditTo.Value,_disVal,_disRate);
                    MessageBox.Show("Dates Edited successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
             */
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtEditCircular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtEditFrom.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnEditCircular_Click(null, null);
            }
        }

        private void txtEditCircular_DoubleClick(object sender, EventArgs e)
        {
            btnEditCircular_Click(null, null);
        }

        private void txtEditDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }





        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btnBrLoc_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadLocations.Text = openFileDialog1.FileName;
        }

        private void btnLocUpload_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtUploadLocations.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadLocations.Text = "";
                txtUploadLocations.Focus();
                return;
            }

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadLocations.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadLocations.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }


            conStr = String.Format(conStr, txtUploadLocations.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            DataTable tem = new DataTable();
            tem.Columns.Add("code");
            foreach (DataRow _dr in dt.Rows)
            {
                //validation
                DataTable _result = null;
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());

                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());

                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());

                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());

                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());

                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", _dr[0].ToString());
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    _result = _basePage.CHNLSVC.General.Get_GET_GPC(_dr[0].ToString(), "");

                }

                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid " + DropDownListPartyTypes.Text.ToString() + " Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }



                DataRow dr = tem.NewRow();
                dr[0] = _dr[0].ToString();
                tem.Rows.Add(dr);


            }
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = tem;
            PCList = tem;
        }

        private void btnUploadItem_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            StringBuilder _errorLst = new StringBuilder();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            if (string.IsNullOrEmpty(txtUploadItems.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Text = "";
                txtUploadItems.Focus();
                return;
            }




            if (cmbSelectCat.SelectedValue.ToString() == "10")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "9")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "8")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "7")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "6")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "5")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "4")
            {
                MessageBox.Show("Can upload only Item,Serial and Promotion\nPlease select Type as Item or Serial or Promotion", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }


            conStr = String.Format(conStr, txtUploadItems.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in dt.Rows)
                {

                    if (string.IsNullOrEmpty(_dr[0].ToString()))
                    {
                        continue;
                    }

                    if (cmbSelectCat.SelectedValue.ToString() == "3")
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                        if (_item == null)
                        {
                            MessageBox.Show("Invalid Item - " + _dr[0].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "1")
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[1].ToString());
                        if (_item == null)
                        {
                            MessageBox.Show("Invalid Item - " + _dr[1].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        string _serial = "";
                        try
                        {
                            _serial = Convert.ToString(_dr[0]);
                        }
                        catch
                        {
                            MessageBox.Show("There is no serial available in the excel file. Please check.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }

                        if (string.IsNullOrEmpty(_serial))
                        {
                            MessageBox.Show("There is no serial available in the excel file. Please check. Item : " + _dr[1].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        var _dup = ItemBrandCat_List.Where(x => x.Sccd_itm == _dr[1].ToString() && x.Sccd_ser == _dr[0].ToString()).ToList();
                        if (_dup != null && _dup.Count > 0)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item and serial are " + _dr[1].ToString() + " duplicate");
                            else _errorLst.Append(" and item and serial are " + _dr[1].ToString() + " duplicate");
                            continue;
                        }
                    }


                    CashCommissionDetailRef _ref = new CashCommissionDetailRef();
                    _ref.Sccd_itm = _dr[0].ToString();
                    if (cmbSelectCat.SelectedValue.ToString() == "1")
                    {
                        _ref.Sccd_ser = _dr[1].ToString();
                    }
                    ItemBrandCat_List.Add(_ref);
                }
            }

            if (!string.IsNullOrEmpty(_errorLst.ToString()))
            {
                if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    ItemBrandCat_List = new List<CashCommissionDetailRef>();
                    grvSalesTypes.AutoGenerateColumns = false;
                    grvSalesTypes.DataSource = ItemBrandCat_List;
                    return;
                }
            }

            grvSalesTypes.AutoGenerateColumns = false;
            grvSalesType.DataSource = new List<CashCommissionDetailRef>();
            grvSalesTypes.DataSource = ItemBrandCat_List;
        }

        private void txtPromo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCircular.Text))
                {
                    List<CashPromotionDiscountHeader> _hdr = CHNLSVC.Sales.GetPromotionalHeader(txtCircular.Text);
                    if (_hdr == null || _hdr.Count == 0)
                    {
                        MessageBox.Show("Please Select saved Circular number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if ((MessageBox.Show("Do you want to approve", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    int result = CHNLSVC.Sales.ApprovePromotionlDiscount(txtCircular.Text, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                    //process
                    if (result > 0)
                    {
                        MessageBox.Show("Successfully Approved", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        pnlCancel.Visible = false;
                        pnlMain.Enabled = true;
                        txtCancelCircualr.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Error occurred while processing\n", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                {
                    MessageBox.Show("Please select circular no", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCircular.Focus();
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

        private void btnSearchMInvType_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtMInvType;
            _CommonSearch.ShowDialog();
            txtMInvType.Select();
        }

        private void btnExcelupload_Click(object sender, EventArgs e)
        {
            txtExcelUpload.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtExcelUpload.Text = openFileDialog1.FileName;
        }

        private void chkupload_Leave(object sender, EventArgs e)
        {
            if (chkupload.Checked)
            {
                txtExcelUpload.Enabled = true;
                btnExcelupload.Enabled = true;
                txtDiscount.Enabled = false;
                cmbSelectCat.SelectedValue = 3;
                cmbSelectCat.Enabled = false;
                txtCustomer.Enabled = false;
                panel12.Enabled = false;
                panel4.Enabled = false;
                dateTimePickerFrom.Enabled = false;
                dateTimePickerTo.Enabled = false;
            }
            else
            {
                txtExcelUpload.Enabled = false;
                btnExcelupload.Enabled = false;
                txtDiscount.Enabled = true;
                txtCustomer.Enabled = true;
                panel12.Enabled = true;
                cmbSelectCat.Enabled = true;
                panel4.Enabled = true;
                dateTimePickerFrom.Enabled = true;
                dateTimePickerTo.Enabled = true;
            }
        }
    }
}
