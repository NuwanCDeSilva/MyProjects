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
using System.Linq.Expressions;
using System.Web.Services;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.HP
{
    public partial class SchemeCreation : Base
    {
        Boolean _isExsistSchDet = false;
        private List<HpSchemeSheduleDefinition> _SchemeShedule = new List<HpSchemeSheduleDefinition>();
        private List<HpSchemeDefinition> _schemeCommDef = new List<HpSchemeDefinition>();
        private List<HpSchemeDefinitionProcess> _schemeProcess = new List<HpSchemeDefinitionProcess>();
        private List<PriceDetailRef> _promoDetails = new List<PriceDetailRef>();
        private List<HPGurantorParam> _tempGur = new List<HPGurantorParam>();
        private List<HPGurantorParam> _finalGurParam = new List<HPGurantorParam>();
        private List<HpOtherCharges> _othChar = new List<HpOtherCharges>();
        private List<PriceBookLevelRef> _othPriceBook = new List<PriceBookLevelRef>();
        private List<HPResheScheme> _finalReShedule = new List<HPResheScheme>();
        private List<HpServiceCharges> _tmpSerChgList = new List<HpServiceCharges>();
        private List<HpServiceCharges> _finalserChgList = new List<HpServiceCharges>();
        private List<HpProofDoc> _tmpProofDoc = new List<HpProofDoc>();
        private List<HpSchemeDefinitionLog> _generalCir = new List<HpSchemeDefinitionLog>();
        private List<HPAddSchemePara> _addSchPara = new List<HPAddSchemePara>();
        private List<HPAddSchemePara> _addVouPara = new List<HPAddSchemePara>();
        private List<SchemetypeCom> _SchemetypeCom = new List<SchemetypeCom>();
        private Boolean _reCall = false;
        string _searchType = "";

        public SchemeCreation()
        {
            InitializeComponent();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Schema_category:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate:
                    {
                        paramsText.Append(txtCateCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllScheme:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllInactiveScheme:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        if (cmbCommDefType.Text == "Promotion Wise")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPromoBook.Text.Trim() + seperator);
                        }
                        else if (cmbCommDefType.Text == "Customer Wise")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtCusPriceBook.Text.Trim() + seperator);
                        }
                        else if (_searchType == "Other_Charge")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtOthBook.Text.Trim() + seperator);
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        if (_searchType == "Gur_Para")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtAppGurChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_searchType == "Ser_Para")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtSerAppChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        if (_searchType == "Gur_Para")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtAppGurChannel.Text + seperator + txtAppGurSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else if (_searchType == "Ser_Para")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtSerAppChannel.Text + seperator + txtSerAppSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        if (_searchType == "Other_Charge")
                        {
                            if (string.IsNullOrEmpty(txtOthMainCate.Text))
                            {
                                paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);

                            }
                            else
                            {
                                paramsText.Append(txtOthMainCate.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtMainCate.Text))
                            {
                                paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);

                            }
                            else
                            {
                                paramsText.Append(txtMainCate.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            }
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        if (_searchType == "Other_Charge")
                        {
                            paramsText.Append(txtOthMainCate.Text + seperator + txtOthSubCate.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        }
                        else
                        {
                            paramsText.Append(txtMainCate.Text + seperator + txtSubCate.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.promoCode:
                    {
                        paramsText.Append(txtPromoBook.Text + seperator + txtPromoLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularByBook:
                    {
                        paramsText.Append(txtPromoBook.Text + seperator + txtPromoLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularForSerial:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialForCircular:
                    {
                        paramsText.Append(txtSerialcir.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProofDoc:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SchByCir:
                    {
                        paramsText.Append(txtSchCircular.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.searchCircular:
                    {
                        paramsText.Append(null + seperator + "Circular" + txtPromoCir.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DisVouTp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void Clear_Data()
        {
            Clear_Main_Data();
            Clear_Shedule_Definition();
            Clear_Comm_Def();
            Clear_Gur_param();
            Clear_Oth_Charges();
            Clear_Service_Chg();
            Clear_Proof();
            Clear_Activation();
        }

        private void Clear_Proof()
        {
            _tmpProofDoc = new List<HpProofDoc>();
            txtProofDoc.Text = "";
            lblProofDoc.Text = "";
            chkMando.Checked = false;
            lstProofAllScheme.Clear();
            dgvProofDoc.Rows.Clear();

            dgvFinalProofDoc.AutoGenerateColumns = false;
            dgvFinalProofDoc.DataSource = new List<HpProofDoc>();
            txtProofDoc.Focus();
        }

        private void Clear_Service_Chg()
        {
            _tmpSerChgList = new List<HpServiceCharges>();
            _finalserChgList = new List<HpServiceCharges>();

            txtSerChgFromVal.Text = "";
            txtSerChgToVal.Text = "";
            txtSerChgAmt.Text = "";
            txtSerChgRate.Text = "";
            txtSerAppChannel.Text = "";
            txtSerAppSubChannel.Text = "";
            txtSerAppPC.Text = "";
            txtAppSerScheme.Text = "";
            lblAppSerIntRate.Text = "";
            lblAppSerTerm.Text = "";
            lblAppSerType.Text = "";
            txtMgrCommAmt.Text = "";
            txtMgrCommRate.Text = "";

            dgvSerChgValDef.AutoGenerateColumns = false;
            dgvSerChgValDef.DataSource = new List<HpServiceCharges>();

            dgvFinalSerChgDef.AutoGenerateColumns = false;
            dgvFinalSerChgDef.DataSource = new List<HpServiceCharges>();

            txtSerChgFromVal.Focus();
        }

        private void Clear_Oth_Charges()
        {
            cmbChargeType.Text = "Stamp Duty";
            txtOthBook.Text = "";
            txtOthLevel.Text = "";
            txtOthMainCate.Text = "";
            txtOthSubCate.Text = "";
            txtOthRange.Text = "";
            txtOthBrand.Text = "";
            txtOthItm.Text = "";
            _searchType = "";
            txtOthSchCode.Text = "";
            //lblOthIntRate.Text = "";
            lblOthSchTerm.Text = "";
            lblOthSchType.Text = "";
            _othChar = new List<HpOtherCharges>();
            _othPriceBook = new List<PriceBookLevelRef>();
            dgvFinalOthDetails.AutoGenerateColumns = false;
            dgvFinalOthDetails.DataSource = new List<HpOtherCharges>();

            dgvOthAppPBook.AutoGenerateColumns = false;
            dgvOthAppPBook.DataSource = new List<PriceBookLevelRef>();
            lstOthItem.Clear();
            lstOthSch.Clear();
        }

        private void Clear_Gur_param()
        {
            _tempGur = new List<HPGurantorParam>();
            dgvTempGurDef.AutoGenerateColumns = false;
            dgvTempGurDef.DataSource = new List<HPGurantorParam>();

            _finalGurParam = new List<HPGurantorParam>();
            dgvFinalGurParam.AutoGenerateColumns = false;
            dgvFinalGurParam.DataSource = new List<HPGurantorParam>();

            lstGurAppPc.Clear();
            lstGurSch.Clear();
            lblGurType.Text = "";
            lblGurTerm.Text = "";
            lblGurIntRt.Text = "";
            txtGurSch.Text = "";
            txtAppGurChannel.Text = "";
            txtAppGurSubChannel.Text = "";
            txtGurValFrom.Text = "";
            txtGurValTo.Text = "";
            txtNoofGur.Text = "";
            cmbGurApp.Text = "Profit Center";
            dtpGurFrom.Value = DateTime.Now.Date;
            dtpGurTo.Value = DateTime.Now.Date;
            txtAppGurPc.Text = "";
        }

        private void Clear_New_Scheme_Data()
        {
            txtAddRental.Text = "";
            chkInsuApp.Checked = false;
            chkAllowGS.Checked = false;
            chkSchStatus.Checked = false;
            cmbFPayType.Text = "RATE";
            txtFPayValue.Text = "";
            chkFPayCalWithVAT.Checked = false;
            chkIntWithVAT.Checked = false;
            chkIntWithService.Checked = false;
            chkIntWithStampDuty.Checked = false;
            chkIntWithInsu.Checked = false;
            chkIntWithInsu.Enabled = false;
            cmbAddType.Text = "RATE";
            txtAddValue.Text = "";
            chkAddCalWithVAT.Checked = false;
            cmbDisType.Text = "RATE";
            txtDisAmount.Text = "";
            chkCommCalVAT.Checked = false;
            txtHPInsuTerm.Text = "";
            txtVehInsuTerm.Text = "";
            txtVehInsuCollectTerm.Text = "";
            chkDisVou.Checked = false;
            chkSpeCusBase.Checked = false;
            chkVouMan.Checked = false;
            lblVouMan.Visible = false;
            chkVouMan.Visible = false;
            chkCC.Checked = false;
            txtCCDays.Text = "0";
            txtCCDays.Enabled = false;

            groupBox16.Enabled = false;
            lstVou.Items.Clear();

            _addSchPara = new List<HPAddSchemePara>();
            _addVouPara = new List<HPAddSchemePara>();

            dgvCusPara.AutoGenerateColumns = false;
            dgvCusPara.DataSource = new List<HPAddSchemePara>();

            groupBox17.Enabled = false;


            // groupBox17.Enabled = false;
            // groupBox16.Enabled = false;
            _isExsistSchDet = false;
        }

        private void Clear_Main_Data()
        {
            txtCateCode.Text = "";
            txtType.Text = "";
            lblCateDesc.Text = "";
            lblTypeDesc.Text = "";
            txtNewType.Text = "";
            txtNewType.Enabled = false;
            txtNewTypeDesc.Text = "";
            txtNewTypeRate.Text = "0.00";
            chkNewtypeActive.Checked = false;
            btnTypeSave.Enabled = false;
            chkIntWithVAT.Enabled = false;
            btnTypeUpdate.Enabled = true;
            txtTerm.Text = "";
            txtSchCode.Text = "";
            txtDesc.Text = "";
            txtIntRate.Text = "";
            txtHPInsuTerm.Enabled = false;
            cmbFPayType.Text = "VALUE";
            groupBox4.Enabled = true;
            chkIntWithVAT.Enabled = true;
            txtHPInsuTerm.Enabled = true;
            txtHPInsuTerm.Text = "";
            chkResSameInv.Checked = false;
            chkRevert.Checked = false;
            chkIntWithInsu.Enabled = true;
            chkIntWithStampDuty.Enabled = true;
            chkIntWithService.Enabled = true;
            //chkIsRed.Checked = false;
            cmbIntMethod.Text = "EQUAL";
            groupBox6.Enabled = true;
            grdCmpny.Rows.Clear();
            Clear_New_Scheme_Data();
            grdCmpny.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            groupBox19.Visible = false;
            chkCommServechg.Checked = false; //Tharindu 2018-07-01
            txtCateCode.Focus();
            txtFPayValue1.Text = "";
            chkFPayCalWithVAT1.Checked = false;
            cmbFPayType1.Text = "VALUE";
        }


        private void SchemeCreation_Load(object sender, EventArgs e)
        {
            Clear_Data();
            groupBox19.Visible = false;
            
        }

        private void btnCateSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemaCategory(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCateCode;
                _CommonSearch.ShowDialog();
                txtCateCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCateCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
                    DataTable _result = CHNLSVC.CommonSearch.GetSchemaCategory(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCateCode;
                    _CommonSearch.ShowDialog();
                    txtCateCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtType.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCateCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemaCategory(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCateCode;
                _CommonSearch.ShowDialog();
                txtCateCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCateCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCateCode.Text))
            {
                DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies(txtCateCode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    lblCateDesc.Text = Convert.ToString(dt.Rows[0]["hsc_desc"]);

                    if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                    {
                        chkFPayCalWithVAT.Checked = false;
                        cmbFPayType.Text = "VALUE";
                        txtFPayValue.Text = "0";
                        groupBox4.Enabled = false;
                        chkIntWithVAT.Checked = false;
                        chkIntWithVAT.Enabled = false;

                        chkIntWithInsu.Checked = true;
                        chkIntWithService.Checked = true;
                        chkIntWithStampDuty.Checked = true;

                        chkIntWithInsu.Enabled = false;
                        chkIntWithStampDuty.Enabled = false;
                        chkIntWithService.Enabled = true;
                        cmbAddType.Text = "RATE";
                        txtAddValue.Text = "0";
                        chkAddCalWithVAT.Checked = true;
                        groupBox6.Enabled = false;
                    }
                    else
                    {

                        cmbFPayType.Text = "VALUE";
                        groupBox4.Enabled = true;
                        chkIntWithVAT.Enabled = true;
                        chkIntWithInsu.Checked = false;
                        chkIntWithService.Checked = false;
                        chkIntWithStampDuty.Checked = false;
                        chkIntWithInsu.Enabled = true;
                        chkIntWithStampDuty.Enabled = true;
                        chkIntWithService.Enabled = true;
                        cmbAddType.Text = "VALUE";
                        txtAddValue.Text = "";
                        chkAddCalWithVAT.Checked = false;
                        groupBox6.Enabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("Invalid category selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCateCode.Text = "";
                    lblCateDesc.Text = "";
                    txtCateCode.Focus();
                }
            }
        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
         
                groupBox19.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                grdCmpny.Visible = true;
              
            

            txtType.Text = "";
            txtNewType.Enabled = true;
            txtNewType.Text = "";
            txtNewTypeDesc.Text = "";
            txtNewTypeRate.Text = "0";
            chkNewtypeActive.Checked = true;
            btnTypeSave.Enabled = true;
            btnTypeUpdate.Enabled = false;
            lblTypeDesc.Text = "";
            txtNewType.Focus();
            groupBox19.Visible = true;
            Load_compny();


        }

        private void btnTypeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCateCode.Text))
                {
                    MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtType.Text = "";
                    txtCateCode.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemaTypeByCate(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtType;
                _CommonSearch.ShowDialog();
                txtType.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtCateCode.Text))
                    {
                        MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtType.Text = "";
                        txtCateCode.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate);
                    DataTable _result = CHNLSVC.CommonSearch.GetSchemaTypeByCate(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtType;
                    _CommonSearch.ShowDialog();
                    txtType.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtNewTypeDesc.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCateCode.Text))
                {
                    MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtType.Text = "";
                    txtCateCode.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemaTypeByCate(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtType;
                _CommonSearch.ShowDialog();
                txtType.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtType.Text))
                {
                    //if (string.IsNullOrEmpty(txtCateCode.Text))
                    //{
                    //    MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtType.Text = "";
                    //    txtCateCode.Text = "";
                    //    txtCateCode.Focus();
                    //    return;
                    //}

                    HpSchemeType _Type = new HpSchemeType();
                    _Type = CHNLSVC.Sales.getSchemeType(txtType.Text.Trim());

                    if (_Type.Hst_cd != null)
                    {
                        txtCateCode.Text = _Type.Hst_sch_cat;
                        txtType.Text = _Type.Hst_cd;
                        lblTypeDesc.Text = _Type.Hst_desc;
                        txtNewType.Text = _Type.Hst_cd;
                        txtNewTypeDesc.Text = _Type.Hst_desc;
                        txtNewTypeRate.Text = _Type.Hst_def_intr.ToString("n");
                        chkNewtypeActive.Checked = _Type.Hst_act;
                        txtNewType.Enabled = false;
                        btnTypeSave.Enabled = false;
                        btnTypeUpdate.Enabled = true;

                        DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies(txtCateCode.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            lblCateDesc.Text = Convert.ToString(dt.Rows[0]["hsc_desc"]);
                        }

                        if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                        {
                            chkFPayCalWithVAT.Checked = false;
                            cmbFPayType.Text = "VALUE";
                            txtFPayValue.Text = "0";
                            groupBox4.Enabled = false;
                            chkIntWithVAT.Checked = false;
                            chkIntWithVAT.Enabled = false;
                            chkIntWithInsu.Checked = true;
                            chkIntWithService.Checked = true;
                            chkIntWithStampDuty.Checked = true;
                            chkIntWithInsu.Enabled = false;
                            chkIntWithStampDuty.Enabled = false;
                            chkIntWithService.Enabled = false;
                            cmbAddType.Text = "RATE";
                            txtAddValue.Text = "0";
                            chkAddCalWithVAT.Checked = true;
                            groupBox6.Enabled = false;
                        }
                        else
                        {

                            cmbFPayType.Text = "VALUE";
                            groupBox4.Enabled = true;
                            chkIntWithVAT.Enabled = true;
                            chkIntWithInsu.Checked = false;
                            chkIntWithService.Checked = false;
                            chkIntWithStampDuty.Checked = false;
                            chkIntWithInsu.Enabled = true;
                            chkIntWithStampDuty.Enabled = true;
                            chkIntWithService.Enabled = true;
                            cmbAddType.Text = "VALUE";
                            txtAddValue.Text = "";
                            chkAddCalWithVAT.Checked = false;
                            groupBox6.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid scheme type selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtType.Text = "";
                        txtType.Focus();
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

        }

        private void btnTypeUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               

                if (string.IsNullOrEmpty(txtCateCode.Text))
                {
                    MessageBox.Show("Please select category code.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCateCode.Focus();
                    txtNewType.Text = "";
                    txtType.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(txtNewType.Text))
                {
                    MessageBox.Show("Please select valid type to update.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewType.Text = "";
                    txtType.Text = "";
                    txtType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewTypeDesc.Text))
                {
                    MessageBox.Show("Type description is missing.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeDesc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewTypeRate.Text))
                {
                    MessageBox.Show("Intrest rate cannot be blank.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "0";
                    txtNewTypeRate.Focus();
                    return;
                }

                if (!IsNumeric(txtNewTypeRate.Text))
                {
                    MessageBox.Show("Intrest rate should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "0";
                    txtNewTypeRate.Focus();
                    return;
                }
                if (!IsNumeric(txtNewTypeRate.Text))
                {
                    MessageBox.Show("Intrest rate should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "0";
                    txtNewTypeRate.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtNewTypeRate.Text.ToString()) > 100 || Convert.ToDecimal(txtNewTypeRate.Text.ToString()) < 0)
                {
                    MessageBox.Show("Intrest should between 0 and 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "0";
                    txtNewTypeRate.Focus();
                    return;
                }
                if (MessageBox.Show("Do you need to add this Record?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateSchemeType();
                }
                
             
               
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSchemeType()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            string _newSchType = "";

            HpSchemeType _SaveSchemeType = new HpSchemeType();
            _SaveSchemeType.Hst_act = chkNewtypeActive.Checked;
            _SaveSchemeType.Hst_cd = txtNewType.Text.Trim();
            _SaveSchemeType.Hst_def_intr = Convert.ToDecimal(txtNewTypeRate.Text);
            _SaveSchemeType.Hst_desc = txtNewTypeDesc.Text.Trim();
            _SaveSchemeType.Hst_sch_cat = txtCateCode.Text.Trim();
            _newSchType = txtNewType.Text.Trim();

            row_aff = (Int32)CHNLSVC.Sales.SaveNewSchemeType(_SaveSchemeType);

            if (row_aff == 1)
            {

                MessageBox.Show("Successfully created.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
                txtNewType.Text = _newSchType;
                HpSchemeType _Type = new HpSchemeType();
                _Type = CHNLSVC.Sales.getSchemeType(txtNewType.Text.Trim());

                if (_Type.Hst_cd != null)
                {
                    txtCateCode.Text = _Type.Hst_sch_cat;
                    txtType.Text = _Type.Hst_cd;
                    lblTypeDesc.Text = _Type.Hst_desc;
                    txtNewType.Text = _Type.Hst_cd;
                    txtNewTypeDesc.Text = _Type.Hst_desc;
                    txtNewTypeRate.Text = _Type.Hst_def_intr.ToString("n");
                    chkNewtypeActive.Checked = _Type.Hst_act;
                    btnTypeSave.Enabled = false;
                    btnTypeUpdate.Enabled = false;
                    txtNewType.Enabled = false;
                    DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies(txtCateCode.Text.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        lblCateDesc.Text = Convert.ToString(dt.Rows[0]["hsc_desc"]);
                    }
                    if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                    {
                        chkFPayCalWithVAT.Checked = false;
                        cmbFPayType.Text = "VALUE";
                        txtFPayValue.Text = "0";
                        groupBox4.Enabled = false;
                        chkIntWithVAT.Checked = false;
                        chkIntWithVAT.Enabled = false;
                        chkIntWithInsu.Checked = true;
                        chkIntWithService.Checked = true;
                        chkIntWithStampDuty.Checked = true;
                        chkIntWithInsu.Enabled = false;
                        chkIntWithStampDuty.Enabled = false;
                        chkIntWithService.Enabled = false;
                        cmbAddType.Text = "RATE";
                        txtAddValue.Text = "0";
                        chkAddCalWithVAT.Checked = true;
                        groupBox6.Enabled = false;
                    }
                    else
                    {

                        cmbFPayType.Text = "VALUE";
                        groupBox4.Enabled = true;
                        chkIntWithVAT.Enabled = true;
                        chkIntWithInsu.Checked = false;
                        chkIntWithService.Checked = false;
                        chkIntWithStampDuty.Checked = false;
                        chkIntWithInsu.Enabled = true;
                        chkIntWithStampDuty.Enabled = true;
                        chkIntWithService.Enabled = true;
                        cmbAddType.Text = "VALUE";
                        txtAddValue.Text = "";
                        chkAddCalWithVAT.Checked = false;
                        groupBox6.Enabled = true;
                    }
                }


                // Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void UpdateSchemeType()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            HpSchemeType _UpdateSchemeType = new HpSchemeType();
            _UpdateSchemeType.Hst_act = chkNewtypeActive.Checked;
            _UpdateSchemeType.Hst_cd = txtNewType.Text.Trim();
            _UpdateSchemeType.Hst_def_intr = Convert.ToDecimal(txtNewTypeRate.Text);
            _UpdateSchemeType.Hst_desc = txtNewTypeDesc.Text.Trim();
            _UpdateSchemeType.Hst_sch_cat = txtCateCode.Text.Trim();

            row_aff = (Int32)CHNLSVC.Sales.UpdateSchemeType(_UpdateSchemeType, BaseCls.GlbUserID, 12);
            saveSchemaTocompany();

            if (row_aff == 1)
            {

                MessageBox.Show("Successfully updated.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Data();
                // Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtNewType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNewType.Text))
                {
                    //if (string.IsNullOrEmpty(txtCateCode.Text))
                    //{
                    //    MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtType.Text = "";
                    //    txtCateCode.Text = "";
                    //    txtNewType.Text = "";
                    //    txtCateCode.Focus();
                    //    return;
                    //}

                    HpSchemeType _Type = new HpSchemeType();
                    _Type = CHNLSVC.Sales.getSchemeType(txtNewType.Text.Trim());

                    if (_Type.Hst_cd != null)
                    {
                        MessageBox.Show("Enter type code is already exsist.Please enter new code.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCateCode.Text = _Type.Hst_sch_cat;
                        txtType.Text = _Type.Hst_cd;
                        lblTypeDesc.Text = _Type.Hst_desc;
                        txtNewType.Text = _Type.Hst_cd;
                        txtNewTypeDesc.Text = _Type.Hst_desc;
                        txtNewTypeRate.Text = _Type.Hst_def_intr.ToString("n");
                        chkNewtypeActive.Checked = _Type.Hst_act;
                        btnTypeSave.Enabled = false;
                        btnTypeUpdate.Enabled = false;
                        txtNewType.Enabled = false;
                        DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies(txtCateCode.Text.Trim());
                        if (dt.Rows.Count > 0)
                        {
                            lblCateDesc.Text = Convert.ToString(dt.Rows[0]["hsc_desc"]);
                        }

                        if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                        {
                            chkFPayCalWithVAT.Checked = false;
                            cmbFPayType.Text = "VALUE";
                            txtFPayValue.Text = "0";
                            groupBox4.Enabled = false;
                            chkIntWithVAT.Checked = false;
                            chkIntWithVAT.Enabled = false;
                            chkIntWithInsu.Checked = true;
                            chkIntWithService.Checked = true;
                            chkIntWithStampDuty.Checked = true;
                            chkIntWithInsu.Enabled = false;
                            chkIntWithStampDuty.Enabled = false;
                            chkIntWithService.Enabled = false;
                            cmbAddType.Text = "RATE";
                            txtAddValue.Text = "0";
                            chkAddCalWithVAT.Checked = true;
                            groupBox6.Enabled = false;
                        }
                        else
                        {

                            cmbFPayType.Text = "VALUE";
                            groupBox4.Enabled = true;
                            chkIntWithVAT.Enabled = true;
                            chkIntWithInsu.Checked = false;
                            chkIntWithService.Checked = false;
                            chkIntWithStampDuty.Checked = false;
                            chkIntWithInsu.Enabled = true;
                            chkIntWithStampDuty.Enabled = true;
                            chkIntWithService.Enabled = true;
                            cmbAddType.Text = "VALUE";
                            txtAddValue.Text = "";
                            chkAddCalWithVAT.Checked = false;
                            groupBox6.Enabled = true;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTypeClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to Clear this Record?", "Clear...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtType.Text = "";
                txtNewType.Text = "";
                txtNewTypeDesc.Text = "";
                txtNewTypeRate.Text = "0";
                chkNewtypeActive.Checked = true;
                btnTypeSave.Enabled = false;
                btnTypeUpdate.Enabled = true;
                lblTypeDesc.Text = "";
                lblCateDesc.Text = "";
                txtCateCode.Text = "";
                txtType.Focus();
            }
        }

        private void txtNewType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNewTypeDesc.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewTypeDesc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNewTypeRate.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNewTypeRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    chkNewtypeActive.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkNewtypeActive_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (btnTypeSave.Enabled == true)
                    {
                        btnTypeSave.Focus();
                    }
                    else
                    {
                        btnTypeUpdate.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTypeSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCateCode.Text))
                {
                    MessageBox.Show("Please select category code.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCateCode.Focus();
                    txtNewType.Text = "";
                    txtType.Text = "";
                    return;
                }

                if (string.IsNullOrEmpty(txtNewType.Text))
                {
                    MessageBox.Show("Please select valid type to update.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewType.Text = "";
                    txtType.Text = "";
                    txtType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewTypeDesc.Text))
                {
                    MessageBox.Show("Type description is missing.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeDesc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNewTypeRate.Text))
                {
                    MessageBox.Show("Intrest rate cannot be blank.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "0";
                    txtNewTypeRate.Focus();
                    return;
                }

                if (!IsNumeric(txtNewTypeRate.Text))
                {
                    MessageBox.Show("Intrest rate should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "0";
                    txtNewTypeRate.Focus();
                    return;
                }

                SaveSchemeType();
                saveSchemaTocompany();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTerm_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTerm.Text))
                {
                    if (!IsNumeric(txtTerm.Text))
                    {
                        MessageBox.Show("Term should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTerm.Text = "";
                        txtTerm.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtType.Text))
                    {
                        MessageBox.Show("Please select scheme type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTerm.Text = "";
                        txtType.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtNewType.Text))
                    {
                        MessageBox.Show("Please select scheme type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTerm.Text = "";
                        txtType.Focus();
                        return;
                    }

                    txtSchCode.Text = txtNewType.Text + Convert.ToInt32(txtTerm.Text).ToString("00");

                    HpSchemeDetails _SchDetails = new HpSchemeDetails();
                    _SchDetails = CHNLSVC.Sales.getSchemeDetByCode(txtSchCode.Text.Trim());
                    _isExsistSchDet = false;

                    if (_SchDetails.Hsd_cd == null)
                    {
                        _isExsistSchDet = false;
                        btnMainSave.Text = "Save";
                        txtDesc.Text = txtNewTypeDesc.Text + " " + Convert.ToInt32(txtTerm.Text).ToString("00") + " " + "MONTHS";
                        txtIntRate.Text = (Convert.ToDecimal(txtNewTypeRate.Text) / 12 * Convert.ToInt32(txtTerm.Text)).ToString("n");
                        chkInsuApp.Checked = false;
                        chkAllowGS.Checked = false;
                        //chkIsRed.Checked = false;
                        cmbIntMethod.Text = "EQUAL";
                        //Clear_New_Scheme_Data();
                        if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                        {
                            chkFPayCalWithVAT.Checked = false;
                            cmbFPayType.Text = "VALUE";
                            txtFPayValue.Text = "0";
                            groupBox4.Enabled = false;
                            chkIntWithVAT.Checked = false;
                            chkIntWithVAT.Enabled = false;

                            chkIntWithInsu.Checked = true;
                            chkIntWithService.Checked = true;
                            chkIntWithStampDuty.Checked = true;

                            chkIntWithInsu.Enabled = false;
                            chkIntWithStampDuty.Enabled = false;
                            chkIntWithService.Enabled = false;
                            cmbAddType.Text = "RATE";
                            txtAddValue.Text = "0";
                            chkAddCalWithVAT.Checked = true;
                            groupBox6.Enabled = false;
                        }
                        else
                        {

                            cmbFPayType.Text = "VALUE";
                            groupBox4.Enabled = true;
                            chkIntWithVAT.Enabled = true;
                            chkIntWithInsu.Checked = false;
                            chkIntWithService.Checked = false;
                            chkIntWithStampDuty.Checked = false;
                            chkIntWithInsu.Enabled = true;
                            chkIntWithStampDuty.Enabled = true;
                            chkIntWithService.Enabled = true;
                            cmbAddType.Text = "VALUE";
                            txtAddValue.Text = "";
                            chkAddCalWithVAT.Checked = false;
                            groupBox6.Enabled = true;
                        }

                    }
                    else
                    {

                        if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                        {
                            chkFPayCalWithVAT.Checked = false;
                            cmbFPayType.Text = "VALUE";
                            txtFPayValue.Text = "0";
                            groupBox4.Enabled = false;
                            chkIntWithVAT.Checked = false;
                            chkIntWithVAT.Enabled = false;

                            chkIntWithInsu.Checked = true;
                            chkIntWithService.Checked = true;
                            chkIntWithStampDuty.Checked = true;

                            chkIntWithInsu.Enabled = false;
                            chkIntWithStampDuty.Enabled = false;
                            chkIntWithService.Enabled = false;
                            cmbAddType.Text = "RATE";
                            txtAddValue.Text = "0";
                            chkAddCalWithVAT.Checked = true;
                            groupBox6.Enabled = false;
                        }
                        else
                        {

                            cmbFPayType.Text = "VALUE";
                            groupBox4.Enabled = true;
                            chkIntWithVAT.Enabled = true;
                            chkIntWithInsu.Checked = false;
                            chkIntWithService.Checked = false;
                            chkIntWithStampDuty.Checked = false;
                            chkIntWithInsu.Enabled = true;
                            chkIntWithStampDuty.Enabled = true;
                            chkIntWithService.Enabled = true;
                            cmbAddType.Text = "VALUE";
                            txtAddValue.Text = "";
                            chkAddCalWithVAT.Checked = false;
                            groupBox6.Enabled = true;
                        }

                        _isExsistSchDet = true;
                        btnMainSave.Text = "Update";
                        txtDesc.Text = _SchDetails.Hsd_desc;
                        txtIntRate.Text = _SchDetails.Hsd_intr_rt.ToString("n");
                        txtAddRental.Text = _SchDetails.Hsd_noof_addrnt.ToString("0");
                        chkInsuApp.Checked = _SchDetails.Hsd_has_insu;
                        chkAllowGS.Checked = _SchDetails.Hsd_alw_gs;
                        chkDisVou.Checked = _SchDetails.Hsd_alw_vou;
                        chkSpeCusBase.Checked = _SchDetails.Hsd_alw_cus;
                        chkSchStatus.Checked = _SchDetails.Hsd_act;
                        //chkIsRed.Checked = _SchDetails.Hsd_is_red;
                        //Tharindu 2018-07-01
                       if(_SchDetails._hsd_is_com_ser_chg == 1) 
                       {
                           chkCommServechg.Checked = true;
                       }
                       else
                       {
                           chkCommServechg.Checked = false;
                       }

                      
                        if (_SchDetails.Hsd_is_red == true)
                        {
                            cmbIntMethod.Text = "REDUCING";
                        }
                        else
                        {
                            cmbIntMethod.Text = "EQUAL";
                        }

                        chkVouMan.Checked = _SchDetails.Hsd_vou_man;

                        if (_SchDetails.Hsd_is_rt == true)
                        {
                            cmbFPayType1.Text = "RATE";
                        }
                        else
                        {
                            cmbFPayType1.Text = "VALUE";
                        }
                        txtFPayValue1.Text = _SchDetails.Hsd_fpay.ToString("n");
                        chkFPayCalWithVAT1.Checked = _SchDetails.Hsd_fpay_calwithvat;
                        chkIntWithVAT.Checked = _SchDetails.Hsd_fpay_withvat;
                        chkIntWithService.Checked = _SchDetails.Hsd_init_serchg;
                        chkIntWithInsu.Checked = _SchDetails.Hsd_init_insu;
                        chkIntWithStampDuty.Checked = _SchDetails.Hsd_init_sduty;

                        if (_SchDetails.Hsd_add_is_rt == true)
                        {
                            cmbAddType.Text = "RATE";
                        }
                        else
                        {
                            cmbAddType.Text = "VALUE";
                        }
                        txtAddValue.Text = _SchDetails.Hsd_add_rnt.ToString("0");
                        chkAddCalWithVAT.Checked = _SchDetails.Hsd_add_calwithvat;

                        if (_SchDetails.Hsd_dis_isrt == true)
                        {
                            cmbDisType.Text = "RATE";
                        }
                        else
                        {
                            cmbDisType.Text = "VALUE";
                        }
                        txtDisAmount.Text = _SchDetails.Hsd_dis.ToString("n");
                        chkCommCalVAT.Checked = _SchDetails.Hsd_comm_on_vat;
                        txtHPInsuTerm.Text = _SchDetails.Hsd_insu_term.ToString("0");
                        txtVehInsuTerm.Text = _SchDetails.Hsd_veh_insu_term.ToString("0");
                        txtVehInsuCollectTerm.Text = _SchDetails.Hsd_veh_insu_col_term.ToString("0");
                        chkRevert.Checked = _SchDetails.Hsd_is_rvt == 1 ? true : false;
                        chk_specVou.Checked = _SchDetails.Hsd_spc_vou == 1 ? true : false;
                        optInvoie.Checked = _SchDetails.Hsd_vou_gen == 1 ? true : false;

                        if (_SchDetails.Hsd_alw_vou == true)
                        {
                            List<HPAddSchemePara> _vouDet = new List<HPAddSchemePara>();
                            _vouDet = CHNLSVC.Sales.GetAddParaDetails("VOU", txtSchCode.Text.Trim());
                            if (_vouDet != null)
                            {
                                foreach (HPAddSchemePara _tmpVou in _vouDet)
                                {
                                    lstVou.Items.Add(_tmpVou.Hap_cd);
                                }
                            }

                            foreach (ListViewItem Item in lstVou.Items)
                            {
                                Item.Checked = true;
                            }
                        }

                        if (_SchDetails.Hsd_alw_cus == true)
                        {
                            _addSchPara = new List<HPAddSchemePara>();
                            _addSchPara = CHNLSVC.Sales.GetAddParaDetails("CUS", txtSchCode.Text.Trim());

                            dgvCusPara.AutoGenerateColumns = false;
                            dgvCusPara.DataSource = new List<HPAddSchemePara>();
                            dgvCusPara.DataSource = _addSchPara;

                        }

                        if (_SchDetails.Hsd_spc_vou == 1)
                        {
                            _addSchPara = new List<HPAddSchemePara>();
                            _addSchPara = CHNLSVC.Sales.GetAddParaDetails("VOU", txtSchCode.Text.Trim());

                            if (_addSchPara != null)
                            {
                                foreach (HPAddSchemePara _tmpschpara in _addSchPara)
                                {
                                    txtvourental.Text = _tmpschpara.Hap_val6.ToString();
                                    lstVou.Items.Add(_tmpschpara.Hap_cd);
                                }
                            }
                        }

                        //if (txtCateCode.Text == "S003" || txtCateCode.Text == "S004")
                        //{
                        //    groupBox4.Enabled = false;
                        //    chkIntWithVAT.Enabled = false;
                        //    chkIntWithInsu.Enabled = false;
                        //    chkIntWithStampDuty.Enabled = false;
                        //    chkIntWithService.Enabled = false;
                        //    groupBox6.Enabled = false;
                        //}
                        //else
                        //{
                        //    groupBox4.Enabled = true;
                        //    chkIntWithVAT.Enabled = true;
                        //    chkIntWithInsu.Enabled = true;
                        //    chkIntWithStampDuty.Enabled = true;
                        //    chkIntWithService.Enabled = true;
                        //    groupBox6.Enabled = true;
                        //}


                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnMainSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCateCode.Text))
                {
                    MessageBox.Show("Please select category code.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCateCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtType.Text))
                {
                    MessageBox.Show("Please select scheme type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtType.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtNewType.Text))
                {
                    MessageBox.Show("Please select scheme type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtTerm.Text))
                {
                    MessageBox.Show("Please enter term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTerm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbIntMethod.Text))
                {
                    MessageBox.Show("Please select intrest calculation method.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbIntMethod.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSchCode.Text))
                {
                    MessageBox.Show("Scheme code is missing.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTerm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDesc.Text))
                {
                    MessageBox.Show("Scheme description is missing.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDesc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtIntRate.Text))
                {
                    MessageBox.Show("Please enter intrest rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIntRate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAddRental.Text))
                {
                    MessageBox.Show("Please enter additional rental.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddRental.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFPayValue1.Text))
                {
                    MessageBox.Show("Please enter first pay value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFPayValue.Focus();
                    return;
                } //Tharindu 2018-07-01 comment due to req

                if (string.IsNullOrEmpty(txtAddValue.Text))
                {
                    MessageBox.Show("Please enter additional rental value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddValue.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    //MessageBox.Show("Please enter discount amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisAmount.Focus();
                    //return;
                    txtDisAmount.Text = "0";
                }

                if (string.IsNullOrEmpty(txtHPInsuTerm.Text))
                {
                    MessageBox.Show("Please enter HP insuarance term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHPInsuTerm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtVehInsuTerm.Text))
                {
                    MessageBox.Show("Please enter vehicle insuarance term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVehInsuTerm.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtVehInsuCollectTerm.Text))
                {
                    MessageBox.Show("Please enter vehicle insuarance collecting term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVehInsuCollectTerm.Focus();
                    return;
                }
             

                if (chkDisVou.Checked == true || chk_specVou.Checked == true)
                {
                    Boolean _isValidVou = false;
                    foreach (ListViewItem Item in lstVou.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidVou = true;
                            goto L3;
                        }
                    }
                L3:

                    if (_isValidVou == false)
                    {
                        MessageBox.Show("No any applicable vouchers are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (chkSpeCusBase.Checked == true)
                {
                    if (dgvCusPara.Rows.Count <= 0)
                    {
                        MessageBox.Show("No any applicable customer parameters are setup.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                _addVouPara = new List<HPAddSchemePara>();

                if (chkDisVou.Checked == true || chk_specVou.Checked == true)
                {
                    foreach (ListViewItem VouList in lstVou.Items)
                    {
                        string _vou = VouList.Text;

                        //if (VouList.Checked == true)
                        //{
                        HPAddSchemePara _tmpVouDet = new HPAddSchemePara();
                        _tmpVouDet.Hap_com = BaseCls.GlbUserComCode;
                        _tmpVouDet.Hap_sch = txtSchCode.Text.Trim();
                        _tmpVouDet.Hap_tp = "VOU";
                        _tmpVouDet.Hap_cd = _vou.Trim();
                        _tmpVouDet.Hap_frm = DateTime.Now.Date;
                        _tmpVouDet.Hap_to = DateTime.Now.Date;
                        _tmpVouDet.Hap_val1 = 0;
                        _tmpVouDet.Hap_val6 = Convert.ToInt32(txtvourental.Text);
                        if (VouList.Checked == true)
                        {
                            _tmpVouDet.Hap_val2 = 1;
                        }
                        else
                        {
                            _tmpVouDet.Hap_val2 = 0;
                        }

                        _addVouPara.Add(_tmpVouDet);
                        //}
                    }
                }


                if (_isExsistSchDet == false)
                {
                    CreateNewScheme(true);
                }
                else
                {
                    CreateNewScheme(false);
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void CreateNewScheme(Boolean _isNew)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            HpSchemeDetails _collectSaveDet = new HpSchemeDetails();
            _collectSaveDet.Hsd_act = false;//chkSchStatus.Checked;
            _collectSaveDet.Hsd_add_calwithvat = chkAddCalWithVAT.Checked;
            if (cmbAddType.Text == "RATE")
            {
                _collectSaveDet.Hsd_add_is_rt = true;
            }
            else
            {
                _collectSaveDet.Hsd_add_is_rt = false;
            }
            _collectSaveDet.Hsd_add_rnt = Convert.ToDecimal(txtAddValue.Text);
            _collectSaveDet.Hsd_alw_gs = chkAllowGS.Checked;
            _collectSaveDet.Hsd_cd = txtSchCode.Text.Trim();
            _collectSaveDet.Hsd_comm_on_vat = chkCommCalVAT.Checked;
            _collectSaveDet.Hsd_cre_by = BaseCls.GlbUserID;
            _collectSaveDet.Hsd_cre_dt = DateTime.Now.Date;
            _collectSaveDet.Hsd_def_intr = Convert.ToDecimal(txtNewTypeRate.Text);
            _collectSaveDet.Hsd_desc = txtDesc.Text.Trim();

            if (cmbDisType.Text == "RATE")
            {
                _collectSaveDet.Hsd_dis_isrt = true;
            }
            else
            {
                _collectSaveDet.Hsd_dis_isrt = false;
            }
            _collectSaveDet.Hsd_dis = Convert.ToDecimal(txtDisAmount.Text);

            if (cmbFPayType1.Text == "RATE")
            {
                _collectSaveDet.Hsd_is_rt = true;
            }
            else
            {
                _collectSaveDet.Hsd_is_rt = false;
            }

            _collectSaveDet.Hsd_fpay = Convert.ToDecimal(txtFPayValue1.Text);
            _collectSaveDet.Hsd_fpay_calwithvat = chkFPayCalWithVAT1.Checked;
            _collectSaveDet.Hsd_fpay_withvat = chkIntWithVAT.Checked; //Tharindu 2018-07-01 comment due to req
            _collectSaveDet.Hsd_has_insu = chkInsuApp.Checked;
            _collectSaveDet.Hsd_init_insu = chkIntWithInsu.Checked;
            _collectSaveDet.Hsd_init_sduty = chkIntWithStampDuty.Checked;
            _collectSaveDet.Hsd_init_serchg = chkIntWithService.Checked;
            _collectSaveDet.Hsd_insu_term = Convert.ToInt32(txtHPInsuTerm.Text);
            _collectSaveDet.Hsd_intr_rt = Convert.ToDecimal(txtIntRate.Text);
            _collectSaveDet.Hsd_noof_addrnt = Convert.ToInt32(txtAddRental.Text);
            _collectSaveDet.Hsd_pty_cd = "GRUP01";
            _collectSaveDet.Hsd_pty_tp = "GPC";
            _collectSaveDet.Hsd_sch_tp = txtNewType.Text.Trim();
            _collectSaveDet.Hsd_term = Convert.ToInt32(txtTerm.Text);
            _collectSaveDet.Hsd_veh_insu_col_term = Convert.ToInt32(txtVehInsuCollectTerm.Text);
            _collectSaveDet.Hsd_veh_insu_term = Convert.ToInt32(txtVehInsuTerm.Text);
            _collectSaveDet.Hsd_alw_vou = chkDisVou.Checked;
            _collectSaveDet.Hsd_alw_cus = chkSpeCusBase.Checked;
            _collectSaveDet.Hsd_vou_man = chkVouMan.Checked;

            _collectSaveDet.Hsd_is_rvt = chkRevert.Checked ? 1 : 0;
            _collectSaveDet.Hsd_spc_vou = chk_specVou.Checked ? 1 : 0;
            _collectSaveDet.Hsd_vou_gen = optInvoie.Checked ? 1 : 2;

            _collectSaveDet._hsd_is_com_ser_chg = chkCommServechg.Checked ? 1 : 0; //Tharindu 2018-07-01 
            _collectSaveDet.Hsd_fpay_withvat = chkIntWithVAT.Checked ? true : false;
            

            if (cmbIntMethod.Text == "REDUCING")
            {
                _collectSaveDet.Hsd_is_red = true;
            }
            else
            {
                _collectSaveDet.Hsd_is_red = false;
            }

            if (_isNew == true)
            {
                row_aff = CHNLSVC.Sales.CreateNewSchemeDetails(_collectSaveDet, _addVouPara, _addSchPara);
            }
            else
            {
                row_aff = CHNLSVC.Sales.UpdateExsistSchemeDetails(_collectSaveDet, _addVouPara, _addSchPara);
            }

            if (row_aff == 1)
            {
                if (_isNew == true)
                {
                    MessageBox.Show("Successfully created new scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Successfully updated selected scheme details.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    MessageBox.Show(_msg, "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Faild to update.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void btnMainClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to Clear this Record?", "Clear...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Clear_Main_Data();
            }
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();

        }

        private void Check_Integer(TextBox txtName, string TextName)
        {
            int number = 0;

            if (Convert.ToInt32(txtName.Text) < 0)
            {
                MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = "";
                txtName.Focus();
                return;
            }

            if (int.TryParse(txtName.Text.Trim(), out number))
            {
                //textBox value is a number
            }
            else
            {
                //not a number
                MessageBox.Show("Please enter correct value for " + TextName, "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = "";
                txtName.Focus();
            }
        }

        private void txtFPayValue_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFPayValue.Text))
            {
                if (!IsNumeric(txtFPayValue.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFPayValue.Text = "";
                    txtFPayValue.Focus();
                    return;
                }

                if (cmbFPayType.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtFPayValue.Text) > 100 || Convert.ToDecimal(txtFPayValue.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFPayValue.Text = "";
                        txtFPayValue.Focus();
                        return;
                    }

                }
                else
                {
                    if (Convert.ToDecimal(txtFPayValue.Text) < 0)
                    {
                        MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFPayValue.Text = "";
                        txtFPayValue.Focus();
                        return;
                    }
                }

                txtFPayValue.Text = Convert.ToDecimal(txtFPayValue.Text).ToString("n");
            }
        }

        private void txtAddRental_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddRental.Text))
            {
                if (!string.IsNullOrEmpty(txtTerm.Text))        //kapila 14/9/2015
                {
                    if (Convert.ToInt16(txtTerm.Text) < Convert.ToInt16(txtAddRental.Text))
                    {
                        MessageBox.Show("No of additional rental cannot be exceed scheme term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddRental.Text = "";
                        txtAddRental.Focus();
                        return;
                    }
                }
                Check_Integer(txtAddRental, "No of Additional Rental");


            }

        }

        private void cmbFPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFPayValue.Text = "";
        }

        private void txtHPInsuTerm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtHPInsuTerm.Text))
            {
                if (Convert.ToInt16(txtTerm.Text) < Convert.ToInt16(txtHPInsuTerm.Text))
                {
                    MessageBox.Show("HP insuarance term cannot be exceed scheme term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHPInsuTerm.Text = "";
                    txtHPInsuTerm.Focus();
                    return;
                }



                Check_Integer(txtHPInsuTerm, "HP insuaance term");
            }
        }

        private void txtVehInsuTerm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVehInsuTerm.Text))
            {
                if (Convert.ToInt16(txtTerm.Text) < Convert.ToInt16(txtVehInsuTerm.Text))
                {
                    MessageBox.Show("HP insuarance term cannot be exceed scheme term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVehInsuTerm.Text = "";
                    txtVehInsuTerm.Focus();
                    return;
                }


                Check_Integer(txtVehInsuTerm, "Vehicle insuarance term");

            }
        }

        private void txtVehInsuCollectTerm_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVehInsuCollectTerm.Text))
            {
                if (Convert.ToInt16(txtTerm.Text) < Convert.ToInt16(txtVehInsuCollectTerm.Text))
                {
                    MessageBox.Show("HP insuarance term cannot be exceed scheme term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVehInsuCollectTerm.Text = "";
                    txtVehInsuCollectTerm.Focus();
                    return;
                }


                Check_Integer(txtVehInsuCollectTerm, "Vehicle insuarance collecting term");
            }
        }

        private void txtAddValue_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddValue.Text))
            {
                if (!IsNumeric(txtAddValue.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddValue.Text = "";
                    txtAddValue.Focus();
                    return;
                }

                if (cmbAddType.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtAddValue.Text) > 100 || Convert.ToDecimal(txtAddValue.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddValue.Text = "";
                        txtAddValue.Focus();
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtAddValue.Text) < 0)
                    {
                        MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddValue.Text = "";
                        txtAddValue.Focus();
                        return;
                    }
                }

                txtAddValue.Text = Convert.ToDecimal(txtAddValue.Text).ToString("n");
            }
        }

        private void txtDisAmount_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisAmount.Text))
            {
                if (!IsNumeric(txtDisAmount.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisAmount.Text = "";
                    txtDisAmount.Focus();
                    return;
                }

                if (cmbDisType.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtDisAmount.Text) > 100 || Convert.ToDecimal(txtDisAmount.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisAmount.Text = "";
                        txtDisAmount.Focus();
                        return;
                    }

                }
                else
                {
                    if (Convert.ToDecimal(txtDisAmount.Text) < 0)
                    {
                        MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisAmount.Text = "";
                        txtDisAmount.Focus();
                        return;
                    }
                }
                txtDisAmount.Text = Convert.ToDecimal(txtDisAmount.Text).ToString("n");
            }
        }

        private void chkInsuApp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInsuApp.Checked == true)
            {
                chkIntWithInsu.Checked = false;
                chkIntWithInsu.Enabled = true;
                txtHPInsuTerm.Enabled = true;
                txtHPInsuTerm.Text = "";
            }
            else
            {
                chkIntWithInsu.Checked = false;
                chkIntWithInsu.Enabled = false;
                txtHPInsuTerm.Text = "0";
                txtHPInsuTerm.Enabled = false;
            }
        }

        private void btnSearchSheSchemes_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSheSchCode;
                _CommonSearch.ShowDialog();
                txtSheSchCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSheSchCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSheSchCode;
                _CommonSearch.ShowDialog();
                txtSheSchCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSheSchCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSheSchCode;
                    _CommonSearch.ShowDialog();
                    txtSheSchCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    cmbSheSchBase.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear_Shedule_Definition()
        {
            rbUser.Checked = true;
            txtSheSchCode.Text = "";
            lblSheSchDesc.Text = "";
            lblSheSchType.Text = "";
            lblsheSchTerm.Text = "";
            lblSheSchDefInt.Text = "";
            lblSheSchInt.Text = "";
            lblSheDefRental.Text = "";
            txtSheDefAmount.Text = "";
            txtSelectReScheme.Text = "";
            txtvourental.Text = "";

            dgvShedule.AutoGenerateColumns = false;
            dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();

            _SchemeShedule = new List<HpSchemeSheduleDefinition>();
            _finalReShedule = new List<HPResheScheme>();

            dgvResheSch.AutoGenerateColumns = false;
            dgvResheSch.DataSource = new List<HPResheScheme>();

            btnSheDefAdd.Enabled = true;
            txtSheSchCode.Focus();
        }

        private void rbSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSystem.Checked == true)
            {
                txtSheSchCode.Text = "";
                lblSheSchDesc.Text = "";
                txtSheSchCode.Enabled = false;
                btnSearchSheSchemes.Enabled = false;
                lblSheSchType.Text = "";
                lblsheSchTerm.Text = "";
                lblSheSchDefInt.Text = "";
                lblSheSchInt.Text = "";
                lblSheDefRental.Text = "";
                txtSheDefAmount.Text = "";

                dgvShedule.AutoGenerateColumns = false;
                dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();
            }
            else
            {
                txtSheSchCode.Text = "";
                lblSheSchDesc.Text = "";
                txtSheSchCode.Enabled = true;
                btnSearchSheSchemes.Enabled = true;
                lblSheSchType.Text = "";
                lblsheSchTerm.Text = "";
                lblSheSchDefInt.Text = "";
                lblSheSchInt.Text = "";
                lblSheDefRental.Text = "";
                txtSheDefAmount.Text = "";

                dgvShedule.AutoGenerateColumns = false;
                dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();
                txtSheSchCode.Focus();
            }
        }

        private void btnSheClear_Click(object sender, EventArgs e)
        {
            Clear_Shedule_Definition();
        }

        private void txtSheSchCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSheSchCode.Text))
                {
                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtSheSchCode.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSheSchCode.Text = "";
                        txtSheSchCode.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSheSchCode.Text = "";
                        txtSheSchCode.Focus();
                        return;
                    }

                    lblSheSchDesc.Text = _tmpSch.Hsd_desc;
                    lblSheSchType.Text = _tmpSch.Hsd_sch_tp;
                    lblsheSchTerm.Text = _tmpSch.Hsd_term.ToString("0"); ;
                    lblSheSchDefInt.Text = _tmpSch.Hsd_def_intr.ToString("n");
                    lblSheSchInt.Text = _tmpSch.Hsd_intr_rt.ToString("n");
                    lblSheDefRental.Text = "1";

                    HpSchemeType _Type = new HpSchemeType();
                    _Type = CHNLSVC.Sales.getSchemeType(lblSheSchType.Text.Trim());

                    if (_Type.Hst_sch_cat == "S003" || _Type.Hst_sch_cat == "S004")
                    {
                        MessageBox.Show("Not allow to define user define shedule for scheme category of : " + _Type.Hst_sch_cat, "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSheDefAdd.Enabled = false;
                        //return;
                    }
                    else
                    {
                        btnSheDefAdd.Enabled = true;
                    }






                    //load if define shedule is exsist
                    _SchemeShedule = new List<HpSchemeSheduleDefinition>();
                    _SchemeShedule = CHNLSVC.Sales.Get_Define_Scheme_Shedule(txtSheSchCode.Text.Trim());

                    if (_SchemeShedule != null)
                    {
                        if (_SchemeShedule.Count == Convert.ToInt16(lblsheSchTerm.Text))
                        {
                            lblSheDefRental.Text = lblsheSchTerm.Text;
                            btnSheDefAdd.Enabled = false;
                        }
                    }

                    dgvShedule.AutoGenerateColumns = false;
                    dgvShedule.DataSource = new List<HpSchemeDefinition>();
                    dgvShedule.DataSource = _SchemeShedule;

                    //load reshedule allow schmes----
                    _finalReShedule = new List<HPResheScheme>();
                    List<HPResheScheme> _CurrentAcitveList = new List<HPResheScheme>();
                    _finalReShedule = CHNLSVC.Sales.getAllowSch(txtSheSchCode.Text);
                    _CurrentAcitveList = CHNLSVC.Sales.getAllowSch(txtSheSchCode.Text);
                    //dgvResheSch.AutoGenerateColumns = false;
                    //dgvResheSch.DataSource = new List<HPResheScheme>();
                    //dgvResheSch.DataSource = _finalReShedule;



                    List<HpSchemeDetails> _tmpSchList = new List<HpSchemeDetails>();
                    _tmpSchList = CHNLSVC.Sales.getAllActiveSchemes(txtSheSchCode.Text);

                    HPResheScheme _newList = new HPResheScheme();

                    if (_finalReShedule == null)
                    {
                        _finalReShedule = new List<HPResheScheme>();
                    }

                    foreach (HpSchemeDetails _tmp in _tmpSchList)
                    {
                        _newList = new HPResheScheme();
                        _newList.Hsr_sch_cd = txtSheSchCode.Text.Trim();
                        _newList.Hsr_rsch_cd = _tmp.Hsd_cd;
                        _finalReShedule.Add(_newList);
                    }



                    dgvResheSch.AutoGenerateColumns = false;
                    dgvResheSch.DataSource = new List<HPResheScheme>();
                    dgvResheSch.DataSource = _finalReShedule;


                    if (_CurrentAcitveList != null)
                    {
                        foreach (HPResheScheme _chk in _CurrentAcitveList)
                        {
                            foreach (DataGridViewRow row in dgvResheSch.Rows)
                            {
                                string _curSch = row.Cells["col_R_othSch"].Value.ToString();
                                if (_curSch == _chk.Hsr_rsch_cd)
                                {
                                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                                    if (Convert.ToBoolean(chk.Value) == false)
                                    {
                                        chk.Value = true;
                                        goto L1;
                                    }
                                }

                            }
                        L1: Int16 x = 1;
                        }
                    }

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSheSchBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSheDefRental.Text = "1";

            _SchemeShedule = new List<HpSchemeSheduleDefinition>();
            dgvShedule.AutoGenerateColumns = false;
            dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();
        }

        private void btnSheDefAdd_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _totRate = 0;

                if (string.IsNullOrEmpty(cmbSheSchBase.Text))
                {
                    MessageBox.Show("Please select definition type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSheSchBase.Focus();
                    return;
                }

                if (Convert.ToInt32(lblSheDefRental.Text) == Convert.ToInt32(lblsheSchTerm.Text))
                {
                    MessageBox.Show("Definition is completed.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtSheDefAmount.Text))
                {
                    MessageBox.Show("Please enter rate / amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSheDefAmount.Focus();
                    return;
                }

                if (!IsNumeric(txtSheDefAmount.Text))
                {
                    MessageBox.Show("Please enter valid amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSheDefAmount.Text = "";
                    txtSheDefAmount.Focus();
                    return;
                }

                if (cmbSheSchBase.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtSheDefAmount.Text) > 100)
                    {
                        MessageBox.Show("Please enter valid rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSheDefAmount.Text = "";
                        txtSheDefAmount.Focus();
                        return;
                    }

                    _totRate = 0;

                    if (_SchemeShedule != null)
                    {
                        foreach (HpSchemeSheduleDefinition _tmp in _SchemeShedule)
                        {
                            _totRate = _totRate + _tmp.Hss_rnt;
                        }
                    }

                    _totRate = _totRate + Convert.ToDecimal(txtSheDefAmount.Text);

                    if (_totRate > 100)
                    {
                        MessageBox.Show("Rate is going to more than 100 %.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSheDefAmount.Focus();
                        return;
                    }

                }

                HpSchemeSheduleDefinition _tmpSch = new HpSchemeSheduleDefinition();
                _tmpSch.Hss_seq = 0;
                _tmpSch.Hss_sch_cd = txtSheSchCode.Text.Trim();
                _tmpSch.Hss_rnt_no = Convert.ToInt32(lblSheDefRental.Text);
                _tmpSch.Hss_rnt = Convert.ToDecimal(txtSheDefAmount.Text);
                if (cmbSheSchBase.Text == "RATE")
                {
                    _tmpSch.Hss_is_rt = true;
                }
                else
                {
                    _tmpSch.Hss_is_rt = false;
                }
                _tmpSch.Hss_cre_by = BaseCls.GlbUserID;
                _tmpSch.Hss_cre_dt = DateTime.Now.Date;

                if (_SchemeShedule == null)
                {
                    _SchemeShedule = new List<HpSchemeSheduleDefinition>();
                }
                _SchemeShedule.Add(_tmpSch);

                dgvShedule.AutoGenerateColumns = false;
                dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();
                dgvShedule.DataSource = _SchemeShedule;

                lblSheDefRental.Text = (Convert.ToInt32(lblSheDefRental.Text) + 1).ToString();

                if (cmbSheSchBase.Text == "RATE")
                {
                    if (Convert.ToInt32(lblSheDefRental.Text) == Convert.ToInt32(lblsheSchTerm.Text))
                    {
                        _tmpSch = new HpSchemeSheduleDefinition();
                        _tmpSch.Hss_seq = 0;
                        _tmpSch.Hss_sch_cd = txtSheSchCode.Text.Trim();
                        _tmpSch.Hss_rnt_no = Convert.ToInt32(lblSheDefRental.Text);
                        _tmpSch.Hss_rnt = 100 - _totRate;
                        if (cmbSheSchBase.Text == "RATE")
                        {
                            _tmpSch.Hss_is_rt = true;
                        }
                        else
                        {
                            _tmpSch.Hss_is_rt = false;
                        }
                        _tmpSch.Hss_cre_by = BaseCls.GlbUserID;
                        _tmpSch.Hss_cre_dt = DateTime.Now.Date;

                        _SchemeShedule.Add(_tmpSch);
                    }
                }

                dgvShedule.AutoGenerateColumns = false;
                dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();
                dgvShedule.DataSource = _SchemeShedule;

                if (Convert.ToInt32(lblSheDefRental.Text) == Convert.ToInt32(lblsheSchTerm.Text))
                {
                    btnSheDefAdd.Enabled = false;

                }
                txtSheDefAmount.Text = "";
                txtSheDefAmount.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteLast_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvShedule.Rows.Count <= 0)
                {
                    MessageBox.Show("No details are found to remove.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToInt32(lblSheDefRental.Text) == Convert.ToInt32(lblsheSchTerm.Text))
                {
                    MessageBox.Show("All slabs are completed. Cannot remove. Please clear all details and re-define.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to remove last slab ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    List<HpSchemeSheduleDefinition> _temp = new List<HpSchemeSheduleDefinition>();
                    _temp = _SchemeShedule;

                    int row_id = dgvShedule.Rows.Count - 1;

                    string _schCode = Convert.ToString(dgvShedule.Rows[row_id].Cells["col_sheSchCode"].Value);
                    Int32 _term = Convert.ToInt32(dgvShedule.Rows[row_id].Cells["col_sheRntNo"].Value);


                    _temp.RemoveAll(x => x.Hss_sch_cd == _schCode && x.Hss_rnt_no == _term);
                    _SchemeShedule = _temp;

                    dgvShedule.AutoGenerateColumns = false;
                    dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();
                    dgvShedule.DataSource = _SchemeShedule;

                    lblSheDefRental.Text = _term.ToString();

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAllSheClear_Click(object sender, EventArgs e)
        {
            _SchemeShedule = new List<HpSchemeSheduleDefinition>();
            lblSheDefRental.Text = "1";
            txtSheDefAmount.Text = "";
            btnSheDefAdd.Enabled = true;

            dgvShedule.AutoGenerateColumns = false;
            dgvShedule.DataSource = new List<HpSchemeSheduleDefinition>();

        }

        private void txtSheDefAmount_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSheDefAmount.Text))
            {
                if (!IsNumeric(txtSheDefAmount.Text))
                {
                    MessageBox.Show("Please enter valid amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSheDefAmount.Text = "";
                    txtSheDefAmount.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtSheDefAmount.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSheDefAmount.Text = "";
                    txtSheDefAmount.Focus();
                    return;
                }

                if (cmbSheSchBase.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtSheDefAmount.Text) > 100)
                    {
                        MessageBox.Show("Rate should be within the range of 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSheDefAmount.Text = "";
                        txtSheDefAmount.Focus();
                        return;
                    }
                }
            }
        }

        private void txtSheDefAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSheDefAdd.Focus();
            }
        }

        private void btnSheSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int16 row_aff = 0;
                string _msg = string.Empty;
                List<HPResheScheme> _saveReSheList = new List<HPResheScheme>();
                HPResheScheme _tmpReList = new HPResheScheme();

                if (dgvShedule.Rows.Count > 0)
                {
                    if (Convert.ToInt32(lblSheDefRental.Text) != Convert.ToInt32(lblsheSchTerm.Text))
                    {
                        MessageBox.Show("All definition slabs are not setup.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }



                foreach (DataGridViewRow row in dgvResheSch.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_R_Get"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _tmpReList = new HPResheScheme();
                        _tmpReList.Hsr_sch_cd = row.Cells["col_R_Sch"].Value.ToString();
                        _tmpReList.Hsr_rsch_cd = row.Cells["col_R_othSch"].Value.ToString();
                        _saveReSheList.Add(_tmpReList);
                    }
                }

                if (dgvShedule.Rows.Count <= 0 && _saveReSheList.Count == 0)
                {
                    MessageBox.Show("Shedule definition and reshedule definitions are not setup.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                row_aff = CHNLSVC.Sales.CreateNewSchemeSheduleDefinition(_SchemeShedule, txtSheSchCode.Text.Trim(), _saveReSheList);

                if (row_aff == 1)
                {
                    MessageBox.Show("Shedule created successfully.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Shedule_Definition();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddRental.Focus();
            }
        }

        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtIntRate.Focus();
            }

        }

        private void txtIntRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIntRate.Text))
            {
                if (!IsNumeric(txtIntRate.Text))
                {
                    MessageBox.Show("Please enter valid rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIntRate.Text = "";
                    txtIntRate.Focus();
                    return;
                }
                else
                {
                    txtIntRate.Text = Convert.ToDecimal(txtIntRate.Text).ToString("n");
                }
            }
        }

        private void txtIntRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddRental.Focus();
            }
        }

        private void txtAddRental_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkSchStatus.Focus();
            }
        }

        private void chkSchStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkInsuApp.Focus();
            }
        }

        private void chkInsuApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkAllowGS.Focus();
            }
        }

        private void chkAllowGS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkCommCalVAT.Focus();
            }
        }

        private void cmbFPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFPayValue.Focus();
            }
        }

        private void txtFPayValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkFPayCalWithVAT.Focus();
            }
        }

        private void chkFPayCalWithVAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkIntWithVAT.Focus();
            }
        }

        private void chkIntWithVAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkIntWithService.Focus();
            }
        }

        private void chkIntWithService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkIntWithInsu.Focus();
            }
        }

        private void chkIntWithInsu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkIntWithStampDuty.Focus();
            }
        }

        private void chkIntWithStampDuty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAddType.Focus();
            }
        }

        private void cmbAddType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddValue.Focus();
            }
        }

        private void txtAddValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkAddCalWithVAT.Focus();
            }
        }

        private void chkAddCalWithVAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHPInsuTerm.Focus();
            }
        }

        private void cmbDisType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisAmount.Focus();
            }
        }

        private void txtDisAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkCommCalVAT.Focus();
            }
        }

        private void chkCommCalVAT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkDisVou.Focus();
            }
        }

        private void txtHPInsuTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVehInsuTerm.Focus();
            }
        }

        private void txtVehInsuTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVehInsuCollectTerm.Focus();
            }
        }

        private void txtVehInsuCollectTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnMainSave.Focus();
            }
        }

        private void btnSearchPbook_Click(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtPriceBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceBook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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
                else if (e.KeyCode == Keys.Enter)
                {
                    txtPriceLevel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceBook_DoubleClick(object sender, EventArgs e)
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
                _CommonSearch.ShowDialog();
                txtPriceBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPriceBook.Clear();
                    txtPriceBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchLevel_Click(object sender, EventArgs e)
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

                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceLevel;
                _CommonSearch.ShowDialog();
                txtPriceLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Clear();
                        txtPriceBook.Focus();
                        return;
                    }
                    _searchType = "";

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPriceLevel;
                    _CommonSearch.ShowDialog();
                    txtPriceLevel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtMainCate.Enabled == true)
                    {
                        txtMainCate.Focus();
                    }
                    else if (txtSubCate.Enabled == true)
                    {
                        txtSubCate.Focus();
                    }
                    else if (txtItemRange.Enabled == true)
                    {
                        txtItemRange.Focus();
                    }
                    else if (txtBrand.Enabled == true)
                    {
                        txtBrand.Focus();
                    }
                    else if (txtItem.Enabled == true)
                    {
                        txtItem.Focus();
                    }
                    else if (btnLoadProducts.Enabled == true)
                    {
                        btnLoadProducts.Focus();
                    }
                    else
                    {
                        txtChanel.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceLevel_DoubleClick(object sender, EventArgs e)
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
                _searchType = "";

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceLevel;
                _CommonSearch.ShowDialog();
                txtPriceLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceLevel.Text)) return;
                if (string.IsNullOrEmpty(txtPriceBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPriceLevel.Clear(); txtPriceBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPriceBook.Text.Trim(), txtPriceLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPriceLevel.Clear(); txtPriceLevel.Focus(); return; }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchChannel_Click(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
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
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSChanel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
                    return;
                }
                _searchType = "";
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtChanel.Text))
                    {
                        MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSChanel.Text = "";
                        txtChanel.Focus();
                        return;
                    }

                    _searchType = "";
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
                else if (e.KeyCode == Keys.Enter)
                {
                    txtPC.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
                    return;
                }

                _searchType = "";
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPC;
                    _CommonSearch.ShowDialog();
                    txtPC.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddItem.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchPC_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtNewTypeRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewTypeRate.Text))
            {
                if (!IsNumeric(txtNewTypeRate.Text))
                {
                    MessageBox.Show("Please enter valid rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewTypeRate.Text = "";
                    txtNewTypeRate.Focus();
                    return;
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            try
            {

                Base _basePage = new Base();

                if (cmbCommDef.Text == "Profit Center")
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                    }
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtSChanel.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtSChanel.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            lstPC.Items.Add(drow["CODE"].ToString());
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            lstPC.Items.Add(drow["CODE"].ToString());
                        }
                    }
                }
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtPC.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void Clear_Comm_Def()
        {
            _schemeCommDef = new List<HpSchemeDefinition>();
            _promoDetails = new List<PriceDetailRef>();
            _generalCir = new List<HpSchemeDefinitionLog>();
            _schemeProcess = new List<HpSchemeDefinitionProcess>();
            _reCall = false;
            lblInfo.Text = "";
            lstPC.Clear();
            lstSch.Clear();
            txtCommAppSch.Text = "";
            lblComSchType.Text = "";
            lblCommSchTerm.Text = "";
            lblCommSchInt.Text = "";
            cmbCommDefType.Enabled = true;
            cmbCommDefType.Text = "Price Book Wise";
            txtPriceBook.Text = "";
            txtPriceLevel.Text = "";
            txtMainCate.Text = "";
            txtSubCate.Text = "";
            txtItem.Text = "";
            txtItemRange.Text = "";
            txtBrand.Text = "";
            txtSchCircular.Text = "";
            txtInstallmentRate.Text = "";
            txtDownPayRate.Text = "";
            cmbSchDisType.Text = "RATE";
            txtSchDis.Text = "";
            chkSchRestrict.Checked = false;
            lblcount.Text = "";
            btnApproved.Enabled = false;
            dtpGenUpdate.Enabled = false;
            txtGenRate.Text = "";
            txtGenRate.Enabled = false;
            cmbGenType.Enabled = false;
            cmbGenCate.Text = "Valid Date";
            txtSchCircular.Enabled = true;
            btnCommAdd.Enabled = true;
            lblSaveCount.Text = "";
            txtCircRem.Text = "";
            txtPriceCirc.Text = "";
            cmbCommDef.Text = "Profit Center";

            int I = (int)CHNLSVC.Sales.DeleteHPSchProcess(BaseCls.GlbUserID);

            SystemAppLevelParam _sysApp = new SystemAppLevelParam();

            _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT029", BaseCls.GlbUserID);
            if (_sysApp.Sarp_cd != null)
            {
                btnApproved.Enabled = true;
            }

            dgvDefDetails.AutoGenerateColumns = false;
            dgvDefDetails.DataSource = new List<HpSchemeDefinition>();

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

        private void btnSearchCommAppSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCommAppSch;
                _CommonSearch.ShowDialog();
                txtCommAppSch.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCommAppSch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCommAppSch;
                    _CommonSearch.ShowDialog();
                    txtCommAppSch.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddSch.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCommAppSch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCommAppSch;
                _CommonSearch.ShowDialog();
                txtCommAppSch.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCommAppSch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCommAppSch.Text))
                {
                    lblComSchType.Text = "";
                    lblCommSchTerm.Text = "";
                    lblCommSchInt.Text = "";

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtCommAppSch.Text.Trim());

                    if (_tmpSch.Hsd_is_rt == true)
                    {
                        cmbFPayType.Text = "RATE";
                    }
                    else
                    {
                        cmbFPayType.Text = "VALUE";
                    }
                    txtFPayValue.Text = _tmpSch.Hsd_fpay.ToString("n");
                    chkFPayCalWithVAT.Checked = _tmpSch.Hsd_fpay_calwithvat;

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCommAppSch.Text = "";
                        txtCommAppSch.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCommAppSch.Text = "";
                        txtCommAppSch.Focus();
                        return;
                    }

                    lblComSchType.Text = _tmpSch.Hsd_sch_tp;
                    lblCommSchTerm.Text = _tmpSch.Hsd_term.ToString("0");
                    lblCommSchInt.Text = _tmpSch.Hsd_intr_rt.ToString("n");

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();
        }

        private void btnAddSch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCommAppSch.Text))
            {
                MessageBox.Show("Please select applicable scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCommAppSch.Focus();
                return;
            }
            lstSch.Items.Add(txtCommAppSch.Text.Trim());
            txtCommAppSch.Text = "";
            lblComSchType.Text = "";
            lblCommSchInt.Text = "";
            lblCommSchTerm.Text = "";
            txtCommAppSch.Focus();
        }

        private void btnCommSchListClear_Click(object sender, EventArgs e)
        {
            lstSch.Clear();
            txtCommAppSch.Text = "";
            lblComSchType.Text = "";
            lblCommSchInt.Text = "";
            lblCommSchTerm.Text = "";
            txtCommAppSch.Focus();
        }

        private void chkFPayCalWithVAT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFPayCalWithVAT.Checked == true)
            {
                chkIntWithVAT.Checked = false;
                chkIntWithVAT.Enabled = true;
            }
            else
            {
                chkIntWithVAT.Checked = false;
                chkIntWithVAT.Enabled = false;
            }
        }

        private void chkIntWithInsu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntWithInsu.Checked == true)
            {
                txtHPInsuTerm.Text = "0";
                txtHPInsuTerm.Enabled = false;
            }
            else
            {
                txtHPInsuTerm.Enabled = true;
                txtHPInsuTerm.Text = "";
            }
        }

        private void cmbAddType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAddValue.Text = "";
        }

        private void cmbDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDisAmount.Text = "";
        }

        private void cmbCommDefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = "";
                if (cmbCommDefType.Text == "Price Book Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    lstItem.Clear();
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm1;
                    txtPriceBook.Enabled = true;
                    txtPriceLevel.Enabled = true;
                    btnSearchPbook.Enabled = true;
                    btnSearchLevel.Enabled = true;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = false;
                    btnSearchBrand.Enabled = false;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = false;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = false;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";

                }
                else if (cmbCommDefType.Text == "Main Category Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    lstItem.Clear();
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm1;
                    txtPriceBook.Enabled = true;
                    txtPriceLevel.Enabled = true;
                    btnSearchPbook.Enabled = true;
                    btnSearchLevel.Enabled = true;
                    txtMainCate.Enabled = true;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = true;
                    btnSearchMainCate.Enabled = true;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = true;
                    btnSearchBrand.Enabled = true;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = true;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }
                else if (cmbCommDefType.Text == "Sub Category Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    lstItem.Clear();
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm1;
                    txtPriceBook.Enabled = true;
                    txtPriceLevel.Enabled = true;
                    btnSearchPbook.Enabled = true;
                    btnSearchLevel.Enabled = true;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = true;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = true;
                    btnSearchBrand.Enabled = true;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = true;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = true;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = true;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }
                else if (cmbCommDefType.Text == "Product Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    lstItem.Clear();
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm1;
                    txtPriceBook.Enabled = true;
                    txtPriceLevel.Enabled = true;
                    btnSearchPbook.Enabled = true;
                    btnSearchLevel.Enabled = true;
                    txtMainCate.Enabled = true;
                    txtSubCate.Enabled = true;
                    txtItemRange.Enabled = true;
                    txtItem.Enabled = true;
                    txtBrand.Enabled = true;
                    btnSearchBrand.Enabled = true;
                    btnSearchMainCate.Enabled = true;
                    btnSearchSubCate.Enabled = true;
                    btnSearchRange.Enabled = true;
                    btnSearchItem.Enabled = true;
                    btnLoadProducts.Enabled = true;
                    txtFileName.Enabled = true;
                    btnSearchFile.Enabled = true;
                    btnUploadFile.Enabled = true;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = true;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }
                else if (cmbCommDefType.Text == "Promotion Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    lstItem.Clear();
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm2;
                    txtPriceBook.Enabled = false;
                    txtPriceLevel.Enabled = false;
                    btnSearchPbook.Enabled = false;
                    btnSearchLevel.Enabled = false;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = false;
                    btnSearchBrand.Enabled = false;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = false;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = false;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }
                else if (cmbCommDefType.Text == "Customer Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    lstItem.Clear();
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm3;
                    txtPriceBook.Enabled = false;
                    txtPriceLevel.Enabled = false;
                    btnSearchPbook.Enabled = false;
                    btnSearchLevel.Enabled = false;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = false;
                    btnSearchBrand.Enabled = false;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = false;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();

                    lstCus.Clear();
                    lstItem.Visible = false;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();

                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }
                else if (cmbCommDefType.Text == "Serial Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    lstItem.Clear();
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm4;
                    txtPriceBook.Enabled = false;
                    txtPriceLevel.Enabled = false;
                    btnSearchPbook.Enabled = false;
                    btnSearchLevel.Enabled = false;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = false;
                    btnSearchBrand.Enabled = false;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = false;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();

                    lstCus.Clear();
                    lstItem.Visible = false;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                    txtSerialcir.Focus();
                }
                else if (cmbCommDefType.Text == "Clone Details")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    lstItem.Clear();
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm5;
                    txtPriceBook.Enabled = false;
                    txtPriceLevel.Enabled = false;
                    btnSearchPbook.Enabled = false;
                    btnSearchLevel.Enabled = false;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = false;
                    btnSearchBrand.Enabled = false;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = false;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();

                    lstCus.Clear();
                    lstItem.Visible = false;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();

                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                    txtSerialcir.Focus();
                }
                else if (cmbCommDefType.Text == "General Updation")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    lstItem.Clear();
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm6;
                    txtPriceBook.Enabled = false;
                    txtPriceLevel.Enabled = false;
                    btnSearchPbook.Enabled = false;
                    btnSearchLevel.Enabled = false;
                    txtMainCate.Enabled = false;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = false;
                    btnSearchBrand.Enabled = false;
                    btnSearchMainCate.Enabled = false;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = false;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    cmbGenCate.Text = "DP. Comm.";
                    cmbGenCate.Text = "Valid Date";
                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();

                    lstCus.Clear();
                    lstItem.Visible = false;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();

                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                }

                //tHARINDU 2018-06-11
                else if (cmbCommDefType.Text == "Brand And Main Category Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    lstItem.Clear();
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm1;
                    txtPriceBook.Enabled = true;
                    txtPriceLevel.Enabled = true;
                    btnSearchPbook.Enabled = true;
                    btnSearchLevel.Enabled = true;
                    txtMainCate.Enabled = true;
                    txtSubCate.Enabled = false;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = true;
                    btnSearchMainCate.Enabled = true;
                    btnSearchSubCate.Enabled = false;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = true;
                    btnSearchBrand.Enabled = true;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = true;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }

                else if (cmbCommDefType.Text == "Brand And Sub Category Wise")
                {
                    txtPriceBook.Text = "";
                    txtPriceLevel.Text = "";
                    txtMainCate.Text = "";
                    txtSubCate.Text = "";
                    txtItem.Text = "";
                    txtItemRange.Text = "";
                    txtBrand.Text = "";
                    lstItem.Clear();
                    txtCusPriceBook.Enabled = true;
                    txtCusLevel.Enabled = true;
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Text = "";
                    txtClonePc.Text = "";

                    tbMainCommDef.SelectedTab = tbComm1;
                    txtPriceBook.Enabled = true;
                    txtPriceLevel.Enabled = true;
                    btnSearchPbook.Enabled = true;
                    btnSearchLevel.Enabled = true;
                    txtMainCate.Enabled = true;
                    txtSubCate.Enabled = true;
                    txtItemRange.Enabled = false;
                    txtItem.Enabled = false;
                    txtBrand.Enabled = true;
                    btnSearchMainCate.Enabled = true;
                    btnSearchSubCate.Enabled = true;
                    btnSearchRange.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnLoadProducts.Enabled = true;
                    btnSearchBrand.Enabled = true;
                    txtFileName.Enabled = false;
                    btnSearchFile.Enabled = false;
                    btnUploadFile.Enabled = false;
                    txtFileName.Text = "";

                    txtPromoBook.Text = "";
                    txtPromoLevel.Text = "";
                    txtPromoCode.Text = "";
                    txtPromoCir.Text = "";
                    _promoDetails = new List<PriceDetailRef>();
                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceDetailRef>();
                    lstCus.Clear();
                    lstItem.Visible = true;
                    dgvSerialDetails.AutoGenerateColumns = false;
                    dgvSerialDetails.DataSource = new List<PriceSerialRef>();


                    dgvCirculars.AutoGenerateColumns = false;
                    dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();

                    txtSerialcir.Text = "";
                    txtSerialItem.Text = "";
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbMainCommDef_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (cmbCommDefType.Text == "Price Book Wise" || cmbCommDefType.Text == "Product Wise" || cmbCommDefType.Text == "Main Category Wise" || cmbCommDefType.Text == "Sub Category Wise")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[0])
                {

                    MessageBox.Show("Not allow for above type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMainCommDef.SelectTab(0);
                    return;

                }
            }
            else if (cmbCommDefType.Text == "Promotion Wise")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[1])
                {

                    MessageBox.Show("Not allow for above type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMainCommDef.SelectTab(1);
                    return;

                }
            }
            else if (cmbCommDefType.Text == "Customer Wise")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[2])
                {

                    MessageBox.Show("Not allow for above type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMainCommDef.SelectTab(2);
                    return;

                }
            }
            else if (cmbCommDefType.Text == "Serial Wise")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[3])
                {

                    MessageBox.Show("Not allow for above type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMainCommDef.SelectTab(3);
                    return;

                }
            }
            else if (cmbCommDefType.Text == "Clone Details")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[4])
                {

                    MessageBox.Show("Not allow for above type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMainCommDef.SelectTab(4);
                    return;

                }
            }
        }

        private void AddCount(Int32 _count)
        {
            lblcount.Text = _count.ToString();
            lblcount.Refresh();
            Application.DoEvents();
        }

        private void AddSaveCount(Int32 _count)
        {
            lblSaveCount.Text = _count.ToString();
            lblSaveCount.Refresh();
            Application.DoEvents();
        }

        private void btnCommAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //_schemeProcess = new List<HpSchemeDefinitionProcess>();
                Int16 _processEffect = 0;
                if (string.IsNullOrEmpty(cmbCommDefType.Text))
                {
                    MessageBox.Show("Please select definition type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCommDefType.Focus();
                    return;
                }

                if (cmbCommDefType.Text != "Clone Details")
                {
                    if (string.IsNullOrEmpty(txtSchCircular.Text))
                    {
                        MessageBox.Show("Please select circular #.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSchCircular.Focus();
                        return;
                    }
                }

                List<HpSchemeDefinition> _recallList = new List<HpSchemeDefinition>();

                _recallList = CHNLSVC.Sales.GetSchemeDetailsByCir(txtSchCircular.Text.Trim());

                if (_recallList.Count > 0 && _recallList != null)
                {

                    var _record = (from _lst in _recallList
                                   select _lst.Hpc_stus).Distinct().ToList();


                    foreach (var tmpRec in _record)
                    {
                        if (tmpRec == "C")
                        {
                            MessageBox.Show("The perticular circular is already cancelled.You cannot use this circular.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (tmpRec == "A")
                        {
                            MessageBox.Show("The perticular circular is already approved.You cannot use this circular.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }


                }



                if (cmbCommDefType.Text != "Clone Details")
                {
                    if (string.IsNullOrEmpty(txtSchCircular.Text))
                    {
                        MessageBox.Show("Please enter circular #.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSchCircular.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtInstallmentRate.Text))
                    {
                        MessageBox.Show("Please enter installment rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInstallmentRate.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtDownPayRate.Text))
                    {
                        MessageBox.Show("Please enter downpayment rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDownPayRate.Focus();
                        return;
                    }

                    if (Convert.ToDateTime(dtpFromDate.Value).Date < Convert.ToDateTime(DateTime.Now).Date)
                    {
                        MessageBox.Show("Valid date cannot back date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpFromDate.Focus();
                        return;
                    }

                    if (Convert.ToDateTime(dtpFromDate.Value).Date > Convert.ToDateTime(dtpToDate.Value).Date)
                    {
                        MessageBox.Show("Valid To date cannot less than from date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpToDate.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtSchDis.Text))
                    {
                        MessageBox.Show("Please enter discount rate / amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSchDis.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(cmbSchDisType.Text))
                    {
                        MessageBox.Show("Please select discount type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbSchDisType.Focus();
                        return;
                    }
                }

                //tHARINDU

                if (cmbFPayType.Text == "RATE")
                {
                    //int x = 0;
                    //int.TryParse(txtFPayValue.Text, out x);

                    decimal x = 0;
                    decimal.TryParse(txtFPayValue.Text, out x);

                    if (x < 0 || x > 100)
                    {
                        MessageBox.Show("Please Enter Rate Between 0-100", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFPayValue.Focus();
                        return;
                    }

     
                    //if (!Enumerable.Range(1, 100).Contains(x))
                    //{
                    //    MessageBox.Show("Please Enter Rate Between 0-100", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtFPayValue.Focus();
                    //    return;
                    //}

                  
                }
                else
                {
                    if(string.IsNullOrEmpty(txtFPayValue.Text))
                    {
                        MessageBox.Show("Please Enter Value/Rate Greater than 0", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFPayValue.Focus();
                        return;
                    }
                    else
                    {
                       
                        decimal x = 0;
                        decimal.TryParse(txtFPayValue.Text, out x);

                        if(x < 0)
                        {
                            MessageBox.Show("Please Enter Value/Rate Greater than or equal to zero", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtFPayValue.Focus();
                            return;
                        }
                    }
                }

                SchemeCreation ss = new SchemeCreation();
                TimeSpan start = DateTime.Now.TimeOfDay;
                Boolean _isValidSch = false;
                if (chkSchRestrict.Checked == true)
                {
                    _isValidSch = false;
                }
                else
                {
                    _isValidSch = true;
                }

                Boolean _isDisRate = false;
                if (cmbSchDisType.Text == "RATE")
                {
                    _isDisRate = true;
                }
                else
                {
                    _isDisRate = false;
                }

                if (cmbCommDefType.Text == "Price Book Wise")
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceLevel.Focus();
                        return;
                    }
                }
                else if (cmbCommDefType.Text == "Main Category Wise")
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceLevel.Focus();
                        return;
                    }

                    //if (string.IsNullOrEmpty(txtMainCate.Text))
                    //{
                    //    MessageBox.Show("Please enter main category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtMainCate.Focus();
                    //    return;
                    //}
                    Boolean _isValidItm = false;
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidItm = true;
                            goto L3;
                        }
                    }
                L3:

                    if (_isValidItm == false)
                    {
                        MessageBox.Show("No any applicable main categories are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    // due to the new req dilanda by //Tharindu 2018-07-01 comment due to req
                    //if (string.IsNullOrEmpty(txtBrand.Text))
                    //{
                    //    MessageBox.Show("Please enter brand.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand.Focus();
                    //    return;
                    //}
                }
                else if (cmbCommDefType.Text == "Sub Category Wise")
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceLevel.Focus();
                        return;
                    }


                    Boolean _isValidItm = false;
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidItm = true;
                            goto L3;
                        }
                    }
                L3:

                    if (_isValidItm == false)
                    {
                        MessageBox.Show("No any applicable sub categories are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //if (string.IsNullOrEmpty(txtSubCate.Text))
                    //{
                    //    MessageBox.Show("Please enter sub category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtSubCate.Focus();
                    //    return;
                    //}

                    // due to the new req dilanda by tharindu
                    //if (string.IsNullOrEmpty(txtBrand.Text))
                    //{
                    //    MessageBox.Show("Please enter brand.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand.Focus();
                    //    return;
                    //}
                }
                else if (cmbCommDefType.Text == "Product Wise")
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceLevel.Focus();
                        return;
                    }

                    Boolean _isValidItm = false;
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidItm = true;
                            goto L3;
                        }
                    }
                L3:

                    if (_isValidItm == false)
                    {
                        MessageBox.Show("No any applicable items are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (cmbCommDefType.Text == "Promotion Wise")
                {
                    if (string.IsNullOrEmpty(txtPromoBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPromoBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPromoLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPromoLevel.Focus();
                        return;
                    }

                    if (dgvPromo.Rows.Count == 0)
                    {
                        MessageBox.Show("Please select applicalble promotion codes.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Boolean _appPromo = false;
                    foreach (DataGridViewRow row in dgvPromo.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells["col_p_Get"] as DataGridViewCheckBoxCell;

                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            _appPromo = true;
                            goto L4;
                        }
                    }
                L4:

                    if (_appPromo == false)
                    {
                        MessageBox.Show("No any applicable promotion code is selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                }
                else if (cmbCommDefType.Text == "Customer Wise")
                {
                    if (string.IsNullOrEmpty(txtCusPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCusLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusLevel.Focus();
                        return;
                    }

                    Boolean _isValidCus = false;
                    foreach (ListViewItem Item in lstCus.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidCus = true;
                            goto L5;
                        }
                    }
                L5:

                    if (_isValidCus == false)
                    {
                        MessageBox.Show("No any applicable customers are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (cmbCommDefType.Text == "Serial Wise")
                {

                    if (dgvSerialDetails.Rows.Count == 0)
                    {
                        MessageBox.Show("Please select applicalble serial details.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Boolean _appSerial = false;
                    foreach (DataGridViewRow row in dgvSerialDetails.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells["col_Ser_Pick"] as DataGridViewCheckBoxCell;

                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            _appSerial = true;
                            goto L6;
                        }
                    }
                L6:

                    if (_appSerial == false)
                    {
                        MessageBox.Show("No any applicable serials are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                }
                else if (cmbCommDefType.Text == "Clone Details")
                {
                    if (string.IsNullOrEmpty(txtClonePc.Text))
                    {
                        MessageBox.Show("Please select clone profit center.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClonePc.Focus();
                        return;
                    }


                }


                // Tharindu 

                else if (cmbCommDefType.Text == "Brand And Main Category Wise")
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceLevel.Focus();
                        return;
                    }

                    Boolean _isValidItm = false;
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidItm = true;
                            goto L7;
                        }
                    }
                L7:

                    if (_isValidItm == false)
                    {
                        MessageBox.Show("No any applicable main categories are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //if (string.IsNullOrEmpty(txtBrand.Text))
                    //{
                    //    MessageBox.Show("Please enter brand.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand.Focus();
                    //    return;
                    //}
                }

                else if (cmbCommDefType.Text == "Brand And Sub Category Wise")
                {
                    if (string.IsNullOrEmpty(txtPriceBook.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceBook.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPriceLevel.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPriceLevel.Focus();
                        return;
                    }

                    Boolean _isValidItm = false;
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidItm = true;
                            goto L8;
                        }
                    }
                L8:

                    if (_isValidItm == false)
                    {
                        MessageBox.Show("No any applicable main categories are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //if (string.IsNullOrEmpty(txtBrand.Text))
                    //{
                    //    MessageBox.Show("Please enter brand.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand.Focus();
                    //    return;
                    //}
                }



                Boolean _isPCFound = false;
                foreach (ListViewItem Item in lstPC.Items)
                {
                    string pc = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isPCFound = true;
                        goto L1;
                    }
                }
            L1:
                if (cmbCommDefType.Text != "Clone Details")
                {
                    if (_isPCFound == false)
                    {
                        MessageBox.Show("No any applicable profit center is selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                Boolean _isSchFound = false;
                foreach (ListViewItem Item in lstSch.Items)
                {
                    string sch = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isSchFound = true;
                        goto L2;
                    }
                }
            L2:

                if (cmbCommDefType.Text != "Clone Details")
                {
                    if (_isSchFound == false)
                    {
                        MessageBox.Show("No any applicable scheme(s) are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                


                if (MessageBox.Show("Confirm to apply details ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                HpSchemeDefinition _tmpList = new HpSchemeDefinition();
                HpSchemeDefinitionProcess _tempProcess = new HpSchemeDefinitionProcess();
                //_schemeCommDef = new List<HpSchemeDefinition>();
                btnCommSave.Enabled = false;
                btnCommAdd.Enabled = true;
                if (cmbCommDefType.Text == "Price Book Wise")
                {

                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {

                                    //foreach (HpSchemeDefinition temp in _schemeCommDef)
                                    //{
                                    //    if (temp.Hpc_brd == txtBrand.Text.Trim() && temp.Hpc_cat == txtSubCate.Text.Trim() && temp.Hpc_main_cat == txtMainCate.Text.Trim() && temp.Hpc_pb == txtPriceBook.Text.Trim() && temp.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && temp.Hpc_pty_cd == pc && temp.Hpc_sch_cd == sch && temp.Hpc_from_dt.Date == dtpFromDate.Value.Date)
                                    //    {
                                    //        goto L10;
                                    //    }
                                    //}

                                    //var _record = (from _lst in _schemeProcess
                                    //               where _lst.Hpc_brd == txtBrand.Text.Trim() && _lst.Hpc_cat == txtSubCate.Text.Trim() && _lst.Hpc_main_cat == txtMainCate.Text.Trim() && _lst.Hpc_pb == txtPriceBook.Text.Trim() && _lst.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && _lst.Hpc_pty_cd == pc && _lst.Hpc_sch_cd == sch && _lst.Hpc_from_dt == dtpFromDate.Value.Date && _lst.Hpc_cre_by == BaseCls.GlbUserID
                                    //               select _lst).ToList();

                                    //if (_record.Count > 0)
                                    //{
                                    //    goto L10;
                                    //}

                                    // _tmpList = new HpSchemeDefinition();
                                    _tempProcess = new HpSchemeDefinitionProcess();
                                    _tempProcess.Hpc_brd = txtBrand.Text;
                                    _tempProcess.Hpc_cat = txtSubCate.Text;
                                    _tempProcess.Hpc_cir_no = txtSchCircular.Text;
                                    if (cmbCommDefType.Text == "Price Book Wise")
                                    {
                                        _tempProcess.Hpc_comm_cat = "PB";
                                    }
                                    else if (cmbCommDefType.Text == "Main Category Wise")
                                    {
                                        _tempProcess.Hpc_comm_cat = "CAT";
                                    }
                                    else if (cmbCommDefType.Text == "Sub Category Wise")
                                    {
                                        _tempProcess.Hpc_comm_cat = "CAT2";
                                    }
                                    else if (cmbCommDefType.Text == "Promotion Wise")
                                    {
                                        _tempProcess.Hpc_comm_cat = "PROMO";
                                    }
                                    else if (cmbCommDefType.Text == "Customer Wise")
                                    {
                                        _tempProcess.Hpc_comm_cat = "CUS";
                                    }
                                    else if (cmbCommDefType.Text == "Serial Wise")
                                    {
                                        _tempProcess.Hpc_comm_cat = "SERIAL";
                                    }

                                    _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                    //_tmpList.Hpc_cre_dt = DateTime.Today.Date;
                                    _tempProcess.Hpc_cust_cd = null;
                                    _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                    _tempProcess.Hpc_disc_isrt = _isDisRate;
                                    _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                    _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                    _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                    _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                    _tempProcess.Hpc_is_alw = _isValidSch;
                                    _tempProcess.Hpc_itm = null;
                                    _tempProcess.Hpc_main_cat = txtMainCate.Text;
                                    _tempProcess.Hpc_pb = txtPriceBook.Text;
                                    _tempProcess.Hpc_pb_lvl = txtPriceLevel.Text;
                                    _tempProcess.Hpc_pro = null;
                                    _tempProcess.Hpc_pty_cd = pc;
                                    _tempProcess.Hpc_price_cir_no = txtPriceCirc.Text;
                                    if (cmbCommDef.Text == "Profit Center")
                                    {
                                        _tempProcess.Hpc_pty_tp = "PC";
                                    }
                                    else
                                    {
                                        _tempProcess.Hpc_pty_tp = "SCHNL";
                                    }
                                    _tempProcess.Hpc_sch_cd = sch;
                                    //_tempProcess.Hpc_seq = 0;
                                    _tempProcess.Hpc_ser = null;
                                    //_schemeCommDef.Add(_tmpList);

                                    // tHARINDU

                                    if (cmbFPayType.Text == "RATE")
                                    {
                                        _tempProcess.Hpc_is_rt = true;
                                    }
                                    else
                                    {
                                        _tempProcess.Hpc_is_rt = false;
                                    }
                                    _tempProcess.Hpc_is_rt_typr = cmbFPayType.Text;
                                    _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                    _tempProcess.Hsd_add_calwithvat = chkFPayCalWithVAT.Checked ? 1 : 0;
                                    _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 

                                    _schemeProcess.Add(_tempProcess);
                                    AddCount(_schemeProcess.Count);

                                }
                            L10: int I = 10;
                            }
                        }
                    }
                }

                else if (cmbCommDefType.Text == "Main Category Wise" || cmbCommDefType.Text == "Sub Category Wise")
                {


                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (ListViewItem itmList in lstItem.Items)
                                    {
                                        string _item = itmList.Text;

                                        if (itmList.Checked == true)
                                        {

                                            _tempProcess = new HpSchemeDefinitionProcess();
                                            _tempProcess.Hpc_brd = txtBrand.Text;
                                            if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_cat = _item;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_cat = null;
                                            }
                                            _tempProcess.Hpc_cir_no = txtSchCircular.Text;
                                            if (cmbCommDefType.Text == "Price Book Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PB";
                                            }
                                            else if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT";
                                            }
                                            else if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT2";
                                            }
                                            else if (cmbCommDefType.Text == "Promotion Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PROMO";
                                            }
                                            else if (cmbCommDefType.Text == "Customer Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CUS";
                                            }
                                            else if (cmbCommDefType.Text == "Serial Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "SERIAL";
                                            }

                                            _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                            //_tmpList.Hpc_cre_dt = DateTime.Today.Date;
                                            _tempProcess.Hpc_cust_cd = null;
                                            _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                            _tempProcess.Hpc_disc_isrt = _isDisRate;
                                            _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                            _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                            _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                            _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                            _tempProcess.Hpc_is_alw = _isValidSch;
                                            _tempProcess.Hpc_itm = null;
                                            _tempProcess.Hpc_price_cir_no = txtPriceCirc.Text;
                                            if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_main_cat = _item;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_main_cat = null;
                                            }
                                            _tempProcess.Hpc_pb = txtPriceBook.Text;
                                            _tempProcess.Hpc_pb_lvl = txtPriceLevel.Text;
                                            _tempProcess.Hpc_pro = null;
                                            _tempProcess.Hpc_pty_cd = pc;
                                            if (cmbCommDef.Text == "Profit Center")
                                            {
                                                _tempProcess.Hpc_pty_tp = "PC";
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_pty_tp = "SCHNL";
                                            }
                                            _tempProcess.Hpc_sch_cd = sch;
                                            //_tempProcess.Hpc_seq = 0;
                                            _tempProcess.Hpc_ser = null;

                                          
                                            // New
                                            if (cmbFPayType.Text == "RATE")
                                            {
                                                _tempProcess.Hpc_is_rt = true;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_is_rt = false;
                                            }
                                            _tempProcess.Hpc_is_rt_typr = cmbFPayType.Text;
                                            _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                            _tempProcess.Hsd_add_calwithvat = chkAddCalWithVAT.Checked ? 1 : 0;
                                            _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 
                                            _schemeProcess.Add(_tempProcess);
                                            AddCount(_schemeProcess.Count);


                                        }
                                    L11: int x = 1;
                                    }
                                }
                            }
                        }
                    }
                }




                else if (cmbCommDefType.Text == "Product Wise")
                {


                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (ListViewItem itmList in lstItem.Items)
                                    {
                                        string _item = itmList.Text;

                                        if (itmList.Checked == true)
                                        {
                                            //foreach (HpSchemeDefinition temp in _schemeCommDef)
                                            //{
                                            //    if (temp.Hpc_pb == txtPriceBook.Text.Trim() && temp.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && temp.Hpc_pty_cd == pc && temp.Hpc_sch_cd == sch && temp.Hpc_itm == _item && temp.Hpc_from_dt.Date == dtpFromDate.Value.Date)
                                            //    {
                                            //        goto L11;
                                            //    }
                                            //}
                                            //var _record = (from _lst in _schemeProcess
                                            //               where _lst.Hpc_pb == txtPriceBook.Text.Trim() && _lst.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && _lst.Hpc_pty_cd == pc && _lst.Hpc_sch_cd == sch && _lst.Hpc_itm == _item && _lst.Hpc_from_dt.Date == dtpFromDate.Value.Date && _lst.Hpc_cre_by == BaseCls.GlbUserID
                                            //               select _lst.Hpc_brd).ToList();

                                            //if (_record.Count > 0)
                                            //{
                                            //    goto L11;
                                            //}

                                            //_tmpList = new HpSchemeDefinition();
                                            _tempProcess = new HpSchemeDefinitionProcess();
                                            _tempProcess.Hpc_brd = null;
                                            _tempProcess.Hpc_cat = null;
                                            _tempProcess.Hpc_cir_no = txtSchCircular.Text;
                                            if (cmbCommDefType.Text == "Price Book Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PB";
                                            }
                                            else if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT";
                                            }
                                            else if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT2";
                                            }
                                            else if (cmbCommDefType.Text == "Product Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "ITM";
                                            }
                                            else if (cmbCommDefType.Text == "Promotion Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PROMO";
                                            }
                                            else if (cmbCommDefType.Text == "Customer Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CUS";
                                            }
                                            else if (cmbCommDefType.Text == "Serial Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "SERIAL";
                                            }

                                            _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                            //_tempProcess.Hpc_cre_dt = DateTime.Today.Date;
                                            _tempProcess.Hpc_cust_cd = null;
                                            _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                            _tempProcess.Hpc_disc_isrt = _isDisRate;
                                            _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                            _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                            _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                            _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                            _tempProcess.Hpc_is_alw = _isValidSch;
                                            _tempProcess.Hpc_itm = _item;
                                            _tempProcess.Hpc_main_cat = null;
                                            _tempProcess.Hpc_pb = txtPriceBook.Text;
                                            _tempProcess.Hpc_pb_lvl = txtPriceLevel.Text;
                                            _tempProcess.Hpc_pro = null;
                                            _tempProcess.Hpc_pty_cd = pc;
                                            _tempProcess.Hpc_price_cir_no = txtPriceCirc.Text;
                                            if (cmbCommDef.Text == "Profit Center")
                                            {
                                                _tempProcess.Hpc_pty_tp = "PC";
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_pty_tp = "SCHNL";
                                            }
                                            _tempProcess.Hpc_sch_cd = sch;
                                            //_tempProcess.Hpc_seq = 0;
                                            _tempProcess.Hpc_ser = null;

                                            //_processEffect = (Int16)(CHNLSVC.Sales.SaveNewSchemeCommProcess(_tempProcess));

                                            // tHARINDU

                                            if (cmbFPayType.Text == "RATE")
                                            {
                                                _tempProcess.Hpc_is_rt = true;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_is_rt = false;
                                            }
                                            _tempProcess.Hpc_is_rt_typr = cmbFPayType.Text;
                                            _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                            _tempProcess.Hsd_add_calwithvat = chkFPayCalWithVAT.Checked ? 1 : 0;
                                            _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 
                                            _schemeProcess.Add(_tempProcess);
                                            AddCount(_schemeProcess.Count);


                                        }
                                    L11: int x = 1;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (cmbCommDefType.Text == "Promotion Wise")
                {
                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (DataGridViewRow row in dgvPromo.Rows)
                                    {
                                        DataGridViewCheckBoxCell chk = row.Cells["col_p_Get"] as DataGridViewCheckBoxCell;

                                        if (Convert.ToBoolean(chk.Value) == true)
                                        {

                                            string _promoCode = row.Cells["col_P_Promo"].Value.ToString();

                                            //foreach (HpSchemeDefinition temp in _schemeCommDef)
                                            //{
                                            //    if (temp.Hpc_pb == txtPriceBook.Text.Trim() && temp.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && temp.Hpc_pty_cd == pc && temp.Hpc_sch_cd == sch && temp.Hpc_pro == _promoCode && temp.Hpc_from_dt.Date == dtpFromDate.Value.Date)
                                            //    {
                                            //        goto L12;
                                            //    }
                                            //}
                                            //var _record = (from _lst in _schemeProcess
                                            //               where _lst.Hpc_pb == txtPriceBook.Text.Trim() && _lst.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && _lst.Hpc_pty_cd == pc && _lst.Hpc_sch_cd == sch && _lst.Hpc_pro == _promoCode && _lst.Hpc_from_dt.Date == dtpFromDate.Value.Date && _lst.Hpc_cre_by== BaseCls.GlbUserID
                                            //               select _lst).ToList();

                                            //if (_record.Count > 0)
                                            //{
                                            //    goto L12;
                                            //}

                                            //_tmpList = new HpSchemeDefinition();
                                            _tempProcess = new HpSchemeDefinitionProcess();
                                            _tempProcess.Hpc_brd = null;
                                            _tempProcess.Hpc_cat = null;
                                            _tempProcess.Hpc_cir_no = txtSchCircular.Text;
                                            if (cmbCommDefType.Text == "Price Book Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PB";
                                            }
                                            else if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT";
                                            }
                                            else if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT2";
                                            }
                                            else if (cmbCommDefType.Text == "Product Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "ITM";
                                            }
                                            else if (cmbCommDefType.Text == "Promotion Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PROMO";
                                            }
                                            else if (cmbCommDefType.Text == "Customer Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CUS";
                                            }
                                            else if (cmbCommDefType.Text == "Serial Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "SERIAL";
                                            }

                                            _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                            // _tempProcess.Hpc_cre_dt = DateTime.Today.Date;
                                            _tempProcess.Hpc_cust_cd = null;
                                            _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                            _tempProcess.Hpc_disc_isrt = _isDisRate;
                                            _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                            _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                            _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                            _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                            _tempProcess.Hpc_is_alw = _isValidSch;
                                            _tempProcess.Hpc_itm = null;
                                            _tempProcess.Hpc_main_cat = null;
                                            _tempProcess.Hpc_pb = txtPromoBook.Text;
                                            _tempProcess.Hpc_pb_lvl = txtPromoLevel.Text;
                                            _tempProcess.Hpc_pro = _promoCode;
                                            _tempProcess.Hpc_pty_cd = pc;
                                            _tempProcess.Hpc_price_cir_no = txtPriceCirc.Text;
                                            if (cmbCommDef.Text == "Profit Center")
                                            {
                                                _tempProcess.Hpc_pty_tp = "PC";
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_pty_tp = "SCHNL";
                                            }
                                            _tempProcess.Hpc_sch_cd = sch;
                                            // _tempProcess.Hpc_seq = 0;
                                            _tempProcess.Hpc_ser = null;
                                            //_schemeCommDef.Add(_tmpList);

                                            // tHARINDU

                                            if (cmbFPayType.Text == "RATE")
                                            {
                                                _tempProcess.Hpc_is_rt = true;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_is_rt = false;
                                            }
                                            _tempProcess.Hpc_is_rt_typr = cmbFPayType.Text;
                                            _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                            _tempProcess.Hsd_add_calwithvat = chkFPayCalWithVAT.Checked ? 1 : 0;
                                            _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 
                                            _schemeProcess.Add(_tempProcess);
                                            AddCount(_schemeProcess.Count);
                                        }
                                    L12: int y = 1;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (cmbCommDefType.Text == "Customer Wise")
                {
                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (ListViewItem cusList in lstCus.Items)
                                    {
                                        string _cus = cusList.Text;

                                        if (cusList.Checked == true)
                                        {
                                            //foreach (HpSchemeDefinition temp in _schemeCommDef)
                                            //{
                                            //    if (temp.Hpc_pb == txtPriceBook.Text.Trim() && temp.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && temp.Hpc_pty_cd == pc && temp.Hpc_sch_cd == sch && temp.Hpc_cust_cd == _cus && temp.Hpc_from_dt.Date == dtpFromDate.Value.Date)
                                            //    {
                                            //        goto L13;
                                            //    }
                                            //}

                                            //var _record = (from _lst in _schemeProcess
                                            //               where _lst.Hpc_pb == txtPriceBook.Text.Trim() && _lst.Hpc_pb_lvl == txtPriceLevel.Text.Trim() && _lst.Hpc_pty_cd == pc && _lst.Hpc_sch_cd == sch && _lst.Hpc_cust_cd == _cus && _lst.Hpc_from_dt.Date == dtpFromDate.Value.Date && _lst.Hpc_cre_by == BaseCls.GlbUserID
                                            //               select _lst).ToList();

                                            //if (_record.Count > 0)
                                            //{
                                            //    goto L13;
                                            //}

                                            //_tmpList = new HpSchemeDefinition();
                                            _tempProcess = new HpSchemeDefinitionProcess();
                                            _tempProcess.Hpc_brd = null;
                                            _tempProcess.Hpc_cat = null;
                                            _tmpList.Hpc_cir_no = txtSchCircular.Text;
                                            if (cmbCommDefType.Text == "Price Book Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PB";
                                            }
                                            else if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT";
                                            }
                                            else if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT2";
                                            }
                                            else if (cmbCommDefType.Text == "Product Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "ITM";
                                            }
                                            else if (cmbCommDefType.Text == "Promotion Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PROMO";
                                            }
                                            else if (cmbCommDefType.Text == "Customer Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CUS";
                                            }
                                            else if (cmbCommDefType.Text == "Serial Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "SERIAL";
                                            }

                                            _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                            // _tempProcess.Hpc_cre_dt = DateTime.Today.Date;
                                            _tempProcess.Hpc_cust_cd = _cus;
                                            _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                            _tempProcess.Hpc_disc_isrt = _isDisRate;
                                            _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                            _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                            _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                            _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                            _tempProcess.Hpc_is_alw = _isValidSch;
                                            _tempProcess.Hpc_itm = null;
                                            _tempProcess.Hpc_main_cat = null;
                                            _tempProcess.Hpc_pb = txtCusPriceBook.Text;
                                            _tempProcess.Hpc_pb_lvl = txtCusLevel.Text;
                                            _tempProcess.Hpc_pro = null;
                                            _tempProcess.Hpc_pty_cd = pc;
                                            if (cmbCommDef.Text == "Profit Center")
                                            {
                                                _tempProcess.Hpc_pty_tp = "PC";
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_pty_tp = "SCHNL";
                                            }
                                            _tempProcess.Hpc_sch_cd = sch;
                                            //_tempProcess.Hpc_seq = 0;
                                            _tempProcess.Hpc_ser = null;
                                            //_schemeCommDef.Add(_tmpList);

                                            // tHARINDU

                                            if (cmbFPayType.Text == "RATE")
                                            {
                                                _tempProcess.Hpc_is_rt = true;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_is_rt = false;
                                            }
                                            _tempProcess.Hpc_is_rt_typr = cmbFPayType.Text;
                                            _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                            _tempProcess.Hsd_add_calwithvat = chkFPayCalWithVAT.Checked ? 1 : 0;
                                            _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 
                                            _schemeProcess.Add(_tempProcess);
                                            AddCount(_schemeProcess.Count);
                                        }
                                    L13: int z = 1;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (cmbCommDefType.Text == "Serial Wise")
                {
                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (DataGridViewRow row in dgvSerialDetails.Rows)
                                    {
                                        DataGridViewCheckBoxCell chk = row.Cells["col_Ser_Pick"] as DataGridViewCheckBoxCell;

                                        if (Convert.ToBoolean(chk.Value) == true)
                                        {
                                            string _Seritem = row.Cells["col_Ser_Item"].Value.ToString();
                                            string _SerSerial = row.Cells["col_Ser_Serial"].Value.ToString();

                                            //foreach (HpSchemeDefinition temp in _schemeCommDef)
                                            //{
                                            //    if (temp.Hpc_pty_cd == pc && temp.Hpc_sch_cd == sch && temp.Hpc_itm == _Seritem && temp.Hpc_ser == _SerSerial && temp.Hpc_from_dt.Date == dtpFromDate.Value.Date)
                                            //    {
                                            //        goto L14;
                                            //    }
                                            //}

                                            //var _record = (from _lst in _schemeProcess
                                            //               where _lst.Hpc_pty_cd == pc && _lst.Hpc_sch_cd == sch && _lst.Hpc_itm == _Seritem && _lst.Hpc_ser == _SerSerial && _lst.Hpc_from_dt.Date == dtpFromDate.Value.Date && _lst.Hpc_cre_by == BaseCls.GlbUserID
                                            //               select _lst).ToList();

                                            //if (_record.Count > 0)
                                            //{
                                            //    goto L14;
                                            //}

                                            //_tmpList = new HpSchemeDefinition();
                                            _tempProcess = new HpSchemeDefinitionProcess();
                                            _tempProcess.Hpc_brd = null;
                                            _tempProcess.Hpc_cat = null;

                                           //updated by akila 2017/09/01 - Schema creation definition, serial wise
                                            _tempProcess.Hpc_pb = null;
                                            _tempProcess.Hpc_pb_lvl = null;

                                            _tempProcess.Hpc_cir_no = txtSchCircular.Text;
                                            if (cmbCommDefType.Text == "Price Book Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PB";
                                            }
                                            else if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT";
                                            }
                                            else if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT2";
                                            }
                                            else if (cmbCommDefType.Text == "Product Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "ITM";
                                            }
                                            else if (cmbCommDefType.Text == "Promotion Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PROMO";
                                            }
                                            else if (cmbCommDefType.Text == "Customer Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CUS";
                                            }
                                            else if (cmbCommDefType.Text == "Serial Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "SERIAL";
                                                _tempProcess.Hpc_pb = row.Cells["col_Ser_Book"].Value == null ? string.Empty : row.Cells["col_Ser_Book"].Value.ToString();
                                                _tempProcess.Hpc_pb_lvl = row.Cells["col_Ser_Lvl"].Value == null ? string.Empty : row.Cells["col_Ser_Lvl"].Value.ToString();
                                            }

                                            _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                            // _tempProcess.Hpc_cre_dt = DateTime.Today.Date;
                                            _tempProcess.Hpc_cust_cd = null;
                                            _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                            _tempProcess.Hpc_disc_isrt = _isDisRate;
                                            _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                            _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                            _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                            _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                            _tempProcess.Hpc_is_alw = _isValidSch;
                                            _tempProcess.Hpc_itm = _Seritem;
                                            _tempProcess.Hpc_main_cat = null;
                                            //_tempProcess.Hpc_pb = null;
                                            //_tempProcess.Hpc_pb_lvl = null;
                                            _tempProcess.Hpc_pro = null;
                                            _tempProcess.Hpc_pty_cd = pc;
                                            _tempProcess.Hpc_price_cir_no = txtPriceCirc.Text;
                                            if (cmbCommDef.Text == "Profit Center")
                                            {
                                                _tempProcess.Hpc_pty_tp = "PC";
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_pty_tp = "SCHNL";
                                            }
                                            _tempProcess.Hpc_sch_cd = sch;
                                            //_tempProcess.Hpc_seq = 0;
                                            _tempProcess.Hpc_ser = _SerSerial;
                                            // _schemeCommDef.Add(_tmpList);

                                            //tHARINDU

                                            if (cmbFPayType.Text == "RATE")
                                            {
                                                _tempProcess.Hpc_is_rt = true;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_is_rt = false;
                                            }
                                            _tempProcess.Hpc_is_rt_typr = cmbFPayType.Text;
                                            _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                            _tempProcess.Hsd_add_calwithvat = chkFPayCalWithVAT.Checked ? 1 : 0;
                                            _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 
                                            _schemeProcess.Add(_tempProcess);
                                            AddCount(_schemeProcess.Count);
                                        }
                                    L14: int p = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (cmbCommDefType.Text == "Clone Details")
                {
                    List<HpSchemeDefinition> _tempClone = new List<HpSchemeDefinition>();
                    List<string> list = new List<string>();

                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            //List<HpSchemeDefinition> _tmpClone = new List<HpSchemeDefinition>();

                            list.Add(pc);



                            //DataTable _tmpClone = CHNLSVC.Sales.getSchemeByPC(txtClonePc.Text.Trim(), DateTime.Now.Date);

                            ////DataTable temp = null;

                            //List<HpSchemeDefinition> _cloneDet = DataTableExtensions.ToGenericList<HpSchemeDefinition>(_tmpClone, HpSchemeDefinition.Converter);

                            //if (_tmpClone != null)
                            //{


                            //    foreach (HpSchemeDefinition _tmp in _cloneDet)
                            //    {
                            //        HpSchemeDefinition _tempCloneProcess = new HpSchemeDefinition();

                            //        _tempCloneProcess.Hpc_brd = _tmp.Hpc_brd;
                            //        _tempCloneProcess.Hpc_cat = _tmp.Hpc_cat;

                            //        _tempCloneProcess.Hpc_cir_no = txtSchCircular.Text;
                            //        _tempCloneProcess.Hpc_comm_cat = _tmp.Hpc_comm_cat;
                            //        _tempCloneProcess.Hpc_cre_by = BaseCls.GlbUserID;
                            //        // _tempProcess.Hpc_cre_dt = DateTime.Today.Date;
                            //        _tempCloneProcess.Hpc_cust_cd = _tmp.Hpc_cust_cd;
                            //        _tempCloneProcess.Hpc_disc = _tmp.Hpc_disc;
                            //        _tempCloneProcess.Hpc_disc_isrt = _tmp.Hpc_disc_isrt;
                            //        _tempCloneProcess.Hpc_dp_comm = _tmp.Hpc_dp_comm;
                            //        _tempCloneProcess.Hpc_from_dt = _tmp.Hpc_from_dt;
                            //        _tempCloneProcess.Hpc_to_dt = _tmp.Hpc_to_dt;
                            //        _tempCloneProcess.Hpc_inst_comm = _tmp.Hpc_inst_comm;
                            //        _tempCloneProcess.Hpc_is_alw = _tmp.Hpc_is_alw;
                            //        _tempCloneProcess.Hpc_itm = _tmp.Hpc_itm;
                            //        _tempCloneProcess.Hpc_main_cat = _tmp.Hpc_main_cat;
                            //        _tempCloneProcess.Hpc_pb = _tmp.Hpc_pb;
                            //        _tempCloneProcess.Hpc_pb_lvl = _tmp.Hpc_pb_lvl;
                            //        _tempCloneProcess.Hpc_pro = _tmp.Hpc_pro;
                            //        _tempCloneProcess.Hpc_pty_cd = pc;
                            //        _tempCloneProcess.Hpc_pty_tp = "PC";
                            //        _tempCloneProcess.Hpc_sch_cd = _tmp.Hpc_sch_cd;
                            //        //_tempProcess.Hpc_seq = 0;
                            //        _tempCloneProcess.Hpc_ser = _tmp.Hpc_ser;

                            //        _tempClone.Add(_tempCloneProcess);
                            //        AddCount(_tempClone.Count);

                            //    }


                            //    //_saveCount = _saveCount + tempDo.Count;
                            //    //AddSaveCount(_saveCount);
                            //}
                            //else
                            //{
                            //    MessageBox.Show("No any applicable details are found.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                        }
                    }

                    _processEffect = (Int16)(CHNLSVC.Sales.SaveNewSchemeCloneRevamp(txtClonePc.Text.Trim(), DateTime.Now.Date, list, BaseCls.GlbUserID, txtSchCircular.Text));

                    //var _tempClonepc = (from _lst in _tempClone
                    //               select _lst.Hpc_pty_cd).ToList().Distinct();

                    //Int32 _saveClone = 0;
                    //foreach (string _pc in _tempClonepc)
                    //{
                    //    List<HpSchemeDefinition> tempListClone = (from _lst in _tempClone
                    //                                              where _lst.Hpc_pty_cd == _pc
                    //                                              select _lst).ToList();

                    //    DataTable dt1 = ConvertToDataTable(tempListClone);
                    //    dt1.TableName = "schClone";

                    //    _processEffect = (Int16)(CHNLSVC.Sales.SaveNewSchemeClone(dt1));

                    //    _saveClone = _saveClone + tempListClone.Count;
                    //    AddSaveCount(_saveClone);
                    //}

                    TimeSpan end2 = DateTime.Now.TimeOfDay;

                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("STEP 01\t" + (end2 - start).ToString());

                    if (_processEffect == 1)
                    {
                        MessageBox.Show("Succsussfully clone.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear_Data();
                        return;
                    }

                }

                //Tharindu 
                else if (cmbCommDefType.Text == "Brand And Main Category Wise" || cmbCommDefType.Text == "Brand And Sub Category Wise")
                {


                    foreach (ListViewItem pcList in lstPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (ListViewItem itmList in lstItem.Items)
                                    {
                                        string _item = itmList.Text;

                                        if (itmList.Checked == true)
                                        {

                                            List<string> tmpList = new List<string>();
                                            tmpList = itmList.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                                            string maincat = null;
                                            string brand = null;
                                            string subcat = null;

                                            if (cmbCommDefType.Text == "Brand And Sub Category Wise")
                                            {
                                                if ((tmpList != null) && (tmpList.Count > 0))
                                                {
                                                    subcat = tmpList[0];
                                                    brand = tmpList[1];
                                                }
                                            }
                                            else
                                            {
                                                if ((tmpList != null) && (tmpList.Count > 0))
                                                {
                                                    maincat = tmpList[0];
                                                    brand = tmpList[1];
                                                }
                                            }

                                           

                                            _tempProcess = new HpSchemeDefinitionProcess();
                                            _tempProcess.Hpc_brd = txtBrand.Text;
                                            if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_cat = _item;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_cat = null;
                                            }
                                            _tempProcess.Hpc_cir_no = txtSchCircular.Text;
                                            if (cmbCommDefType.Text == "Price Book Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PB";
                                            }
                                            else if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT";
                                            }
                                            else if (cmbCommDefType.Text == "Sub Category Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CAT2";
                                            }
                                            else if (cmbCommDefType.Text == "Promotion Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "PROMO";
                                            }
                                            else if (cmbCommDefType.Text == "Customer Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "CUS";
                                            }
                                            else if (cmbCommDefType.Text == "Serial Wise")
                                            {
                                                _tempProcess.Hpc_comm_cat = "SERIAL";
                                            }

                                            _tempProcess.Hpc_cre_by = BaseCls.GlbUserID;
                                            //_tmpList.Hpc_cre_dt = DateTime.Today.Date;
                                            _tempProcess.Hpc_cust_cd = null;
                                            _tempProcess.Hpc_disc = Convert.ToDecimal(txtSchDis.Text);
                                            _tempProcess.Hpc_disc_isrt = _isDisRate;
                                            _tempProcess.Hpc_dp_comm = Convert.ToDecimal(txtDownPayRate.Text);
                                            _tempProcess.Hpc_from_dt = dtpFromDate.Value.Date;
                                            _tempProcess.Hpc_to_dt = dtpToDate.Value.Date;
                                            _tempProcess.Hpc_inst_comm = Convert.ToDecimal(txtInstallmentRate.Text);
                                            _tempProcess.Hpc_is_alw = _isValidSch;
                                            _tempProcess.Hpc_itm = null;
                                            _tempProcess.Hpc_price_cir_no = txtPriceCirc.Text;
                                            if (cmbCommDefType.Text == "Main Category Wise")
                                            {
                                                _tempProcess.Hpc_main_cat = _item;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_main_cat = null;
                                            }
                                            _tempProcess.Hpc_pb = txtPriceBook.Text;
                                            _tempProcess.Hpc_pb_lvl = txtPriceLevel.Text;
                                            _tempProcess.Hpc_pro = null;
                                            _tempProcess.Hpc_pty_cd = pc;
                                            if (cmbCommDef.Text == "Profit Center")
                                            {
                                                _tempProcess.Hpc_pty_tp = "PC";
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_pty_tp = "SCHNL";
                                            }
                                            _tempProcess.Hpc_sch_cd = sch;
                                            //_tempProcess.Hpc_seq = 0;
                                            _tempProcess.Hpc_ser = null;
                                            //_schemeCommDef.Add(_tmpList);

                                            _tempProcess.Hpc_main_cat = maincat;
                                            _tempProcess.Hpc_brd = brand;
                                            _tempProcess.Hpc_cat = subcat;

                                            if (cmbAddType.Text == "RATE")
                                            {
                                                _tempProcess.Hpc_is_rt = true;
                                            }
                                            else
                                            {
                                                _tempProcess.Hpc_is_rt = false;
                                            }
                                            _tempProcess.Hpc_is_rt_typr = cmbAddType.Text;
                                            _tempProcess.Hpc_fpay = Convert.ToDecimal(txtFPayValue.Text);
                                            _tempProcess.Hsd_add_calwithvat = chkFPayCalWithVAT.Checked ? 1 : 0;
                                            _tempProcess.Hpc_with_vat = chkFPayCalWithVAT.Checked ? true : false; 
                                            _schemeProcess.Add(_tempProcess);
                                            AddCount(_schemeProcess.Count);

                                        }
                                    L11: int x = 1;
                                    }
                                }
                            }
                        }
                    }
                }

                dgvDefDetails.AutoGenerateColumns = false;
                dgvDefDetails.DataSource = new List<HpSchemeDefinition>();
                dgvDefDetails.DataSource = _schemeProcess;

                var _tempDO = (from _lst in _schemeProcess
                               select _lst.Hpc_pty_cd).ToList().Distinct();

                Int32 _saveCount = 0;

                foreach (string _pc in _tempDO)
                {
                    List<HpSchemeDefinitionProcess> tempDo = (from _lst in _schemeProcess
                                                              where _lst.Hpc_pty_cd == _pc
                                                              select _lst).ToList();

                    DataTable dt = ConvertToDataTable(tempDo);
                    dt.TableName = "schSchemeCommDef";

                    _processEffect = (Int16)(CHNLSVC.Sales.SaveNewSchemeCommProcess(dt));

                    _saveCount = _saveCount + tempDo.Count;
                    AddSaveCount(_saveCount);
                }


                cmbCommDefType.Enabled = false;
                btnApproved.Enabled = false;
                txtSchCircular.Enabled = false;
                //cmbCommDefType_SelectedIndexChanged(null, null);
                btnCommAdd.Enabled = true;
                btnCommSave.Enabled = true;
                TimeSpan end1 = DateTime.Now.TimeOfDay;

                Cursor.Current = Cursors.Default;
                MessageBox.Show("STEP 01\t" + (end1 - start).ToString());

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                btnCommSave.Enabled = true;
                btnCommAdd.Enabled = true;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCommSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (dgvDefDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Cannot find commission definition details.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_schemeCommDef.Count == 0 && _schemeProcess.Count == 0)
                {
                    MessageBox.Show("Cannot find commission definition details.Please apply before save.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<HpSchemeDefinition> _recallList = new List<HpSchemeDefinition>();

                _recallList = CHNLSVC.Sales.GetSchemeDetailsByCir(txtSchCircular.Text.Trim());

                if (_recallList.Count > 0 && _recallList != null)
                {

                    var _record = (from _lst in _recallList
                                   select _lst.Hpc_stus).Distinct().ToList();


                    foreach (var tmpRec in _record)
                    {
                        if (tmpRec == "C")
                        {
                            MessageBox.Show("Circular is already exsist.Cancel Circular.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (tmpRec == "A")
                        {
                            MessageBox.Show("Circular is already exsist.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (tmpRec == "S")
                        {
                            if (MessageBox.Show("Do you want to ammend selected circular ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                            {
                                return;
                            }
                        }
                    }

                }


                // DataTable dt = ConvertToDataTable(_schemeCommDef);
                if (MessageBox.Show("Are you sure ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                Cursor.Current = Cursors.WaitCursor;
                row_aff = (Int32)CHNLSVC.Sales.SaveNewSchemeCommDefinition(BaseCls.GlbUserID, txtSchCircular.Text.Trim());

                if (row_aff == 1)
                {
                   // sendMail();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Commission definition created successfully.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }


        private void txtSchCircular_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dtpFromDate.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
                    DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.obj_TragetTextBox = txtSchCircular;
                    _CommonSearch.ShowDialog();
                    txtSchCircular.Select();

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpToDate.Focus();
            }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInstallmentRate.Focus();
            }
        }

        private void txtInstallmentRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDownPayRate.Focus();
            }
        }

        private void txtDownPayRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSchDisType.Focus();
            }
        }

        private void btnCommClear_Click(object sender, EventArgs e)
        {
            Clear_Comm_Def();
        }

        private void btnCommSchSelect_Click(object sender, EventArgs e)
        {

            foreach (ListViewItem Item in lstSch.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnCommSchUnSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSch.Items)
            {
                Item.Checked = false;
            }
        }

        private void txtInstallmentRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInstallmentRate.Text))
            {
                if (!IsNumeric(txtInstallmentRate.Text))
                {
                    MessageBox.Show("Entered installment rate is invalid.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInstallmentRate.Text = "";
                    txtInstallmentRate.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtInstallmentRate.Text) > 100 || Convert.ToDecimal(txtInstallmentRate.Text) < 0)
                {
                    MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInstallmentRate.Text = "";
                    txtInstallmentRate.Focus();
                    return;
                }
            }
        }

        private void txtDownPayRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDownPayRate.Text))
            {
                if (!IsNumeric(txtDownPayRate.Text))
                {
                    MessageBox.Show("Entered down payment rate is invalid.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDownPayRate.Text = "";
                    txtDownPayRate.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtDownPayRate.Text) > 100 || Convert.ToDecimal(txtDownPayRate.Text) < 0)
                {
                    MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDownPayRate.Text = "";
                    txtDownPayRate.Focus();
                    return;
                }
            }
        }

        private void txtMainCate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSubCate.Enabled == true)
                    {
                        txtSubCate.Focus();
                    }
                    else if (txtItemRange.Enabled == true)
                    {
                        txtItemRange.Focus();
                    }
                    else if (txtBrand.Enabled == true)
                    {
                        txtBrand.Focus();
                    }
                    else if (txtItem.Enabled == true)
                    {
                        txtItem.Focus();
                    }
                    else if (btnLoadProducts.Enabled == true)
                    {
                        btnLoadProducts.Focus();
                    }
                    else
                    {
                        txtChanel.Focus();
                    }
                }
                else if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtMainCate;
                    _CommonSearch.ShowDialog();
                    txtMainCate.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchMainCate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMainCate;
                _CommonSearch.ShowDialog();
                txtMainCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMainCate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMainCate;
                _CommonSearch.ShowDialog();
                txtMainCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSubCate_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubCate;
                _CommonSearch.ShowDialog();
                txtSubCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSubCate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSubCate;
                    _CommonSearch.ShowDialog();
                    txtSubCate.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtItemRange.Enabled == true)
                    {
                        txtItemRange.Focus();
                    }
                    else if (txtBrand.Enabled == true)
                    {
                        txtBrand.Focus();
                    }
                    else if (txtItem.Enabled == true)
                    {
                        txtItem.Focus();
                    }
                    else if (btnLoadProducts.Enabled == true)
                    {
                        btnLoadProducts.Focus();
                    }
                    else
                    {
                        txtChanel.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSubCate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubCate;
                _CommonSearch.ShowDialog();
                txtSubCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSubCate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubCate.Text)) return;
                //if (string.IsNullOrEmpty(txtMainCate.Text)) { MessageBox.Show("Please select the main category first", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSubCate.Clear(); txtMainCate.Focus(); return; }

                if (!string.IsNullOrEmpty(txtMainCate.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtSubCate.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Please select the valid category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSubCate.Clear();
                        txtSubCate.Focus();
                        return;
                    }
                }
                else
                {
                    MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(txtSubCate.Text);
                    if (subCate.Ric2_cd == null)
                    {
                        MessageBox.Show("Please select the valid category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSubCate.Clear();
                        txtSubCate.Focus();
                        return;
                    }
                }
                txtItemRange.Clear();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMainCate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMainCate.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtMainCate.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMainCate.Clear();
                    txtMainCate.Focus();
                    return;
                }
                txtSubCate.Clear();
                txtItemRange.Clear();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchRange_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemRange;
                _CommonSearch.ShowDialog();
                txtItemRange.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemRange_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItemRange;
                    _CommonSearch.ShowDialog();
                    txtItemRange.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    if (txtBrand.Enabled == true)
                    {
                        txtBrand.Focus();
                    }
                    else if (txtItem.Enabled == true)
                    {
                        txtItem.Focus();
                    }
                    else if (btnLoadProducts.Enabled == true)
                    {
                        btnLoadProducts.Focus();
                    }
                    else
                    {
                        txtChanel.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemRange_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemRange;
                _CommonSearch.ShowDialog();
                txtItemRange.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItemRange_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemRange.Text)) return;
                if (string.IsNullOrEmpty(txtItemRange.Text)) { MessageBox.Show("Please select the main category first", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txtItemRange.Clear(); txtItemRange.Focus(); return; }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtItemRange.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid item range.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemRange.Clear();
                    txtItemRange.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtBrand;
                    _CommonSearch.ShowDialog();
                    txtBrand.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtItem.Enabled == true)
                    {
                        txtItem.Focus();
                    }
                    else if (btnLoadProducts.Enabled == true)
                    {
                        btnLoadProducts.Focus();
                    }
                    else
                    {
                        txtChanel.Focus();
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBrand_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrand.Text))
                {
                    MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(txtBrand.Text.Trim());

                    if (_brd.Mb_cd == null)
                    {
                        MessageBox.Show("Please select the valid brand.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBrand.Text = "";
                        txtBrand.Focus();
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
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Clear();
                        txtItem.Focus();

                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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
                else if (e.KeyCode == Keys.Enter)
                {
                    if (btnLoadProducts.Enabled == true)
                    {
                        btnLoadProducts.Focus();
                    }
                    else
                    {
                        txtChanel.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadProducts_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbCommDefType.Text == "Product Wise")
                {
                    if (string.IsNullOrEmpty(txtMainCate.Text) && string.IsNullOrEmpty(txtSubCate.Text) && string.IsNullOrEmpty(txtItemRange.Text) && string.IsNullOrEmpty(txtBrand.Text) && string.IsNullOrEmpty(txtItem.Text))
                    {
                        MessageBox.Show("Please enter searching parameters.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMainCate.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtMainCate.Text) && (!string.IsNullOrEmpty(txtSubCate.Text)))
                    {
                        MessageBox.Show("Cannot search by sub category only.Please select main category as well.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSubCate.Text = "";
                        txtMainCate.Focus();
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtItemRange.Text) && (string.IsNullOrEmpty(txtSubCate.Text)))
                    {
                        MessageBox.Show("Cannot search by item range without selecting sub category.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemRange.Text = "";
                        txtSubCate.Focus();
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtItem.Text))
                    {
                        lstItem.Items.Add(txtItem.Text.Trim());
                    }
                    else
                    {

                        // lstItem.Clear();
                        //List<MasterItem> _tmpItem = CHNLSVC.Sales.GetItemsByCateAndBrand(txtMainCate.Text, txtSubCate.Text, txtItemRange.Text, txtBrand.Text, BaseCls.GlbUserComCode);
                        DataTable _dtItm = CHNLSVC.Sales.GetItemsByCateAndBrandNew(txtMainCate.Text, txtSubCate.Text, txtItemRange.Text, txtBrand.Text, BaseCls.GlbUserComCode);

                        foreach (DataRow drow in _dtItm.Rows)
                        {
                            lstItem.Items.Add(drow["mi_cd"].ToString());
                        }


                        //foreach (MasterItem _temp in _tmpItem)
                        //{
                        //    lstItem.Items.Add(_temp.Mi_cd);
                        //}

                    }
                }
                if (cmbCommDefType.Text == "Main Category Wise")
                {
                    if (!string.IsNullOrEmpty(txtMainCate.Text))
                    {
                        ListViewItem item1 = lstItem.FindItemWithText(txtMainCate.Text);
                        if (item1 == null)
                        {
                            lstItem.Items.Add(txtMainCate.Text.Trim());
                        }
                    }
                }
                if (cmbCommDefType.Text == "Sub Category Wise")
                {
                    if (!string.IsNullOrEmpty(txtSubCate.Text))
                    {
                        ListViewItem item1 = lstItem.FindItemWithText(txtSubCate.Text);
                        if (item1 == null)
                        {

                            lstItem.Items.Add(txtSubCate.Text.Trim());
                        }

                    }
                }

                // Tahrindu 
                if (cmbCommDefType.Text == "Brand And Main Category Wise")
                {
                    if (string.IsNullOrEmpty(txtMainCate.Text))
                    {
                        MessageBox.Show("Category Cannot be Empty", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(txtBrand.Text))
                    {
                        MessageBox.Show("Brand Cannot be Empty", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtMainCate.Text))
                    {
                        ListViewItem item1 = lstItem.FindItemWithText(txtMainCate.Text);
                        if (item1 == null)
                        {
                            lstItem.Items.Add(txtMainCate.Text.Trim() + "|" + txtBrand.Text.Trim());
                        }
                    }
                      
                }

                if (cmbCommDefType.Text == "Brand And Sub Category Wise")
                {
                    if (string.IsNullOrEmpty(txtMainCate.Text))
                    {
                        MessageBox.Show("Category Cannot be Empty", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(txtBrand.Text))
                    {
                        MessageBox.Show("Brand Cannot be Empty", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtMainCate.Text))
                    {
                        ListViewItem item1 = lstItem.FindItemWithText(txtMainCate.Text);
                        if (item1 == null)
                        {
                            lstItem.Items.Add(txtSubCate.Text.Trim() + "|" + txtBrand.Text.Trim());
                        }
                    }

                   
                  //  lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                }

                txtMainCate.Text = "";
                txtSubCate.Text = "";
                txtItemRange.Text = "";
                txtBrand.Text = "";
                txtItem.Text = "";
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstItem.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnselectItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstItem.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            lstItem.Clear();
            txtMainCate.Text = "";
            txtSubCate.Text = "";
            txtItemRange.Text = "";
            txtBrand.Text = "";
            txtItem.Text = "";
            txtPriceBook.Text = "";
            txtPriceLevel.Text = "";
            txtPriceBook.Focus();
        }

        private void btnPromoSearchBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromoBook;
                _CommonSearch.ShowDialog();
                txtPromoBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoBook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoBook;
                    _CommonSearch.ShowDialog();
                    txtPromoBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtPromoLevel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoBook_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromoBook;
                _CommonSearch.ShowDialog();
                txtPromoBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPromoSearchLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPromoBook.Clear();
                    txtPromoBook.Focus();
                    return;
                }

                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromoLevel;
                _CommonSearch.ShowDialog();
                txtPromoLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPromoBook.Clear();
                    txtPromoBook.Focus();
                    return;
                }

                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromoLevel;
                _CommonSearch.ShowDialog();
                txtPromoLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtPromoBook.Text))
                    {
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPromoBook.Clear();
                        txtPromoBook.Focus();
                        return;
                    }

                    _searchType = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoLevel;
                    _CommonSearch.ShowDialog();
                    txtPromoLevel.Select();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtPromoBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPromoBook.Clear();
                    txtPromoBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoLevel.Text)) return;
                if (string.IsNullOrEmpty(txtPromoBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPromoLevel.Clear(); txtPromoBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPromoBook.Text.Trim(), txtPromoLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPromoLevel.Clear(); txtPromoLevel.Focus(); return; }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchPromoCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.promoCode);
                DataTable _result = CHNLSVC.CommonSearch.Get_Promotion_Codes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromoCode;
                _CommonSearch.ShowDialog();
                txtPromoCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.promoCode);
                DataTable _result = CHNLSVC.CommonSearch.Get_Promotion_Codes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromoCode;
                _CommonSearch.ShowDialog();
                txtPromoCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.promoCode);
                    DataTable _result = CHNLSVC.CommonSearch.Get_Promotion_Codes(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoCode;
                    _CommonSearch.ShowDialog();
                    txtPromoCode.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchPromoCir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoBook.Text) && string.IsNullOrEmpty(txtPromoLevel.Text))
                {
                    txtPromoCir.Text = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.searchCircular);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoCir;
                    _CommonSearch.ShowDialog();
                    txtPromoCir.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchByBookAndLevel(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoCir;
                    _CommonSearch.ShowDialog();
                    txtPromoCir.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoCir_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoBook.Text) && string.IsNullOrEmpty(txtPromoLevel.Text))
                {
                    txtPromoCir.Text = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.searchCircular);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoCir;
                    _CommonSearch.ShowDialog();
                    txtPromoCir.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchByBookAndLevel(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoCir;
                    _CommonSearch.ShowDialog();
                    txtPromoCir.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoCir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtPromoBook.Text) && string.IsNullOrEmpty(txtPromoLevel.Text))
                    {
                        txtPromoCir.Text = "";
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.searchCircular);
                        DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtPromoCir;
                        _CommonSearch.ShowDialog();
                        txtPromoCir.Select();
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByBook);
                        DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchByBookAndLevel(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtPromoCir;
                        _CommonSearch.ShowDialog();
                        txtPromoCir.Select();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPromoCir_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtPromoCir.Text)) return;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByBook);
                //DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchByBookAndLevel(SearchParams, null, null);
                //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CIRCULAR") == txtPromoCir.Text.Trim()).ToList();
                //if (_validate == null || _validate.Count <= 0)
                //{
                //    MessageBox.Show("Please select the valid circular no", "Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtPromoCir.Clear();
                //    txtPromoCir.Focus();
                //    return;
                //}
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtPromoCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoCode.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.promoCode);
                DataTable _result = CHNLSVC.CommonSearch.Get_Promotion_Codes(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtPromoCode.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid circular no", "Circular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPromoCode.Clear();
                    txtPromoCode.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendMail()
        {
            if (chkPC.Checked)
            {
                string _mail = "";

                List<string> list = new List<string>();
                list.Add("kapila@abansgroup.com");
                list.Add("christina@abansgroup.com");

                _mail += "Dear Sir/Madam," + Environment.NewLine + Environment.NewLine;
                _mail += "Scheme Circular " + txtSchCircular.Text + " is activated for price circular " + txtPriceCirc.Text + Environment.NewLine + Environment.NewLine;

                _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;

                for (int i = 0; i < list.Count; i++)
                {
                    CHNLSVC.CommonSearch.Send_SMTPMail(list[i], "New Scheme Circular", _mail);
                }

            }
            //    Int32 _pricTp = 0;
            //    string _promoCd = "";
            //    oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, null, 10);


            //    if (oTemplate != null && oTemplate.Sml_templ_mail != null)
            //    {
            //        string emailBody = oTemplate.Sml_templ_mail;

            //        _dtCirc = CHNLSVC.Sales.GetCircularNo(_circNo);
            //        if (_dtCirc.Rows.Count > 0)
            //        {
            //            _promoCirc = _circNo;
            //            emailBody = emailBody.Replace("[circularno]", _circNo);
            //            emailBody = emailBody.Replace("[validfrom]", _dtCirc.Rows[0]["Sapd_from_date"].ToString());
            //            emailBody = emailBody.Replace("[validto]", _dtCirc.Rows[0]["Sapd_to_date"].ToString());
            //            _pricTp = Convert.ToInt32(_dtCirc.Rows[0]["sapd_price_type"]);
            //            _promoCd = _dtCirc.Rows[0]["sapd_promo_cd"].ToString();
            //        }

            //        _rowExist = false;
            //        param.Clear();

            //        #region price type 0
            //        if (_pricTp == 0)
            //        {
            //            _dt = CHNLSVC.Sales.GetMailPBLevels(_circNo);
            //            foreach (DataRow r in _dt.Rows)     //price book and price levels
            //            {
            //                _dt1 = CHNLSVC.Sales.GetMailLocations(r["sapd_pb_tp_cd"].ToString(), r["sapd_pbk_lvl_cd"].ToString());
            //                foreach (DataRow r1 in _dt1.Rows)   //location list
            //                {
            //                    foreach (DataRow drow in param.Rows)
            //                    {
            //                        if (drow["SR"].ToString() == r1["sadd_pc"].ToString())
            //                        {
            //                            _rowExist = true;
            //                            break;
            //                        }
            //                    }
            //                    if (_rowExist == false)
            //                    {
            //                        dr = param.NewRow();
            //                        dr["SR"] = r1["sadd_pc"].ToString();
            //                        param.Rows.Add(dr);
            //                        _rowExist = false;
            //                    }
            //                }
            //            }

            //            foreach (DataRow drowloc in param.Rows)
            //            {
            //                _masterPC = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, drowloc["SR"].ToString());
            //                if (_masterPC != null)
            //                {
            //                    Service_Message oMessage = new Service_Message();
            //                    oMessage.Sm_email = _masterPC.Mpc_email;

            //                    oMessage.Sm_com = BaseCls.GlbUserComCode;
            //                    oMessage.Sm_jobno = _promoCirc;
            //                    oMessage.Sm_joboline = 1;
            //                    oMessage.Sm_jobstage = 0;
            //                    oMessage.Sm_ref_num = string.Empty;
            //                    oMessage.Sm_status = 0;
            //                    oMessage.Sm_msg_tmlt_id = 10;
            //                    oMessage.Sm_sms_text = string.Empty;
            //                    oMessage.Sm_sms_gap = 0;
            //                    oMessage.Sm_sms_done = 0;
            //                    oMessage.Sm_mail_text = emailBody;
            //                    oMessage.Sm_mail_gap = 0;
            //                    oMessage.Sm_email_done = 0;
            //                    oMessage.Sm_cre_by = BaseCls.GlbUserID;
            //                    oMessage.Sm_cre_dt = DateTime.Now;
            //                    oMessage.Sm_mod_by = BaseCls.GlbUserID;
            //                    oMessage.Sm_mod_dt = DateTime.Now;

            //                    int result = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
            //                }
            //            }
            //        }
            //        #endregion
            //        else
            //        {
            //            DataTable _dtPC = CHNLSVC.Sales.GetProfitCenterDetail(BaseCls.GlbUserComCode, _pricTp, "", "", _promoCd);
            //            foreach (DataRow r in _dtPC.Rows)
            //            {
            //                Service_Message oMessage = new Service_Message();
            //                oMessage.Sm_email = r["Mpc_email"].ToString();

            //                oMessage.Sm_com = BaseCls.GlbUserComCode;
            //                oMessage.Sm_jobno = _promoCirc;
            //                oMessage.Sm_joboline = 1;
            //                oMessage.Sm_jobstage = 0;
            //                oMessage.Sm_ref_num = string.Empty;
            //                oMessage.Sm_status = 0;
            //                oMessage.Sm_msg_tmlt_id = 10;
            //                oMessage.Sm_sms_text = string.Empty;
            //                oMessage.Sm_sms_gap = 0;
            //                oMessage.Sm_sms_done = 0;
            //                oMessage.Sm_mail_text = emailBody;
            //                oMessage.Sm_mail_gap = 0;
            //                oMessage.Sm_email_done = 0;
            //                oMessage.Sm_cre_by = BaseCls.GlbUserID;
            //                oMessage.Sm_cre_dt = DateTime.Now;
            //                oMessage.Sm_mod_by = BaseCls.GlbUserID;
            //                oMessage.Sm_mod_dt = DateTime.Now;

            //                int result = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
            //            }
            //        }
            //    }
        }
        private void btnAddPromo_Click(object sender, EventArgs e)
        {
            try
            {
                _promoDetails = new List<PriceDetailRef>();
                _promoDetails = CHNLSVC.Sales.GetPriceDetailsByCir(txtPromoBook.Text, txtPromoLevel.Text, txtPromoCode.Text, txtPromoCir.Text);

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = new List<PriceDetailRef>();
                dgvPromo.DataSource = _promoDetails;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPromo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgvPromo.Rows[dgvPromo.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
                case "True":
                    {
                        ch1.Value = false;
                        break;
                    }
            }

            Int32 _pbSeq = 0;
            Int32 _Seq = 0;
            string _mainItem = "";
            lstPromoItem.Clear();

            _pbSeq = Convert.ToInt32(dgvPromo.Rows[e.RowIndex].Cells["col_p_pbSeq"].Value);
            _Seq = Convert.ToInt32(dgvPromo.Rows[e.RowIndex].Cells["col_p_Seq"].Value);
            _mainItem = dgvPromo.Rows[e.RowIndex].Cells["col_P_Item"].Value.ToString();

            List<PriceCombinedItemRef> _combineItems = new List<PriceCombinedItemRef>();
            _combineItems = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbSeq, _Seq, _mainItem, string.Empty);

            foreach (PriceCombinedItemRef tmpPromo in _combineItems)
            {
                lstPromoItem.Items.Add(tmpPromo.Sapc_itm_cd);
            }
        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            _promoDetails = new List<PriceDetailRef>();
            dgvPromo.AutoGenerateColumns = false;
            dgvPromo.DataSource = new List<PriceDetailRef>();
            lstPromoItem.Clear();
            txtPromoBook.Text = "";
            txtPromoLevel.Text = "";
            txtPromoCir.Text = "";
            txtPromoCode.Text = "";
            txtPromoBook.Focus();
        }

        private void btnSelectAllPromo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPromo.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == false)
                {
                    chk.Value = true;
                }

            }
        }

        private void btnUnSelectAllPromo_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPromo.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }

            }
        }

        private void btnSearchCusBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusPriceBook;
                _CommonSearch.ShowDialog();
                txtCusPriceBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusPriceBook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusPriceBook;
                    _CommonSearch.ShowDialog();
                    txtCusPriceBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCusLevel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusPriceBook_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusPriceBook;
                _CommonSearch.ShowDialog();
                txtCusPriceBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchCusLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusPriceBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPriceBook.Clear();
                    txtCusPriceBook.Focus();
                    return;
                }


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusLevel;
                _CommonSearch.ShowDialog();
                txtCusLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtCusPriceBook.Text))
                    {
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusPriceBook.Clear();
                        txtCusPriceBook.Focus();
                        return;
                    }


                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusLevel;
                    _CommonSearch.ShowDialog();
                    txtCusLevel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCustomerCode.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusPriceBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPriceBook.Clear();
                    txtCusPriceBook.Focus();
                    return;
                }


                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusLevel;
                _CommonSearch.ShowDialog();
                txtCusLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchCusCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomerCode;
                _CommonSearch.ShowDialog();
                txtCustomerCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCustomerCode;
                    _CommonSearch.ShowDialog();
                    txtCustomerCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddCusDetails.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtCustomerCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustomerCode;
                _CommonSearch.ShowDialog();
                txtCustomerCode.Select();


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtCustomerCode_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCustomerCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomerCode.Text.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd == null)
                    {
                        MessageBox.Show("Please enter valid customer code.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCustomerCode.Text = "";
                        txtCustomerCode.Focus();
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

        }

        private void btnAddCusDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusPriceBook.Text))
                {
                    MessageBox.Show("Please select applicable price book.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomerCode.Text = "";
                    txtCusPriceBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusLevel.Text))
                {
                    MessageBox.Show("Please select applicable price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomerCode.Text = "";
                    txtCusPriceBook.Text = "";
                    txtCusLevel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCustomerCode.Text))
                {
                    MessageBox.Show("Please select applicable customer.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomerCode.Focus();
                    return;
                }
                lstCus.Items.Add(txtCustomerCode.Text.Trim());

                txtCustomerCode.Text = "";
                txtCusPriceBook.Enabled = false;
                txtCusLevel.Enabled = false;
                txtCustomerCode.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCusClear_Click(object sender, EventArgs e)
        {
            txtCustomerCode.Text = "";
            txtCusPriceBook.Text = "";
            txtCusLevel.Text = "";
            txtCusPriceBook.Enabled = true;
            txtCusLevel.Enabled = true;
            lstCus.Clear();
            txtCusPriceBook.Focus();
        }

        private void btnCusUnSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstCus.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnCusSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstCus.Items)
            {
                Item.Checked = true;
            }
        }

        private void cmbSchDisType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSchDis.Focus();
            }
        }

        private void txtSchDis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkSchRestrict.Focus();
            }
        }

        private void chkSchRestrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCommAdd.Focus();
            }
        }

        private void txtSchDis_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSchDis.Text))
            {
                if (!IsNumeric(txtSchDis.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSchDis.Text = "";
                    txtSchDis.Focus();
                    return;
                }

                if (cmbSchDisType.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtSchDis.Text) > 100 || Convert.ToDecimal(txtSchDis.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSchDis.Text = "";
                        txtSchDis.Focus();
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtSchDis.Text) < 0)
                    {
                        MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSchDis.Text = "";
                        txtSchDis.Focus();
                        return;
                    }
                }

                txtSchDis.Text = Convert.ToDecimal(txtSchDis.Text).ToString("n");
            }
        }

        private void cmbSchDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSchDis.Text = "";
        }

        private void cmbGurApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGurApp.Text == "Profit Center")
            {
                txtAppGurChannel.Text = "";
                txtAppGurPc.Text = "";
                txtAppGurSubChannel.Text = "";
                txtAppGurChannel.Enabled = true;
                txtAppGurPc.Enabled = true;
                txtAppGurSubChannel.Enabled = true;
                btnSearchGurChannel.Enabled = true;
                btnSearchGurSubChannel.Enabled = true;
                btnSearchGurPC.Enabled = true;
                lstGurAppPc.Clear();
                btnGurAppAdd.Enabled = true;
                txtAppGurChannel.Focus();

            }
            else
            {
                txtAppGurChannel.Text = "";
                txtAppGurPc.Text = "";
                txtAppGurSubChannel.Text = "";
                txtAppGurChannel.Enabled = false;
                txtAppGurPc.Enabled = false;
                txtAppGurSubChannel.Enabled = false;
                btnSearchGurChannel.Enabled = false;
                btnSearchGurSubChannel.Enabled = false;
                btnSearchGurPC.Enabled = false;
                btnGurAppAdd.Enabled = false;
                lstGurAppPc.Clear();
            }
        }

        private void btnGurAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbGurCheckOn.Text))
                {
                    MessageBox.Show("please select cheking on parameter.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGurCheckOn.Text = "";
                    cmbGurCheckOn.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtGurValFrom.Text))
                {
                    MessageBox.Show("Please enter value from amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValFrom.Text = "";
                    txtGurValFrom.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtGurValTo.Text))
                {
                    MessageBox.Show("Please enter value to amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValTo.Text = "";
                    txtGurValTo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNoofGur.Text))
                {
                    MessageBox.Show("Please enter no of gurantors.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoofGur.Text = "";
                    txtNoofGur.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtGurValFrom.Text) > Convert.ToDecimal(txtGurValTo.Text))
                {
                    MessageBox.Show("To value range cannot exceed from value range.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValTo.Text = "";
                    txtGurValTo.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpGurFrom.Value).Date < Convert.ToDateTime(DateTime.Now).Date)
                {
                    MessageBox.Show("Valid date cannot back date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpGurFrom.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpGurFrom.Value).Date > Convert.ToDateTime(dtpGurTo.Value).Date)
                {
                    MessageBox.Show("Valid To date cannot less than from date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpGurTo.Focus();
                    return;
                }

                HPGurantorParam _tmpAddParam = new HPGurantorParam();
                string _chkOn = string.Empty;
                if (cmbGurCheckOn.Text == "Unit Price")
                {
                    _chkOn = "UP";
                }
                else if (cmbGurCheckOn.Text == "Amount Finance")
                {
                    _chkOn = "AF";
                }
                else if (cmbGurCheckOn.Text == "Hire Value")
                {
                    _chkOn = "HP";
                }
                else
                {
                    MessageBox.Show("Invalid type selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGurCheckOn.Focus();
                    return;
                }

                _tmpAddParam.Hpg_chk_on = _chkOn;
                _tmpAddParam.Hpg_cre_by = null;
                _tmpAddParam.Hpg_cre_dt = DateTime.Now.Date;
                _tmpAddParam.Hpg_from_dt = Convert.ToDateTime(dtpGurFrom.Value).Date;
                _tmpAddParam.Hpg_from_val = Convert.ToDecimal(txtGurValFrom.Text);
                _tmpAddParam.Hpg_no_of_gua = Convert.ToInt32(txtNoofGur.Text);
                _tmpAddParam.Hpg_pty_cd = null;
                _tmpAddParam.Hpg_pty_tp = null;
                _tmpAddParam.Hpg_sch_cd = null;
                _tmpAddParam.Hpg_seq = 0;
                _tmpAddParam.Hpg_to_dt = Convert.ToDateTime(dtpGurTo.Value).Date;
                _tmpAddParam.Hpg_to_val = Convert.ToDecimal(txtGurValTo.Text);
                _tempGur.Add(_tmpAddParam);

                dgvTempGurDef.AutoGenerateColumns = false;
                dgvTempGurDef.DataSource = new List<HPGurantorParam>();
                dgvTempGurDef.DataSource = _tempGur;


                txtGurValFrom.Text = (Convert.ToDecimal(txtGurValTo.Text) + 1).ToString();
                txtGurValTo.Text = "";
                txtNoofGur.Text = "";
                txtGurValTo.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearGur_Click(object sender, EventArgs e)
        {
            Clear_Gur_param();
        }

        private void txtNoofGur_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoofGur.Text))
            {
                if (!IsNumeric(txtNoofGur.Text))
                {
                    MessageBox.Show("Invalid value enter to no of gurantors.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoofGur.Text = "";
                    txtNoofGur.Focus();
                    return;
                }

                if (Convert.ToInt32(txtNoofGur.Text) < 0)
                {
                    MessageBox.Show("No of gurantors cannot be minus / zero value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoofGur.Text = "";
                    txtNoofGur.Focus();
                    return;
                }



                Check_Integer(txtNoofGur, "No of Gurantors");
            }
        }

        private void txtGurValFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGurValFrom.Text))
            {
                if (!IsNumeric(txtGurValFrom.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValFrom.Text = "";
                    txtGurValFrom.Focus();
                    return;
                }



                if (Convert.ToDecimal(txtGurValFrom.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValFrom.Text = "";
                    txtGurValFrom.Focus();
                    return;
                }


                txtGurValFrom.Text = Convert.ToDecimal(txtGurValFrom.Text).ToString("n");
            }
        }

        private void txtGurValTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGurValTo.Text))
            {
                if (!IsNumeric(txtGurValTo.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValTo.Text = "";
                    txtGurValTo.Focus();
                    return;
                }



                if (Convert.ToDecimal(txtGurValTo.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGurValTo.Text = "";
                    txtGurValTo.Focus();
                    return;
                }


                txtGurValTo.Text = Convert.ToDecimal(txtGurValTo.Text).ToString("n");
            }
        }

        private void txtCusPriceBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusPriceBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtCusPriceBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPriceBook.Clear();
                    txtCusPriceBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCusLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusLevel.Text)) return;
                if (string.IsNullOrEmpty(txtCusPriceBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCusLevel.Clear(); txtCusPriceBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtCusPriceBook.Text.Trim(), txtCusLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCusLevel.Clear(); txtCusLevel.Focus(); return; }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchGurChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppGurChannel;
                _CommonSearch.ShowDialog();
                txtAppGurChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppGurChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppGurChannel;
                _CommonSearch.ShowDialog();
                txtAppGurChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppGurChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtAppGurChannel;
                    _CommonSearch.ShowDialog();
                    txtAppGurChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtAppGurSubChannel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchGurSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAppGurChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAppGurSubChannel.Text = "";
                    txtAppGurChannel.Focus();
                    return;
                }

                _searchType = "Gur_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppGurSubChannel;
                _CommonSearch.ShowDialog();
                txtAppGurSubChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppGurSubChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAppGurChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAppGurSubChannel.Text = "";
                    txtAppGurChannel.Focus();
                    return;
                }

                _searchType = "Gur_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppGurSubChannel;
                _CommonSearch.ShowDialog();
                txtAppGurSubChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppGurSubChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtAppGurChannel.Text))
                    {
                        MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAppGurSubChannel.Text = "";
                        txtAppGurChannel.Focus();
                        return;
                    }

                    _searchType = "Gur_Para";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtAppGurSubChannel;
                    _CommonSearch.ShowDialog();
                    txtAppGurSubChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtAppGurPc.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchGurPC_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Gur_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppGurPc;
                _CommonSearch.ShowDialog();
                txtAppGurPc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppGurPc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Gur_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppGurPc;
                _CommonSearch.ShowDialog();
                txtAppGurPc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppGurPc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "Gur_Para";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtAppGurPc;
                    _CommonSearch.ShowDialog();
                    txtAppGurPc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnGurAppAdd.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAppGurSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstGurAppPc.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnAppGurUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstGurAppPc.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnAppGurClear_Click(object sender, EventArgs e)
        {
            lstGurAppPc.Clear();
            txtAppGurChannel.Text = "";
            txtAppGurSubChannel.Text = "";
            txtAppGurPc.Text = "";

        }

        private void cmbGurCheckOn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpGurFrom.Focus();
            }
        }

        private void dtpGurFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpGurTo.Focus();
            }
        }

        private void dtpGurTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGurValFrom.Focus();
            }
        }

        private void txtGurValFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGurValTo.Focus();
            }
        }

        private void txtGurValTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNoofGur.Focus();
            }
        }

        private void txtNoofGur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGurAdd.Focus();
            }
        }

        private void btnGurDeleteLast_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTempGurDef.Rows.Count <= 0)
                {
                    MessageBox.Show("No details are found to remove.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to remove last slab ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    List<HPGurantorParam> _temp = new List<HPGurantorParam>();
                    _temp = _tempGur;

                    int row_id = dgvTempGurDef.Rows.Count - 1;

                    string _chkOn = Convert.ToString(dgvTempGurDef.Rows[row_id].Cells["col_g_ChkOn"].Value);
                    decimal _valFrom = Convert.ToDecimal(dgvTempGurDef.Rows[row_id].Cells["col_G_frmVal"].Value);
                    decimal _valTo = Convert.ToDecimal(dgvTempGurDef.Rows[row_id].Cells["col_G_ValTo"].Value);
                    DateTime _fromDt = Convert.ToDateTime(dgvTempGurDef.Rows[row_id].Cells["col_G_ValidFrom"].Value);
                    DateTime _toDt = Convert.ToDateTime(dgvTempGurDef.Rows[row_id].Cells["col_G_ToDate"].Value);
                    Int32 _NoofGur = Convert.ToInt32(dgvTempGurDef.Rows[row_id].Cells["col_G_NoofGur"].Value);


                    _temp.RemoveAll(x => x.Hpg_chk_on == _chkOn && x.Hpg_from_val == _valFrom && x.Hpg_to_val == _valTo && x.Hpg_from_dt == _fromDt && x.Hpg_to_dt == _toDt && x.Hpg_no_of_gua == _NoofGur);
                    _tempGur = _temp;

                    dgvTempGurDef.AutoGenerateColumns = false;
                    dgvTempGurDef.DataSource = new List<HPGurantorParam>();
                    dgvTempGurDef.DataSource = _tempGur;

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGurClearGrid_Click(object sender, EventArgs e)
        {
            _tempGur = new List<HPGurantorParam>();
            dgvTempGurDef.AutoGenerateColumns = false;
            dgvTempGurDef.DataSource = new List<HPGurantorParam>();
            dgvTempGurDef.DataSource = _tempGur;
        }

        private void btnSearchGruSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGurSch;
                _CommonSearch.ShowDialog();
                txtGurSch.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGurSch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGurSch;
                _CommonSearch.ShowDialog();
                txtGurSch.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGurSch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtGurSch;
                    _CommonSearch.ShowDialog();
                    txtGurSch.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnGurSchAdd.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGurSch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGurSch.Text))
                {
                    lblGurType.Text = "";
                    lblGurTerm.Text = "";
                    lblGurIntRt.Text = "";

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtGurSch.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGurSch.Text = "";
                        txtGurSch.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGurSch.Text = "";
                        txtGurSch.Focus();
                        return;
                    }

                    lblGurType.Text = _tmpSch.Hsd_sch_tp;
                    lblGurTerm.Text = _tmpSch.Hsd_term.ToString("0");
                    lblGurIntRt.Text = _tmpSch.Hsd_intr_rt.ToString("n");

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




        private void btnGurSchClear_Click(object sender, EventArgs e)
        {
            lstGurSch.Clear();
            lblGurType.Text = "";
            lblGurTerm.Text = "";
            lblGurIntRt.Text = "";
            txtGurSch.Text = "";
            txtGurSch.Focus();

        }

        private void btnGurSchSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstGurSch.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnGurUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstGurSch.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnGurSchAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGurSch.Text))
            {
                MessageBox.Show("Please select applicable scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGurSch.Text = "";
                txtGurSch.Focus();
                return;
            }

            lstGurSch.Items.Add(txtGurSch.Text.Trim());
            lblGurType.Text = "";
            lblGurTerm.Text = "";
            lblGurIntRt.Text = "";
            txtGurSch.Text = "";
            txtGurSch.Focus();


        }

        private void btnGurFinalApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTempGurDef.Rows.Count == 0)
                {
                    MessageBox.Show("Please setup applicable parameters to add.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(cmbGurApp.Text))
                {
                    MessageBox.Show("Please select applicable profit center define type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGurApp.Focus();
                    return;
                }

                if (cmbGurApp.Text == "Profit Center")
                {
                    Boolean _isPCFound = false;
                    foreach (ListViewItem Item in lstGurAppPc.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isPCFound = true;
                            goto L1;
                        }
                    }
                L1:

                    if (_isPCFound == false)
                    {
                        MessageBox.Show("No any applicable profit center is selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                Boolean _isSchFound = false;
                foreach (ListViewItem Item in lstGurSch.Items)
                {
                    string sch = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isSchFound = true;
                        goto L2;
                    }
                }
            L2:

                if (_isSchFound == false)
                {
                    MessageBox.Show("No any applicable scheme(s) are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                HPGurantorParam _tempGurParam = new HPGurantorParam();
                if (cmbGurApp.Text == "Profit Center")
                {
                    foreach (ListViewItem pcList in lstGurAppPc.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstGurSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (DataGridViewRow row in dgvTempGurDef.Rows)
                                    {
                                        _tempGurParam = new HPGurantorParam();

                                        _tempGurParam.Hpg_chk_on = row.Cells["col_g_ChkOn"].Value.ToString();
                                        _tempGurParam.Hpg_cre_by = BaseCls.GlbUserID;
                                        _tempGurParam.Hpg_cre_dt = DateTime.Now.Date;
                                        _tempGurParam.Hpg_from_dt = Convert.ToDateTime(row.Cells["col_G_ValidFrom"].Value).Date;
                                        _tempGurParam.Hpg_from_val = Convert.ToDecimal(row.Cells["col_G_frmVal"].Value);
                                        _tempGurParam.Hpg_no_of_gua = Convert.ToInt32(row.Cells["col_G_NoofGur"].Value);
                                        _tempGurParam.Hpg_pty_cd = pc;
                                        _tempGurParam.Hpg_pty_tp = "PC";
                                        _tempGurParam.Hpg_sch_cd = sch;
                                        _tempGurParam.Hpg_seq = 0;
                                        _tempGurParam.Hpg_to_dt = Convert.ToDateTime(row.Cells["col_G_ToDate"].Value).Date;
                                        _tempGurParam.Hpg_to_val = Convert.ToDecimal(row.Cells["col_G_ValTo"].Value);
                                        _finalGurParam.Add(_tempGurParam);


                                    }
                                }
                            }
                        }
                    }
                    dgvFinalGurParam.AutoGenerateColumns = false;
                    dgvFinalGurParam.DataSource = new List<HPGurantorParam>();
                    dgvFinalGurParam.DataSource = _finalGurParam;
                }
                else
                {
                    foreach (ListViewItem schList in lstGurSch.Items)
                    {
                        string sch = schList.Text;

                        if (schList.Checked == true)
                        {
                            foreach (DataGridViewRow row in dgvTempGurDef.Rows)
                            {
                                _tempGurParam = new HPGurantorParam();

                                _tempGurParam.Hpg_chk_on = row.Cells["col_g_ChkOn"].Value.ToString();
                                _tempGurParam.Hpg_cre_by = BaseCls.GlbUserID;
                                _tempGurParam.Hpg_cre_dt = DateTime.Now.Date;
                                _tempGurParam.Hpg_from_dt = Convert.ToDateTime(row.Cells["col_G_ValidFrom"].Value).Date;
                                _tempGurParam.Hpg_from_val = Convert.ToDecimal(row.Cells["col_G_frmVal"].Value);
                                _tempGurParam.Hpg_no_of_gua = Convert.ToInt32(row.Cells["col_G_NoofGur"].Value);
                                if (cmbGurApp.Text == "Group")
                                {
                                    _tempGurParam.Hpg_pty_cd = "GRUP01";
                                    _tempGurParam.Hpg_pty_tp = "GPC";
                                }
                                else if (cmbGurApp.Text == "Company")
                                {
                                    _tempGurParam.Hpg_pty_cd = BaseCls.GlbUserComCode;
                                    _tempGurParam.Hpg_pty_tp = "COM";
                                }
                                _tempGurParam.Hpg_sch_cd = sch;
                                _tempGurParam.Hpg_seq = 0;
                                _tempGurParam.Hpg_to_dt = Convert.ToDateTime(row.Cells["col_G_ToDate"].Value).Date;
                                _tempGurParam.Hpg_to_val = Convert.ToDecimal(row.Cells["col_G_ValTo"].Value);
                                _finalGurParam.Add(_tempGurParam);


                            }
                        }
                    }
                    dgvFinalGurParam.AutoGenerateColumns = false;
                    dgvFinalGurParam.DataSource = new List<HPGurantorParam>();
                    dgvFinalGurParam.DataSource = _finalGurParam;
                }

                txtGurValFrom.Text = "";
                txtGurValTo.Text = "";
                txtNoofGur.Text = "";
                _tempGur = new List<HPGurantorParam>();
                dgvTempGurDef.AutoGenerateColumns = false;
                dgvTempGurDef.DataSource = new List<HPGurantorParam>();
                dgvTempGurDef.DataSource = _tempGur;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveGur_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (dgvFinalGurParam.Rows.Count == 0)
                {
                    MessageBox.Show("No any details find to process.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                row_aff = (Int32)CHNLSVC.Sales.SaveHpGurantorsParam(_finalGurParam);

                if (row_aff == 1)
                {

                    MessageBox.Show("Gurantor parameters added successfully.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGurAppAdd_Click(object sender, EventArgs e)
        {

            try
            {
                //lstPC.Clear();
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtAppGurChannel.Text, txtAppGurSubChannel.Text, null, null, null, txtAppGurPc.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstGurAppPc.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                txtAppGurChannel.Text = "";
                txtAppGurSubChannel.Text = "";
                txtAppGurPc.Text = "";
                txtAppGurPc.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearOthCha_Click(object sender, EventArgs e)
        {
            Clear_Oth_Charges();
        }

        private void btnSearchOthBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthBook;
                _CommonSearch.ShowDialog();
                txtOthBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthBook_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthBook;
                _CommonSearch.ShowDialog();
                txtOthBook.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthBook_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthBook;
                    _CommonSearch.ShowDialog();
                    txtOthBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtOthLevel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOthLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthBook.Clear();
                    txtOthBook.Focus();
                    return;
                }

                _searchType = "Other_Charge";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthLevel;
                _CommonSearch.ShowDialog();
                txtOthLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthBook.Clear();
                    txtOthBook.Focus();
                    return;
                }

                _searchType = "Other_Charge";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthLevel;
                _CommonSearch.ShowDialog();
                txtOthLevel.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtOthBook.Text))
                    {
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthBook.Clear();
                        txtOthBook.Focus();
                        return;
                    }
                    _searchType = "Other_Charge";

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthLevel;
                    _CommonSearch.ShowDialog();
                    txtOthLevel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddOthPBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbChargeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOthBook.Focus();
            }
        }

        private void txtOthBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtOthBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthBook.Clear();
                    txtOthBook.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthLevel.Text)) return;
                if (string.IsNullOrEmpty(txtOthBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtOthLevel.Clear(); txtOthBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtOthBook.Text.Trim(), txtOthLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtOthLevel.Clear(); txtOthLevel.Focus(); return; }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOthMainCate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthMainCate;
                _CommonSearch.ShowDialog();
                txtOthMainCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthMainCate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthMainCate;
                _CommonSearch.ShowDialog();
                txtOthMainCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthMainCate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthMainCate;
                    _CommonSearch.ShowDialog();
                    txtOthMainCate.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtOthSubCate.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOthSubCate_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Other_Charge";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthSubCate;
                _CommonSearch.ShowDialog();
                txtOthSubCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthSubCate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Other_Charge";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthSubCate;
                _CommonSearch.ShowDialog();
                txtOthSubCate.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthSubCate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "Other_Charge";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthSubCate;
                    _CommonSearch.ShowDialog();
                    txtOthSubCate.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtOthRange.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthMainCate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthMainCate.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtOthMainCate.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthMainCate.Clear();
                    txtOthMainCate.Focus();
                    return;
                }
                txtOthSubCate.Clear();
                txtOthRange.Clear();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthSubCate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthSubCate.Text)) return;
                //if (string.IsNullOrEmpty(txtMainCate.Text)) { MessageBox.Show("Please select the main category first", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txtSubCate.Clear(); txtMainCate.Focus(); return; }

                if (!string.IsNullOrEmpty(txtOthMainCate.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtOthSubCate.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Please select the valid category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthSubCate.Clear();
                        txtOthSubCate.Focus();
                        return;
                    }
                }
                else
                {
                    MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(txtOthSubCate.Text);
                    if (subCate.Ric2_cd == null)
                    {
                        MessageBox.Show("Please select the valid category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthSubCate.Clear();
                        txtOthSubCate.Focus();
                        return;
                    }
                }
                txtOthRange.Clear();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOthRange_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Other_Charge";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthRange;
                _CommonSearch.ShowDialog();
                txtOthRange.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthRange_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Other_Charge";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthRange;
                _CommonSearch.ShowDialog();
                txtOthRange.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthRange_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthRange;
                    _CommonSearch.ShowDialog();
                    txtOthRange.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtOthBrand.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthRange_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthRange.Text)) return;
                if (string.IsNullOrEmpty(txtOthRange.Text)) { MessageBox.Show("Please select the main category first", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information); txtOthRange.Clear(); txtOthRange.Focus(); return; }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtOthRange.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid item range.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthRange.Clear();
                    txtOthRange.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOthBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthBrand;
                _CommonSearch.ShowDialog();
                txtOthBrand.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthBrand_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthBrand;
                _CommonSearch.ShowDialog();
                txtOthBrand.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthBrand;
                    _CommonSearch.ShowDialog();
                    txtOthBrand.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtOthItm.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthBrand_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOthBrand.Text))
                {
                    MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(txtOthBrand.Text.Trim());

                    if (_brd.Mb_cd == null)
                    {
                        MessageBox.Show("Please select the valid brand.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthBrand.Text = "";
                        txtOthBrand.Focus();
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
        }

        private void btnSearchOthItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthItm;
                _CommonSearch.ShowDialog();
                txtOthItm.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthItm_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthItm;
                _CommonSearch.ShowDialog();
                txtOthItm.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthItm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthItm;
                    _CommonSearch.ShowDialog();
                    txtOthItm.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddOthItems.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthItm_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOthItm.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtOthItm.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthItm.Clear();
                        txtOthItm.Focus();

                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddOthItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOthMainCate.Text) && string.IsNullOrEmpty(txtOthSubCate.Text) && string.IsNullOrEmpty(txtOthRange.Text) && string.IsNullOrEmpty(txtOthBrand.Text) && string.IsNullOrEmpty(txtOthItm.Text))
                {
                    MessageBox.Show("Please enter searching parameters.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthMainCate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtOthMainCate.Text) && (!string.IsNullOrEmpty(txtOthSubCate.Text)))
                {
                    MessageBox.Show("Cannot search by sub category only.Please select main category as well.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthSubCate.Text = "";
                    txtOthMainCate.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtOthRange.Text) && (string.IsNullOrEmpty(txtOthSubCate.Text)))
                {
                    MessageBox.Show("Cannot search by item range without selecting sub category.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthRange.Text = "";
                    txtOthSubCate.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtOthItm.Text))
                {
                    lstOthItem.Items.Add(txtOthItm.Text.Trim());
                }
                else
                {

                    lstOthItem.Clear();
                    List<MasterItem> _tmpItem = CHNLSVC.Sales.GetItemsByCateAndBrand(txtOthMainCate.Text, txtOthSubCate.Text, txtOthRange.Text, txtOthBrand.Text, BaseCls.GlbUserComCode);
                    foreach (MasterItem _temp in _tmpItem)
                    {
                        lstOthItem.Items.Add(_temp.Mi_cd);
                    }

                }

                txtOthMainCate.Text = "";
                txtOthSubCate.Text = "";
                txtOthRange.Text = "";
                txtOthBrand.Text = "";
                txtOthItm.Text = "";
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthItemSelect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstOthItem.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthItemUnselect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstOthItem.Items)
                {
                    Item.Checked = false;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthItemClear_Click(object sender, EventArgs e)
        {
            txtOthMainCate.Text = "";
            txtOthSubCate.Text = "";
            txtOthRange.Text = "";
            txtOthBrand.Text = "";
            txtOthItm.Text = "";
            lstOthItem.Clear();
        }

        private void btnSearchOthScheme_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthSchCode;
                _CommonSearch.ShowDialog();
                txtOthSchCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthSchCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthSchCode;
                _CommonSearch.ShowDialog();
                txtOthSchCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthSchCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOthSchCode;
                    _CommonSearch.ShowDialog();
                    txtOthSchCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddOthSch.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtOthSchCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOthSchCode.Text))
                {
                    lblOthSchType.Text = "";
                    lblOthSchTerm.Text = "";
                    //lblOthIntRate.Text = "";

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtOthSchCode.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthSchCode.Text = "";
                        txtOthSchCode.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthSchCode.Text = "";
                        txtOthSchCode.Focus();
                        return;
                    }

                    lblOthSchType.Text = _tmpSch.Hsd_sch_tp;
                    lblOthSchTerm.Text = _tmpSch.Hsd_term.ToString("0");
                    // lblOthIntRate.Text = _tmpSch.Hsd_intr_rt.ToString("n");

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddOthSch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOthSchCode.Text))
            {
                MessageBox.Show("Please applicable select scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOthSchCode.Text = "";
                txtOthSchCode.Focus();
                return;
            }

            lstOthSch.Items.Add(txtOthSchCode.Text.Trim());
            lblOthSchType.Text = "";
            lblOthSchTerm.Text = "";
            //lblOthIntRate.Text = "";
            txtOthSchCode.Text = "";
            txtOthSchCode.Focus();
        }

        private void btnOthSchSelect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstOthSch.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthSchUnselect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstOthSch.Items)
                {
                    Item.Checked = false;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthSchClear_Click(object sender, EventArgs e)
        {
            lstOthSch.Clear();
            txtOthSchCode.Text = "";
            lblOthSchType.Text = "";
            lblOthSchTerm.Text = "";
            //lblOthIntRate.Text = "";
            txtOthSchCode.Focus();
        }

        private void dtpOthFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpOthTo.Focus();
            }
        }

        private void dtpOthTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOthMainCate.Focus();
            }
        }

        private void btnAddFinalOthDef_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOthAppPBook.Rows.Count == 0)
                {
                    MessageBox.Show("Please select applicable price books / levels.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbChargeType.Text))
                {
                    MessageBox.Show("Please select applicable charge type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbChargeType.Text = "";
                    cmbChargeType.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpOthFrom.Value).Date < Convert.ToDateTime(DateTime.Now).Date)
                {
                    MessageBox.Show("Valid date cannot back date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpOthFrom.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpOthFrom.Value).Date > Convert.ToDateTime(dtpOthTo.Value).Date)
                {
                    MessageBox.Show("Valid To date cannot less than from date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpOthTo.Focus();
                    return;
                }

                Boolean _isFoundItem = false;
                foreach (ListViewItem Item in lstOthItem.Items)
                {
                    string _item = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isFoundItem = true;
                        goto L1;
                    }
                }
            L1:

                if (_isFoundItem == false)
                {
                    MessageBox.Show("No any applicable item is selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Boolean _isSchFound = false;
                foreach (ListViewItem Item in lstOthSch.Items)
                {
                    string sch = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isSchFound = true;
                        goto L2;
                    }
                }
            L2:

                if (_isSchFound == false)
                {
                    MessageBox.Show("No any applicable scheme(s) are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (Convert.ToDateTime(dtpOthFrom.Value).Date < Convert.ToDateTime(DateTime.Now).Date)
                {
                    MessageBox.Show("Valid date cannot back date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpOthFrom.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpOthFrom.Value).Date > Convert.ToDateTime(dtpOthTo.Value).Date)
                {
                    MessageBox.Show("Valid To date cannot less than from date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpOthTo.Focus();
                    return;
                }

                HpOtherCharges _tmpOth = new HpOtherCharges();

                foreach (ListViewItem schList in lstOthSch.Items)
                {
                    string sch = schList.Text;

                    if (schList.Checked == true)
                    {
                        foreach (ListViewItem _itmList in lstOthItem.Items)
                        {
                            string _item = _itmList.Text;

                            if (_itmList.Checked == true)
                            {
                                foreach (DataGridViewRow row in dgvOthAppPBook.Rows)
                                {
                                    _tmpOth = new HpOtherCharges();
                                    _tmpOth.Hoc_brd = null;
                                    _tmpOth.Hoc_cat = null;
                                    _tmpOth.Hoc_comm_cat = null;
                                    _tmpOth.Hoc_cre_by = BaseCls.GlbUserID;
                                    _tmpOth.Hoc_cre_dt = DateTime.Now.Date;
                                    _tmpOth.Hoc_cus_cd = null;
                                    _tmpOth.Hoc_desc = "STAMP DUTY";
                                    _tmpOth.Hoc_from_dt = Convert.ToDateTime(dtpOthFrom.Value).Date;
                                    _tmpOth.Hoc_itm = _item;
                                    _tmpOth.Hoc_main_cat = null;
                                    _tmpOth.Hoc_pb = row.Cells["col_Oth_Book"].Value.ToString();
                                    _tmpOth.Hoc_pb_lvl = row.Cells["col_Oth_Lvl"].Value.ToString();
                                    _tmpOth.Hoc_pro = null;
                                    _tmpOth.Hoc_sch_cd = sch;
                                    _tmpOth.Hoc_seq = 0;
                                    _tmpOth.Hoc_ser = null;
                                    _tmpOth.Hoc_to_dt = Convert.ToDateTime(dtpOthTo.Value).Date;
                                    _tmpOth.Hoc_tp = "STM";
                                    // _tmpOth.Hoc_val = 0;
                                    _othChar.Add(_tmpOth);
                                }
                            }
                        }
                    }
                }
                dgvFinalOthDetails.AutoGenerateColumns = false;
                dgvFinalOthDetails.DataSource = new List<HpOtherCharges>();
                dgvFinalOthDetails.DataSource = _othChar;

                txtOthBook.Text = "";
                txtOthLevel.Text = "";
                dtpOthFrom.Value = DateTime.Now.Date;
                dtpOthTo.Value = DateTime.Now.Date;
                lstOthItem.Clear();
                lstOthSch.Clear();
                dgvOthAppPBook.AutoGenerateColumns = false;
                dgvOthAppPBook.DataSource = new List<PriceBookLevelRef>();
                _othPriceBook = new List<PriceBookLevelRef>();
                txtOthBook.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveOthCha_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (dgvFinalOthDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Cannot find charges details.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                row_aff = (Int32)CHNLSVC.Sales.SaveHpOtherChargeDef(_othChar);

                if (row_aff == 1)
                {

                    MessageBox.Show("Other charges parameters added successfully.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddOthPBook_Click(object sender, EventArgs e)
        {
            try
            {
                //_othPriceBook = new List<PriceBookLevelRef>();

                if (string.IsNullOrEmpty(txtOthBook.Text))
                {
                    MessageBox.Show("Please select applicable price book.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtOthLevel.Text))
                {
                    MessageBox.Show("Please select applicable price level.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthLevel.Focus();
                    return;
                }

                PriceBookLevelRef _tmpOthBook = new PriceBookLevelRef();
                _tmpOthBook.Sapl_pb = txtOthBook.Text;
                _tmpOthBook.Sapl_pb_lvl_cd = txtOthLevel.Text;
                _othPriceBook.Add(_tmpOthBook);

                dgvOthAppPBook.AutoGenerateColumns = false;
                dgvOthAppPBook.DataSource = new List<PriceBookLevelRef>();
                dgvOthAppPBook.DataSource = _othPriceBook;

                txtOthBook.Text = "";
                txtOthLevel.Text = "";
                txtOthBook.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthBookDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOthAppPBook.Rows.Count <= 0)
                {
                    MessageBox.Show("No details are found to remove.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Do you want to remove last row ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    List<PriceBookLevelRef> _temp = new List<PriceBookLevelRef>();
                    _temp = _othPriceBook;

                    int row_id = dgvOthAppPBook.Rows.Count - 1;

                    string _book = Convert.ToString(dgvOthAppPBook.Rows[row_id].Cells["col_Oth_Book"].Value);
                    string _level = Convert.ToString(dgvOthAppPBook.Rows[row_id].Cells["col_Oth_Lvl"].Value);

                    _temp.RemoveAll(x => x.Sapl_pb == _book && x.Sapl_pb_lvl_cd == _level);
                    _othPriceBook = _temp;

                    dgvOthAppPBook.AutoGenerateColumns = false;
                    dgvOthAppPBook.DataSource = new List<PriceBookLevelRef>();
                    dgvOthAppPBook.DataSource = _othPriceBook;

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOthBookClear_Click(object sender, EventArgs e)
        {
            _othPriceBook = new List<PriceBookLevelRef>();
            dgvOthAppPBook.AutoGenerateColumns = false;
            dgvOthAppPBook.DataSource = new List<PriceBookLevelRef>();
            txtOthBook.Text = "";
            txtOthLevel.Text = "";
            txtOthBook.Focus();
        }

        private void dgvResheSch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgvResheSch.Rows[dgvResheSch.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
                case "True":
                    {
                        ch1.Value = false;
                        break;
                    }
            }
        }

        private void btnReSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResheSch.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == false)
                {
                    chk.Value = true;
                }

            }
        }

        private void btnReUnselect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResheSch.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }

            }
        }

        private void btnSearchReSheSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSelectReScheme;
                _CommonSearch.ShowDialog();
                txtSelectReScheme.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSelectReScheme_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSelectReScheme;
                    _CommonSearch.ShowDialog();
                    txtSelectReScheme.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnSelectEnterSch.Focus();
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSelectReScheme_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
            DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSelectReScheme;
            _CommonSearch.ShowDialog();
            txtSelectReScheme.Select();
        }

        private void txtSelectReScheme_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSelectReScheme.Text))
                {

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtSelectReScheme.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSelectReScheme.Text = "";
                        txtSelectReScheme.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSelectReScheme.Text = "";
                        txtSelectReScheme.Focus();
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
        }

        private void btnSelectEnterSch_Click(object sender, EventArgs e)
        {
            Boolean _isFound = false;
            foreach (DataGridViewRow row in dgvResheSch.Rows)
            {
                string _curSch = row.Cells["col_R_othSch"].Value.ToString();
                if (_curSch == txtSelectReScheme.Text)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == false)
                    {
                        chk.Value = true;
                        _isFound = true;
                        goto L1;
                    }
                }

            }
        L1:

            if (_isFound == false)
            {
                MessageBox.Show("Cannot found selected scheme in above list.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSelectReScheme.Text = "";
                txtSelectReScheme.Focus();
                return;
            }
            txtSelectReScheme.Text = "";
            txtSelectReScheme.Focus();

        }

        private void txtSerChgFromVal_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerChgFromVal.Text))
            {
                if (!IsNumeric(txtSerChgFromVal.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgFromVal.Text = "";
                    txtSerChgFromVal.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSerChgFromVal.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgFromVal.Text = "";
                    txtSerChgFromVal.Focus();
                    return;
                }


                txtSerChgFromVal.Text = Convert.ToDecimal(txtSerChgFromVal.Text).ToString("n");
            }
        }

        private void txtSerChgToVal_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerChgToVal.Text))
            {
                if (!IsNumeric(txtSerChgToVal.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgToVal.Text = "";
                    txtSerChgToVal.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSerChgToVal.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgToVal.Text = "";
                    txtSerChgToVal.Focus();
                    return;
                }


                txtSerChgToVal.Text = Convert.ToDecimal(txtSerChgToVal.Text).ToString("n");
            }
        }

        private void txtSerChgFromVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerChgToVal.Focus();
            }
        }

        private void txtSerChgToVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSerChgCheck.Focus();
            }
        }

        private void cmbSerChgCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSerChgCal.Focus();
            }
        }

        private void cmbSerChgCal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerChgAmt.Focus();
            }
        }

        private void txtSerChgAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerChgRate.Focus();
            }
        }

        private void txtSerChgRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMgrCommAmt.Focus();
            }
        }

        private void btnAddSerChg_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerChgFromVal.Text))
                {
                    MessageBox.Show("Please enter from value range.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgFromVal.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSerChgToVal.Text))
                {
                    MessageBox.Show("Please enter to value range.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgToVal.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbSerChgCheck.Text))
                {
                    MessageBox.Show("Please select service charge checking.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSerChgCheck.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbSerChgCal.Text))
                {
                    MessageBox.Show("Please select service charge calculating.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSerChgCal.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSerChgAmt.Text))
                {
                    MessageBox.Show("Please enter service charge amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgAmt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSerChgRate.Text))
                {
                    MessageBox.Show("Please enter service charge rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgRate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMgrCommAmt.Text))
                {
                    MessageBox.Show("Please enter service commission amount.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommAmt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMgrCommRate.Text))
                {
                    MessageBox.Show("Please enter service commission rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommRate.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSerChgFromVal.Text) > Convert.ToDecimal(txtSerChgToVal.Text))
                {
                    MessageBox.Show("To value range cannot exceed from value range.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgToVal.Text = "";
                    txtSerChgToVal.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpSerValidFrom.Value).Date < Convert.ToDateTime(DateTime.Now).Date)
                {
                    MessageBox.Show("Valid date cannot back date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpSerValidFrom.Focus();
                    return;
                }

                if (Convert.ToDateTime(dtpSerValidFrom.Value).Date > Convert.ToDateTime(dtpSerValidTo.Value).Date)
                {
                    MessageBox.Show("Valid To date cannot less than from date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpSerValidTo.Focus();
                    return;
                }

                HpServiceCharges _addSerList = new HpServiceCharges();
                Boolean _isChkonAF = false;
                Boolean _isCalonAF = false;

                if (cmbSerChgCheck.Text == "Amount Finance")
                {
                    _isChkonAF = true;
                }
                else
                {
                    _isChkonAF = false;
                }

                if (cmbSerChgCal.Text == "Amount Finance")
                {
                    _isCalonAF = true;
                }
                else
                {
                    _isCalonAF = false;
                }

                _addSerList.Hps_cal_on = _isCalonAF;
                _addSerList.Hps_chg = Convert.ToDecimal(txtSerChgAmt.Text);
                _addSerList.Hps_chk_on = _isChkonAF;
                _addSerList.Hps_cre_by = BaseCls.GlbUserID;
                _addSerList.Hps_cre_dt = DateTime.Now.Date;
                _addSerList.Hps_from_val = Convert.ToDecimal(txtSerChgFromVal.Text);
                _addSerList.Hps_pty_cd = null;
                _addSerList.Hps_pty_tp = null;
                _addSerList.Hps_rt = Convert.ToDecimal(txtSerChgRate.Text);
                _addSerList.Hps_sch_cd = null;
                _addSerList.Hps_seq = 0;
                _addSerList.Hps_to_val = Convert.ToDecimal(txtSerChgToVal.Text);
                _addSerList.Hps_valid_from = Convert.ToDateTime(dtpSerValidFrom.Value).Date;
                _addSerList.Hps_valid_to = Convert.ToDateTime(dtpSerValidTo.Value).Date;
                _addSerList.Hps_mgr_comm_amt = Convert.ToDecimal(txtMgrCommAmt.Text);
                _addSerList.Hps_mgr_comm_rt = Convert.ToDecimal(txtMgrCommRate.Text);
                _tmpSerChgList.Add(_addSerList);

                dgvSerChgValDef.AutoGenerateColumns = false;
                dgvSerChgValDef.DataSource = new List<HpServiceCharges>();
                dgvSerChgValDef.DataSource = _tmpSerChgList;

                txtSerChgFromVal.Text = (Convert.ToDecimal(txtSerChgToVal.Text) + 1).ToString("n");
                txtSerChgToVal.Text = "";
                txtSerChgAmt.Text = "";
                txtSerChgRate.Text = "";
                txtSerChgFromVal.Focus();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerChgAmt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerChgAmt.Text))
            {
                if (!IsNumeric(txtSerChgAmt.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgAmt.Text = "";
                    txtSerChgAmt.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSerChgAmt.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgAmt.Text = "";
                    txtSerChgAmt.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtSerChgAmt.Text) > 0)
                {
                    txtSerChgRate.Text = "0";
                }

                txtSerChgAmt.Text = Convert.ToDecimal(txtSerChgAmt.Text).ToString("n");
            }
        }

        private void txtSerChgRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSerChgRate.Text))
            {
                if (!IsNumeric(txtSerChgRate.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgRate.Text = "";
                    txtSerChgRate.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSerChgRate.Text) < 0)
                {
                    MessageBox.Show("Rate should be within the range of 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgRate.Text = "";
                    txtSerChgRate.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtSerChgRate.Text) > 100)
                {
                    MessageBox.Show("Rate should be within the range of 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerChgRate.Text = "";
                    txtSerChgRate.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtSerChgRate.Text) > 0)
                {
                    txtSerChgAmt.Text = "0";
                }

                txtSerChgRate.Text = Convert.ToDecimal(txtSerChgRate.Text).ToString("n");
            }
        }

        private void btnSerChgClear_Click(object sender, EventArgs e)
        {
            Clear_Service_Chg();
        }

        private void cmbSerAppDefBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSerAppDefBy.Text == "Profit Center")
            {
                txtSerAppChannel.Text = "";
                txtSerAppPC.Text = "";
                txtSerAppSubChannel.Text = "";
                txtSerAppChannel.Enabled = true;
                txtSerAppPC.Enabled = true;
                txtSerAppSubChannel.Enabled = true;
                btnSearchSerAppChannal.Enabled = true;
                btnSearchSerAppSubchannel.Enabled = true;
                btnSearchSerAppPC.Enabled = true;
                lstSerChgAppPC.Clear();
                btnAddSerAppPC.Enabled = true;
                txtSerAppChannel.Focus();

            }
            else
            {
                txtSerAppChannel.Text = "";
                txtSerAppPC.Text = "";
                txtSerAppSubChannel.Text = "";
                txtSerAppChannel.Enabled = false;
                txtSerAppPC.Enabled = false;
                txtSerAppSubChannel.Enabled = false;
                btnSearchSerAppChannal.Enabled = false;
                btnSearchSerAppSubchannel.Enabled = false;
                btnSearchSerAppPC.Enabled = false;
                lstSerChgAppPC.Clear();
                btnAddSerAppPC.Enabled = false;

            }
        }

        private void btnSearchSerAppChannal_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerAppChannel;
                _CommonSearch.ShowDialog();
                txtSerAppChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerAppChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerAppChannel;
                _CommonSearch.ShowDialog();
                txtSerAppChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerAppChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerAppChannel;
                    _CommonSearch.ShowDialog();
                    txtSerAppChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSerAppSubChannel.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSerAppSubchannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerAppChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerAppChannel.Text = "";
                    txtSerAppChannel.Focus();
                    return;
                }

                _searchType = "Ser_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerAppSubChannel;
                _CommonSearch.ShowDialog();
                txtSerAppSubChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerAppSubChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSerAppChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerAppChannel.Text = "";
                    txtSerAppChannel.Focus();
                    return;
                }

                _searchType = "Ser_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerAppSubChannel;
                _CommonSearch.ShowDialog();
                txtSerAppSubChannel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerAppSubChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtSerAppChannel.Text))
                    {
                        MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerAppChannel.Text = "";
                        txtSerAppChannel.Focus();
                        return;
                    }

                    _searchType = "Ser_Para";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerAppSubChannel;
                    _CommonSearch.ShowDialog();
                    txtSerAppSubChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSerAppPC.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSerAppPC_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Ser_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerAppPC;
                _CommonSearch.ShowDialog();
                txtSerAppPC.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerAppPC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "Ser_Para";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerAppPC;
                _CommonSearch.ShowDialog();
                txtSerAppPC.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerAppPC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "Ser_Para";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerAppPC;
                    _CommonSearch.ShowDialog();
                    txtSerAppPC.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddSerAppPC.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddSerAppPC_Click(object sender, EventArgs e)
        {

            try
            {
                //lstPC.Clear();
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtSerAppChannel.Text, txtSerAppSubChannel.Text, null, null, null, txtSerAppPC.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstSerChgAppPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                txtSerAppChannel.Text = "";
                txtSerAppSubChannel.Text = "";
                txtSerAppPC.Text = "";
                txtSerAppPC.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSerSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSerChgAppPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnSerUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSerChgAppPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnSerAppClear_Click(object sender, EventArgs e)
        {
            lstSerChgAppPC.Clear();
            txtSerAppChannel.Text = "";
            txtSerAppSubChannel.Text = "";
            txtSerAppPC.Text = "";
        }

        private void btnSearchAppSch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppSerScheme;
                _CommonSearch.ShowDialog();
                txtAppSerScheme.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppSerScheme_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtAppSerScheme;
                    _CommonSearch.ShowDialog();
                    txtAppSerScheme.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddAppSerScheme.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppSerScheme_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAppSerScheme;
                _CommonSearch.ShowDialog();
                txtAppSerScheme.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAppSerScheme_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAppSerScheme.Text))
                {
                    lblAppSerType.Text = "";
                    lblAppSerTerm.Text = "";
                    lblAppSerIntRate.Text = "";

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtAppSerScheme.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAppSerScheme.Text = "";
                        txtAppSerScheme.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAppSerScheme.Text = "";
                        txtAppSerScheme.Focus();
                        return;
                    }

                    lblAppSerType.Text = _tmpSch.Hsd_sch_tp;
                    lblAppSerTerm.Text = _tmpSch.Hsd_term.ToString("0");
                    lblAppSerIntRate.Text = _tmpSch.Hsd_intr_rt.ToString("n");

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAppSerScheme_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAppSerScheme.Text))
            {
                MessageBox.Show("Please select applicable scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAppSerScheme.Text = "";
                txtAppSerScheme.Focus();
                return;
            }

            lstSerAppSch.Items.Add(txtAppSerScheme.Text.Trim());
            lblAppSerType.Text = "";
            lblAppSerTerm.Text = "";
            lblAppSerIntRate.Text = "";
            txtAppSerScheme.Text = "";
            txtAppSerScheme.Focus();
        }

        private void btnAppSerSchSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSerAppSch.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnAppSerSchUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSerAppSch.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnAppSerSchClear_Click(object sender, EventArgs e)
        {
            txtAppSerScheme.Text = "";
            lblAppSerTerm.Text = "";
            lblAppSerType.Text = "";
            lblAppSerIntRate.Text = "";
            lstSerAppSch.Clear();
            txtAppSerScheme.Focus();
        }

        private void btnSerChgFinalApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSerChgValDef.Rows.Count == 0)
                {
                    MessageBox.Show("Value definition is not set.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Boolean _isSchFound = false;
                foreach (ListViewItem Item in lstSerAppSch.Items)
                {
                    string sch = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isSchFound = true;
                        goto L2;
                    }
                }
            L2:

                if (_isSchFound == false)
                {
                    MessageBox.Show("No any applicable scheme(s) are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cmbSerAppDefBy.Text == "Profit Center")
                {
                    Boolean _isPCFound = false;
                    foreach (ListViewItem Item in lstSerChgAppPC.Items)
                    {
                        string pc = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isPCFound = true;
                            goto L1;
                        }
                    }
                L1:

                    if (_isPCFound == false)
                    {
                        MessageBox.Show("No any applicable profit center is selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                HpServiceCharges _tmpSerChg = new HpServiceCharges();
                if (cmbSerAppDefBy.Text == "Profit Center")
                {
                    foreach (ListViewItem pcList in lstSerChgAppPC.Items)
                    {
                        string pc = pcList.Text;

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem schList in lstSerAppSch.Items)
                            {
                                string sch = schList.Text;

                                if (schList.Checked == true)
                                {
                                    foreach (DataGridViewRow row in dgvSerChgValDef.Rows)
                                    {
                                        _tmpSerChg = new HpServiceCharges();

                                        _tmpSerChg.Hps_cal_on = Convert.ToBoolean(row.Cells["col_S_CalOn"].Value);
                                        _tmpSerChg.Hps_chg = Convert.ToDecimal(row.Cells["col_S_ChgAmt"].Value);
                                        _tmpSerChg.Hps_chk_on = Convert.ToBoolean(row.Cells["col_S_ChckOn"].Value);
                                        _tmpSerChg.Hps_cre_by = BaseCls.GlbUserID;
                                        _tmpSerChg.Hps_cre_dt = DateTime.Now.Date;
                                        _tmpSerChg.Hps_from_val = Convert.ToDecimal(row.Cells["col_S_FromVal"].Value);
                                        _tmpSerChg.Hps_pty_cd = pc;
                                        _tmpSerChg.Hps_pty_tp = "PC";
                                        _tmpSerChg.Hps_rt = Convert.ToDecimal(row.Cells["col_S_ChgRt"].Value);
                                        _tmpSerChg.Hps_sch_cd = sch;
                                        _tmpSerChg.Hps_seq = 0;
                                        _tmpSerChg.Hps_to_val = Convert.ToDecimal(row.Cells["col_S_ToVal"].Value);
                                        _tmpSerChg.Hps_valid_from = Convert.ToDateTime(row.Cells["col_S_FromDt"].Value);
                                        _tmpSerChg.Hps_valid_to = Convert.ToDateTime(row.Cells["col_s_ToDt"].Value);
                                        _tmpSerChg.Hps_mgr_comm_amt = Convert.ToDecimal(row.Cells["col_S_CommAmt"].Value);
                                        _tmpSerChg.Hps_mgr_comm_rt = Convert.ToDecimal(row.Cells["col_S_commRt"].Value);
                                        _finalserChgList.Add(_tmpSerChg);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                    foreach (ListViewItem schList in lstSerAppSch.Items)
                    {
                        string sch = schList.Text;

                        if (schList.Checked == true)
                        {
                            foreach (DataGridViewRow row in dgvSerChgValDef.Rows)
                            {
                                _tmpSerChg = new HpServiceCharges();

                                _tmpSerChg.Hps_cal_on = Convert.ToBoolean(row.Cells["col_S_CalOn"].Value);
                                _tmpSerChg.Hps_chg = Convert.ToDecimal(row.Cells["col_S_ChgAmt"].Value);
                                _tmpSerChg.Hps_chk_on = Convert.ToBoolean(row.Cells["col_S_ChckOn"].Value);
                                _tmpSerChg.Hps_cre_by = BaseCls.GlbUserID;
                                _tmpSerChg.Hps_cre_dt = DateTime.Now.Date;
                                _tmpSerChg.Hps_from_val = Convert.ToDecimal(row.Cells["col_S_FromVal"].Value);
                                if (cmbSerAppDefBy.Text == "Group")
                                {
                                    _tmpSerChg.Hps_pty_cd = "GRUP01";
                                    _tmpSerChg.Hps_pty_tp = "GPC";
                                }
                                else if (cmbSerAppDefBy.Text == "Company")
                                {
                                    _tmpSerChg.Hps_pty_cd = BaseCls.GlbUserComCode;
                                    _tmpSerChg.Hps_pty_tp = "COM";
                                }

                                _tmpSerChg.Hps_rt = Convert.ToDecimal(row.Cells["col_S_ChgRt"].Value);
                                _tmpSerChg.Hps_sch_cd = sch;
                                _tmpSerChg.Hps_seq = 0;
                                _tmpSerChg.Hps_to_val = Convert.ToDecimal(row.Cells["col_S_ToVal"].Value);
                                _tmpSerChg.Hps_valid_from = Convert.ToDateTime(row.Cells["col_S_FromDt"].Value);
                                _tmpSerChg.Hps_valid_to = Convert.ToDateTime(row.Cells["col_s_ToDt"].Value);
                                _tmpSerChg.Hps_mgr_comm_amt = Convert.ToDecimal(row.Cells["col_S_CommAmt"].Value);
                                _tmpSerChg.Hps_mgr_comm_rt = Convert.ToDecimal(row.Cells["col_S_commRt"].Value);
                                _finalserChgList.Add(_tmpSerChg);
                            }
                        }
                    }


                }

                dgvFinalSerChgDef.AutoGenerateColumns = false;
                dgvFinalSerChgDef.DataSource = new List<HpServiceCharges>();
                dgvFinalSerChgDef.DataSource = _finalserChgList;

                txtSerChgFromVal.Text = "";
                lstSerAppSch.Clear();
                lstSerChgAppPC.Clear();
                dgvSerChgValDef.AutoGenerateColumns = false;
                dgvSerChgValDef.DataSource = new List<HpServiceCharges>();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSerChgSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (dgvFinalSerChgDef.Rows.Count == 0)
                {
                    MessageBox.Show("No definition found to save.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                row_aff = (Int32)CHNLSVC.Sales.SaveServiceChgDef(_finalserChgList);

                if (row_aff == 1)
                {

                    MessageBox.Show("Service charge definition created successfully.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSerialCir_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularForSerial);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForSerial(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialcir;
                _CommonSearch.ShowDialog();
                txtSerialcir.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerialcir_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularForSerial);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForSerial(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialcir;
                _CommonSearch.ShowDialog();
                txtSerialcir.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerialcir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularForSerial);
                    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForSerial(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerialcir;
                    _CommonSearch.ShowDialog();
                    txtSerialcir.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSerialItem.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSerialItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSerialcir.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialForCircular);
                    DataTable _result = CHNLSVC.CommonSearch.GetSerialDetForCir(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerialItem;
                    _CommonSearch.ShowDialog();
                    txtSerialItem.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerialItem;
                    _CommonSearch.ShowDialog();
                    txtSerialItem.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerialItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSerialcir.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialForCircular);
                    DataTable _result = CHNLSVC.CommonSearch.GetSerialDetForCir(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerialItem;
                    _CommonSearch.ShowDialog();
                    txtSerialItem.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSerialItem;
                    _CommonSearch.ShowDialog();
                    txtSerialItem.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerialItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (!string.IsNullOrEmpty(txtSerialcir.Text))
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialForCircular);
                        DataTable _result = CHNLSVC.CommonSearch.GetSerialDetForCir(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtSerialItem;
                        _CommonSearch.ShowDialog();
                        txtSerialItem.Select();
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtSerialItem;
                        _CommonSearch.ShowDialog();
                        txtSerialItem.Select();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddSerial.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSerialItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSerialItem.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtSerialItem.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerialItem.Clear();
                        txtSerialItem.Focus();

                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddSerial_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSerialDetails.AutoGenerateColumns = false;
                dgvSerialDetails.DataSource = new List<PriceSerialRef>();

                if (string.IsNullOrEmpty(txtSerialcir.Text) && string.IsNullOrEmpty(txtSerialItem.Text))
                {
                    MessageBox.Show("Please enter circular # or item.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerialcir.Focus();
                    return;
                }

                //List<PriceSerialRef> _tmpSerialPrice = new List<PriceSerialRef>();
                //_tmpSerialPrice = CHNLSVC.Sales.getSerialpriceDetailsForCir(txtSerialItem.Text, txtSerialcir.Text);

                DataTable _tmpSerialPrice = new DataTable();
                _tmpSerialPrice = CHNLSVC.Sales.getSerialpriceDetailsForCirDT(txtSerialItem.Text, txtSerialcir.Text);

                if (_tmpSerialPrice.Rows.Count <1)
                {
                    MessageBox.Show("Cannot find any serial detail for selected criteria.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerialcir.Focus();
                    return;
                }

                dgvSerialDetails.AutoGenerateColumns = false;
                dgvSerialDetails.DataSource = new List<PriceSerialRef>();
                dgvSerialDetails.DataSource = _tmpSerialPrice;

                txtSerialcir.Text = "";
                txtSerialItem.Text = "";
                txtSerialcir.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSerialClear_Click(object sender, EventArgs e)
        {
            dgvSerialDetails.AutoGenerateColumns = false;
            dgvSerialDetails.DataSource = new List<PriceSerialRef>();

            txtSerialcir.Text = "";
            txtSerialItem.Text = "";
            txtSerialcir.Focus();
        }

        private void btnSerialSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSerialDetails.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == false)
                {
                    chk.Value = true;
                }

            }
        }

        private void btnSerialUnSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvSerialDetails.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }

            }
        }

        private void dgvSerialDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgvSerialDetails.Rows[dgvSerialDetails.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
                case "True":
                    {
                        ch1.Value = false;
                        break;
                    }
            }
        }

        private void btnLoadSchems_Click(object sender, EventArgs e)
        {
            try
            {
                //List<HpSchemeDetails> _tmpSchList = new List<HpSchemeDetails>();

                //_tmpSchList = CHNLSVC.Sales.getAllActiveSchemes(null);
                //foreach (HpSchemeDetails _row in _tmpSchList)
                //{
                //    lstProofAllScheme.Items.Add(_row.Hsd_cd);
                //}

                lstProofAllScheme.Clear();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(SearchParams, null, null);

                if (_result != null)
                {
                    foreach (DataRow _row in _result.Rows)
                    {
                        lstProofAllScheme.Items.Add(_row["Code"].ToString());
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnProofClear_Click(object sender, EventArgs e)
        {
            Clear_Proof();
        }

        private void btnSearchDoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProofDoc);
                DataTable _result = CHNLSVC.CommonSearch.GetAllProofDocs(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProofDoc;
                _CommonSearch.ShowDialog();
                txtProofDoc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProofDoc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProofDoc);
                DataTable _result = CHNLSVC.CommonSearch.GetAllProofDocs(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProofDoc;
                _CommonSearch.ShowDialog();
                txtProofDoc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProofDoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProofDoc);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllProofDocs(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtProofDoc;
                    _CommonSearch.ShowDialog();
                    txtProofDoc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    chkMando.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProofDoc.Text))
                {
                    MessageBox.Show("Please select document", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProofDoc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblProofDoc.Text))
                {
                    MessageBox.Show("Please select document", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProofDoc.Focus();
                    return;
                }

                for (int x = 0; x < dgvProofDoc.Rows.Count; x++)
                {
                    if (dgvProofDoc.Rows[x].Cells["col_P_DocCD"].Value.ToString() == txtProofDoc.Text.Trim())
                    {
                        MessageBox.Show("Selected document is already added.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtProofDoc.Text = "";
                        lblProofDoc.Text = "";
                        txtProofDoc.Focus();
                        return;
                    }
                }

                string _isMan = "";
                dgvProofDoc.Rows.Add();
                dgvProofDoc["col_P_DocCD", dgvProofDoc.Rows.Count - 1].Value = txtProofDoc.Text.Trim();
                dgvProofDoc["col_P_Doc", dgvProofDoc.Rows.Count - 1].Value = lblProofDoc.Text.Trim();
                if (chkMando.Checked == true)
                {
                    _isMan = "Yes";
                }
                else
                {
                    _isMan = "No";
                }
                dgvProofDoc["col_P_DocMan", dgvProofDoc.Rows.Count - 1].Value = _isMan;


                txtProofDoc.Text = "";
                lblProofDoc.Text = "";
                chkMando.Checked = false;
                txtProofDoc.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProofDoc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Do you want to remove selected document ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Int32 I = e.RowIndex;
                dgvProofDoc.Rows.RemoveAt(I);
            }
        }

        private void btnProofApply_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isSchFound = false;
                foreach (ListViewItem Item in lstProofAllScheme.Items)
                {
                    string sch = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isSchFound = true;
                        goto L2;
                    }
                }
            L2:

                if (_isSchFound == false)
                {
                    MessageBox.Show("No any applicable scheme(s) are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvProofDoc.Rows.Count == 0)
                {
                    MessageBox.Show("No any documents are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _tmpProofDoc = new List<HpProofDoc>();
                HpProofDoc _addProof = new HpProofDoc();

                foreach (ListViewItem schList in lstProofAllScheme.Items)
                {
                    string sch = schList.Text;

                    if (schList.Checked == true)
                    {
                        foreach (DataGridViewRow row in dgvProofDoc.Rows)
                        {
                            string _isreq = row.Cells["col_P_DocMan"].Value.ToString();
                            _addProof = new HpProofDoc();
                            _addProof.Hsp_cre_by = BaseCls.GlbUserID;
                            _addProof.Hsp_cre_dt = DateTime.Now.Date;
                            if (_isreq == "Yes")
                            {
                                _addProof.Hsp_is_required = true;
                            }
                            else
                            {
                                _addProof.Hsp_is_required = false;
                            }
                            _addProof.Hsp_prd_cd = Convert.ToInt32(row.Cells["col_P_DocCD"].Value);
                            _addProof.Hsp_sch_cd = sch;

                            _tmpProofDoc.Add(_addProof);
                        }
                    }
                }

                dgvFinalProofDoc.AutoGenerateColumns = false;
                dgvFinalProofDoc.DataSource = new List<HpProofDoc>();
                dgvFinalProofDoc.DataSource = _tmpProofDoc;

                dgvProofDoc.Rows.Clear();
                lstProofAllScheme.Clear();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProofSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (dgvFinalProofDoc.Rows.Count == 0)
                {
                    MessageBox.Show("Cannot find any details to save", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                row_aff = (Int32)CHNLSVC.Sales.SaveProofDoc(_tmpProofDoc);

                if (row_aff == 1)
                {

                    MessageBox.Show("Proof document definition created successfully.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProofDoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProofDoc.Text))
                {
                    MasterProofDocs _tmpProof = new MasterProofDocs();
                    _tmpProof = CHNLSVC.Sales.GetMasterProofDoc(txtProofDoc.Text);

                    if (_tmpProof.Hpd_prd_cd == null)
                    {
                        MessageBox.Show("Invalid document code.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtProofDoc.Text = "";
                        txtProofDoc.Focus();
                        return;
                    }
                    else
                    {
                        lblProofDoc.Text = _tmpProof.Hpd_desc;
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProfSchClear_Click(object sender, EventArgs e)
        {
            lstProofAllScheme.Clear();
        }

        private void btnProfSchSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstProofAllScheme.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnProfSchUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstProofAllScheme.Items)
            {
                Item.Checked = false;
            }
        }

        private void chkMando_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddDoc.Focus();
            }
        }

        private void btnSearchClonePc_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtClonePc;
                _CommonSearch.ShowDialog();
                txtClonePc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtClonePc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _searchType = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtClonePc;
                    _CommonSearch.ShowDialog();
                    txtClonePc.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtClonePc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtClonePc;
                _CommonSearch.ShowDialog();
                txtClonePc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSchCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtSchCircular;
                _CommonSearch.ShowDialog();
                txtSchCircular.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSchCircular_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtSchCircular;
                _CommonSearch.ShowDialog();
                txtSchCircular.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSchCircular_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSchCircular.Text))
                {
                    List<HpSchemeDefinition> _recallList = new List<HpSchemeDefinition>();

                    _recallList = CHNLSVC.Sales.GetSchemeDetailsByCir(txtSchCircular.Text.Trim());

                    if (_recallList.Count > 0 && _recallList != null)
                    {
                        if (MessageBox.Show("Definitions are already exsist for this circular.Do you want to view those. ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {

                            var _record = (from _lst in _recallList
                                           select _lst.Hpc_comm_cat).Distinct().ToList();

                            if (_record.Count > 1)
                            {
                                MessageBox.Show("Same circular exsist multiple scheme tpyes.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSchCircular.Text = "";
                                txtSchCircular.Focus();
                                return;
                            }
                            else
                            {
                                foreach (var j in _record)
                                {
                                    if (j == "PB")
                                    {
                                        cmbCommDefType.Text = "Price Book Wise";
                                        goto L1;
                                    }
                                    else if (j == "CAT")
                                    {
                                        cmbCommDefType.Text = "Main Category Wise";
                                        goto L1;
                                    }
                                    else if (j == "CAT2")
                                    {
                                        cmbCommDefType.Text = "Sub Category Wise";
                                        goto L1;
                                    }
                                    else if (j == "ITM")
                                    {
                                        cmbCommDefType.Text = "Product Wise";
                                        goto L1;
                                    }
                                    else if (j == "PROMO")
                                    {
                                        cmbCommDefType.Text = "Promotion Wise";
                                        goto L1;
                                    }
                                    else if (j == "CUS")
                                    {
                                        cmbCommDefType.Text = "Customer Wise";
                                        goto L1;
                                    }
                                    else if (j == "SERIAL")
                                    {
                                        cmbCommDefType.Text = "Serial Wise";
                                        goto L1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Unrecognized scheme type found.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtSchCircular.Text = "";
                                        txtSchCircular.Focus();
                                        return;
                                    }
                                }
                              
                            L1: Int16 x = 1;

                                var _stus = (from _lst in _recallList
                                             select _lst.Hpc_stus).Distinct().ToList();


                                foreach (var tmpRec in _stus)
                                {
                                    if (tmpRec == "C")
                                    {
                                        lblInfo.Text = "Selected Circular is already cancelled.";
                                    }
                                    else if (tmpRec == "A")
                                    {
                                        lblInfo.Text = "Selected Circular is Already approved.";
                                    }
                                    else if (tmpRec == "S")
                                    {
                                        lblInfo.Text = "Selected Circular is temp saved.";
                                    }
                                }

                                var _isaall = (from _lst in _recallList
                                               select _lst.Hpc_is_alw).Distinct().ToList();
                                foreach (var tmpRec in _isaall)
                                {
                                    if (tmpRec == true)
                                    {
                                        chkSchRestrict.Checked = false;
                                    }
                                    else
                                    {
                                        chkSchRestrict.Checked = true;
                                    }
                                }

                                lstItem.Clear();

                                //var _itm = (from _lst in _recallList
                                //               select _lst.Hpc_itm).Distinct().ToList();

                                //if (_itm.Count > 0)
                                //{
                                //    foreach (var _tmpItm in _itm)
                                //    {
                                //        lstItem.Items.Add(_tmpItm);
                                //    }
                                //}

                                DataTable dt = CHNLSVC.Sales.GetSchItembyCir(txtSchCircular.Text);
                                if (dt != null)
                                {
                                    foreach (DataRow drow in dt.Rows)
                                    {
                                        lstItem.Items.Add(drow["HPC_ITM"].ToString());
                                    }
                                }



                                foreach (ListViewItem Item in lstItem.Items)
                                {
                                    Item.Checked = true;
                                }

                                lstPC.Clear();

                                //var _pc = (from _lst in _recallList
                                //               where _lst.Hpc_pty_tp == "PC"
                                //               select _lst.Hpc_pty_cd).Distinct().ToList();

                                //if (_pc.Count > 0)
                                //{
                                //    foreach (var _tmppc in _pc)
                                //    {
                                //        lstPC.Items.Add(_tmppc);
                                //    }
                                //}

                                DataTable _pc = CHNLSVC.Sales.GetSchPCbyCir(txtSchCircular.Text);
                                if (_pc != null)
                                {
                                    foreach (DataRow drow in _pc.Rows)
                                    {
                                        lstPC.Items.Add(drow["hpc_pty_cd"].ToString());
                                    }
                                }

                                foreach (ListViewItem Item in lstPC.Items)
                                {
                                    Item.Checked = true;
                                }

                                lstSch.Clear();

                                //var _sch = (from _lst in _recallList
                                //            select _lst.Hpc_sch_cd).Distinct().ToList();

                                //if (_sch.Count > 0)
                                //{
                                //    foreach (var _tmpSch in _sch)
                                //    {
                                //        lstSch.Items.Add(_tmpSch);
                                //    }
                                //}

                                DataTable _sch = CHNLSVC.Sales.GetSchShedulebyCir(txtSchCircular.Text);
                                if (_sch != null)
                                {
                                    foreach (DataRow drow in _sch.Rows)
                                    {
                                        lstSch.Items.Add(drow["hpc_sch_cd"].ToString());
                                    }
                                }

                                foreach (ListViewItem Item in lstSch.Items)
                                {
                                    Item.Checked = true;
                                }


                                dgvPromo.AutoGenerateColumns = false;
                                dgvPromo.DataSource = new List<PriceDetailRef>();

                                _promoDetails = new List<PriceDetailRef>();

                                //var _promo = (from _lst in _recallList
                                //              select new { _lst.Hpc_pb, _lst.Hpc_pb_lvl,_lst.Hpc_pro }).Distinct().ToList();

                                DataTable _Promo = CHNLSVC.Sales.GetSchPromobyCir(txtSchCircular.Text);

                                //if (_promo.Count > 0)
                                //{
                                //    foreach (var _tmpPro in _promo)
                                //    {
                                //        List<PriceDetailRef> _tmpPromo = new List<PriceDetailRef>();
                                //        _tmpPromo = CHNLSVC.Sales.GetPriceDetailsByCir(_tmpPro.Hpc_pb, _tmpPro.Hpc_pb_lvl, _tmpPro.Hpc_pro, null);
                                //        _promoDetails.AddRange(_tmpPromo);        
                                //    }
                                //}

                                if (_Promo.Rows.Count > 0)
                                {
                                    foreach (DataRow drow in _Promo.Rows)
                                    {

                                        List<PriceDetailRef> _tmpPromo = new List<PriceDetailRef>();
                                        _tmpPromo = CHNLSVC.Sales.GetPriceDetailsByCir(drow["hpc_pb"].ToString(), drow["hpc_pb_lvl"].ToString(), drow["hpc_pro"].ToString(), null);
                                        _promoDetails.AddRange(_tmpPromo);

                                    }
                                }

                                dgvPromo.AutoGenerateColumns = false;
                                dgvPromo.DataSource = new List<PriceDetailRef>();
                                dgvPromo.DataSource = _promoDetails;

                                foreach (DataGridViewRow row in dgvPromo.Rows)
                                {
                                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                                    if (Convert.ToBoolean(chk.Value) == false)
                                    {
                                        chk.Value = true;
                                    }

                                }


                                lstCus.Clear();

                                //var _cus = (from _lst in _recallList
                                //            select _lst.Hpc_cust_cd).Distinct().ToList();

                                //if (_cus.Count > 0)
                                //{
                                //    foreach (var _tmpCus in _cus)
                                //    {
                                //        lstCus.Items.Add(_tmpCus);
                                //    }
                                //}

                                DataTable _cus = CHNLSVC.Sales.GetSchCusbyCir(txtSchCircular.Text);
                                if (_cus != null)
                                {
                                    foreach (DataRow drow in _cus.Rows)
                                    {
                                        lstCus.Items.Add(drow["hpc_cust_cd"].ToString());
                                    }
                                }

                                foreach (ListViewItem Item in lstCus.Items)
                                {
                                    Item.Checked = true;
                                }

                                _reCall = true;
                                dgvDefDetails.AutoGenerateColumns = false;
                                dgvDefDetails.DataSource = new List<HpSchemeDefinition>();
                                dgvDefDetails.DataSource = _recallList;
                            }

                        }
                        else
                        {
                            txtSchCircular.Text = "";
                            txtSchCircular.Focus();
                            return;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                List<HpSchemeDefinition> _recallList = new List<HpSchemeDefinition>();

                _recallList = CHNLSVC.Sales.GetSchemeDetailsByCir(txtSchCircular.Text.Trim());

                if (_recallList.Count > 0 && _recallList != null)
                {

                    var _record = (from _lst in _recallList
                                   select _lst.Hpc_stus).Distinct().ToList();


                    foreach (var tmpRec in _record)
                    {
                        if (tmpRec == "C")
                        {
                            MessageBox.Show("The perticular circular is already cancelled.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (tmpRec == "A")
                        {
                            MessageBox.Show("The perticular circular is already approved.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }


                }
                else
                {
                    MessageBox.Show("Cannot find any details for given circular.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<HpSchemeDefinitionLog> _SaveList = new List<HpSchemeDefinitionLog>();


                HpSchemeDefinitionLog _tmpList = new HpSchemeDefinitionLog();
                _tmpList.Hscl_cir = txtSchCircular.Text.Trim();
                _tmpList.Hscl_dis = 0;
                _tmpList.Hscl_dp_comm = 0;
                _tmpList.Hscl_inst_comm = 0;
                _tmpList.Hscl_restrict = false;
                _tmpList.Hscl_rmk = "APPROVAL GIVEN";
                _tmpList.Hscl_session = BaseCls.GlbUserSessionID;
                _tmpList.Hscl_usr = BaseCls.GlbUserID;
                _tmpList.Hscl_valid_from = Convert.ToDateTime(dtpFromDate.Value).Date;
                _tmpList.Hscl_valid_to = Convert.ToDateTime(dtpToDate.Value).Date;
                _SaveList.Add(_tmpList);




                row_aff = CHNLSVC.Sales.UpdateSchemeStatus(txtSchCircular.Text.Trim(), "A", BaseCls.GlbUserID, _SaveList);

                if (row_aff > 0)
                {
                    MessageBox.Show("Successfully approved.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetScheme_Click(object sender, EventArgs e)
        {
            try
            {
                //List<HpSchemeDefinition> _tmpList = new List<HpSchemeDefinition>();

                //_tmpList = CHNLSVC.Sales.GetAllSchemeCirculars();

                DataTable _schDt = CHNLSVC.Sales.GetAllSchemeCircularsToDataTable();

                if (_schDt != null)
                {
                    foreach (DataRow drow in _schDt.Rows)
                    {
                        HpSchemeDefinitionLog _newList = new HpSchemeDefinitionLog();
                        _newList.Hscl_cir = drow["Hpc_cir_no"].ToString();
                        _newList.Hscl_valid_from = Convert.ToDateTime(drow["Hpc_from_dt"]).Date;
                        _newList.Hscl_valid_to = Convert.ToDateTime(drow["Hpc_to_dt"]).Date;
                        _newList.Hscl_dp_comm = Convert.ToDecimal(drow["Hpc_dp_comm"]);
                        _newList.Hscl_inst_comm = Convert.ToDecimal(drow["Hpc_inst_comm"]);
                        _newList.Hscl_dis = Convert.ToDecimal(drow["Hpc_disc"]);
                        _newList.Hscl_restrict = Convert.ToBoolean(drow["Hpc_is_alw"]);
                        _generalCir.Add(_newList);

                        //lstCus.Items.Add(drow["hpc_cust_cd"].ToString());
                    }
                }

                //foreach (DataTable _temp in _schDt)
                //{
                //    HpSchemeDefinitionLog _newList = new HpSchemeDefinitionLog();
                //    _newList.Hscl_cir = _temp.Hpc_cir_no;
                //    _newList.Hscl_valid_from = _temp.Hpc_from_dt;
                //    _newList.Hscl_valid_to = _temp.Hpc_to_dt;
                //    _newList.Hscl_dp_comm = _temp.Hpc_dp_comm;
                //    _newList.Hscl_inst_comm = _temp.Hpc_inst_comm;
                //    _newList.Hscl_dis = _temp.Hpc_disc;
                //    _newList.Hscl_restrict = _temp.Hpc_is_alw;
                //    _generalCir.Add(_newList);
                //}

                dgvCirculars.AutoGenerateColumns = false;
                dgvCirculars.DataSource = new List<HpSchemeDefinitionLog>();
                dgvCirculars.DataSource = _generalCir;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCirculars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgvCirculars.Rows[dgvCirculars.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
                case "True":
                    {
                        ch1.Value = false;
                        break;
                    }
            }
        }

        private void cmbGenCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGenCate.Text == "Valid Date")
            {
                dtpGenUpdate.Enabled = true;
                txtGenRate.Text = "0";
                txtGenRate.Enabled = false;
                cmbGenType.Enabled = false;
            }
            else if (cmbGenCate.Text == "DP. Comm.")
            {
                dtpGenUpdate.Enabled = false;
                txtGenRate.Enabled = true;
                txtGenRate.Text = "";
                cmbGenType.Enabled = false;
            }
            else if (cmbGenCate.Text == "Inst. Comm.")
            {
                dtpGenUpdate.Enabled = false;
                txtGenRate.Enabled = true;
                txtGenRate.Text = "";
                cmbGenType.Enabled = false;
            }
            else if (cmbGenCate.Text == "Discount")
            {
                dtpGenUpdate.Enabled = false;
                txtGenRate.Enabled = true;
                txtGenRate.Text = "";
                cmbGenType.Enabled = true;
            }
        }

        private void txtGenRate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGenRate.Text))
                {
                    if (!IsNumeric(txtGenRate.Text))
                    {
                        MessageBox.Show("Invalid Rate.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGenRate.Text = "";
                        txtGenRate.Focus();
                        return;
                    }

                    if (cmbGenCate.Text == "Discount")
                    {
                        if (string.IsNullOrEmpty(cmbGenType.Text))
                        {
                            MessageBox.Show("Please select the type of discount.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbGenType.Focus();
                            return;
                        }
                        else if (cmbGenType.Text == "RATE")
                        {
                            if (Convert.ToDecimal(txtGenRate.Text) > 100 || Convert.ToDecimal(txtGenRate.Text) < 0)
                            {
                                MessageBox.Show("Discount rate should be within the range of 0 to 100.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtGenRate.Text = "";
                                txtGenRate.Focus();
                                return;
                            }
                        }
                        else if (cmbGenType.Text == "VALUE")
                        {
                            if (Convert.ToDecimal(txtGenRate.Text) < 0)
                            {
                                MessageBox.Show("Discount amount cannot less than zero.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtGenRate.Text = "";
                                txtGenRate.Focus();
                                return;
                            }
                        }

                    }
                    else if (cmbGenType.Text != "Valid Date")
                    {
                        if (Convert.ToDecimal(txtGenRate.Text) > 100 || Convert.ToDecimal(txtGenRate.Text) < 0)
                        {
                            MessageBox.Show("Discount rate should be within the range of 0 to 100.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtGenRate.Text = "";
                            txtGenRate.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCommonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string _rmk = "";
                Int32 row_aff = 0;
                Int16 _isRate = 0;
                string _msg = string.Empty;

                if (cmbGenCate.Text != "Valid Date")
                {
                    if (string.IsNullOrEmpty(txtGenRate.Text))
                    {
                        MessageBox.Show("Please enter rate.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGenRate.Text = "";
                        txtGenRate.Focus();
                        return;
                    }
                }

                if (cmbGenCate.Text == "Valid Date")
                {
                    if (Convert.ToDateTime(dtpGenUpdate.Value).Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("Cannot set backdate.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpGenUpdate.Focus();
                        return;
                    }
                }

                Boolean _appCir = false;
                foreach (DataGridViewRow row in dgvCirculars.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_C_Get"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _appCir = true;
                        goto L4;
                    }
                }
            L4:

                if (_appCir == false)
                {
                    MessageBox.Show("No any applicable circular is selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _rmk = cmbGenCate.Text + " " + "Updated";

                if (MessageBox.Show("Confirm to update selected scheme circulars. ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    List<HpSchemeDefinitionLog> _SaveList = new List<HpSchemeDefinitionLog>();

                    foreach (DataGridViewRow row in dgvCirculars.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells["col_C_Get"] as DataGridViewCheckBoxCell;

                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            HpSchemeDefinitionLog _tmpList = new HpSchemeDefinitionLog();
                            _tmpList.Hscl_cir = row.Cells["col_C_Cir"].Value.ToString();
                            _tmpList.Hscl_dis = Convert.ToDecimal(row.Cells["col_C_Dis"].Value);
                            _tmpList.Hscl_dp_comm = Convert.ToDecimal(row.Cells["col_C_DP"].Value);
                            _tmpList.Hscl_inst_comm = Convert.ToDecimal(row.Cells["col_C_Inst"].Value);
                            _tmpList.Hscl_restrict = Convert.ToBoolean(row.Cells["col_C_Alow"].Value);
                            _tmpList.Hscl_rmk = _rmk;
                            _tmpList.Hscl_session = BaseCls.GlbUserSessionID;
                            _tmpList.Hscl_usr = BaseCls.GlbUserID;
                            _tmpList.Hscl_valid_from = Convert.ToDateTime(row.Cells["col_C_frm"].Value).Date;
                            _tmpList.Hscl_valid_to = Convert.ToDateTime(row.Cells["col_C_To"].Value).Date;
                            _SaveList.Add(_tmpList);
                        }
                    }

                    if (cmbGenType.Text == "RATE")
                    {
                        _isRate = 1;
                    }
                    else
                    {
                        _isRate = 0;
                    }

                    row_aff = CHNLSVC.Sales.SaveGeneralSchemeUpdation(_SaveList, dtpGenUpdate.Value.Date, Convert.ToDecimal(txtGenRate.Text), _isRate, BaseCls.GlbUserID, cmbGenCate.Text, "S");

                    if (row_aff == 1)
                    {
                        MessageBox.Show("Successfully approved.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear_Data();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            MessageBox.Show(_msg, "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Faild to update.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGenRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnCommonUpdate.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnGenSearchCir_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtGenCir;
                _CommonSearch.ShowDialog();
                txtGenCir.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGenCir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
                    DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.obj_TragetTextBox = txtGenCir;
                    _CommonSearch.ShowDialog();
                    txtGenCir.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnGenCirSelect.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGenCir_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtGenCir;
                _CommonSearch.ShowDialog();
                txtGenCir.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGenCir_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSchCircular.Text))
                {
                    List<HpSchemeDefinition> _recallList = new List<HpSchemeDefinition>();

                    _recallList = CHNLSVC.Sales.GetSchemeDetailsByCir(txtGenCir.Text.Trim());

                    if (_recallList.Count > 0 && _recallList != null)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Please enter valid circular.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGenCir.Text = "";
                        txtGenCir.Focus();

                    }
                }
            }

            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenCirSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isFound = false;
                foreach (DataGridViewRow row in dgvCirculars.Rows)
                {
                    string _curSch = row.Cells["col_C_Cir"].Value.ToString();
                    if (_curSch == txtGenCir.Text)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == false)
                        {
                            chk.Value = true;
                            _isFound = true;
                            goto L1;
                        }
                    }

                }
            L1:

                if (_isFound == false)
                {
                    MessageBox.Show("Cannot found selected circular in above list.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGenCir.Text = "";
                    txtGenCir.Focus();
                    return;
                }
                txtGenCir.Text = "";
                txtGenCir.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpSerValidFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpSerValidTo.Focus();
            }
        }

        private void dtpSerValidTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerChgFromVal.Focus();
            }
        }

        private void txtMgrCommAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMgrCommRate.Focus();
            }
        }

        private void txtMgrCommRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddSerChg.Focus();
            }
        }

        private void txtMgrCommAmt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMgrCommAmt.Text))
            {
                if (!IsNumeric(txtMgrCommAmt.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommAmt.Text = "";
                    txtMgrCommAmt.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtMgrCommAmt.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommAmt.Text = "";
                    txtMgrCommAmt.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtMgrCommAmt.Text) > 0)
                {
                    txtMgrCommRate.Text = "0";
                }

                txtMgrCommAmt.Text = Convert.ToDecimal(txtMgrCommAmt.Text).ToString("n");
            }
        }

        private void txtMgrCommRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMgrCommRate.Text))
            {
                if (!IsNumeric(txtMgrCommRate.Text))
                {
                    MessageBox.Show("Please enter correct rate.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommRate.Text = "";
                    txtMgrCommRate.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtMgrCommRate.Text) < 0)
                {
                    MessageBox.Show("Rate should be within the range of 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommRate.Text = "";
                    txtMgrCommRate.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtMgrCommRate.Text) > 100)
                {
                    MessageBox.Show("Rate should be within the range of 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMgrCommRate.Text = "";
                    txtMgrCommRate.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtMgrCommRate.Text) > 0)
                {
                    txtMgrCommAmt.Text = "0";
                }

                txtMgrCommRate.Text = Convert.ToDecimal(txtMgrCommRate.Text).ToString("n");
            }
        }

        private void cmbCommDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommDef.Text == "Profit Center")
            {
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtChanel.Enabled = true;
                txtPC.Enabled = true;
                txtSChanel.Enabled = true;
                btnSearchChannel.Enabled = true;
                btnSearchPC.Enabled = true;
                btnSearchSubChannel.Enabled = true;
                lstPC.Clear();
                txtChanel.Focus();

            }
            else
            {
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtChanel.Enabled = true;
                txtPC.Enabled = false;
                txtSChanel.Enabled = true;
                btnSearchChannel.Enabled = true;
                btnSearchPC.Enabled = false;
                btnSearchSubChannel.Enabled = true;
                lstPC.Clear();
                txtChanel.Focus();
            }
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            //txtFileName.Text = _obj[_obj.Length - 1].ToString();
            txtFileName.Text = openFileDialog1.FileName;
        }

        private List<string> _itemLst = null;

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            lstItem.Clear();
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Clear();
                txtFileName.Focus();
                return;
            }

            System.IO.FileInfo _fileObj = new System.IO.FileInfo(txtFileName.Text);

            if (_fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Focus();
                return;
            }

            string _extension = _fileObj.Extension;
            string _conStr = string.Empty;

            if (_extension.ToUpper() == ".XLS") _conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"].ConnectionString;
            else if (_extension.ToUpper() == ".XLSX") _conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"].ConnectionString;

            _conStr = String.Format(_conStr, txtFileName.Text, "NO");
            OleDbConnection _connExcel = new OleDbConnection(_conStr);
            OleDbCommand _cmdExcel = new OleDbCommand();
            OleDbDataAdapter _oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            _cmdExcel.Connection = _connExcel;

            //Get the name of First Sheet
            _connExcel.Open();
            DataTable _dtExcelSchema;
            _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            _connExcel.Close();

            _connExcel.Open();
            _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            _oda.SelectCommand = _cmdExcel;
            _oda.Fill(_dt);
            _connExcel.Close();
            _itemLst = new List<string>();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (string.IsNullOrEmpty(_dr[0].ToString())) continue;

                    MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString().Trim());
                    if (_item == null)
                    {
                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid item - " + _dr[0].ToString());
                        else _errorLst.Append(" and invalid item - " + _dr[0].ToString());
                        continue;
                    }
                    var _dup = _itemLst.Where(x => x == _dr[0].ToString()).ToList();
                    if (_dup != null && _dup.Count > 0)
                    {
                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item " + _dr[0].ToString() + " duplicate");
                        else _errorLst.Append(" and item " + _dr[0].ToString() + " duplicate");
                        continue;
                    }
                    //_itemLst.Add(_dr[0].ToString().Trim());
                    lstItem.Items.Add(_dr[0].ToString().Trim());
                }

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        ////  _itemLst = new List<string>();

                    }
                }

            }

        }

        private void tbMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            SystemAppLevelParam _sysApp = new SystemAppLevelParam();

            if (tbMain.SelectedTab == tbMain.TabPages[7])
            {
                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT039", BaseCls.GlbUserID);
                if (_sysApp.Sarp_cd != null)
                {

                }
                else
                {
                    MessageBox.Show("You do not have permission to activate schemes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMain.SelectTab(0);
                    return;
                }
            }
        }

        private void btnSearchACate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemaCategory(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtACate;
                _CommonSearch.ShowDialog();
                txtACate.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtACate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtACate.Text))
            {
                DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies(txtACate.Text.Trim());
                if (dt.Rows.Count > 0)
                {

                }
                else
                {
                    MessageBox.Show("Invalid category selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtACate.Text = "";
                    txtACate.Focus();
                }
            }
        }

        private void txtACate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
                    DataTable _result = CHNLSVC.CommonSearch.GetSchemaCategory(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtACate;
                    _CommonSearch.ShowDialog();
                    txtACate.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtASchTp.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchASch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtACate.Text))
                {
                    MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtASchTp.Text = "";
                    txtACate.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate);
                DataTable _result = CHNLSVC.CommonSearch.GetSchemaTypeByCate(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtACate;
                _CommonSearch.ShowDialog();
                txtACate.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtASchTp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtASchTp.Text))
                {

                    HpSchemeType _Type = new HpSchemeType();
                    _Type = CHNLSVC.Sales.getSchemeType(txtASchTp.Text.Trim());

                    if (_Type.Hst_cd != null)
                    {
                        txtACate.Text = _Type.Hst_sch_cat;
                        txtASchTp.Text = _Type.Hst_cd;

                    }
                    else
                    {
                        MessageBox.Show("Invalid scheme type selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtASchTp.Text = "";
                        txtASchTp.Focus();
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
        }

        private void txtASchTp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtACate.Text))
                    {
                        MessageBox.Show("Please select scheme category.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtASchTp.Text = "";
                        txtACate.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate);
                    DataTable _result = CHNLSVC.CommonSearch.GetSchemaTypeByCate(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtASchTp;
                    _CommonSearch.ShowDialog();
                    txtASchTp.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtASchCode.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchAScheme_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllInactiveScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllInactiveScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtASchCode;
                _CommonSearch.ShowDialog();
                txtASchCode.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtASchCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtASchCode.Text))
                {
                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtASchCode.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtASchCode.Text = "";
                        txtASchCode.Focus();
                        return;
                    }
                    else
                    {
                        if (_tmpSch.Hsd_act == true)
                        {
                            MessageBox.Show("Selected scheme is already active.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtASchCode.Text = "";
                            txtASchCode.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchInactScheme_Click(object sender, EventArgs e)
        {
            try
            {
                List<HpSchemeDetails> _newList = new List<HpSchemeDetails>();

                dgvActSch.AutoGenerateColumns = false;
                dgvActSch.DataSource = new List<HpSchemeDetails>();

                _newList = CHNLSVC.Sales.GetSchForActivation(txtASchTp.Text.Trim(), txtASchCode.Text.Trim(), 0);

                dgvActSch.AutoGenerateColumns = false;
                dgvActSch.DataSource = new List<HpSchemeDetails>();
                dgvActSch.DataSource = _newList;

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearActivation_Click(object sender, EventArgs e)
        {
            Clear_Activation();
        }

        private void Clear_Activation()
        {
            txtACate.Text = "";
            txtASchTp.Text = "";
            txtASchCode.Text = "";
            lblSchDesc.Text = "";

            dgvActSch.AutoGenerateColumns = false;
            dgvActSch.DataSource = new List<HpSchemeDetails>();
        }

        private void txtASchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchInactScheme.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllInactiveScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllInactiveScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtASchCode;
                _CommonSearch.ShowDialog();
                txtASchCode.Select();
            }
        }

        private void dgvActSch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgvActSch.Rows[dgvActSch.CurrentRow.Index].Cells[6];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
                case "True":
                    {
                        ch1.Value = false;
                        break;
                    }
            }
        }

        private void btnSchActivation_Click(object sender, EventArgs e)
        {
            try
            {
                string _err = "";
                Boolean _isSelect = false;
                List<HpSchemeDetails> _saveList = new List<HpSchemeDetails>();

                foreach (DataGridViewRow row in dgvActSch.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["hsd_act"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _isSelect = true;
                        goto L10;
                    }
                }

            L10: Int16 I = 0;

                if (_isSelect == false)
                {
                    MessageBox.Show("Please select activation required scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                HpSchemeDetails _tmpList = new HpSchemeDetails();
                foreach (DataGridViewRow row in dgvActSch.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["hsd_act"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _tmpList = new HpSchemeDetails();
                        _tmpList.Hsd_cd = row.Cells["hsd_cd"].Value.ToString();
                        _tmpList.Hsd_cre_by = BaseCls.GlbUserID;
                        _tmpList.Hsd_act = true;
                        _saveList.Add(_tmpList);
                    }
                }

                Int16 effect = CHNLSVC.Sales.SchemeActivation(_saveList, out _err);

                if (effect == 1)
                {
                    MessageBox.Show("Successfully activated." + _err, "Scheme Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Activation();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_err))
                    {
                        MessageBox.Show(_err, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Activation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSearchVouType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVouType;
                _CommonSearch.ShowDialog();
                txtVouType.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVouType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVouType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtVouType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid voucher type.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVouType.Clear();
                    txtVouType.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVouType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVouType;
                _CommonSearch.ShowDialog();
                txtVouType.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVouType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                    DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtVouType;
                    _CommonSearch.ShowDialog();
                    txtVouType.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddVouTp_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(chkDisVou.Checked == true || chk_specVou.Checked == true))
                {
                    MessageBox.Show("Scheme is not setup as voucher applicable scheme.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (ListViewItem VouList in lstVou.Items)
                {
                    string _vou = VouList.Text;
                    if (txtVouType.Text.Trim() == _vou)
                    {
                        MessageBox.Show("Already selected.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVouType.Text = "";
                        txtVouType.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtVouType.Text))
                {
                    lstVou.Items.Add(txtVouType.Text.Trim());
                    txtVouType.Text = "";
                    txtVouType.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearVou_Click(object sender, EventArgs e)
        {
            lstVou.Clear();
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

        private void txtMInvType_DoubleClick(object sender, EventArgs e)
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

        private void txtMInvType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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
                else if (e.KeyCode == Keys.Enter)
                {
                    txtMaxAcc.Focus();
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

        private void txtMInvType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMInvType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("srtp_cd") == txtMInvType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid invoice type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dtpTo.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAddCusPara.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaxAcc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    chkResSameInv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCusPara_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkSpeCusBase.Checked == false)
                {
                    MessageBox.Show("Scheme is not setup as based on customer base.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtMInvType.Text))
                {
                    MessageBox.Show("Please select the valid invoice type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMInvType.Focus();
                    return;
                }

                //kapila 7/9/2015
                if (txtMInvType.Text != "HS" && chkIsClose.Checked == true)
                {
                    MessageBox.Show("Please select the valid invoice type.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMInvType.Focus();
                    return;

                }
                if (string.IsNullOrEmpty(txtMaxAcc.Text))
                {
                    MessageBox.Show("Please enter applicable no of accounts.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaxAcc.Focus();
                    return;
                }

                if (!IsNumeric(txtMaxAcc.Text))
                {
                    MessageBox.Show("No of accounts should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaxAcc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPrd.Text))
                {
                    MessageBox.Show("Please enter the period.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtValFrom.Text))
                {
                    MessageBox.Show("Please enter the value from.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValFrom.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtValTo.Text))
                {
                    MessageBox.Show("Please enter the value to.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValTo.Focus();
                    return;
                }

                if (chkCC.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtCCDays.Text))
                    {
                        MessageBox.Show("Please enter days to consider Cash Convertion.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCCDays.Focus();
                        return;
                    }

                    if (!IsNumeric(txtCCDays.Text))
                    {
                        MessageBox.Show("Please enter valid days.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCCDays.Text = "";
                        txtCCDays.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtCCDays.Text))
                {
                    if (Convert.ToInt32(txtCCDays.Text) > 0)
                    {
                        if (chkCC.Checked == false)
                        {
                            MessageBox.Show("Please select allow CC option before enter days.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCCDays.Text = "";
                            txtCCDays.Focus();
                            return;
                        }
                    }
                }

                //if (Convert.ToDateTime(dtpFrom.Value).Date < Convert.ToDateTime(DateTime.Now).Date)
                //{
                //    MessageBox.Show("Valid date cannot back date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    dtpFrom.Focus();
                //    return;
                //}

                if (Convert.ToDateTime(dtpFrom.Value).Date > Convert.ToDateTime(dtpTo.Value).Date)
                {
                    MessageBox.Show("Valid To date cannot less than from date.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpTo.Focus();
                    return;
                }

                for (int x = 0; x < dgvCusPara.Rows.Count; x++)
                {
                    if (dgvCusPara.Rows[x].Cells["col_SchCd"].Value.ToString() == txtSchCode.Text.Trim() && dgvCusPara.Rows[x].Cells["Col_SalesTp"].Value.ToString() == txtMInvType.Text.Trim())
                    {
                        MessageBox.Show("Selected sales type is already added.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMInvType.Focus();
                        return;
                    }

                    if (Convert.ToInt16(dgvCusPara.Rows[x].Cells["col_AppAcc"].Value) != Convert.ToInt16(txtMaxAcc.Text))
                    {
                        MessageBox.Show("Maximum no of applicable accounts cannot be differ.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaxAcc.Focus();
                        return;
                    }
                }

                HPAddSchemePara _tmpAddDet = new HPAddSchemePara();
                _tmpAddDet.Hap_com = BaseCls.GlbUserComCode;
                _tmpAddDet.Hap_sch = txtSchCode.Text.Trim();
                _tmpAddDet.Hap_tp = "CUS";
                _tmpAddDet.Hap_cd = txtMInvType.Text.Trim();
                _tmpAddDet.Hap_frm = dtpFrom.Value.Date;
                _tmpAddDet.Hap_to = dtpTo.Value.Date;
                _tmpAddDet.Hap_val1 = Convert.ToInt16(txtMaxAcc.Text);
                _tmpAddDet.Hap_val2 = 1;
                _tmpAddDet.Hap_val3 = Convert.ToInt16(chkResSameInv.Checked);
                _tmpAddDet.Hap_val4 = chkCC.Checked;
                _tmpAddDet.Hap_val5 = Convert.ToInt32(txtCCDays.Text);
                _tmpAddDet.Hap_anal6 = Convert.ToInt32(chkIsClose.Checked);   //kapila 7/9/2015
                _tmpAddDet.Hap_anal7 = Convert.ToInt32(txtPrd.Text);   //kapila 24/9/2015
                _tmpAddDet.Hap_anal8 = Convert.ToDecimal(txtValFrom.Text);
                _tmpAddDet.Hap_anal9 = Convert.ToDecimal(txtValTo.Text);

                _addSchPara.Add(_tmpAddDet);

                dgvCusPara.AutoGenerateColumns = false;
                dgvCusPara.DataSource = new List<HPAddSchemePara>();
                dgvCusPara.DataSource = _addSchPara;

                txtMInvType.Text = "";
                txtMaxAcc.Text = "";
                chkResSameInv.Checked = false;
                txtMInvType.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkDisVou_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisVou.Checked == true)
            {
                groupBox16.Enabled = true;
                chkVouMan.Visible = true;
                lblVouMan.Visible = true;
                //chk_specVou.Checked = false;
                optInvoie.Checked = true;
            }
            else
            {
                groupBox16.Enabled = false;
                lstVou.Items.Clear();
                chkVouMan.Checked = false;
                chkVouMan.Visible = false;
                lblVouMan.Visible = false;
            }
            optCollection_CheckedChanged(sender, e);
        }

        private void chkSpeCusBase_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSpeCusBase.Checked == true)
            {
                groupBox17.Enabled = true;
            }
            else
            {
                groupBox17.Enabled = false;
                dgvCusPara.AutoGenerateColumns = false;
                dgvCusPara.DataSource = new List<HPAddSchemePara>();
            }
        }

        private void dgvCusPara_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you want to remove selected details ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_addSchPara == null || _addSchPara.Count == 0) return;

                    string _sch = txtSchCode.Text.Trim();
                    string _item = dgvCusPara.Rows[e.RowIndex].Cells["Col_SalesTp"].Value.ToString();



                    List<HPAddSchemePara> _temp = new List<HPAddSchemePara>();
                    _temp = _addSchPara;

                    _temp.RemoveAll(x => x.Hap_sch == _sch && x.Hap_cd == _item);
                    _addSchPara = _temp;


                    dgvCusPara.AutoGenerateColumns = false;
                    dgvCusPara.DataSource = new List<HPAddSchemePara>();
                    dgvCusPara.DataSource = _addSchPara;
                }
            }
        }

        private void chkDisVou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkSpeCusBase.Focus();
            }

        }

        private void chkSpeCusBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbFPayType.Focus();
            }
        }

        private void chkResSameInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpFrom.Focus();
            }
        }

        private void chkCC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCC.Checked == true)
            {
                txtCCDays.Text = "";
                txtCCDays.Enabled = true;
            }
            else
            {
                txtCCDays.Text = "0";
                txtCCDays.Enabled = false;
            }
        }

        private void txtPrd_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrd.Text))
            {
                if (!IsNumeric(txtPrd.Text))
                {
                    MessageBox.Show("Should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrd.Focus();
                    return;
                }
            }
        }

        private void txtValFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValFrom.Text))
            {
                if (!IsNumeric(txtValFrom.Text))
                {
                    MessageBox.Show("Should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValFrom.Focus();
                    return;
                }
            }
        }

        private void txtValTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValTo.Text))
            {
                if (!IsNumeric(txtValTo.Text))
                {
                    MessageBox.Show("Should be numeric.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtValTo.Focus();
                    return;
                }
            }
        }

        private void chk_specVou_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_specVou.Checked == true)
            {
                //chkDisVou.Checked = false;
                optCollection.Checked = true;
                groupBox16.Enabled = true;
            }
            optCollection_CheckedChanged(sender, e);
        }

        private void optCollection_CheckedChanged(object sender, EventArgs e)
        {
            //if (optCollection.Checked == true)
            //{
            //    grpVouRental.Visible = true;
            //}
            //else
            //{
            //    grpVouRental.Visible = false;
            //}
        }

        private void optInvoie_CheckedChanged(object sender, EventArgs e)
        {
            if (optInvoie.Checked == true)
            {
                grpVouRental.Visible = false;
            }
            else
            {
                grpVouRental.Visible = true;
            }
        }

        private void txtvourental_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvourental_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtvourental.Text))
            {
                txtvourental.Text = "0";
            }
            if (Convert.ToInt16(txtvourental.Text) < 0)
            {
                MessageBox.Show("Rental value should not be less than zero (o).", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtvourental.Focus();
                return;
            }
            if (Convert.ToInt16(txtvourental.Text) > Convert.ToInt16(txtTerm.Text))
            {
                MessageBox.Show("Rental value should be equal or less than Term.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtvourental.Focus();
                return;
            }

        }

        private void txtTerm_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkPC_Click(object sender, EventArgs e)
        {

        }

        private void chkPC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPC.Checked)
            {
                txtPriceCirc.Enabled = true;
                btn_srch_price_circ.Enabled = true;
                btn_load_circ.Enabled = true;
            }
            else
            {
                txtPriceCirc.Enabled = false;
                btn_srch_price_circ.Enabled = false;
                btn_load_circ.Enabled = false;
            }
        }

        private void btn_load_circ_Click(object sender, EventArgs e)
        {
            DataTable _dt = null;
            if (cmbCommDefType.Text == "Product Wise" || cmbCommDefType.Text == "Main Category Wise" || cmbCommDefType.Text == "Sub Category Wise")
            {
                lstItem.Clear();
                _dt = CHNLSVC.Sales.GetSchDetByPriceCirc(txtPriceCirc.Text, cmbCommDefType.Text);
                foreach (DataRow r in _dt.Rows)
                {
                    if (cmbCommDefType.Text == "Product Wise")
                        lstItem.Items.Add(r["sapd_itm_cd"].ToString());
                    if (cmbCommDefType.Text == "Main Category Wise")
                        lstItem.Items.Add(r["mi_cate_1"].ToString());
                    if (cmbCommDefType.Text == "Sub Category Wise")
                        lstItem.Items.Add(r["mi_cate_2"].ToString());

         
                }
                  _dt = CHNLSVC.Sales.GetSchDetByPriceCirc(txtPriceCirc.Text, "Price Book Wise");

                  if (_dt.Rows.Count > 0)
                  {
                      txtPriceBook.Text = _dt.Rows[0]["sapd_pb_tp_cd"].ToString();
                      txtPriceLevel.Text = _dt.Rows[0]["sapd_pbk_lvl_cd"].ToString();
                      txtPromoBook.Text = _dt.Rows[0]["sapd_pb_tp_cd"].ToString();
                      txtPromoLevel.Text = _dt.Rows[0]["sapd_pbk_lvl_cd"].ToString();
                  }
            }
            if (cmbCommDefType.Text == "Promotion Wise")
            {
                _dt = CHNLSVC.Sales.GetSchDetByPriceCirc(txtPriceCirc.Text, cmbCommDefType.Text);

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = _dt;
            }
            if (cmbCommDefType.Text == "Serial Wise")
            {
                _dt = CHNLSVC.Sales.GetSchDetByPriceCirc(txtPriceCirc.Text, cmbCommDefType.Text);

                dgvSerialDetails.AutoGenerateColumns = false;
                dgvSerialDetails.DataSource = _dt;
            }
            if (cmbCommDefType.Text == "Price Book Wise")
            {
                _dt = CHNLSVC.Sales.GetSchDetByPriceCirc(txtPriceCirc.Text, cmbCommDefType.Text);

                if (_dt.Rows.Count > 0)
                {
                    txtPriceBook.Text = _dt.Rows[0]["sapd_pb_tp_cd"].ToString();
                    txtPriceLevel.Text = _dt.Rows[0]["sapd_pbk_lvl_cd"].ToString();
                    txtPromoBook.Text = _dt.Rows[0]["sapd_pb_tp_cd"].ToString();
                    txtPromoLevel.Text = _dt.Rows[0]["sapd_pbk_lvl_cd"].ToString();
                }
            }
            lstSch.Clear();
            DataTable _dtPSCH = CHNLSVC.General.GetPBScheme(txtPriceCirc.Text);
            foreach (DataRow r in _dtPSCH.Rows)
            {
                lstSch.Items.Add(r["saps_sch"].ToString());
            }
            btnCommSchSelect_Click(null, null);
            txtCircRem.Visible = true;
        }

        private void btn_srch_price_circ_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPromoBook.Text) && string.IsNullOrEmpty(txtPromoLevel.Text))
                {
                    txtPromoCir.Text = "";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.searchCircular);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPriceCirc;
                    _CommonSearch.ShowDialog();
                    txtPriceCirc.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchByBookAndLevel(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPriceCirc;
                    _CommonSearch.ShowDialog();
                    txtPriceCirc.Select();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPriceCirc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPriceCirc.Text))
            {
                DataTable _det = CHNLSVC.Sales.GetCircularNo(txtPriceCirc.Text);
                if (_det.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid Circular number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPriceCirc.Text = "";
                    return;
                }
                txtCircRem.Text = "";
                DataTable _dtPSCHR = CHNLSVC.General.GetPBSchemeRem(txtPriceCirc.Text);
                if (_dtPSCHR.Rows.Count > 0)
                {
                    txtCircRem.Text = _dtPSCHR.Rows[0]["sapsr_rem"].ToString();
                }
            }
        }

        private void txtPriceCirc_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_price_circ_Click(null, null);
        }

        private void txtPriceCirc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_price_circ_Click(null, null);
        }

        private void txtCircRem_Click(object sender, EventArgs e)
        {
            txtCircRem.Visible = false;
        }
        //tharanga  2017/05/20
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (groupBox19.Visible == true)
            {
                grdCmpny.Visible = false;
                groupBox2.Visible = true;
                groupBox3.Visible = true;
                groupBox19.Visible = false;
            }
            else
            {
                groupBox19.Visible = true;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                grdCmpny.Visible = true;
                Load_compny();
            }
         
        }

        private void Load_compny()
        {
            #region Load company list with saved data
            try
            {

             
                grdCmpny.Rows.Clear();
                DataTable dt = CHNLSVC.MsgPortal.GetCompanyTable();
                if (dt.Rows.Count > 0)
                {
                    for (int count = 0; count < dt.Rows.Count; count++)
                    {

                        grdCmpny.Rows.Add();
                        grdCmpny.Rows[count].Cells["Code"].Value = dt.Rows[count]["MC_CD"].ToString();
                        grdCmpny.Rows[count].Cells["Description"].Value = dt.Rows[count]["MC_DESC"].ToString();
                    }
                }

                DataTable odt = new DataTable();
                odt = CHNLSVC.Sales.Get_SCH_ALW_COM(txtType.Text.Trim());
                if ((odt.Rows.Count > 0) && (grdCmpny.Rows.Count > 0))
                {
                    grdCmpny.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    if (grdCmpny.CurrentRow != null) { grdCmpny.CurrentRow.Selected = false; }

                    foreach (DataRow _company in odt.Rows)
                    {
                        foreach (DataGridViewRow _grvRow in grdCmpny.Rows)
                        {
                            string _tmpAllCom = _company["HSAC_SCH_COM"] == DBNull.Value ? string.Empty : _company["HSAC_SCH_COM"].ToString();
                            if (_grvRow.Cells["Code"].Value.ToString() == _tmpAllCom)
                            {
                                _grvRow.Selected = true;
                                _grvRow.Cells["chkcom"].Value = true;

                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
        //tharanga  2017/05/20
        private void saveSchemaTocompany()
        {
            int isactive = 1;
            try
            {
                if (chkNewtypeActive.Checked)
                {
                    isactive = 1;
                }
                else
                {
                    isactive =0;
                }
                SchemetypeCom SchemetypeCom = new SchemetypeCom();
                HpSchemeType _Type = new HpSchemeType();
                List<SchemetypeCom> _list = new List<SchemetypeCom>();
                int counline;
                _Type = CHNLSVC.Sales.getSchemeType(txtType.Text.Trim());
                if (_Type.Hst_cd != null)
                {
                    
                   
                    DataTable dtmaxcount = new DataTable();
                    dtmaxcount = CHNLSVC.Sales.GetmaxCount_SCH_ALW_COM(txtType.Text.Trim());
                    counline = Convert.ToInt32(dtmaxcount.Rows[0]["count"].ToString());


                    foreach (DataGridViewRow Row in grdCmpny.Rows)
                    {
                        SchemetypeCom _SchemetypeCom = new SchemetypeCom();
                        if ((Row.Cells["chkcom"].Value == null ? false : (bool)Row.Cells["chkcom"].Value) == true)
                        {
                          //  SchemetypeCom _SchemetypeCom = new SchemetypeCom();

                            counline++;
                            bool chj = Convert.ToBoolean(Row.Cells[0].Value);
                            if (chj == true)
                            {
                                isactive = 1;
                            }
                            else
                            {
                                isactive = 0;
                            }
                            _SchemetypeCom.HSAC_SCH_TP = (txtType.Text.ToString().Trim());
                            _SchemetypeCom.HSAC_LINE = Convert.ToInt32(counline.ToString());
                            _SchemetypeCom.HSAC_SCH_COM = Row.Cells["Code"].Value.ToString();
                            _SchemetypeCom.HSAC_ACT_STUS = isactive;
                            _SchemetypeCom.HSAC_CRE_BY = BaseCls.GlbUserID;
                            _SchemetypeCom.HSAC_MOD_BY = BaseCls.GlbUserID;
                            _list.Add(_SchemetypeCom);
                        }
                 }


                    int _eff = CHNLSVC.Sales.Save_SCH_ALW_COM(_list);


                    foreach (DataGridViewRow allrow in grdCmpny.Rows)
                    {
                        SchemetypeCom _SchemetypeCom = new SchemetypeCom();

                        bool chj = Convert.ToBoolean(allrow.Cells[0].Value);
                            if (chj == true)
                            {
                                isactive = 1;
                            }
                            else
                            {
                                isactive = 0;
                            }
                            _SchemetypeCom.HSAC_SCH_TP = (txtType.Text.ToString().Trim());
                            _SchemetypeCom.HSAC_LINE = Convert.ToInt32(counline.ToString());
                            _SchemetypeCom.HSAC_SCH_COM = allrow.Cells["Code"].Value.ToString();
                            _SchemetypeCom.HSAC_ACT_STUS = isactive;
                            _SchemetypeCom.HSAC_CRE_BY = BaseCls.GlbUserID;
                            _SchemetypeCom.HSAC_MOD_BY = BaseCls.GlbUserID;
                            _list.Add(_SchemetypeCom);
                    }

                    int _count = CHNLSVC.Sales.update_SCH_ALW_COM(_list);


                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    
        }


        //tharanga  2017/05/22
        private void updateSchemaTocompany()
        {
            int isactive = 0;
            try
            {
                SchemetypeCom SchemetypeCom = new SchemetypeCom();
                HpSchemeType _Type = new HpSchemeType();
                List<SchemetypeCom> _list = new List<SchemetypeCom>();
                int counline;
                _Type = CHNLSVC.Sales.getSchemeType(txtType.Text.Trim());
                if (_Type.Hst_cd != null)
                {
                    DataTable dtmaxcount = new DataTable();
                    dtmaxcount = CHNLSVC.Sales.GetmaxCount_SCH_ALW_COM(txtType.Text.Trim());
                    counline = Convert.ToInt32(dtmaxcount.Rows[0]["count"].ToString());
                    foreach (DataGridViewRow Row in grdCmpny.Rows)
                    {
                        SchemetypeCom _SchemetypeCom = new SchemetypeCom();
                            counline++;

                            bool chj = Convert.ToBoolean(Row.Cells[0].Value);
                            if (chj == true)
                            {
                                isactive = 1;
                            }
                            else
                            {
                                isactive = 0;
                            }

                            _SchemetypeCom.HSAC_SCH_TP = (txtType.Text.ToString().Trim());
                            _SchemetypeCom.HSAC_LINE = Convert.ToInt32(counline.ToString());
                            _SchemetypeCom.HSAC_SCH_COM = Row.Cells["Code"].Value.ToString();
                            _SchemetypeCom.HSAC_ACT_STUS = isactive;
                            _SchemetypeCom.HSAC_CRE_BY = BaseCls.GlbUserID;
                            _SchemetypeCom.HSAC_MOD_BY = BaseCls.GlbUserID;
                            _list.Add(_SchemetypeCom);
                    }
                    int _eff = CHNLSVC.Sales.update_SCH_ALW_COM(_list);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }







    }
}
