using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceParameters : Base
    {
        ServiceChnlDetails obj_service_chnl;
        ServiceChnlDetails obj_service_centers;
        ServiceChnlDetails obj_old_part;
        List<ServiceChnlDetails> _lstServiceCenters;
        List<ServiceChnlDetails> _lstMainStores;
        List<ServiceChnlDetails> _lstOldPart;
        private MasterItem _itemdetail = null;
        DataTable dtAll = new DataTable();
        bool isUpdate = false;
        private string _Stype = "";

        public ServiceParameters()
        {
            InitializeComponent();
        }

        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, BaseCls.GlbUserComCode);
        }
        private void load_Warranty_itemStatus(string plevel)
        {
            DataTable dtpl = CHNLSVC.Sales.Load_Item_dets(BaseCls.GlbUserComCode, plevel, txtWpricebook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                cmbItemStatus.DataSource = dtpl;
                cmbItemStatus.DisplayMember = "mis_desc";
                cmbItemStatus.ValueMember = "mis_cd";
                cmbItemStatus.SelectedIndex = -1;
            
            }
        }

        private void loadgrid(string chnl)
        {
            DataTable dtpl = CHNLSVC.General.get_serviceCenters(BaseCls.GlbUserComCode, chnl);
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                dgvServiceCenters.AutoGenerateColumns = false;
                dgvServiceCenters.DataSource = dtpl;

            }
        }


        
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {

                            paramsText.Append(BaseCls.GlbUserComCode + seperator);
                            break;
                        

                    }
                case CommonUIDefiniton.SearchUserControlType.DisVouTp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        if (_Stype == "Maintain")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtMBook.Text.Trim() + seperator);
                        }
                        else if (_Stype == "Warranty")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtWpricebook.Text.Trim() + seperator);
                        }

                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + txtMBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(string.Empty + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append(string.Empty + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
             
             
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
         
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular:
                //    {
                //        paramsText.Append(txtCircular_pv.Text + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular:
                    {
                        paramsText.Append(string.Empty + seperator + "Circular" + seperator);
                        break;
                    }


                default:
                    break;
            }

            return paramsText.ToString();
        }


        private void btnSrchChannel_Click(object sender, EventArgs e)
        {
            try
            {
               
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                //DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                DataTable _result = CHNLSVC.General.get_sub_chanels(_CommonSearch.SearchParams, null, null);
             
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.ShowDialog();
                txtChanel.Select();
                loadgrid(txtChanel.Text.Trim());
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchMBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMBook;
                _CommonSearch.ShowDialog();
                txtMBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchMLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Clear();
                    txtMBook.Focus();
                    return;
                }
                _Stype = "Maintain";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMLevel;
                _CommonSearch.ShowDialog();
                txtMLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchMInvType_Click(object sender, EventArgs e)
        {
             try
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        

        private void btnCurrencySearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCurrency;
                _CommonSearch.ShowDialog();
                txtCurrency.Select();
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

        private void btnItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCode;
                _CommonSearch.ShowDialog();
                txtItemCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnWpb_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtWpricebook;
                _CommonSearch.ShowDialog();
                txtWpricebook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnWplevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtWpricebook.Text))
                {
                    MessageBox.Show("Please select the warranty price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtWpricebook.Clear();
                    txtWpricebook.Focus();
                    return;
                }
                _Stype = "Warranty";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtWPlevel;
                _CommonSearch.ShowDialog();
                txtWPlevel.Select();
                load_Warranty_itemStatus(txtWPlevel.Text.Trim());

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



        private ServiceChnlDetails filltoService()
        {
            obj_service_chnl = new ServiceChnlDetails();
            obj_service_chnl.Company = BaseCls.GlbUserComCode;
            obj_service_chnl.Channel = txtChanel.Text.Trim();
            obj_service_chnl.CusCode = txtcusCode.Text.Trim();
            obj_service_chnl.InvType = txtMInvType.Text.Trim();
            obj_service_chnl.Pricebook = txtMBook.Text.Trim();
            obj_service_chnl.Pricelevel = txtMLevel.Text.Trim();
            obj_service_chnl.Currancy = txtCurrency.Text.Trim();
            obj_service_chnl.Warrnty_pb = txtWpricebook.Text.Trim();
            obj_service_chnl.Warrnty_pl = txtWPlevel.Text.Trim();
            obj_service_chnl.Itemcode = txtItemCode.Text.Trim();
            obj_service_chnl.Cre_user = BaseCls.GlbUserID;

            if(chkAllowWrkpro.Checked == true)
            {
                obj_service_chnl.IsWrkpro = 1;
            }
            else if (chkAllowWrkpro.Checked == false)
            {
                obj_service_chnl.IsWrkpro = 0;
            }
            obj_service_chnl.Ser1 = txtserial1.Text.Trim();
            if(chkAllowgatepass.Checked == true)
            {
                obj_service_chnl.IsAllow_gate_pass = 1;
            }
            else if (chkAllowgatepass.Checked == false)
            {
                obj_service_chnl.IsAllow_gate_pass = 0;
            }
            obj_service_chnl.Ser2 = txtserial2.Text.Trim();
            if(chkAllowSup.Checked == true)
            {
                obj_service_chnl.IsAllowSuper = 1;
            }
            else if (chkAllowSup.Checked == false)
            {
                obj_service_chnl.IsAllowSuper = 0;
            }
            obj_service_chnl.Ser3 = txtserial3.Text.Trim();
            if(chkCheckRec.Checked == true)
            {
                obj_service_chnl.Chk_recved = 1;
            }
            else if (chkCheckRec.Checked == false)
            {
                obj_service_chnl.Chk_recved = 0;
            }
            obj_service_chnl.Ser4 = txtserial4.Text.Trim();
            //obj_service_chnl.Item_stus = cmbItemStatus.SelectedValue.ToString();

            //obj_service_chnl.Item_stus = ((DataRowView)cmbItemStatus.SelectedItem)["mis_desc"].ToString();
            obj_service_chnl.Item_stus = cmbItemStatus.Text;

            string job = cmbJobScreen.Text;
            if (job == "Normal")
            {
                obj_service_chnl.JobScreen = 1;
            }
            else if (job == "Automobile")
            {
                obj_service_chnl.JobScreen = 2;
            }
            if (chkResetJob.Checked == true)
            {
                obj_service_chnl.IsReset_job = 1;
            }
            else if (chkResetJob.Checked == false)
            {
                obj_service_chnl.IsReset_job = 0;
            }
            obj_service_chnl.Slot = "";
            obj_service_chnl.ItemCode_inspection = "";


            return obj_service_chnl;

        }

        private void clearFields()
        {
            txtChanel.Text = "";
            txtcusCode.Text = "";
            txtCurrency.Text = "";
            txtItemCode.Text = "";
            txtMBook.Text = "";
            txtMInvType.Text = "";
            txtMLevel.Text = "";
            txtserial1.Text = "";
            txtserial2.Text = "";
            txtserial3.Text = "";
            txtserial4.Text = "";
            txtWPlevel.Text = "";
            txtWpricebook.Text = "";
            cmbItemStatus.SelectedIndex = -1;
            cmbJobScreen.SelectedIndex = -1;
            chkAllowgatepass.Checked = false;
            chkAllowSup.Checked = false;
            chkAllowWrkpro.Checked = false;
            chkCheckRec.Checked = false;
            chkResetJob.Checked = false;

            btnSave.Text = "Save";
            isUpdate = false;

            txtChanel.Enabled = true;
            btnSrchChannel.Enabled = true;


        }

        private void refreshGrid()
        {
            dtAll = CHNLSVC.General.getAllserviceChannels(BaseCls.GlbUserComCode);
            if (dtAll != null && dtAll.Rows.Count > 0)
            {
                dgvParaDetails.AutoGenerateColumns = false;
                dgvParaDetails.DataSource = dtAll;
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
             #region validation

            if (txtChanel.Text == "")
            {
                MessageBox.Show("Please select a channel", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMBook.Text.Trim() == "")
            {
                MessageBox.Show("Please enter default price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion

                if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                if (isUpdate == false)
                {
                    //bool chek = CHNLSVC.General.check_avl_chn(BaseCls.GlbUserComCode, txtChanCode.Text.Trim());
                    //if (chek == true)
                    //{
                    //    MessageBox.Show("Thease Records are already inserted!!!..Try Again..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtChanCode.Text = "";
                    //    txtNewChanel.Text = "";
                    //    return;
                    //}
                    string _error = "";
                    int result = CHNLSVC.General.Insert_to_serviceChnl_para(filltoService(), out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearFields();
                        refreshGrid();

                        btnSave.Text = "Save";
                        isUpdate = false;
                       
                    }
                }
                else
                {
                    string _error = "";
                    int result = CHNLSVC.General.Update_to_serviceChnl_para(filltoService(), out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records Updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearFields();
                        refreshGrid();

                        btnSave.Text = "Save";
                        isUpdate = false;

                    }


                }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void ServiceParameters_Load(object sender, EventArgs e)
        {
            refreshGrid();
        }

        private void dgvParaDetails_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                if (e.RowIndex != -1)
                {
                    DataRow[] foundRows;
                    DataTable hdt1 = null;
                    string chn = dgvParaDetails["clmChannel", e.RowIndex].Value.ToString();
                    string expression = "SP_SERCHNL like '%" + chn + "%'";
                    foundRows = dtAll.Select(expression);
                    if (foundRows.Count() > 0)
                    {
                        hdt1 = foundRows.CopyToDataTable<DataRow>();
                        DataRow dr = hdt1.Rows[0];
                        txtChanel.Text = dr["SP_SERCHNL"].ToString();

                        txtChanel.Enabled = false;
                        btnSrchChannel.Enabled = false;

                        txtMBook.Text = dr["SP_DEFPB"].ToString();
                        txtMLevel.Text = dr["SP_DEFPBLVL"].ToString();
                        txtMInvType.Text = dr["SP_DEFINVC_TP"].ToString();
                        if (Convert.ToInt32(dr["SP_RESETJOBMONTHLY"]) == 1)
                        {
                            chkResetJob.Checked = true;
                        }
                        else if (Convert.ToInt32(dr["SP_RESETJOBMONTHLY"]) == 0)
                        {
                            chkResetJob.Checked = false;
                        }


                        txtcusCode.Text = dr["SP_EXTERNALCUST"].ToString();
                        if (Convert.ToInt32(dr["SP_ISNEEDWIP"]) == 1)
                        {
                            chkAllowWrkpro.Checked = true;
                        }
                        else if (Convert.ToInt32(dr["SP_ISNEEDWIP"]) == 0)
                        {
                            chkAllowWrkpro.Checked = false;
                        }
                        txtCurrency.Text = dr["SP_DEFCURRENCY"].ToString();
                        txtItemCode.Text = dr["SP_CONSUMABLE_CD"].ToString();
                        if (Convert.ToInt32(dr["SP_ISNEEDGATEPASS"]) == 1)
                        {
                            chkAllowgatepass.Checked = true;

                        }
                        else if (Convert.ToInt32(dr["SP_ISNEEDGATEPASS"]) == 0)
                        {
                            chkAllowgatepass.Checked = false;
                        }
                        txtserial1.Text = dr["SP_DB_SERIAL"].ToString();
                        txtserial2.Text = dr["SP_DB_CHASSIS"].ToString();
                        if (Convert.ToInt32(dr["SP_ALOC_SUPERVISOR"]) == 1)
                        {
                            chkAllowSup.Checked = true;
                        }
                        else if (Convert.ToInt32(dr["SP_ALOC_SUPERVISOR"]) == 0)
                        {
                            chkAllowSup.Checked = false;
                        }
                        txtserial3.Text = dr["SP_DB_VEHI_REG"].ToString();
                        if (Convert.ToInt32(dr["SP_CHECK_OLDPART"]) == 1)
                        {
                            chkCheckRec.Checked = true;
                        }
                        else if (Convert.ToInt32(dr["SP_CHECK_OLDPART"]) == 0)
                        {
                            chkCheckRec.Checked = false;
                        }
                        txtserial4.Text = dr["SP_DB_MSN"].ToString();
                        txtWPlevel.Text = dr["SP_WARPBLVL"].ToString();
                        txtWpricebook.Text = dr["SP_WARPBK"].ToString();

                        cmbItemStatus.Text = dr["SP_WARRPBITEMTYPE"].ToString();
                        load_Warranty_itemStatus(txtWPlevel.Text);

                        if (Convert.ToInt32(dr["SP_JOB_SCREEN"]) == 1)
                        {
                            cmbJobScreen.Text = "Normal";
                        }
                        else
                        {
                            cmbJobScreen.Text = "Automobile";
                        }

                        isUpdate = true;
                        btnSave.Text = "Edit";


                    }


                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
                
        }

        private void cmbItemStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void cmbJobScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void btnMLocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.General.GetServiceLocation(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMlocCode;
                _CommonSearch.ShowDialog();
                txtMlocCode.Select();
            }

            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        
        }

        private void btnOlocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.General.GetServiceLocation(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOlocCode;
                _CommonSearch.ShowDialog();
                txtOlocCode.Select();
            }

            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void AddOldValToGrid()
        {
            try
            {
                string srv_cntr = null;


                foreach (DataGridViewRow itm in dgvServiceCenters.Rows)
                {
                    bool chkOption = Convert.ToBoolean(itm.Cells["option"].Value);
                    if (chkOption)
                    {

                        srv_cntr = itm.Cells["clmCode"].Value.ToString();
                    }
                }
                if (srv_cntr == null)
                {
                    MessageBox.Show("Please select the service center location.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (DataGridViewRow dgvr in dgvOldpart.Rows)
                {
                    if (srv_cntr == Convert.ToString(dgvr.Cells["clmOserCenter"].Value) && txtOlocCode.Text == Convert.ToString(dgvr.Cells["clmOcode"].Value))
                    {

                        MessageBox.Show("Thease values are Already Added.", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                dgvOldpart.Rows.Add();

                dgvOldpart["clmOserCenter", dgvOldpart.Rows.Count - 1].Value = srv_cntr;
                dgvOldpart["clmOcode", dgvOldpart.Rows.Count - 1].Value = txtOlocCode.Text.Trim();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }



        private void AddValueToGrid()
        {
            try
            {
                string srv_cntr = null;


                foreach (DataGridViewRow itm in dgvServiceCenters.Rows)
                {
                    bool chkOption = Convert.ToBoolean(itm.Cells["option"].Value);
                    if (chkOption)
                    {

                         srv_cntr = itm.Cells["clmCode"].Value.ToString();
                    }
                }
                if (srv_cntr == null)
                {
                    MessageBox.Show("Please select the service center location.", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }



                foreach (DataGridViewRow dgvr in dgvMainStores.Rows)
                {
                    if (srv_cntr == Convert.ToString(dgvr.Cells["clmserviceCenter"].Value) && txtMlocCode.Text == Convert.ToString(dgvr.Cells["clmMcode"].Value))
                    {

                        MessageBox.Show("Thease values are Already Added.", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                dgvMainStores.Rows.Add();

                dgvMainStores["clmserviceCenter", dgvMainStores.Rows.Count - 1].Value = srv_cntr;
                dgvMainStores["clmMcode", dgvMainStores.Rows.Count - 1].Value = txtMlocCode.Text.Trim();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }


        private void btnAddtogrid_Click(object sender, EventArgs e)
        {
            if (txtMlocCode.Text.Trim() == "")
            {
                MessageBox.Show("Please select a location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMlocCode.Focus();
                return;
            }
            //DataTable dt = new DataTable();
            //dt = CHNLSVC.General.getAllservice_locDetails(BaseCls.GlbUserComCode, txtMlocCode.Text.Trim(), null);
       
            //if (dt.Rows.Count > 0  )
            //{
            //    MessageBox.Show("This location already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtMlocCode.Focus();
            //    return;
            //}



            AddValueToGrid();
            txtMlocCode.Text = "";
        }

        private void dgvServiceCenters_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateCellValue(e.RowIndex);
        }

        private void UpdateCellValue(int CurrentRowIndex)
        {
            for (int row = 0; row < dgvServiceCenters.Rows.Count; row++)
            {
                if (CurrentRowIndex != row)
                {

                    dgvServiceCenters.Rows[row].Cells["option"].Value = false;
                }
                else
                {
                    dgvServiceCenters.Rows[row].Cells["option"].Value = true;
                }
            }

        }

        private void btnOAddgrid_Click(object sender, EventArgs e)
        {
            if (txtOlocCode.Text.Trim() == "")
            {
                MessageBox.Show("Please select a location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOlocCode.Focus();
                return;
            }
            DataTable dt = new DataTable();
            dt = CHNLSVC.General.getAllservice_locDetails(BaseCls.GlbUserComCode, null, txtOlocCode.Text.Trim());

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("This location already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOlocCode.Focus();
                return;
            }
            AddOldValToGrid();
            txtOlocCode.Text = "";
        }

        private List<ServiceChnlDetails> fillToServiceCenters()
        {
          

            _lstServiceCenters = new List<ServiceChnlDetails>();
            _lstMainStores = new List<ServiceChnlDetails>();
            _lstOldPart = new List<ServiceChnlDetails>();
            //save Main Stores Details
            foreach (DataGridViewRow dgvr in dgvMainStores.Rows)
            {

                      

                string type = "S";
                obj_service_centers = new ServiceChnlDetails();

                obj_service_centers.Company = BaseCls.GlbUserComCode;

                obj_service_centers.Itemcode = dgvr.Cells["clmserviceCenter"].Value.ToString();
                obj_service_centers.InvType = type;
                obj_service_centers.Ser1 = dgvr.Cells["clmMcode"].Value.ToString();
                _lstMainStores.Add(obj_service_centers);

            }
            //save OLD PART Details
            foreach (DataGridViewRow dgv in dgvOldpart.Rows)
            { 
                string type = "O";
                obj_old_part = new ServiceChnlDetails();

                obj_old_part.Company = BaseCls.GlbUserComCode;

                obj_old_part.Itemcode = dgv.Cells["clmOserCenter"].Value.ToString();
                obj_old_part.InvType = type;
                obj_old_part.Ser1 = dgv.Cells["clmOcode"].Value.ToString();
                _lstOldPart.Add(obj_old_part);

            }
            var result = _lstMainStores.Concat(_lstOldPart);
            _lstServiceCenters = result.ToList();





            if (_lstServiceCenters.Count <= 0)
            {
                MessageBox.Show("Please add Main and Old stores details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _lstServiceCenters;
        }

        private void btnSaveService_Click(object sender, EventArgs e)
        {
            if (dgvMainStores.Rows.Count > 0 && dgvOldpart.Rows.Count > 0)
            {
                _lstServiceCenters = new List<ServiceChnlDetails>();

 

                _lstServiceCenters = fillToServiceCenters();
                if (_lstServiceCenters.Count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    string _error = "";
                    int result = CHNLSVC.General.Insert_to_serviceloc(_lstServiceCenters, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnClearService_Click(null, null);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add item category details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearService_Click(object sender, EventArgs e)
        {
            dgvMainStores.Rows.Clear();
            dgvOldpart.Rows.Clear();
            dgvServiceCenters.DataSource = null;
            txtMlocCode.Text = "";
            txtOlocCode.Text = "";
        }

        private void btnPrintService_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "ServiceLocDetailsReport.rpt";
            BaseCls.GlbReportHeading = "Service Location Details";
            _view.Show();
            _view = null;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "ServiceChnlParaReport.rpt";
            BaseCls.GlbReportHeading = "Service Channel Parameter Details";
            _view.Show();
            _view = null;
        }


        private void loadServiceCenters()
        {
            cmbServiceCenters.SelectedIndexChanged -= new EventHandler(cmbServiceCenters_SelectedIndexChanged);
            DataTable dt = CHNLSVC.General.get_service_center_dets(BaseCls.GlbUserComCode);
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbServiceCenters.DataSource = dt;
                cmbServiceCenters.DisplayMember = "sll_scv_loc";
                cmbServiceCenters.ValueMember = "sll_scv_loc";
                cmbServiceCenters.SelectedIndex = -1;
                cmbServiceCenters.SelectedIndexChanged += new EventHandler(cmbServiceCenters_SelectedIndexChanged);
            }
            else
            {
                cmbServiceCenters.DataSource = null;
            }
        }

        private void loadTypeWiseLoc(string service_cent)
        {
          string  type;

            if (rdoMainStores.Checked)
            {
                type = "S";
            }
            else
            {
                type = "O";
            }


            DataTable dt = CHNLSVC.General.get_loc_services(BaseCls.GlbUserComCode, service_cent, type);
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbLocation.DataSource = dt;
                cmbLocation.DisplayMember = "sll_loc";
                cmbLocation.ValueMember = "sll_loc";
                cmbLocation.SelectedIndex = -1;
            }
            else
            {
                cmbLocation.DataSource = null;
            }
        }

        private void btnInActive_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Visible = true;
            loadServiceCenters();

        }

        private ServiceChnlDetails filltoInativation()
        {
            string typeIn;

            if (rdoMainStores.Checked)
            {
                typeIn = "S";
            }
            else
            {
                typeIn = "O";
            }

            obj_service_centers = new ServiceChnlDetails();
            obj_service_centers.Company = BaseCls.GlbUserComCode;
            obj_service_centers.CusCode = cmbServiceCenters.SelectedValue.ToString();
            obj_service_centers.InvType = typeIn;
            obj_service_centers.Itemcode = cmbLocation.SelectedValue.ToString();


            return obj_service_centers;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           

            #region validation

            if (cmbServiceCenters.SelectedIndex == -1)
            {
                MessageBox.Show("Please select cervice center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbLocation.SelectedIndex == -1)
            {
                MessageBox.Show("Please select location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            if (MessageBox.Show("Are you sure want to update ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string _error = "";
            int result = CHNLSVC.General.Update_to_InactiveServices(filltoInativation(), out _error);
            if (result == -1)
            {
                MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Records updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //btnClear_Click(null, null);
                cmbLocation.DataSource = null;
                cmbServiceCenters.DataSource = null;
                pnlCreateNew.Visible = false;
            }


        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Visible = false;
            cmbServiceCenters.DataSource = null;
            cmbLocation.DataSource = null;
        }

        private void rdoOldPart_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbServiceCenters.SelectedIndex != -1)
            {
                loadTypeWiseLoc(cmbServiceCenters.SelectedValue.ToString());
            }
        }

        private void rdoMainStores_CheckedChanged(object sender, EventArgs e)
        {
            if(cmbServiceCenters.SelectedIndex != -1)
            {
                loadTypeWiseLoc(cmbServiceCenters.SelectedValue.ToString());
            }
        }

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(cmbLocation.SelectedIndex != -1)
            {
            string type;

            if (rdoMainStores.Checked)
            {
                type = "S";
            }
            else
            {
                type = "O";
            }

            DataTable dt = CHNLSVC.General.get_Status_for_services(BaseCls.GlbUserComCode, cmbServiceCenters.SelectedValue.ToString(), type,cmbLocation.SelectedValue.ToString());
              if (dt != null && dt.Rows.Count > 0)
              {
                  int stus = Convert.ToInt32(dt.Rows[0][0]);
                  if (stus == 1)
                  {
                      chkinActNew.Text = "Active";
                      btnEdit.Text = "Inactive";
                  }
                  else
                  {
                      chkinActNew.Text = "Inactive";
                      btnEdit.Text = "Active";
                  }


              }
            }
        }

        private void cmbServiceCenters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServiceCenters.SelectedIndex != -1)
            {
              loadTypeWiseLoc(cmbServiceCenters.SelectedValue.ToString());
            }
        }

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSrchChannel_Click(null, null);
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnSrchChannel_Click(null, null);
            }

            if(e.KeyCode == Keys.Enter)
            {
                txtMBook.Focus();
            }
        }

        private void txtChanel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
               // DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                DataTable _result = CHNLSVC.General.get_sub_chanels(SearchParams, null, null);


                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtChanel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid channel", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtChanel.Clear();
                    txtChanel.Focus();
                    return;
                }
                loadgrid(txtChanel.Text.Trim());
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchMBook_Click(null, null);
        }

        private void txtMBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnSearchMBook_Click(null, null);
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtMLevel.Focus();
            }
        }

        private void txtMBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
              
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("BOOK") == txtMBook.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid price book", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMBook.Clear();
                    txtMBook.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchMLevel_Click(null, null);
        }

        private void txtMLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnSearchMLevel_Click(null, null);
            }

            if (e.KeyCode == Keys.Enter)
            {
                txtMInvType.Focus();
            }
        }

        private void dgvParaDetailstxtMLevel_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtMBook.Text == "")
                {
                    MessageBox.Show("Please Enter Price Book", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMLevel.Clear();
                    txtMBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMLevel.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("PRICE LEVEL") == txtMLevel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid price level", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMLevel.Clear();
                    txtMLevel.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMBook_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMLevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtWpricebook_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMInvType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchMInvType_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtcusCode.Focus();
            }
        }

        private void txtMInvType_DoubleClick(object sender, EventArgs e)
        {
            btnSearchMInvType_Click(null, null);
        }

        private void txtcusCode_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCustomer_Click(null, null);
        }

        private void txtcusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchCustomer_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtCurrency.Focus();
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtcusCode;
                _CommonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _CommonSearch.ShowDialog();
                txtcusCode.Select();



            }
            catch (Exception err)
            {
                txtcusCode.Clear();
                this.Cursor = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtcusCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCurrency_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCurrency_DoubleClick(object sender, EventArgs e)
        {
            btnCurrencySearch_Click(null, null);
        }

        private void txtCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnCurrencySearch_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtWpricebook.Focus();
            }

        }

        private void txtWpricebook_DoubleClick(object sender, EventArgs e)
        {
            btnWpb_Click(null, null);
        }

        private void txtWpricebook_MouseDown(object sender, MouseEventArgs e)
        {
         
        }

        private void txtWpricebook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnWpb_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtWPlevel.Focus();
            }
        }

        private void txtWPlevel_DoubleClick(object sender, EventArgs e)
        {
            btnWplevel_Click(null, null);
        }

        private void txtWPlevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnWplevel_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                cmbItemStatus.Focus();
            }
        }

        private void txtWPlevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnItemSearch_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                cmbJobScreen.Focus();
            }
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btnItemSearch_Click(null, null);
        }

        private void txtOlocCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnOlocSearch_Click(null, null);
            }
        }

        private void txtOlocCode_DoubleClick(object sender, EventArgs e)
        {
            btnOlocSearch_Click(null, null);
        }

        private void txtMlocCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnMLocSearch_Click(null, null);
            }
        }

        private void txtMlocCode_DoubleClick(object sender, EventArgs e)
        {
            btnMLocSearch_Click(null, null);
        }

        private void txtMlocCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMInvType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMInvType_Leave(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                if (string.IsNullOrEmpty(txtMInvType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("srtp_cd") == txtMInvType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid invoice type", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMInvType.Clear();
                    txtMInvType.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtcusCode_Leave(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtcusCode.Text)) return;

                MasterBusinessEntity custProf = GetbyCustCD(txtcusCode.Text.Trim().ToUpper());
                
                    if (custProf.Mbe_cd !=txtcusCode.Text.Trim().ToUpper())
                    {
                        MessageBox.Show("Please select the valid customer", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtcusCode.Clear();
                    txtcusCode.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCurrency_Leave(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                if (string.IsNullOrEmpty(txtCurrency.Text)) return;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(_CommonSearch.SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCurrency.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid currency", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCurrency.Clear();
                    txtCurrency.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtWpricebook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtWpricebook.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);

                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("BOOK") == txtWpricebook.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid price book", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWpricebook.Clear();
                    txtWpricebook.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtWPlevel_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtWpricebook.Text == "")
                {
                    MessageBox.Show("Please Enter Price Book", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWPlevel.Clear();
                    txtWpricebook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtWPlevel.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("PRICE LEVEL") == txtWPlevel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid price level", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWPlevel.Clear();
                    txtWPlevel.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {          
            try
            {           

   
                  
                if (string.IsNullOrEmpty(txtItemCode.Text)) return;

                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtItemCode.Text)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
                if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                  
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid item code", "Service Parameters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); txtItemCode.Clear();
                    txtItemCode.Focus();

                    return;
                }

          
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMlocCode_Leave(object sender, EventArgs e)
        {
            try
            {
                

                if (!string.IsNullOrEmpty(txtMlocCode.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtMlocCode.Text.ToString());
                    if (_masterLocation != null)
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid location code!", "Incorrect Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMlocCode.Clear();
                        txtMlocCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtOlocCode_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtOlocCode.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtOlocCode.Text.ToString());
                    if (_masterLocation != null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Invalid location code!", "Incorrect Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOlocCode.Clear();
                        txtOlocCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtserial1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtserial1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserial2.Focus();
            }
        }

        private void txtserial3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserial4.Focus();
            }
        }

        private void txtserial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserial3.Focus();
            }
        }

        private void txtserial4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkAllowSup.Focus();
            }
        }

        private void cmbItemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemCode.Focus();
            }
        }

        private void cmbJobScreen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbJobScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtserial1.Focus();
            }
        }

        private void chkSel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSel.Checked == true)
            {

                for (int i = 0; i < dgvServiceCenters.Rows.Count; i++)
                {

                    dgvServiceCenters["option", i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < dgvServiceCenters.Rows.Count; i++)
                {
                    dgvServiceCenters["option", i].Value = false;
                }
            }
        }

        private void dgvMainStores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
 
                    foreach (DataGridViewRow item in this.dgvMainStores.SelectedRows)
                    {
                        dgvMainStores.Rows.RemoveAt(item.Index);
                    }
                    if (dgvMainStores.Rows.Count ==1)
                    {
                        dgvMainStores.Rows.Clear();
                    }
                }
            }

            
        }

        private void dgvOldpart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow item in this.dgvOldpart.SelectedRows)
                    {
                        dgvOldpart.Rows.RemoveAt(item.Index);
                    }
                    if (dgvOldpart.Rows.Count == 1)
                    {
                        dgvOldpart.Rows.Clear();
                    }
                }
            }

        }

        private void dgvParaDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtOlocCode_TextChanged(object sender, EventArgs e)
        {

        }

     

  
      

        

    }
}
