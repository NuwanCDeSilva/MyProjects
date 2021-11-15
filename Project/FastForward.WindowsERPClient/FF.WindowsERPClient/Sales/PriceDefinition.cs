using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Configuration;
using System.Data.OleDb;


namespace FF.WindowsERPClient.Sales
{
    public partial class PriceDefinition : Base
    {


        private List<PriceDetailRef> _list = new List<PriceDetailRef>();
        private List<PriceSerialRef> _serial = new List<PriceSerialRef>();
        private List<PriceCombinedItemRef> _comList = new List<PriceCombinedItemRef>();
        private List<PriceProfitCenterPromotion> _appPcList = new List<PriceProfitCenterPromotion>();
        private List<MasterItemSimilar> _similarDetails = new List<MasterItemSimilar>();
        private List<PriceDefinitionRef> _defDet = new List<PriceDefinitionRef>();
        private List<SAR_DOC_CHANNEL_PRICE_DEFN> _defDetdef = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();
        private List<PromoVouRedeemPB> _lstRdmPB = new List<PromoVouRedeemPB>();
        Deposit_Bank_Pc_wise objdefaultupdate = null;
        Deposit_Bank_Pc_wise objpromoupdate = null;
        private Int32 _seqNo = 0;
        private Boolean _isRecal = false;
        private Boolean _isSimilarRecal = false;
        private string _Stype = "";
        private string _Ltype = "";
        private int seq = 0;
        private int MainLine = 0;
        private int SubLine = 0;
        private bool canAmmend;
        private Boolean _isRestrict = false;
        private Boolean _isUpdate = false;
        private Boolean isRedeemConfirms = false;
        List<MasterItemSubCate> _lstcate2 = new List<MasterItemSubCate>();
        List<PromotionVoucherPara> _promoVouPara = new List<PromotionVoucherPara>();
        List<Circular_Schemes> _CircSch = new List<Circular_Schemes>();
        private Boolean _isrecallDef = false;
        private Int32 _validPeriod = 0;
        private DataTable _dtSIItems = null;
        private DataTable shpmnet_Hdr = new DataTable(); // add by tharanga 2017/10/30
        private DataTable shipmtDetail = new DataTable();
        private List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
        private PriceBookLevelRef _priceBookLevelRef = new PriceBookLevelRef();
        private Boolean _isStrucBaseTax = false;
        private PriceBookLevelRef _PriceBookLevelRef = new PriceBookLevelRef();
        private bool IsSaleFigureRoundUp = true;
        public PriceDefinition()
        {
            InitializeComponent();
            MainLine = -1;
            SubLine = -1;
            canAmmend = true;
            pnlPB.Size = new Size(901, 500);
            pnlPB.Location = new Point(1, 1);
            pnlSI.Size = new Size(902, 513);
            pnlSI.Location = new Point(1, 0);
            pnlCanselSer.Size = new Size(567, 513);
            pnlCanselSer.Location = new Point(336, 0);
            pnlPriceTp.Location = new Point(336, 69);
            pnlBatch.Location = new Point(1, 54);
            gbAppPc.Location = new Point(2, 144);
            gbRes.Location = new Point(431, 0);
            dtpEnd.Value = DateTime.Now.Date;
        }


        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AllScheme:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    {

                        paramsText.Append(txtSrchCat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CancelSerialCirc:
                    {
                        paramsText.Append(txtCanCirc.Text + seperator + txtCanSer.Text + seperator + txtCanPB.Text + seperator + txtCanPBLvl.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        if (_Stype == "PromoVou")
                        {
                            paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator);
                            break;
                        }

                    }
                case CommonUIDefiniton.SearchUserControlType.DisVouTp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        if (_Stype == "Maintain")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtMBook.Text.Trim() + seperator);
                        }
                        else if (_Stype == "Additional")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtAddBook.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PromoVou")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB_pv.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PromoVouRedeem")
                        {
                            paramsText.Append(cmbRdmAllowCompany.SelectedValue + seperator + txtRdmComPB.Text.Trim() + seperator);
                        }

                        else if (_Stype == "DefMaintain")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtDpb.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PBDEF1")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtNpb.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PBDEF2")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtBasepb.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PBDEF3")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLvlPB.Text.Trim() + seperator);
                        }
                        else if (_Stype == "PBDEF4")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txt_p_book.Text.Trim() + seperator);
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtBook.Text.Trim() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllPBLevelByBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLvlPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
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

                case CommonUIDefiniton.SearchUserControlType.CircularDef:
                    {
                        paramsText.Append(string.Empty + seperator + BaseCls.GlbUserComCode + seperator);
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
                        if (_Ltype == "Similar")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtSChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_Ltype == "Maintain")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtmChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else if (_Ltype == "PromoVou")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChnnl_pv.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }

                        else if (_Ltype == "DefMaintain")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtDchannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        if (_Ltype == "Similar")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtSChannel.Text + seperator + txtSSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        }
                        else if (_Ltype == "Maintain")
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + txtmChannel.Text + seperator + txtmSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
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
                        if (_Stype == "PromoVou")
                        {
                            paramsText.Append(txtCat1_pv.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;

                        }
                        else if (_Stype == "PBDEF")
                        {
                            paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;

                        }

                        else
                        {
                            paramsText.Append(txtMainCate.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        if (_Stype == "PBDEF")
                        {
                            paramsText.Append(txtCate1.Text + seperator + txtCate3.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
                        else
                        {

                            paramsText.Append(txtMainCate.Text + seperator + txtSubCate.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                            break;
                        }
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
                case CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular:
                    {
                        paramsText.Append(txtCircular_pv.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular:
                    {
                        paramsText.Append(string.Empty + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.brandmngr:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private string SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType _type)
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
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtAgeCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtAgeCate1.Text + seperator + txtAgeCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtAgeOriPb.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private string SetCommonSearchInitialParameters2(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtAgeCloPb.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private string SetCommonSearchInitialParameters3(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SRMGR");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }


                default:
                    break;
            }

            return paramsText.ToString();
        }


        #endregion

        private void btnMainClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {
            canAmmend = true;
            _list = new List<PriceDetailRef>();
            _comList = new List<PriceCombinedItemRef>();
            _lstRdmPB = new List<PromoVouRedeemPB>();
            _appPcList = new List<PriceProfitCenterPromotion>();
            _similarDetails = new List<MasterItemSimilar>();
            _defDet = new List<PriceDefinitionRef>();
            _serial = new List<PriceSerialRef>();
            _CircSch = new List<Circular_Schemes>();
            cmbPriceType.Text = "NORMAL";
            cmbPriceType.Enabled = true;
            chkEndDate.Checked = false;
            chkMulti.Checked = false;
            chkMulti.Visible = false;
            txtBook.Text = "";
            txtLevel.Text = "";
            txtPromoCode.Text = "";
            txtCircular.Text = "";
            txtCusCode.Text = "";
            txtFilePath.Text = "";
            dtpFrom.Value = DateTime.Now.Date;
            dtpTo.Value = Convert.ToDateTime("31-Dec-2999").Date;
            chkEndDate.Checked = false;
            dtpTo.Enabled = false;
            chkCombine.Checked = false;
            txtItemCode.Text = "";
            lblModel.Text = "";
            txtFromQty.Text = "";
            txtToQty.Text = "";
            txtItemPrice.Text = "";
            txtNoOfTimes.Text = "999";
            txtWaraRemarks.Text = "N/A";
            _seqNo = 0;
            chkInactPrice.Checked = false;
            lstPromoItem.Clear();
            _isRecal = false;
            _isSimilarRecal = false;
            btnSimApply.Enabled = true;
            chkEndDate.Text = "End Date";
            btnAmend.Enabled = false;
            btnCancel.Enabled = false;
            chkWithOutCombine.Checked = false;
            chkWithOutCombine.Visible = false;
            _Stype = "";
            _Ltype = "";

            dgvType.AutoGenerateColumns = false;
            dgvType.DataSource = new DataTable();
            pnlPriceTp.Visible = false;

            _isRestrict = false;
            chkCustomer.Checked = false;
            chkNIC.Checked = false;
            chkMobile.Checked = false;
            txtMessage.Text = "";
            chkPP.Checked = false;
            chkDL.Checked = false;

            lblMainItem.Text = "";
            lblMainLine.Text = "";
            lblPrice.Text = "";
            btnAppPCUpdate.Enabled = false;

            btnCombineItems.Enabled = false;
            dgvPriceDet.AutoGenerateColumns = false;
            dgvPriceDet.DataSource = new List<PriceDetailRef>();

            dgvAppPC.AutoGenerateColumns = false;
            dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();

            dgvPromo.AutoGenerateColumns = false;
            dgvPromo.DataSource = new List<PriceCombinedItemRef>();

            btnMainSave.Enabled = true;
            gbCombine.Visible = false;
            txtBook.Focus();

            MainLine = -1;
            SubLine = -1;
            chkEndDate.Enabled = true;
            cmbPriceType.Enabled = true;

            dtAgTo.Value = Convert.ToDateTime("31-Dec-2999").Date;
            select_ITEMS_List = new DataTable();
            //tbPrice.TabPages.Remove(tbPromoVoucher);

            lblPAStatus.Text = "";
            //btnBatch.Enabled = false;
            chkAppPendings.Checked = false;
            btnSubAdd.Enabled = true;
            // txtSI.Text = "";

            lstSch.Clear();
            txtRem.Text = "";
            chkInf.Enabled = true;
            chkInf.Checked = false;
            btnCrdCntl.Enabled = false;

            btnAddSI.Enabled = true;
            grbUploadExcel.Visible = true;
            grbUploadCircular.Visible = false;

            rbUploadCircular.Enabled = false;
            rbUploadCircular.Checked = false;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16073)) { rbUploadCircular.Enabled = true; } else { rbUploadCircular.Enabled = false; }

            rbUploadExcel.Checked = true;
            txtUploadCircular.Text = "";
            pnlCircularPromoCodes.Visible = false;
            circularPromoList.Clear();
            btnUploadCircular.Visible = false;
            cmbPriceType.Enabled = true;
            TXTBRANDMNGR.Text = "";
            txtexpectedqty.Text = "";
            txtexpectedvalue.Text = "";
            DataTable shpmnet_Hdr = new DataTable();
            DataTable shipmtDetail = new DataTable();
            grdshipmentDet.DataSource = shipmtDetail;
            grdshipment_hdr.DataSource = shpmnet_Hdr;
            DataTable sar_pb_det = new DataTable();
            grdcurdet.DataSource = sar_pb_det;
            pnlcirdet.Visible = false;

            chkIsinfromim.Checked = false; //Tharindu
        }

        protected void BindPriceCategory()
        {
            cmbPriceCate.Items.Clear();
            List<PriceCategoryRef> _list = CHNLSVC.Sales.GetAllPriceCategory(string.Empty);
            _list.Add(new PriceCategoryRef { Sarpc_cd = "" });

            cmbPriceCate.DataSource = _list;
            cmbPriceCate.ValueMember = "Sarpc_cd";
            cmbPriceCate.DisplayMember = "Sarpc_cd";

        }

        protected void BindInvoiceType()
        {
            cmbPVInvoiceType.Items.Clear();
            List<MasterInvoiceType> _list = CHNLSVC.Sales.GetAllInvoiceType();
            List<MasterInvoiceType> _list1 = new List<MasterInvoiceType>();
            _list.Add(new MasterInvoiceType { Srtp_cd = "" });
            _list1 = _list.OrderBy(x => x.Srtp_cd).Where(x => x.Srtp_main_tp != "").ToList();
            cmbPVInvoiceType.DataSource = _list1;
            cmbPVInvoiceType.ValueMember = "Srtp_cd";
            cmbPVInvoiceType.DisplayMember = "Srtp_cd";
        }

        protected void BindPriceType()
        {
            cmbPriceType.Items.Clear();
            List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            _list.Add(new PriceTypeRef { Sarpt_cd = "", Sarpt_indi = -1 });
            cmbPriceType.DataSource = _list;
            cmbPriceType.DisplayMember = "Sarpt_cd";
            cmbPriceType.ValueMember = "Sarpt_indi";

            cmbPriceTp.Items.Clear();
            cmbPriceTp.DataSource = _list;
            cmbPriceTp.DisplayMember = "Sarpt_cd";
            cmbPriceTp.ValueMember = "Sarpt_indi";


            cmbptype.Items.Clear();
            cmbptype.DataSource = _list;
            cmbptype.DisplayMember = "Sarpt_cd";
            cmbptype.ValueMember = "Sarpt_indi";


        }
        private void BindPB()
        {
            DataTable _dt = CHNLSVC.Sales.SearchPriceBooks(BaseCls.GlbUserComCode);
            grvPB.AutoGenerateColumns = false;
            grvPB.DataSource = _dt;
        }

        private void PriceDefinition_Load(object sender, EventArgs e)
        {
            ClearVoucherParameters();//Addby akila 2017/12/22
            lblVouchDesc.Text = "";
            Clear_Data();
            clear_Similar();
            Clear_Maintaince();
            Clear_Add();
            BindPriceCategory();
            BindPriceType();
            InitializeItemDataTable();
            bindPayTypes(); //kapila 19/1/2016
            BindPB();  //kapila 8/4/2016
            BindInvoiceType();//Sanjeewa 2016-03-28

            BindCategoryTypes();

            //add sachith
            btnSaveAs.Enabled = false;
            tbPrice_SelectedIndexChanged(null, null);
            //check permission
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11052))
            {
                dgvPriceDet.Columns["col_p_Act"].ReadOnly = false;
            }
            else
            {
                dgvPriceDet.Columns["col_p_Act"].ReadOnly = true;
            }
            dtAgTo.Value = Convert.ToDateTime("31-Dec-2999").Date;
            lblVouchDesc.Text = "";
        }

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBook;
                _CommonSearch.ShowDialog();
                txtBook.Select();
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

        private void txtBook_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtBook;
                    _CommonSearch.ShowDialog();
                    txtBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtLevel.Focus();
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

        private void txtBook_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBook;
                _CommonSearch.ShowDialog();
                txtBook.Select();
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

        private void txtBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBook.Clear();
                    txtBook.Focus();
                }
                //ADDED BY SACHITH
                //ON 2014/02/06
                //CHECK USER PB,PLEVEL
                else
                {
                    /*
                     * Check user has 11048 permission
                     * if have get user pb and plevel acoording to date 
                     * if no data found provide error 
                     * 
                     */
                    //check permission
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                    {
                        //chek for pb and plevel
                        if (!string.IsNullOrEmpty(txtLevel.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, txtBook.Text.Trim(), txtLevel.Text.Trim(), 2);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + txtBook.Text + " and Price Level - " + txtLevel.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLevel.Text = "";
                                txtBook.Text = "";
                                txtBook.Focus();
                                return;
                            }
                        }
                        //chek for pb only
                        if (string.IsNullOrEmpty(txtLevel.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, txtBook.Text.Trim(), "", 1);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + txtBook.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtBook.Text = "";
                                txtBook.Focus();
                                return;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBook.Clear();
                    txtBook.Focus();
                    return;
                }
                _Stype = "";
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

        private void txtLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtBook.Text))
                    {
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBook.Clear();
                        txtBook.Focus();
                        return;
                    }

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
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCircular.Focus();
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

        private void txtLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBook.Clear();
                    txtBook.Focus();
                    return;
                }

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

        private void txtLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLevel.Text)) return;
                if (string.IsNullOrEmpty(txtLevel.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtLevel.Clear(); txtBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtBook.Text.Trim(), txtLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtLevel.Clear(); txtLevel.Focus(); return; }
                //ADDED BY SACHITH
                //ON 2014/02/06
                //CHECK USER PB,PLEVEL
                else
                {
                    /*
                     * Check user has 11048 permission
                     * if have get user pb and plevel acoording to date 
                     * if no data found provide error 
                     * 
                     */
                    //check permission
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                    {
                        //chek for pb and plevel
                        if (!string.IsNullOrEmpty(txtBook.Text))
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, txtBook.Text.Trim(), txtLevel.Text.Trim(), 2);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + txtBook.Text + " and Price Level - " + txtLevel.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLevel.Text = "";
                                txtBook.Text = "";
                                txtBook.Focus();
                                return;
                            }
                        }
                        //chek for plevel only
                        else
                        {
                            List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, "", txtLevel.Text.Trim(), 3);
                            if (_resList == null || _resList.Count <= 0)
                            {
                                MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + txtBook.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLevel.Text = "";
                                txtLevel.Focus();
                                return;
                            }
                        }
                    }

                    if (_tbl.Sapl_isbatch_wise == true)
                    {
                        btnBatch.Enabled = true;
                    }
                    else
                    {
                        btnBatch.Enabled = false;
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

        private void chkEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEndDate.Checked == true)
            {
                dtpTo.Enabled = true;
                dtpTo.Value = DateTime.Now.Date;

                if (_isRecal == true && canAmmend)
                {
                    btnAmend.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                    btnAmend.Enabled = false;
                    btnCancel.Enabled = false;
                }

                dtpTo.Focus();
            }
            else
            {
                btnAmend.Enabled = false;
                btnCancel.Enabled = false;
                dtpTo.Enabled = false;
                dtpTo.Value = Convert.ToDateTime("31-Dec-2999").Date;

            }
        }

        private void btnSearchCus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
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

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtFilePath.Focus();
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

        private void txtCusCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
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

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCusCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd == null)
                    {
                        MessageBox.Show("Please enter valid customer code.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusCode.Text = "";
                        txtCusCode.Focus();
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtFilePath.Text = openFileDialog1.FileName;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;
                if (string.IsNullOrEmpty(txtFilePath.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFilePath.Text = "";
                    txtFilePath.Focus();
                    return;
                }


                if (! string.IsNullOrEmpty(txtCircular.Text))
                {
                    //MessageBox.Show("Circular is ." + txtCircular.Text.Trim(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (MessageBox.Show("Confirm to Circular " + txtCircular.Text.Trim() , "Circular Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
               
                }

                if (string.IsNullOrEmpty(cmbPriceCate.Text))
                {
                    MessageBox.Show("Please select price category.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPriceCate.Text = "";
                    cmbPriceCate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbPriceType.Text))
                {
                    MessageBox.Show("Please select price type.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPriceType.Text = "";
                    cmbPriceType.Focus();
                    return;
                }

                if (chkWithOutCombine.Checked == true)
                {
                    if (chkCombine.Checked == true)
                    {
                        MessageBox.Show("You cannot select both combine and without combine.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (chkCombine.Checked == true)
                {
                    List<PriceTypeRef> _Typelist = CHNLSVC.Sales.GetAllPriceType(cmbPriceType.Text);
                    foreach (PriceTypeRef _tmp in _Typelist)
                    {
                        if (_tmp.Sarpt_is_com == true)
                        {
                            // btnCombineItems.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Please check selected price type.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
                else
                {
                    List<PriceTypeRef> _Typelist = CHNLSVC.Sales.GetAllPriceType(cmbPriceType.Text);
                    foreach (PriceTypeRef _tmp in _Typelist)
                    {
                        if (_tmp.Sarpt_is_com == true)
                        {
                            if (chkWithOutCombine.Checked == true)
                            {

                                if (MessageBox.Show("Confirm to continue without combine / free items ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Please check the price type and upload sheet.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                    }
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFilePath.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFilePath.Focus();
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension == ".xls")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension == ".xlsx")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }


                conStr = String.Format(conStr, txtFilePath.Text, 0);
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

                _list = new List<PriceDetailRef>();
                _comList = new List<PriceCombinedItemRef>();

                Int32 _currentRow = 0;

                MasterAutoNumber masterAuto = new MasterAutoNumber();

                if (cmbPriceType.SelectedItem != "NORMAL")
                {
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "PRO";
                    masterAuto.Aut_year = null;
                }
                else
                {
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "NOR";
                    masterAuto.Aut_year = null;
                }

                if (cmbPriceCate.Text == "NORMAL" || cmbPriceCate.Text == "TRANSFER")
                {
                    if (chkCombine.Checked == true)
                    {
                        Int32 _mainLine = 0;
                        Int32 _subLine = 0;
                        Int32 _pbSeq = 0;

                        _list = new List<PriceDetailRef>();
                        _comList = new List<PriceCombinedItemRef>();

                        foreach (DataRow _row in dt.Rows)
                        {
                            string _combineItem = "";
                            Int32 _combineQty = 0;
                            decimal _combinePrice = 0;
                            string _mainItemForcombine = "";

                            PriceDetailRef _one = new PriceDetailRef();
                            PriceCombinedItemRef _comDet = new PriceCombinedItemRef();

                            _currentRow = _currentRow + 1;
                            string _book = Convert.ToString(_row["A"]);
                            string _level = Convert.ToString(_row["B"]);
                            string _mainItem = Convert.ToString(_row["C"]);

                            //book level validation
                            //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                            //{
                            //    //chek for pb and plevel
                            //    if (!string.IsNullOrEmpty(_level))
                            //    {
                            //        List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, _level, 2);
                            //        if (_resList == null || _resList.Count <= 0)
                            //        {
                            //            MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book + " and Price Level - " + _level, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            return;
                            //        }
                            //    }
                            //    //chek for pb only
                            //    if (string.IsNullOrEmpty(_level))
                            //    {
                            //        List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, "", 1);
                            //        if (_resList == null || _resList.Count <= 0)
                            //        {
                            //            MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            return;
                            //        }
                            //    }
                            //}

                            _mainItemForcombine = Convert.ToString(_row["I"]);

                            bool _isValidItem = CHNLSVC.Inventory.IsValidItem(BaseCls.GlbUserComCode, _mainItem);
                            bool _isValidBook = CHNLSVC.Sales.IsValidBook(BaseCls.GlbUserComCode, _book);
                            bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(BaseCls.GlbUserComCode, _book, _level);

                            if (_isValidItem == false)
                            {
                                MessageBox.Show("Item code in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidBook == false)
                            {
                                MessageBox.Show("Price book in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidLevel == false)
                            {
                                MessageBox.Show("Price level in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            if (!string.IsNullOrEmpty(_mainItemForcombine))
                            {


                                bool _isValidMainItem = CHNLSVC.Inventory.IsValidItem(BaseCls.GlbUserComCode, _mainItemForcombine);

                                if (_isValidMainItem == false)
                                {
                                    MessageBox.Show("Main item code for combine item in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                _combineItem = Convert.ToString(_row["C"]); ;
                                _combineQty = Convert.ToInt32(_row["F"].ToString().Trim());
                                _combinePrice = Convert.ToDecimal(_row["H"]);
                                _subLine = _subLine + 1;

                                //if (_combinePrice <=0)
                                //{
                                //    MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //}

                                _comDet.Sapc_increse = chkMulti.Checked;
                                _comDet.Sapc_itm_cd = _combineItem;
                                _comDet.Sapc_itm_line = _subLine;
                                _comDet.Sapc_main_itm_cd = _mainItemForcombine;
                                _comDet.Sapc_main_line = _mainLine;
                                _comDet.Sapc_main_ser = null;
                                _comDet.Sapc_pb_seq = _pbSeq;
                                _comDet.Sapc_price = _combinePrice;
                                _comDet.Sapc_qty = _combineQty;
                                _comDet.Sapc_sub_ser = null;
                                _comDet.Sapc_tot_com = true;
                                _comList.Add(_comDet);

                            }
                            else
                            {
                                _mainLine = 1;
                                _subLine = 0;
                                _pbSeq = _pbSeq + 1;

                                if (string.IsNullOrEmpty(_row["D"].ToString()))
                                {
                                    MessageBox.Show("Invalid 'from date'. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                DateTime _fromDate = Convert.ToDateTime(_row["D"]).Date;
                                DateTime _toDate;
                                if (_row["E"] == DBNull.Value) _toDate = dtpTo.MaxDate;
                                else _toDate = Convert.ToDateTime(_row["E"]).Date;
                                Int32 _fromQty = Convert.ToInt32(_row["F"].ToString().Trim());
                                Int32 _toQty = Convert.ToInt32(_row["G"].ToString().Trim());
                                Decimal _UPrice = Convert.ToDecimal(_row["H"]);
                                string _warrantyRemarks = Convert.ToString(_row["N"]);
                                Int32 _noOfTimes = Convert.ToInt32(_row["J"]);
                                string _cusCode = Convert.ToString(_row["M"]);
                                string circular_no = "";
                                Int32 _SAPD_EST_QTY = 0;
                                Int32 SAPD_EST_VAL = 0;
                                Int32 SAPD_IS_INFM_IMD = 0;

                                //updated by akila 2018/02/08
                                if (IsRestrictBackDatePriceUpload(_fromDate,_toDate))
                                {
                                    return;
                                }


                                try // add by tharanga 2017/10/25
                                {
                                    circular_no = Convert.ToString(_row["P"]).Trim();
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("The excel file not contain column P ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                try // add by tharanga 2017/11/21
                                {
                                    _SAPD_EST_QTY = string.IsNullOrEmpty(_row["Q"].ToString()) ? 0 : Convert.ToInt32(_row["Q"]);
                                }
                                catch (Exception)
                                {
                                
                                    MessageBox.Show("The excel file not contain column Q ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                try // add by tharanga 2017/11/21
                                {
                                    SAPD_EST_VAL = string.IsNullOrEmpty(_row["R"].ToString()) ? 0 : Convert.ToInt32(_row["R"]);
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("The excel file not contain column R ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                if (string.IsNullOrEmpty(txtCircular.Text)) // add by tharanga 2017/10/25
                                {
                                    if (string.IsNullOrEmpty(circular_no))// add by tharanga 2017/10/25
                                    {
                                        MessageBox.Show("Please enter circular # in Excle File.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                }

                                if (string.IsNullOrEmpty(txtCircular.Text)) // add by tharanga 2017/10/25
                                {
                                    if (string.IsNullOrEmpty(circular_no))
                                    {
                                        MessageBox.Show("Please enter circular # in Excle File.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                }

                                MasterItem _oneItem = new MasterItem();
                                _oneItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItem);


                                if (!string.IsNullOrEmpty(_cusCode))
                                {
                                    MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _cusCode, string.Empty, string.Empty, "C");

                                    if (_masterBusinessCompany.Mbe_cd == null)
                                    {
                                        MessageBox.Show("Customer code is invalid. - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                }
                                if (_UPrice < 0)
                                {
                                    MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                //Tharindu

                                try 
                                {
                                    SAPD_IS_INFM_IMD = string.IsNullOrEmpty(_row["S"].ToString()) ? 0 : Convert.ToInt32(_row["S"]);
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("The excel file not contain column S ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                _one.Sapd_warr_remarks = _warrantyRemarks;
                                _one.Sapd_upload_dt = DateTime.Now;
                                _one.Sapd_update_dt = DateTime.Now;
                                _one.Sapd_to_date = _toDate;
                                _one.Sapd_session_id = BaseCls.GlbUserSessionID;
                                _one.Sapd_seq_no = _mainLine;
                                _one.Sapd_qty_to = _toQty;
                                _one.Sapd_qty_from = _fromQty;
                                _one.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                                _one.Sapd_price_stus = "P";
                                _one.Sapd_pbk_lvl_cd = _level;
                                _one.Sapd_pb_tp_cd = _book;
                                _one.Sapd_pb_seq = _pbSeq;
                                _one.Sapd_no_of_use_times = 0;
                                _one.Sapd_no_of_times = _noOfTimes;
                                _one.Sapd_model = _oneItem.Mi_model;
                                _one.Sapd_mod_when = DateTime.Now;
                                _one.Sapd_mod_by = BaseCls.GlbUserID;
                                _one.Sapd_margin = 0;
                                _one.Sapd_lst_cost = 0;
                                _one.Sapd_itm_price = _UPrice;
                                _one.Sapd_itm_cd = _mainItem;
                                _one.Sapd_is_cancel = false;
                                _one.Sapd_is_allow_individual = false;
                                _one.Sapd_from_date = _fromDate;
                                _one.Sapd_erp_ref = _level;
                                _one.Sapd_dp_ex_cost = 0;
                                _one.Sapd_day_attempt = 0;
                                _one.Sapd_customer_cd = _cusCode;
                                _one.Sapd_cre_when = DateTime.Now;
                                _one.Sapd_cre_by = BaseCls.GlbUserID;
                                if (!string.IsNullOrEmpty(txtCircular.Text))//add by tharanga 2017/10/25
                                {
                                    _one.Sapd_circular_no = txtCircular.Text.Trim();
                                }
                                else
                                {
                                    _one.Sapd_circular_no = circular_no;
                                }
                                //_one.Sapd_circular_no = txtCircular.Text.Trim();
                              
                                _one.Sapd_cancel_dt = DateTime.MinValue;
                                _one.Sapd_avg_cost = 0;
                                _one.Sapd_apply_on = "0";
                                _one.SAPD_EST_QTY = _SAPD_EST_QTY;//add by tharanga 2017/11/22
                                _one.SAPD_EST_VAL = SAPD_EST_VAL;//add by tharanga 2017/11/22
                                _one.Sapd_is_inform_immediatly = SAPD_IS_INFM_IMD;
                                _list.Add(_one);

                            }
                        }

                        if (_comList == null || _comList.Count <= 0)
                        {
                            MessageBox.Show("Cannot find combine items.Please check upload excel.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        Int32 _mainLine = 0;
                        Int32 _subLine = 0;
                        Int32 _pbSeq = 0;

                        _list = new List<PriceDetailRef>();
                        _comList = new List<PriceCombinedItemRef>();

                        foreach (DataRow _row in dt.Rows)
                        {
                            //string _combineItem = "";
                            //Int32 _combineQty = 0;
                            //decimal _combinePrice = 0;
                            //string _mainItemForcombine = "";

                            PriceDetailRef _one = new PriceDetailRef();
                            //PriceCombinedItemRef _comDet = new PriceCombinedItemRef();

                            _currentRow = _currentRow + 1;
                            string _book = Convert.ToString(_row["A"]);
                            string _level = Convert.ToString(_row["B"]);
                            string _mainItem = Convert.ToString(_row["C"].ToString().Trim());



                            //_mainItemForcombine = Convert.ToString(_row["I"]);

                            bool _isValidItem = CHNLSVC.Inventory.IsValidItem(BaseCls.GlbUserComCode, _mainItem);
                            bool _isValidBook = CHNLSVC.Sales.IsValidBook(BaseCls.GlbUserComCode, _book);
                            bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(BaseCls.GlbUserComCode, _book, _level);

                            if (_isValidItem == false)
                            {
                                MessageBox.Show("Item code in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidBook == false)
                            {
                                MessageBox.Show("Price book in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidLevel == false)
                            {
                                MessageBox.Show("Price level in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                            {
                                //chek for pb and plevel
                                if (!string.IsNullOrEmpty(_level))
                                {
                                    List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, _level, 2);
                                    if (_resList == null || _resList.Count <= 0)
                                    {
                                        MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book + " and Price Level - " + _level, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                //chek for pb only
                                if (string.IsNullOrEmpty(_level))
                                {
                                    List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, "", 1);
                                    if (_resList == null || _resList.Count <= 0)
                                    {
                                        MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }



                            _mainLine = 1;
                            _subLine = 0;
                            _pbSeq = _pbSeq + 1;

                            if (string.IsNullOrEmpty(_row["D"].ToString()))
                            {
                                MessageBox.Show("Invalid 'from date'. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            DateTime _fromDate = Convert.ToDateTime(_row["D"]).Date;
                            DateTime _toDate;
                            if (_row["E"] == DBNull.Value) _toDate = dtpTo.MaxDate;
                            else _toDate = Convert.ToDateTime(_row["E"]).Date;
                            Int32 _fromQty = Convert.ToInt32(_row["F"].ToString().Trim());
                            Int32 _toQty = Convert.ToInt32(_row["G"].ToString().Trim());
                            Decimal _UPrice = Convert.ToDecimal(_row["H"]);
                            string _warrantyRemarks = Convert.ToString(_row["N"]);
                            Int32 _noOfTimes = Convert.ToInt32(_row["J"]);
                            string _cusCode = Convert.ToString(_row["M"]);
                            string _stus = Convert.ToString(_row["O"]);
                            string circular_no = "";
                            Int32 _SAPD_EST_QTY = 0;
                            Int32 SAPD_EST_VAL = 0;
                            Int32 SAPD_IS_INFM_IMD = 0;

                            //updated by akila 2018/02/08
                            if (IsRestrictBackDatePriceUpload(_fromDate, _toDate))
                            {
                                return;
                            }

                            try // add by tharanga 2017/10/25
                            {
                                circular_no = Convert.ToString(_row["P"]).Trim();
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column P ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            try // add by tharanga 2017/10/25
                            {
                                _SAPD_EST_QTY = string.IsNullOrEmpty(_row["Q"].ToString()) ? 0 : Convert.ToInt32(_row["Q"]); 
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column Q ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            try // add by tharanga 2017/10/25
                            {
                                SAPD_EST_VAL = string.IsNullOrEmpty(_row["R"].ToString()) ? 0 : Convert.ToInt32(_row["R"]); 
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column R ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (string.IsNullOrEmpty(txtCircular.Text)) // add by tharanga 2017/10/25
                            {
                                if (string.IsNullOrEmpty(circular_no))
                                {
                                    MessageBox.Show("Please enter circular # in Excle File.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                             
                            }

                            MasterItem _oneItem = new MasterItem();
                            _oneItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItem);


                            if (!string.IsNullOrEmpty(_cusCode))
                            {
                                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _cusCode, string.Empty, string.Empty, "C");

                                if (_masterBusinessCompany.Mbe_cd == null)
                                {
                                    MessageBox.Show("Customer code is invalid. - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            }
                            //if (_UPrice <= 0)
                            //{
                            //    MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            if (_UPrice <= 0 && _stus != "S")
                            {
                                MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else if (_UPrice > 0 && _stus == "S")
                            {
                                MessageBox.Show("Suspend price should be zero.\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //Tharindu

                            try
                            {
                                SAPD_IS_INFM_IMD = string.IsNullOrEmpty(_row["S"].ToString()) ? 0 : Convert.ToInt32(_row["S"]);
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column S ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            _one.Sapd_warr_remarks = _warrantyRemarks;
                            _one.Sapd_upload_dt = DateTime.Now;
                            _one.Sapd_update_dt = DateTime.Now;
                            _one.Sapd_to_date = _toDate;
                            _one.Sapd_session_id = BaseCls.GlbUserSessionID;
                            _one.Sapd_seq_no = _mainLine;
                            _one.Sapd_qty_to = _toQty;
                            _one.Sapd_qty_from = _fromQty;
                            _one.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                            if (_stus == "S")
                            {
                                _one.Sapd_price_stus = _stus;
                            }
                            else
                            {
                                _one.Sapd_price_stus = "P";
                            }
                            _one.Sapd_pbk_lvl_cd = _level;
                            _one.Sapd_pb_tp_cd = _book;
                            _one.Sapd_pb_seq = _pbSeq;
                            _one.Sapd_no_of_use_times = 0;
                            _one.Sapd_no_of_times = _noOfTimes;
                            _one.Sapd_model = _oneItem.Mi_model;
                            _one.Sapd_mod_when = DateTime.Now;
                            _one.Sapd_mod_by = BaseCls.GlbUserID;
                            _one.Sapd_margin = 0;
                            _one.Sapd_lst_cost = 0;
                            _one.Sapd_itm_price = _UPrice;
                            _one.Sapd_itm_cd = _mainItem;
                            _one.Sapd_is_cancel = false;
                            _one.Sapd_is_allow_individual = false;
                            _one.Sapd_from_date = _fromDate;
                            _one.Sapd_erp_ref = _level;
                            _one.Sapd_dp_ex_cost = 0;
                            _one.Sapd_day_attempt = 0;
                            _one.Sapd_customer_cd = _cusCode;
                            _one.Sapd_cre_when = DateTime.Now;
                            _one.Sapd_cre_by = BaseCls.GlbUserID;
                            if (!string.IsNullOrEmpty(txtCircular.Text))//add by tharanga 2017/10/25
                            {
                                _one.Sapd_circular_no = txtCircular.Text.Trim();
                            }
                            else
                            {
                                _one.Sapd_circular_no = circular_no;
                            }
                            //_one.Sapd_circular_no = txtCircular.Text.Trim();
                            _one.Sapd_cancel_dt = DateTime.MinValue;
                            _one.Sapd_avg_cost = 0;
                            _one.Sapd_apply_on = "0";
                            if (chkWithOutCombine.Checked == true)
                            {
                                _one.Sapd_usr_ip = "IGNORE COMBINE";
                            }
                            _one.SAPD_EST_QTY = _SAPD_EST_QTY;
                            _one.SAPD_EST_VAL = SAPD_EST_VAL;

                            _one.Sapd_is_inform_immediatly = SAPD_IS_INFM_IMD; //Tharindu
                            _list.Add(_one);


                        }


                    }

                }
                else if (cmbPriceCate.Text == "SERIALIZED")
                {
                    if (chkCombine.Checked == true)
                    {
                        Int32 _mainLine = 0;
                        Int32 _subLine = 0;
                        Int32 _pbSeq = 0;

                        _serial = new List<PriceSerialRef>();
                        _comList = new List<PriceCombinedItemRef>();

                        foreach (DataRow _row in dt.Rows)
                        {
                            string _combineItem = "";
                            Int32 _combineQty = 0;
                            decimal _combinePrice = 0;
                            string _combineSerial = "";
                            string _mainItemForcombine = "";
                            string _mainSerialForcombine = "";

                            PriceSerialRef _one = new PriceSerialRef();
                            PriceCombinedItemRef _comDet = new PriceCombinedItemRef();

                            _currentRow = _currentRow + 1;
                            string _book = Convert.ToString(_row["A"]);
                            string _level = Convert.ToString(_row["B"]);
                            string _mainItem = Convert.ToString(_row["C"]);
                            string _mainSerial = Convert.ToString(_row["K"]);

                            _mainItemForcombine = Convert.ToString(_row["I"]);
                            _mainSerialForcombine = Convert.ToString(_row["L"]);

                            bool _isValidItem = CHNLSVC.Inventory.IsValidItem(BaseCls.GlbUserComCode, _mainItem);
                            bool _isValidBook = CHNLSVC.Sales.IsValidBook(BaseCls.GlbUserComCode, _book);
                            bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(BaseCls.GlbUserComCode, _book, _level);

                            if (_isValidItem == false)
                            {
                                MessageBox.Show("Item code in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidBook == false)
                            {
                                MessageBox.Show("Price book in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidLevel == false)
                            {
                                MessageBox.Show("Price level in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                            {
                                //chek for pb and plevel
                                if (!string.IsNullOrEmpty(_level))
                                {
                                    List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, _level, 2);
                                    if (_resList == null || _resList.Count <= 0)
                                    {
                                        MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book + " and Price Level - " + _level, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                //chek for pb only
                                if (string.IsNullOrEmpty(_level))
                                {
                                    List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, "", 1);
                                    if (_resList == null || _resList.Count <= 0)
                                    {
                                        MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }


                            if (!string.IsNullOrEmpty(_mainItemForcombine) || !string.IsNullOrEmpty(_mainSerialForcombine))
                            {


                                bool _isValidMainItem = CHNLSVC.Inventory.IsValidItem(BaseCls.GlbUserComCode, _mainItemForcombine);

                                if (_isValidMainItem == false)
                                {
                                    MessageBox.Show("Main item code for combine item in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                _combineItem = Convert.ToString(_row["C"]); ;
                                _combineQty = Convert.ToInt32(_row["F"].ToString().Trim());
                                _combinePrice = Convert.ToDecimal(_row["H"]);
                                _combineSerial = Convert.ToString(_row["K"]);

                                if (_combinePrice < 0)
                                {
                                    MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                _subLine = _subLine + 1;

                                _comDet.Sapc_increse = chkMulti.Checked;
                                _comDet.Sapc_itm_cd = _combineItem;
                                _comDet.Sapc_itm_line = _subLine;
                                _comDet.Sapc_main_itm_cd = _mainItemForcombine;
                                _comDet.Sapc_main_line = _mainLine;
                                _comDet.Sapc_main_ser = _mainSerialForcombine;
                                _comDet.Sapc_pb_seq = _pbSeq;
                                _comDet.Sapc_price = _combinePrice;
                                _comDet.Sapc_qty = _combineQty;
                                _comDet.Sapc_sub_ser = _combineSerial;
                                _comDet.Sapc_tot_com = true;
                                _comList.Add(_comDet);

                            }
                            else
                            {
                                _mainLine = 1;
                                _subLine = 0;
                                _pbSeq = _pbSeq + 1;

                                if (string.IsNullOrEmpty(_row["D"].ToString()))
                                {
                                    MessageBox.Show("Invalid 'from date'. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                DateTime _fromDate = Convert.ToDateTime(_row["D"]).Date;
                                DateTime _toDate;
                                if (_row["E"] == DBNull.Value) _toDate = dtpTo.MaxDate;
                                else _toDate = Convert.ToDateTime(_row["E"]).Date;
                                Int32 _fromQty = Convert.ToInt32(_row["F"].ToString().Trim());
                                Int32 _toQty = Convert.ToInt32(_row["G"].ToString().Trim());
                                Decimal _UPrice = Convert.ToDecimal(_row["H"]);
                                //string _warrantyRemarks = Convert.ToString(_row["K"]);
                                Int32 _noOfTimes = Convert.ToInt32(_row["J"]);
                                string _cusCode = Convert.ToString(_row["M"]);
                                string _waraRemark = Convert.ToString(_row["N"]);

                                string circular_no = "";
                                try // add by tharanga 2017/10/25
                                {
                                    circular_no = Convert.ToString(_row["P"]).Trim();
                                }
                                catch (Exception)
                                {

                                    MessageBox.Show("The excel file not contain column P ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                if (string.IsNullOrEmpty(txtCircular.Text)) // add by tharanga 2017/10/25
                                {
                                    if (string.IsNullOrEmpty(circular_no))// add by tharanga 2017/10/25
                                    {
                                        MessageBox.Show("Please enter circular # in Excle File.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                }


                                MasterItem _oneItem = new MasterItem();
                                _oneItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItem);


                                if (!string.IsNullOrEmpty(_cusCode))
                                {
                                    MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _cusCode, string.Empty, string.Empty, "C");

                                    if (_masterBusinessCompany.Mbe_cd == null)
                                    {
                                        MessageBox.Show("Customer code is invalid. - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                }
                                if (_UPrice < 0)
                                {
                                    MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(txtCircular.Text))//add by tharanga 2017/10/25
                                {
                                    _one.Sars_circular_no = txtCircular.Text.Trim();
                                }
                                else
                                {
                                    _one.Sars_circular_no = circular_no;
                                }

                                //_one.Sars_circular_no = txtCircular.Text.Trim();
                                _one.Sars_cre_by = BaseCls.GlbUserID;
                                _one.Sars_cre_when = DateTime.Now;
                                _one.Sars_customer_cd = _cusCode;
                                _one.Sars_day_attempt = 1;
                                _one.Sars_hp_allowed = 1;
                                _one.Sars_is_cancel = false;
                                _one.Sars_is_fix_qty = true;
                                _one.Sars_itm_cd = _mainItem;
                                _one.Sars_itm_price = _UPrice;
                                _one.Sars_mod_by = BaseCls.GlbUserID;
                                _one.Sars_mod_when = DateTime.Now;
                                _one.Sars_pb_seq = _pbSeq;
                                _one.Sars_pbook = _book;
                                _one.Sars_price_lvl = _level;
                                _one.Sars_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                                _one.Sars_promo_cd = "N/A";
                                _one.Sars_ser_no = _mainSerial;
                                _one.Sars_update_dt = Convert.ToDateTime(DateTime.Now).Date;
                                _one.Sars_val_frm = _fromDate;
                                _one.Sars_val_to = _toDate;
                                _one.Sars_warr_remarks = _waraRemark;
                                _serial.Add(_one);

                            }

                        }

                        if (_comList == null || _comList.Count <= 0)
                        {
                            MessageBox.Show("Cannot find combine items.Please check upload excel.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        Int32 _mainLine = 0;
                        Int32 _subLine = 0;
                        Int32 _pbSeq = 0;

                        _serial = new List<PriceSerialRef>();

                        foreach (DataRow _row in dt.Rows)
                        {

                            PriceSerialRef _one = new PriceSerialRef();
                            _currentRow = _currentRow + 1;
                            string _book = Convert.ToString(_row["A"]);
                            string _level = Convert.ToString(_row["B"]);
                            string _mainItem = Convert.ToString(_row["C"]);
                            string _mainSerial = Convert.ToString(_row["K"]);

                            bool _isValidItem = CHNLSVC.Inventory.IsValidItem(BaseCls.GlbUserComCode, _mainItem);
                            bool _isValidBook = CHNLSVC.Sales.IsValidBook(BaseCls.GlbUserComCode, _book);
                            bool _isValidLevel = CHNLSVC.Sales.IsValidLevel(BaseCls.GlbUserComCode, _book, _level);

                            if (_isValidItem == false)
                            {
                                MessageBox.Show("Item code in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidBook == false)
                            {
                                MessageBox.Show("Price book in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (_isValidLevel == false)
                            {
                                MessageBox.Show("Price level in the document is invalid. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                            {
                                //chek for pb and plevel
                                if (!string.IsNullOrEmpty(_level))
                                {
                                    List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, _level, 2);
                                    if (_resList == null || _resList.Count <= 0)
                                    {
                                        MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book + " and Price Level - " + _level, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                //chek for pb only
                                if (string.IsNullOrEmpty(_level))
                                {
                                    List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, _book, "", 1);
                                    if (_resList == null || _resList.Count <= 0)
                                    {
                                        MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + _book, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }


                            _mainLine = 1;
                            _subLine = 0;
                            _pbSeq = _pbSeq + 1;

                            if (string.IsNullOrEmpty(_row["D"].ToString()))
                            {
                                MessageBox.Show("Invalid 'from date'. please check the row - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            DateTime _fromDate = Convert.ToDateTime(_row["D"]).Date;
                            DateTime _toDate;
                            if (_row["E"] == DBNull.Value) _toDate = dtpTo.MaxDate;
                            else _toDate = Convert.ToDateTime(_row["E"]).Date;
                            Int32 _fromQty = Convert.ToInt32(_row["F"].ToString().Trim());
                            Int32 _toQty = Convert.ToInt32(_row["G"].ToString().Trim());
                            Decimal _UPrice = Convert.ToDecimal(_row["H"]);
                            //string _warrantyRemarks = Convert.ToString(_row["K"]);
                            Int32 _noOfTimes = Convert.ToInt32(_row["J"]);
                            string _cusCode = Convert.ToString(_row["M"]);
                            string _waraRemark = Convert.ToString(_row["N"]);
                            string circular_no = "";
                            Int32 _SAPD_EST_QTY = 0;
                            Int32 SAPD_EST_VAL = 0;
                            Int32 SAPD_IS_INFM_IMD = 0;
                            try // add by tharanga 2017/10/25
                            {
                                circular_no = Convert.ToString(_row["P"]).Trim();
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column P ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            try // add by tharanga 2017/11/21
                            {
                                _SAPD_EST_QTY = string.IsNullOrEmpty(_row["Q"].ToString()) ? 0 : Convert.ToInt32(_row["Q"]);
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column Q ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            try // add by tharanga 2017/11/21
                            {
                                SAPD_EST_VAL = string.IsNullOrEmpty(_row["R"].ToString()) ? 0 : Convert.ToInt32(_row["R"]);
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("The excel file not contain column R ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (string.IsNullOrEmpty(txtCircular.Text)) // add by tharanga 2017/10/25
                            {
                                if (string.IsNullOrEmpty(circular_no))
                                {
                                    MessageBox.Show("Please enter circular # in Excle File.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            }

                            MasterItem _oneItem = new MasterItem();
                            _oneItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItem);


                            if (!string.IsNullOrEmpty(_cusCode))
                            {
                                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();

                                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _cusCode, string.Empty, string.Empty, "C");

                                if (_masterBusinessCompany.Mbe_cd == null)
                                {
                                    MessageBox.Show("Customer code is invalid. - " + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                            }
                            if (_UPrice < 0)
                            {
                                MessageBox.Show("Price can not be 0 or less\nOn Row" + _currentRow.ToString(), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (!string.IsNullOrEmpty(txtCircular.Text))//add by tharanga 2017/10/25
                            {
                                _one.Sars_circular_no = txtCircular.Text.Trim();
                            }
                            else
                            {
                                _one.Sars_circular_no = circular_no;
                            }
                            //_one.Sars_circular_no = txtCircular.Text.Trim();
                            _one.Sars_cre_by = BaseCls.GlbUserID;
                            _one.Sars_cre_when = DateTime.Now;
                            _one.Sars_customer_cd = _cusCode;
                            _one.Sars_day_attempt = 1;
                            _one.Sars_hp_allowed = 1;
                            _one.Sars_is_cancel = false;
                            _one.Sars_is_fix_qty = true;
                            _one.Sars_itm_cd = _mainItem;
                            _one.Sars_itm_price = _UPrice;
                            _one.Sars_mod_by = BaseCls.GlbUserID;
                            _one.Sars_mod_when = DateTime.Now;
                            _one.Sars_pb_seq = _pbSeq;
                            _one.Sars_pbook = _book;
                            _one.Sars_price_lvl = _level;
                            _one.Sars_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                            _one.Sars_promo_cd = "N/A";
                            _one.Sars_ser_no = _mainSerial;
                            _one.Sars_update_dt = Convert.ToDateTime(DateTime.Now).Date;
                            _one.Sars_val_frm = _fromDate;
                            _one.Sars_val_to = _toDate;
                            _one.Sars_warr_remarks = _waraRemark;
                            _serial.Add(_one);



                        }


                    }
                }

                List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();

                //_savePcAllocList = _appPcList;
                foreach (DataGridViewRow row in dgvAppPC.Rows)
                {
                    string _pc = row.Cells["col_a_Pc"].Value.ToString();
                    string _promo = row.Cells["col_a_Promo"].Value.ToString();
                    Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                    Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                    if (_active == 1)
                    {
                        foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                        {
                            if (_tmp.Srpr_com == BaseCls.GlbUserComCode && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                            {
                                //PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                                //_tmpList.Srpr_act = _active;
                                //_tmpList.Srpr_com = BaseCls.GlbUserComCode;
                                //_tmpList.Srpr_cre_by = BaseCls.GlbUserID;
                                //_tmpList.Srpr_mod_by = BaseCls.GlbUserID;
                                //_tmpList.Srpr_pbseq = _pbSeq;
                                //_tmpList.Srpr_pc = _pc;
                                //_tmpList.Srpr_promo_cd = _promo;
                                _tmp.Srpr_act = _active;
                                _savePcAllocList.Add(_tmp);
                            }

                        }
                    }

                }

                PriceDetailRestriction _priceRes = new PriceDetailRestriction();
                //_priceRes = null;

                if (_isRestrict == true)
                {
                    _priceRes.Spr_com = BaseCls.GlbUserComCode;
                    _priceRes.Spr_msg = txtMessage.Text;
                    _priceRes.Spr_need_cus = chkCustomer.Checked;
                    _priceRes.Spr_need_nic = chkNIC.Checked;
                    _priceRes.Spr_need_pp = false;
                    _priceRes.Spr_need_dl = false;
                    _priceRes.Spr_usr = BaseCls.GlbUserID;
                    _priceRes.Spr_when = DateTime.Now;
                    _priceRes.Spr_promo = "";
                }
                else
                {
                    _priceRes = null;
                }

                //kapila 8/3/2017
                if (chkInf.Checked)
                {
                    foreach (ListViewItem schList in lstSch.Items)
                    {
                        string sch = schList.Text;

                        if (schList.Checked == true)
                        {
                            Circular_Schemes _listS = new Circular_Schemes();
                            _listS.Circ = txtCircular.Text;
                            _listS.Circ_Sch = sch;
                            _CircSch.Add(_listS);
                        }
                    }
                }

                string _err = "";
                row_aff = (Int32)CHNLSVC.Sales.SavePriceDetails(_list, _comList, masterAuto, _savePcAllocList, _serial, _priceRes, out _err, BaseCls.GlbUserSessionID, BaseCls.GlbUserID, BaseCls.GlbUserComCode, _CircSch, Convert.ToInt32(chkInf.Checked), txtRem.Text);

                if (row_aff == 1)
                {

                    MessageBox.Show("Price definition created successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_err))
                    {
                        MessageBox.Show(_err, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearchPromo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPromoCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromoCode;
                    _CommonSearch.ShowDialog();
                    txtPromoCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtCircular.Focus();
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

        private void btnSearchCirclar_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                if (chkAppPendings.Checked)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                }

                DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);

                DataTable _result = _resultTemp.Clone();
                if (chkAppPendings.Checked)
                {
                    _result = _resultTemp.Select("STATUS = 'P'").CopyToDataTable();
                }
                else
                {
                    _result.Merge(_resultTemp);
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.ShowDialog();
                txtCircular.Select();
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

        private void txtCircular_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                if (chkAppPendings.Checked)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                }

                DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);

                DataTable _result = _resultTemp.Clone();
                if (chkAppPendings.Checked)
                {
                    _result = _resultTemp.Select("STATUS = 'P'").CopyToDataTable();
                }
                else
                {
                    _result.Merge(_resultTemp);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.ShowDialog();
                txtCircular.Select();
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

        private void txtCircular_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    if (chkAppPendings.Checked)
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular);
                    }
                    else
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                    }

                    DataTable _resultTemp = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);

                    DataTable _result = _resultTemp.Clone();
                    if (chkAppPendings.Checked)
                    {
                        _result = _resultTemp.Select("STATUS = 'P'").CopyToDataTable();
                    }
                    else
                    {
                        _result.Merge(_resultTemp);
                    }
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.dvResult.Columns["STATUS"].Visible = false;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCircular;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtCircular.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddPromo.Focus();
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

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchType = "ITEMS";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItemCode;
                    _CommonSearch.ShowDialog();
                    txtItemCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtFromQty.Focus();
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

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
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

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemCode.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblModel.Text = "";
                        txtItemCode.Clear();
                        txtItemCode.Focus();

                    }
                    else
                    {
                        lblModel.Text = _itemdetail.Mi_model;
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

        private void txtFromQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFromQty.Text))
            {
                if (!IsNumeric(txtFromQty.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFromQty.Text = "";
                    txtFromQty.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtFromQty.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFromQty.Text = "";
                    txtFromQty.Focus();
                    return;
                }

            }
        }

        private void txtToQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtToQty.Text))
            {
                if (!IsNumeric(txtToQty.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToQty.Text = "";
                    txtToQty.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtToQty.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToQty.Text = "";
                    txtToQty.Focus();
                    return;
                }
            }
        }

        private void txtItemPrice_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemPrice.Text))
            {
                if (!IsNumeric(txtItemPrice.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemPrice.Text = "";
                    txtItemPrice.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtItemPrice.Text) < 0)
                {
                    MessageBox.Show("Value cannot be less than zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemPrice.Text = "";
                    txtItemPrice.Focus();
                    return;
                }

                txtItemPrice.Text = Convert.ToDecimal(txtItemPrice.Text).ToString("n");
            }
        }

        private void txtNoOfTimes_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoOfTimes.Text))
            {
                if (!IsNumeric(txtNoOfTimes.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfTimes.Text = "";
                    txtNoOfTimes.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtNoOfTimes.Text) < 0)
                {
                    MessageBox.Show("Times cannot be less than zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfTimes.Text = "";
                    txtNoOfTimes.Focus();
                    return;
                }

            }
        }

        private void txtFromQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtToQty.Focus();
            }
        }

        private void txtToQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemPrice.Focus();
            }
        }

        private void txtItemPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNoOfTimes.Focus();
            }
        }

        private void txtNoOfTimes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWaraRemarks.Focus();
            }
        }

        private void txtWaraRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddPrice.Focus();
            }
        }

        private void btnAddPrice_Click(object sender, EventArgs e)
        {
            try
            {
                string _priceType = "A";


                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    MessageBox.Show("Please select item code.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemCode.Text = "";
                    txtItemCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtFromQty.Text))
                {
                    MessageBox.Show("Please enter valid from qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFromQty.Text = "";
                    txtFromQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtToQty.Text))
                {
                    MessageBox.Show("Please enter valid to qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtToQty.Text = "";
                    txtToQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItemPrice.Text))
                {
                    MessageBox.Show("Please enter item price.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItemPrice.Text = "";
                    txtItemPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNoOfTimes.Text))
                {
                    MessageBox.Show("Please enter no of valid times.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfTimes.Text = "";
                    txtNoOfTimes.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtWaraRemarks.Text))
                {
                    txtWaraRemarks.Text = "N/A";
                }

                if (chkInactPrice.Checked == true)
                {
                    if (Convert.ToDecimal(txtItemPrice.Text) > 0)
                    {
                        MessageBox.Show("Inactive price should be zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItemPrice.Text = "";
                        txtItemPrice.Focus();
                        return;
                    }
                    _priceType = "S";
                }

                if (seq <= 0)
                {
                    _seqNo = _seqNo + 1;
                    PriceDetailRef _newPriceDet = new PriceDetailRef();
                    _newPriceDet.Sapd_warr_remarks = txtWaraRemarks.Text.Trim();
                    _newPriceDet.Sapd_upload_dt = DateTime.Now;
                    _newPriceDet.Sapd_update_dt = DateTime.Now;
                    _newPriceDet.Sapd_to_date = Convert.ToDateTime(dtpTo.Value).Date;
                    _newPriceDet.Sapd_session_id = BaseCls.GlbUserSessionID;
                    _newPriceDet.Sapd_seq_no = _seqNo;
                    _newPriceDet.Sapd_qty_to = Convert.ToDecimal(txtToQty.Text);
                    _newPriceDet.Sapd_qty_from = Convert.ToDecimal(txtFromQty.Text);
                    _newPriceDet.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                    _newPriceDet.Sapd_price_stus = _priceType;
                    _newPriceDet.Sapd_pbk_lvl_cd = txtLevel.Text.Trim();
                    _newPriceDet.Sapd_pb_tp_cd = txtBook.Text.Trim();
                    _newPriceDet.Sapd_pb_seq = _seqNo;
                    _newPriceDet.Sapd_no_of_use_times = 0;
                    _newPriceDet.Sapd_no_of_times = Convert.ToInt16(txtNoOfTimes.Text);
                    _newPriceDet.Sapd_model = lblModel.Text;
                    _newPriceDet.Sapd_mod_when = DateTime.Now;
                    _newPriceDet.Sapd_mod_by = BaseCls.GlbUserID;
                    _newPriceDet.Sapd_itm_price = Convert.ToDecimal(txtItemPrice.Text);
                    _newPriceDet.Sapd_itm_cd = txtItemCode.Text;
                    _newPriceDet.Sapd_is_cancel = chkInactPrice.Checked;
                    _newPriceDet.Sapd_is_allow_individual = false;
                    _newPriceDet.Sapd_from_date = Convert.ToDateTime(dtpFrom.Value).Date;
                    _newPriceDet.Sapd_erp_ref = txtLevel.Text.Trim();
                    _newPriceDet.Sapd_dp_ex_cost = 0;
                    _newPriceDet.Sapd_day_attempt = 0;
                    _newPriceDet.Sapd_customer_cd = txtCusCode.Text.Trim();
                    _newPriceDet.Sapd_cre_when = DateTime.Now;
                    _newPriceDet.Sapd_cre_by = BaseCls.GlbUserID;
                    _newPriceDet.Sapd_circular_no = txtCircular.Text.Trim();
                    _newPriceDet.Sapd_cancel_dt = DateTime.MinValue;
                    _newPriceDet.Sapd_apply_on = "0";
                    if (chkEndDate.Checked && btnAmend.Enabled)
                        _newPriceDet.Sapd_ser_upload = 6;
                    
                    // Tharindu 2018-07-16
                    int val = 0;
                    if (chkIsinfromim.Checked)
                    {
                        val = 1;
                    }

                    _newPriceDet.Sapd_is_inform_immediatly = val; 

                    //kapila 11/4/2016
                    List<mst_itm_com_reorder> _ltsCost = new List<mst_itm_com_reorder>();
                    _ltsCost = CHNLSVC.General.GetReOrder(txtItemCode.Text.ToUpper().Trim());
                    if (_ltsCost == null)
                    {
                        _ltsCost = new List<mst_itm_com_reorder>();
                    }
                    foreach (mst_itm_com_reorder _temp in _ltsCost)
                    {
                        if (_temp.Icr_com_code == BaseCls.GlbUserComCode)
                        {
                            _newPriceDet.Sapd_lst_cost = _temp.Icr_max_cost;
                            _newPriceDet.Sapd_avg_cost = _temp.Icr_avg_cost;

                            if (_newPriceDet.Sapd_avg_cost > 0)
                            {
                                _newPriceDet.Sapd_margin = (_newPriceDet.Sapd_itm_price - _newPriceDet.Sapd_avg_cost) / _newPriceDet.Sapd_avg_cost * 100;
                            }
                            else
                            {
                                _newPriceDet.Sapd_margin = 100;
                            }
                        }
                    }

                    _list.Add(_newPriceDet);


                }
                else
                {
                    //update
                    //get old item code
                    List<PriceDetailRef> _temp = (from _res in _list
                                                  where _res.Sapd_seq_no == seq
                                                  select _res).ToList<PriceDetailRef>();
                    string item = _temp[0].Sapd_itm_cd;

                    //update main item details
                    (from _res in _list
                     where _res.Sapd_seq_no == seq
                     select _res).ToList<PriceDetailRef>().ForEach(x =>
                     {
                         x.Sapd_itm_cd = txtItemCode.Text;
                         x.Sapd_qty_from = Convert.ToDecimal(txtFromQty.Text);
                         x.Sapd_qty_to = Convert.ToDecimal(txtToQty.Text);
                         x.Sapd_itm_price = Convert.ToDecimal(txtItemPrice.Text);
                         x.Sapd_warr_remarks = txtWaraRemarks.Text;
                         x.Sapd_model = item;
                         x.Sapd_ser_upload = 5;
                         x.Sapd_no_of_times = Convert.ToInt32(txtNoOfTimes.Text);
                     });


                    //update combine item details
                    //if have combine item
                    if (btnCombineItems.Enabled)
                    {
                        (from _res in _comList
                         where _res.Sapc_main_line == seq && _res.Sapc_main_itm_cd == item
                         select _res).ToList<PriceCombinedItemRef>().ForEach(x =>
                         {
                             x.Sapc_main_itm_cd = txtItemCode.Text;
                         });
                    }
                }


                dgvPriceDet.AutoGenerateColumns = false;
                dgvPriceDet.DataSource = new List<PriceDetailRef>();
                dgvPriceDet.DataSource = _list;

                //txtItemCode.Text = "";
                lblModel.Text = "";
                txtFromQty.Text = "";
                txtToQty.Text = "";
                txtItemPrice.Text = "";
                txtNoOfTimes.Text = "999";
                txtWaraRemarks.Text = "";
                chkCombine.Checked = false;
                txtItemCode.Focus();

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

        private void btnAddPromo_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtCircular.Text))
                {
                    MessageBox.Show("Please enter circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Text = "";
                    _isRecal = false;
                    btnMainSave.Enabled = true;
                    btnAmend.Enabled = false;
                    btnCancel.Enabled = false;
                    txtCircular.Focus();
                    return;
                }

                DataTable _tp = CHNLSVC.Sales.GetPriceTypeByCir(txtCircular.Text);

                if (_tp.Rows.Count >= 2)
                {
                    if (MessageBox.Show("This circular having multiple price type. Do you want to check type wise ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        pnlPriceTp.Visible = true;
                        dgvType.AutoGenerateColumns = false;
                        dgvType.DataSource = new DataTable();
                        dgvType.DataSource = _tp;
                        return;
                    }
                }

                lstPromoItem.Clear();
                DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtCircular.Text, txtPromoCode.Text, txtBook.Text, txtLevel.Text);

                //var _lst = dt.AsEnumerable().Select(X => X.Field<string>("esep_cat_cd")).Distinct().ToList();


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstPromoItem.Items.Add(drow["sapd_promo_cd"].ToString());
                    }

                    _isRecal = true;
                    btnCancel.Enabled = true;
                    //btnMainSave.Enabled = false;
                    cmbPriceType.Enabled = false;
                    chkEndDate.Text = "Change end Date";
                }
                else
                {
                    MessageBox.Show("Please check enter circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Text = "";
                    _isRecal = false;
                    btnMainSave.Enabled = true;
                    btnAmend.Enabled = false;
                    btnCancel.Enabled = false;
                    txtCircular.Focus();
                    return;
                }
                List<SAR_PB_CIREFFECT> _SAR_PB_CIREFFECT = new List<SAR_PB_CIREFFECT>();
                _SAR_PB_CIREFFECT = CHNLSVC.Sales.get_SAR_PB_CIREFFECT(txtCircular.Text, 1);
                if (_SAR_PB_CIREFFECT !=null)
                {
                    TXTBRANDMNGR.Text = _SAR_PB_CIREFFECT.FirstOrDefault().SPC_BRAND_MNGR;
                    txtexpectedvalue.Text = _SAR_PB_CIREFFECT.FirstOrDefault().spc_est_val.ToString();
                    txtexpectedqty.Text = _SAR_PB_CIREFFECT.FirstOrDefault().spc_est_qty.ToString();
                    pnl_mor_det.Visible = true;
                    this.pnl_mor_det.Size = new System.Drawing.Size(293, 115);
                    this.pnl_mor_det.Location = new System.Drawing.Point(36, 114);
                }
                //add by tharanga 2017/11/21
                DataTable sar_pb_det = new DataTable();

                sar_pb_det = CHNLSVC.MsgPortal.GetPricebyCircular(txtCircular.Text, null);
                if (sar_pb_det.Rows.Count > 0)
                {
                    grdcurdet.AutoGenerateColumns = false;
                    grdcurdet.DataSource = sar_pb_det;
                    pnlcirdet.Visible = true;
                    this.pnlcirdet.Size = new System.Drawing.Size(713, 187);
                    this.pnlcirdet.Location = new System.Drawing.Point(1, 300);

                    // Tharindu
                    string val = sar_pb_det.Rows[0]["SAPD_INFO_NW"].ToString();
                    if(val == "1")
                    {
                        chkIsinfromim.Checked = true;
                    }
                    else
                    {
                        chkIsinfromim.Checked = false;
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

        private void btnSelectAllPromo_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPromoItem.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnUnSelectAllPromo_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPromoItem.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            txtCircular.Text = "";
            txtPromoCode.Text = "";
            lstPromoItem.Clear();
        }

        private void cmbPriceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<PriceTypeRef> _list = CHNLSVC.Sales.GetAllPriceType(cmbPriceType.Text);
                foreach (PriceTypeRef _tmp in _list)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        btnCombineItems.Enabled = true;
                        chkMulti.Checked = false;
                        chkMulti.Visible = true;
                        chkCombine.Enabled = true;
                        chkCombine.Checked = false;
                        chkWithOutCombine.Visible = true;
                    }
                    else
                    {
                        btnCombineItems.Enabled = false;
                        chkMulti.Checked = false;
                        chkMulti.Visible = false;
                        chkCombine.Checked = false;
                        chkCombine.Enabled = false;
                        chkWithOutCombine.Visible = false;
                    }

                    if (_tmp.Sarpt_indi == 0)
                    {
                        btnAppPC.Enabled = false;
                    }
                    else
                    {
                        btnAppPC.Enabled = true;
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

        private void btnViewPromoDet_Click(object sender, EventArgs e)
        {
            try
            {
                btnMainSave.Enabled = true;
                Boolean _isValidItm = false;
                List<string> _item = new List<string>();

                dgvPriceDet.AutoGenerateColumns = false;
                dgvPriceDet.DataSource = new List<PriceDetailRef>();

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = new List<PriceCombinedItemRef>();

                dgvAppPC.AutoGenerateColumns = false;
                dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();

                foreach (ListViewItem Item in lstPromoItem.Items)
                {


                    if (Item.Checked == true)
                    {
                        _item.Add(Item.Text);
                        _isValidItm = true;

                    }
                }
                Int16 I = 0;

                if (_isValidItm == false)
                {
                    MessageBox.Show("Please select promotion code to get details.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //kapila 8/3/2017
                _CircSch = new List<Circular_Schemes>();
                lstSch.Clear();
                DataTable _dtPSCH = CHNLSVC.General.GetPBScheme(txtCircular.Text);
                foreach (DataRow r in _dtPSCH.Rows)
                {
                    lstSch.Items.Add(r["saps_sch"].ToString());
                }
                btnCommSchSelect_Click(null, null);

                txtRem.Text = "";
                chkInf.Checked = false;
                chkInf.Enabled = true;
                btnCrdCntl.Enabled = false;
                DataTable _dtPSCHR = CHNLSVC.General.GetPBSchemeRem(txtCircular.Text);
                if (_dtPSCHR.Rows.Count > 0)
                {
                    chkInf.Checked = true;
                    chkInf.Enabled = false;
                    btnCrdCntl.Enabled = true;
                    txtRem.Text = _dtPSCHR.Rows[0]["sapsr_rem"].ToString();
                }
                _list = new List<PriceDetailRef>();
                _appPcList = new List<PriceProfitCenterPromotion>();

                List<PriceCombinedItemRef> _tmpComList = new List<PriceCombinedItemRef>();
                _comList = new List<PriceCombinedItemRef>();

                foreach (string st in _item)
                {
                    _list.AddRange(CHNLSVC.Sales.GetPricebyCirandPromo(txtCircular.Text.Trim(), st));
                }

                foreach (PriceDetailRef _tmp in _list)
                {
                    txtBook.Text = _tmp.Sapd_pb_tp_cd;
                    txtLevel.Text = _tmp.Sapd_pbk_lvl_cd;
                    dtpFrom.Value = _tmp.Sapd_from_date.Date;
                    dtpTo.Value = _tmp.Sapd_to_date.Date;
                    txtPromoCode.Text = _tmp.Sapd_promo_cd;
                    cmbPriceType.SelectedValue = _tmp.Sapd_price_type;
                    txtCusCode.Text = _tmp.Sapd_customer_cd;
                    if (_tmp.Sapd_usr_ip == "IGNORE COMBINE")
                    {
                        chkWithOutCombine.Checked = true;
                    }
                    else
                    {
                        chkWithOutCombine.Checked = false;
                    }
                    //cmbPriceType.Enabled = false;

                    _tmpComList = new List<PriceCombinedItemRef>();
                    _tmpComList = CHNLSVC.Sales.GetPriceCombinedItemLine(_tmp.Sapd_pb_seq, _tmp.Sapd_seq_no, _tmp.Sapd_itm_cd, null);
                    _comList.AddRange(_tmpComList);

                    //Tharindu
                    chkIsinfromim.Checked = _tmp.Sapd_is_inform_immediatly == 1 ? true : false; 

                    List<PriceProfitCenterPromotion> _tmpAppPC = new List<PriceProfitCenterPromotion>();
                    _tmpAppPC.AddRange(CHNLSVC.Sales.GetAllocPromoPc(BaseCls.GlbUserComCode, _tmp.Sapd_promo_cd, _tmp.Sapd_pb_seq));
                    _appPcList.AddRange(_tmpAppPC);

                    if (_tmp.Sapd_price_stus == "A")
                    {
                        lblPAStatus.Text = "Active";
                    }
                    else if (_tmp.Sapd_price_stus == "P")
                    {
                        lblPAStatus.Text = "Pening Approval";

                        //kapila 11/4/2016
                        List<mst_itm_com_reorder> _ltsCost = new List<mst_itm_com_reorder>();
                        _ltsCost = CHNLSVC.General.GetReOrder(_tmp.Sapd_itm_cd);
                        if (_ltsCost == null)
                        {
                            _ltsCost = new List<mst_itm_com_reorder>();
                        }
                        foreach (mst_itm_com_reorder _tempr in _ltsCost)
                        {
                            if (_tempr.Icr_com_code == BaseCls.GlbUserComCode)
                            {
                                _tmp.Sapd_lst_cost = _tempr.Icr_max_cost;
                                _tmp.Sapd_avg_cost = _tempr.Icr_avg_cost;

                                if (_tmp.Sapd_avg_cost > 0)
                                {
                                    _tmp.Sapd_margin = (_tmp.Sapd_itm_price - _tmp.Sapd_avg_cost) / _tmp.Sapd_avg_cost * 100;
                                }
                                else
                                {
                                    _tmp.Sapd_margin = 100;
                                }
                            }
                        }

                    }
                }

                dgvPriceDet.AutoGenerateColumns = false;
                dgvPriceDet.DataSource = new List<PriceDetailRef>();
                dgvPriceDet.DataSource = _list;

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = new List<PriceCombinedItemRef>();
                dgvPromo.DataSource = _comList;

                dgvAppPC.AutoGenerateColumns = false;
                dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                dgvAppPC.DataSource = _appPcList;

                if (_appPcList != null)
                {
                    foreach (PriceProfitCenterPromotion _chk in _appPcList)
                    {
                        foreach (DataGridViewRow row in dgvAppPC.Rows)
                        {
                            string _pc = row.Cells["col_a_Pc"].Value.ToString();
                            string _promo = row.Cells["col_a_Promo"].Value.ToString();
                            Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                            if (_pc == _chk.Srpr_pc && _promo == _chk.Srpr_promo_cd && _pbSeq == _chk.Srpr_pbseq)
                            {
                                DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                                //if (Convert.ToBoolean(chk.Value) == false)
                                if (_chk.Srpr_act == 1)
                                {
                                    chk.Value = true;
                                    goto L1;
                                }
                                else
                                {
                                    chk.Value = false;
                                    goto L1;
                                }
                            }

                        }
                    L1: Int16 x = 1;
                    }
                }

                //give used message
                foreach (PriceDetailRef _ref in _list)
                {
                    if (_ref.Sapd_no_of_use_times > 0)
                    {
                        MessageBox.Show("Promotion Code - " + _ref.Sapd_promo_cd + " has used " + _ref.Sapd_no_of_use_times);
                    }
                }

                PriceDetailRestriction _tmpRes = new PriceDetailRestriction();
                foreach (PriceDetailRef _tmp in _list)
                {
                    _tmpRes = CHNLSVC.Sales.GetPromotionRestriction(BaseCls.GlbUserComCode, _tmp.Sapd_promo_cd);
                }

                chkCustomer.Checked = false;
                chkNIC.Checked = false;
                chkMobile.Checked = false;
                txtMessage.Text = "";
                chkPP.Checked = false;
                chkDL.Checked = false;

                if (_tmpRes != null)
                {
                    chkCustomer.Checked = _tmpRes.Spr_need_cus;
                    chkNIC.Checked = _tmpRes.Spr_need_nic;
                    chkMobile.Checked = _tmpRes.Spr_need_mob;
                    txtMessage.Text = _tmpRes.Spr_msg;
                    chkPP.Checked = _tmpRes.Spr_need_pp;
                    chkDL.Checked = _tmpRes.Spr_need_dl;
                }

                _isRecal = true;
                btnCancel.Enabled = true;
                btnAppPCUpdate.Enabled = true;
                btnMainSave.Enabled = false;
                btnSaveAs.Enabled = true;
                btnAddSI.Enabled = false;
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

        private void btnSearchSubItems_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubItem;
                _CommonSearch.ShowDialog();
                txtSubItem.Select();
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

        private void txtSubItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubItem;
                _CommonSearch.ShowDialog();
                txtSubItem.Select();
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

        private void txtSubItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchType = "ITEMS";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSubItem;
                    _CommonSearch.ShowDialog();
                    txtSubItem.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSubQty.Focus();
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

        private void txtSubQty_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSubQty.Text))
            {
                if (!IsNumeric(txtSubQty.Text))
                {
                    MessageBox.Show("Please enter correct qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubQty.Text = "";
                    txtSubQty.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSubQty.Text) < 0)
                {
                    MessageBox.Show("Qty cannot be less than zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubQty.Text = "";
                    txtSubQty.Focus();
                    return;
                }
            }
        }

        private void txtSubQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSubPrice.Focus();
            }
        }

        private void txtSubPrice_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSubPrice.Text))
            {
                if (!IsNumeric(txtSubPrice.Text))
                {
                    MessageBox.Show("Please enter correct price.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubPrice.Text = "";
                    txtSubPrice.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtSubPrice.Text) < 0)
                {
                    MessageBox.Show("Price cannot be less than zero.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubPrice.Text = "";
                    txtSubPrice.Focus();
                    return;
                }
                txtSubPrice.Text = Convert.ToDecimal(txtSubPrice.Text).ToString("n");

            }
        }

        private void txtSubPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubAdd.Focus();
            }
        }

        private void btnSubAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _subSeq = 0;
                if (string.IsNullOrEmpty(lblMainItem.Text))
                {
                    MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(lblSubModel.Text))
                {
                    MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(lblMainLine.Text))
                {
                    MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtSubItem.Text))
                {
                    MessageBox.Show("Please select sub item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubItem.Text = "";
                    txtSubItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSubQty.Text))
                {
                    MessageBox.Show("Please enter item qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubQty.Text = "";
                    txtSubQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtSubPrice.Text))
                {
                    MessageBox.Show("Please enter combine item price.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubPrice.Text = "";
                    txtSubPrice.Focus();
                    return;
                }

                if (_comList.Count > 0)
                {
                    var _tmp = (from _lst in _comList
                                where _lst.Sapc_main_itm_cd == lblMainItem.Text.Trim() && _lst.Sapc_main_line == Convert.ToInt16(lblMainLine.Text.Trim())
                                select (int?)_lst.Sapc_itm_line).Max();

                    _subSeq = Convert.ToInt32(_tmp);
                }
                if (MainLine <= 0 && SubLine <= 0)
                {
                    _subSeq = _subSeq + 1;

                    PriceCombinedItemRef _comItmRef = new PriceCombinedItemRef();
                    _comItmRef.Sapc_increse = false;
                    _comItmRef.Sapc_itm_cd = txtSubItem.Text.Trim();
                    _comItmRef.Sapc_itm_line = _subSeq;
                    _comItmRef.Sapc_main_itm_cd = lblMainItem.Text.Trim();
                    _comItmRef.Sapc_main_line = Convert.ToInt32(lblMainLine.Text);
                    _comItmRef.Sapc_main_ser = null;
                    _comItmRef.Sapc_pb_seq = Convert.ToInt32(lblPbSeq.Text);
                    _comItmRef.Sapc_price = Convert.ToDecimal(txtSubPrice.Text);
                    _comItmRef.Sapc_qty = Convert.ToDecimal(txtSubQty.Text);
                    _comItmRef.Sapc_sub_ser = null;
                    _comItmRef.Sapc_tot_com = false;
                    if (chkEndDate.Checked && btnAmend.Enabled)
                    {
                        _comItmRef.Sapc_sub_ser = "6";
                    }
                    _comList.Add(_comItmRef);
                }
                else
                {
                    //edit 
                    (from _res in _comList
                     where _res.Sapc_main_line == MainLine && _res.Sapc_itm_line == SubLine
                     select _res).ToList<PriceCombinedItemRef>().ForEach(x =>
                     {
                         x.Sapc_qty = Convert.ToDecimal(txtSubQty.Text);
                         x.Sapc_itm_cd = txtSubItem.Text;
                         x.Sapc_price = Convert.ToDecimal(txtSubPrice.Text);
                         x.Sapc_sub_ser = "5";
                     });
                }

                dgvPromo.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _comList;
                dgvPromo.DataSource = new List<PriceCombinedItemRef>();
                dgvPromo.DataSource = _source;

                txtSubItem.Text = "";
                txtSubQty.Text = "";
                txtSubPrice.Text = "";
                lblSubModel.Text = "";
                txtSubItem.Focus();

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

        private void txtSubItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSubItem.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtSubItem.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblSubModel.Text = "";
                        txtSubItem.Clear();
                        txtSubItem.Focus();

                    }
                    else
                    {
                        lblSubModel.Text = _itemdetail.Mi_model;
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

        private void btnSubOk_Click(object sender, EventArgs e)
        {
            txtSubItem.Text = "";
            txtSubQty.Text = "";
            txtSubPrice.Text = "";
            lblSubModel.Text = "";
            lblMainItem.Text = "";
            lblMainLine.Text = "";

            gbCombine.Visible = false;
        }

        private void btnSubClear_Click(object sender, EventArgs e)
        {
            txtSubItem.Text = "";
            txtSubQty.Text = "";
            txtSubPrice.Text = "";
            lblSubModel.Text = "";
            lblMainItem.Text = "";
            lblMainLine.Text = "";

            dgvPromo.AutoGenerateColumns = false;
            dgvPromo.DataSource = new List<PriceCombinedItemRef>();

            gbCombine.Visible = false;
        }

        private void btnCombineItems_Click(object sender, EventArgs e)
        {
            if (gbCombine.Visible == false)
            {
                gbCombine.Visible = true;
            }
            else
            {
                gbCombine.Visible = false;
            }
        }

        private void dgvPriceDet_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string _mainItm = string.Empty;
            Int32 _mainLine = 0;
            decimal _mainPrice = 0;
            Int32 _pbSeq = 0;
            if (e.RowIndex != -1)
            {
                int used_times = Convert.ToInt32(dgvPriceDet.Rows[e.RowIndex].Cells["used_times"].Value.ToString());

                if (used_times > 0)
                {
                    if (chkEndDate.Checked)
                    {
                        MessageBox.Show("Can not amend used items", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        canAmmend = false;
                        //btnAmend.Enabled = false;
                        //return;
                    }
                    else
                    {
                        MessageBox.Show("Can not amend used items", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        chkEndDate.Checked = false;
                        canAmmend = false;
                        //cmbPriceType.Enabled = false;
                        //chkEndDate.Enabled = false;
                    }
                }

                _mainItm = dgvPriceDet.Rows[e.RowIndex].Cells["col_p_Item"].Value.ToString();
                _mainLine = Convert.ToInt32(dgvPriceDet.Rows[e.RowIndex].Cells["col_p_Seq"].Value);
                _mainPrice = Convert.ToDecimal(dgvPriceDet.Rows[e.RowIndex].Cells["col_p_price"].Value);
                _pbSeq = Convert.ToInt32(dgvPriceDet.Rows[e.RowIndex].Cells["col_p_PbSeq"].Value);
                seq = Convert.ToInt32(dgvPriceDet.Rows[e.RowIndex].Cells["col_p_Seq"].Value);

                txtItemCode.Text = dgvPriceDet.Rows[e.RowIndex].Cells["col_p_Item"].Value.ToString();
                txtFromQty.Text = dgvPriceDet.Rows[e.RowIndex].Cells["col_p_frmQty"].Value.ToString();
                txtToQty.Text = dgvPriceDet.Rows[e.RowIndex].Cells["col_p_ToQty"].Value.ToString();
                txtItemPrice.Text = _mainPrice.ToString();
                txtWaraRemarks.Text = dgvPriceDet.Rows[e.RowIndex].Cells["col_p_WaraRmk"].Value.ToString();
                txtNoOfTimes.Text = dgvPriceDet.Rows[e.RowIndex].Cells["col_p_NoofTimes"].Value.ToString();


                lblMainItem.Text = _mainItm;
                lblMainLine.Text = _mainLine.ToString();
                lblPbSeq.Text = _pbSeq.ToString();
                lblPrice.Text = _mainPrice.ToString("n");

            }
        }

        private void btnMainSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtBook.Text))
                {
                    MessageBox.Show("Please select Price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBook.Text = "";
                    txtBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtLevel.Text))
                {
                    MessageBox.Show("Please select Price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLevel.Text = "";
                    txtLevel.Focus();
                    return;
                }

                if (dtpTo.Value.Date < dtpFrom.Value.Date)
                {
                    MessageBox.Show("To date cannot be less than from date.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpTo.Focus();
                    return;
                }

                //updated by akila 2018/02/08
                if (IsRestrictBackDatePriceUpload(dtpFrom.Value, dtpTo.Value))
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtCircular.Text))
                {
                    MessageBox.Show("Please enter circular #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Text = "";
                    txtCircular.Focus();
                    return;
                }

                List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(cmbPriceType.Text);
                foreach (PriceTypeRef _tmp in _type)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        if (chkWithOutCombine.Checked == true)
                        {
                            if (dgvPromo.Rows.Count <= 0)
                            {
                                if (MessageBox.Show("Confirm to continue without combine / free items ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Cannot continue, you select without combine but combine items are available.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                        }
                        else
                        {
                            if (dgvPromo.Rows.Count <= 0)
                            {
                                MessageBox.Show("No combine / free items are define.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                }

                //check permission
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11048))
                {
                    //chek for pb and plevel
                    if (!string.IsNullOrEmpty(txtLevel.Text))
                    {
                        List<PriceDefinitionUserRestriction> _resList = CHNLSVC.Sales.GetUserRestriction(BaseCls.GlbUserID, BaseCls.GlbUserComCode, DateTime.Now, txtBook.Text.Trim(), txtLevel.Text.Trim(), 2);
                        if (_resList == null || _resList.Count <= 0)
                        {
                            MessageBox.Show("User - " + BaseCls.GlbUserID + " not allowed to user Price Book - " + txtBook.Text + " and Price Level - " + txtLevel.Text, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtLevel.Text = "";
                            txtBook.Text = "";
                            txtBook.Focus();
                            return;
                        }
                    }
                }

                //kapila 8/3/2017
                List<Circular_Schemes> _CircSch = new List<Circular_Schemes>();
                if (chkInf.Checked)
                {
                    foreach (ListViewItem schList in lstSch.Items)
                    {
                        string sch = schList.Text;

                        if (schList.Checked == true)
                        {
                            Circular_Schemes _listS = new Circular_Schemes();
                            _listS.Circ = txtCircular.Text;
                            _listS.Circ_Sch = sch;
                            _CircSch.Add(_listS);
                        }
                    }
                }
                List<PriceDetailRef> _saveDetail = new List<PriceDetailRef>();
                List<PriceCombinedItemRef> _saveComDet = new List<PriceCombinedItemRef>();

                //Tharindu
                int val = 0;
                if (chkIsinfromim.Checked)
                {
                    val = 1;
                }

                foreach (PriceDetailRef _tmpPriceDet in _list)
                {
                    PriceDetailRef _tmpPriceList = new PriceDetailRef();
                    _tmpPriceList = _tmpPriceDet;
                    _tmpPriceList.Sapd_customer_cd = txtCusCode.Text;
                    _tmpPriceList.Sapd_from_date = dtpFrom.Value.Date;
                    _tmpPriceList.Sapd_to_date = dtpTo.Value.Date;
                    _tmpPriceList.Sapd_circular_no = txtCircular.Text;
                    _tmpPriceList.Sapd_pb_tp_cd = txtBook.Text;
                    _tmpPriceList.Sapd_pbk_lvl_cd = txtLevel.Text;
                    _tmpPriceList.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                    _tmpPriceList.Sapd_erp_ref = txtLevel.Text;

                    // Tharindu
                    _tmpPriceList.Sapd_is_inform_immediatly = val; 
                    if (chkWithOutCombine.Checked == true)
                    {
                        _tmpPriceList.Sapd_usr_ip = "IGNORE COMBINE";
                    }
                    else
                    {

                    }
                    _tmpPriceList.Sapd_price_stus = "P";
                    _saveDetail.Add(_tmpPriceList);
                }

                foreach (PriceCombinedItemRef _tmpComList in _comList)
                {
                    _tmpComList.Sapc_increse = chkMulti.Checked;
                    _saveComDet.Add(_tmpComList);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();

                if (cmbPriceType.Text != "NORMAL")
                {
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "PRO";
                    masterAuto.Aut_year = null;
                }
                else
                {
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "NOR";
                    masterAuto.Aut_year = null;
                }

                List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();

                //_savePcAllocList = _appPcList;
                foreach (DataGridViewRow row in dgvAppPC.Rows)
                {
                    string _pc = row.Cells["col_a_Pc"].Value.ToString();
                    string _promo = row.Cells["col_a_Promo"].Value.ToString();
                    Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                    Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                    if (_active == 1)
                    {
                        foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                        {
                            if (_tmp.Srpr_com == BaseCls.GlbUserComCode && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                            {
                                //PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                                //_tmpList.Srpr_act = _active;
                                //_tmpList.Srpr_com = BaseCls.GlbUserComCode;
                                //_tmpList.Srpr_cre_by = BaseCls.GlbUserID;
                                //_tmpList.Srpr_mod_by = BaseCls.GlbUserID;
                                //_tmpList.Srpr_pbseq = _pbSeq;
                                //_tmpList.Srpr_pc = _pc;
                                //_tmpList.Srpr_promo_cd = _promo;
                                _tmp.Srpr_act = _active;
                                _savePcAllocList.Add(_tmp);
                            }

                        }
                    }

                }

                PriceDetailRestriction _priceRes = new PriceDetailRestriction();
                // _priceRes = null;
                if (_isRestrict == true)
                {
                    _priceRes.Spr_com = BaseCls.GlbUserComCode;
                    _priceRes.Spr_msg = txtMessage.Text;
                    _priceRes.Spr_need_cus = chkCustomer.Checked;
                    _priceRes.Spr_need_nic = chkNIC.Checked;
                    _priceRes.Spr_need_pp = false;
                    _priceRes.Spr_need_dl = false;
                    _priceRes.Spr_usr = BaseCls.GlbUserID;
                    _priceRes.Spr_when = DateTime.Now;
                    _priceRes.Spr_promo = "";
                }
                else
                {
                    _priceRes = null;
                }
                if (true)
                {

                } 
                //add by tharanga 2017/11/21
                SAR_PB_CIREFFECT _SAR_PB_CIREFFECT=new SAR_PB_CIREFFECT();
                if (string.IsNullOrEmpty(txtexpectedqty.Text) && string.IsNullOrEmpty(txtexpectedvalue.Text) && string.IsNullOrEmpty(TXTBRANDMNGR.Text))
                {
                    _SAR_PB_CIREFFECT = null;
                }
                else
                {
                    _SAR_PB_CIREFFECT.spc_circular = txtCircular.Text;
                    _SAR_PB_CIREFFECT.spc_cre_by = BaseCls.GlbUserID;
                    _SAR_PB_CIREFFECT.spc_est_qty = string.IsNullOrEmpty(txtexpectedqty.Text) ? 0 : Convert.ToInt32(txtexpectedqty.Text.ToString());
                    _SAR_PB_CIREFFECT.spc_est_val = string.IsNullOrEmpty(txtexpectedvalue.Text) ? 0 : Convert.ToInt32(txtexpectedvalue.Text.ToString());
                    _SAR_PB_CIREFFECT.spc_mod_by = BaseCls.GlbUserID;
                    _SAR_PB_CIREFFECT.spc_act = 1;
                    _SAR_PB_CIREFFECT.SPC_BRAND_MNGR = TXTBRANDMNGR.Text;
                    _SAR_PB_CIREFFECT.spc_session = BaseCls.GlbUserSessionID;
                }

                string _err = "";
                row_aff = (Int32)CHNLSVC.Sales.SavePriceDetails(_saveDetail, _comList, masterAuto, _savePcAllocList, _serial, _priceRes, out _err, BaseCls.GlbUserSessionID, BaseCls.GlbUserID, BaseCls.GlbUserComCode, _CircSch, Convert.ToInt32(chkInf.Checked), txtRem.Text, _SAR_PB_CIREFFECT);

                if (row_aff == 1)
                {
                    MessageBox.Show("Price definition created successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_err))
                    {
                        MessageBox.Show("Error Occurred while processing!!!\n" + _err, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void sendMail(string _circNo)
        {
            string outMsg;
            DataTable _dt = null;
            DataTable _dt1 = null;
            string _promoCirc = "";
            DataTable param = new DataTable();
            DataRow dr;
            Boolean _rowExist = false;
            MasterProfitCenter _masterPC = new MasterProfitCenter();
            DataTable _dtCirc = new DataTable();
            DataTable _dtPSCH = new DataTable();
            Service_Message_Template oTemplate = new Service_Message_Template();

            param.Clear();
            param.Columns.Add("SR", typeof(string));

            #region send mail to AST WH
            if (BaseCls.GlbUserComCode == "ABL" && BaseCls.GlbDefChannel == "ABT" && (BaseCls.GlbDefSubChannel == "ABS" || BaseCls.GlbDefSubChannel == "SKE" || BaseCls.GlbDefSubChannel == "TFS"))    //kapila 17/2/2017
            {
                string _mail = "";

                List<string> list = new List<string>();
                list.Add("lasanthaa@abansgroup.com");
                list.Add("amalr@abansgroup.com");

                _dtCirc = CHNLSVC.Sales.GetCircularNo(_circNo);
                if (_dtCirc.Rows.Count > 0)
                {

                    _mail += "New Price Circular has been Released" + Environment.NewLine + Environment.NewLine;
                    _mail += "Circular # - " + _circNo + "" + Environment.NewLine;
                    _mail += "Valid From - " + Convert.ToDateTime(_dtCirc.Rows[0]["Sapd_from_date"]).ToShortDateString() + "" + Environment.NewLine;
                    _mail += "Valid To - " + Convert.ToDateTime(_dtCirc.Rows[0]["Sapd_to_date"]).ToShortDateString() + "" + Environment.NewLine + Environment.NewLine;

                    _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;

                    for (int i = 0; i < list.Count; i++)
                    {
                        CHNLSVC.CommonSearch.Send_SMTPMail(list[i], "New Price Circular", _mail);
                    }
                }
            }
            #endregion
            #region send mail to credit dept
            if (BaseCls.GlbUserComCode == "ABL")
            {
                if (chkInf.Checked)
                {
                    string _mail = "";

                    List<string> list = new List<string>();
                    list.Add("chaminda@abansgroup.com");
                    list.Add("charana@abansgroup.com");
                    list.Add("dulanja@abansgroup.com");
                    list.Add("poorna@abansgroup.com");
                    list.Add("dulan@abansgroup.com");
                    list.Add("thusithas@abansgroup.com");

                    _dtCirc = CHNLSVC.General.GetPBSchemeRem(_circNo);
                    if (_dtCirc.Rows.Count > 0)
                    {
                        _mail += "New Price Circular has been Released" + Environment.NewLine + Environment.NewLine;
                        _mail += "Circular # - " + _circNo + "" + Environment.NewLine;
                        _mail += "Schemes :" + Environment.NewLine;
                        _mail += "----------" + Environment.NewLine;

                        _dtPSCH = CHNLSVC.General.GetPBScheme(_circNo);
                        foreach (DataRow r in _dtPSCH.Rows)
                        {
                            _mail += r["saps_sch"].ToString() + Environment.NewLine;

                        }
                        _mail += "Remarks :" + Environment.NewLine;
                        _mail += _dtCirc.Rows[0]["sapsr_rem"].ToString() + Environment.NewLine;


                        _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;

                        for (int i = 0; i < list.Count; i++)
                        {
                            CHNLSVC.CommonSearch.Send_SMTPMail(list[i], "New Price Circular", _mail);
                        }
                    }
                }
            }
            #endregion


            //--------------------------------------------------------------------
            #region template 10 send mail to showroom
            Int32 _pricTp = 0;
            string _promoCd = "";
            oTemplate = CHNLSVC.CustService.GetMessageTemplates_byID(BaseCls.GlbUserComCode, null, 10);


            if (oTemplate != null && oTemplate.Sml_templ_mail != null)
            {
                string emailBody = oTemplate.Sml_templ_mail;

                _dtCirc = CHNLSVC.Sales.GetCircularNo(_circNo);
                if (_dtCirc.Rows.Count > 0)
                {
                    _promoCirc = _circNo;
                    emailBody = emailBody.Replace("[circularno]", _circNo);
                    emailBody = emailBody.Replace("[validfrom]", _dtCirc.Rows[0]["Sapd_from_date"].ToString());
                    emailBody = emailBody.Replace("[validto]", _dtCirc.Rows[0]["Sapd_to_date"].ToString());
                    _pricTp = Convert.ToInt32(_dtCirc.Rows[0]["sapd_price_type"]);
                    _promoCd = _dtCirc.Rows[0]["sapd_promo_cd"].ToString();
                }

                _rowExist = false;
                param.Clear();

                #region price type 0
                if (_pricTp == 0)
                {
                    _dt = CHNLSVC.Sales.GetMailPBLevels(_circNo);
                    foreach (DataRow r in _dt.Rows)     //price book and price levels
                    {
                        _dt1 = CHNLSVC.Sales.GetMailLocations(r["sapd_pb_tp_cd"].ToString(), r["sapd_pbk_lvl_cd"].ToString());
                        foreach (DataRow r1 in _dt1.Rows)   //location list
                        {
                            foreach (DataRow drow in param.Rows)
                            {
                                if (drow["SR"].ToString() == r1["sadd_pc"].ToString())
                                {
                                    _rowExist = true;
                                    break;
                                }
                            }
                            if (_rowExist == false)
                            {
                                dr = param.NewRow();
                                dr["SR"] = r1["sadd_pc"].ToString();
                                param.Rows.Add(dr);
                                _rowExist = false;
                            }
                        }
                    }

                    foreach (DataRow drowloc in param.Rows)
                    {
                        _masterPC = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, drowloc["SR"].ToString());
                        if (_masterPC != null)
                        {
                            Service_Message oMessage = new Service_Message();
                            oMessage.Sm_email = _masterPC.Mpc_email;

                            oMessage.Sm_com = BaseCls.GlbUserComCode;
                            oMessage.Sm_jobno = _promoCirc;
                            oMessage.Sm_joboline = 1;
                            oMessage.Sm_jobstage = 0;
                            oMessage.Sm_ref_num = string.Empty;
                            oMessage.Sm_status = 0;
                            oMessage.Sm_msg_tmlt_id = 10;
                            oMessage.Sm_sms_text = string.Empty;
                            oMessage.Sm_sms_gap = 0;
                            oMessage.Sm_sms_done = 0;
                            oMessage.Sm_mail_text = emailBody;
                            oMessage.Sm_mail_gap = 0;
                            oMessage.Sm_email_done = 0;
                            oMessage.Sm_cre_by = BaseCls.GlbUserID;
                            oMessage.Sm_cre_dt = DateTime.Now;
                            oMessage.Sm_mod_by = BaseCls.GlbUserID;
                            oMessage.Sm_mod_dt = DateTime.Now;

                           // int result = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                        }
                    }
                }
                #endregion
                else
                {
                    DataTable _dtPC = CHNLSVC.Sales.GetProfitCenterDetail(BaseCls.GlbUserComCode, _pricTp, "", "", _promoCd);
                    foreach (DataRow r in _dtPC.Rows)
                    {
                        Service_Message oMessage = new Service_Message();
                        oMessage.Sm_email = r["Mpc_email"].ToString();

                        oMessage.Sm_com = BaseCls.GlbUserComCode;
                        oMessage.Sm_jobno = _promoCirc;
                        oMessage.Sm_joboline = 1;
                        oMessage.Sm_jobstage = 0;
                        oMessage.Sm_ref_num = string.Empty;
                        oMessage.Sm_status = 0;
                        oMessage.Sm_msg_tmlt_id = 10;
                        oMessage.Sm_sms_text = string.Empty;
                        oMessage.Sm_sms_gap = 0;
                        oMessage.Sm_sms_done = 0;
                        oMessage.Sm_mail_text = emailBody;
                        oMessage.Sm_mail_gap = 0;
                        oMessage.Sm_email_done = 0;
                        oMessage.Sm_cre_by = BaseCls.GlbUserID;
                        oMessage.Sm_cre_dt = DateTime.Now;
                        oMessage.Sm_mod_by = BaseCls.GlbUserID;
                        oMessage.Sm_mod_dt = DateTime.Now;

                        int result = CHNLSVC.CustService.SaveServiceMsg(oMessage, out outMsg);
                    }
                }
            }
            #endregion
        }

        private void clearSI()
        {
            gvSI.AutoGenerateColumns = false;
            gvSI.DataSource = null;
            gvSIItems.AutoGenerateColumns = false;
            gvSIItems.DataSource = null;
            txtSrchCat1.Text = "";
            txtSrchCat2.Text = "";
            txtSrchItm.Text = "";
            txtSrchModel.Text = "";
            txtSrchSI.Text = "";

        }
        private void btnAppPCUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();


                foreach (DataGridViewRow row in dgvAppPC.Rows)
                {
                    string _pc = row.Cells["col_a_Pc"].Value.ToString();
                    string _promo = row.Cells["col_a_Promo"].Value.ToString();
                    Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                    Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);
                    string _pty_type = row.Cells["SRPR_PTY_TP"].Value.ToString();

                    foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                    {
                        if (_tmp.Srpr_com == BaseCls.GlbUserComCode && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                        {
                            PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                            _tmpList.Srpr_act = _active;
                            _tmpList.Srpr_com = BaseCls.GlbUserComCode;
                            _tmpList.Srpr_cre_by = BaseCls.GlbUserID;
                            _tmpList.Srpr_mod_by = BaseCls.GlbUserID;
                            _tmpList.Srpr_pbseq = _pbSeq;
                            _tmpList.Srpr_pc = _pc;
                            _tmpList.Srpr_promo_cd = _promo;
                            _tmpList.Srpr_pty_tp = _pty_type;   //kapila 22/2/2017
                            _savePcAllocList.Add(_tmpList);
                        }

                    }

                }
                DataTable _dt = _savePcAllocList.ToDataTable<PriceProfitCenterPromotion>();
                string _error;
                _dt.TableName = "aaa";
                row_aff = CHNLSVC.Sales.SaveAppPromoPcDataTable(_dt, out _error);

                if (row_aff == 1)
                {

                    MessageBox.Show("Profit center allocated successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show("Error occured while processing!!!\n" + _error, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvAppPC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgvAppPC.Rows[dgvAppPC.CurrentRow.Index].Cells[2];

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

        private void btnAppPC_Click(object sender, EventArgs e)
        {
            if (gbAppPc.Visible == true)
            {
                gbAppPc.Visible = false;
            }
            else
            {
                gbAppPc.Visible = true;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                _Ltype = "";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                    _Ltype = "";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                _Ltype = "";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchPC_Click(object sender, EventArgs e)
        {
            try
            {
                _Ltype = "";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _Ltype = "";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _Ltype = "";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            try
            {
                //lstPC.Clear();
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();
        }

        private void btnAppPcClear_Click(object sender, EventArgs e)
        {
            dgvAppPC.AutoGenerateColumns = false;
            dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
            _appPcList = new List<PriceProfitCenterPromotion>();
            lstPC.Clear();


            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            txtChanel.Focus();

        }

        private void btnAppPcApply_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isPCFound = false;
                if (optPC.Checked)      //kapila 23/12/2016
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
                if (optPC.Checked)      //kapila 23/12/2016
                    if (_isPCFound == false)
                    {
                        MessageBox.Show("No any applicable profit centers are selected.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                if (_isRecal == true)
                {
                    Boolean _isPromoFound = false;
                    foreach (ListViewItem Item in lstPromoItem.Items)
                    {
                        string promoCD = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isPromoFound = true;
                            goto L2;
                        }
                    }
                L2:

                    if (_isPromoFound == false)
                    {
                        MessageBox.Show("No any applicable promotions are selected.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Int32 _listPbSeq = 0;
                    foreach (ListViewItem promoList in lstPromoItem.Items)
                    {
                        string promo = promoList.Text;

                        if (promoList.Checked == true)
                        {
                            List<PriceDetailRef> _tmpdetbyPromo = new List<PriceDetailRef>();

                            _tmpdetbyPromo = CHNLSVC.Sales.GetPriceByPromoCD(promo);
                            _listPbSeq = 0;
                            foreach (PriceDetailRef _tmpList in _tmpdetbyPromo)
                            {
                                _listPbSeq = _tmpList.Sapd_pb_seq;
                            }

                            if (optPC.Checked)      //kapila 23/12/2016
                            {
                                foreach (ListViewItem pcList in lstPC.Items)
                                {
                                    string pc = pcList.Text;

                                    if (pcList.Checked == true)
                                    {
                                        PriceProfitCenterPromotion _tmpAppPc = new PriceProfitCenterPromotion();
                                        _tmpAppPc.Srpr_act = 1;
                                        _tmpAppPc.Srpr_com = BaseCls.GlbUserComCode;
                                        _tmpAppPc.Srpr_cre_by = BaseCls.GlbUserID;
                                        _tmpAppPc.Srpr_mod_by = BaseCls.GlbUserID;
                                        _tmpAppPc.Srpr_pbseq = _listPbSeq;
                                        _tmpAppPc.Srpr_pc = pc;
                                        _tmpAppPc.Srpr_promo_cd = promo;
                                        _tmpAppPc.Srpr_pty_tp = "PC";
                                        _appPcList.Add(_tmpAppPc);
                                    }
                                }
                            }
                            else
                            {
                                PriceProfitCenterPromotion _tmpAppPc = new PriceProfitCenterPromotion();
                                _tmpAppPc.Srpr_act = 1;
                                _tmpAppPc.Srpr_com = BaseCls.GlbUserComCode;
                                _tmpAppPc.Srpr_cre_by = BaseCls.GlbUserID;
                                _tmpAppPc.Srpr_mod_by = BaseCls.GlbUserID;
                                _tmpAppPc.Srpr_pbseq = _listPbSeq;
                                _tmpAppPc.Srpr_promo_cd = promo;
                                if (optChnl.Checked)
                                {
                                    _tmpAppPc.Srpr_pc = txtChanel.Text;
                                    _tmpAppPc.Srpr_pty_tp = "CHNL";
                                }
                                else
                                {
                                    _tmpAppPc.Srpr_pc = txtSChanel.Text;
                                    _tmpAppPc.Srpr_pty_tp = "SCHNL";
                                }
                                _appPcList.Add(_tmpAppPc);
                            }
                        }
                    }

                    lstPC.Clear();

                    dgvAppPC.AutoGenerateColumns = false;
                    dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                    dgvAppPC.DataSource = _appPcList;

                    if (_appPcList != null)
                    {
                        foreach (DataGridViewRow row in dgvAppPC.Rows)
                        {
                            DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                            chk.Value = true;
                        }
                    }
                }
                else    //not recall
                {
                    if (optPC.Checked)      //kapila 23/12/2016
                    {
                        foreach (ListViewItem pcList in lstPC.Items)
                        {
                            string pc = pcList.Text;

                            if (pcList.Checked == true)
                            {

                                PriceProfitCenterPromotion _tmpAppPc = new PriceProfitCenterPromotion();
                                _tmpAppPc.Srpr_act = 1;
                                _tmpAppPc.Srpr_com = BaseCls.GlbUserComCode;
                                _tmpAppPc.Srpr_cre_by = BaseCls.GlbUserID;
                                _tmpAppPc.Srpr_mod_by = BaseCls.GlbUserID;
                                _tmpAppPc.Srpr_pbseq = 0;
                                _tmpAppPc.Srpr_pc = pc;
                                _tmpAppPc.Srpr_promo_cd = "N/A";
                                _tmpAppPc.Srpr_pty_tp = "PC";
                                _appPcList.Add(_tmpAppPc);

                            }
                        }
                    }
                    else
                    {
                        PriceProfitCenterPromotion _tmpAppPc = new PriceProfitCenterPromotion();
                        _tmpAppPc.Srpr_act = 1;
                        _tmpAppPc.Srpr_com = BaseCls.GlbUserComCode;
                        _tmpAppPc.Srpr_cre_by = BaseCls.GlbUserID;
                        _tmpAppPc.Srpr_mod_by = BaseCls.GlbUserID;
                        _tmpAppPc.Srpr_pbseq = 0;
                        _tmpAppPc.Srpr_promo_cd = "N/A";
                        if (optChnl.Checked)      //kapila 23/12/2016
                        {
                            _tmpAppPc.Srpr_pty_tp = "CHNL";
                            _tmpAppPc.Srpr_pc = txtChanel.Text;
                        }
                        else
                        {
                            _tmpAppPc.Srpr_pty_tp = "SCHNL";
                            _tmpAppPc.Srpr_pc = txtSChanel.Text;
                        }

                        _appPcList.Add(_tmpAppPc);
                    }


                    lstPC.Clear();

                    dgvAppPC.AutoGenerateColumns = false;
                    dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                    dgvAppPC.DataSource = _appPcList;

                    foreach (DataGridViewRow row in dgvAppPC.Rows)
                    {

                        {
                            DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                            //if (Convert.ToBoolean(chk.Value) == false)
                            chk.Value = true;

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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPromoCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPromoCode.Text))
                {
                    btnMainSave.Enabled = true;
                    //Boolean _isValidItm = false;
                    //string _item = string.Empty;

                    dgvPriceDet.AutoGenerateColumns = false;
                    dgvPriceDet.DataSource = new List<PriceDetailRef>();

                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceCombinedItemRef>();

                    dgvAppPC.AutoGenerateColumns = false;
                    dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();

                    //    foreach (ListViewItem Item in lstPromoItem.Items)
                    //    {
                    //        _item = Item.Text;

                    //        if (Item.Checked == true)
                    //        {
                    //            _isValidItm = true;
                    //            goto L3;
                    //        }
                    //    }
                    //L3: Int16 I = 0;

                    //    if (_isValidItm == false)
                    //    {
                    //        MessageBox.Show("Please select promotion code to get details.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    _list = new List<PriceDetailRef>();
                    _appPcList = new List<PriceProfitCenterPromotion>();

                    List<PriceCombinedItemRef> _tmpComList = new List<PriceCombinedItemRef>();
                    _comList = new List<PriceCombinedItemRef>();

                    _list = CHNLSVC.Sales.GetPriceByPromoCD(txtPromoCode.Text.Trim());

                    if (_list == null)
                    {
                        MessageBox.Show("Invalid promo code.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    foreach (PriceDetailRef _tmp in _list)
                    {
                        txtBook.Text = _tmp.Sapd_pb_tp_cd;
                        txtLevel.Text = _tmp.Sapd_pbk_lvl_cd;
                        dtpFrom.Value = _tmp.Sapd_from_date.Date;
                        dtpTo.Value = _tmp.Sapd_to_date.Date;
                        txtPromoCode.Text = _tmp.Sapd_promo_cd;
                        cmbPriceType.SelectedValue = _tmp.Sapd_price_type;
                        txtCircular.Text = _tmp.Sapd_circular_no;

                        _tmpComList = new List<PriceCombinedItemRef>();
                        _tmpComList = CHNLSVC.Sales.GetPriceCombinedItemLine(_tmp.Sapd_pb_seq, _tmp.Sapd_seq_no, _tmp.Sapd_itm_cd, null);
                        _comList.AddRange(_tmpComList);


                        List<PriceProfitCenterPromotion> _tmpAppPC = new List<PriceProfitCenterPromotion>();
                        _tmpAppPC = CHNLSVC.Sales.GetAllocPromoPc(BaseCls.GlbUserComCode, txtPromoCode.Text.Trim(), _tmp.Sapd_pb_seq);
                        _appPcList.AddRange(_tmpAppPC);

                    }

                    dgvPriceDet.AutoGenerateColumns = false;
                    dgvPriceDet.DataSource = new List<PriceDetailRef>();
                    dgvPriceDet.DataSource = _list;

                    dgvPromo.AutoGenerateColumns = false;
                    dgvPromo.DataSource = new List<PriceCombinedItemRef>();
                    dgvPromo.DataSource = _comList;

                    dgvAppPC.AutoGenerateColumns = false;
                    dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                    dgvAppPC.DataSource = _appPcList;

                    if (_appPcList != null)
                    {
                        foreach (PriceProfitCenterPromotion _chk in _appPcList)
                        {
                            foreach (DataGridViewRow row in dgvAppPC.Rows)
                            {
                                string _pc = row.Cells["col_a_Pc"].Value.ToString();
                                string _promo = row.Cells["col_a_Promo"].Value.ToString();
                                Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                                if (_pc == _chk.Srpr_pc && _promo == _chk.Srpr_promo_cd && _pbSeq == _chk.Srpr_pbseq)
                                {
                                    DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                                    //if (Convert.ToBoolean(chk.Value) == false)
                                    if (_chk.Srpr_act == 1)
                                    {
                                        chk.Value = true;
                                        goto L1;
                                    }
                                    else
                                    {
                                        chk.Value = false;
                                        goto L1;
                                    }
                                }

                            }
                        L1: Int16 x = 1;
                        }
                    }
                    _isRecal = true;
                    btnAppPCUpdate.Enabled = true;
                    btnMainSave.Enabled = false;
                    btnSaveAs.Enabled = true;
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

        private void btnAppPcOk_Click(object sender, EventArgs e)
        {
            gbAppPc.Visible = false;
        }

        private void btnSearchMainItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMainItem;
                _CommonSearch.ShowDialog();
                txtMainItem.Select();
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

        private void txtMainItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMainItem;
                _CommonSearch.ShowDialog();
                txtMainItem.Select();
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

        private void txtMainItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchType = "ITEMS";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtMainItem;
                    _CommonSearch.ShowDialog();
                    txtMainItem.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    dtpSFrom.Focus();
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

        private void txtMainItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMainItem.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtMainItem.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblMainModel.Text = "";
                        lblMainDesc.Text = "";
                        txtMainItem.Clear();
                        txtMainItem.Focus();

                    }
                    else
                    {
                        lblMainModel.Text = _itemdetail.Mi_model;
                        lblMainDesc.Text = _itemdetail.Mi_shortdesc;
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

        private void dtpSFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpSTo.Focus();
            }
        }

        private void dtpSTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInvoiceNo.Focus();
            }
        }

        private void txtInvoiceNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    if (string.IsNullOrEmpty(txtMainItem.Text))
                    {
                        MessageBox.Show("Please select item.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoiceNo.Text = "";
                        txtMainItem.Focus();
                        return;
                    }

                    InvoiceHeader _tmpInv = new InvoiceHeader();
                    _tmpInv = CHNLSVC.Sales.GetInvoiceHdrByCom(BaseCls.GlbUserComCode, txtInvoiceNo.Text.Trim());

                    if (_tmpInv.Sah_inv_no == null)
                    {
                        MessageBox.Show("Invalid invoice #", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoiceNo.Text = "";
                        txtInvoiceNo.Focus();
                        return;
                    }

                    InvoiceItem _tmpInvItm = new InvoiceItem();
                    _tmpInvItm = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoiceNo.Text.Trim(), txtMainItem.Text.Trim());

                    if (_tmpInvItm == null)
                    {
                        MessageBox.Show("No such item found on this invoice", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoiceNo.Text = "";
                        txtInvoiceNo.Focus();
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

        private void btnSearchMainCate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMainCate_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtMainCate;
                    _CommonSearch.ShowDialog();
                    txtMainCate.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSubCate.Focus();
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

        private void btnSearchSubCate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSubCate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSubCate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
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
                    txtItemRange.Focus();
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

        private void btnSearchRange_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItemRange_DoubleClick(object sender, EventArgs e)
        {
            try
            {

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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItemRange_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtItemRange;
                    _CommonSearch.ShowDialog();
                    txtItemRange.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtBrand.Focus();
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

        private void btnSearchBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtBrand;
                    _CommonSearch.ShowDialog();
                    txtBrand.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtItem.Focus();
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

        private void txtBrand_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrand.Text))
                {
                    MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(txtBrand.Text.Trim());

                    if (_brd.Mb_cd == null)
                    {
                        MessageBox.Show("Please select the valid brand.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                    MessageBox.Show("Please select the valid main category code", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                        MessageBox.Show("Please select the valid category code", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Please select the valid category code", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchSimilarItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchType = "ITEMS";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
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
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                    _CommonSearch.SearchType = "ITEMS";
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
                    btnLoadProducts.Focus();
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

        private void btnLoadProducts_Click(object sender, EventArgs e)
        {
            try
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
                   
                    foreach (ListViewItem Item in txtVoucherValue.Items)
                    {
                      
                       if (Item.Text == txtItem.Text.Trim())
                       {
                           MessageBox.Show("Already added this voucher", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           txtItem.Text = "";
                           txtItem.Focus();
                           return;
                       }

                    }
                    txtVoucherValue.Items.Add(txtItem.Text.Trim());
                    //Add by akila 2017/12/29
                    MasterItem _itemMaster = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim().ToUpper());
                    if (_itemMaster != null && _itemMaster.Mi_itm_tp == "G")//check whether item is a GIFT VOUCHER
                    {
                        DisplayVoucherParameterPnl();
                }
                }
                else
                {

                    txtVoucherValue.Clear();
                    List<MasterItem> _tmpItem = CHNLSVC.Sales.GetItemsByCateAndBrand(txtMainCate.Text, txtSubCate.Text, txtItemRange.Text, txtBrand.Text, BaseCls.GlbUserComCode);
                    foreach (MasterItem _temp in _tmpItem)
                    {
                        txtVoucherValue.Items.Add(_temp.Mi_cd);
                    }

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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSelectAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in txtVoucherValue.Items)
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnUnselectItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in txtVoucherValue.Items)
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            txtVoucherValue.Clear();
            txtMainCate.Text = "";
            txtSubCate.Text = "";
            txtItemRange.Text = "";
            txtBrand.Text = "";
            txtItem.Text = "";
            txtMainCate.Focus();

            rbdDiscValue.Checked = false;
            rbdDiscountRate.Checked = false;
            pnlDiscount.Visible = false;
            pnlDiscType.Visible = false;
            pnlVoucherValue.Visible = false;
            txtVoucherDiscount.Text = string.Empty;
            txtVouValue.Text = string.Empty;
            pnlVoucherValue.Visible = false;
            pnlDiscType.Visible = false;
            pnlDiscount.Visible = false;
            txtVoucherDiscount.Text = string.Empty;
            txtVoucherValue.Text = string.Empty;
            pnlValidPeriod.Visible = false;
            txtValidPeriod.Text = string.Empty;
            pnlVoucherParameters.Visible = false;

            cmbParameterType.Focus();
            cmbParameterType.Select();
            cmbParameterType.SelectedIndex = -1;
        }

        private void btnSearchSChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChannel;
                _CommonSearch.ShowDialog();
                txtSChannel.Select();
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

        private void txtSChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChannel;
                _CommonSearch.ShowDialog();
                txtSChannel.Select();
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

        private void txtSChannel_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtSChannel;
                    _CommonSearch.ShowDialog();
                    txtSChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSSubChannel.Focus();
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

        private void btnSearchSSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChannel.Text = "";
                    txtSChannel.Focus();
                    return;
                }

                _Ltype = "Similar";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSSubChannel;
                _CommonSearch.ShowDialog();
                txtSSubChannel.Select();
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

        private void txtSSubChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChannel.Text = "";
                    txtSChannel.Focus();
                    return;
                }

                _Ltype = "Similar";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSSubChannel;
                _CommonSearch.ShowDialog();
                txtSSubChannel.Select();
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

        private void txtSSubChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtSChannel.Text))
                    {
                        MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSChannel.Text = "";
                        txtSChannel.Focus();
                        return;
                    }
                    _Ltype = "Similar";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSSubChannel;
                    _CommonSearch.ShowDialog();
                    txtSSubChannel.Select();
                }

                else if (e.KeyCode == Keys.Enter)
                {
                    txtSPc.Focus();
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

        private void btnSearchSPc_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _Ltype = "Similar";
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSPc;
                _CommonSearch.ShowDialog();
                txtSPc.Select();

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

        private void txtSPc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _Ltype = "Similar";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSPc;
                _CommonSearch.ShowDialog();
                txtSPc.Select();

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

        private void txtSPc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    _Ltype = "Similar";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSPc;
                    _CommonSearch.ShowDialog();
                    txtSPc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddSPC.Focus();
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

        private void btnAddSPC_Click(object sender, EventArgs e)
        {

            try
            {
                //lstPC.Clear();
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtSChannel.Text, txtSSubChannel.Text, null, null, null, txtSPc.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstSpc.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                txtSChannel.Text = "";
                txtSSubChannel.Text = "";
                txtSPc.Text = "";
                txtSPc.Focus();
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

        private void btnSSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSpc.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnSUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSpc.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnSClear_Click(object sender, EventArgs e)
        {
            txtSChannel.Text = "";
            txtSSubChannel.Text = "";
            txtSPc.Text = "";
            lstSpc.Clear();
            txtSChannel.Focus();
        }

        private void btnSearchSCir_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSCir;
                _CommonSearch.ShowDialog();
                txtSCir.Select();
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

        private void txtSCir_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSCir;
                _CommonSearch.ShowDialog();
                txtSCir.Select();
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

        private void txtSCir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSCir;
                    _CommonSearch.ShowDialog();
                    txtSCir.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSpromo.Focus();
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

        private void btnSearchSPromo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSpromo;
                _CommonSearch.ShowDialog();
                txtSpromo.Select();
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

        private void txtSpromo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSpromo;
                _CommonSearch.ShowDialog();
                txtSpromo.Select();
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

        private void txtSpromo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSpromo;
                    _CommonSearch.ShowDialog();
                    txtSpromo.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnSPromoAdd.Focus();
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

        private void txtSpromo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSpromo.Text))
                {
                    List<PriceDetailRef> _sList = new List<PriceDetailRef>();
                    _sList = CHNLSVC.Sales.GetPriceByPromoCD(txtPromoCode.Text.Trim());

                    if (_sList == null)
                    {
                        MessageBox.Show("Invalid promo code.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSpromo.Text = "";
                        txtSpromo.Focus();
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

        private void btnSPromoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSCir.Text) && string.IsNullOrEmpty(txtSpromo.Text))
                {
                    MessageBox.Show("Please enter circluar # or promo code.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSCir.Text = "";
                    txtSCir.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtSpromo.Text))
                {
                    lstsPromo.Items.Add(txtSpromo.Text.Trim());
                    return;
                }

                lstsPromo.Clear();
                DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtSCir.Text.Trim(), txtPromoCode.Text, txtBook.Text, txtLevel.Text);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstsPromo.Items.Add(drow["sapd_promo_cd"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please check enter circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSCir.Text = "";
                    txtSCir.Focus();
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

        private void btnSPSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstsPromo.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnSPUnselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstsPromo.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnSPClear_Click(object sender, EventArgs e)
        {
            txtSCir.Text = "";
            txtSpromo.Text = "";
            txtSCir.Focus();
        }

        private void btnSimHis_Click(object sender, EventArgs e)
        {
            try
            {
                _isSimilarRecal = false;
                btnSaveSimilar.Text = "Save";
                if (string.IsNullOrEmpty(txtMainItem.Text))
                {
                    MessageBox.Show("Please enter main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMainItem.Focus();
                    _isSimilarRecal = false;
                    return;
                }

                _similarDetails = new List<MasterItemSimilar>();

                _similarDetails = CHNLSVC.Sales.GetSimilarSetupDet(BaseCls.GlbUserComCode, txtMainItem.Text.Trim(), "S");

                if (_similarDetails == null)
                {
                    MessageBox.Show("Cannot find any setup details for this item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMainItem.Focus();
                    _isSimilarRecal = false;
                    btnSimApply.Enabled = true;
                    return;
                }
                else
                {
                    _isSimilarRecal = true;
                    btnSaveSimilar.Text = "Update";
                    btnSimApply.Enabled = false;
                }

                dgvSimDet.AutoGenerateColumns = false;
                dgvSimDet.DataSource = new List<MasterItemSimilar>();
                dgvSimDet.DataSource = _similarDetails;

                if (_similarDetails != null)
                {
                    foreach (MasterItemSimilar _chk in _similarDetails)
                    {
                        foreach (DataGridViewRow row in dgvSimDet.Rows)
                        {
                            Int32 _pbSeq = Convert.ToInt32(row.Cells["col_s_Seq"].Value);

                            if (_pbSeq == _chk.Misi_seq_no)
                            {
                                DataGridViewCheckBoxCell chk = row.Cells[7] as DataGridViewCheckBoxCell;
                                //if (Convert.ToBoolean(chk.Value) == false)
                                if (_chk.Misi_act == true)
                                {
                                    chk.Value = true;
                                    goto L1;
                                }
                                else
                                {
                                    chk.Value = false;
                                    goto L1;
                                }
                            }

                        }
                    L1: Int16 x = 1;
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

        private void btnClearSimilar_Click(object sender, EventArgs e)
        {
            clear_Similar();
        }

        private void clear_Similar()
        {
            rbdDiscValue.Checked = false;
            rbdDiscountRate.Checked = false;
            pnlDiscount.Visible = false;
            pnlDiscType.Visible = false;
            pnlVoucherValue.Visible = false;
            txtVoucherDiscount.Text = string.Empty;
            txtVouValue.Text = string.Empty;
            pnlVoucherValue.Visible = false;
            pnlDiscType.Visible = false;
            pnlDiscount.Visible = false;
            txtVoucherDiscount.Text = string.Empty;
            txtVoucherValue.Text = string.Empty;
            pnlValidPeriod.Visible = false;
            txtValidPeriod.Text = string.Empty;
            pnlVoucherParameters.Visible = false;
            cmbParameterType.Focus();
            cmbParameterType.Select();
            cmbParameterType.SelectedIndex = -1;

            btnSimApply.Enabled = true;
            _isSimilarRecal = false;
            btnSaveSimilar.Text = "Save";
            txtMainItem.Text = "";
            lblMainModel.Text = "";
            lblMainDesc.Text = "";
            dtpSFrom.Value = DateTime.Now.Date;
            dtpSTo.Value = DateTime.Now.Date;
            txtInvoiceNo.Text = "";
            txtMainCate.Text = "";
            txtSubCate.Text = "";
            txtItemRange.Text = "";
            txtBrand.Text = "";
            txtItem.Text = "";
            txtVoucherValue.Clear();
            txtSChannel.Text = "";
            txtSSubChannel.Text = "";
            txtSPc.Text = "";
            lstSpc.Clear();
            txtSCir.Text = "";
            txtSpromo.Text = "";
            txtMainItem.Enabled = true;
            _similarDetails = new List<MasterItemSimilar>();
            _Stype = "";
            _Ltype = "";

            dgvSimDet.AutoGenerateColumns = false;
            dgvSimDet.DataSource = new List<MasterItemSimilar>();
            txt_p_book.Text = "";
            txt_p_level.Text="";
            lstsPromo.Clear();

        }

        private void btnSaveSimilar_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtMainItem.Text))
                {
                    MessageBox.Show("Please select main item.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMainItem.Focus();
                    return;
                }

                if (_similarDetails == null || _similarDetails.Count == 0)
                {
                    MessageBox.Show("Similar item details are missing.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvSimDet.Rows.Count == 0)
                {
                    MessageBox.Show("Similar item details are missing.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //updated by akila 2017/12/29
                if (_similarDetails != null && _similarDetails.Count > 0)
                {
                    if (_similarDetails.Count == 1) //voucher parameters can be define only for one item
                    {
                        MasterItem _itemMaster = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _similarDetails.FirstOrDefault().Misi_sim_itm_cd);
                        if (_itemMaster != null && _itemMaster.Mi_itm_tp == "G")//check whether item is a GIFT VOUCHER
                        {
                            if (cmbParameterType.Items.Count > 0 && string.IsNullOrEmpty(cmbParameterType.Text))
                            {
                                MessageBox.Show("Voucher parameters has not defined for selected item !", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DisplayVoucherParameterPnl();
                                return;
                            }
                            else
                            {
                                _similarDetails = UpdateItemList(_similarDetails);
                            }
                        }
                    }
                }

                List<MasterItemSimilar> _UpdateList = new List<MasterItemSimilar>();

                if (_isSimilarRecal == true)
                {
                    foreach (DataGridViewRow row in dgvSimDet.Rows)
                    {
                        Int32 _pbSeq = Convert.ToInt32(row.Cells["col_s_Seq"].Value);
                        Boolean _Act = Convert.ToBoolean(row.Cells["col_s_Act"].Value);

                        foreach (MasterItemSimilar _tmpSList in _similarDetails)
                        {
                            if (_pbSeq == _tmpSList.Misi_seq_no)
                            {
                                _tmpSList.Misi_act = _Act;
                                _UpdateList.Add(_tmpSList);
                                goto L2;
                            }

                        }
                    L2: Int16 I = 1;
                    }

                    row_aff = (Int32)CHNLSVC.Sales.UpdateSimilarItems(_UpdateList);

                    if (row_aff == 1)
                    {

                        MessageBox.Show("Similar item definition updated.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_Similar();

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dgvSimDet.Rows)
                    {
                        Boolean _Newact = Convert.ToBoolean(row.Cells["col_s_Act"].Value);
                        Int32 _seq = Convert.ToInt32(row.Cells["col_s_Seq"].Value);
                        if (_Newact == true)
                        {
                            //string _com = Convert.ToString(row.Cells["col_s_Com"].Value);
                            //string _mainItem = txtMainItem.Text.Trim();
                            //string _sItem = Convert.ToString(row.Cells["col_s_Item"].Value);
                            //DateTime _fdate = Convert.ToDateTime(row.Cells["col_s_From"].Value).Date;
                            //DateTime _tdate = Convert.ToDateTime(row.Cells["col_s_To"].Value).Date;
                            //string _sPC = Convert.ToString(row.Cells["col_S_pc"].Value);
                            //string _refDoc = Convert.ToString(row.Cells["col_s_Doc"].Value);
                            //string _promo = Convert.ToString(row.Cells["col_s_Promo"].Value);

                            foreach (MasterItemSimilar _tmpSList in _similarDetails)
                            {
                                if (_seq == _tmpSList.Misi_seq_no)
                                {
                                    _tmpSList.Misi_act = _Newact;
                                    _UpdateList.Add(_tmpSList);
                                    goto L3;
                                }

                            }
                        L3: Int16 I = 1;
                        }
                    }

                    row_aff = (Int32)CHNLSVC.Sales.SaveSimilarItems(_UpdateList);

                    if (row_aff == 1)
                    {

                        MessageBox.Show("Similar item definition created.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_Similar();

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSimApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMainItem.Text))
                {
                    MessageBox.Show("Please enter main item.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMainItem.Focus();
                    return;
                }

                Boolean _isValidItm = false;
                foreach (ListViewItem Item in txtVoucherValue.Items)
                {
                    string _item = Item.Text;

                    if (Item.Checked == true)
                    {
                        //add by akila 2017/012/29
                        MasterItem _itemMaster = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        if (_itemMaster != null && _itemMaster.Mi_itm_tp == "G")//check whether item is a GIFT VOUCHER
                        {
                            if (cmbParameterType.Items.Count > 0 && string.IsNullOrEmpty(cmbParameterType.Text))
                            {
                                MessageBox.Show("Voucher parameters has not defined for selected item !", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DisplayVoucherParameterPnl();
                                return;
                            }

                        }

                        _isValidItm = true;
                        goto L3;
                    }
                }
            L3:

                if (_isValidItm == false)
                {
                    MessageBox.Show("No any applicable similar items are selected.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MasterItemSimilar _tmpList = new MasterItemSimilar();

                Boolean _isValidPc = false;
                if (optPCSIS.Checked)
                {
                    foreach (ListViewItem Item in lstSpc.Items)
                    {
                        string _item = Item.Text;

                        if (Item.Checked == true)
                        {
                            _isValidPc = true;
                            goto L4;
                        }
                    }
                }
            L4: Int16 x = 1;

                Boolean _isValidPromo = false;
                foreach (ListViewItem Item in lstsPromo.Items)
                {
                    string _item = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isValidPromo = true;
                        goto L5;
                    }
                }
            L5: Int16 y = 1;

                if (optPCSIS.Checked)       //kapila 3/1/2017
                {
                    if (_isValidPc == false && _isValidPromo == false)
                    {
                        foreach (ListViewItem itmList in txtVoucherValue.Items)
                        {
                            string itm = itmList.Text;

                            if (itmList.Checked == true)
                            {
                                _tmpList = new MasterItemSimilar();
                                _tmpList.Misi_act = true;
                                _tmpList.Misi_com = BaseCls.GlbUserComCode;
                                _tmpList.Misi_cre_by = BaseCls.GlbUserID;
                                _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                _tmpList.Misi_from = dtpSFrom.Value.Date;
                                _tmpList.Misi_to = dtpSTo.Value.Date;
                                _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                _tmpList.Misi_loc = null;
                                _tmpList.Misi_mod_by = BaseCls.GlbUserID;
                                _tmpList.Misi_pc = null;
                                _tmpList.Misi_promo = null;
                                _tmpList.Misi_seq_no = _tmpList.Misi_seq_no + 1;
                                _tmpList.Misi_sim_itm_cd = itm;
                                _tmpList.Misi_tp = "S";
                                _tmpList.Misi_pty_tp = "PC";
                                //_tmpList.Misi_price_book=txt_p_book.Text.ToString().Trim();
                                //_tmpList.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                                _similarDetails.Add(_tmpList);

                            }
                        }
                    }
                    else if (_isValidPc == true && _isValidPromo == false)
                    {
                        foreach (ListViewItem itmList in txtVoucherValue.Items)
                        {
                            string itm = itmList.Text;

                            if (itmList.Checked == true)
                            {
                                foreach (ListViewItem pcList in lstSpc.Items)
                                {
                                    string _pc = pcList.Text;

                                    if (pcList.Checked == true)
                                    {
                                        _tmpList = new MasterItemSimilar();
                                        _tmpList.Misi_act = true;
                                        _tmpList.Misi_com = BaseCls.GlbUserComCode;
                                        _tmpList.Misi_cre_by = BaseCls.GlbUserID;
                                        _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                        _tmpList.Misi_from = dtpSFrom.Value.Date;
                                        _tmpList.Misi_to = dtpSTo.Value.Date;
                                        _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                        _tmpList.Misi_loc = null;
                                        _tmpList.Misi_mod_by = BaseCls.GlbUserID;
                                        _tmpList.Misi_pc = _pc;
                                        _tmpList.Misi_promo = null;
                                        _tmpList.Misi_seq_no = _similarDetails.Count + 1;
                                        _tmpList.Misi_sim_itm_cd = itm;
                                        _tmpList.Misi_tp = "S";
                                        _tmpList.Misi_pty_tp = "PC";
                                        //_tmpList.Misi_price_book = txt_p_book.Text.ToString().Trim();
                                        //_tmpList.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                                        _similarDetails.Add(_tmpList);
                                    }
                                }
                            }
                        }
                    }
                    else if (_isValidPc == false && _isValidPromo == true)
                    {
                        foreach (ListViewItem itmList in txtVoucherValue.Items)
                        {
                            string itm = itmList.Text;

                            if (itmList.Checked == true)
                            {
                                foreach (ListViewItem promoList in lstsPromo.Items)
                                {
                                    string _promo = promoList.Text;

                                    if (promoList.Checked == true)
                                    {
                                        _tmpList = new MasterItemSimilar();
                                        _tmpList.Misi_act = true;
                                        _tmpList.Misi_com = BaseCls.GlbUserComCode;
                                        _tmpList.Misi_cre_by = BaseCls.GlbUserID;
                                        _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                        _tmpList.Misi_from = dtpSFrom.Value.Date;
                                        _tmpList.Misi_to = dtpSTo.Value.Date;
                                        _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                        _tmpList.Misi_loc = null;
                                        _tmpList.Misi_mod_by = BaseCls.GlbUserID;
                                        _tmpList.Misi_pc = null;
                                        _tmpList.Misi_promo = _promo;
                                        _tmpList.Misi_seq_no = _similarDetails.Count + 1;
                                        _tmpList.Misi_sim_itm_cd = itm;
                                        _tmpList.Misi_tp = "S";
                                        _tmpList.Misi_pty_tp = "PC";
                                        //_tmpList.Misi_price_book = txt_p_book.Text.ToString().Trim();
                                        //_tmpList.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                                        _similarDetails.Add(_tmpList);
                                    }
                                }
                            }
                        }
                    }
                    else if (_isValidPc == true && _isValidPromo == true)
                    {
                        foreach (ListViewItem itmList in txtVoucherValue.Items)
                        {
                            string itm = itmList.Text;

                            if (itmList.Checked == true)
                            {
                                foreach (ListViewItem pcList in lstSpc.Items)
                                {
                                    string _pc = pcList.Text;

                                    if (pcList.Checked == true)
                                    {

                                        foreach (ListViewItem promoList in lstsPromo.Items)
                                        {
                                            string _promo = promoList.Text;

                                            if (promoList.Checked == true)
                                            {
                                                _tmpList = new MasterItemSimilar();
                                                _tmpList.Misi_act = true;
                                                _tmpList.Misi_com = BaseCls.GlbUserComCode;
                                                _tmpList.Misi_cre_by = BaseCls.GlbUserID;
                                                _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                                _tmpList.Misi_from = dtpSFrom.Value.Date;
                                                _tmpList.Misi_to = dtpSTo.Value.Date;
                                                _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                                _tmpList.Misi_loc = null;
                                                _tmpList.Misi_mod_by = BaseCls.GlbUserID;
                                                _tmpList.Misi_pc = _pc;
                                                _tmpList.Misi_promo = _promo;
                                                _tmpList.Misi_seq_no = _tmpList.Misi_seq_no + 1;
                                                _tmpList.Misi_sim_itm_cd = itm;
                                                _tmpList.Misi_tp = "S";
                                                _tmpList.Misi_pty_tp = "PC";
                                                //_tmpList.Misi_price_book = txt_p_book.Text.ToString().Trim();
                                                //_tmpList.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                                                _similarDetails.Add(_tmpList);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else   //kapila 3/1/2017 - not profit center
                {
                    if (_isValidPromo == false)
                    {
                        foreach (ListViewItem itmList in txtVoucherValue.Items)
                        {
                            string itm = itmList.Text;

                            if (itmList.Checked == true)
                            {
                                _tmpList = new MasterItemSimilar();
                                _tmpList.Misi_act = true;
                                _tmpList.Misi_com = BaseCls.GlbUserComCode;
                                _tmpList.Misi_cre_by = BaseCls.GlbUserID;
                                _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                _tmpList.Misi_from = dtpSFrom.Value.Date;
                                _tmpList.Misi_to = dtpSTo.Value.Date;
                                _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                _tmpList.Misi_loc = null;
                                _tmpList.Misi_mod_by = BaseCls.GlbUserID;
                                if (optChnlSIS.Checked)
                                {
                                    _tmpList.Misi_pc = txtSChannel.Text;
                                    _tmpList.Misi_pty_tp = "CHNL";
                                }
                                else
                                {
                                    _tmpList.Misi_pc = txtSSubChannel.Text;
                                    _tmpList.Misi_pty_tp = "SCHNL";
                                }
                                _tmpList.Misi_promo = null;
                                _tmpList.Misi_seq_no = _tmpList.Misi_seq_no + 1;
                                _tmpList.Misi_sim_itm_cd = itm;
                                _tmpList.Misi_tp = "S";
                                //_tmpList.Misi_price_book = txt_p_book.Text.ToString().Trim();
                                //_tmpList.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                                _similarDetails.Add(_tmpList);

                            }
                        }
                    }
                    else     //_isValidPromo=true
                    {
                        foreach (ListViewItem itmList in txtVoucherValue.Items)
                        {
                            string itm = itmList.Text;

                            if (itmList.Checked == true)
                            {
                                foreach (ListViewItem promoList in lstsPromo.Items)
                                {
                                    string _promo = promoList.Text;

                                    if (promoList.Checked == true)
                                    {
                                        _tmpList = new MasterItemSimilar();
                                        _tmpList.Misi_act = true;
                                        _tmpList.Misi_com = BaseCls.GlbUserComCode;
                                        _tmpList.Misi_cre_by = BaseCls.GlbUserID;
                                        _tmpList.Misi_doc_no = txtInvoiceNo.Text;
                                        _tmpList.Misi_from = dtpSFrom.Value.Date;
                                        _tmpList.Misi_to = dtpSTo.Value.Date;
                                        _tmpList.Misi_itm_cd = txtMainItem.Text.Trim();
                                        _tmpList.Misi_loc = null;
                                        _tmpList.Misi_mod_by = BaseCls.GlbUserID;
                                        if (optChnlSIS.Checked)
                                        {
                                            _tmpList.Misi_pc = txtSChannel.Text;
                                            _tmpList.Misi_pty_tp = "CHNL";
                                        }
                                        else
                                        {
                                            _tmpList.Misi_pc = txtSSubChannel.Text;
                                            _tmpList.Misi_pty_tp = "SCHNL";
                                        }
                                        _tmpList.Misi_promo = _promo;
                                        _tmpList.Misi_seq_no = _similarDetails.Count + 1;
                                        _tmpList.Misi_sim_itm_cd = itm;
                                        _tmpList.Misi_tp = "S";
                                        //_tmpList.Misi_price_book = txt_p_book.Text.ToString().Trim();
                                        //_tmpList.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                                        _similarDetails.Add(_tmpList);
                                    }
                                }
                            }
                        }
                    }
                }

                txtVoucherValue.Clear();
                //txt_p_book.Clear();
                //txt_p_level.Clear();
                lstSpc.Clear();
                lstsPromo.Clear();
                txtMainItem.Enabled = false;
                dgvSimDet.AutoGenerateColumns = false;
                dgvSimDet.DataSource = new List<MasterItemSimilar>();
                dgvSimDet.DataSource = _similarDetails;

                foreach (DataGridViewRow row in dgvSimDet.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[7] as DataGridViewCheckBoxCell;
                    chk.Value = true;

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

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtMainCate.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice);
                    DataTable _result = CHNLSVC.CommonSearch.GetComInvoice(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                    _CommonSearch.ShowDialog();
                    txtInvoiceNo.Select();


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

        private void btnMClear_Click(object sender, EventArgs e)
        {
            Clear_Maintaince();
        }

        private void Clear_Maintaince()
        {
            _defDet = new List<PriceDefinitionRef>();
            _Stype = "";
            _Ltype = "";


            chkSetDefault.Checked = true;
            chkDefDis.Checked = false;
            txtDisRate.Enabled = false;
            txtDisRate.Text = "0";
            chkChkCredit.Checked = false;
            txtmChannel.Text = "";
            txtmSubChannel.Text = "";
            txtMPc.Text = "";
            lstMpc.Clear();
            btnMSave.Enabled = true;

            txtMBook.Text = "";
            txtMLevel.Text = "";
            txtMInvType.Text = "";
            txtMPc.Text = "";


            dgvMDef.AutoGenerateColumns = false;
            dgvMDef.DataSource = new List<PriceDefinitionRef>();
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

        private void txtMBook_DoubleClick(object sender, EventArgs e)
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

        private void txtMBook_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtMBook;
                    _CommonSearch.ShowDialog();
                    txtMBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtMLevel.Focus();
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

        private void txtMBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtMBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Clear();
                    txtMBook.Focus();
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

        private void txtMLevel_DoubleClick(object sender, EventArgs e)
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

        private void txtMLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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
                else if (e.KeyCode == Keys.Enter)
                {
                    txtMPc.Focus();
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

        private void btnSearchMPc_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMPc;
                _CommonSearch.ShowDialog();
                txtMPc.Select();

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

        private void txtMPc_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMPc;
                _CommonSearch.ShowDialog();
                txtMPc.Select();

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

        private void txtMPc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtMPc;
                    _CommonSearch.ShowDialog();
                    txtMPc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtMInvType.Focus();
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
                    btnLoadMainDet.Focus();
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

        private void btnLoadMainDet_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text) && string.IsNullOrEmpty(txtMLevel.Text) && string.IsNullOrEmpty(txtMPc.Text) && string.IsNullOrEmpty(txtMInvType.Text))
                {
                    MessageBox.Show("Please enter one of above selection categories.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMBook.Text) && !string.IsNullOrEmpty(txtMLevel.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Focus();
                    return;
                }

                _defDet = new List<PriceDefinitionRef>();

                _defDet = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(BaseCls.GlbUserComCode, txtMBook.Text, txtMLevel.Text, txtMInvType.Text, txtMPc.Text);

                dgvMDef.AutoGenerateColumns = false;
                dgvMDef.DataSource = new List<PriceDefinitionRef>();
                dgvMDef.DataSource = _defDet;

                btnMSave.Enabled = false;
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

        private void txtMLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMLevel.Text)) return;
                if (string.IsNullOrEmpty(txtMBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMLevel.Clear(); txtMBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtMBook.Text.Trim(), txtMLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtMLevel.Clear(); txtMLevel.Focus(); return; }
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
                    MessageBox.Show("Please select the valid invoice type.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearchMChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtmChannel;
                _CommonSearch.ShowDialog();
                txtmChannel.Select();
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

        private void txtmChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtmChannel;
                _CommonSearch.ShowDialog();
                txtmChannel.Select();
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

        private void txtmChannel_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtmChannel;
                    _CommonSearch.ShowDialog();
                    txtmChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtmSubChannel.Focus();
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

        private void btnSearchMSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmChannel.Text = "";
                    txtmChannel.Focus();
                    return;
                }

                _Ltype = "Maintain";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtmSubChannel;
                _CommonSearch.ShowDialog();
                txtmSubChannel.Select();
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

        private void btnSearchmAppPc_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _Ltype = "Maintain";
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMAppPc;
                _CommonSearch.ShowDialog();
                txtMAppPc.Select();

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

        private void txtmSubChannel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmChannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmChannel.Text = "";
                    txtmChannel.Focus();
                    return;
                }

                _Ltype = "Maintain";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtmSubChannel;
                _CommonSearch.ShowDialog();
                txtmSubChannel.Select();
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

        private void txtmSubChannel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtmChannel.Text))
                    {
                        MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtmChannel.Text = "";
                        txtmChannel.Focus();
                        return;
                    }

                    _Ltype = "Maintain";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtmSubChannel;
                    _CommonSearch.ShowDialog();
                    txtmSubChannel.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtMAppPc.Focus();
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

        private void txtMAppPc_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _Ltype = "Maintain";
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMAppPc;
                _CommonSearch.ShowDialog();
                txtMAppPc.Select();

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

        private void txtMAppPc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _Ltype = "Maintain";
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtMAppPc;
                    _CommonSearch.ShowDialog();
                    txtMAppPc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddMAppPc.Focus();
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

        private void btnMpcselect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstMpc.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnMunSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstMpc.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnMAppClear_Click(object sender, EventArgs e)
        {
            lstMpc.Clear();
            txtmChannel.Text = "";
            txtmSubChannel.Text = "";
            txtMAppPc.Text = "";
            txtmChannel.Focus();
        }

        private void btnAddMAppPc_Click(object sender, EventArgs e)
        {

            try
            {
                //lstPC.Clear();
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtmChannel.Text, txtmSubChannel.Text, null, null, null, txtMAppPc.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstMpc.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                txtmChannel.Text = "";
                txtmSubChannel.Text = "";
                txtMAppPc.Text = "";
                txtMAppPc.Focus();
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

        private void chkDefDis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefDis.Checked == true)
            {
                txtDisRate.Enabled = true;
                txtDisRate.Text = "0";

            }
            else
            {
                txtDisRate.Enabled = false;
                txtDisRate.Text = "0";

            }
        }

        private void chkSetDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetDefault.Checked == true)
            {
                txtPrefix.Enabled = false;
            }
            else
            {
                txtPrefix.Enabled = true;
            }
        }

        private void btnMApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text))
                {
                    MessageBox.Show("Please enter applicable price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Text = "";
                    txtMBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMLevel.Text))
                {
                    MessageBox.Show("Please enter applicable price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMLevel.Text = "";
                    txtMLevel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMInvType.Text))
                {
                    MessageBox.Show("Please enter applicable invoice type.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMInvType.Text = "";
                    txtMInvType.Focus();
                    return;
                }

                if (chkDefDis.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        MessageBox.Show("Defalut discount rate is missing.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "";
                        txtDisRate.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtDisRate.Text) > 100 || Convert.ToDecimal(txtDisRate.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "";
                        txtDisRate.Focus();
                        return;
                    }
                }

                if (chkSetDefault.Checked == false)
                {
                    if (string.IsNullOrEmpty(txtPrefix.Text))
                    {
                        MessageBox.Show("Please enter invoice prefix.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPrefix.Text = "";
                        txtPrefix.Focus();
                        return;
                    }
                }

                Boolean _isValidpc = false;
                foreach (ListViewItem Item in lstMpc.Items)
                {
                    string _item = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isValidpc = true;
                        goto L3;
                    }
                }
            L3:

                if (_isValidpc == false)
                {
                    MessageBox.Show("No any applicable profit center is select.", "Price Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                PriceDefinitionRef _tmpList = new PriceDefinitionRef();
                string _invPreTp = "";
                foreach (ListViewItem mAppPcList in lstMpc.Items)
                {
                    string itm = mAppPcList.Text;

                    if (mAppPcList.Checked == true)
                    {
                        _tmpList = new PriceDefinitionRef();
                        _invPreTp = "";
                        _tmpList.Sadd_chk_credit_bal = chkChkCredit.Checked;
                        _tmpList.Sadd_com = BaseCls.GlbUserComCode;
                        _tmpList.Sadd_cre_by = BaseCls.GlbUserID;
                        _tmpList.Sadd_cre_when = DateTime.Today.Date;
                        _tmpList.Sadd_def = false;
                        _tmpList.Sadd_def_stus = null;
                        _tmpList.Sadd_disc_rt = Convert.ToDecimal(txtDisRate.Text);
                        _tmpList.Sadd_doc_tp = txtMInvType.Text;
                        _tmpList.Sadd_is_bank_ex_rt = true;
                        _tmpList.Sadd_is_disc = chkDefDis.Checked;
                        _tmpList.Sadd_mod_by = BaseCls.GlbUserID;
                        _tmpList.Sadd_mod_when = DateTime.Today.Date;
                        _tmpList.Sadd_p_lvl = txtMLevel.Text;
                        _tmpList.Sadd_pb = txtMBook.Text;
                        _tmpList.Sadd_pc = itm;

                        //if (txtMInvType.Text == "LEASE")
                        //{
                        //    _invPreTp = "LS";
                        //}
                        //else if (txtMInvType.Text == "CRED")
                        //{
                        //    _invPreTp = "CR";
                        //}
                        //else if (txtMInvType.Text == "CS")
                        //{
                        //    _invPreTp = "CS";
                        //}
                        //else if (txtMInvType.Text == "HS")
                        //{
                        //    _invPreTp = "HS";
                        //}
                        //else if (txtMInvType.Text == "INTR")
                        //{
                        //    _invPreTp = "IR";
                        //}
                        //else
                        //{
                        //    _invPreTp = txtMInvType.Text.Trim();
                        //}

                        DataTable _type = new DataTable();
                        _type = CHNLSVC.Sales.GetDefInvPrefix(BaseCls.GlbUserComCode, txtMInvType.Text.Trim());

                        if (_type.Rows.Count > 1)
                        {
                            MessageBox.Show("Multyple prefixes are loading.", "Price Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (_type.Rows.Count > 0)
                        {
                            _invPreTp = Convert.ToString(_type.Rows[0]["SDP_PFIX"]);
                        }
                        else
                        {
                            MessageBox.Show("Default company prefixes are not define for this company.", "Price Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        if (chkSetDefault.Checked == true)
                        {
                            if (txtMInvType.Text.Trim() == "HS")
                            {
                                //if (BaseCls.GlbUserComCode == "AAL")
                                //{
                                //    _tmpList.Sadd_prefix = _invPreTp;
                                //}
                                //else
                                //{
                                //_tmpList.Sadd_prefix = _invPreTp + "-";
                                //}
                                _tmpList.Sadd_prefix = _invPreTp;
                            }
                            else
                            {
                                _tmpList.Sadd_prefix = itm + "-" + _invPreTp;
                            }
                        }
                        else
                        {
                            _tmpList.Sadd_prefix = txtPrefix.Text.Trim();
                        }

                        _defDet.Add(_tmpList);

                    }
                }

                dgvMDef.AutoGenerateColumns = false;
                dgvMDef.DataSource = new List<PriceDefinitionRef>();
                dgvMDef.DataSource = _defDet;

                txtMBook.Text = "";
                txtMLevel.Text = "";
                txtMPc.Text = "";
                txtMInvType.Text = "";
                txtmChannel.Text = "";
                txtmSubChannel.Text = "";
                txtMAppPc.Text = "";
                lstMpc.Clear();
                chkDefDis.Checked = false;
                txtDisRate.Text = "0";
                txtDisRate.Enabled = false;
                chkChkCredit.Checked = false;
                txtPrefix.Text = "";
                chkSetDefault.Checked = true;

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

        private void txtDisRate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    if (!IsNumeric(txtDisRate.Text))
                    {
                        MessageBox.Show("Discount rate should be numeric.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "";
                        txtDisRate.Focus();
                    }

                    if (Convert.ToDecimal(txtDisRate.Text) > 100 || Convert.ToDecimal(txtDisRate.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDisRate.Text = "";
                        txtDisRate.Focus();
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

        private void btnMSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (_defDet == null || _defDet.Count == 0)
                {
                    MessageBox.Show("Cannot find define details.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                row_aff = (Int32)CHNLSVC.Sales.SavePcPriceDefinition(_defDet);

                if (row_aff == 1)
                {

                    MessageBox.Show("Price level permission activated.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Maintaince();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearchInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetComInvoice(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
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

        private void txtInvoiceNo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetComInvoice(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
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

        private void dgvMDef_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (_defDet == null || _defDet.Count == 0) return;

                if (MessageBox.Show("Do you want to remove access ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {


                    int row_id = e.RowIndex;
                    string _pc = dgvMDef.Rows[e.RowIndex].Cells["col_m_pc"].Value.ToString();
                    string _invtp = dgvMDef.Rows[e.RowIndex].Cells["col_m_invTp"].Value.ToString();
                    string _lvl = dgvMDef.Rows[e.RowIndex].Cells["col_m_lvl"].Value.ToString();
                    string _com = dgvMDef.Rows[e.RowIndex].Cells["col_m_com"].Value.ToString();
                    string _pb = dgvMDef.Rows[e.RowIndex].Cells["col_m_book"].Value.ToString();




                    List<PriceDefinitionRef> _temp = new List<PriceDefinitionRef>();
                    _temp = _defDet;


                    _temp.RemoveAll(x => x.Sadd_com == _com && x.Sadd_pc == _pc && x.Sadd_doc_tp == _invtp && x.Sadd_pb == _pb && x.Sadd_p_lvl == _lvl);
                    _defDet = _temp;

                    dgvMDef.AutoGenerateColumns = false;
                    dgvMDef.DataSource = new List<PriceDefinitionRef>();
                    dgvMDef.DataSource = _defDet;


                    row_aff = (Int32)CHNLSVC.Sales.RemovePriceAccess(_pc, _invtp, _lvl, _com, _pb, BaseCls.GlbUserID);

                    if (row_aff == 1)
                    {

                        MessageBox.Show("Price level permission removed.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchAddbook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAddBook;
                _CommonSearch.ShowDialog();
                txtAddBook.Select();
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

        private void txtAddBook_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtAddBook;
                    _CommonSearch.ShowDialog();
                    txtAddBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtAddLevel.Focus();
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

        private void txtAddBook_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAddBook;
                _CommonSearch.ShowDialog();
                txtAddBook.Select();
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

        private void txtAddBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMBook.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtAddBook.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Clear();
                    txtMBook.Focus();
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

        private void btnSearchAddLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAddBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddBook.Clear();
                    txtAddBook.Focus();
                    return;
                }
                _Stype = "Additional";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAddBook;
                _CommonSearch.ShowDialog();
                txtAddBook.Select();

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

        private void txtAddLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAddBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddBook.Clear();
                    txtAddBook.Focus();
                    return;
                }
                _Stype = "Additional";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAddBook;
                _CommonSearch.ShowDialog();
                txtAddBook.Select();

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

        private void txtAddLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtAddBook.Text))
                    {
                        MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddBook.Clear();
                        txtAddBook.Focus();
                        return;
                    }
                    _Stype = "Additional";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtAddBook;
                    _CommonSearch.ShowDialog();
                    txtAddBook.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtMsg.Focus();
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

        private void txtMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkAge.Focus();
            }
        }

        private void chkAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkCusMan.Focus();
            }
        }

        private void txtAddLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAddLevel.Text)) return;
                if (string.IsNullOrEmpty(txtAddBook.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAddLevel.Clear(); txtAddBook.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtAddBook.Text.Trim(), txtAddLevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAddLevel.Clear(); txtAddLevel.Focus();
                    return;
                }
                else
                {
                    txtMsg.Text = _tbl.Sapl_spmsg;
                    chkAge.Checked = _tbl.Sapl_isage;
                    chkCusMan.Checked = _tbl.Sapl_needcus;
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

        private void btnAddClear_Click(object sender, EventArgs e)
        {
            Clear_Add();
        }

        private void Clear_Add()
        {
            txtAddBook.Text = "";
            txtAddLevel.Text = "";
            txtMsg.Text = "";
            chkAge.Checked = false;
            chkCusMan.Checked = false;
            txtAddBook.Focus();

        }

        private void Clear_Base()
        {
            _isrecallDef = false;
            txtBaseCircular.Text = "";
            txtNpb.Text = "";
            txtNpl.Text = "";
            txtBasepb.Text = "";
            txtBasepbl.Text = "";
            txtUploadItems.Text = "";
            cmbSelectCat.Text = "";
            txtbbrd.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";
            txtCate3.Text = "";
            txtMarkup.Text = "";
            txtCharge.Text = "";

            txtBaseItem.Text = "";



            cmbCate.SelectedIndex = -1;
            cmbActivebase.SelectedIndex = -1;
            ItemBrandCat_List = new List<sar_pb_def_det>();
            grvSalesTypes.DataSource = null;
        }


        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtAddBook.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddBook.Clear();
                    txtAddBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAddLevel.Text))
                {
                    MessageBox.Show("Please select the price level", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddLevel.Clear();
                    txtAddLevel.Focus();
                    return;
                }

                row_aff = (Int32)CHNLSVC.Sales.UpdateAddPricingParam(BaseCls.GlbUserID, chkAge.Checked, txtMsg.Text, chkCusMan.Checked, txtAddLevel.Text, txtAddBook.Text, BaseCls.GlbUserComCode);

                if (row_aff > 0)
                {

                    MessageBox.Show("Additional parameters setup.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Add();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAmend_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _promoList = "";
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtBook.Text))
                {
                    MessageBox.Show("Please select a price book.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBook.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtLevel.Text))
                {
                    MessageBox.Show("Please select a price level.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLevel.Focus();
                    return;
                }
                if (_isRecal == false)
                {
                    MessageBox.Show("Please recall exsisting circular / promotion to extend.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dtpTo.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("To date cannot be less than today.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpTo.Focus();
                    return;
                }

                Boolean _isPromoFound = false;
                foreach (ListViewItem Item in lstPromoItem.Items)
                {
                    //string promoCD = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isPromoFound = true;
                        goto L2;
                    }
                }
            L2:

                if (_isPromoFound == false)
                {
                    MessageBox.Show("No any applicable promotions are selected.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                List<string> lstPromoI = new List<string>();


                foreach (ListViewItem Item in lstPromoItem.Items)
                {
                    //string promoCD = Item.Text;

                    if (Item.Checked == true)
                    {
                        _promoList = _promoList + "," + Item.Text;
                        lstPromoI.Add(Item.Text);
                    }
                }

                if (_promoList != null)
                {
                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    if (cmbPriceType.Text != "NORMAL")
                    {
                        masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                        masterAuto.Aut_cate_tp = "COM";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "PRICE";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = "PRO";
                        masterAuto.Aut_year = null;
                    }
                    else
                    {
                        masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                        masterAuto.Aut_cate_tp = "COM";
                        masterAuto.Aut_direction = null;
                        masterAuto.Aut_modify_dt = null;
                        masterAuto.Aut_moduleid = "PRICE";
                        masterAuto.Aut_number = 5;//what is Aut_number
                        masterAuto.Aut_start_char = "NOR";
                        masterAuto.Aut_year = null;
                    }

                    foreach (PriceDetailRef item in _list)
                    {
                        item.Sapd_price_stus = "P";
                    }


                    string _err = "";
                    row_aff = (Int32)CHNLSVC.Sales.AmendPromotion(_promoList, BaseCls.GlbUserID, Convert.ToDateTime(dtpTo.Value).Date, BaseCls.GlbUserSessionID, _list, _comList, masterAuto, out _err, txtBook.Text.ToUpper(), txtLevel.Text.ToUpper(), txtCircular.Text.ToUpper(), lstPromoI);

                    if (row_aff > 0)
                    {

                        MessageBox.Show("Promotion amended.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear_Data();

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_err))
                        {
                            MessageBox.Show(_err, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No any applicable promotions are selected.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtBook.Text))
                {
                    MessageBox.Show("Please select Price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBook.Text = "";
                    txtBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtLevel.Text))
                {
                    MessageBox.Show("Please select Price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLevel.Text = "";
                    txtLevel.Focus();
                    return;
                }

                if (dtpTo.Value.Date < dtpFrom.Value.Date)
                {
                    MessageBox.Show("To date cannot be less than from date.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpTo.Focus();
                    return;
                }

                //updated by akila 2018/02/08
                if (IsRestrictBackDatePriceUpload(dtpFrom.Value, dtpTo.Value))
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtCircular.Text))
                {
                    MessageBox.Show("Please enter circular #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Text = "";
                    txtCircular.Focus();
                    return;
                }
                DataTable _circular = CHNLSVC.Sales.GetCircularNo(txtCircular.Text.ToUpper());
                if (_circular.Rows.Count > 0)
                {
                    MessageBox.Show("Circular exists,Please enter another name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(cmbPriceType.Text);
                foreach (PriceTypeRef _tmp in _type)
                {
                    if (_tmp.Sarpt_is_com == true)
                    {
                        if (dgvPromo.Rows.Count <= 0)
                        {
                            MessageBox.Show("No combine / free items are define.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }

                List<PriceDetailRef> _saveDetail = new List<PriceDetailRef>();
                List<PriceCombinedItemRef> _saveComDet = new List<PriceCombinedItemRef>();

                foreach (PriceDetailRef _tmpPriceDet in _list)
                {
                    PriceDetailRef _tmpPriceList = new PriceDetailRef();
                    _tmpPriceList = _tmpPriceDet;
                    _tmpPriceList.Sapd_customer_cd = txtCusCode.Text;
                    _tmpPriceList.Sapd_from_date = dtpFrom.Value.Date;
                    _tmpPriceList.Sapd_to_date = dtpTo.Value.Date;
                    _tmpPriceList.Sapd_circular_no = txtCircular.Text;
                    _tmpPriceList.Sapd_pb_tp_cd = txtBook.Text;
                    _tmpPriceList.Sapd_pbk_lvl_cd = txtLevel.Text;
                    _tmpPriceList.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                    _tmpPriceList.Sapd_erp_ref = txtLevel.Text;
                    _tmpPriceList.Sapd_no_of_use_times = 0;
                    _tmpPriceList.Sapd_price_stus = "P";
                    _saveDetail.Add(_tmpPriceList);
                }

                foreach (PriceCombinedItemRef _tmpComList in _comList)
                {
                    _tmpComList.Sapc_increse = chkMulti.Checked;
                    _saveComDet.Add(_tmpComList);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();

                if (cmbPriceType.Text != "NORMAL")
                {
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "PRO";
                    masterAuto.Aut_year = null;
                }
                else
                {
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserComCode;
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PRICE";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "NOR";
                    masterAuto.Aut_year = null;
                }

                List<PriceProfitCenterPromotion> _savePcAllocList = new List<PriceProfitCenterPromotion>();

                //_savePcAllocList = _appPcList;
                foreach (DataGridViewRow row in dgvAppPC.Rows)
                {
                    string _pc = row.Cells["col_a_Pc"].Value.ToString();
                    string _promo = row.Cells["col_a_Promo"].Value.ToString();
                    Int16 _active = Convert.ToInt16(row.Cells["col_a_Act"].Value);
                    Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                    if (_active == 1)
                    {
                        foreach (PriceProfitCenterPromotion _tmp in _appPcList)
                        {
                            if (_tmp.Srpr_com == BaseCls.GlbUserComCode && _tmp.Srpr_pbseq == _pbSeq && _tmp.Srpr_pc == _pc && _tmp.Srpr_promo_cd == _promo)
                            {
                                //PriceProfitCenterPromotion _tmpList = new PriceProfitCenterPromotion();
                                //_tmpList.Srpr_act = _active;
                                //_tmpList.Srpr_com = BaseCls.GlbUserComCode;
                                //_tmpList.Srpr_cre_by = BaseCls.GlbUserID;
                                //_tmpList.Srpr_mod_by = BaseCls.GlbUserID;
                                //_tmpList.Srpr_pbseq = _pbSeq;
                                //_tmpList.Srpr_pc = _pc;
                                //_tmpList.Srpr_promo_cd = _promo;
                                _tmp.Srpr_act = _active;
                                _savePcAllocList.Add(_tmp);
                            }

                        }
                    }

                }

                //save list



                //update list

                PriceDetailRestriction _priceRes = new PriceDetailRestriction();
                //_priceRes = null;

                if (!string.IsNullOrEmpty(txtMessage.Text))
                {
                    _priceRes.Spr_com = BaseCls.GlbUserComCode;
                    _priceRes.Spr_msg = txtMessage.Text;
                    _priceRes.Spr_need_cus = chkCustomer.Checked;
                    _priceRes.Spr_need_nic = chkNIC.Checked;
                    _priceRes.Spr_need_pp = false;
                    _priceRes.Spr_need_dl = false;
                    _priceRes.Spr_usr = BaseCls.GlbUserID;
                    _priceRes.Spr_when = DateTime.Now;
                    _priceRes.Spr_promo = "";

                }
                else
                {
                    _priceRes = null;
                }

                string _err = "";


                row_aff = (Int32)CHNLSVC.Sales.SavePriceDetailsSaveAs(_saveDetail, _comList, masterAuto, _savePcAllocList, _serial, _priceRes, out _err, BaseCls.GlbUserSessionID, BaseCls.GlbUserID);

                if (row_aff == 1)
                {

                    MessageBox.Show("Price definition created successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(_err))
                    {
                        MessageBox.Show("Error occurred while processing !!!\n" + _err, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvPromo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    txtSubItem.Text = dgvPromo.Rows[e.RowIndex].Cells["col_s_SubItem"].Value.ToString();
                    txtSubQty.Text = dgvPromo.Rows[e.RowIndex].Cells["col_s_Qty"].Value.ToString();
                    txtSubPrice.Text = dgvPromo.Rows[e.RowIndex].Cells["col_s_Price"].Value.ToString();

                    MainLine = Convert.ToInt32(dgvPromo.Rows[e.RowIndex].Cells["col_s_MainLine"].Value);
                    SubLine = Convert.ToInt32(dgvPromo.Rows[e.RowIndex].Cells["col_s_SubLine"].Value);
                    txtSubItem_Leave(null, null);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                //get all promotions
                //check for used items
                //cancel all
                string error = "";
                List<string> _errList = new List<string>();
                List<string> _promoCodes = new List<string>();
                if (lstPromoItem.Items.Count <= 0)
                {
                    MessageBox.Show("Please add/select promotions from list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (ListViewItem item in lstPromoItem.Items)
                {
                    if (item.Checked)
                        _promoCodes.Add(item.Text);
                }

                int result = CHNLSVC.Sales.ProcessPromotionCancel(_promoCodes, out  error, out  _errList, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                // Nadeeka
                int resultsub = CHNLSVC.Sales.ProcessPromotionCancelSubPb(_promoCodes, out  error, out  _errList, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);

                if (result > 0)
                {
                    MessageBox.Show("Canceled Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (error != "")
                    {
                        MessageBox.Show("Error occurred while processing\n" + error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (_errList != null && _errList.Count > 0)
                    {
                        string _errPromo = "";
                        foreach (string st in _errList)
                        {
                            _errPromo = _errPromo + st + ",";
                        }
                        MessageBox.Show("Can not Cancel, following promotion has used items\n" + _errPromo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void dgvPromo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 6)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _comList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _comList;
                    dgvPromo.DataSource = _source;

                    chkEndDate.Enabled = false;
                }
            }
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            if (gbRes.Visible == false)
            {
                gbRes.Visible = true;
            }
            else { gbRes.Visible = false; }

        }

        private void btnResOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text))
            {
                MessageBox.Show("Please enter relavant message.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                _isRestrict = true;
                gbRes.Visible = false;
            }
        }

        private void btnResClr_Click(object sender, EventArgs e)
        {
            chkCustomer.Checked = false;
            chkNIC.Checked = false;
            chkPP.Checked = false;
            chkDL.Checked = false;
            chkMobile.Checked = false;
            _isRestrict = false;
            gbRes.Visible = false;
        }

        private void tbPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbPrice.SelectedIndex == 0)
            {

                //check permission
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11044))
                {
                    btnMainSave.Enabled = true;
                }
                else
                {
                    btnMainSave.Enabled = false;
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11050))
                {
                    btnCancel.Enabled = true;
                }
                else
                {
                    btnCancel.Enabled = false;
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11056))
                {
                    btnAmend.Enabled = true;
                }
                else
                {
                    btnAmend.Enabled = false;
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11057))
                {
                    btnSaveAs.Enabled = true;
                }
                else
                {
                    btnSaveAs.Enabled = false;
                }
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10087))
                {
                    btnPAApprove.Enabled = true;
                }
                else
                {
                    btnPAApprove.Enabled = false;
                }
            }
            else if (tbPrice.SelectedIndex == 1)
            {
                //check permission
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11045))
                {
                    btnSaveSimilar.Enabled = true;
                }
                else
                {
                    btnSaveSimilar.Enabled = false;
                }
            }
            else if (tbPrice.SelectedIndex == 2)
            {
                //check permission
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11046))
                {
                    btnMSave.Enabled = true;
                }
                else
                {
                    btnMSave.Enabled = false;
                }
            }
            else if (tbPrice.SelectedIndex == 3)
            {
                //check permission
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11047))
                {
                    btnAddUpdate.Enabled = true;
                }
                else
                {
                    btnAddUpdate.Enabled = false;
                }
            }
            else if (tbPrice.SelectedIndex == 7)
            {
                //check permission Nadeeka 03-06-2015
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11066))
                {
                    btndSave.Enabled = true;
                }
                else
                {
                    btndSave.Enabled = false;
                }
            }
            else
            {
                //check permission
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11054))
                {
                    btnAgingSave.Enabled = true;
                }
                else
                {
                    btnAgingSave.Enabled = false;
                }

            }

        }

        #region age pric activation region -BY SACHITH

        #region aging properties

        List<Tuple<string, decimal>> _ageItems = new List<Tuple<string, decimal>>();
        DataTable select_ITEMS_List = new DataTable();

        #endregion

        private void btnAgingClear_Click(object sender, EventArgs e)
        {
            txtAgeUpload.Text = "";
            txtAgeCloPb.Text = "";
            txtAgeCloPlevl.Text = "";
            txtAgeOriPb.Text = "";
            txtAgeOriPlevel.Text = "";
            txtAgeCircular.Text = "";
            txtAgeBrand.Text = "";
            txtAgeCate1.Text = "";
            txtAgeCate2.Text = "";
            txtItemCD.Text = "";
            gvPreview.DataSource = null;
            grvItemList.DataSource = null;

        }

        private void btnAgingSave_Click(object sender, EventArgs e)
        {
            //validation
            try
            {
                if (string.IsNullOrEmpty(txtAgeCircular.Text))
                {
                    MessageBox.Show("Please enter circular number.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeCircular.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    MessageBox.Show("Please enter Original Price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeOriPlevel.Text))
                {
                    MessageBox.Show("Please enter Original Price Level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    MessageBox.Show("Please enter Clone Price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeCloPlevl.Text))
                {
                    MessageBox.Show("Please enter Clone Price Level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11054))
                {
                    MessageBox.Show("You do not have permission 11054", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                btnAgingSave.Enabled = false;
                //save function
                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    _ageItems = new List<Tuple<string, decimal>>();
                    decimal _Disc = 0;
                    bool isDiscNum = Decimal.TryParse(txtAgeDisc.Text, out _Disc);
                    if (!isDiscNum)
                    {
                        MessageBox.Show("Entered discount is not a number.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    foreach (DataRow _dr in select_ITEMS_List.Rows)
                    {
                        _ageItems.Add(new Tuple<string, decimal>(_dr["Code"].ToString(), _Disc));
                    }

                }

                string _error;
                int _result = CHNLSVC.Sales.SaveAgePriceActivation(_ageItems, txtAgeOriPb.Text.Trim(), txtAgeOriPlevel.Text.Trim(), txtAgeCloPb.Text.Trim(), txtAgeCloPlevl.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtAgeCircular.Text, out _error, dtAgFrom.Value.Date, dtAgTo.Value.Date);
                if (_result == -1)
                {
                    MessageBox.Show("Error occured while processing\n" + _error, "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Sucessfully Saved.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                btnAgingSave.Enabled = false;
                btnAgingClear_Click(null, null);
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

        private void btnAgeUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    MessageBox.Show("You have selected Items in list also.\nPlease Clear Item list before upload.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 row_aff = 0;
                string _msg = string.Empty;
                if (string.IsNullOrEmpty(txtAgeUpload.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFilePath.Text = "";
                    txtFilePath.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtAgeCircular.Text))
                {
                    MessageBox.Show("Please enter circular #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Text = "";
                    txtCircular.Focus();
                    return;
                }


                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtAgeUpload.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeUpload.Focus();
                    return;
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension == ".xls")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension == ".xlsx")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }


                conStr = String.Format(conStr, txtAgeUpload.Text, "NO");
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


                List<string> _itemLst = new List<string>();

                //add to list
                foreach (DataRow _row in dt.Rows)
                {
                    string _item = _row[0].ToString();
                    string _discount = _row[1].ToString();
                    MasterItem _msItem;
                    if (!string.IsNullOrEmpty(_item))
                    {
                        _msItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        if (_msItem == null)
                        {
                            MessageBox.Show("Invalid Item Code.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    decimal _tempDisc = 0;
                    bool _isDiscNumber = Decimal.TryParse(_discount, out  _tempDisc);
                    if (!_isDiscNumber)
                    {
                        MessageBox.Show("Item - " + _item + "\nDiscount Rate is not number", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var _dup = _itemLst.Where(x => x == _row[0].ToString()).ToList();
                    if (_dup != null && _dup.Count > 0)
                    {
                        MessageBox.Show("Item - " + _row[0] + "\nis duplicate.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    _itemLst.Add(_row[0].ToString().Trim());
                    _ageItems.Add(new Tuple<string, decimal>(_msItem.Mi_cd, Convert.ToDecimal(_discount)));
                }

                MessageBox.Show("Upload Sucessful!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void btnAgeBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtAgeUpload.Text = openFileDialog1.FileName;
        }

        private void btnAgeOriPb_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeOriPb;
                _CommonSearch.ShowDialog();
                txtAgeOriPb.Select();
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

        private void btnAgeOriPlevl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Clear();
                    txtAgeOriPb.Focus();
                    return;
                }
                _Stype = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeOriPlevel;
                _CommonSearch.ShowDialog();
                txtAgeOriPlevel.Select();

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

        private void btnAgeCloPb_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeCloPb;
                _CommonSearch.ShowDialog();
                txtAgeCloPb.Select();
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

        private void btnAgeCloPlevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeCloPb.Clear();
                    txtAgeCloPb.Focus();
                    return;
                }
                _Stype = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters2(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeCloPlevl;
                _CommonSearch.ShowDialog();
                txtAgeCloPlevl.Select();

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

        private void txtAgeCircular_Leave(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtCircular.Text, txtPromoCode.Text, txtBook.Text, txtLevel.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show("Entered Circular number already in use.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCircular.Text = "";
                return;
            }
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeBrand;
                //_CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtAgeBrand.Focus();
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

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeCate1;
                // _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtAgeCate1.Focus();
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

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAgeCate2;
                //  _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtAgeCate2.Focus();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters1(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.ShowDialog();
                txtItemCD.Focus();
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

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            if (txtAgeBrand.Text.Trim() == "" && txtAgeCate1.Text.Trim() == "" && txtAgeCate2.Text.Trim() == "" && txtItemCD.Text.Trim().ToUpper() == "")
            {
                MessageBox.Show("Please select a searching value", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //select_ITEMS_List = new DataTable();

            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("ITEM", txtAgeBrand.Text.Trim(), txtAgeCate1.Text.Trim(), txtAgeCate2.Text.Trim(), null, txtItemCD.Text.Trim(), string.Empty, string.Empty, string.Empty);
            select_ITEMS_List.Merge(dt);
            grvItemList.DataSource = null;
            grvItemList.AutoGenerateColumns = false;
            grvItemList.DataSource = select_ITEMS_List;
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            grvItemList.DataSource = null;
            grvItemList.AutoGenerateColumns = false;
            select_ITEMS_List = new DataTable();
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvItemList.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvItemList.EndEdit();
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

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvItemList.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvItemList.EndEdit();
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

        private void btnAgeView_Click(object sender, EventArgs e)
        {
            //validation
            try
            {
                if (string.IsNullOrEmpty(txtAgeCircular.Text))
                {
                    MessageBox.Show("Please enter circular number.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeCircular.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeOriPb.Text))
                {
                    MessageBox.Show("Please enter Original Price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeOriPlevel.Text))
                {
                    MessageBox.Show("Please enter Original Price Level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeCloPb.Text))
                {
                    MessageBox.Show("Please enter Clone Price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }
                if (string.IsNullOrEmpty(txtAgeCloPlevl.Text))
                {
                    MessageBox.Show("Please enter Clone Price Level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Focus();
                }

                if (select_ITEMS_List != null && select_ITEMS_List.Rows.Count > 0)
                {
                    _ageItems = new List<Tuple<string, decimal>>();
                    decimal _Disc = 0;
                    bool isDiscNum = Decimal.TryParse(txtAgeDisc.Text, out _Disc);
                    if (!isDiscNum)
                    {
                        MessageBox.Show("Entered discount is not a number.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    foreach (DataRow _dr in select_ITEMS_List.Rows)
                    {
                        _ageItems.Add(new Tuple<string, decimal>(_dr["Code"].ToString(), _Disc));
                    }

                }

                gvPreview.AutoGenerateColumns = false;
                string _error;
                DataTable _priceList;
                List<MasterCompany> _s = new List<MasterCompany>();
                _s.ToDataTable();
                CHNLSVC.Sales.GetAgePriceActivation(_ageItems, txtAgeOriPb.Text.Trim(), txtAgeOriPlevel.Text.Trim(), txtAgeCloPb.Text.Trim(), txtAgeCloPlevl.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtAgeCircular.Text, out _error, out _priceList);
                gvPreview.DataSource = _priceList;
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

        private void txtAgeOriPb_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeOriPb.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtAgeOriPb.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeOriPb.Clear();
                    txtAgeOriPb.Focus();
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

        private void txtAgeCloPb_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCloPb.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtAgeCloPb.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAgeCloPb.Clear();
                    txtAgeCloPb.Focus();
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

        private void txtAgeCloPlevl_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeCloPlevl.Text)) return;
                if (string.IsNullOrEmpty(txtAgeCloPb.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAgeCloPb.Clear(); txtAgeCloPb.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtAgeCloPb.Text.Trim(), txtAgeCloPlevl.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAgeCloPlevl.Clear(); txtAgeCloPlevl.Focus(); return; }
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

        private void txtAgeOriPlevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAgeOriPlevel.Text)) return;
                if (string.IsNullOrEmpty(txtAgeOriPb.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAgeOriPb.Clear(); txtAgeOriPb.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtAgeOriPb.Text.Trim(), txtAgeOriPlevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAgeOriPlevel.Clear(); txtAgeOriPlevel.Focus(); return; }
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
        #endregion

        private void btnRePayClose_Click(object sender, EventArgs e)
        {
            dgvType.AutoGenerateColumns = false;
            dgvType.DataSource = new DataTable();
            pnlPriceTp.Visible = false;
        }

        private void dgvType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvType.Rows.Count <= 0) return;

                Int32 _line = Convert.ToInt32(dgvType.Rows[e.RowIndex].Cells["sarpt_indi"].Value);

                lstPromoItem.Clear();
                DataTable dt = CHNLSVC.Sales.GetPromobyCirAndTp(txtCircular.Text, _line);

                //var _lst = dt.AsEnumerable().Select(X => X.Field<string>("esep_cat_cd")).Distinct().ToList();


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstPromoItem.Items.Add(drow["sapd_promo_cd"].ToString());
                    }

                    _isRecal = true;
                    btnCancel.Enabled = true;
                    //btnMainSave.Enabled = false;
                    cmbPriceType.Enabled = false;
                    chkEndDate.Text = "Change end Date";
                    pnlPriceTp.Visible = false;
                }
                else
                {
                    MessageBox.Show("Please check enter circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular.Text = "";
                    _isRecal = false;
                    btnMainSave.Enabled = true;
                    btnAmend.Enabled = false;
                    btnCancel.Enabled = false;
                    pnlPriceTp.Visible = false;
                    txtCircular.Focus();
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

        #region :: Promotion Voucher Definition Tab :: By Chamal 18-Jun-2014

        private List<PromoVoucherDefinition> _schemeProcess = new List<PromoVoucherDefinition>();

        private void ClearProVouItems()
        {
            txtPB_pv.Clear();
            txtPL_pv.Clear();
            txtCat1_pv.Clear();
            txtCat2_pv.Clear();
            txtCat3_pv.Clear();
            txtItem_pv.Clear();
            txtBrand_pv.Clear();
            txtFileName_pv.Clear();
            lstPDItems.Clear();
            txtProVouType.Clear();
            txtProVouTypeDesc.Clear();
            optVou1.Checked = true;

            txtPB_pv.Enabled = false;
            txtPB_pv.Enabled = false;
            txtPL_pv.Enabled = false;
            txtCat1_pv.Enabled = false;
            txtCat2_pv.Enabled = false;
            txtCat3_pv.Enabled = false;
            txtItem_pv.Enabled = false;
            txtBrand_pv.Enabled = false;
            txtFileName_pv.Enabled = false;
            btnSearchFile_spv.Enabled = false;
            btnUploadFile_spv.Enabled = false;
            btnLoadPara_pv.Enabled = false;
            lstPDItems.Enabled = false;
            btnSelectAll_pv.Enabled = false;
            btnUnselectAll_pv.Enabled = false;
            btnClear_pv.Enabled = false;

            _Stype = "";
            _Ltype = "";
            HandleCobjects(true);

        }

        private void cmbProVouDefType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvVouCat.Visible = false;
            pnlSaleTp.Visible = false;
            if (cmbProVouDefType.Text == "Voucher Type(s)")
            {
                ClearProVouItems();
                tbMainCommDef.SelectedTab = tbProVouType;
                txtProVouType.Focus();
                HandleCobjects(false);
                pnlSaleTp.Visible = true;
            }
            else if (cmbProVouDefType.Text == "Product Wise")
            {
                ClearProVouItems();
                tbMainCommDef.SelectedTab = tbProVouItem;
                txtCat1_pv.Enabled = true;
                txtCat2_pv.Enabled = true;
                txtCat3_pv.Enabled = true;
                txtItem_pv.Enabled = true;
                txtBrand_pv.Enabled = true;
                txtFileName_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSearchFile_spv.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text == "Brand Wise")
            {
                ClearProVouItems();
                tbMainCommDef.SelectedTab = tbProVouItem;
                txtBrand_pv.Enabled = true;
                txtBrand_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;


                txtFileName_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSearchFile_spv.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;

                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text == "Main Category Wise")
            {
                ClearProVouItems();
                tbMainCommDef.SelectedTab = tbProVouItem;
                txtCat1_pv.Enabled = true;
                txtCat1_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;


                txtFileName_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSearchFile_spv.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text == "Sub Category Wise")
            {
                ClearProVouItems();
                tbMainCommDef.SelectedTab = tbProVouItem;
                txtCat1_pv.Enabled = true;
                txtCat2_pv.Enabled = true;
                txtCat1_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSelectAll_pv.Enabled = true;
                btnUnselectAll_pv.Enabled = true;
                btnClear_pv.Enabled = true;

                gvVouCat.Visible = true;

                txtFileName_pv.Enabled = true;
                lstPDItems.Enabled = true;
                btnSearchFile_spv.Enabled = true;
                btnUploadFile_spv.Enabled = true;
                btnLoadPara_pv.Enabled = true;
                lstPDItems.Enabled = true;
                txtPB_pv.Focus();
            }
            else if (cmbProVouDefType.Text == "Price Book Wise")
            {
                ClearProVouItems();
                tbMainCommDef.SelectedTab = tbProVouItem;
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                txtPB_pv.Focus();
                txtPB_pv.Enabled = true;
                txtPL_pv.Enabled = true;
                txtPB_pv.Focus();
            }
            if (cmbProVouDefType.Text != "Voucher Type(s)")
            {
                txtVochCode_pv.Focus();
            }
        }

        private void tbMainCommDef_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (cmbProVouDefType.Text == "Price Book Wise" || cmbProVouDefType.Text == "Product Wise" || cmbProVouDefType.Text == "Main Category Wise" || cmbProVouDefType.Text == "Sub Category Wise" || cmbProVouDefType.Text == "Brand Wise")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[0])
                {
                    MessageBox.Show("Not allow for above type.", "Promotion Voucher Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HandleCobjects(true);
                    tbMainCommDef.SelectTab(0);
                    return;

                }
            }
            else if (cmbProVouDefType.Text == "Voucher Type(s)")
            {
                if (tbMainCommDef.SelectedTab != tbMainCommDef.TabPages[1])
                {
                    MessageBox.Show("Not allow for above type.", "Promotion Voucher Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HandleCobjects(false);
                    tbMainCommDef.SelectTab(1);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Select Definition Type.", "Promotion Voucher Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbProVouDefType.Focus();
                cmbProVouDefType.Text = "Product Wise";
                tbMainCommDef.SelectTab(0);
                return;

            }
        }

        #region Pbook Validation
        private void btnPB_spv_Click(object sender, EventArgs e)
        {
            try
            {
                txtPL_pv.Clear();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPB_pv;
                _CommonSearch.ShowDialog();
                txtPB_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPB_pv_DoubleClick(object sender, EventArgs e)
        {
            btnPB_spv_Click(null, null);
        }

        private void txtPB_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPB_pv.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtPB_pv.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Promotion Vouchers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPB_pv.Clear();
                    txtPB_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPB_pv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnPB_spv_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtPL_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region PLevel Validation
        private void btnPL_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPB_pv.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPB_pv.Clear();
                    txtPB_pv.Focus();
                    return;
                }
                _Stype = "PromoVou";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPL_pv;
                _CommonSearch.ShowDialog();
                txtPL_pv.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPL_pv_DoubleClick(object sender, EventArgs e)
        {
            btnPL_spv_Click(null, null);
        }

        private void txtPL_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPB_pv.Text))
                {
                    PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPB_pv.Text.Trim(), txtPL_pv.Text.Trim());
                    if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                    {
                        MessageBox.Show("Please enter valid price level.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPL_pv.Clear();
                        txtPL_pv.Focus();
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPL_pv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtPB_pv.Text))
                    { MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPB_pv.Clear(); txtPB_pv.Focus(); return; }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPL_pv;
                    _CommonSearch.ShowDialog();
                    txtPL_pv.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtChnnl_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Cate1 Validation
        private void btnCat1_spv_Click(object sender, EventArgs e)
        {
            try
            {
                txtCat2_pv.Clear();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat1_pv;
                _CommonSearch.ShowDialog();
                txtCat1_pv.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCat1_pv_DoubleClick(object sender, EventArgs e)
        {
            btnCat1_spv_Click(null, null);
        }

        private void txtCat1_pv_KeyDown(object sender, KeyEventArgs e)
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
                    _CommonSearch.obj_TragetTextBox = txtCat1_pv;
                    _CommonSearch.ShowDialog();
                    txtCat1_pv.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtCat2_pv.Enabled == true) txtCat2_pv.Focus();
                    else if (txtCat3_pv.Enabled == true) txtCat3_pv.Focus();
                    else if (txtBrand_pv.Enabled == true) txtBrand_pv.Focus();
                    else if (txtItem_pv.Enabled == true) txtItem_pv.Focus();
                    else if (btnLoadPara_pv.Enabled == true) btnLoadPara_pv.Focus();
                    else txtChnnl_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCat1_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCat1_pv.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCat1_pv.Clear();
                    txtCat1_pv.Focus();
                    return;
                }
                txtCat2_pv.Clear();
                txtCat3_pv.Clear();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Cate2 Validation
        private void btnCat2_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text))
                {
                    MessageBox.Show("Please select the main category code", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCat1_pv.Clear();
                    txtCat1_pv.Focus();
                    return;
                }
                _Stype = "PromoVou";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat2_pv;
                _CommonSearch.ShowDialog();
                txtCat2_pv.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtCat2_pv_DoubleClick(object sender, EventArgs e)
        {
            btnCat2_spv_Click(null, null);
        }

        private void txtCat2_pv_Leave(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtCat2_pv.Text))
                {
                    if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    {
                        MessageBox.Show("Please select the main category code", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCat1_pv.Clear();
                        txtCat1_pv.Focus();
                        return;
                    }

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCat2_pv.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Please select the valid sub category code", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCat2_pv.Clear();
                        txtCat2_pv.Focus();
                        return;
                    }

                    txtCat3_pv.Clear();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCat2_pv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnCat2_spv_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtCat2_pv.Enabled == true) txtCat2_pv.Focus();
                    else if (txtCat3_pv.Enabled == true) txtCat3_pv.Focus();
                    else if (txtBrand_pv.Enabled == true) txtBrand_pv.Focus();
                    else if (txtItem_pv.Enabled == true) txtItem_pv.Focus();
                    else if (btnLoadPara_pv.Enabled == true) btnLoadPara_pv.Focus();
                    else txtChnnl_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Cate3 Validation
        private void btnCat3_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCat1_pv.Text))
                {
                    MessageBox.Show("Please select the main category code", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCat1_pv.Clear();
                    txtCat1_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCat2_pv.Text))
                {
                    MessageBox.Show("Please select the sub category code", "Sub Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCat2_pv.Clear();
                    txtCat2_pv.Focus();
                    return;
                }
                txtBrand_pv.Clear();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCat3_pv;
                _CommonSearch.ShowDialog();
                txtCat3_pv.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCat3_pv_DoubleClick(object sender, EventArgs e)
        {
            btnCat3_spv_Click(null, null);
        }

        private void txtCat3_pv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnCat3_spv_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (txtBrand_pv.Enabled == true) txtBrand_pv.Focus();
                    else if (txtItem_pv.Enabled == true) txtItem_pv.Focus();
                    else if (btnLoadPara_pv.Enabled == true) btnLoadPara_pv.Focus();
                    else txtChnnl_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCat3_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat3_pv.Text))
                {
                    if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    {
                        MessageBox.Show("Please select the main category code", "Main Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCat1_pv.Clear();
                        txtCat1_pv.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCat2_pv.Text))
                    {
                        MessageBox.Show("Please select the sub category code", "Sub Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCat2_pv.Clear();
                        txtCat2_pv.Focus();
                        return;
                    }

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtCat3_pv.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Please select the valid item range.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCat3_pv.Clear();
                        txtCat3_pv.Focus();
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Brand Validation
        private void btnBrand_spv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand_pv;
                _CommonSearch.ShowDialog();
                txtBrand_pv.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBrand_pv_DoubleClick(object sender, EventArgs e)
        {
            btnBrand_spv_Click(null, null);
        }

        private void txtBrand_pv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnBrand_spv_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtChnnl_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBrand_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrand_pv.Text))
                {
                    MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(txtBrand_pv.Text.Trim());
                    if (_brd.Mb_cd == null)
                    {
                        MessageBox.Show("Please select the valid brand.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBrand_pv.Clear();
                        txtBrand_pv.Focus();
                        return;
                    }

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Item Validation
        private void btnItem_spv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem_pv;
                _CommonSearch.ShowDialog();
                txtItem_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_pv_DoubleClick(object sender, EventArgs e)
        {
            btnItem_spv_Click(null, null);
        }

        private void txtItem_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItem_pv.Text))
                {
                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem_pv.Text);
                    if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the item code", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem_pv.Clear();
                        txtItem_pv.Focus();
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtItem_pv_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnItem_spv_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (btnLoadPara_pv.Enabled == true)
                    { btnLoadPara_pv.Focus(); }
                    else
                    { txtChanel.Focus(); }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Item Load

        private void btnLoadPara_pv_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtCat1_pv.Text) && string.IsNullOrEmpty(txtCat2_pv.Text) && string.IsNullOrEmpty(txtCat3_pv.Text) && string.IsNullOrEmpty(txtBrand_pv.Text) && string.IsNullOrEmpty(txtItem_pv.Text))
                {
                    MessageBox.Show("Please enter searching parameters.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCat1_pv.Focus();
                    return;
                }

                //  Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(txtItem_pv.Text))
                {
                    MasterCompanyItem Item = CHNLSVC.Sales.GetAllCompanyItems(BaseCls.GlbUserComCode, txtItem_pv.Text, 1);
                    if (Item != null)
                    {
                        foreach (ListViewItem item in lstPDItems.Items)
                        {
                            if (item.Text == txtItem_pv.Text.Trim())
                            {
                                MessageBox.Show("Already Exist.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtItem_pv.Focus();
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                        lstPDItems.Items.Add(txtItem_pv.Text.Trim());
                    }
                    //if (dtCompanyItems.Select("mci_itm_cd = '" + txtItem_pv.Text.Trim() + "'").Length > 0)
                    //{
                    //    lstPDItems.Items.Add(txtItem_pv.Text.Trim());
                    //}
                }

                else if (cmbProVouDefType.Text == "Brand Wise" && !string.IsNullOrEmpty(txtBrand_pv.Text))
                {

                    foreach (ListViewItem item in lstPDItems.Items)
                    {
                        if (item.Text == txtBrand_pv.Text.Trim())
                        {
                            MessageBox.Show("Already Exist.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtBrand_pv.Focus();
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                    }
                    //ListViewItem oItem = new ListViewItem();
                    //oItem.Text = txtBrand_pv.Text.Trim();
                    //if (lstPDItems.Items.Contains(oItem))
                    //{
                    //    MessageBox.Show("Already Exist.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand_pv.Focus();
                    //    return;
                    //}
                    lstPDItems.Items.Add(txtBrand_pv.Text.Trim());
                }
                else if (cmbProVouDefType.Text == "Main Category Wise" && !string.IsNullOrEmpty(txtCat1_pv.Text))
                {
                    foreach (ListViewItem item in lstPDItems.Items)
                    {
                        if (item.Text == txtCat1_pv.Text.Trim())
                        {
                            MessageBox.Show("Already Exist.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCat1_pv.Focus();
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                    }
                    lstPDItems.Items.Add(txtCat1_pv.Text.Trim());
                }
                else if (cmbProVouDefType.Text == "Sub Category Wise")
                {
                    if (_lstcate2 == null)
                    {
                        _lstcate2 = new List<MasterItemSubCate>();
                    }

                    if (_lstcate2 != null)
                    {

                        MasterItemSubCate result = _lstcate2.Find(x => x.Ric2_cd == txtCat2_pv.Text && x.Ric2_cd1 == txtCat1_pv.Text);
                        if (result != null)
                        {

                            MessageBox.Show("Already Exist", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                    }

                    MasterItemSubCate _cate2 = new MasterItemSubCate();
                    _cate2.Ric2_cd = txtCat2_pv.Text;
                    _cate2.Ric2_cd1 = txtCat1_pv.Text;
                    _lstcate2.Add(_cate2);

                    gvVouCat.DataSource = null;
                    gvVouCat.AutoGenerateColumns = false;
                    gvVouCat.DataSource = new List<MasterItemSubCate>();
                    gvVouCat.DataSource = _lstcate2;

                }
                else
                {
                    DataTable _dtItm = CHNLSVC.Sales.GetItemsByCateAndBrandNew(txtCat1_pv.Text, txtCat2_pv.Text, txtCat3_pv.Text, txtBrand_pv.Text, BaseCls.GlbUserComCode);

                    if (_dtItm.Rows.Count > 0)
                    {
                        DataTable dtCompanyItems = channelService.General.GetCompanyItemsByCompany(BaseCls.GlbUserComCode);

                        foreach (DataRow drow in _dtItm.Rows)
                        {
                            if (dtCompanyItems.Select("mci_itm_cd = '" + drow["mi_cd"].ToString() + "'").Length > 0)
                            {
                                lstPDItems.Items.Add(drow["mi_cd"].ToString());
                            }
                        }
                    }

                }
                Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtFileName_pv.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName_pv.Text = openFileDialog1.FileName;
        }

        private List<string> _itemLst = null;

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            lstPDItems.Clear();
            if (string.IsNullOrEmpty(txtFileName_pv.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName_pv.Clear();
                txtFileName_pv.Focus();
                return;
            }

            //System.IO.FileInfo _fileObj = new System.IO.FileInfo(txtFileName_pv.Text);

            //if (_fileObj.Exists == false)
            //{
            //    MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtFileName_pv.Focus();
            //    return;
            //}

            //string _extension = _fileObj.Extension;
            //string _conStr = string.Empty;

            //if (_extension.ToUpper() == ".XLS")
            //{
            //    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName_pv.Text + "; Extended Properties='Excel 8.0;HDR=YES;'";
            //}
            //else if (_extension.ToUpper() == ".XLSX")
            //{
            //    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName_pv.Text + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
            //}
            //else
            //{
            //    MessageBox.Show("Please Select valid Ms Excel File.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //Cursor.Current = Cursors.WaitCursor;

            //_conStr = String.Format(_conStr, txtFileName_pv.Text, "NO");
            //OleDbConnection _connExcel = new OleDbConnection(_conStr);
            //OleDbCommand _cmdExcel = new OleDbCommand();
            //OleDbDataAdapter _oda = new OleDbDataAdapter();
            //DataTable _dt = new DataTable();
            //_cmdExcel.Connection = _connExcel;

            ////Get the name of First Sheet
            //_connExcel.Open();
            //DataTable _dtExcelSchema;
            //_dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //_connExcel.Close();

            //_connExcel.Open();
            //_cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            //_oda.SelectCommand = _cmdExcel;
            //_oda.Fill(_dt);
            //_connExcel.Close();

            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName_pv.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGvBrowse.Focus();
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


            conStr = String.Format(conStr, txtFileName_pv.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
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
            oda.Fill(_dt);
            connExcel.Close();




            _itemLst = new List<string>();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {



                foreach (DataRow _dr in _dt.Rows)
                {

                    if (cmbProVouDefType.Text == "Product Wise")
                    {
                        DataTable dtCompanyItems = channelService.General.GetCompanyItemsByCompany(BaseCls.GlbUserComCode);
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

                        if (dtCompanyItems.Select("mci_itm_cd = '" + _dr[0].ToString().Trim() + "'").Length > 0)
                        {
                            lstPDItems.Items.Add(_dr[0].ToString().Trim());
                        }
                    }
                    else if (cmbProVouDefType.Text == "Brand Wise")
                    {
                        if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                        MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                        if (_brd.Mb_cd == null)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                            else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                            continue;
                        }
                        var _dup = _itemLst.Where(x => x == _dr[1].ToString()).ToList();
                        if (_dup != null && _dup.Count > 1)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand " + _dr[1].ToString() + " duplicate");
                            else _errorLst.Append(" and brand " + _dr[1].ToString() + " duplicate");
                            continue;
                        }


                        lstPDItems.Items.Add(_dr[1].ToString().Trim());


                    }


                    else if (cmbProVouDefType.Text == "Main Category Wise")
                    {
                        if (string.IsNullOrEmpty(_dr[2].ToString())) continue;

                        DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                        if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                            else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                            continue;
                        }
                        var _dup = _itemLst.Where(x => x == _dr[2].ToString()).ToList();
                        if (_dup != null && _dup.Count > 2)
                        {
                            if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr[2].ToString() + " duplicate");
                            else _errorLst.Append(" and main category " + _dr[2].ToString() + " duplicate");
                            continue;
                        }


                        lstPDItems.Items.Add(_dr[2].ToString().Trim());

                    }


                    else if (cmbProVouDefType.Text == "Sub Category Wise")
                    {
                        if (!string.IsNullOrEmpty(_dr[2].ToString()) && !string.IsNullOrEmpty(_dr[3].ToString()))
                        {
                            if (_lstcate2 == null)
                            {
                                _lstcate2 = new List<MasterItemSubCate>();
                            }

                            MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                            if (subCate.Ric2_cd1 == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[3].ToString());
                                else _errorLst.Append(" and invalid sub category  - " + _dr[3].ToString());
                                continue;
                            }
                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                continue;
                            }


                            if (_lstcate2 != null)
                            {

                                MasterItemSubCate result = _lstcate2.Find(x => x.Ric2_cd == _dr[3].ToString().Trim() && x.Ric2_cd1 == _dr[2].ToString().Trim());
                                if (result != null)
                                {

                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category/Sub Category " + _dr[2].ToString() + "/" + _dr[3].ToString() + " duplicate");
                                    else _errorLst.Append(" and main category/Sub Category  " + _dr[2].ToString() + "/" + _dr[3].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            MasterItemSubCate _cate2 = new MasterItemSubCate();
                            _cate2.Ric2_cd = _dr[3].ToString().Trim();
                            _cate2.Ric2_cd1 = _dr[2].ToString().Trim();
                            _lstcate2.Add(_cate2);

                            gvVouCat.DataSource = null;
                            gvVouCat.AutoGenerateColumns = false;
                            gvVouCat.DataSource = new List<MasterItemSubCate>();
                            gvVouCat.DataSource = _lstcate2;
                        }

                    }

                }

                Cursor.Current = Cursors.Default;

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        ////  _itemLst = new List<string>();
                    }
                }

            }
        }

        private void btnSelectAll_pv_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstPDItems.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnselectAll_pv_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPDItems.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnClear_pv_Click(object sender, EventArgs e)
        {
            txtPB_pv.Clear();
            txtPL_pv.Clear();
            txtCat1_pv.Clear();
            txtCat2_pv.Clear();
            txtCat3_pv.Clear();
            txtItem_pv.Clear();
            txtBrand_pv.Clear();
            txtFileName_pv.Clear();
            lstPDItems.Clear();
            // gvVouCat.Rows.Clear();
            _lstcate2 = new List<MasterItemSubCate>();
            gvVouCat.DataSource = null;
            gvVouCat.AutoGenerateColumns = false;
            gvVouCat.DataSource = new List<MasterItemSubCate>();
            gvVouCat.DataSource = _lstcate2;


        }

        #endregion

        #region Applicable Profit Centers and Sub Channels
        private void cmbDefby_pv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDefby_pv.Text == "Profit Center")
            {
                txtChnnl_pv.Text = "";
                txtSChnnl_pv.Text = "";
                txtPC_pv.Text = "";
                txtChnnl_pv.Enabled = true;
                txtPC_pv.Enabled = true;
                txtSChnnl_pv.Enabled = true;
                btnChnnl_spv.Enabled = true;
                btnPC_spv.Enabled = true;
                btnSChnnl_spv.Enabled = true;
                lstLocations.Clear();
                txtPC_pv.Focus();

                txtChnnl_pv.Enabled = false;
                txtSChnnl_pv.Enabled = false;
                btnSChnnl_spv.Enabled = false;
                btnChnnl_spv.Enabled = false;

                btnPC_spv.Enabled = true;

            }
            else if (cmbDefby_pv.Text == "Sub Channel")
            {
                txtChnnl_pv.Text = "";
                txtSChnnl_pv.Text = "";
                txtPC_pv.Text = "";
                txtChnnl_pv.Enabled = true;
                txtPC_pv.Enabled = false;
                txtSChnnl_pv.Enabled = true;
                btnChnnl_spv.Enabled = true;
                btnPC_spv.Enabled = false;
                btnSChnnl_spv.Enabled = true;
                lstLocations.Clear();
                txtChnnl_pv.Focus();

                txtPC_pv.Enabled = false;
                btnPC_spv.Enabled = false;

                txtChnnl_pv.Enabled = true;
                txtSChnnl_pv.Enabled = true;
                btnSChnnl_spv.Enabled = true;
                btnChnnl_spv.Enabled = true;
            }
        }

        private void btnChnnl_spv_Click(object sender, EventArgs e)
        {
            try
            {
                txtSChnnl_pv.Clear();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChnnl_pv;
                _CommonSearch.ShowDialog();
                txtChnnl_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtChnnl_pv_DoubleClick(object sender, EventArgs e)
        {
            btnChnnl_spv_Click(null, null);
        }

        private void btnSChnnl_spv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChnnl_pv.Text))
                {
                    MessageBox.Show("Please select channel.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChnnl_pv.Clear();
                    txtChnnl_pv.Focus();
                    return;
                }
                _Ltype = "PromoVou";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChnnl_pv;
                _CommonSearch.ShowDialog();
                txtSChnnl_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSChnnl_pv_DoubleClick(object sender, EventArgs e)
        {
            btnSChnnl_spv_Click(null, null);
        }

        private void btnPC_spv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC_pv;
                _CommonSearch.ShowDialog();
                txtPC_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPC_pv_DoubleClick(object sender, EventArgs e)
        {
            btnPC_spv_Click(null, null);
        }

        private void btnSelectLocs_pv_Click(object sender, EventArgs e)
        {

            try
            {
                Base _basePage = new Base();
                if (cmbDefby_pv.Text == "Profit Center")
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChnnl_pv.Text, txtSChnnl_pv.Text, null, null, null, txtPC_pv.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!checkItemExists(drow["PROFIT_CENTER"].ToString()))
                        {
                            lstLocations.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                }
                else if (cmbDefby_pv.Text == "Sub Channel")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    if (!string.IsNullOrEmpty(txtSChnnl_pv.Text))
                    {
                        if (!checkItemExists(txtSChnnl_pv.Text))
                        {
                            lstLocations.Items.Add(txtSChnnl_pv.Text);
                        }

                        return;
                    }

                    if (!string.IsNullOrEmpty(txtChnnl_pv.Text))
                    {
                        _Ltype = "PromoVou";
                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _resultNew = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        foreach (DataRow drow in _resultNew.Rows)
                        {
                            if (!checkItemExists(drow["CODE"].ToString()))
                            {
                                lstLocations.Items.Add(drow["CODE"].ToString());
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (!checkItemExists(drow["CODE"].ToString()))
                            {
                                lstLocations.Items.Add(drow["CODE"].ToString());
                            }
                        }
                        return;
                    }
                }
                txtChnnl_pv.Clear();
                txtSChnnl_pv.Clear();
                txtPC_pv.Clear();
                txtPC_pv.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLocSelectAll_spc_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstLocations.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnLocUnselect_spc_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstLocations.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnLocClr_spc_Click(object sender, EventArgs e)
        {
            txtChnnl_pv.Clear();
            txtSChnnl_pv.Clear();
            txtPC_pv.Clear();
            lstLocations.Clear();
            txtChnnl_pv.Focus();
        }
        #endregion

        #region Main Parameter
        private void cmbDisType_pv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDis_pv.Focus();
            }
        }

        private void cmbDisType_pv_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDis_pv.Clear();
            txtDis_pv.Focus();
        }

        private void txtDis_pv_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDis_pv.Text))
            {
                if (!IsNumeric(txtDis_pv.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDis_pv.Text = "";
                    txtDis_pv.Focus();
                    return;
                }

                if (cmbDisType_pv.Text == "RATE")
                {
                    if (Convert.ToDecimal(txtDis_pv.Text) > 100 || Convert.ToDecimal(txtDis_pv.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDis_pv.Text = "";
                        txtDis_pv.Focus();
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtDis_pv.Text) < 0)
                    {
                        MessageBox.Show("Value cannot be less than zero.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDis_pv.Text = "";
                        txtDis_pv.Focus();
                        return;
                    }
                }

                txtDis_pv.Text = Convert.ToDecimal(txtDis_pv.Text).ToString("n");
            }
        }

        private void btnSearchCircular_pv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionVoucherByCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular_pv;
                _CommonSearch.ShowDialog();
                txtCircular_pv.Select();
                FillPromotionVoucherDefinitionByCircular();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCircular_pv_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCircular_pv_Click(null, null);
        }
        #endregion

        private void btnCommAdd__pv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVochCode_pv.Text))
                {
                    MessageBox.Show("Please select voucher code.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVochCode_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbProVouDefType.Text))
                {
                    MessageBox.Show("Please select definition type.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbProVouDefType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPB_pv.Text))
                {
                    MessageBox.Show("Please select Price Book.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPB_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPL_pv.Text))
                {
                    MessageBox.Show("Please select Price Level.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPL_pv.Focus();
                    return;
                }
                if (select_ITEMS_List.Rows.Count < 0)
                {
                    MessageBox.Show("Please select Redeem Items.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnlRedeemItems.Visible = true;
                    return;
                }

                //Redeem Item panel validation
                if (grvRdmPb.Rows.Count == 0)
                {
                    MessageBox.Show("Please Add Redeem Price book/level.", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    return;
                }

                List<PromoVoucherDefinition> _recallList = new List<PromoVoucherDefinition>();

                _recallList = CHNLSVC.Sales.GetProVouhByCir(txtCircular_pv.Text.Trim()).FindAll(x => x.Spd_com == BaseCls.GlbUserComCode);

                if (_recallList.Count > 0 && _recallList != null)
                {

                    var _record = (from _lst in _recallList
                                   select _lst.Spd_stus).Distinct().ToList();


                    foreach (var tmpRec in _record)
                    {
                        if (tmpRec == false)
                        {
                            MessageBox.Show("The perticular circular is already cancelled.You cannot use this circular.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (tmpRec == true)
                        {
                            MessageBox.Show("The perticular circular is already approved.You cannot use this circular.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }

                if (string.IsNullOrEmpty(txtCircular_pv.Text))
                {
                    MessageBox.Show("Please enter circular #.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircular_pv.Focus();
                    return;
                }
                if (Convert.ToDateTime(dtpFromDate__pv.Value).Date < CHNLSVC.Security.GetServerDateTime().Date)
                {
                    MessageBox.Show("Valid date cannot back date.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromDate__pv.Focus();
                    return;
                }
                if (Convert.ToDateTime(dtpFromDate__pv.Value).Date > Convert.ToDateTime(dtpToDate_pv.Value).Date)
                {
                    MessageBox.Show("Valid To date cannot less than from date.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpToDate_pv.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDis_pv.Text))
                {
                    MessageBox.Show("Please enter discount rate / amount.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDis_pv.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbDisType_pv.Text))
                {
                    MessageBox.Show("Please select discount type.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbDisType_pv.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbPVInvoiceType.Text))
                {
                    MessageBox.Show("Please select Invoice type.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPVInvoiceType.Focus();
                    return;

                }
                //SchemeCreation ss = new SchemeCreation();

                Boolean _isDisRate = false;
                if (cmbDisType_pv.Text == "RATE")
                { _isDisRate = true; }
                else
                { _isDisRate = false; }

                if (cmbProVouDefType.Text == "Price Book Wise")
                {
                    if (string.IsNullOrEmpty(txtPB_pv.Text))
                    {
                        MessageBox.Show("Please enter price book.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPB_pv.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtPL_pv.Text))
                    {
                        MessageBox.Show("Please enter price level.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPL_pv.Focus();
                        return;
                    }
                }
                else if (cmbProVouDefType.Text == "Main Category Wise")
                {
                    if (string.IsNullOrEmpty(txtCat1_pv.Text))
                    {
                        //MessageBox.Show("Please enter main category.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtCat1_pv.Focus();
                        //return;
                    }

                    //if (string.IsNullOrEmpty(txtBrand_pv.Text))
                    //{
                    //    MessageBox.Show("Please enter brand.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtBrand_pv.Focus();
                    //    return;
                    //}
                }
                else if (cmbProVouDefType.Text == "Sub Category Wise")
                {
                    if (string.IsNullOrEmpty(txtCat2_pv.Text))
                    {
                        //MessageBox.Show("Please enter sub category.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtCat2_pv.Focus();
                        //return;
                    }
                }
                else if (cmbProVouDefType.Text == "Brand Wise")
                {
                    if (string.IsNullOrEmpty(txtBrand_pv.Text))
                    {
                        //MessageBox.Show("Please enter brand.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtBrand_pv.Focus();
                        //return;
                    }


                }
                else if (cmbProVouDefType.Text == "Product Wise")
                {
                    Boolean _isValidItm = false;
                    foreach (ListViewItem Item in lstPDItems.Items)
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
                        MessageBox.Show("No any applicable items are selected.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                Boolean _isPCFound = false;
                foreach (ListViewItem Item in lstLocations.Items)
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
                    MessageBox.Show("No any applicable profit center(s)/Channel(s) selected.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show("Confirm to apply details ?", "Promotion Voucher", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                Cursor.Current = Cursors.WaitCursor;
                PromoVoucherDefinition _tempProcess = new PromoVoucherDefinition();

                btnProVouSave.Enabled = false;
                btnCommAdd__pv.Enabled = false;
                int index = 0;

                if (cmbProVouDefType.Text == "Product Wise" || cmbProVouDefType.Text == "Brand Wise" || cmbProVouDefType.Text == "Main Category Wise")
                {
                    foreach (ListViewItem pcList in lstLocations.Items)
                    {

                        if (pcList.Checked == true)
                        {
                            foreach (ListViewItem itmList in lstPDItems.Items)
                            {
                                if (itmList.Checked == true)
                                {
                                    _tempProcess = new PromoVoucherDefinition();
                                    _tempProcess.Spd_seq = index;
                                    _tempProcess.Spd_com = BaseCls.GlbUserComCode;
                                    _tempProcess.Spd_sale_tp = cmbPVInvoiceType.Text;
                                    _tempProcess.Spd_stus = true;
                                    _tempProcess.Spd_vou_cd = txtVochCode_pv.Text;
                                    if (cmbProVouDefType.Text == "Product Wise")
                                    {
                                        _tempProcess.Spd_brd = null;
                                        _tempProcess.Spd_cat = null;
                                        _tempProcess.Spd_main_cat = null;
                                        _tempProcess.Spd_itm = itmList.Text.ToUpper().ToString();
                                    }
                                    else if (cmbProVouDefType.Text == "Brand Wise")
                                    {
                                        _tempProcess.Spd_brd = itmList.Text.ToUpper().ToString();
                                        _tempProcess.Spd_cat = null;
                                        _tempProcess.Spd_main_cat = null;
                                        _tempProcess.Spd_itm = null;
                                    }
                                    else if (cmbProVouDefType.Text == "Main Category Wise")
                                    {
                                        _tempProcess.Spd_brd = null;
                                        _tempProcess.Spd_cat = null;
                                        _tempProcess.Spd_main_cat = itmList.Text.ToUpper().ToString();
                                        _tempProcess.Spd_itm = null;
                                    }


                                    _tempProcess.Spd_circular_no = txtCircular_pv.Text;
                                    _tempProcess.Spd_cre_by = BaseCls.GlbUserID;
                                    _tempProcess.Spd_mod_by = BaseCls.GlbUserID;
                                    _tempProcess.Spd_disc = Convert.ToDecimal(txtDis_pv.Text);
                                    _tempProcess.Spd_disc_isrt = _isDisRate;
                                    _tempProcess.Spd_from_dt = dtpFromDate__pv.Value.Date;
                                    _tempProcess.Spd_to_dt = dtpToDate_pv.Value.Date;

                                    _tempProcess.Spd_pb = txtPB_pv.Text;
                                    _tempProcess.Spd_pb_lvl = txtPL_pv.Text;
                                    _tempProcess.Spd_pty_cd = pcList.Text.ToUpper().ToString();

                                    _tempProcess.Spd_period = _validPeriod;
                                    _tempProcess.Spd_rdm_com = BaseCls.GlbUserComCode;
                                    _tempProcess.Spd_rdm_pb = ".";
                                    _tempProcess.Spd_rdm_pb_lvl = ".";

                                    if (cmbDefby_pv.Text == "Profit Center")
                                    {
                                        _tempProcess.Spd_pty_tp = "PC";
                                    }
                                    else
                                    {
                                        _tempProcess.Spd_pty_tp = "SCHNL";
                                    }
                                    _schemeProcess.Add(_tempProcess);
                                }
                            }
                        }
                        index++;
                    }
                }

                else if (cmbProVouDefType.Text == "Sub Category Wise")
                {
                    foreach (ListViewItem pcList in lstLocations.Items)
                    {

                        if (pcList.Checked == true)
                        {
                            foreach (MasterItemSubCate itmList in _lstcate2)
                            {


                                _tempProcess = new PromoVoucherDefinition();
                                _tempProcess.Spd_seq = index;
                                _tempProcess.Spd_com = BaseCls.GlbUserComCode;
                                _tempProcess.Spd_sale_tp = cmbPVInvoiceType.Text;
                                _tempProcess.Spd_stus = true;
                                _tempProcess.Spd_vou_cd = txtVochCode_pv.Text;


                                _tempProcess.Spd_brd = null;
                                _tempProcess.Spd_cat = itmList.Ric2_cd;
                                _tempProcess.Spd_main_cat = itmList.Ric2_cd1;
                                _tempProcess.Spd_itm = null;


                                _tempProcess.Spd_circular_no = txtCircular_pv.Text;
                                _tempProcess.Spd_cre_by = BaseCls.GlbUserID;
                                _tempProcess.Spd_mod_by = BaseCls.GlbUserID;
                                _tempProcess.Spd_disc = Convert.ToDecimal(txtDis_pv.Text);
                                _tempProcess.Spd_disc_isrt = _isDisRate;
                                _tempProcess.Spd_from_dt = dtpFromDate__pv.Value.Date;
                                _tempProcess.Spd_to_dt = dtpToDate_pv.Value.Date;

                                _tempProcess.Spd_pb = txtPB_pv.Text;
                                _tempProcess.Spd_pb_lvl = txtPL_pv.Text;
                                _tempProcess.Spd_pty_cd = pcList.Text.ToUpper().ToString();

                                _tempProcess.Spd_period = _validPeriod;
                                _tempProcess.Spd_rdm_com = cmbRdmAllowCompany.SelectedValue.ToString();
                                _tempProcess.Spd_rdm_pb = ".";
                                _tempProcess.Spd_rdm_pb_lvl = ".";

                                if (cmbDefby_pv.Text == "Profit Center")
                                { _tempProcess.Spd_pty_tp = "PC"; }
                                else
                                { _tempProcess.Spd_pty_tp = "SCHNL"; }
                                _schemeProcess.Add(_tempProcess);

                            }
                        }
                        index++;
                    }
                }

                else
                {
                    foreach (ListViewItem pcList in lstLocations.Items)
                    {
                        if (pcList.Checked == true)
                        {
                            _tempProcess = new PromoVoucherDefinition();
                            _tempProcess.Spd_com = BaseCls.GlbUserComCode;
                            _tempProcess.Spd_sale_tp = cmbPVInvoiceType.Text;
                            _tempProcess.Spd_stus = true;
                            _tempProcess.Spd_vou_cd = txtVochCode_pv.Text;
                            _tempProcess.Spd_brd = txtBrand_pv.Text;
                            _tempProcess.Spd_cat = txtCat2_pv.Text;
                            _tempProcess.Spd_circular_no = txtCircular_pv.Text;
                            _tempProcess.Spd_cre_by = BaseCls.GlbUserID;
                            _tempProcess.Spd_disc = Convert.ToDecimal(txtDis_pv.Text);
                            _tempProcess.Spd_disc_isrt = _isDisRate;
                            _tempProcess.Spd_from_dt = dtpFromDate__pv.Value.Date;
                            _tempProcess.Spd_to_dt = dtpToDate_pv.Value.Date;
                            _tempProcess.Spd_itm = null;
                            _tempProcess.Spd_main_cat = txtCat1_pv.Text;
                            _tempProcess.Spd_pb = txtPB_pv.Text;
                            _tempProcess.Spd_pb_lvl = txtPL_pv.Text;
                            _tempProcess.Spd_pty_cd = pcList.Text.ToUpper().ToString();
                            _tempProcess.Spd_mod_by = BaseCls.GlbUserID;

                            _tempProcess.Spd_period = _validPeriod;
                            _tempProcess.Spd_rdm_com = cmbRdmAllowCompany.SelectedValue.ToString();
                            _tempProcess.Spd_rdm_pb = ".";
                            _tempProcess.Spd_rdm_pb_lvl = ".";

                            if (cmbDefby_pv.Text == "Profit Center")
                            { _tempProcess.Spd_pty_tp = "PC"; }
                            else
                            { _tempProcess.Spd_pty_tp = "SCHNL"; }
                            _schemeProcess.Add(_tempProcess);
                        }
                    }
                }

                dgvDefDetails_pv.AutoGenerateColumns = false;
                dgvDefDetails_pv.DataSource = new List<HpSchemeDefinition>();
                dgvDefDetails_pv.DataSource = _schemeProcess;

                var _tempDO = (from _lst in _schemeProcess
                               select _lst.Spd_pty_cd).ToList().Distinct();

                Int32 _saveCount = 0;

                //Clrear Temp table
                CHNLSVC.Sales.DeleteTempPromoVoucher(BaseCls.GlbUserID);

                foreach (string _pc in _tempDO)
                {
                    List<PromoVoucherDefinition> tempDo = (from _lst in _schemeProcess
                                                           where _lst.Spd_pty_cd == _pc
                                                           select _lst).ToList();
                    DataTable dt = ConvertToDataTable(tempDo);
                    dt.TableName = "schSchemeCommDef";
                    int _pro = (Int16)(CHNLSVC.Sales.SaveTempPromoVoucher(dt));
                    _saveCount = _saveCount + tempDo.Count;
                    lblcount.Text = _saveCount.ToString();
                    lblSaveCount.Text = _saveCount.ToString();
                }

                SavePromotionVouItemsTemp();

                cmbProVouDefType.Enabled = true;
                //cmbCommDefType_SelectedIndexChanged(null, null);
                btnCommAdd__pv.Enabled = true;
                btnProVouSave.Enabled = true;
                TimeSpan end1 = DateTime.Now.TimeOfDay;

                Cursor.Current = Cursors.Default;
                // MessageBox.Show("Succesfully Saved.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                btnProVouSave.Enabled = true;
                btnCommAdd__pv.Enabled = true;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProVouType_DoubleClick(object sender, EventArgs e)
        {
            btnProVouType_spv_Click(null, null);
        }

        private void txtProVouType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProVouType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.General.GetProVoutype(BaseCls.GlbUserComCode, txtProVouType.Text.Trim());
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("spt_vou_cd") == txtProVouType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    txtProVouTypeDesc.Focus();
                    _isUpdate = false;
                }
                else
                {
                    //updated by akila 2017/11/06  - null handled
                    _isUpdate = true;

                    txtProVouTypeDesc.Text = _validate[0]["spt_vou_desc"] == DBNull.Value ? string.Empty : _validate[0]["spt_vou_desc"].ToString();
                    txtCond.Text = _validate[0]["spt_cond"] == DBNull.Value ? string.Empty :  _validate[0]["spt_cond"].ToString();

                    Int32 _isActive = 0;
                    Int32.TryParse(_validate[0]["SPT_ACT"].ToString(), out _isActive);
                    if (_isActive == 1)
                    {
                        optVou1.Checked = true;
                    }
                    else
                    {
                        optVou0.Checked = true;
                    }

                    //if (Convert.ToInt32(_validate[0]["SPT_ACT"].ToString()) == 1)
                    //{
                    //    optVou1.Checked = true;
                    //}
                    //else
                    //{
                    //    optVou0.Checked = true;
                    //}

                    Int32 _isQtyWise = 0;
                    Int32.TryParse(_validate[0]["spt_is_qtywise"].ToString(), out _isQtyWise);
                    if (_isQtyWise == 1)
                    {
                        chkIssueQtywise.Checked = true;
                    }
                    else
                    {
                        chkIssueQtywise.Checked = false;
                    }

                    //if (Convert.ToInt32(_validate[0]["spt_is_qtywise"].ToString()) == 1)
                    //{
                    //    chkIssueQtywise.Checked = true;
                    //}
                    //else
                    //{
                    //    chkIssueQtywise.Checked = false;
                    //}

                    _promoVouPara = new List<PromotionVoucherPara>();
                    _promoVouPara = CHNLSVC.Financial.getProVouPara(txtProVouType.Text);
                    grvVouPara.DataSource = _promoVouPara;

                    Int32 _smsAlert= 0;
                    Int32.TryParse(_validate[0]["spt_sms_alert"].ToString(), out _smsAlert);
                    //if (Convert.ToInt32(_validate[0]["spt_sms_alert"].ToString()) == 1)
                    if (_smsAlert == 1)
                    {
                        chkSMS.Checked = true;
                        txtPurSMS.Enabled = true;
                        txtRedeemSMS.Enabled = true;
                        txtPurSMS.Text = _validate[0]["spt_cus_pur_sms"] == DBNull.Value ? string.Empty : _validate[0]["spt_cus_pur_sms"].ToString();
                        txtRedeemSMS.Text = _validate[0]["spt_cus_red_sms"] == DBNull.Value ? string.Empty : _validate[0]["spt_cus_red_sms"].ToString();
                        txtMinVal.Text = _validate[0]["spt_min_val"] == DBNull.Value ?string.Empty : _validate[0]["spt_min_val"].ToString();
                    }
                    else
                    {
                        chkSMS.Checked = false;
                        txtPurSMS.Enabled = false;
                        txtRedeemSMS.Enabled = false;
                        txtPurSMS.Text = "";
                        txtRedeemSMS.Text = "";
                        txtMinVal.Text = "";
                    }

                }


            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVouchAtt_pv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtVochCode_pv;
                _CommonSearch.ShowDialog();
                txtVochCode_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtVochCode_pv_DoubleClick(object sender, EventArgs e)
        {
            btnVouchAtt_pv_Click(null, null);
        }

        private void txtVochCode_pv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVochCode_pv.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtVochCode_pv.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid voucher code", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtVochCode_pv.Clear();
                    txtVochCode_pv.Focus();
                    lblVouchDesc.Text = "";
                    return;
                }
                else
                {
                    lblVouchDesc.Text = _validate[0].ItemArray[1].ToString();
                    txtPB_pv.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProVouType_spv_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisVouTp);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionVoucherAll(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProVouType;
                _CommonSearch.ShowDialog();
                txtProVouType.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProVouSave_Click(object sender, EventArgs e)
        {
            DialogResult dgr = MessageBox.Show("Do you want to Save?", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgr == DialogResult.Yes)
            {
                if (tbMainCommDef.SelectedTab == tbMainCommDef.TabPages[0])
                {
                    if (dgvDefDetails_pv.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        int row_aff = CHNLSVC.General.SavePromoVouDefinition(BaseCls.GlbUserID);

                        if (row_aff != -99 && row_aff >= 0)
                        {
                            CHNLSVC.Sales.DeleteTempPromoVoucher(BaseCls.GlbUserID);
                            CHNLSVC.Sales.DeleteTempPromoVoucherRedeemPB(BaseCls.GlbUserID);
                            MessageBox.Show("Successfully Saved", "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Cursor.Current = Cursors.Default;
                            ClearPV();
                        }
                        else
                        {
                            MessageBox.Show(txtProVouType.Text, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please fill details.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                else // promotion voucher types
                {
                    if (string.IsNullOrEmpty(txtMinVal.Text))// Nadeeka 20-10-2015
                    {
                        MessageBox.Show("Enter minimum value.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMinVal.Focus();
                        return;
                    }

                    if (_isUpdate == true) // Update 
                    {
                        string P_cd = txtProVouType.Text.Trim();
                        int p_status = 0;
                        Int32 p_qty_wise = 0;
                        Int32 p_SMS = 0;
                        Int32 p_separateprint = 0;
                        string _error = "";
                        if (optVou1.Checked)
                        {
                            p_status = 1;
                        }
                        else if (optVou0.Checked)
                        {
                            p_status = 0;
                        }
                        if (chkIssueQtywise.Checked == true)
                        {
                            p_qty_wise = 1;
                        }
                        else
                        {
                            p_qty_wise = 0;
                        }

                        if (chkSMS.Checked == true)
                        {
                            p_SMS = 1;
                        }
                        else
                        {
                            p_SMS = 0;
                        }
                        if (chkSeparatePrint.Checked == true)
                        {
                            p_separateprint = 1;
                        }
                        else
                        {
                            p_separateprint = 0;
                        }


                        int row_aff = CHNLSVC.General.UpdateProVouTypes(BaseCls.GlbUserComCode, P_cd, txtProVouTypeDesc.Text.Trim(), p_status, BaseCls.GlbUserID, DateTime.Today.Date, p_qty_wise, p_SMS, txtPurSMS.Text.Trim(), txtRedeemSMS.Text.Trim(), Convert.ToDecimal(txtMinVal.Text), txtCond.Text, _promoVouPara, optDisc.Checked == true ? 1 : 2, p_separateprint);
                        if (row_aff != -99 && row_aff >= 0)
                        {
                            MessageBox.Show("Successfully Updated", "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(txtProVouType.Text, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // insert
                    {
                        string P_cd = txtProVouType.Text.Trim();
                        int p_status = 0;
                        Int32 p_qty_wise = 0;
                        Int32 p_SMS = 0;
                        Int32 p_separateprint = 0;
                        string _error = "";
                        if (optVou1.Checked)
                        {
                            p_status = 1;
                        }
                        else if (optVou0.Checked)
                        {
                            p_status = 0;
                        }

                        if (chkIssueQtywise.Checked == true)
                        {
                            p_qty_wise = 1;
                        }
                        else
                        {
                            p_qty_wise = 0;
                        }
                        if (chkSMS.Checked == true)
                        {
                            p_SMS = 1;
                        }
                        else
                        {
                            p_SMS = 0;
                        }
                        if (chkSeparatePrint.Checked == true)
                        {
                            p_separateprint = 1;
                        }
                        else
                        {
                            p_separateprint = 0;
                        }

                        int row_aff = CHNLSVC.General.SavePromoVouType(BaseCls.GlbUserComCode, P_cd, txtProVouTypeDesc.Text.Trim(), p_status, BaseCls.GlbUserID, p_qty_wise, p_SMS, txtPurSMS.Text.Trim(), txtRedeemSMS.Text.Trim(), Convert.ToDecimal(txtMinVal.Text), txtCond.Text, _promoVouPara, optDisc.Checked == true ? 1 : 2, p_separateprint, out _error);
                        if (row_aff != -99 && row_aff >= 0)
                        {
                            MessageBox.Show("Successfully Saved. Code number - " + txtProVouType.Text, "Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show(_error, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    ClearPromotionPoucher();
                }
            }
        }

        #region Redeem Items

        #endregion

        private void btnUploadFile_rd_Click(object sender, EventArgs e)
        {
            try
            {
                string _msg = string.Empty;
                if (string.IsNullOrEmpty(txtFileName_rd.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFileName_rd.Clear();
                    txtFileName_rd.Focus();
                    return;
                }

                //System.IO.FileInfo _fileObj = new System.IO.FileInfo(txtFileName_rd.Text);

                //if (_fileObj.Exists == false)
                //{
                //    MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtFileName_rd.Focus();
                //    return;
                //}

                //string _extension = _fileObj.Extension;
                //string _conStr = string.Empty;



                //if (_extension.ToUpper() == ".XLS")
                //{
                //    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName_rd.Text + "; Extended Properties='Excel 8.0;HDR=YES;'";
                //}
                //else if (_extension.ToUpper() == ".XLSX")
                //{
                //    _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName_rd.Text + ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
                //}
                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName_rd.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGvBrowse.Focus();
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


                conStr = String.Format(conStr, txtFileName_rd.Text, "NO");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
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
                oda.Fill(_dt);
                connExcel.Close();

                select_ITEMS_List.Clear();
                grvRedeemItems.DataSource = null;
                grvRedeemItems.AutoGenerateColumns = false;
                grvRedeemItems.DataSource = select_ITEMS_List;


                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        DataRow drItem = select_ITEMS_List.NewRow();
                        if (!string.IsNullOrEmpty(_dr[0].ToString())) // Item
                        {
                            if (string.IsNullOrEmpty(_dr[0].ToString())) continue;

                            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString().Trim());
                            if (_item == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid item - " + _dr[0].ToString());
                                else _errorLst.Append(" and invalid item - " + _dr[0].ToString());
                                continue;
                            }
                            if (_itemLst != null)
                            {
                                var _dup = _itemLst.Where(x => x == _dr[0].ToString()).ToList();
                                if (_dup != null && _dup.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("item " + _dr[0].ToString() + " duplicate");
                                    else _errorLst.Append(" and item " + _dr[0].ToString() + " duplicate");
                                    continue;
                                }
                            }
                            drItem["CODE"] = _dr[0].ToString().Trim();
                            select_ITEMS_List.Rows.Add(drItem);
                        }

                        else if (!string.IsNullOrEmpty(_dr[1].ToString())) // Brand
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;

                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());
                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }
                            if (_itemLst != null)
                            {
                                var _dup = _itemLst.Where(x => x == _dr[1].ToString()).ToList();
                                if (_dup != null && _dup.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand " + _dr[1].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand " + _dr[1].ToString() + " duplicate");
                                    continue;
                                }
                            }
                            drItem["BRAND"] = _dr[1].ToString().Trim();
                            select_ITEMS_List.Rows.Add(drItem);
                        }

                        else if (!string.IsNullOrEmpty(_dr[2].ToString()) && !string.IsNullOrEmpty(_dr[3].ToString())) // Main Cate
                        {
                            if (string.IsNullOrEmpty(_dr[2].ToString())) continue;


                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category - " + _dr[2].ToString());
                                continue;
                            }

                            MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());


                            if (subCate.Ric2_cd1 == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[3].ToString());
                                else _errorLst.Append(" and invalid sub category - " + _dr[3].ToString());
                                continue;
                            }

                            if (_itemLst != null)
                            {

                                var _dup = _itemLst.Where(x => x == _dr[2].ToString() + _dr[3].ToString()).ToList();
                                if (_dup != null && _dup.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr[2].ToString() + " sub category " + _dr[3].ToString() + " duplicate");
                                    else _errorLst.Append(" and main category " + _dr[2].ToString() + " and sub category " + _dr[3].ToString() + "  duplicate");
                                    continue;
                                }
                            }
                            drItem["CAT1"] = _dr[2].ToString().Trim();

                            drItem["CAT2"] = _dr[3].ToString().Trim();
                            select_ITEMS_List.Rows.Add(drItem);

                        }

                        else if (!string.IsNullOrEmpty(_dr[2].ToString()) && string.IsNullOrEmpty(_dr[3].ToString())) // Main Cate
                        {
                            if (string.IsNullOrEmpty(_dr[2].ToString())) continue;


                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());
                            if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                            {
                            }

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category - " + _dr[2].ToString());
                                continue;
                            }
                            if (_itemLst != null)
                            {
                                var _dup = _itemLst.Where(x => x == _dr[2].ToString()).ToList();
                                if (_dup != null && _dup.Count > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr[2].ToString() + " duplicate");
                                    else _errorLst.Append(" and main category " + _dr[2].ToString() + " duplicate");
                                    continue;
                                }
                            }
                            drItem["CAT1"] = _dr[2].ToString().Trim();
                            select_ITEMS_List.Rows.Add(drItem);
                        }


                    }
                    grvRedeemItems.DataSource = null;
                    grvRedeemItems.AutoGenerateColumns = false;
                    grvRedeemItems.DataSource = select_ITEMS_List;
                    txtItem_rd.Clear();
                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            ////  _itemLst = new List<string>();

                        }
                    }

                }

                MessageBox.Show("Successfully Uploaded  !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnLoadPara_rd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem_rd.Text))
            {
                MessageBox.Show("Please select an item code", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (select_ITEMS_List.Rows.Count > 0)
            {
                if (select_ITEMS_List.Select("CODE = '" + txtItem_rd.Text + "'").Length > 0)
                {
                    MessageBox.Show("Selected item is already in the list.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem_rd.Clear();
                    return;
                }
            }

            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems("ITEM", string.Empty, string.Empty, string.Empty, string.Empty, txtItem_rd.Text.Trim(), string.Empty, string.Empty, string.Empty);
            select_ITEMS_List.Merge(dt);
            grvRedeemItems.DataSource = null;
            grvRedeemItems.AutoGenerateColumns = false;
            grvRedeemItems.DataSource = select_ITEMS_List;
            txtItem_rd.Clear();
        }

        private void btnRedeemItem_pv_Click(object sender, EventArgs e)
        {
            if (pnlRedeemItems.Visible == true)
            {
                pnlRedeemItems.Visible = false;
            }
            else
            {
                pnlRedeemItems.Location = new Point(5, 221);
                BindCompany();
                pnlRedeemItems.Visible = true;
            }
        }

        private void txtProVouType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProVouType_Leave(null, null);
            }
        }

        private void ClearPromotionPoucher()
        {
            txtProVouType.Clear();
            txtProVouTypeDesc.Clear();
            chkIssueQtywise.Checked = false;
            chkSMS.Checked = false;
            optVou1.Checked = false;
            optVou0.Checked = false;
            txtRedeemSMS.Text = "";
            txtCond.Text = "";
            txtPurSMS.Text = "";
            txtRedeemSMS.Enabled = false;
            txtPurSMS.Enabled = false;

            _promoVouPara = new List<PromotionVoucherPara>();
            grvVouPara.DataSource = null;
            grvVouPara.AutoGenerateColumns = false;
            grvVouPara.DataSource = new List<PromotionVoucherPara>();
            grvVouPara.DataSource = _promoVouPara;
        }

        private void txtVochCode_pv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVochCode_pv_Leave(null, null);
            }
        }

        private void btnClosePVRedeem_Click(object sender, EventArgs e)
        {
            if (pnlRedeemItems.Visible == true)
            {
                pnlRedeemItems.Visible = false;
            }
            else
            {
                pnlRedeemItems.Visible = true;
            }
        }

        private void btnProVouClear_Click(object sender, EventArgs e)
        {
            ClearPV();
        }

        private void txtPB_pv_TextChanged(object sender, EventArgs e)
        {
            txtPL_pv.Clear();
        }

        private void txtBrand_pv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCat1_pv_TextChanged(object sender, EventArgs e)
        {
            txtCat2_pv.Clear();
            txtCat3_pv.Clear();
            txtBrand_pv.Clear();
        }

        private void txtCat3_pv_TextChanged(object sender, EventArgs e)
        {
            txtBrand_pv.Clear();
        }

        private void dtpToDate_pv_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpToDate_pv_Leave(object sender, EventArgs e)
        {
            if (dtpFromDate__pv.Value.Date > dtpToDate_pv.Value.Date)
            {
                MessageBox.Show("Please select valied date range.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpToDate_pv.Focus();
            }
        }

        private void btnItem_rd_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem_rd;
                _CommonSearch.ShowDialog();
                txtItem_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindCompany()
        {
            //cmbRdmAllowCompany.Items.Clear();

            List<MasterCompany> _lst = CHNLSVC.General.GetALLMasterCompaniesData();
            var _n = new MasterCompany();
            _n.Mc_cd = string.Empty;
            _n.Mc_cd = string.Empty;
            _lst.Insert(0, _n);
            cmbRdmAllowCompany.DataSource = _lst;
            cmbRdmAllowCompany.DisplayMember = "Mc_desc";
            cmbRdmAllowCompany.ValueMember = "Mc_cd";
            cmbRdmAllowCompany.SelectedValue = BaseCls.GlbUserComCode;
        }

        private void btnRdmComPB_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PromoVou";
                txtRdmComPB.Clear();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRdmComPB;
                _CommonSearch.ShowDialog();
                txtPB_pv.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRdmComPBLvl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRdmComPB.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRdmComPBLvl.Clear();
                    txtRdmComPBLvl.Focus();
                    return;
                }
                _Stype = "PromoVouRedeem";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRdmComPBLvl;
                _CommonSearch.ShowDialog();
                txtRdmComPBLvl.Select();

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRdmComPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRdmComPB_Leave(null, null);
            }
        }

        private void txtRdmComPBLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPVRDMValiedPeriod.Focus();
            }
        }

        private void txtRdmComPB_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRdmComPB.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(cmbRdmAllowCompany.SelectedValue.ToString(), txtRdmComPB.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Promotion Vouchers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRdmComPB.Clear();
                    txtRdmComPB.Focus();
                }
                else
                {
                    txtRdmComPBLvl.Focus();
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRdmComPBLvl_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRdmComPB.Text))
                {
                    PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(cmbRdmAllowCompany.SelectedValue.ToString(), txtRdmComPB.Text.Trim(), txtRdmComPBLvl.Text.Trim());
                    if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                    {
                        MessageBox.Show("Please enter valid price level.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRdmComPBLvl.Clear();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRedmPnlConfirm_Click(object sender, EventArgs e)
        {
            if (grvRdmPb.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Redeem Price book/level.", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                return;
            }
            if (grvRedeemItems.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items.", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                txtItem_rd.Focus();
                return;
            }

            DialogResult dgr = MessageBox.Show("Do you want to confirm?", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dgr == DialogResult.Yes)
            {
                isRedeemConfirms = true;

                foreach (Control c in pnlRedeemItems.Controls)
                {
                    if (c.Name == "btnClosePVRedeem" || c.Name == "btnRedmPnlConfirm" || c.Name == "btnRedmPnlClear")
                    {
                        c.Enabled = true;
                    }
                    else
                    {

                        c.Enabled = false;
                    }
                }

                //kapila 22/10/2016
                CHNLSVC.Sales.DeleteTempPromoVoucherRedeemPB(BaseCls.GlbUserID);
                Int32 _e = CHNLSVC.Sales.SaveTempPromoVoucherRedeemPB(_lstRdmPB);

                pnlRedeemItems.Visible = false;
            }

        }

        private void btnRedmPnlClear_Click(object sender, EventArgs e)
        {
            DialogResult dgr = MessageBox.Show("Do you want to Clear?", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgr == DialogResult.Yes)
            {
                ClearRedeemItemPln();
                foreach (Control c in pnlRedeemItems.Controls)
                {
                    if (c.Name != "btnClosePVRedeem" || c.Name != "btnRedmPnlConfirm" || c.Name != "btnRedmPnlClear")
                    {
                        c.Enabled = true;
                    }
                }
            }

        }

        private void btnSearchFile_srd_Click(object sender, EventArgs e)
        {
            txtFileName_rd.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName_rd.Text = openFileDialog1.FileName;
        }

        private void grvRedeemItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvRedeemItems.RowCount > 0)
            {

                int _rowCount = e.RowIndex;
                if (_rowCount != -1)
                {
                    if (grvRedeemItems.Columns[e.ColumnIndex].Name == "delete")
                    {
                        if (MessageBox.Show("Do you need to remove this record?", "Reove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) RemoveItemFromDT(grvRedeemItems.Rows[e.RowIndex].Cells[1].Value.ToString());
                    }
                }
            }
        }

        private void RemoveItemFromDT(string ItemCode)
        {
            DataRow[] drTemp;
            drTemp = select_ITEMS_List.Select("CODE = '" + ItemCode + "'");
            if (drTemp.Length > 0)
            {
                select_ITEMS_List.Rows.Remove(drTemp[0]);
            }
        }

        private void txtDis_pv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPVInvoiceType.Focus();
            }
        }

        private void txtPVRDMValiedPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPVRDMValiedPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPVRDMValiedPeriod.Text))
                {
                    MessageBox.Show("Please enter valied period.", "Promotion Vouchers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void txtItem_rd_DoubleClick(object sender, EventArgs e)
        {
            btnItem_rd_Click(null, null);
        }

        private void txtRdmComPB_DoubleClick(object sender, EventArgs e)
        {
            btnRdmComPB_Click(null, null);
        }

        private void txtRdmComPBLvl_DoubleClick(object sender, EventArgs e)
        {
            btnRdmComPBLvl_Click(null, null);
        }

        private void ClearPV()
        {
            cmbProVouDefType.Text = "";
            txtVochCode_pv.Clear();
            lblVouchDesc.Text = "";
            txtPB_pv.Clear();
            txtCat1_pv.Clear();
            txtCat3_pv.Clear();
            txtItem_pv.Clear();
            txtPL_pv.Clear();
            txtCat2_pv.Clear();
            txtBrand_pv.Clear();
            txtFileName_pv.Clear();
            lstPDItems.Items.Clear();
            txtMinVal.Clear();
            ClearPromotionPoucher();

            cmbDefby_pv.Text = "";
            txtChnnl_pv.Clear();
            txtSChnnl_pv.Clear();
            txtPC_pv.Clear();
            lstLocations.Items.Clear();
            txtCircular_pv.Clear();

            dtpFromDate__pv.Value = DateTime.Today;
            dtpToDate_pv.Value = DateTime.Today;
            cmbDisType_pv.Text = "";
            txtDis_pv.Clear();
            dgvDefDetails_pv.DataSource = null;

            ClearRedeemItemPln();
            btnCommAdd__pv.Enabled = true;

            //clear temp table
            CHNLSVC.Sales.DeleteTempPromoVoucher(BaseCls.GlbUserID);
            CHNLSVC.Sales.DeleteTempPromoVoucherRedeemPB(BaseCls.GlbUserID);

            isRedeemConfirms = false;

            lblcount.Text = "";
            lblSaveCount.Text = "";
            _isUpdate = false;  //kapila 24/2/2016

            ClearRedeemItemPln();

            foreach (Control c in pnlRedeemItems.Controls)
            {
                c.Enabled = true;
            }

            _schemeProcess.Clear();

            pnlRedeemItems.Visible = false;

            lblPVStatus.Text = "";
            lblPVStatus.ForeColor = Color.Blue;


            _lstcate2 = new List<MasterItemSubCate>();
            gvVouCat.DataSource = null;
            gvVouCat.AutoGenerateColumns = false;
            gvVouCat.DataSource = new List<MasterItemSubCate>();
            gvVouCat.DataSource = _lstcate2;

            _promoVouPara = new List<PromotionVoucherPara>();
            grvVouPara.DataSource = null;
            grvVouPara.AutoGenerateColumns = false;
            grvVouPara.DataSource = new List<PromotionVoucherPara>();
            grvVouPara.DataSource = _promoVouPara;

            grvRdmPb.AutoGenerateColumns = false;
            grvRdmPb.DataSource = null;

            cmbRdmAllowCompany.Text = "";
            txtRdmComPB.Text = "";
            txtRdmComPBLvl.Text = "";
            txtPVRDMValiedPeriod.Text = "";

            _lstRdmPB = new List<PromoVouRedeemPB>();
        }

        private void ClearRedeemItemPln()
        {
            txtItem_rd.Clear();
            txtFileName_rd.Clear();
            grvRedeemItems.DataSource = null;
            cmbRdmAllowCompany.Text = "";
            txtRdmComPB.Clear();
            txtRdmComPBLvl.Clear();
            select_ITEMS_List.Clear();
            txtPVRDMValiedPeriod.Text = "";
            grvRdmPb.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _lstRdmPB = new List<PromoVouRedeemPB>();
            _source.DataSource = _lstRdmPB;
            grvRdmPb.DataSource = new List<PromoVouRedeemPB>();
            grvRdmPb.DataSource = _source;
        }

        private void InitializeItemDataTable()
        {
            select_ITEMS_List.Columns.Add("CODE", typeof(string));
            select_ITEMS_List.Columns.Add("DESCRIPT", typeof(string));
            select_ITEMS_List.Columns.Add("BRAND", typeof(string));
            select_ITEMS_List.Columns.Add("CAT1", typeof(string));
            select_ITEMS_List.Columns.Add("CAT2", typeof(string));
        }

        private void SavePromotionVouItemsTemp()
        {
            DataTable DtTemp = new DataTable("tblPromoVouItems");
            DtTemp.Columns.Add("Spi_seq", typeof(Int32));
            DtTemp.Columns.Add("Spi_itm_seq", typeof(Int32));
            DtTemp.Columns.Add("Spi_itm", typeof(string));
            DtTemp.Columns.Add("Spi_brand", typeof(string));
            DtTemp.Columns.Add("Spi_cat1", typeof(string));
            DtTemp.Columns.Add("Spi_cat2", typeof(string));
            DtTemp.Columns.Add("Spi_itm_stus", typeof(string));
            DtTemp.Columns.Add("Spi_act", typeof(Int32));
            DtTemp.Columns.Add("Spi_cre_by", typeof(string));
            DtTemp.Columns.Add("Spi_mod_by", typeof(string));

            for (int i = 0; i < select_ITEMS_List.Rows.Count; i++)
            {
                DataRow drTemp = DtTemp.NewRow();
                drTemp["Spi_seq"] = i.ToString();
                drTemp["Spi_itm_seq"] = i.ToString();
                drTemp["Spi_itm"] = select_ITEMS_List.Rows[i]["CODE"].ToString();
                drTemp["Spi_brand"] = select_ITEMS_List.Rows[i]["BRAND"].ToString();
                drTemp["Spi_cat1"] = select_ITEMS_List.Rows[i]["CAT1"].ToString();
                drTemp["Spi_cat2"] = select_ITEMS_List.Rows[i]["CAT2"].ToString();
                drTemp["Spi_itm_stus"] = "GOD";
                drTemp["Spi_act"] = "1";
                drTemp["Spi_cre_by"] = BaseCls.GlbUserID;
                drTemp["Spi_mod_by"] = BaseCls.GlbUserID;
                DtTemp.Rows.Add(drTemp);
            }

            int _pro = (Int32)(CHNLSVC.Sales.SaveTempPromoVoucherItems(DtTemp));
        }

        private void cmbRdmAllowCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void HandleCobjects(bool isEnabled)
        {
            if (isEnabled == true)
            {
                foreach (Control item in groupBox8.Controls)
                {
                    item.Enabled = true;
                }
                foreach (Control item in groupBox14.Controls)
                {
                    item.Enabled = true;
                }
            }
            else
            {
                foreach (Control item in groupBox8.Controls)
                {
                    item.Enabled = false;
                }
                foreach (Control item in groupBox14.Controls)
                {
                    item.Enabled = false;
                }
            }
        }

        private bool checkItemExists(string ItemText)
        {
            bool isExists = false;

            foreach (ListViewItem item in lstLocations.Items)
            {
                if (item.Text == ItemText)
                {
                    isExists = true;
                    break;
                }
            }

            return isExists;
        }

        private void txtDis_pv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void FillPromotionVoucherDefinitionByCircular()
        {

            List<PromoVoucherDefinition> _recallList = new List<PromoVoucherDefinition>();
            _recallList = CHNLSVC.Sales.GetProVouhByCir(txtCircular_pv.Text.Trim());

            if (_recallList.Count > 0)
            {
                ClearPV();
                txtCircular_pv.Text = _recallList[0].Spd_circular_no;
                dgvDefDetails_pv.AutoGenerateColumns = false;
                dgvDefDetails_pv.DataSource = new List<HpSchemeDefinition>();
                dgvDefDetails_pv.DataSource = _recallList;
                lblcount.Text = _recallList.Count.ToString();

                Cursor.Current = Cursors.WaitCursor;

                int ItemCount = _recallList.FindAll(x => x.Spd_itm != null || x.Spd_itm != string.Empty).Count;

                if (ItemCount > 0)
                {
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl))
                    {
                        cmbProVouDefType.Text = "Price Book Wise";
                    }
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl) && !string.IsNullOrEmpty(_recallList[0].Spd_main_cat))
                    {
                        cmbProVouDefType.Text = "Main Category Wise";
                    }
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl) && !string.IsNullOrEmpty(_recallList[0].Spd_main_cat) && !string.IsNullOrEmpty(_recallList[0].Spd_cat))
                    {
                        cmbProVouDefType.Text = "Sub Category Wise";
                    }
                    if (!string.IsNullOrEmpty(_recallList[0].Spd_pb) && !string.IsNullOrEmpty(_recallList[0].Spd_pb_lvl) && !string.IsNullOrEmpty(_recallList[0].Spd_brd))
                    {
                        cmbProVouDefType.Text = "Brand Wise";
                    }
                    txtVochCode_pv.Text = _recallList[0].Spd_vou_cd;
                    txtVochCode_pv_Leave(null, null);
                    txtPB_pv.Text = _recallList[0].Spd_pb;
                    txtPL_pv.Text = _recallList[0].Spd_pb_lvl;
                    txtCat1_pv.Text = _recallList[0].Spd_main_cat;
                    txtCat2_pv.Text = _recallList[0].Spd_cat;
                    txtBrand_pv.Text = _recallList[0].Spd_brd;
                    if (_recallList[0].Spd_disc_isrt)
                    {
                        cmbDisType_pv.Text = "RATE";
                    }
                    else
                    {
                        cmbDisType_pv.Text = "VALUE";
                    }
                    txtDis_pv.Text = _recallList[0].Spd_disc.ToString("N");
                    dtpFromDate__pv.Value = _recallList[0].Spd_from_dt;
                    dtpToDate_pv.Value = _recallList[0].Spd_to_dt;

                    if (_recallList[0].Spd_pty_tp == "PC")
                    {
                        cmbDefby_pv.Text = "Profit Center";
                    }
                    else
                    {
                        cmbDefby_pv.Text = "Sub Channel";
                    }
                    foreach (PromoVoucherDefinition item in _recallList)
                    {
                        if (!checkItemExists(item.Spd_pty_cd))
                        {
                            lstLocations.Items.Add(item.Spd_pty_cd);
                        }
                    }

                    if (_recallList[0].Spd_stus == true)
                    {
                        lblPVStatus.ForeColor = Color.Blue;
                        lblPVStatus.Text = "Active";

                    }
                    else
                    {
                        lblPVStatus.ForeColor = Color.Red;
                        lblPVStatus.Text = "De-Active";
                    }

                    cmbPVInvoiceType.Text = _recallList[0].Spd_sale_tp;

                    var seletedItems = (from _temp in _recallList select _temp.Spd_itm).Distinct().ToList();

                    foreach (var item in seletedItems)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            lstPDItems.Items.Add(item);
                            cmbProVouDefType.Text = "Product Wise";
                        }
                    }

                    cmbRdmAllowCompany.SelectedValue = _recallList[0].Spd_rdm_com;

                    //  txtRdmComPB.Text = _recallList[0].Spd_rdm_pb;
                    //  txtRdmComPBLvl.Text = _recallList[0].Spd_rdm_pb_lvl;
                    //  txtPVRDMValiedPeriod.Text = _recallList[0].Spd_period.ToString();

                    DataTable _dt = CHNLSVC.Sales.getProVouRdmPb(_recallList[0].Spd_seq);
                    grvRdmPb.AutoGenerateColumns = false;
                    grvRdmPb.DataSource = _dt;

                    DataTable dtRedeemItems = new DataTable();
                    dtRedeemItems.Merge(channelService.Sales.GetPromotionItemsByBatchSeq(_recallList[0].SPD_BATCH_SEQ));

                    if (dtRedeemItems.Rows.Count > 0)
                    {
                        //select_ITEMS_List.Columns.Add("CODE", typeof(string));
                        //select_ITEMS_List.Columns.Add("BRAND", typeof(string));
                        //select_ITEMS_List.Columns.Add("CAT1", typeof(string));
                        //select_ITEMS_List.Columns.Add("CAT2", typeof(string));
                        foreach (DataRow item in dtRedeemItems.Rows)
                        {
                            //DataRow drRDItem = select_ITEMS_List.NewRow();
                            //drRDItem["CODE"] = item["spi_itm"].ToString();
                            //drRDItem["BRAND"] = item["spi_brand"].ToString();
                            //drRDItem["CAT1"] = item["spi_cat1"].ToString();
                            //drRDItem["CAT2"] = item["spi_cat2"].ToString();


                            select_ITEMS_List.Rows.Add(item["spi_itm"].ToString(), item["spi_brand"].ToString(), item["spi_cat1"].ToString(), item["spi_cat2"].ToString());
                        }
                        grvRedeemItems.DataSource = null;
                        grvRedeemItems.AutoGenerateColumns = false;
                        grvRedeemItems.DataSource = select_ITEMS_List;
                    }
                    dgvDefDetails_pv.Focus();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void tbPromoVoucher_Enter(object sender, EventArgs e)
        {
            BindCompany();
        }

        private void tbProVouType_Enter(object sender, EventArgs e)
        {
            txtProVouType.Focus();
        }

        private void btnPDCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCircular_pv.Text))
            {
                MessageBox.Show("Please select a circular.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCircular_pv.Focus();
                return;

            }
            DialogResult dgr = MessageBox.Show("Do you want to Cancel the selected circular number?", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dgr == DialogResult.Yes)
            {
                CHNLSVC.Sales.UpdatePromotionVoucherStatus(txtCircular_pv.Text, 0, BaseCls.GlbUserID);
                MessageBox.Show("Successfuly Completed.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearPV();
            }
        }

        #endregion

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

        private void btnPAApprove_Click(object sender, EventArgs e)
        {
            try
            {


                DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtCircular.Text, txtPromoCode.Text, txtBook.Text, txtLevel.Text);
                if (dt.Rows.Count > 0)
                {
                    string error = "";
                    List<string> _errList = new List<string>();
                    List<string> _promoCodes = new List<string>();
                    if (lstPromoItem.Items.Count <= 0)
                    {
                        MessageBox.Show("Please add promotions from list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    foreach (ListViewItem item in lstPromoItem.Items)
                    {
                        if (item.Checked)
                            _promoCodes.Add(item.Text);
                    }
                    if (_promoCodes.Count <= 0)
                    {
                        MessageBox.Show("Please select promotions from list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (lblPAStatus.Text != "Pening Approval")
                    {
                        MessageBox.Show("Please valied cricular from list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //kapila 16/12/2016
                    Boolean _isSupNotFound = false;
                    MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                    foreach (ListViewItem promoList in lstPromoItem.Items)
                    {
                        string promo = promoList.Text;

                        if (promoList.Checked == true)
                        {
                            //get supplier
                            List<PriceDetailRef> _tmpdetbyPromo = new List<PriceDetailRef>();
                            _tmpdetbyPromo = CHNLSVC.Sales.GetPriceByPromoCD(promo);
                            //check the price level is a transfer
                            PriceBookLevelRef _tbl = new PriceBookLevelRef();
                            _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _tmpdetbyPromo[0].Sapd_pb_tp_cd, _tmpdetbyPromo[0].Sapd_pbk_lvl_cd);
                            if (_tbl.Sapl_is_transfer == true)
                            {
                                DataTable _dt = CHNLSVC.Sales.GetTransPBSupplier(BaseCls.GlbUserComCode, _tmpdetbyPromo[0].Sapd_pb_tp_cd, _tmpdetbyPromo[0].Sapd_pbk_lvl_cd);
                                if (_dt.Rows.Count == 0)
                                {
                                    _isSupNotFound = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (_isSupNotFound == true)
                    {
                        MessageBox.Show("Supplier not found for quotation generation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MessageBox.Show("Do you want to approve this promotions", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    int result = CHNLSVC.Sales.ProcessPromotionApprove(_promoCodes, out  error, out  _errList, BaseCls.GlbUserID, BaseCls.GlbUserSessionID);
                    if (result > 0)
                    {
                        //kapila 18/4/2016
                        sendMail(txtCircular.Text);

                        //kapila 24/6/2016
                        genQuotation();

                        MessageBox.Show("Approved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Clear_Data();
                    }
                    else
                    {
                        MessageBox.Show("Error occurred while processing\n" + error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Please select valied promotion circluar.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void genQuotation()
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            foreach (ListViewItem promoList in lstPromoItem.Items)
            {
                string promo = promoList.Text;

                if (promoList.Checked == true)
                {
                    //get supplier
                    List<PriceDetailRef> _tmpdetbyPromo = new List<PriceDetailRef>();
                    _tmpdetbyPromo = CHNLSVC.Sales.GetPriceByPromoCD(promo);
                    //check the price level is a transfer
                    PriceBookLevelRef _tbl = new PriceBookLevelRef();
                    _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _tmpdetbyPromo[0].Sapd_pb_tp_cd, _tmpdetbyPromo[0].Sapd_pbk_lvl_cd);
                    if (_tbl.Sapl_is_transfer == true)
                    {
                        DataTable _dt = CHNLSVC.Sales.GetTransPBSupplier(BaseCls.GlbUserComCode, _tmpdetbyPromo[0].Sapd_pb_tp_cd, _tmpdetbyPromo[0].Sapd_pbk_lvl_cd);
                        foreach (DataRow r in _dt.Rows)
                        {
                            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(r["sritc_to_com"].ToString(), r["sritc_sup"].ToString(), string.Empty, string.Empty, "S");
                            if (_masterBusinessCompany.Mbe_cd != null)
                            {
                                //collect quo header
                                QuotationHeader _saveHdr = new QuotationHeader();
                                _saveHdr.Qh_seq_no = 0;
                                _saveHdr.Qh_add1 = "";
                                _saveHdr.Qh_add2 = "";
                                _saveHdr.Qh_com = r["sritc_to_com"].ToString();
                                _saveHdr.Qh_cre_by = BaseCls.GlbUserID;
                                _saveHdr.Qh_cur_cd = "LKR";
                                _saveHdr.Qh_del_cusadd1 = "";
                                _saveHdr.Qh_del_cusadd2 = "";
                                _saveHdr.Qh_del_cuscd = "";
                                _saveHdr.Qh_del_cusfax = "";
                                _saveHdr.Qh_del_cusid = "";
                                _saveHdr.Qh_del_cusname = "";
                                _saveHdr.Qh_del_custel = "";
                                _saveHdr.Qh_del_cusvatreg = null;
                                _saveHdr.Qh_dt = Convert.ToDateTime(DateTime.Now.Date);
                                _saveHdr.Qh_ex_dt = Convert.ToDateTime(_tmpdetbyPromo[0].Sapd_to_date);      //price to date
                                _saveHdr.Qh_ex_rt = 1;
                                _saveHdr.Qh_frm_dt = Convert.ToDateTime(_tmpdetbyPromo[0].Sapd_from_date);     //price from date
                                _saveHdr.Qh_is_tax = false;
                                _saveHdr.Qh_jobno = "";
                                _saveHdr.Qh_mobi = "";
                                _saveHdr.Qh_mod_by = BaseCls.GlbUserID;
                                _saveHdr.Qh_no = "";
                                _saveHdr.Qh_party_cd = r["sritc_sup"].ToString();
                                _saveHdr.Qh_party_name = _masterBusinessCompany.Mbe_name;
                                _saveHdr.Qh_pc = BaseCls.GlbUserDefProf;
                                _saveHdr.Qh_ref = "";
                                _saveHdr.Qh_remarks = "AUTO-GEN";
                                _saveHdr.Qh_sales_ex = "";
                                _saveHdr.Qh_session_id = BaseCls.GlbUserSessionID;
                                _saveHdr.Qh_stus = "A";
                                _saveHdr.Qh_sub_tp = "N";
                                _saveHdr.Qh_tel = "";
                                _saveHdr.Qh_tp = "S";

                                _saveHdr.Qh_no = null;

                                _saveHdr.Qh_anal_5 = 0;

                                MasterAutoNumber masterAuto = new MasterAutoNumber();
                                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                                masterAuto.Aut_cate_tp = "PC";
                                masterAuto.Aut_direction = null;
                                masterAuto.Aut_modify_dt = null;
                                masterAuto.Aut_moduleid = "QUA";
                                masterAuto.Aut_number = 5;//what is Aut_number
                                masterAuto.Aut_start_char = "QUA";
                                masterAuto.Aut_year = null;

                                string QTNum;

                                //get item list
                                Int32 _lineNo = 1;
                                MasterItem _item = new MasterItem();

                                List<QoutationDetails> _QuotationItemList = new List<QoutationDetails>();

                                //  _tmpdetbyPromo = CHNLSVC.Sales.GetPriceByPromoCD(promo);
                                foreach (PriceDetailRef _tmpList in _tmpdetbyPromo)
                                {

                                    _item = CHNLSVC.Inventory.GetItem(r["sritc_to_com"].ToString(), _tmpList.Sapd_itm_cd);

                                    QoutationDetails _tempItem = new QoutationDetails();
                                    _tempItem.Qd_amt = 0;
                                    _tempItem.Qd_cbatch_line = 0;
                                    _tempItem.Qd_cdoc_no = "";
                                    _tempItem.Qd_citm_line = 0;
                                    _tempItem.Qd_cost_amt = 0;
                                    _tempItem.Qd_dis_amt = 0;
                                    _tempItem.Qd_dit_rt = 0;
                                    _tempItem.Qd_frm_qty = 1;  //TODO
                                    _tempItem.Qd_to_qty = 999;   //TODO
                                    _tempItem.Qd_issue_qty = 0;
                                    _tempItem.Qd_itm_cd = _tmpList.Sapd_itm_cd;
                                    _tempItem.Qd_itm_desc = _item.Mi_longdesc;
                                    string _itemStatus = "GDLP";        //TODO
                                    _tempItem.Qd_itm_stus = _itemStatus;
                                    _tempItem.Qd_itm_tax = 0;
                                    _tempItem.Qd_line_no = _lineNo;
                                    _tempItem.Qd_nitm_cd = null;
                                    _tempItem.Qd_nitm_desc = null;
                                    _tempItem.Qd_no = "";
                                    _tempItem.Qd_pb_lvl = "";
                                    _tempItem.Qd_pb_price = 0;
                                    _tempItem.Qd_pb_seq = 0;
                                    _tempItem.Qd_pbook = "";
                                    _tempItem.Qd_quo_tp = "R";
                                    _tempItem.Qd_res_no = null;
                                    _tempItem.Qd_res_qty = 0;
                                    _tempItem.Qd_resbal_qty = 0;
                                    _tempItem.Qd_resitm_cd = "";
                                    _tempItem.Qd_resline_no = 0;
                                    _tempItem.Qd_resreq_no = null;
                                    _tempItem.Qd_seq_no = 0;
                                    _tempItem.Qd_tot_amt = 0;
                                    _tempItem.Qd_unit_price = Convert.ToDecimal(_tmpList.Sapd_itm_price);
                                    _tempItem.Mi_longdesc = _item.Mi_longdesc;
                                    _tempItem.Mi_model = _item.Mi_model;


                                    string _NormalPb = "";
                                    string _NormalLvl = "";

                                    MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

                                    _NormalPb = _mastercompany.Mc_anal7;
                                    _NormalLvl = _mastercompany.Mc_anal8;

                                    _tempItem.Qd_uom = _item.Mi_itm_uom;
                                    _tempItem.Qd_warr_rmk = "";
                                    _tempItem.Qd_warr_pd = 0;
                                    _lineNo = _lineNo++;
                                    _QuotationItemList.Add(_tempItem);
                                }

                                List<QuotationSerial> _QuSerLst = new List<QuotationSerial>();
                                Int32 row_aff = (Int32)CHNLSVC.Sales.Quotation_save(_saveHdr, _QuotationItemList, masterAuto, _QuSerLst, null, null, null, false, null, null, out QTNum);
                            }
                        }
                    }
                }
            }


        }
        private void TextBoxLocation_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TextBoxLocation.Clear();
                    TextBoxLocation.Focus();
                    txtDPriceLevel.Text = "";
                    txtDPricebook.Text = "";
                    txtItemStatus.Text = "";
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceLevel.Text = "";
                    return;
                }

                else
                {
                    txtDPricebook.Text = "";
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    txtAlertPriceLevel.Text = "";
                    txtAlertPriceBook.Text = "";
                    DataTable dt = CHNLSVC.Sales.Load_Default_PriceBook(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                    DataTable dtPromo = CHNLSVC.Sales.Load_Promotion_PriceBook(BaseCls.GlbUserComCode, TextBoxLocation.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtDPricebook.Text = dt.Rows[0]["SADD_PB"].ToString();
                        txtDPriceLevel.Text = dt.Rows[0]["SADD_P_LVL"].ToString();
                        txtItemStatus.Text = dt.Rows[0]["SADD_DEF_STUS"].ToString();
                    }
                    if (dtPromo != null && dtPromo.Rows.Count > 0)
                    {
                        txtAlertPriceBook.Text = dtPromo.Rows[0]["SADD_PB"].ToString();
                        txtAlertPriceLevel.Text = dtPromo.Rows[0]["SADD_P_LVL"].ToString();

                    }

                }

            }
        }

        private void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters3(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                //DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = TextBoxLocation;
                //_CommonSearch.ShowDialog();
                //TextBoxLocation.Select();
                TextBoxLocation_MouseDoubleClick(null, null);

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

        private void txtDPricebook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPricebook.Text = "";
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }


                if (string.IsNullOrEmpty(txtDPricebook.Text.Trim())) return;

                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), "", txtDPricebook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPricebook.Clear();
                    txtDPricebook.Focus();
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    //return;
                }
                else
                {
                    //txtDPriceLevel.Text = "";
                    //txtItemStatus.Text = "";
                    //return;
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

        private void txtDPriceLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPriceLevel.Text = "";
                    txtDPricebook.Text = "";
                    txtItemStatus.Text = "";
                    //TextBoxLocation.Focus();

                    return;
                }


                if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim())) return;
                if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
                {
                    MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }
                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), txtDPriceLevel.Text.Trim(), txtDPricebook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price level", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPriceLevel.Clear();
                    txtDPriceLevel.Focus();
                    txtItemStatus.Text = "";
                    //return;
                }
                else
                {
                    //txtItemStatus.Text = "";
                    //return;
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

        private void txtItemStatus_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPriceLevel.Text = "";
                    txtDPricebook.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }
                if (string.IsNullOrEmpty(txtItemStatus.Text.Trim())) return;
                if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
                {
                    MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";

                    return;
                }
                if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim()))
                {
                    MessageBox.Show("Please select the price level.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // txtDPriceLevel.Text = "";
                    txtItemStatus.Text = "";
                    return;
                }

                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(BaseCls.GlbUserComCode, "", txtDPriceLevel.Text.Trim(), txtDPricebook.Text.Trim(), txtItemStatus.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price level", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItemStatus.Clear();
                    txtItemStatus.Focus();
                    //return;
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

        private void txtAlertPriceBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceLevel.Text = "";

                    return;
                }


                if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim())) return;

                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), "", txtAlertPriceBook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceBook.Clear();
                    txtAlertPriceBook.Focus();
                    txtAlertPriceLevel.Text = "";

                    //return;
                }
                else
                {
                    //txtAlertPriceLevel.Text = "";
                    //return;
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

        private void txtAlertPriceLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
                {
                    MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceLevel.Text = "";

                    return;
                }


                if (string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim())) return;
                if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
                {
                    MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceLevel.Text = "";
                    return;
                }
                DataTable _tbl = CHNLSVC.Sales.Check_price_bookDetails(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), txtAlertPriceLevel.Text.Trim(), txtAlertPriceBook.Text.Trim(), "");
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price level", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceLevel.Clear();
                    txtAlertPriceLevel.Focus();
                    // return;
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

        private void txtDPricebook_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDPriceBook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();

                return;
            }
            DataTable dtpb = CHNLSVC.Sales.Load_PcWise_PriceBook(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim());
            if (dtpb != null && dtpb.Rows.Count > 0)
            {
                pnlPriceBook.Visible = true;
                dgvPriceBookDetails.AutoGenerateColumns = false;
                dgvPriceBookDetails.DataSource = dtpb;


            }
            else
            {
                MessageBox.Show("No data Found!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnDPriceLevel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
            {
                MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDPricebook.Text = "";
                txtDPricebook.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_PcWise_Price_level(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), txtDPricebook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                pnlPriceLevel.Visible = true;
                dgvPricelevel.AutoGenerateColumns = false;
                dgvPricelevel.DataSource = dtpl;

            }
            else
            {
                MessageBox.Show("No data Found!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
            {
                MessageBox.Show("Please select the default price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDPricebook.Text = "";
                txtDPricebook.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim()))
            {
                MessageBox.Show("Please select the default price level.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDPriceLevel.Text = "";
                txtDPriceLevel.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtItemStatus.Text.Trim()))
            {
                MessageBox.Show("Please select the item Status.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtItemStatus.Text = "";
                txtItemStatus.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim()))
                {
                    MessageBox.Show("Please select the promotion price level.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceLevel.Text = "";
                    txtAlertPriceLevel.Focus();
                    return;
                }

            }
            if (!string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
                {
                    MessageBox.Show("Please select the promotion price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAlertPriceBook.Text = "";
                    txtAlertPriceBook.Focus();
                    return;
                }

            }

            //if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
            //{
            //    MessageBox.Show("Please select the promotion price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtAlertPriceBook.Text = "";
            //    txtAlertPriceBook.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtAlertPriceLevel.Text.Trim()))
            //{
            //    MessageBox.Show("Please select the promotion price level.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtAlertPriceLevel.Text = "";
            //    txtAlertPriceLevel.Focus();
            //    return;
            //}

            //update process going here.......


            try
            {
                objdefaultupdate = new Deposit_Bank_Pc_wise();

                objdefaultupdate.Stus = txtItemStatus.Text.Trim();
                objdefaultupdate.Profit_center = TextBoxLocation.Text.Trim();
                objdefaultupdate.Company = BaseCls.GlbUserComCode;
                objdefaultupdate.Price_book = txtDPricebook.Text.Trim();
                objdefaultupdate.Price_lvl = txtDPriceLevel.Text.Trim();
                objdefaultupdate.Modifyby = BaseCls.GlbUserID;

                objpromoupdate = new Deposit_Bank_Pc_wise();
                objpromoupdate.Profit_center = TextBoxLocation.Text.Trim();
                objpromoupdate.Company = BaseCls.GlbUserComCode;
                objpromoupdate.Promo_p_book = txtAlertPriceBook.Text.Trim();
                objpromoupdate.Promo_price_lvl = txtAlertPriceLevel.Text.Trim();
                objpromoupdate.Modifyby = BaseCls.GlbUserID;

                if (MessageBox.Show("Are you sure want to update ?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                string _error = "";
                int result = CHNLSVC.Sales.Update_To_Parameters(objdefaultupdate, objpromoupdate, out _error);
                if (result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Records updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void btnItemStatusSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDPricebook.Text.Trim()))
            {
                MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDPricebook.Text = "";
                txtDPricebook.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDPriceLevel.Text.Trim()))
            {
                MessageBox.Show("Please select the price level.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDPriceLevel.Text = "";
                txtDPriceLevel.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_Item_dets(BaseCls.GlbUserComCode, txtDPriceLevel.Text.Trim(), txtDPricebook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                pnlItemStatus.Visible = true;
                dgvItemStatus.AutoGenerateColumns = false;
                dgvItemStatus.DataSource = dtpl;

            }
            else
            {
                MessageBox.Show("No data Found!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvPriceBookDetails_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{

            //    txtDPricebook.Text = dgvPriceBookDetails["clmPriceBook", e.RowIndex].Value.ToString();
            //    txtDPriceLevel.Text = "";
            //    txtItemStatus.Text = "";
            //    pnlPriceBook.Visible = false;
            //    txtDPricebook.Focus();

            //}
        }

        private void dgvPricelevel_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{

            //    txtDPriceLevel.Text = dgvPricelevel["clmpricelevel", e.RowIndex].Value.ToString();
            //    txtItemStatus.Text = "";

            //    pnlPriceLevel.Visible = false;
            //    txtItemStatus.Focus();
            //}
        }

        private void dgvItemStatus_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{

            //    txtItemStatus.Text = dgvItemStatus["clmItemStatus", e.RowIndex].Value.ToString();
            //    pnlItemStatus.Visible = false;
            //    txtAlertPriceBook.Focus();
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TextBoxLocation.Text = "";
            txtDPricebook.Text = "";
            txtDPriceLevel.Text = "";
            txtItemStatus.Text = "";
            txtAlertPriceLevel.Text = "";
            txtAlertPriceBook.Text = "";

        }

        private void btnAlertPbookSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();

                return;
            }
            DataTable dtpb = CHNLSVC.Sales.Load_PcWise_PriceBook(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim());
            if (dtpb != null && dtpb.Rows.Count > 0)
            {
                pnlAlertPB.Visible = true;
                dgvAlertPb.AutoGenerateColumns = false;
                dgvAlertPb.DataSource = dtpb;

            }
            else
            {
                MessageBox.Show("No data Found!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAlertPlevelSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim()))
            {
                MessageBox.Show("Please select the profit center.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxLocation.Text = "";
                TextBoxLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtAlertPriceBook.Text.Trim()))
            {
                MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlertPriceBook.Text = "";
                txtAlertPriceBook.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_PcWise_Price_level(BaseCls.GlbUserComCode, TextBoxLocation.Text.Trim(), txtAlertPriceBook.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                pnlAlertPriceLevel.Visible = true;
                dgvAlertPricelevel.AutoGenerateColumns = false;
                dgvAlertPricelevel.DataSource = dtpl;

            }
            else
            {
                MessageBox.Show("No data Found!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClosePricelevel_Click(object sender, EventArgs e)
        {
            pnlPriceLevel.Visible = false;
        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {
            pnlPriceBook.Visible = false;
        }

        private void btnCloseItem_Click(object sender, EventArgs e)
        {
            pnlItemStatus.Visible = false;
        }

        private void btnAlertPLClose_Click(object sender, EventArgs e)
        {
            pnlAlertPriceLevel.Visible = false;

        }

        private void btnAlertPBClose_Click(object sender, EventArgs e)
        {
            pnlAlertPB.Visible = false;
        }

        private void dgvAlertPricelevel_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{

            //    txtAlertPriceLevel.Text = dgvAlertPricelevel["clmAlertPl", e.RowIndex].Value.ToString();
            //    pnlAlertPriceLevel.Visible = false;


            //}

        }

        private void dgvAlertPb_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{

            //    txtAlertPriceBook.Text = dgvAlertPb["clmAlertpb", e.RowIndex].Value.ToString();
            //    txtAlertPriceLevel.Text = "";
            //    pnlAlertPB.Visible = false;
            //    txtAlertPriceLevel.Focus();

            //}
        }

        private void TextBoxLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxLocation;
            _CommonSearch.ShowDialog();
            //TextBoxLocation.Select();
            txtDPricebook.Focus();
        }

        private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                TextBoxLocation_MouseDoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtDPricebook.Focus();
            }
        }

        private void txtDPricebook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //pnlPriceBook.Visible = true;
            btnDPriceBook_Click(null, null);
        }

        private void txtDPricebook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDPricebook_MouseDoubleClick(null, null);
            }
        }

        private void txtDPriceLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDPriceLevel_Click(null, null);
        }

        private void txtDPriceLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDPriceLevel_MouseDoubleClick(null, null);
            }
        }

        private void txtItemStatus_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItemStatusSearch_Click(null, null);
        }

        private void txtItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtItemStatus_MouseDoubleClick(null, null);
            }
        }

        private void txtAlertPriceBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnAlertPbookSearch_Click(null, null);
        }

        private void txtAlertPriceBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAlertPriceBook_MouseDoubleClick(null, null);
            }

        }

        private void txtAlertPriceLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnAlertPlevelSearch_Click(null, null);
        }

        private void txtAlertPriceLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAlertPriceLevel_MouseDoubleClick(null, null);
            }
        }

        private void dgvAlertPb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int row = dgvAlertPb.CurrentCell.RowIndex;
                //GetSelectedRowData();
                //obj_TragetTextBox.Text = GetResult(GlbSelectData, ReturnIndex);
                //this.Close();

                txtAlertPriceBook.Text = dgvAlertPb["clmAlertpb", row].Value.ToString();
                txtAlertPriceLevel.Text = "";
                pnlAlertPB.Visible = false;
                txtAlertPriceLevel.Focus();

            }

        }

        private void dgvAlertPb_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                txtAlertPriceBook.Text = dgvAlertPb["clmAlertpb", e.RowIndex].Value.ToString();
                txtAlertPriceLevel.Text = "";
                pnlAlertPB.Visible = false;
                txtAlertPriceLevel.Focus();

            }
        }

        private void dgvPricelevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int row = dgvPricelevel.CurrentCell.RowIndex;

                txtDPriceLevel.Text = dgvPricelevel["clmpricelevel", row].Value.ToString();
                txtItemStatus.Text = "";

                pnlPriceLevel.Visible = false;
                txtItemStatus.Focus();

            }

        }

        private void dgvPricelevel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                txtDPriceLevel.Text = dgvPricelevel["clmpricelevel", e.RowIndex].Value.ToString();
                txtItemStatus.Text = "";

                pnlPriceLevel.Visible = false;
                txtItemStatus.Focus();
            }
        }

        private void dgvAlertPricelevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int row = dgvAlertPricelevel.CurrentCell.RowIndex;

                txtAlertPriceLevel.Text = dgvAlertPricelevel["clmAlertPl", row].Value.ToString();
                pnlAlertPriceLevel.Visible = false;


            }

        }

        private void dgvAlertPricelevel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                txtAlertPriceLevel.Text = dgvAlertPricelevel["clmAlertPl", e.RowIndex].Value.ToString();
                pnlAlertPriceLevel.Visible = false;

            }
        }

        private void dgvPriceBookDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                txtDPricebook.Text = dgvPriceBookDetails["clmPriceBook", e.RowIndex].Value.ToString();
                txtDPriceLevel.Text = "";
                txtItemStatus.Text = "";
                pnlPriceBook.Visible = false;
                txtDPriceLevel.Focus();

            }
        }

        private void dgvPriceBookDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int row = dgvPriceBookDetails.CurrentCell.RowIndex;

                txtDPricebook.Text = dgvPriceBookDetails["clmPriceBook", row].Value.ToString();
                txtDPriceLevel.Text = "";
                txtItemStatus.Text = "";
                pnlPriceBook.Visible = false;
                txtDPriceLevel.Focus();

            }
        }

        private void dgvItemStatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                txtItemStatus.Text = dgvItemStatus["clmItemStatus", e.RowIndex].Value.ToString();
                pnlItemStatus.Visible = false;
                txtAlertPriceBook.Focus();
            }
        }

        private void dgvItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int row = dgvItemStatus.CurrentCell.RowIndex;

                txtItemStatus.Text = dgvItemStatus["clmItemStatus", row].Value.ToString();
                pnlItemStatus.Visible = false;
                txtAlertPriceBook.Focus();

            }

        }

        private void btndApp_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDpb.Text))
                {
                    MessageBox.Show("Please enter applicable price book.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDpb.Text = "";
                    txtDpb.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDpblevel.Text))
                {
                    MessageBox.Show("Please enter applicable price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDpblevel.Text = "";
                    txtDpblevel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDinvType.Text))
                {
                    MessageBox.Show("Please enter applicable invoice type.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDinvType.Text = "";
                    txtDinvType.Focus();
                    return;
                }

                if (chkDisApp.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtDdisrate.Text))
                    {
                        MessageBox.Show("Defalut discount rate is missing.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDdisrate.Text = "";
                        txtDdisrate.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtDdisrate.Text) > 100 || Convert.ToDecimal(txtDdisrate.Text) < 0)
                    {
                        MessageBox.Show("Rate should be between 0 to 100.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDdisrate.Text = "";
                        txtDdisrate.Focus();
                        return;
                    }
                }



                Boolean _isValidpc = false;
                foreach (ListViewItem Item in lstSubChannel.Items)
                {
                    string _item = Item.Text;

                    if (Item.Checked == true)
                    {
                        _isValidpc = true;
                        goto L3;
                    }
                }
            L3:

                if (_isValidpc == false)
                {
                    MessageBox.Show("No any applicable sub channel is select.", "Price Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SAR_DOC_CHANNEL_PRICE_DEFN _tmpList = new SAR_DOC_CHANNEL_PRICE_DEFN();
                string _invPreTp = "";
                foreach (ListViewItem mAppPcList in lstSubChannel.Items)
                {
                    string itm = mAppPcList.Text;

                    if (mAppPcList.Checked == true)
                    {
                        if (chkDefpblvl.Checked)
                        {
                            SAR_DOC_CHANNEL_PRICE_DEFN result = _defDetdef.Find(x => x.Sdcp_def == true && x.Sdcp_sub_chanl == itm);
                            //if (result != null)
                            //{
                            //    MessageBox.Show("You have already define default value for sub channel - " + itm, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            if (CHNLSVC.Sales.CheckAssignChannelDef(itm, BaseCls.GlbUserComCode) == 1)
                            {
                                MessageBox.Show("You have already define default value for sub channel - " + itm, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }


                            if (string.IsNullOrEmpty(txtDstatus.Text))
                            {
                                MessageBox.Show("Enter default status.", "Price Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtDstatus.Focus();
                                return;
                            }
                        }


                        _tmpList = new SAR_DOC_CHANNEL_PRICE_DEFN();
                        _invPreTp = "";
                        _tmpList.Sdcp_chk_credit_bal = chkDcrerate.Checked;
                        _tmpList.Sdcp_com = BaseCls.GlbUserComCode;
                        _tmpList.Sdcp_cre_by = BaseCls.GlbUserID;
                        _tmpList.Sdcp_cre_when = DateTime.Today.Date;
                        _tmpList.Sdcp_def = chkDefpblvl.Checked;
                        _tmpList.Sdcp_def_stus = txtDstatus.Text;

                        if (!string.IsNullOrEmpty(txtDdisrate.Text))
                        {
                            _tmpList.Sdcp_disc_rt = Convert.ToDecimal(txtDdisrate.Text);
                        }
                        else
                        {
                            _tmpList.Sdcp_disc_rt = 0;
                        }
                        _tmpList.Sdcp_doc_tp = txtDinvType.Text;
                        _tmpList.Sdcp_is_bank_ex_rt = 1;
                        _tmpList.Sdcp_is_disc = chkDisApp.Checked;
                        _tmpList.Sdcp_mod_by = BaseCls.GlbUserID;
                        _tmpList.Sdcp_mod_when = DateTime.Today.Date;
                        _tmpList.Sdcp_p_lvl = txtDpblevel.Text;
                        _tmpList.Sdcp_pb = txtDpb.Text;
                        _tmpList.Sdcp_sub_chanl = itm;
                        _tmpList.Sdcp_def_pb = chkDefAlreart.Checked;
                        _defDetdef.Add(_tmpList);

                    }
                }

                dgvDpbDef.AutoGenerateColumns = false;
                dgvDpbDef.DataSource = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();
                dgvDpbDef.DataSource = _defDetdef;
                chkDcrerate.Checked = false;
                chkDefpblvl.Checked = false;
                txtDstatus.Text = "";
                txtDdisrate.Text = "";
                txtDinvType.Text = "";
                chkDisApp.Checked = false;
                txtDpblevel.Text = "";
                txtDpb.Text = "";
                chkDefAlreart.Checked = false;

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

        private void btnDsearchpb_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDpb;
                _CommonSearch.ShowDialog();
                txtDpb.Select();
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

        private void btnDsearchpl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDpb.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDpb.Clear();
                    txtDpb.Focus();
                    return;
                }
                _Stype = "DefMaintain";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDpblevel;
                _CommonSearch.ShowDialog();
                txtDpblevel.Select();

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

        private void btnDsearchinv_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDinvType;
                _CommonSearch.ShowDialog();
                txtDinvType.Select();

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

        private void btnDsearchCha_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDchannel;
                _CommonSearch.ShowDialog();
                txtDchannel.Select();
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

        private void btnDsearchsChanel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDchannel.Text))
                {
                    MessageBox.Show("Please select channel.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDchannel.Text = "";
                    txtDchannel.Focus();
                    return;
                }

                _Ltype = "DefMaintain";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDsubchannel;
                _CommonSearch.ShowDialog();
                txtDsubchannel.Select();
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

        private void btndsChannelAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_SubChannel(BaseCls.GlbUserComCode, txtDchannel.Text, txtDsubchannel.Text, null, null, null, null);
                foreach (DataRow drow in dt.Rows)
                {
                    lstSubChannel.Items.Add(drow["SubChannel"].ToString());


                }

                foreach (ListViewItem Item in lstSubChannel.Items)
                {
                    Item.Checked = true;
                }

                txtDchannel.Text = "";
                txtDsubchannel.Text = "";

                txtDsubchannel.Focus();
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

        private void btndSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSubChannel.Items)
            {
                Item.Checked = true;
            }
        }

        private void btndUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSubChannel.Items)
            {
                Item.Checked = false;
            }
        }

        private void btndClearAll_Click(object sender, EventArgs e)
        {
            lstSubChannel.Clear();
            txtDchannel.Text = "";
            txtDsubchannel.Text = "";

            lstSubChannel.Focus();
        }

        private void btndSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (_defDetdef == null || _defDetdef.Count == 0)
                {
                    MessageBox.Show("Cannot find define details.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }



                var DistinctItems = _defDetdef.GroupBy(x => x.Sdcp_sub_chanl).Select(y => y.First());

                foreach (var item in DistinctItems)
                {
                    if (CHNLSVC.Sales.CheckAssignChannelDef(item.Sdcp_sub_chanl, BaseCls.GlbUserComCode) == 0)
                    {
                        SAR_DOC_CHANNEL_PRICE_DEFN result = _defDetdef.Find(x => x.Sdcp_def == true);

                        if (result == null)
                        {
                            MessageBox.Show("You have not setup default value for sub channel - " + item.Sdcp_sub_chanl, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }


                row_aff = (Int32)CHNLSVC.Sales.SavePcPriceDefinitionsChannel(_defDetdef);

                if (row_aff == 1)
                {

                    MessageBox.Show("Price level permission activated.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Clear_MaintainceDef();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnDsearchStat_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(txtDpb.Text.Trim()))
            {
                MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDpb.Text = "";
                txtDpb.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDpblevel.Text.Trim()))
            {
                MessageBox.Show("Please select the price level.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDpblevel.Text = "";
                txtDpblevel.Focus();
                return;
            }
            DataTable dtpl = CHNLSVC.Sales.Load_Item_dets(BaseCls.GlbUserComCode, txtDpblevel.Text.Trim(), txtDpb.Text.Trim());
            if (dtpl != null && dtpl.Rows.Count > 0)
            {
                pnlDitemStatus.Visible = true;
                dgvDitemstatus.AutoGenerateColumns = false;
                dgvDitemstatus.DataSource = dtpl;

            }
            else
            {
                MessageBox.Show("No data Found!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvAlertPb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDitemstatus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                txtDstatus.Text = dgvDitemstatus["col_dstatus", e.RowIndex].Value.ToString();
                pnlDitemStatus.Visible = false;

            }
        }

        private void dgvDitemstatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClosepnlstatus_Click(object sender, EventArgs e)
        {
            pnlDitemStatus.Visible = false;
        }

        private void dgvMDef_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btndload_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDpb.Text) && string.IsNullOrEmpty(txtDpblevel.Text) && string.IsNullOrEmpty(txtDinvType.Text) && string.IsNullOrEmpty(txtDsubchannel.Text))
                {
                    MessageBox.Show("Please enter one of above selection categories.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDpb.Text) && !string.IsNullOrEmpty(txtDpblevel.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMBook.Focus();
                    return;
                }

                _defDetdef = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();

                _defDetdef = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevelSubChannel(BaseCls.GlbUserComCode, txtDpb.Text, txtDpblevel.Text, txtDinvType.Text, txtDsubchannel.Text);

                dgvDpbDef.AutoGenerateColumns = false;
                dgvDpbDef.DataSource = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();
                dgvDpbDef.DataSource = _defDetdef;

                btndSave.Enabled = false;
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

        private void label224_Click(object sender, EventArgs e)
        {

        }

        private void chkDefpblvl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefpblvl.Checked)
            {
                txtDstatus.Enabled = true;
                btnDsearchStat.Enabled = true;
                txtDstatus.Text = "GOD";
            }
            else
            {
                txtDstatus.Enabled = false;
                btnDsearchStat.Enabled = false;
                txtDstatus.Clear();
            }
        }

        private void dgvDpbDef_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (_defDetdef == null || _defDetdef.Count == 0) return;

                if (MessageBox.Show("Do you want to remove access ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {


                    int row_id = e.RowIndex;
                    string _schannel = dgvDpbDef.Rows[e.RowIndex].Cells["Sub_Channel"].Value.ToString();
                    string _invtp = dgvDpbDef.Rows[e.RowIndex].Cells["col_dinvType"].Value.ToString();
                    string _lvl = dgvDpbDef.Rows[e.RowIndex].Cells["col_d_level"].Value.ToString();
                    string _com = dgvDpbDef.Rows[e.RowIndex].Cells["col_d_com"].Value.ToString();
                    string _pb = dgvDpbDef.Rows[e.RowIndex].Cells["col_d_book"].Value.ToString();




                    List<SAR_DOC_CHANNEL_PRICE_DEFN> _temp = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();
                    _temp = _defDetdef;


                    _temp.RemoveAll(x => x.Sdcp_com == _com && x.Sdcp_sub_chanl == _schannel && x.Sdcp_doc_tp == _invtp && x.Sdcp_pb == _pb && x.Sdcp_p_lvl == _lvl);
                    _defDetdef = _temp;

                    dgvDpbDef.AutoGenerateColumns = false;
                    dgvDpbDef.DataSource = new List<PriceDefinitionRef>();
                    dgvDpbDef.DataSource = _defDet;


                    row_aff = (Int32)CHNLSVC.Sales.RemovePriceAccessSubChannel(_schannel, _invtp, _lvl, _com, _pb, BaseCls.GlbUserID);

                    if (row_aff == 1)
                    {

                        MessageBox.Show("Price level permission removed.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            MessageBox.Show(_msg, "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Faild to update.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMBook_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDpb_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDpb.Text)) return;
                DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtDpb.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Please enter valid price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDpb.Clear();
                    txtDpb.Focus();
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

        private void txtDpblevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDpblevel.Text)) return;
                if (string.IsNullOrEmpty(txtDpb.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDpblevel.Clear(); txtDpb.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtDpb.Text.Trim(), txtDpblevel.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                { MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDpblevel.Clear(); txtDpblevel.Focus(); return; }
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

        private void txtDinvType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDinvType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("srtp_cd") == txtDinvType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid invoice type.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDinvType.Clear();
                    txtDinvType.Focus();
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

        private void txtMLevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDpblevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMInvType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDinvType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtmSubChannel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDpb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDsearchpb_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDpblevel.Focus();
            }
        }

        private void txtDpblevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDsearchpl_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDinvType.Focus();
            }
        }

        private void txtDinvType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDsearchinv_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDchannel.Focus();
            }
        }

        private void txtDchannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDsearchCha_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDsubchannel.Focus();
            }
        }

        private void txtDsubchannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnDsearchsChanel_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btndsChannelAdd.Focus();
            }
        }

        private void txtDpb_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDpb_DoubleClick(object sender, EventArgs e)
        {
            btnDsearchpb_Click(null, null);
        }

        private void txtDpblevel_DoubleClick(object sender, EventArgs e)
        {
            btnDsearchpl_Click(null, null);
        }

        private void txtDinvType_DoubleClick(object sender, EventArgs e)
        {
            btnDsearchinv_Click(null, null);
        }

        private void txtDchannel_DoubleClick(object sender, EventArgs e)
        {
            btnDsearchCha_Click(null, null);
        }

        private void txtDsubchannel_DoubleClick(object sender, EventArgs e)
        {
            btnDsearchsChanel_Click(null, null);
        }

        private void txtDchannel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDsubchannel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDstatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDstatus_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDstatus.Text))
            {
                DataTable dtpl = CHNLSVC.Sales.Load_Item_dets(BaseCls.GlbUserComCode, txtDpblevel.Text.Trim(), txtDpb.Text.Trim());
                if (dtpl != null && dtpl.Rows.Count > 0)
                {
                    DataRow dataRow = dtpl.AsEnumerable().FirstOrDefault(r => Convert.ToString(r["mis_cd"]) == txtDstatus.Text);
                    if (dataRow != null)
                    {
                        MessageBox.Show("Invalid Item Status.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDstatus.Clear();
                        txtDstatus.Focus();
                        return;
                    }

                }
            }
        }

        private void btndClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Clear_MaintainceDef();
            }
        }

        private void Clear_MaintainceDef()
        {
            _defDetdef = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();
            _Stype = "";
            _Ltype = "";

            txtDdisrate.Text = Convert.ToString(0);
            txtDpb.Text = "";
            txtDpblevel.Text = "";
            txtDinvType.Text = "";
            txtDchannel.Text = "";
            txtDsubchannel.Text = "";
            chkDisApp.Checked = false;

            chkDcrerate.Checked = false;
            chkDefpblvl.Checked = false;
            txtDstatus.Text = "";
            chkDefAlreart.Checked = false;
            btndSave.Enabled = true;
            lstSubChannel.Clear();
            dgvDpbDef.AutoGenerateColumns = false;
            dgvDpbDef.DataSource = new List<SAR_DOC_CHANNEL_PRICE_DEFN>();
        }

        private void btnSearchBatLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBatchLoc;
                _CommonSearch.ShowDialog();
                txtBatchLoc.Select();
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

        private void txtBatchLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchBatLoc_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                dtpProcessFrom.Focus();
            }
        }

        private void btnSearchBatCat1_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBatCat1;
                _CommonSearch.txtSearchbyword.Text = txtBatCat1.Text;
                _CommonSearch.ShowDialog();
                txtBatCat1.Focus();
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

        private void txtBatCat1_DoubleClick(object sender, EventArgs e)
        {
            btnSearchBatCat1_Click(null, null);
        }

        private void txtBatCat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchBatCat1_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void btnSearchBatCat2_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBatCat2;
                _CommonSearch.txtSearchbyword.Text = txtBatCat2.Text;
                _CommonSearch.ShowDialog();
                txtBatCat2.Focus();
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

        private void txtBatCat2_DoubleClick(object sender, EventArgs e)
        {
            btnSearchBatCat2_Click(null, null);
        }

        private void btnSearchBatCat3_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBatCat3;
                _CommonSearch.txtSearchbyword.Text = txtBatCat3.Text;
                _CommonSearch.ShowDialog();
                txtBatCat3.Focus();
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

        private void txtBatCat3_DoubleClick(object sender, EventArgs e)
        {
            btnSearchBatCat3_Click(null, null);
        }

        private void txtBatCat3_KeyDown(object sender, KeyEventArgs e)
        {
            btnSearchBatCat3_Click(null, null);
        }

        private void btnBatProApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpProcessTo.Value.Date < dtpProcessFrom.Value.Date)
                {
                    MessageBox.Show("Date range mismatching.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpProcessTo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMargin.Text))
                {
                    MessageBox.Show("Please enter margin.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMargin.Focus();
                    return;
                }

                if (!IsNumeric(txtMargin.Text))
                {
                    MessageBox.Show("Please enter valid margin.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMargin.Text = "";
                    txtMargin.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtMargin.Text) > 100)
                {
                    MessageBox.Show("Please enter valid margin rate.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMargin.Text = "";
                    txtMargin.Focus();
                    return;
                }

                DataTable _batchPrice = new DataTable();
                dgBatchPrice.DataSource = _batchPrice;
                _batchPrice = CHNLSVC.Sales.GetDetailsforBatchPrice(BaseCls.GlbUserComCode, txtBatchLoc.Text.Trim(), Convert.ToDecimal(txtMargin.Text), txtBatCat1.Text.Trim(), txtBatCat2.Text.Trim(), txtBatCat3.Text.Trim(), txtBatchDoc.Text.Trim(), dtpProcessFrom.Value.Date, dtpProcessTo.Value.Date);
                dgBatchPrice.AutoGenerateColumns = false;
                dgBatchPrice.DataSource = _batchPrice;

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

        private void btnBatch_Click(object sender, EventArgs e)
        {
            if (pnlBatch.Visible == false)
            {
                pnlBatch.Visible = true;
            }
            else
            {
                pnlBatch.Visible = false;
            }
        }

        private void btnBatProConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _newSeq = 0;

                if (dgBatchPrice.Rows.Count <= 0)
                {
                    MessageBox.Show("Cannot find details to proceed.", "Price definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataGridViewRow row in dgBatchPrice.Rows)
                {
                    _newSeq = _newSeq + 1;
                    PriceDetailRef _newPriceDet = new PriceDetailRef();
                    _newPriceDet.Sapd_warr_remarks = txtWaraRemarks.Text.Trim();
                    _newPriceDet.Sapd_upload_dt = DateTime.Now;
                    _newPriceDet.Sapd_update_dt = DateTime.Now;
                    _newPriceDet.Sapd_to_date = Convert.ToDateTime(dtpTo.Value).Date;
                    _newPriceDet.Sapd_session_id = BaseCls.GlbUserSessionID;
                    _newPriceDet.Sapd_seq_no = _newSeq;
                    _newPriceDet.Sapd_qty_to = Convert.ToDecimal(row.Cells["col_FreeQty"].Value);
                    _newPriceDet.Sapd_qty_from = 1;
                    _newPriceDet.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                    _newPriceDet.Sapd_price_stus = "A";
                    _newPriceDet.Sapd_pbk_lvl_cd = txtLevel.Text.Trim();
                    _newPriceDet.Sapd_pb_tp_cd = txtBook.Text.Trim();
                    _newPriceDet.Sapd_pb_seq = _newSeq;
                    _newPriceDet.Sapd_no_of_use_times = 0;
                    _newPriceDet.Sapd_no_of_times = 9999;
                    _newPriceDet.Sapd_model = "BATCH";
                    _newPriceDet.Sapd_mod_when = DateTime.Now;
                    _newPriceDet.Sapd_mod_by = BaseCls.GlbUserID;
                    _newPriceDet.Sapd_margin = 0;
                    _newPriceDet.Sapd_lst_cost = 0;
                    _newPriceDet.Sapd_itm_price = Convert.ToDecimal(row.Cells["col_newPrice"].Value);
                    _newPriceDet.Sapd_itm_cd = row.Cells["col_itmCd"].Value.ToString();
                    _newPriceDet.Sapd_is_cancel = false;
                    _newPriceDet.Sapd_is_allow_individual = false;
                    _newPriceDet.Sapd_from_date = Convert.ToDateTime(dtpFrom.Value).Date;
                    _newPriceDet.Sapd_erp_ref = txtLevel.Text.Trim();
                    _newPriceDet.Sapd_dp_ex_cost = 0;
                    _newPriceDet.Sapd_day_attempt = 0;
                    _newPriceDet.Sapd_customer_cd = txtCusCode.Text.Trim();
                    _newPriceDet.Sapd_cre_when = DateTime.Now;
                    _newPriceDet.Sapd_cre_by = BaseCls.GlbUserID;
                    _newPriceDet.Sapd_circular_no = txtCircular.Text.Trim();
                    _newPriceDet.Sapd_cancel_dt = DateTime.MinValue;
                    _newPriceDet.Sapd_avg_cost = 0;
                    _newPriceDet.Sapd_apply_on = "0";
                    _newPriceDet.Sapd_batch_no = row.Cells["col_batchNo"].Value.ToString();
                    _newPriceDet.Sapd_doc_no = row.Cells["col_docNo"].Value.ToString();
                    _newPriceDet.Sapd_batch_seqno = Convert.ToInt32(row.Cells["col_batchSeq"].Value);
                    _newPriceDet.Sapd_itm_line_no = Convert.ToInt32(row.Cells["col_batchitmLine"].Value);
                    _newPriceDet.Sapd_batch_line_no = Convert.ToInt32(row.Cells["col_batchLine"].Value);
                    if (chkEndDate.Checked && btnAmend.Enabled)
                        _newPriceDet.Sapd_ser_upload = 6;
                    _list.Add(_newPriceDet);
                }

                dgvPriceDet.AutoGenerateColumns = false;
                dgvPriceDet.DataSource = new List<PriceDetailRef>();
                dgvPriceDet.DataSource = _list;

                pnlBatch.Visible = false;
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

        private void btnsrhpbbrand_Click(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtbbrd;
                _CommonSearch.ShowDialog();
                txtbbrd.Focus();
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

        private void btnsrhpbcat1_Click(object sender, EventArgs e)
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

        private void btnsrhpbcat2_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PBDEF";
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

        private void btnsrhpbcat3_Click(object sender, EventArgs e)
        {
            try
            {
                _Stype = "PBDEF";
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
                _CommonSearch.obj_TragetTextBox = txtBaseItem;
                _CommonSearch.ShowDialog();
                txtBaseItem.Select();
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


        private void BindCategoryTypes()
        {

            Dictionary<int, string> categoryType = new Dictionary<int, string>();

            categoryType.Add(3, "ITEM");
            categoryType.Add(4, "BRAND & SUB CAT");
            categoryType.Add(5, "BRAND & CAT");
            categoryType.Add(6, "BRAND & MAIN CAT");
            categoryType.Add(7, "BRAND");
            categoryType.Add(8, "SUB CAT");
            categoryType.Add(9, "CAT");
            categoryType.Add(10, "MAIN CAT");


            cmbSelectCat.DataSource = new BindingSource(categoryType, null);
            cmbSelectCat.DisplayMember = "Value";
            cmbSelectCat.ValueMember = "Key";


            Dictionary<string, string> categoryTypemain = new Dictionary<string, string>();
            categoryTypemain.Add("I", "Item");
            categoryTypemain.Add("C", "Charge");
            cmbCate.DataSource = new BindingSource(categoryTypemain, null);
            cmbCate.DisplayMember = "Value";
            cmbCate.ValueMember = "Key";


        }
        List<sar_pb_def_det> ItemBrandCat_List;
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
            if (ItemBrandCat_List == null)
            {
                ItemBrandCat_List = new List<sar_pb_def_det>();
            }
            try
            {

                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtMarkup.Text))
                {
                    MessageBox.Show("Please enter markup", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMarkup.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbCate.Text))
                {
                    MessageBox.Show("Please select category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbCate.Focus();
                    return;
                }


                if (cmbCate.SelectedItem.ToString() == "C")
                {
                    if (string.IsNullOrEmpty(txtCharge.Text))
                    {
                        MessageBox.Show("Please enter charge code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCharge.Focus();
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    if (txtCate3.Text == string.Empty)
                    {
                        MessageBox.Show("Specify sub category!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    if (txtCate2.Text == string.Empty)
                    {
                        MessageBox.Show("Specify  category!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    if (txtCate1.Text == string.Empty)
                    {
                        MessageBox.Show("Specify main category!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (cmbSelectCat.SelectedValue.ToString() == "5" || cmbSelectCat.SelectedValue.ToString() == "6" || cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    if (txtbbrd.Text == string.Empty)
                    {
                        MessageBox.Show("Specify brand also!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string selection = "";
                if (cmbSelectCat.SelectedValue.ToString() == "10")
                {
                    selection = "CATE1";
                    grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "9")
                {
                    selection = "CATE2";
                    grvSalesTypes.Columns[1].HeaderText = "Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "8")
                {
                    selection = "CATE3";
                    grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "7")
                {
                    selection = "BRAND";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "6")
                {
                    selection = "BRAND_CATE1";
                    grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "5")
                {
                    selection = "BRAND_CATE2";
                    grvSalesTypes.Columns[1].HeaderText = "Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "4")
                {
                    selection = "BRAND_CATE3";
                    grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                }
                else if (cmbSelectCat.SelectedValue.ToString() == "3")
                {
                    selection = "ITEM";
                    grvSalesTypes.Columns[1].HeaderText = "Item";
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

                DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtbbrd.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtCate3.Text, txtBaseItem.Text.Trim(), null, txtCircular.Text.Trim(), null);

                if (dt.Rows.Count > 0)
                {
                    //  grvSalesTypes.Columns[1].HeaderText = cmbSelectCat.Text;
                }
                List<sar_pb_def_det> addList = new List<sar_pb_def_det>();
                foreach (DataRow dr in dt.Rows)
                {
                    string code = dr["code"].ToString();
                    string brand = txtbbrd.Text;
                    sar_pb_def_det obj = new sar_pb_def_det(); //for display purpose
                    if (cmbSelectCat.SelectedValue.ToString() == "4" || cmbSelectCat.SelectedValue.ToString() == "5" || cmbSelectCat.SelectedValue.ToString() == "6")

                    //    if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE3")
                    {
                        obj.Spdd_brand = brand;
                        obj.Spdd_cat1 = txtCate1.Text.Trim();
                        obj.Spdd_cat2 = txtCate2.Text.Trim();
                        obj.Spdd_cat3 = txtCate3.Text;

                    }

                    else if (cmbSelectCat.SelectedValue.ToString() == "3")
                    {
                        obj.Spdd_item = code;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "7")
                    {
                        obj.Spdd_brand = brand;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "10")
                    {
                        obj.Spdd_cat1 = code;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "9")
                    {
                        obj.Spdd_cat2 = code;
                    }
                    else if (cmbSelectCat.SelectedValue.ToString() == "8")
                    {
                        obj.Spdd_cat3 = code;
                    }
                    else
                    {
                        obj.Spdd_brand = string.Empty;
                    }

                    obj.Spdd_Des = code;
                    obj.Spdd_margin = Convert.ToDecimal(txtMarkup.Text);
                    obj.Spdd_ch_code = txtCharge.Text;
                    try
                    {
                        //  obj.Spdd_Des = dr["descript"].ToString();
                    }
                    catch (Exception)
                    {
                        obj.Spdd_cat1 = "";
                    }
                    obj.Spdd_active = 1;
                    var _duplicate = from _dup in ItemBrandCat_List
                                     where _dup.Spdd_Des == obj.Spdd_Des && _dup.Spdd_brand == obj.Spdd_brand
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
                txtBaseItem.Text = "";
                txtCharge.Text = "";
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

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void btnAllpb_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in grvSalesTypes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                chk.Value = "True";
            }
        }

        private void btnAllnone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in grvSalesTypes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grvSalesTypes.Rows[gr.Index].Cells[0];
                chk.Value = "False";
            }
        }

        private void btnAllclr_Click(object sender, EventArgs e)
        {
            grvSalesTypes.DataSource = null;
        }

        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btnUploadItem_Click(object sender, EventArgs e)
        {
            try
            {
                string _msg = string.Empty;
                StringBuilder _errorLst = new StringBuilder();
                ItemBrandCat_List = new List<sar_pb_def_det>();
                if (string.IsNullOrEmpty(txtUploadItems.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Text = "";
                    txtUploadItems.Focus();
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


                        if (string.IsNullOrEmpty(_dr[5].ToString()))
                        {
                            continue;
                        }

                        //if (!char.IsNumber(Convert.ToChar(_dr[5].ToString())))
                        //{
                        //    continue;
                        //}

                        if (cmbSelectCat.SelectedValue.ToString() == "3")
                        {

                            if (string.IsNullOrEmpty(_dr[0].ToString()))
                            {
                                continue;
                            }
                            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                            if (_item == null)
                            {

                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid Item - " + _dr[0].ToString());
                                else _errorLst.Append(" and Invalid Item  - " + _dr[0].ToString());
                                continue;

                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[0].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Item " + _dr[0].ToString() + " duplicate");
                                    else _errorLst.Append(" and Item " + _dr[0].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_item = _dr[0].ToString();
                            _ref.Spdd_Des = _dr[0].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();

                            ItemBrandCat_List.Add(_ref);
                        }
                        else if (cmbSelectCat.SelectedValue.ToString() == "10")// Main category
                        {
                            if (string.IsNullOrEmpty(_dr[2].ToString())) continue;

                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[2].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("main category " + _dr[2].ToString() + " duplicate");
                                    else _errorLst.Append(" and main category " + _dr[2].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_cat1 = _dr[2].ToString();
                            _ref.Spdd_Des = _dr[2].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);

                        }
                        else if (cmbSelectCat.SelectedValue.ToString() == "7")// Brand
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[1].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand" + _dr[1].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand" + _dr[1].ToString() + " duplicate");
                                    continue;
                                }
                            }

                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            //_ref.Spdd_Des = _dr[1].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);


                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "9")//  sub category
                        {
                            if (!string.IsNullOrEmpty(_dr[3].ToString()))
                            {


                                MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                                if (subCate.Ric2_cd1 == null)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[3].ToString());
                                    else _errorLst.Append(" and invalid sub category  - " + _dr[3].ToString());
                                    continue;
                                }
                                if (ItemBrandCat_List != null)
                                {
                                    var _duplicate = from _dup in ItemBrandCat_List
                                                     where _dup.Spdd_Des == _dr[3].ToString()
                                                     select _dup;


                                    if (_duplicate.Count() > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("  category " + _dr[3].ToString() + " duplicate");
                                        else _errorLst.Append(" and   category " + _dr[3].ToString() + " duplicate");
                                        continue;
                                    }
                                }


                                sar_pb_def_det _ref = new sar_pb_def_det();
                                //  _ref.Spdd_cat1 = _dr[0].ToString();
                                _ref.Spdd_cat2 = _dr[3].ToString();
                                _ref.Spdd_Des = _dr[3].ToString();
                                _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                                _ref.Spdd_ch_code = _dr[6].ToString();
                                ItemBrandCat_List.Add(_ref);
                            }

                        }

                        else if (cmbSelectCat.SelectedValue.ToString() == "8")//    category 3
                        {
                            if (!string.IsNullOrEmpty(_dr[4].ToString()))
                            {


                                //   MasterItemSubCate subCate = CHNLSVC.Sales.GetItemran(_dr[4].ToString());

                                //  if (subCate.Ric2_cd1 == null)
                                //  {
                                //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid sub category - " + _dr[4].ToString());
                                //    else _errorLst.Append(" and invalid sub category  - " + _dr[4].ToString());
                                //    continue;
                                //}
                                //DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                                //if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                                //{
                                //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                //    else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                //    continue;
                                //}

                                if (ItemBrandCat_List != null)
                                {
                                    var _duplicate = from _dup in ItemBrandCat_List
                                                     where _dup.Spdd_Des == _dr[4].ToString()
                                                     select _dup;


                                    if (_duplicate.Count() > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("sub category " + _dr[4].ToString() + " duplicate");
                                        else _errorLst.Append(" and sub category " + _dr[4].ToString() + " duplicate");
                                        continue;
                                    }
                                }
                                sar_pb_def_det _ref = new sar_pb_def_det();
                                //  _ref.Spdd_cat1 = _dr[0].ToString();
                                _ref.Spdd_cat3 = _dr[4].ToString();
                                _ref.Spdd_Des = _dr[4].ToString();
                                _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                                _ref.Spdd_ch_code = _dr[6].ToString();
                                ItemBrandCat_List.Add(_ref);
                            }

                        }


                        else if (cmbSelectCat.SelectedValue.ToString() == "6")// Brand/ cat1
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            if (string.IsNullOrEmpty(_dr[2].ToString())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }


                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_dr[2].ToString().Trim());

                            if (_categoryDet == null || _categoryDet.Rows.Count < 0)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid main category - " + _dr[2].ToString());
                                else _errorLst.Append(" and invalid main category  - " + _dr[2].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[2].ToString() && _dup.Spdd_brand == _dr[1].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand/main category " + _dr[2].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand/main category " + _dr[2].ToString() + " duplicate");
                                    continue;
                                }
                            }





                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_cat1 = _dr[2].ToString();
                            _ref.Spdd_Des = _dr[2].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);


                        }


                        else if (cmbSelectCat.SelectedValue.ToString() == "5")// Brand/ cat2
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            if (string.IsNullOrEmpty(_dr[3].ToString())) continue;
                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }



                            MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                            if (subCate.Ric2_cd1 == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid   category - " + _dr[3].ToString());
                                else _errorLst.Append(" and invalid   category  - " + _dr[3].ToString());
                                continue;
                            }
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[3].ToString() && _dup.Spdd_brand == _dr[1].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand/  category " + _dr[3].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand/  category " + _dr[3].ToString() + " duplicate");
                                    continue;
                                }
                            }





                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_cat2 = _dr[3].ToString();
                            _ref.Spdd_Des = _dr[3].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);


                        }


                        else if (cmbSelectCat.SelectedValue.ToString() == "4")// Brand/ cat3
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString())) continue;
                            if (string.IsNullOrEmpty(_dr[4].ToString())) continue;

                            MasterItemBrand _brd = CHNLSVC.Sales.GetItemBrand(_dr[1].ToString());

                            if (_brd.Mb_cd == null)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid brand - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid brand - " + _dr[1].ToString());
                                continue;
                            }



                            //MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(_dr[3].ToString());

                            //if (subCate.Ric2_cd1 == null)
                            //{
                            //    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid   category - " + _dr[3].ToString());
                            //    else _errorLst.Append(" and invalid   category  - " + _dr[3].ToString());
                            //    continue;
                            //}
                            if (ItemBrandCat_List != null)
                            {
                                var _duplicate = from _dup in ItemBrandCat_List
                                                 where _dup.Spdd_Des == _dr[4].ToString() && _dup.Spdd_brand == _dr[1].ToString()
                                                 select _dup;


                                if (_duplicate.Count() > 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("brand/sub category " + _dr[4].ToString() + " duplicate");
                                    else _errorLst.Append(" and brand/sub   category " + _dr[4].ToString() + " duplicate");
                                    continue;
                                }
                            }



                            sar_pb_def_det _ref = new sar_pb_def_det();
                            _ref.Spdd_brand = _dr[1].ToString();
                            _ref.Spdd_cat3 = _dr[4].ToString();
                            _ref.Spdd_Des = _dr[4].ToString();
                            _ref.Spdd_margin = Convert.ToDecimal(_dr[5].ToString());
                            _ref.Spdd_ch_code = _dr[6].ToString();
                            ItemBrandCat_List.Add(_ref);


                        }
                    }

                }

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        ItemBrandCat_List = new List<sar_pb_def_det>();
                        grvSalesTypes.AutoGenerateColumns = false;
                        grvSalesTypes.DataSource = ItemBrandCat_List;
                        return;
                    }
                }

                grvSalesTypes.AutoGenerateColumns = false;
                // grvSalesType.DataSource = new List<CashCommissionDetailRef>();
                grvSalesTypes.DataSource = ItemBrandCat_List;


            }


            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show("Unable to upload. please select the correct file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }

        private void btnSrhPbd_Click(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNpb;
                _CommonSearch.ShowDialog();
                txtNpb.Select();
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

        private void btnSrhPbdBase_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBasepb;
                _CommonSearch.ShowDialog();
                txtBasepb.Select();
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

        private void btnSrhPbld_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNpb.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNpb.Clear();
                    txtNpb.Focus();
                    return;
                }
                _Stype = "PBDEF1";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNpl;
                _CommonSearch.ShowDialog();
                txtNpl.Select();

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

        private void btnSrhPbldBase_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBasepb.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBasepb.Clear();
                    txtBasepb.Focus();
                    return;
                }
                _Stype = "PBDEF2";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBasepbl;
                _CommonSearch.ShowDialog();
                txtBasepbl.Select();

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

        private void txtBaseCircular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSrhCircular_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtBaseCircular_Leave(null, null);
            }

        }

        private void txtNpb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNpl.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnSrhPbd_Click(null, null);
        }

        private void txtNpl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBasepb.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnSrhPbld_Click(null, null);

        }

        private void txtBasepb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBasepbl.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnSrhPbdBase_Click(null, null);
        }

        private void txtBasepbl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpBaseFrom.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnSrhPbldBase_Click(null, null);
        }

        private void cmbSelectCat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtbbrd.Focus();
            }


        }

        private void txtbbrd_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate2.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnsrhpbcat1_Click(null, null);

        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCate3.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnsrhpbcat2_Click(null, null);
        }

        private void txtCate3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBaseItem.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnsrhpbcat3_Click(null, null);

        }

        private void txtBaseItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMarkup.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnItem_Click(null, null);
        }

        private void btnSavePricedef_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtBaseCircular.Text))
                {
                    MessageBox.Show("Please enter circular", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBaseCircular.Clear();
                    txtBaseCircular.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNpb.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNpb.Clear();
                    txtNpb.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtNpl.Text))
                {
                    MessageBox.Show("Please select the price level", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNpl.Clear();
                    txtNpl.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(txtBasepb.Text))
                {
                    MessageBox.Show("Please select the base price book", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBasepb.Clear();
                    txtBasepb.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtBasepbl.Text))
                {
                    MessageBox.Show("Please select the base price level", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBasepbl.Clear();
                    txtBasepbl.Focus();
                    return;
                }


                if (string.IsNullOrEmpty(cmbActivebase.Text))
                {
                    MessageBox.Show("Please select the status", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbActivebase.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbCate.Text))
                {
                    MessageBox.Show("Please select the category", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cmbCate.Focus();
                    return;
                }

                if (dtpBaseFrom.Value.Date > dtpBaseTo.Value.Date)
                {
                    MessageBox.Show("From date can't be higher than the To date", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpBaseFrom.Focus();
                    return;
                }
                List<sar_pb_def> _hdr = null;
                 _hdr = CHNLSVC.Sales.GetPriceDefHeader(txtBaseCircular.Text);
                 if (_hdr != null)
                 {
                     if (_hdr.Count > 0)
                     {
                         MessageBox.Show("circular alredy exist ", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                     }
                 }
               

                if (_isrecallDef == false)
                {
                    if (dtpBaseFrom.Value.Date < DateTime.Today.Date)
                    {
                        MessageBox.Show("From date can't be lesser than the current date", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpBaseFrom.Focus();
                        return;
                    }

                    if (ItemBrandCat_List.Count == 0)
                    {
                        MessageBox.Show("Please select details ", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        return;
                    }
                }


                sar_pb_def _pbdef = new sar_pb_def();
                _pbdef.Spd_seq = 0;
                _pbdef.Spd_com = BaseCls.GlbUserComCode;
                _pbdef.Spd_circular = txtBaseCircular.Text;
                _pbdef.Spd_base_pb = txtBasepb.Text;
                _pbdef.Spd_base_pblvl = txtBasepbl.Text;
                _pbdef.Spd_pb = txtNpb.Text;
                _pbdef.Spd_pblvl = txtNpl.Text;
                _pbdef.Spd_type = cmbSelectCat.SelectedValue.ToString();
                _pbdef.Spd_from = dtpBaseFrom.Value.Date;
                _pbdef.Spd_to = dtpBaseTo.Value.Date;
                _pbdef.Spd_cate = cmbCate.SelectedValue.ToString();
                if (cmbActivebase.Text == "YES") { _pbdef.Spd_act = 1; } else { _pbdef.Spd_act = 0; }
                _pbdef.Spd_cre_by = BaseCls.GlbUserID;

                _pbdef.Spd_mod_by = BaseCls.GlbUserID;

                //_pbdef.SPD_PRICE_TYPE= Convert.ToInt32(cmbPriceType.SelectedValue);

                if (string.IsNullOrEmpty(cmbptype.Text))
                {
                    _pbdef.SPD_PRICE_TYPE = 99;
                }
                else
                {
                    _pbdef.SPD_PRICE_TYPE = Convert.ToInt32(cmbptype.SelectedValue);
                }

                string err;
                row_aff = (Int32)CHNLSVC.Sales.SavePriceBookDefinition(_pbdef, ItemBrandCat_List, out err);

                if (row_aff > 0)
                {

                    MessageBox.Show("Price Book definition successfully added.", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _isrecallDef = false;
                    Clear_Base();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        MessageBox.Show(_msg, "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faild to update.", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSavePriceClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Clear_Base();
            }
        }

        private void txtCate2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBasepbl_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBasepb_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBasepb_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBasepb.Text)) return;
            DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtBasepb.Text.Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                MessageBox.Show("Please enter valid price book", "Price Definifion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBasepb.Clear();
                txtBasepb.Focus();
            }


            if (!string.IsNullOrEmpty(txtBasepb.Text) && !string.IsNullOrEmpty(txtNpb.Text))
            {
                if (txtBasepb.Text == txtNpb.Text)
                {
                    //MessageBox.Show("Base Price book can't be same as price book ", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtBasepb.Clear();
                    //txtBasepb.Focus();
                    //return;
                }
            }
        }

        private void dtpBaseFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpBaseTo.Focus();
            }
        }

        private void dtpBaseTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbActivebase.Focus();
            }
        }

        private void cmbActivebase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCate.Focus();
            }

        }

        private void cmbCate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbSelectCat.Focus();
            }
        }

        private void btnSrhCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularDef);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceDefCircularSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBaseCircular;
                _CommonSearch.ShowDialog();
                txtBaseCircular.Select();
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

        private void txtBaseCircular_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBaseCircular_Leave(object sender, EventArgs e)
        {

            List<sar_pb_def> _hdr = null;
            List<sar_pb_def_det> _item = new List<sar_pb_def_det>();
            try
            {
                if (txtBaseCircular.Text != "")
                {
                    _hdr = CHNLSVC.Sales.GetPriceDefHeader(txtBaseCircular.Text);
                    if (_hdr != null || _hdr.Count > 0)
                    {
                        _isrecallDef = true;
                        //_hdr[0].Spd_seq = 0;

                        txtBasepb.Text = _hdr[0].Spd_base_pb;
                        txtBasepbl.Text = _hdr[0].Spd_base_pblvl;
                        txtNpb.Text = _hdr[0].Spd_pb;
                        txtNpl.Text = _hdr[0].Spd_pblvl;
                        cmbSelectCat.SelectedValue = Convert.ToInt32(_hdr[0].Spd_type);
                        dtpBaseFrom.Value = _hdr[0].Spd_from;
                        dtpBaseTo.Value = _hdr[0].Spd_to;
                        cmbCate.SelectedValue = _hdr[0].Spd_cate;
                        cmbptype.SelectedValue = _hdr[0].SPD_PRICE_TYPE;
                        if (_hdr[0].Spd_act == 1) { cmbActivebase.Text = "YES"; } else { cmbActivebase.Text = "NO"; }

                        _item = CHNLSVC.Sales.GetPriceDefDet(_hdr[0].Spd_seq);


                    }

                    List<sar_pb_def_det> _itemList = new List<sar_pb_def_det>();

                    foreach (sar_pb_def_det _itm in _item)
                    {

                        string brand = txtBrand.Text;
                        sar_pb_def_det obj = new sar_pb_def_det(); //for display purpose


                        if (_hdr[0].Spd_type == "4" || _hdr[0].Spd_type == "5" || _hdr[0].Spd_type == "6")

                        //    if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE3")
                        {
                            obj.Spdd_brand = _itm.Spdd_brand;
                            obj.Spdd_cat1 = _itm.Spdd_cat1;
                            obj.Spdd_cat2 = _itm.Spdd_cat2;
                            obj.Spdd_cat3 = _itm.Spdd_cat3;
                            //    obj.Spdd_Des = _itm.Spdd_brand;
                            if (_hdr[0].Spd_type == "4")
                            {
                                obj.Spdd_Des = _itm.Spdd_cat3;
                                grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";

                            }
                            if (_hdr[0].Spd_type == "5")
                            {
                                obj.Spdd_Des = _itm.Spdd_cat2;
                                grvSalesTypes.Columns[1].HeaderText = "Cat.";

                            }
                            if (_hdr[0].Spd_type == "6")
                            {
                                obj.Spdd_Des = _itm.Spdd_cat1;
                                grvSalesTypes.Columns[1].HeaderText = "Main Cat.";

                            }
                        }

                        else if (_hdr[0].Spd_type == "3")
                        {
                            obj.Spdd_item = _itm.Spdd_item;
                            obj.Spdd_Des = _itm.Spdd_item;
                            grvSalesTypes.Columns[1].HeaderText = "Item";
                        }
                        else if (_hdr[0].Spd_type == "7")
                        {
                            obj.Spdd_brand = _itm.Spdd_brand;
                            //  obj.Spdd_Des = _itm.Spdd_brand;
                        }
                        else if (_hdr[0].Spd_type == "10")
                        {
                            obj.Spdd_cat1 = _itm.Spdd_cat1;
                            obj.Spdd_Des = _itm.Spdd_cat1;
                            grvSalesTypes.Columns[1].HeaderText = "Main Cat.";
                        }
                        else if (_hdr[0].Spd_type == "9")
                        {
                            obj.Spdd_cat2 = _itm.Spdd_cat2;
                            obj.Spdd_Des = _itm.Spdd_cat2;
                            grvSalesTypes.Columns[1].HeaderText = "Sub Cat.";
                        }
                        else if (_hdr[0].Spd_type == "8")
                        {
                            obj.Spdd_cat3 = _itm.Spdd_cat3;
                            obj.Spdd_Des = _itm.Spdd_cat3;
                            grvSalesTypes.Columns[1].HeaderText = "Cat.";
                        }
                        else
                        {
                            obj.Spdd_brand = string.Empty;
                        }

                        //   obj.Spdd_Des = code;
                        obj.Spdd_margin = _itm.Spdd_margin;
                        obj.Spdd_ch_code = _itm.Spdd_ch_code;


                        try
                        {
                            // obj.Spdd_Des = "";
                        }
                        catch (Exception)
                        {
                            //obj.Spdd_Des = "";
                        }
                        _itemList.Add(obj);
                    }

                    grvSalesTypes.AutoGenerateColumns = false;
                    BindingSource source3 = new BindingSource();
                    source3.DataSource = _itemList;
                    grvSalesTypes.DataSource = source3;
                    ItemBrandCat_List = _itemList;
                }
            }
            catch
            {
            }
        }

        private void txtMarkup_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            if (e.KeyChar == (char)13) e.Handled = false; //Allow Enter 
        }

        private void txtMarkup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCat.Focus();
            }

        }

        private void btnItemCharge_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCharge;
                _CommonSearch.ShowDialog();
                txtCharge.Focus();
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

        private void txtCharge_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCharge.Text))
            {
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtCharge.Text.ToUpper());
                if (_item == null)
                {
                    MessageBox.Show("Please enter a valid item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCharge.Focus();
                }
                else
                {
                    if (_item.Mi_itm_tp != "V")
                    {
                        MessageBox.Show("Please enter a valid virtual item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCharge.Text = "";
                        txtCharge.Focus();
                    }
                }
            }
        }

        private void txtBasepbl_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBasepbl.Text)) return;
                if (string.IsNullOrEmpty(txtBasepb.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBasepbl.Clear(); txtBasepb.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtBasepb.Text.Trim(), txtBasepbl.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBasepbl.Clear(); txtBasepbl.Focus();
                    return;
                }
                else
                {
                    if (_pbIssrl != _tbl.Sapl_is_serialized)
                    {
                        MessageBox.Show("Base Price level type and price level type should be equal.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBasepbl.Clear(); txtBasepbl.Focus();
                        return;
                    }
                }


                if (!string.IsNullOrEmpty(txtBasepb.Text) && !string.IsNullOrEmpty(txtNpb.Text) && !string.IsNullOrEmpty(txtBasepbl.Text) && !string.IsNullOrEmpty(txtNpl.Text))
                {
                    if (txtBasepb.Text == txtNpb.Text && txtBasepbl.Text == txtNpl.Text)
                    {
                        MessageBox.Show("Base Price book/ Price level can't be same as price book/price level ", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBasepb.Clear();
                        txtBasepb.Focus();
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
        Boolean _pbIssrl = false;
        private void txtNpl_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNpl.Text)) return;
                if (string.IsNullOrEmpty(txtNpb.Text)) { MessageBox.Show("Please select the price book.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNpl.Clear(); txtNpb.Focus(); return; }
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtNpb.Text.Trim(), txtNpl.Text.Trim());
                if (string.IsNullOrEmpty(_tbl.Sapl_com_cd))
                {
                    MessageBox.Show("Please enter valid price level.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information); txtNpl.Clear(); txtNpl.Focus();
                    return;
                }
                else
                {
                    _pbIssrl = _tbl.Sapl_is_serialized;
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
        private List<GiftVoucherPages> _lstgvPages = new List<GiftVoucherPages>();
        private void btnGvUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string _msg = string.Empty;
                string _custCode = "";
                MasterItem _Mstitm = new MasterItem();
                List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();

                if (string.IsNullOrEmpty(txtGVUpload.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGVUpload.Text = "";
                    txtGVUpload.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtGVUpload.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGvBrowse.Focus();
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


                conStr = String.Format(conStr, txtGVUpload.Text, "NO");
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
                //DataTable tem = new DataTable();
                //tem.Columns.Add("PROFIT_CENTER");
                //tem.Columns.Add("PC_DESCRIPTION");
                if (dt.Rows.Count > 0)
                {
                    #region validate excel
                    DataRow row = dt.Rows[0];
                    foreach (DataRow _dr in dt.Rows)
                    {
                        if (row != _dr)
                        {
                            if (string.IsNullOrEmpty(_dr[1].ToString()))
                            {
                                MessageBox.Show("Process halted. Invalid book number found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (string.IsNullOrEmpty(_dr[5].ToString()))
                            {
                                MessageBox.Show("Process halted. Invalid value found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (string.IsNullOrEmpty(_dr[0].ToString()))
                            {
                                MessageBox.Show("Process halted. Empty GV code found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            _Mstitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _dr[0].ToString());
                            if (_Mstitm == null)
                            {
                                MessageBox.Show("Process halted. Invalid GV code found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            Int32 _bkno = 0;
                            _bkno = Convert.ToInt32(_dr[1].ToString());

                            DataTable _dtlgvbokk = CHNLSVC.Inventory.GetAvailable_GV_books(_bkno, _dr[0].ToString(), BaseCls.GlbUserComCode);


                            if (_dtlgvbokk.Rows.Count > 0)
                            {
                                MessageBox.Show("Process halted. Vouchar book already exist !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }


                            _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, _dr[3].ToString(), string.Empty, string.Empty, "C");
                            if (_custList == null && _custList.Count == 0)
                            {
                                MessageBox.Show("Process halted. Registered customer not found !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;

                            }

                        }

                    }
                    #endregion
                    Int32 _gvline = 0;
                    _lstgvPages = new List<GiftVoucherPages>();
                    DataRow row1 = dt.Rows[0];
                    foreach (DataRow _dr in dt.Rows)
                    {
                        if (row1 != _dr)
                        {
                            _custCode = "";
                            Int32 _count = _lstgvPages.Where(X => X.Gvp_gv_cd == _dr[0].ToString() && X.Gvp_book == Convert.ToInt32(_dr[1]) && X.Gvp_page == Convert.ToInt32(_dr[4])).Count();
                            if (_count > 0)
                                break;

                            _gvline = _gvline + 1;
                            GiftVoucherPages _gvou = new GiftVoucherPages();
                            _gvou.Gvp_gv_tp = _dr[2].ToString();// "VALUE";
                            _gvou.Gvp_amt = Convert.ToDecimal(_dr[5]);
                            _gvou.Gvp_app_by = BaseCls.GlbUserID;
                            _gvou.Gvp_bal_amt = Convert.ToDecimal(_dr[5]);
                            _gvou.Gvp_book = Convert.ToInt32(_dr[1]);
                            _gvou.Gvp_line = _gvline;
                            _gvou.Gvp_can_by = "";
                            _gvou.Gvp_can_dt = DateTime.Now.Date;
                            _gvou.Gvp_com = BaseCls.GlbUserComCode;
                            _gvou.Gvp_cre_by = BaseCls.GlbUserID;
                            _gvou.Gvp_cre_dt = DateTime.Now.Date;
                            _gvou.Gvp_cus_add1 = _dr[9].ToString();
                            _gvou.Gvp_cus_add2 = _dr[10].ToString();
                            _gvou.Gvp_cus_cd = _dr[3].ToString();
                            _gvou.Gvp_cus_mob = _dr[6].ToString();
                            _gvou.Gvp_cus_name = _dr[8].ToString();
                            _gvou.Gvp_gv_cd = _dr[0].ToString();
                            _gvou.Gvp_gv_prefix = "P_GV";
                            _gvou.Gvp_is_allow_promo = false;
                            _gvou.Gvp_issu_itm = 0;
                            _gvou.Gvp_issue_by = "";
                            _gvou.Gvp_issue_dt = DateTime.Now.Date;
                            //_gvou.Gvp_line = i;
                            _gvou.Gvp_mod_by = "";
                            _gvou.Gvp_mod_dt = DateTime.Now.Date;
                            _gvou.Gvp_noof_itm = 1;
                            _gvou.Gvp_oth_ref = "";
                            _gvou.Gvp_page = Convert.ToInt32(_dr[4]);
                            _gvou.Gvp_pc = "HO";
                            _gvou.Gvp_ref = "";
                            _gvou.Gvp_stus = "A";
                            _gvou.Gvp_valid_from = Convert.ToDateTime(_dr[11].ToString());
                            _gvou.Gvp_valid_to = Convert.ToDateTime(_dr[12].ToString());
                            _gvou.Gvp_cus_nic = _dr[7].ToString();

                            _lstgvPages.Add(_gvou);
                        }
                    }
                }

                //gvPages.AutoGenerateColumns = false;
                //BindingSource _source = new BindingSource();
                //_source.DataSource = _lstgvPages;
                //gvPages.DataSource = _source;
                if (_lstgvPages.Count == 0)
                {
                    MessageBox.Show("Gift voucher details not found !", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to upload the Gift voucher? ", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No) return;

                Int32 _eff = CHNLSVC.Sales.SaveGiftVoucherPages(_lstgvPages);
                MessageBox.Show("Successfully Uploaded  !", "Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnGvBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtGVUpload.Text = openFileDialog1.FileName;
        }

        private void btnCloseGV_Click(object sender, EventArgs e)
        {
            pnlGV.Visible = false;
        }

        private void cmbCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCate.SelectedItem != null)
            {
                ItemBrandCat_List = new List<sar_pb_def_det>();
                grvSalesTypes.DataSource = null;

                if (cmbCate.SelectedValue.ToString() == "C")
                {
                    txtCharge.Enabled = true;
                    btnItemCharge.Enabled = true;
                }
                else
                {
                    txtCharge.Enabled = false;
                    btnItemCharge.Enabled = false;
                }
            }
        }

        private void btnGv_Click(object sender, EventArgs e)
        {
            pnlGV.Visible = true;
        }

        private void btnClearUpload_Click(object sender, EventArgs e)
        {
            txtGVUpload.Text = "";
        }

        private void txtCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItemCharge_Click(null, null);
        }

        private void grvSalesTypes_DoubleClick(object sender, EventArgs e)
        {

        }

        private void lstSpc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grvSalesTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //remove item line
                    ItemBrandCat_List.RemoveAt(e.RowIndex);
                    BindingSource source = new BindingSource();
                    source.DataSource = ItemBrandCat_List;
                    grvSalesTypes.AutoGenerateColumns = false;
                    grvSalesTypes.DataSource = source;
                }
            }
        }

        private void txtNpl_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemBrandCat_List = new List<sar_pb_def_det>();
            grvSalesTypes.DataSource = null;



            txtCate3.Enabled = false;
            btnsrhpbcat3.Enabled = false;
            txtCate2.Enabled = false;
            btnsrhpbcat2.Enabled = false;
            txtCate1.Enabled = false;
            btnsrhpbcat1.Enabled = false;
            txtBaseItem.Enabled = false; ;
            btnItem.Enabled = false;
            txtbbrd.Enabled = false;
            btnsrhpbbrand.Enabled = false;

            if (cmbSelectCat.SelectedValue.ToString() == "10")
            {
                txtCate1.Enabled = true;
                btnsrhpbcat1.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "9")
            {
                txtCate2.Enabled = true;
                btnsrhpbcat2.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "8")
            {
                txtCate3.Enabled = true;
                btnsrhpbcat3.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "7")
            {
                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "6")
            {
                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                txtCate1.Enabled = true;
                btnsrhpbcat1.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "5")
            {
                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                txtCate2.Enabled = true;
                btnsrhpbcat2.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "4")
            {

                txtbbrd.Enabled = true;
                btnsrhpbbrand.Enabled = true;
                txtCate3.Enabled = true;
                btnsrhpbcat3.Enabled = true;
            }
            else if (cmbSelectCat.SelectedValue.ToString() == "3")
            {
                txtBaseItem.Enabled = true; ;
                btnItem.Enabled = true;
            }
        }

        private void txtCharge_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNpb_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNpb.Text)) return;
            DataTable _tbl = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtNpb.Text.Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                MessageBox.Show("Please enter valid price book", "Price Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNpb.Clear();
                txtNpb.Focus();
            }
        }

        private void txtNpb_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSpromo_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvVouCat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {


                if (e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string type1 = gvVouCat.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string type2 = gvVouCat.Rows[e.RowIndex].Cells[2].Value.ToString();

                        _lstcate2.RemoveAll(x => x.Ric2_cd == type2 && x.Ric2_cd1 == type1);
                        BindingSource source = new BindingSource();
                        source.DataSource = _lstcate2;
                        gvVouCat.DataSource = source;


                    }
                }
            }
        }

        private void tbProVouItem_Click(object sender, EventArgs e)
        {

        }

        private void gvVouCat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lstPDItems_DoubleClick(object sender, EventArgs e)
        {
            lstPDItems.SelectedItems.Clear();
        }

        private void txtCat2_pv_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkSMS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSMS.Checked == true)
            {
                txtPurSMS.Enabled = true;
                txtRedeemSMS.Enabled = true;
            }
            else
            {
                txtPurSMS.Enabled = false;
                txtRedeemSMS.Enabled = false;
            }
        }

        private void txtPurSMS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRedeemSMS.Focus();
            }
        }

        private void txtCate3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMarkup_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMinVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtProVouType_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void btn_srch_sale_tp_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSaleTp;
                _CommonSearch.ShowDialog();
                txtSaleTp.Select();

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

        private void btnAddSaleTp_Click(object sender, EventArgs e)
        {
            if (optPayTp.Checked == false && optSaleTp.Checked == false && optPriceTp.Checked == false)
            {
                MessageBox.Show("Please select the option", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PromotionVoucherPara _vouPara = new PromotionVoucherPara();
            _vouPara.Spdp_vou_cd = txtProVouType.Text;

            if (optSaleTp.Checked == true)
            {
                _vouPara.Spdp_tp = optSaleTp.Tag.ToString();
                _vouPara.Spdp_sale_tp = txtSaleTp.Text;
                _vouPara.Spdp_sale_tp = txtSaleTp.Text;
            }
            if (optPayTp.Checked == true)
            {
                _vouPara.Spdp_tp = optPayTp.Tag.ToString();
                _vouPara.Spdp_pay_tp = comboBoxPayModes.Text;
                _vouPara.Spdp_pay_prd = Convert.ToInt32(txtPrd.Text);
            }
            if (optPriceTp.Checked == true)
            {
                _vouPara.Spdp_tp = optPriceTp.Tag.ToString();
                _vouPara.Spdp_price_tp = cmbPriceTp.Text;
            }
            _promoVouPara.Add(_vouPara);

            grvVouPara.AutoGenerateColumns = false;
            grvVouPara.DataSource = new List<PromotionVoucherPara>();
            grvVouPara.DataSource = _promoVouPara;



        }

        private void bindPayTypes()
        {
            List<string> payTypes = new List<string>();
            //List<PaymentType> _paymentTypeRef = null;
            DataTable _paymentTypeRef = CHNLSVC.Sales.GetPossiblePayTypes(BaseCls.GlbUserComCode, "ALL", BaseCls.GlbUserDefProf, null, DateTime.Now.Date);
            for (int i = 0; i < _paymentTypeRef.Rows.Count; i++)
            {
                payTypes.Add(_paymentTypeRef.Rows[i]["Stp_pay_tp"].ToString());
            }
            comboBoxPayModes.DataSource = payTypes;
        }

        private void btnCloseVouPara_Click(object sender, EventArgs e)
        {
            pnlSaleTp.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "PriceDet.rpt";
            BaseCls.GlbReportDoc = txtCircular.Text.Trim();
            _view.Show();
            _view = null;
        }

        private void btnPB_Click(object sender, EventArgs e)
        {
            pnlPB.Visible = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSavePB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPB.Text))
            {
                MessageBox.Show("Please enter price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                txtPB.Focus();
            }
            if (string.IsNullOrEmpty(txtPBDesc.Text))
            {
                MessageBox.Show("Please enter price book description", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                txtPBDesc.Focus();
            }
            PriceBookRef book = new PriceBookRef();
            Int32 _effect = 0;

            Int32 isactive = 1;

            if (MessageBox.Show("Do you want to save ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            if (chkAct.Checked == true)
            {
                isactive = 1;
            }
            else
            {
                isactive = 0;
            }

            book.Sapb_com = BaseCls.GlbUserComCode;
            book.Sapb_pb = txtPB.Text.Trim().ToUpper();
            book.Sapb_desc = txtPBDesc.Text.Trim();
            book.Sapb_hierachy_lvl = 0;
            book.Sapb_act = Convert.ToBoolean(isactive);
            book.Sapb_cre_by = BaseCls.GlbUserID;
            book.Sapb_mod_by = BaseCls.GlbUserID;

            _effect = CHNLSVC.Sales.UpdatePriceBook(book);

            if (_effect != -1)
            {
                MessageBox.Show("Price book created successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPB.Text = "";
                txtPBDesc.Text = "";
                chkAct.Checked = false;
                BindPB();
            }
            else
            {
                MessageBox.Show("Error Occurred while processing !!!", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void btnClosePB_Click(object sender, EventArgs e)
        {
            pnlPB.Visible = false;
        }

        private void btnSavePBLevl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLvlPB.Text))
            {
                MessageBox.Show("Please enter price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLvlPB.Focus();
                return;

            }
            if (string.IsNullOrEmpty(txtPBLvl.Text))
            {
                MessageBox.Show("Please enter price level", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPBLvl.Focus();
                return;

            }
            if (string.IsNullOrEmpty(txtLvlDesc.Text))
            {
                MessageBox.Show("Please enter price level description", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLvlDesc.Focus();
                return;

            }

            PriceBookLevelRef booklevel = new PriceBookLevelRef();
            Int32 _effect = 0;

            if (MessageBox.Show("Do you want to save ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            if (string.IsNullOrEmpty(txtPeriod.Text))
            {
                txtPeriod.Text = "0";
            }

            booklevel.Sapl_pb_lvl_cd = txtPBLvl.Text.Trim();
            booklevel.Sapl_erp_ref = txtPBLvl.Text.Trim();
            booklevel.Sapl_pb_lvl_desc = txtLvlDesc.Text.Trim();
            booklevel.Sapl_act = true;
            booklevel.Sapl_cre_by = BaseCls.GlbUserID;
            booklevel.Sapl_cre_when = CHNLSVC.Security.GetServerDateTime();
            booklevel.Sapl_mod_by = BaseCls.GlbUserID;
            booklevel.Sapl_mod_when = CHNLSVC.Security.GetServerDateTime();
            booklevel.Sapl_com_cd = BaseCls.GlbUserComCode;
            //kapila 12/7/2017
            PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
            _lvlDef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtLvlPB.Text.Trim(), txtPBLvl.Text.Trim());
            if (_lvlDef == null)
                booklevel.Sapl_is_def = true;
            else
                booklevel.Sapl_is_def = false;

            booklevel.Sapl_pb = txtLvlPB.Text.Trim();
            booklevel.Sapl_itm_stuts = txtItmStus.Text;
            booklevel.Sapl_warr_period = Convert.ToInt32(txtPeriod.Text.Trim());
            booklevel.Sapl_is_serialized = chkSer.Checked ? true : false;
            booklevel.Sapl_currency_cd = txtCur.Text.Trim();
            booklevel.Sapl_is_print = false;

            booklevel.Sapl_set_warr = chkWarBase.Checked ? true : false;
            booklevel.Sapl_vat_calc = chkWithVat.Checked ? true : false;
            booklevel.Sapl_chk_st_tp = chkStus.Checked ? true : false;
            booklevel.Sapl_is_transfer = chkTrans.Checked ? true : false;
            booklevel.Sapl_is_sales = chkForSale.Checked ? true : false;
            booklevel.Sapl_isage = chkAging.Checked ? true : false;
            booklevel.Sapl_isbatch_wise = chkBatchwise.Checked ? true : false;
            booklevel.Sapl_act = chkActLvl.Checked ? true : false;
            booklevel.Sapl_model_base = chkModelBase.Checked ? true : false;
            booklevel.Sapl_base_on_tot_inv_qty = chkTotSaleBase.Checked ? true : false;

            booklevel.Sapl_credit_period = 0;
            booklevel.Sapl_is_valid = true;
            booklevel.Sapl_grn_com = BaseCls.GlbUserComCode;
            booklevel.Sapl_is_without_p = false;
            booklevel.Sapl_is_for_po = false;


            _effect = CHNLSVC.Sales.UpdatePriceBookLevel(booklevel);

            if (_effect != -1)
            {
                MessageBox.Show("Price level created successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkSer.Checked = false;
                chkWarBase.Checked = false;
                chkWithVat.Checked = false;
                chkStus.Checked = false;
                chkTrans.Checked = false;
                chkForSale.Checked = false;
                txtLvlPB.Text = "";
                txtPBLDesc.Text = "";
                txtLvlDesc.Text = "";
                txtPBLvl.Text = "";
                txtCur.Text = "";
                txtPeriod.Text = "";
                txtItmStus.Text = "";
                chkTotSaleBase.Checked = false;
                chkModelBase.Checked = false;
                chkBatchwise.Checked = false;
                chkAging.Checked = false;
                chkActLvl.Checked = false;

            }
            else
            {
                MessageBox.Show("Error Occurred while processing !!!", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_srch_pb_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPB;
                _CommonSearch.ShowDialog();
                txtPB.Select();

                load_PBDesc();
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

        private void btn_srch_cur_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
            DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCur;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtCur.Select();
        }

        private void btnDisUpd_Click(object sender, EventArgs e)
        {
            string err = "";
            Int32 _effect = 0;
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10134))
                {
                    MessageBox.Show("Sorry, You have no permission to update the details!\n( Advice: Required permission code :10134 )", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtDiscPC.Text))
                {
                    MessageBox.Show("Please select a profit center !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscPC.Text = "";
                    txtDiscPC.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtInvTp.Text))
                {
                    MessageBox.Show("Please select a invoice type !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInvTp.Text = "";
                    txtInvTp.Focus();
                    return;

                }
                else if (string.IsNullOrEmpty(txtDiscPB.Text))
                {
                    MessageBox.Show("Please select a price book !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscPB.Text = "";
                    txtDiscPB.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtDiscPBLvl.Text))
                {
                    MessageBox.Show("Please select a price level!!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscPBLvl.Text = "";
                    txtDiscPBLvl.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtDiscRate.Text) && chkAlowDis.Checked)
                {
                    MessageBox.Show("Please enter a discount rate !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDiscRate.Text = "0.00";
                    txtDiscRate.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtEditRate.Text) && chkEditPrice.Checked)
                {
                    MessageBox.Show("Please enter a edit rate !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEditRate.Text = "0.00";
                    txtEditRate.Focus();
                    return;
                }
                PriceDefinitionRef _priceRef = new PriceDefinitionRef();
                _priceRef.Sadd_pc = txtDiscPC.Text.ToUpper();
                _priceRef.Sadd_doc_tp = txtInvTp.Text.ToUpper();
                _priceRef.Sadd_p_lvl = txtDiscPBLvl.Text.ToUpper();
                //_priceRef.Sadd_is_bank_ex_rt=             
                _priceRef.Sadd_is_disc = chkAlowDis.Checked;
                _priceRef.Sadd_disc_rt = string.IsNullOrEmpty(txtDiscRate.Text) ? 0 : Convert.ToDecimal(txtDiscRate.Text);
                _priceRef.Sadd_com = BaseCls.GlbUserComCode;
                //_priceRef.Sadd_chk_credit_bal            
                _priceRef.Sadd_cre_by = BaseCls.GlbUserID;
                _priceRef.Sadd_cre_when = DateTime.Now;
                _priceRef.Sadd_mod_by = BaseCls.GlbUserID;
                _priceRef.Sadd_mod_when = DateTime.Now;
                _priceRef.Sadd_pb = txtDiscPB.Text.ToUpper();
                //_priceRef.sadd_prefix                    
                //_priceRef.sadd_def                       
                //_priceRef.sadd_def_stus                  
                //_priceRef.sadd_def_pb                    
                //_priceRef.sadd_is_rep                    
                //_priceRef.sadd_rep_order                 
                _priceRef.Sadd_is_edit = chkEditPrice.Checked ? 1 : 0; ;
                _priceRef.Sadd_edit_rt = string.IsNullOrEmpty(txtEditRate.Text) ? 0 : Convert.ToDecimal(txtEditRate.Text);

                _effect = CHNLSVC.Sales.UpdatePriceDefinitionRef(_priceRef, out err);
                if (_effect == 1)
                {
                    MessageBox.Show("Discount Rate/Edit Rate Updated Successfully !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearDiscount();
                }
                else if (_effect == -1)
                {
                    MessageBox.Show("Error Occurred while processing !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occurred while processing !!!", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ClearDiscount()
        {
            txtDiscPC.Text = "";
            txtInvTp.Text = "";
            txtDiscPB.Text = "";
            txtDiscPBLvl.Text = "";
            chkAlowDis.Checked = false;
            txtDiscRate.Text = "0.00";
            txtDiscRate.Enabled = false;
            chkEditPrice.Checked = false;
            txtEditRate.Text = "0.00";
            txtEditRate.Enabled = false;
            grvDisc.AutoGenerateColumns = false;
            grvDisc.DataSource = null;

        }

        private void chkAlowDis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlowDis.Checked)
            {
                txtDiscRate.Enabled = true;
                txtDiscRate.Focus();
            }
            else
            {
                txtDiscRate.Text = "0.00";
                txtDiscRate.Enabled = false;
            }
        }

        private void chkEditPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditPrice.Checked)
            {
                txtEditRate.Enabled = true;
                txtEditRate.Focus();
            }
            else
            {
                txtEditRate.Text = "0.00";
                txtEditRate.Enabled = false;
            }
        }

        private void btnDisClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearDiscount();
            }
        }

        private void btn_srch_dic_pc_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            //DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtDiscPC;
            //_CommonSearch.ShowDialog();
            ////TextBoxLocation.Select();
            //txtDiscPC.Focus();

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDiscPC;
            _CommonSearch.ShowDialog();
            //TextBoxLocation.Select();
            txtDiscPC.Focus();

            BindDisGrid();
        }

        private void BindDisGrid()
        {
            List<PriceDefinitionRef> _priceDefRef = new List<PriceDefinitionRef>();
            if (!string.IsNullOrEmpty(txtDiscPC.Text))
            {
                _priceDefRef = CHNLSVC.Sales.GetPriceDefinitionRefs(new PriceDefinitionRef()
                {
                    Sadd_com = BaseCls.GlbUserComCode,
                    Sadd_pc = txtDiscPC.Text.ToUpper()
                });
                if (_priceDefRef.Count > 0)
                {
                    grvDisc.AutoGenerateColumns = false;
                    grvDisc.DataSource = _priceDefRef;
                }
            }

        }

        private void txtDiscPC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiscPC.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtDiscPC.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDiscPC.Text = "";
                    txtDiscPC.Focus();
                    return;
                }
                BindDisGrid();
            }
        }

        private void btn_srch_dic_invtp_Click(object sender, EventArgs e)
        {
            PriceDefinitionRef _priceRef = new PriceDefinitionRef()
            {
                Sadd_com = BaseCls.GlbUserComCode,
                Sadd_pc = txtDiscPC.Text
            };
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            DataTable _result = CHNLSVC.CommonSearch.SearchDocPriceDefDocTp(_priceRef, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInvTp;
            _CommonSearch.ShowDialog();
            txtInvTp.Focus();
        }

        private void btn_srch_dic_pb_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvTp.Text))
            {
                MessageBox.Show("Please select invoice type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            PriceDefinitionRef _priceRef = new PriceDefinitionRef()
            {
                Sadd_com = BaseCls.GlbUserComCode,
                Sadd_pc = txtDiscPC.Text,
                Sadd_doc_tp = txtInvTp.Text
            };
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            DataTable _result = CHNLSVC.CommonSearch.SearchDocPriceDefPrBook(_priceRef, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDiscPB;
            _CommonSearch.ShowDialog();
            txtDiscPB.Focus();
        }

        private void btn_srch_dic_lvl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDiscPB.Text))
            {
                MessageBox.Show("Please select price book.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            PriceDefinitionRef _priceRef = new PriceDefinitionRef()
            {
                Sadd_com = BaseCls.GlbUserComCode,
                Sadd_pc = txtDiscPC.Text,
                Sadd_doc_tp = txtInvTp.Text,
                Sadd_pb = txtDiscPB.Text
            };
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            DataTable _result = CHNLSVC.CommonSearch.SearchDocPriceDefPrLVL(_priceRef, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDiscPBLvl;
            _CommonSearch.ShowDialog();
            txtDiscPBLvl.Focus();
        }

        private void txtDiscPC_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_dic_pc_Click(null, null);
        }

        private void txtDiscPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_dic_pc_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtInvTp.Focus();
        }

        private void txtInvTp_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_dic_invtp_Click(null, null);
        }

        private void txtInvTp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_dic_invtp_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtDiscPB.Focus();
        }

        private void txtDiscPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_dic_pb_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtDiscPBLvl.Focus();
        }

        private void txtDiscPB_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_dic_pb_Click(null, null);
        }

        private void txtDiscPBLvl_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_dic_lvl_Click(null, null);
        }

        private void txtDiscPBLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_dic_lvl_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                chkAlowDis.Focus();
        }

        private void txtDiscPB_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiscPB.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtDiscPB.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid price book.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDiscPB.Text = "";
                    return;
                }
            }
        }

        private void txtDiscPBLvl_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiscPBLvl.Text))
            {
                PriceBookLevelRef _pblvl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtDiscPB.Text, txtDiscPBLvl.Text);
                if (_pblvl.Sapl_pb_lvl_cd == null)
                {
                    MessageBox.Show("Invalid price level.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDiscPBLvl.Text = "";
                    return;
                }
            }
        }

        private void txtInvTp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvTp.Text))
            {
                PriceDefinitionRef _priceRef = new PriceDefinitionRef()
                {
                    Sadd_com = BaseCls.GlbUserComCode,
                    Sadd_pc = txtDiscPC.Text,
                    Sadd_doc_tp = txtInvTp.Text
                };
                DataTable _result = CHNLSVC.CommonSearch.SearchDocPriceDefDocTp(_priceRef, null, null);
                if (_result.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid invoice type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvTp.Text = "";
                    return;
                }
            }
        }

        private void grvDisc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            decimal _disrate = 0;
            decimal _editrate = 0;
            txtDiscPC.Text = grvDisc.Rows[e.RowIndex].Cells["Sadd_pc"].Value.ToString();
            txtInvTp.Text = grvDisc.Rows[e.RowIndex].Cells["Sadd_doc_tp"].Value.ToString();
            txtDiscPB.Text = grvDisc.Rows[e.RowIndex].Cells["Sadd_pb"].Value.ToString();
            txtDiscPBLvl.Text = grvDisc.Rows[e.RowIndex].Cells["Sadd_p_lvl"].Value.ToString();
            chkAlowDis.Checked = Convert.ToInt32(grvDisc.Rows[e.RowIndex].Cells["sadd_is_disc"].Value) == 1 ? true : false;
            _disrate = Convert.ToDecimal(grvDisc.Rows[e.RowIndex].Cells["sadd_disc_rt"].Value);
            chkEditPrice.Checked = Convert.ToInt32(grvDisc.Rows[e.RowIndex].Cells["sadd_is_edit"].Value) == 1 ? true : false;
            _editrate = Convert.ToDecimal(grvDisc.Rows[e.RowIndex].Cells["sadd_edit_rt"].Value);
            txtEditRate.Text = _editrate.ToString("0.00");
            txtDiscRate.Text = _disrate.ToString("0.00");
        }

        private void btn_clr_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clearPB();
            }
        }
        private void clearPB()
        {
            txtPB.Text = "";
            txtPBDesc.Text = "";
            chkAct.Checked = false;

        }

        private void btn_srch_pb_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void btn_srch_pb_cre_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLvlPB;
                _CommonSearch.ShowDialog();
                txtLvlPB.Select();
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

        private void btn_srch_lvl_pb_lvl_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLvlPB.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLvlPB.Clear();
                    txtLvlPB.Focus();
                    return;
                }
                if (chkAll.Checked)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllPBLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllPBLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPBLvl;
                    _CommonSearch.ShowDialog();
                    txtPBLvl.Select();
                }
                else
                {
                    _Stype = "PBDEF3";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPBLvl;
                    _CommonSearch.ShowDialog();
                    txtPBLvl.Select();
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

        private void load_price_level()
        {
            PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtLvlPB.Text.Trim(), txtPBLvl.Text.Trim());
            if (_tbl.Sapl_pb_lvl_desc != null)
            {
                txtLvlDesc.Text = _tbl.Sapl_pb_lvl_desc;
                txtItmStus.Text = _tbl.Sapl_itm_stuts;
                txtPeriod.Text = _tbl.Sapl_warr_period.ToString();
                txtCur.Text = _tbl.Sapl_currency_cd;

                chkSer.Checked = Convert.ToInt32(_tbl.Sapl_is_serialized) == 1 ? true : false;
                chkWarBase.Checked = Convert.ToInt32(_tbl.Sapl_set_warr) == 1 ? true : false;
                chkWithVat.Checked = Convert.ToInt32(_tbl.Sapl_vat_calc) == 1 ? true : false;
                chkStus.Checked = Convert.ToInt32(_tbl.Sapl_chk_st_tp) == 1 ? true : false;
                chkTrans.Checked = Convert.ToInt32(_tbl.Sapl_is_transfer) == 1 ? true : false;
                chkForSale.Checked = Convert.ToInt32(_tbl.Sapl_is_sales) == 1 ? true : false;
                chkAging.Checked = Convert.ToInt32(_tbl.Sapl_isage) == 1 ? true : false;
                chkBatchwise.Checked = Convert.ToInt32(_tbl.Sapl_isbatch_wise) == 1 ? true : false;
                chkActLvl.Checked = Convert.ToInt32(_tbl.Sapl_act) == 1 ? true : false;
                chkModelBase.Checked = Convert.ToInt32(_tbl.Sapl_model_base) == 1 ? true : false;
                chkTotSaleBase.Checked = Convert.ToInt32(_tbl.Sapl_base_on_tot_inv_qty) == 1 ? true : false;

            }
            else
            {
                txtLvlDesc.Text = "";
                txtItmStus.Text = "";
                txtPeriod.Text = "";
                txtCur.Text = "";

                chkSer.Checked = false;
                chkWarBase.Checked = false;
                chkWithVat.Checked = false;
                chkStus.Checked = false;
                chkTrans.Checked = false;
                chkForSale.Checked = false;
                chkAge.Checked = false;
                chkBatchwise.Checked = false;
                chkActLvl.Checked = false;
                chkModelBase.Checked = false;
                chkTotSaleBase.Checked = false;
            }


        }

        private void txtPB_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_pb_Click(null, null);
        }

        private void txtPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_pb_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtPBDesc.Focus();
        }

        private void txtLvlPB_DoubleClick(object sender, EventArgs e)
        {

            btn_srch_pb_cre_Click(null, null);
        }

        private void txtLvlPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_pb_cre_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtPBLvl.Focus();
        }

        private void txtPBLvl_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_lvl_pb_lvl_Click(null, null);
        }

        private void txtPBLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_lvl_pb_lvl_Click(null, null);
        }

        private void txtCur_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_cur_Click(null, null);
        }

        private void txtCur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_cur_Click(null, null);
        }

        private void txtPB_Leave(object sender, EventArgs e)
        {
            txtPBDesc.Text = "";
            chkAct.Checked = false;

            if (!string.IsNullOrEmpty(txtPB.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtPB.Text);
                if (_dt.Rows.Count > 0)
                {
                    txtPBDesc.Text = _dt.Rows[0]["sapb_desc"].ToString();
                    chkAct.Checked = Convert.ToInt32(_dt.Rows[0]["sapb_act"]) == 1 ? true : false;
                }

            }
        }

        private void load_PBDesc()
        {

        }

        private void btn_clr_l_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearPBLevel();
            }
        }
        private void ClearPBLevel()
        {
            txtLvlPB.Text = "";
            txtPBLDesc.Text = "";
            txtPBLvl.Text = "";
            txtLvlDesc.Text = "";
            txtItmStus.Text = "";
            txtCur.Text = "";
            txtPeriod.Text = "";

            chkSer.Checked = false;
            chkWarBase.Checked = false;
            chkWithVat.Checked = false;
            chkStus.Checked = false;
            chkTrans.Checked = false;
            chkForSale.Checked = false;
            chkAging.Checked = false;
            chkBatchwise.Checked = false;
            chkActLvl.Checked = false;
            chkModelBase.Checked = false;
            chkTotSaleBase.Checked = false;


        }

        private void load_item_status()
        {

        }

        private void txtDiscRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDiscRate.Text))
                if (!IsNumeric(txtDiscRate.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDiscRate.Text = "";
                    txtDiscRate.Focus();
                    return;
                }
            if (Convert.ToDecimal(txtDiscRate.Text) < 0)
            {
                MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiscRate.Text = "";
                txtDiscRate.Focus();
                return;
            }
        }

        private void txtEditRate_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEditRate.Text))
                if (!IsNumeric(txtEditRate.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEditRate.Text = "";
                    txtEditRate.Focus();
                    return;
                }
            if (Convert.ToDecimal(txtEditRate.Text) < 0)
            {
                MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEditRate.Text = "";
                txtEditRate.Focus();
                return;
            }
        }

        private void btn_Srch_Itm_Stus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItmStus;
            _CommonSearch.ShowDialog();
            txtItmStus.Focus();
        }

        private void txtPeriod_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPeriod.Text))
            {
                if (!IsNumeric(txtPeriod.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPeriod.Text = "";
                    txtPeriod.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtPeriod.Text) < 0)
                {
                    MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPeriod.Text = "";
                    txtPeriod.Focus();
                    return;
                }
            }
        }

        private void txtLvlPB_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLvlPB.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtLvlPB.Text);
                if (_dt.Rows.Count > 0)
                {
                    txtPBLDesc.Text = _dt.Rows[0]["sapb_desc"].ToString();

                }
                else
                {
                    MessageBox.Show("Invalid price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPBLDesc.Text = "";
                    txtLvlPB.Text = "";
                    txtLvlPB.Focus();
                }
            }
        }

        private void txtPBLvl_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPBLvl.Text))
                load_price_level();
        }

        private void btnCloseCan_Click(object sender, EventArgs e)
        {
            clear_price_cancel();
            pnlCanselSer.Visible = false;
        }

        private void btnCancelSer_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10135))
            {
                MessageBox.Show("Sorry, You have no permission to cancel!\n( Advice: Required permission code :10135 )", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            pnlCanselSer.Visible = true;
        }

        private void btnCancel_Ser_Click(object sender, EventArgs e)
        {
            Boolean _isDataFound = false;
            if (optEnd.Checked && dtpEnd.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Please check the price ending date", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (DataGridViewRow row in grvCanSer.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _isDataFound = true;
                }
            }

            if (_isDataFound == false)
            {
                MessageBox.Show("Please select serial for cancel/update", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<PriceSerialRef> _lstPriceSer = new List<PriceSerialRef>();
            if (MessageBox.Show("Do you want to save? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            foreach (DataGridViewRow row in grvCanSer.Rows)
            {
                string _circ = row.Cells["Circular"].Value.ToString();
                string _ser = row.Cells["Serial"].Value.ToString();
                string _itm = row.Cells["ItemCode"].Value.ToString();
                string _pb = row.Cells["PriceBook"].Value.ToString();
                string _lvl = row.Cells["Level"].Value.ToString();

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _isDataFound = true;
                    PriceSerialRef _objPriceSer = new PriceSerialRef();
                    _objPriceSer.Sars_itm_cd = _itm;
                    _objPriceSer.Sars_pbook = _pb;
                    _objPriceSer.Sars_price_lvl = _lvl;
                    _objPriceSer.Sars_circular_no = _circ;
                    _objPriceSer.Sars_ser_no = _ser;
                    _objPriceSer.Sars_mod_by = BaseCls.GlbUserID;
                    if (optEnd.Checked)
                        _objPriceSer.Sars_val_to = dtpEnd.Value.Date;

                    _lstPriceSer.Add(_objPriceSer);
                }
            }


            Int32 _eff = CHNLSVC.Sales.CancelSerialPrice(_lstPriceSer);
            if (_eff == 1)
            {
                if (optEnd.Checked)
                    MessageBox.Show("Successfully changed the price ending date.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Serialized price cancelled successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear_price_cancel();
            }
            else
            {
                MessageBox.Show("Failed to cancel.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void clear_price_cancel()
        {
            grvCanSer.AutoGenerateColumns = false;
            grvCanSer.DataSource = null;
            txtCanCirc.Text = "";
            txtCanPB.Text = "";
            txtCanPBLvl.Text = "";
            txtCanSer.Text = "";
            dtpEnd.Value = DateTime.Now.Date;
        }
        private void btnCanCirc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CancelSerialCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForCancel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanCirc;
                _CommonSearch.ShowDialog();
                txtCanCirc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvCanSer_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvCanSer.CurrentCell is DataGridViewCheckBoxCell)
                grvCanSer.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSrch_CanSer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CancelSerialCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetSerialPriceForCancel(_CommonSearch.SearchParams, null, null);
                grvCanSer.AutoGenerateColumns = false;
                grvCanSer.DataSource = _result;

                if (_result.Rows.Count == 0)
                    MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCanSer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CancelSerialCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForCancel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanSer;
                _CommonSearch.ShowDialog();
                txtCanSer.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCanPBLvl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 4;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CancelSerialCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForCancel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanPBLvl;
                _CommonSearch.ShowDialog();
                txtCanPBLvl.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCanPB_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CancelSerialCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForCancel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCanPB;
                _CommonSearch.ShowDialog();
                txtCanPB.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCanCirc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSrch_CanSer_Click(null, null);
            if (e.KeyCode == Keys.F2)
                btnCanCirc_Click(null, null);
        }

        private void txtCanPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSrch_CanSer_Click(null, null);
            if (e.KeyCode == Keys.F2)
                btnCanPB_Click(null, null);
        }

        private void txtCanPBLvl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSrch_CanSer_Click(null, null);
            if (e.KeyCode == Keys.F2)
                btnCanPBLvl_Click(null, null);
        }

        private void txtCanSer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSrch_CanSer_Click(null, null);
            if (e.KeyCode == Keys.F2)
                btnCanSer_Click(null, null);
        }

        private void btnSelAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grvCanSer.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = true;
            }
        }

        private void btnClrCan_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grvCanSer.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = false;
            }
        }

        private void optEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (optEnd.Checked)
                btnCancel_Ser.Text = "Update Date";
        }

        private void optCan_CheckedChanged(object sender, EventArgs e)
        {
            if (optCan.Checked)
                btnCancel_Ser.Text = "Cancel Price";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear_price_cancel();
        }

        private void txtCur_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCur.Text))
                if (IsNumeric(txtCur.Text))
                {
                    MessageBox.Show("Please enter correct value.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCur.Text = "";
                    txtCur.Focus();
                    return;
                }
        }

        private void txtCanSer_DoubleClick(object sender, EventArgs e)
        {
            btnCanSer_Click(null, null);
        }

        private void txtCanPBLvl_DoubleClick(object sender, EventArgs e)
        {
            btnCanPBLvl_Click(null, null);
        }

        private void txtCanCirc_DoubleClick(object sender, EventArgs e)
        {
            btnCanCirc_Click(null, null);
        }

        private void txtCanPB_DoubleClick(object sender, EventArgs e)
        {
            btnCanPB_Click(null, null);
        }

        private void btnAddRdmPB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbRdmAllowCompany.Text))
            {
                MessageBox.Show("Please select Redeem company.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbRdmAllowCompany.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRdmComPB.Text))
            {
                MessageBox.Show("Please enter the price book for redem items.", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                txtRdmComPB.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRdmComPBLvl.Text))
            {
                MessageBox.Show("Please enter the price level for redem items.", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                txtRdmComPBLvl.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPVRDMValiedPeriod.Text))
            {
                MessageBox.Show("Please enter the Valied period for redem items.", "Promotion Vouchers", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                txtPVRDMValiedPeriod.Focus();
                return;
            }

            foreach (DataGridViewRow row in grvRdmPb.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(cmbRdmAllowCompany.SelectedValue.ToString()) && row.Cells[2].Value.ToString().Equals(txtRdmComPB.Text) && row.Cells[3].Value.ToString().Equals(txtRdmComPBLvl.Text))
                {
                    MessageBox.Show("Already Exist.", "Promotion Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            PromoVouRedeemPB _rdmPB = new PromoVouRedeemPB();
            _rdmPB.SPRPB_COMP = cmbRdmAllowCompany.SelectedValue.ToString();
            _rdmPB.SPRPB_LVL = txtRdmComPBLvl.Text;
            _rdmPB.SPRPB_PB = txtRdmComPB.Text;
            _rdmPB.SPRPB_PERIOD = Convert.ToInt32(txtPVRDMValiedPeriod.Text);
            _rdmPB.SPRPB_USER = BaseCls.GlbUserID;
            _validPeriod = Convert.ToInt32(txtPVRDMValiedPeriod.Text);
            _lstRdmPB.Add(_rdmPB);

            grvRdmPb.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _lstRdmPB;
            grvRdmPb.DataSource = new List<PromoVouRedeemPB>();
            grvRdmPb.DataSource = _source;

            cmbRdmAllowCompany.Text = "";
            txtRdmComPB.Text = "";
            txtRdmComPBLvl.Text = "";
            txtPVRDMValiedPeriod.Text = "";
        }

        private void label191_Click(object sender, EventArgs e)
        {

        }

        private void grvRdmPb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    _lstRdmPB.RemoveAt(e.RowIndex);
                    BindingSource _bnding = new BindingSource();
                    _bnding.DataSource = _lstRdmPB;

                    grvRdmPb.DataSource = _bnding;

                }
            }
        }

        private void btnAddSI_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBook.Text))
            {
                MessageBox.Show("Please enter price book", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBook.Text = "";
                txtBook.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLevel.Text))
            {
                MessageBox.Show("Please enter price level", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLevel.Text = "";
                txtLevel.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtFromQty.Text))
            {
                MessageBox.Show("Please enter valid from qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFromQty.Text = "";
                txtFromQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtToQty.Text))
            {
                MessageBox.Show("Please enter valid to qty.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtToQty.Text = "";
                txtToQty.Focus();
                return;
            }
            clearSI();
            pnlSI.Visible = true;

        }

        private void btnCloseSI_Click(object sender, EventArgs e)
        {
            pnlSI.Visible = false;
        }

        private void btn_srch_SI_Click(object sender, EventArgs e)
        {
            DataTable _dt = CHNLSVC.Sales.GET_SI_4_Price(BaseCls.GlbUserComCode, txtSrchSI.Text, dtFromSI.Value.Date, dtToSI.Value.Date, txtSrchItm.Text, txtSrchCat1.Text, txtSrchCat2.Text, txtSrchModel.Text);
            gvSI.AutoGenerateColumns = false;
            gvSI.DataSource = _dt;
        }

        private void btn_add_si_Click(object sender, EventArgs e)
        {
            gvSIItems.AutoGenerateColumns = false;
            _dtSIItems = new DataTable();
            gvSIItems.DataSource = _dtSIItems;

            foreach (DataGridViewRow row in gvSI.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    load_SI_Items(row.Cells["ib_doc_no"].Value.ToString());
                    btnFinalize.Enabled = false;
                }
            }

        }

        private void load_SI_Items(string _si)
        {
            DataTable _dt = CHNLSVC.Sales.GET_SI_ITEMS(_si, optwfoc.Checked == true ? 1 : 0);
            _dtSIItems.Merge(_dt);
            gvSIItems.DataSource = _dtSIItems;
        }

        private void btn_apply_si_Click(object sender, EventArgs e)
        {
            decimal _unitPrice = 0;
            if (string.IsNullOrEmpty(txtSIMargin.Text))
            {
                MessageBox.Show("Please enter margin", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                txtSIMargin.Focus();
                return;
            }
            foreach (DataGridViewRow row in gvSIItems.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    gvSIItems.Rows[row.Index].Cells["ice_margin"].Value = txtSIMargin.Text.ToString();
                    _unitPrice = Convert.ToDecimal(gvSIItems.Rows[row.Index].Cells["ice_actl_rt"].Value) / 100 * (100 + Convert.ToDecimal(txtSIMargin.Text));
                    gvSIItems.Rows[row.Index].Cells["ice_unit_price"].Value = _unitPrice.ToString("0.00");
                    btnFinalize.Enabled = true;
                }
            }

        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            _comList = new List<PriceCombinedItemRef>();
            Int32 _subSeq = 0;

            foreach (DataGridViewRow row in gvSIItems.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    _subSeq = _subSeq + 1;
                    PriceDetailRef _newPriceDet = new PriceDetailRef();
                    _newPriceDet.Sapd_warr_remarks = txtWaraRemarks.Text.Trim();
                    _newPriceDet.Sapd_upload_dt = DateTime.Now;
                    _newPriceDet.Sapd_update_dt = DateTime.Now;
                    _newPriceDet.Sapd_to_date = Convert.ToDateTime(dtpTo.Value).Date;
                    _newPriceDet.Sapd_session_id = BaseCls.GlbUserSessionID;
                    _newPriceDet.Sapd_seq_no = _subSeq;
                    _newPriceDet.Sapd_qty_to = Convert.ToDecimal(txtToQty.Text);
                    _newPriceDet.Sapd_qty_from = Convert.ToDecimal(txtFromQty.Text);
                    _newPriceDet.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                    _newPriceDet.Sapd_price_stus = "S";
                    _newPriceDet.Sapd_pbk_lvl_cd = txtLevel.Text.Trim();
                    _newPriceDet.Sapd_pb_tp_cd = txtBook.Text.Trim();
                    _newPriceDet.Sapd_pb_seq = _subSeq;
                    _newPriceDet.Sapd_no_of_use_times = 0;
                    _newPriceDet.Sapd_no_of_times = Convert.ToInt16(txtNoOfTimes.Text);
                    MasterItem _mstItm = CHNLSVC.Inventory.GetItem(null, Convert.ToString(gvSIItems.Rows[row.Index].Cells["ibi_itm_cd"].Value));
                    _newPriceDet.Sapd_model = _mstItm.Mi_model;
                    _newPriceDet.Sapd_mod_when = DateTime.Now;
                    _newPriceDet.Sapd_mod_by = BaseCls.GlbUserID;
                    _newPriceDet.Sapd_itm_price = Convert.ToDecimal(gvSIItems.Rows[row.Index].Cells["ice_unit_price"].Value);
                    _newPriceDet.Sapd_itm_cd = Convert.ToString(gvSIItems.Rows[row.Index].Cells["ibi_itm_cd"].Value);
                    _newPriceDet.Sapd_is_cancel = chkInactPrice.Checked;
                    _newPriceDet.Sapd_is_allow_individual = false;
                    _newPriceDet.Sapd_from_date = Convert.ToDateTime(dtpFrom.Value).Date;
                    _newPriceDet.Sapd_erp_ref = txtLevel.Text.Trim();
                    _newPriceDet.Sapd_dp_ex_cost = 0;
                    _newPriceDet.Sapd_day_attempt = 0;
                    _newPriceDet.Sapd_customer_cd = txtCusCode.Text.Trim();
                    _newPriceDet.Sapd_cre_when = DateTime.Now;
                    _newPriceDet.Sapd_cre_by = BaseCls.GlbUserID;
                    _newPriceDet.Sapd_circular_no = txtCircular.Text.Trim();
                    _newPriceDet.Sapd_cancel_dt = DateTime.MinValue;
                    _newPriceDet.Sapd_apply_on = "0";
                    if (chkEndDate.Checked && btnAmend.Enabled)
                        _newPriceDet.Sapd_ser_upload = 6;

                    _list.Add(_newPriceDet);
                }
            }

            dgvPriceDet.AutoGenerateColumns = false;
            dgvPriceDet.DataSource = new List<PriceDetailRef>();
            dgvPriceDet.DataSource = _list;

            pnlSI.Visible = false;
        }

        private void btnSelSI_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvSI.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = true;
            }
        }

        private void btnDeSelSI_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvSI.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = false;
            }
        }

        private void btnSelSIitm_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvSIItems.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = true;
            }
        }

        private void btnDeSelSIitm_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gvSIItems.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = false;
            }
        }

        private void txtSIMargin_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSIMargin.Text))
            {
                if (Convert.ToDecimal(txtSIMargin.Text) < 0)
                {
                    MessageBox.Show("Invalid margin", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    txtSIMargin.Text = "";
                    txtSIMargin.Focus();
                    return;
                }
            }
        }

        private void btn_srch_sicat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchCat1;
            _CommonSearch.txtSearchbyword.Text = txtSrchCat1.Text;
            _CommonSearch.ShowDialog();
            txtSrchCat1.Focus();
        }

        private void btn_srch_sicat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchCat2;
            _CommonSearch.txtSearchbyword.Text = txtSrchCat2.Text;
            _CommonSearch.ShowDialog();
            txtSrchCat2.Focus();
        }

        private void btn_srch_siItm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchItm;
            _CommonSearch.ShowDialog();
            txtSrchItm.Select();
        }

        private void btn_srch_simodel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtSrchModel.Focus();
        }

        private void btn_srch_sino_Click(object sender, EventArgs e)
        {

            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
            DataTable _result = CHNLSVC.CommonSearch.SearchBLHeaderNew(_CommonSearch.SearchParams, null, null, 0, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date);
            _CommonSearch.dtpFrom.Value = DateTime.Now.Date.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchSI;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtSrchSI.Select();
        }

        private void btnCloseSch_Click(object sender, EventArgs e)
        {
            pnlSch.Visible = false;
        }

        private void btnCommSchUnSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSch.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnCommSchSelect_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstSch.Items)
            {
                Item.Checked = true;
            }
        }



        private void btnAddSch_Click(object sender, EventArgs e)
        {
            Boolean _isfound = false;
            if (string.IsNullOrEmpty(txtCommAppSch.Text))
            {
                MessageBox.Show("Please select applicable scheme.", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCommAppSch.Focus();
                return;
            }
            foreach (ListViewItem schList in lstSch.Items)
            {
                if (schList.Text == txtCommAppSch.Text.Trim())
                {
                    _isfound = true;
                    MessageBox.Show("Scheme code already available", "Scheme Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
            if (_isfound == false)
            {
                lstSch.Items.Add(txtCommAppSch.Text.Trim());
                btnCommSchSelect_Click(null, null);
                txtCommAppSch.Text = "";
                lblComSchType.Text = "";
                lblCommSchInt.Text = "";
                lblCommSchTerm.Text = "";
            }
            txtCommAppSch.Focus();
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

        private void btnSearchCommAppSch_Click_1(object sender, EventArgs e)
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



        private void optChnl_CheckedChanged(object sender, EventArgs e)
        {
            if (optChnl.Checked) btnAddItem.Enabled = false;
        }

        private void optSChnl_CheckedChanged(object sender, EventArgs e)
        {
            if (optSChnl.Checked) btnAddItem.Enabled = false;
        }

        private void optPC_CheckedChanged(object sender, EventArgs e)
        {
            if (optPC.Checked) btnAddItem.Enabled = true;
        }

        private void optChnlSIS_CheckedChanged(object sender, EventArgs e)
        {
            if (optChnlSIS.Checked) btnAddSPC.Enabled = false;
        }

        private void optSCHNLSIS_CheckedChanged(object sender, EventArgs e)
        {
            if (optSCHNLSIS.Checked) btnAddSPC.Enabled = false;
        }

        private void btnCrdCntl_Click(object sender, EventArgs e)
        {
            pnlSch.Visible = true;
        }

        private void chkInf_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInf.Checked)
                btnCrdCntl.Enabled = true;
            else
            {
                if (!string.IsNullOrEmpty(txtRem.Text) || lstSch.Items.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to clear the credit control information?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        lstSch.Clear();
                        txtRem.Text = "";
                    }
                    else
                    {

                    }
                }
                btnCrdCntl.Enabled = false;
            }
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

        private void optPCSIS_CheckedChanged(object sender, EventArgs e)
        {
            btnAddSPC.Enabled = true;
        }

        private void txtSrchCat2_Leave(object sender, EventArgs e)
        {

        }

        private void rbUploadExcel_CheckedChanged(object sender, EventArgs e)
        {
            grbUploadCircular.Visible = false;
            grbUploadExcel.Visible = true;
        }

        private void rbUploadCircular_CheckedChanged(object sender, EventArgs e)
        {
            grbUploadExcel.Visible = false;
            grbUploadCircular.Visible = true;
        }

        private void btnSearchUploadCircular_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUploadCircular.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                    DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtUploadCircular;
                    _CommonSearch.ShowDialog();
                    txtUploadCircular.Select();
                }


                if (!string.IsNullOrEmpty(txtUploadCircular.Text))
                {

                    DataTable _tp = CHNLSVC.Sales.GetPriceTypeByCir(txtUploadCircular.Text.Trim());

                    //if (_tp.Rows.Count >= 2)
                    //{
                    //    if (MessageBox.Show("This circular having multiple price type. Do you want to check type wise ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    //    {
                    //        pnlPriceTp.Visible = true;
                    //        dgvType.AutoGenerateColumns = false;
                    //        dgvType.DataSource = new DataTable();
                    //        dgvType.DataSource = _tp;
                    //        return;
                    //    }
                    //}

                    circularPromoList.Clear();
                    DataTable dt = CHNLSVC.Sales.GetPromoCodesByCir(txtUploadCircular.Text.Trim(), null, null, null);

                    if (dt.Rows.Count > 0)
                    {
                        pnlCircularPromoCodes.Visible = true;
                        btnMainSave.Enabled = true;
                        foreach (DataRow drow in dt.Rows)
                        {
                            circularPromoList.Items.Add(drow["sapd_promo_cd"].ToString());
                        }
                        btnUploadCircular.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Records not found. Please enter valid circluar #.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUploadCircular.Text = "";
                        txtUploadCircular.Focus();
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

        private void txtUploadCircular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchUploadCircular_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                //if (string.IsNullOrEmpty(txtUploadCircular.Text))
                //{
                btnSearchUploadCircular.Focus();
                btnSearchUploadCircular.Select();
                btnSearchUploadCircular_Click(null, null);
                //}
                //else { btnUploadCircular.Focus(); }
            }
        }

        private void txtUploadCircular_DoubleClick(object sender, EventArgs e)
        {
            btnSearchUploadCircular_Click(null, null);
        }

        private void btnUploadCircular_Click(object sender, EventArgs e)
        {
            try
            {
                cmbPriceType.Enabled = true;
                btnMainSave.Enabled = true;
                Boolean _isValidItm = false;
                List<string> _item = new List<string>();

                dgvPriceDet.AutoGenerateColumns = false;
                dgvPriceDet.DataSource = new List<PriceDetailRef>();

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = new List<PriceCombinedItemRef>();

                dgvAppPC.AutoGenerateColumns = false;
                dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();

                foreach (ListViewItem Item in circularPromoList.Items)
                {


                    if (Item.Checked == true)
                    {
                        _item.Add(Item.Text);
                        _isValidItm = true;

                    }
                }
                Int16 I = 0;

                if (_isValidItm == false)
                {
                    MessageBox.Show("Please select promotion code to get details.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //kapila 8/3/2017
                _CircSch = new List<Circular_Schemes>();
                lstSch.Clear();
                DataTable _dtPSCH = CHNLSVC.General.GetPBScheme(txtUploadCircular.Text);
                foreach (DataRow r in _dtPSCH.Rows)
                {
                    lstSch.Items.Add(r["saps_sch"].ToString());
                }
                btnCommSchSelect_Click(null, null);

                txtRem.Text = "";
                chkInf.Checked = false;
                chkInf.Enabled = true;
                btnCrdCntl.Enabled = false;
                DataTable _dtPSCHR = CHNLSVC.General.GetPBSchemeRem(txtUploadCircular.Text);
                if (_dtPSCHR.Rows.Count > 0)
                {
                    chkInf.Checked = true;
                    chkInf.Enabled = false;
                    btnCrdCntl.Enabled = true;
                    txtRem.Text = _dtPSCHR.Rows[0]["sapsr_rem"].ToString();
                }
                _list = new List<PriceDetailRef>();
                _appPcList = new List<PriceProfitCenterPromotion>();

                List<PriceCombinedItemRef> _tmpComList = new List<PriceCombinedItemRef>();
                _comList = new List<PriceCombinedItemRef>();

                foreach (string st in _item)
                {
                    _list.AddRange(CHNLSVC.Sales.GetPricebyCirandPromo(txtUploadCircular.Text.Trim(), st));
                }


                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        //Check item is active
                        List<string> _tmpItems = new List<string>();
                        foreach (PriceDetailRef promoItem in _list)
                        {
                            if (CHNLSVC.Inventory.IsItemActive(promoItem.Sapd_itm_cd) == 0)
                            {
                                _tmpItems.Add(promoItem.Sapd_itm_cd);
                            }
                        }

                        if (_tmpItems.Count > 0)
                        {
                            DialogResult _result = MessageBox.Show("Selected promotion contains inactive items. Do you want to add those items ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == System.Windows.Forms.DialogResult.No)
                            {
                                foreach (string item in _tmpItems)
                                {
                                    _list.RemoveAll(x => x.Sapd_itm_cd == item);
                                }
                            }
                        }

                        //Check item is inactive for company
                        List<string> _inactiveItems = new List<string>();
                        foreach (PriceDetailRef promoItem in _list)
                        {
                            if (!CHNLSVC.Inventory.IsValidCompanyItem(BaseCls.GlbUserComCode, promoItem.Sapd_itm_cd, 1))
                            {
                                _inactiveItems.Add(promoItem.Sapd_itm_cd);
                            }
                        }

                        if (_inactiveItems.Count > 0)
                        {
                            DialogResult _result = MessageBox.Show("Selected promotion contains inactive items for current company. Do you want to add those items ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == System.Windows.Forms.DialogResult.No)
                            {
                                foreach (string tmpItem in _inactiveItems)
                                {
                                    _list.RemoveAll(x => x.Sapd_itm_cd == tmpItem);
                                }
                            }
                        }

                        //check for inactive price
                        int _inactiveItemCount = 0;
                        _inactiveItemCount = _list.Where(x => x.Sapd_is_cancel == true).Select(x => x.Sapd_itm_cd).Count();
                        if (_inactiveItemCount > 0)
                        {
                            DialogResult _result = MessageBox.Show("Selected promotion contains inactive prices. Do you want to add those items ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == System.Windows.Forms.DialogResult.No)
                            {
                                _list.RemoveAll(x => x.Sapd_is_cancel == true);
                            }
                        }

                        //check for cancel price
                        int _cancelItemCount = 0;
                        _cancelItemCount = _list.Where(x => x.Sapd_price_stus == "C").Select(x => x.Sapd_itm_cd).Count();
                        if (_cancelItemCount > 0)
                        {
                            DialogResult _result = MessageBox.Show("Selected promotion contains cancel prices. Do you want to add those items ?", "Price Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (_result == System.Windows.Forms.DialogResult.No)
                            {
                                _list.RemoveAll(x => x.Sapd_price_stus == "C");
                            }
                        }

                        //Check for combine promotions
                        int _tmpCount = _list.Where(x => x.Sapd_price_type == 1).Select(x => x.Sapd_itm_cd).Count();
                        if ((_tmpCount > 0) && (cmbPriceType.Items.Count > 0))
                        {
                            cmbPriceType.SelectedIndex = 3;
                            cmbPriceType.Enabled = false;
                        }
                    }
                }

                foreach (PriceDetailRef _tmp in _list)
                {
                    // txtBook.Text = _tmp.Sapd_pb_tp_cd;
                    //txtLevel.Text = _tmp.Sapd_pbk_lvl_cd;
                    //dtpFrom.Value = _tmp.Sapd_from_date.Date;
                    // dtpTo.Value = _tmp.Sapd_to_date.Date;
                    //txtPromoCode.Text = _tmp.Sapd_promo_cd;
                    //cmbPriceType.SelectedValue = _tmp.Sapd_price_type;
                    //txtCusCode.Text = _tmp.Sapd_customer_cd;
                    if (_tmp.Sapd_usr_ip == "IGNORE COMBINE")
                    {
                        chkWithOutCombine.Checked = true;
                    }
                    else
                    {
                        chkWithOutCombine.Checked = false;
                    }
                    //cmbPriceType.Enabled = false;

                    _tmpComList = new List<PriceCombinedItemRef>();
                    _tmpComList = CHNLSVC.Sales.GetPriceCombinedItemLine(_tmp.Sapd_pb_seq, _tmp.Sapd_seq_no, _tmp.Sapd_itm_cd, null);
                    _comList.AddRange(_tmpComList);


                    List<PriceProfitCenterPromotion> _tmpAppPC = new List<PriceProfitCenterPromotion>();
                    _tmpAppPC.AddRange(CHNLSVC.Sales.GetAllocPromoPc(BaseCls.GlbUserComCode, _tmp.Sapd_promo_cd, _tmp.Sapd_pb_seq));
                    _appPcList.AddRange(_tmpAppPC);

                    if (_tmp.Sapd_price_stus == "A")
                    {
                        lblPAStatus.Text = "Active";
                    }
                    else if (_tmp.Sapd_price_stus == "P")
                    {
                        lblPAStatus.Text = "Pening Approval";

                        //kapila 11/4/2016
                        List<mst_itm_com_reorder> _ltsCost = new List<mst_itm_com_reorder>();
                        _ltsCost = CHNLSVC.General.GetReOrder(_tmp.Sapd_itm_cd);
                        if (_ltsCost == null)
                        {
                            _ltsCost = new List<mst_itm_com_reorder>();
                        }
                        foreach (mst_itm_com_reorder _tempr in _ltsCost)
                        {
                            if (_tempr.Icr_com_code == BaseCls.GlbUserComCode)
                            {
                                _tmp.Sapd_lst_cost = _tempr.Icr_max_cost;
                                _tmp.Sapd_avg_cost = _tempr.Icr_avg_cost;

                                if (_tmp.Sapd_avg_cost > 0)
                                {
                                    _tmp.Sapd_margin = (_tmp.Sapd_itm_price - _tmp.Sapd_avg_cost) / _tmp.Sapd_avg_cost * 100;
                                }
                                else
                                {
                                    _tmp.Sapd_margin = 100;
                                }
                            }
                        }

                    }
                }

                dgvPriceDet.AutoGenerateColumns = false;
                dgvPriceDet.DataSource = new List<PriceDetailRef>();
                dgvPriceDet.DataSource = _list;

                dgvPromo.AutoGenerateColumns = false;
                dgvPromo.DataSource = new List<PriceCombinedItemRef>();
                dgvPromo.DataSource = _comList;

                dgvAppPC.AutoGenerateColumns = false;
                dgvAppPC.DataSource = new List<PriceProfitCenterPromotion>();
                dgvAppPC.DataSource = _appPcList;

                if (_appPcList != null)
                {
                    foreach (PriceProfitCenterPromotion _chk in _appPcList)
                    {
                        foreach (DataGridViewRow row in dgvAppPC.Rows)
                        {
                            string _pc = row.Cells["col_a_Pc"].Value.ToString();
                            string _promo = row.Cells["col_a_Promo"].Value.ToString();
                            Int32 _pbSeq = Convert.ToInt32(row.Cells["col_a_pbSeq"].Value);

                            if (_pc == _chk.Srpr_pc && _promo == _chk.Srpr_promo_cd && _pbSeq == _chk.Srpr_pbseq)
                            {
                                DataGridViewCheckBoxCell chk = row.Cells[2] as DataGridViewCheckBoxCell;
                                //if (Convert.ToBoolean(chk.Value) == false)
                                if (_chk.Srpr_act == 1)
                                {
                                    chk.Value = true;
                                    goto L1;
                                }
                                else
                                {
                                    chk.Value = false;
                                    goto L1;
                                }
                            }

                        }
                    L1: Int16 x = 1;
                    }
                }

                if (_list.Count > 0)
                {
                    _list.ForEach(x => x.Sapd_no_of_use_times = 0);
                }
                ////give used message
                //foreach (PriceDetailRef _ref in _list)
                //{
                //    if (_ref.Sapd_no_of_use_times > 0)
                //    {
                //        MessageBox.Show("Promotion Code - " + _ref.Sapd_promo_cd + " has used " + _ref.Sapd_no_of_use_times);
                //    }
                //}

                PriceDetailRestriction _tmpRes = new PriceDetailRestriction();
                foreach (PriceDetailRef _tmp in _list)
                {
                    _tmpRes = CHNLSVC.Sales.GetPromotionRestriction(BaseCls.GlbUserComCode, _tmp.Sapd_promo_cd);
                }

                chkCustomer.Checked = false;
                chkNIC.Checked = false;
                chkMobile.Checked = false;
                txtMessage.Text = "";
                chkPP.Checked = false;
                chkDL.Checked = false;

                if (_tmpRes != null)
                {
                    //chkCustomer.Checked = _tmpRes.Spr_need_cus;
                    //chkNIC.Checked = _tmpRes.Spr_need_nic;
                    //chkMobile.Checked = _tmpRes.Spr_need_mob;
                    //txtMessage.Text = _tmpRes.Spr_msg;
                    //chkPP.Checked = _tmpRes.Spr_need_pp;
                    //chkDL.Checked = _tmpRes.Spr_need_dl;
                }

                _isRecal = true;
                btnCancel.Enabled = true;
                btnAppPCUpdate.Enabled = true;
                // btnMainSave.Enabled = false;
                btnSaveAs.Enabled = true;
                btnAddSI.Enabled = false;
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

        private void UploadCircularDetails()
        {
            try
            {
                if (string.IsNullOrEmpty(txtUploadCircular.Text))
                {
                    MessageBox.Show("Please enter the circular number!", "Upload Circular - Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUploadCircular.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("An error occurred while loading circular details" + Environment.NewLine + ex.Message, "Upload Circular - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSelectPromos_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in circularPromoList.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnUnSelectPromos_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in circularPromoList.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnCloseCircularPnl_Click(object sender, EventArgs e)
        {
            int _selectedPromoCount = 0;
            foreach (ListViewItem Item in circularPromoList.Items)
            {
                if (Item.Checked == true)
                {
                    _selectedPromoCount += 1;
                }
            }

            if (_selectedPromoCount == 0)
            {
                DialogResult _dialog = MessageBox.Show("Promotion code hasn't selected. Do you want to continue ?", "Upload Circular - Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_dialog == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            pnlCircularPromoCodes.Visible = false;
        }

        private void txtUploadCircular_TextChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtUploadCircular.Text)) || (string.IsNullOrWhiteSpace(txtUploadCircular.Text)))
            {
                btnUploadCircular.Visible = false;
            }
        }

        private void btnserchShipmnt_Click(object sender, EventArgs e)
        {

            shpmnet_Hdr = CHNLSVC.Inventory.GET_shpmnet_Hdr(BaseCls.GlbUserComCode, dtfrmshipment.Value.Date, dttoshipment.Value.Date, "I", "C", txtdocNo.Text.Trim());
            if (shpmnet_Hdr.Rows.Count > 0)
            {
              
                grdshipment_hdr.AutoGenerateColumns = false;
                grdshipment_hdr.DataSource = shpmnet_Hdr;

            }
            else
            {
                grdshipmentDet.DataSource = null;
                grdshipment_hdr.DataSource = null;
                MessageBox.Show("Record Not Found", "Base on Shipmant costing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUploadCircular.Focus();
                return;
            }
        }

        private void btnshipmentview_Click(object sender, EventArgs e)
        {
            pnl_shipment.Visible = true;
            rdowithoutfoc.Checked = true;
            this.pnl_shipment.Size = new System.Drawing.Size(750, 435);
            this.pnl_shipment.Location = new System.Drawing.Point(61, 5);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            pnl_shipment.Visible = false;
        }

        private void grdshipment_hdr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                //try
                //{
                //    Int32 row_aff = 0;
                //    string _msg = string.Empty;

                //    if (grdshipment_hdr == null) return;

                //    int row_id = e.RowIndex;
                //    string sts = "";
                //    string doc_no = grdshipment_hdr.Rows[e.RowIndex].Cells["ib_doc_nos"].Value.ToString();
                //     shipmtDetail = CHNLSVC.Inventory.GET_shpmnet_Detail(doc_no);
                    
                     

                //    if (shipmtDetail.Rows.Count > 0)
                //    {
                //        if (rdowithfoc.Checked == true)
                //        {
                //            IEnumerable<DataRow> results = (from MyRows in shipmtDetail.AsEnumerable()
                //                                            where
                //                                             MyRows.Field<string>("ibi_tp") != "F"
                //                                            select MyRows);
                //            shipmtDetail = results.CopyToDataTable();
                //            grdshipmentDet.AutoGenerateColumns = false;
                //            grdshipmentDet.DataSource = shipmtDetail;
                //        }
                //        else
                //        {
                //            grdshipmentDet.AutoGenerateColumns = false;
                //           grdshipmentDet.DataSource = shipmtDetail;
                //        }
                //        //SELECt * FROM IMP_BL_ITM WHERE IBI_DOC_NO ='ABLSI171629' --ibi_tp
                          
                //    }
                 
                //}
                //catch (Exception)
                //{
                //     throw;
                //}
                   
                
              

    


             
        }

        private void rdowithoutfoc_CheckedChanged(object sender, EventArgs e)
        {
            DataTable shipmtDetailnew = new DataTable();
           
            foreach (DataGridViewRow dgvr in grdshipment_hdr.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["chk"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    string doc_no = dgvr.Cells["ib_doc_nos"].Value.ToString();
                    DataTable temp = CHNLSVC.Inventory.GET_shpmnet_Detail(doc_no);
                    shipmtDetailnew.Merge(temp);
                }
            }

            if (shipmtDetailnew.Rows.Count > 0)
            {
                if (rdowithfoc.Checked == true)
                {
                    //IEnumerable<DataRow> results = (from MyRows in shipmtDetailnew.AsEnumerable()
                    //                                where
                    //                                 MyRows.Field<string>("ibi_tp") == "F"
                    //                                select MyRows);
                    //shipmtDetailnew = results.CopyToDataTable();
                    grdshipmentDet.AutoGenerateColumns = false;
                    grdshipmentDet.DataSource = shipmtDetailnew;
                }
                else
                {
                    IEnumerable<DataRow> results = (from MyRows in shipmtDetailnew.AsEnumerable()
                                                    where
                                                     MyRows.Field<string>("ibi_tp") != "F"
                                                    select MyRows);
                    shipmtDetailnew = results.CopyToDataTable();
                    grdshipmentDet.AutoGenerateColumns = false;
                    grdshipmentDet.DataSource = shipmtDetailnew;

                }
                //SELECt * FROM IMP_BL_ITM WHERE IBI_DOC_NO ='ABLSI171629' --ibi_tp

            }
        }

        private void btnprocess_Click(object sender, EventArgs e)
        {
            DataTable shipmtDetailnew = new DataTable();
            shipmtDetail = new DataTable();
            foreach (DataGridViewRow dgvr in grdshipment_hdr.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["chk"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    string doc_no = dgvr.Cells["ib_doc_nos"].Value.ToString();
                    DataTable temp = CHNLSVC.Inventory.GET_shpmnet_Detail(doc_no);
                    shipmtDetail.Merge(temp);
                }
            }
            if (shipmtDetail.Rows.Count > 0)
            {
                if (rdowithfoc.Checked == true)
                {
                    //IEnumerable<DataRow> results = (from MyRows in shipmtDetail.AsEnumerable()
                    //                                where
                    //                                 MyRows.Field<string>("ibi_tp") == "F"
                    //                                select MyRows);
                    //shipmtDetailnew = results.CopyToDataTable();
                    grdshipmentDet.AutoGenerateColumns = false;
                    grdshipmentDet.DataSource = shipmtDetail;
                }
                else
                {
                    IEnumerable<DataRow> results = (from MyRows in shipmtDetail.AsEnumerable()
                                                    where
                                                     MyRows.Field<string>("ibi_tp") != "F"
                                                    select MyRows);
                    shipmtDetailnew = results.CopyToDataTable();
                    grdshipmentDet.AutoGenerateColumns = false;
                    grdshipmentDet.DataSource = shipmtDetailnew;

                }
            }
           
        }

        private void btncostprocess_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in grdshipmentDet.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                 


                    Int32 seqNo = Convert.ToInt32(dgvr.Cells["seq_no"].Value.ToString());
                    Int32 refline = Convert.ToInt32(dgvr.Cells["ref_line"].Value.ToString());
                    decimal duty_paid_cost = CHNLSVC.Inventory.GET_ICE_ACTL_RT(seqNo, refline);
                    dgvr.Cells["duty_paid_cost"].Value = duty_paid_cost.ToString("#.##");
                }

            }

        }

        private void btnmargingapply_Click(object sender, EventArgs e)
        {
            if (!IsNumeric( txtmarging.Text))
            {
                 MessageBox.Show("Please enter marging value!", "Base on Shipmant costing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  txtmarging.Focus();
                    return;
            }  
            foreach (DataGridViewRow dgvr in grdshipmentDet.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    if (rdodutypaidcost.Checked==true)
                    {
                        try
                        {
                            Decimal duty_paid_cost = Convert.ToDecimal(dgvr.Cells["duty_paid_cost"].Value.ToString());
                            Decimal pricetoupdate = (duty_paid_cost * Convert.ToDecimal(txtmarging.Text)) / 100 + duty_paid_cost;
                            dgvr.Cells["update_price"].Value = pricetoupdate.ToString("#.##");
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("First Process Duty paid cost", "Base on Shipmant costing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                   

                  
                  
                }

            }
        }

        private void btnfinalz_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in grdshipmentDet.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells["Select"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    if (rdodutypaidcost.Checked == true)
                    {
                        try
                        {
                            _seqNo = _seqNo + 1;
                            PriceDetailRef _newPriceDet = new PriceDetailRef();
                            _newPriceDet.Sapd_warr_remarks = "N/A";
                            _newPriceDet.Sapd_upload_dt = DateTime.Now;
                            _newPriceDet.Sapd_update_dt = DateTime.Now;
                            _newPriceDet.Sapd_to_date = Convert.ToDateTime(dtpTo.Value).Date;
                            _newPriceDet.Sapd_session_id = BaseCls.GlbUserSessionID;
                            _newPriceDet.Sapd_seq_no = _seqNo;
                            _newPriceDet.Sapd_qty_to = 9999;
                            _newPriceDet.Sapd_qty_from = 1;
                            _newPriceDet.Sapd_price_type = Convert.ToInt16(cmbPriceType.SelectedValue);
                           // _newPriceDet.Sapd_price_stus = _priceType;
                            _newPriceDet.Sapd_pbk_lvl_cd = txtLevel.Text.Trim();
                            _newPriceDet.Sapd_pb_tp_cd = txtBook.Text.Trim();
                            _newPriceDet.Sapd_pb_seq = _seqNo;
                            _newPriceDet.Sapd_no_of_use_times = 0;
                            _newPriceDet.Sapd_no_of_times = 9999;
                            _newPriceDet.Sapd_model = dgvr.Cells["model"].Value.ToString();
                            _newPriceDet.Sapd_mod_when = DateTime.Now;
                            _newPriceDet.Sapd_mod_by = BaseCls.GlbUserID;
                            _newPriceDet.Sapd_itm_price = Convert.ToDecimal(dgvr.Cells["update_price"].Value.ToString());
                            _newPriceDet.Sapd_itm_cd = dgvr.Cells["itm_cd"].Value.ToString();
                            _newPriceDet.Mi_shortdesc = dgvr.Cells["itm_shortdesc"].Value.ToString();
                              
                            //_newPriceDet.Sapd_is_cancel = chkInactPrice.Checked;
                            _newPriceDet.Sapd_is_allow_individual = false;
                            //_newPriceDet.Sapd_from_date = Convert.ToDateTime(dtpFrom.Value).Date;
                            //_newPriceDet.Sapd_erp_ref = txtLevel.Text.Trim();
                            _newPriceDet.Sapd_dp_ex_cost = 0;
                            _newPriceDet.Sapd_day_attempt = 0;
                            //_newPriceDet.Sapd_customer_cd = txtCusCode.Text.Trim();
                            _newPriceDet.Sapd_cre_when = DateTime.Now;
                            _newPriceDet.Sapd_cre_by = BaseCls.GlbUserID;
                            _newPriceDet.Sapd_circular_no = txtCircular.Text.Trim();
                            _newPriceDet.Sapd_cancel_dt = DateTime.MinValue;
                            _newPriceDet.Sapd_apply_on = "0";
                             _list.Add(_newPriceDet);
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("First Process Duty paid cost", "Base on Shipmant costing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }




                }

            }

            dgvPriceDet.AutoGenerateColumns = false;
            dgvPriceDet.DataSource = _list;
            pnl_shipment.Visible = false;
        }

     

        private void chkselectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkselectAll.Checked == true)
            {
                for (int i = 0; i < grdshipmentDet.RowCount; i++)
                {
                    grdshipmentDet[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grdshipmentDet.RowCount; i++)
                {
                    grdshipmentDet[0, i].Value = false;
                }
            }
        }

        private void chkgrdshipment_slectall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkgrdshipment_slectall.Checked == true)
            {
                for (int i = 0; i < grdshipment_hdr.RowCount; i++)
                {
                    grdshipment_hdr[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grdshipment_hdr.RowCount; i++)
                {
                    grdshipment_hdr[0, i].Value = false;
                }
            }
        }

       

        private void btnaddmoredet_Click(object sender, EventArgs e)
        {
            pnl_mor_det.Visible = true;
            this.pnl_mor_det.Size = new System.Drawing.Size(293, 115);
            this.pnl_mor_det.Location = new System.Drawing.Point(36, 114);
            //DataTable GetBrandManager = CHNLSVC.Sales.GetBrandManager(BaseCls.GlbUserComCode);
            //if (GetBrandManager != null && GetBrandManager.Rows.Count > 0)
            // {
            //     cmbbrandmnger.DataSource = GetBrandManager;
            //     cmbbrandmnger.DisplayMember = "esep_name_initials";
            //     cmbbrandmnger.ValueMember = "esep_epf";
            //     cmbbrandmnger.SelectedIndex = -1;
            // }
        }

        private void BTNSERCHNRANDMNGR_Click(object sender, EventArgs e)
        {

        }

        private void TXTBRANDMNGR_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void TXTBRANDMNGR_Leave(object sender, EventArgs e)
        {
            
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCircular.Text))
            {
                MessageBox.Show("Select Circular number", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtCircular.Text) && string.IsNullOrEmpty(txtexpectedqty.Text) && string.IsNullOrEmpty(txtexpectedqty.Text) )
            {
               MessageBox.Show("Select values", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SAR_PB_CIREFFECT _SAR_PB_CIREFFECT = new SAR_PB_CIREFFECT();
            _SAR_PB_CIREFFECT.spc_circular = txtCircular.Text;
            _SAR_PB_CIREFFECT.spc_cre_by = BaseCls.GlbUserID;
            _SAR_PB_CIREFFECT.spc_est_qty = string.IsNullOrEmpty(txtexpectedqty.Text) ? 0 : Convert.ToInt32(txtexpectedqty.Text.ToString());
            _SAR_PB_CIREFFECT.spc_est_val = string.IsNullOrEmpty(txtexpectedvalue.Text) ? 0 : Convert.ToInt32(txtexpectedvalue.Text.ToString());
            _SAR_PB_CIREFFECT.spc_mod_by = BaseCls.GlbUserID;
            _SAR_PB_CIREFFECT.spc_act = 1;
            _SAR_PB_CIREFFECT.SPC_BRAND_MNGR = TXTBRANDMNGR.Text;
            _SAR_PB_CIREFFECT.spc_session = BaseCls.GlbUserSessionID;


          
            string _err = "";
            Int32 eff = (Int32)CHNLSVC.Sales.SAVE_SAR_PB_CIREFFECT(_SAR_PB_CIREFFECT);

            if (eff == 1)
            {
                MessageBox.Show("Update successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Clear_Data();

            }
        }

        private void TXTBRANDMNGR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
               // btnclsmordet_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void updatepur_det_Click(object sender, EventArgs e)
        {
            List<PriceDetailRef> _listnew = new List<PriceDetailRef>();
            foreach (DataGridViewRow _row in grdcurdet.Rows)
            {
                if (! String.IsNullOrEmpty(_row.Cells[7].Value.ToString()) && ! String.IsNullOrEmpty(_row.Cells[8].Value.ToString()))
                {
                    FF.BusinessObjects.PriceDetailRef _PriceDetailRef = new FF.BusinessObjects.PriceDetailRef();
                    _PriceDetailRef.Sapd_pb_tp_cd = _row.Cells[0].Value.ToString();
                    //sapd_pb_tp_cd
                    _PriceDetailRef.Sapd_pbk_lvl_cd = _row.Cells[1].Value.ToString();
                    _PriceDetailRef.Sapd_seq_no = Convert.ToInt32(_row.Cells[2].Value.ToString());
                    _PriceDetailRef.Sapd_erp_ref = _row.Cells[3].Value.ToString();
                    _PriceDetailRef.Sapd_itm_cd = _row.Cells[4].Value.ToString();
                    _PriceDetailRef.Sapd_model = _row.Cells[5].Value.ToString();
                    _PriceDetailRef.Sapd_circular_no = _row.Cells[6].Value.ToString();
                    _PriceDetailRef.SAPD_EST_QTY = Convert.ToInt32(_row.Cells[7].Value.ToString());
                    _PriceDetailRef.SAPD_EST_VAL = Convert.ToInt32(_row.Cells[8].Value.ToString());
                    _PriceDetailRef.Sapd_mod_by = BaseCls.GlbUserID;
                    // _PriceDetailRef.Sapd_mod_when = _row.Cells[0].Value.ToString();
                    _listnew.Add(_PriceDetailRef); 
                }
            
                
            }

            Int32 eff = CHNLSVC.Sales.Update_price_def_EST(_listnew);
            if (eff == 1)
            {
                MessageBox.Show("Update successfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Update Unsuccessfully.", "Price creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
                 // String header = row.Columns[i].HeaderText;
                   // String cellText = row.Cells[i].Text;
                
            
        }

        private void btnclsmordet_Click(object sender, EventArgs e)
        {
            pnl_mor_det.Visible = false;
            }

        private void btncls_det_Click(object sender, EventArgs e)
        {
            pnlcirdet.Visible = false;
        }

        private void btnbranmgrserch_Click(object sender, EventArgs e)
        {
            try
            {
                _Ltype = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.brandmngr);
                DataTable _result = CHNLSVC.CommonSearch.Getbrandmng(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TXTBRANDMNGR;
                _CommonSearch.ShowDialog();
                TXTBRANDMNGR.Select();

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

        private void cmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbParameterType.Text == "DISCOUNT")
            {
                rbdDiscValue.Checked = true;
                pnlDiscount.Visible = true;
                pnlDiscType.Visible = true;
                pnlVoucherValue.Visible = false;
                txtVoucherDiscount.Text = string.Empty;
                txtVoucherValue.Text = string.Empty;
                pnlValidPeriod.Visible = true;
                txtValidPeriod.Text = string.Empty;
                pnl_p_book.Visible = false;
                pnl_p_level.Visible = false;
            }
            else if (cmbParameterType.Text == "VALUE")
            {
                pnlVoucherValue.Visible = true;
                pnlDiscType.Visible = false;
                pnlDiscount.Visible = false;
                txtVoucherDiscount.Text = string.Empty;
                txtVoucherValue.Text = string.Empty;
                pnlValidPeriod.Visible = true;
                txtValidPeriod.Text = string.Empty;
                pnl_p_book.Visible = false;
                pnl_p_level.Visible = false;
            }
            else if (cmbParameterType.Text == "CURRENT PRICE")
            {
                pnlVoucherValue.Visible = true;
                pnlDiscType.Visible = false;
                pnlDiscount.Visible = false;
                txtVoucherDiscount.Text = string.Empty;
                txtVoucherValue.Text = string.Empty;
                pnlValidPeriod.Visible = true;
                txtValidPeriod.Text = string.Empty;
                pnl_p_book.Visible = true;
                pnl_p_level.Visible = true;
                pnlVoucherValue.Visible = false;
                btn_get_price.Visible = false;
            }
        }

        private void rbdDiscValue_CheckedChanged(object sender, EventArgs e)
        {
            lblDiscType.Text = "Discount Value";
            txtVoucherDiscount.Text = string.Empty;
            txtVoucherValue.Text = string.Empty;
        }

        private void rbdDiscountRate_CheckedChanged(object sender, EventArgs e)
        {
            lblDiscType.Text = "Discount Rate";
            txtVoucherDiscount.Text = string.Empty;
            txtVoucherValue.Text = string.Empty;
        }

        private void btnClosePnlVoucherParameter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbParameterType.Text))
            {
                MessageBox.Show("Please selected the voucher type", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbParameterType.Focus();
                cmbParameterType.Select();
                return;
            }

            if (cmbParameterType.Text == "DISCOUNT" && (!rbdDiscValue.Checked) && (rbdDiscValue.Checked))
            {
                MessageBox.Show("Please selected the discount type", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rbdDiscValue.Focus();
                rbdDiscValue.Select();
                return;
        }

            int _tmpValue = 0;
            if (cmbParameterType.Text == "DISCOUNT")
            {
                int.TryParse(txtVoucherDiscount.Text, out _tmpValue);
                if (_tmpValue < 1)
                {
                    if (rbdDiscValue.Checked)
                    {
                        MessageBox.Show("Discount value cannot be empty, negative or zero !", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtVoucherDiscount.Focus();
                        return;
                    }
                    else if (rbdDiscountRate.Checked)
                    {
                        MessageBox.Show("Discount rate cannot be empty, negative or zero !", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtVoucherDiscount.Focus();
                        return;
                    }
                }
            }
            else if (cmbParameterType.Text == "VALUE")
            {
                int.TryParse(txtVouValue.Text, out _tmpValue);
                if (_tmpValue < 1)
                {
                    MessageBox.Show("Voucher value cannot be empty, negative or zero !", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVouValue.Focus();
                    return;
                }
            }
            else if (cmbParameterType.Text == "CURRENT PRICE")
            {
                int.TryParse( "1", out _tmpValue);
                if (_tmpValue < 1)
                {
                    MessageBox.Show("Voucher value cannot be empty, negative or zero !", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVouValue.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txt_p_book.Text))
                {
                    MessageBox.Show("Please selected the voucher price book ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_p_book.Focus();
                    txt_p_book.Select();
                    return;
                }
                if (string.IsNullOrEmpty(txt_p_level.Text))
                {
                    MessageBox.Show("Please selected the voucher price book ", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_p_level.Focus();
                    txt_p_level.Select();
                    return;
                }
                if (string.IsNullOrEmpty(txtValidPeriod.Text))
                {
                    MessageBox.Show("Voucher valid period cannot be empty, negative or zero !", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValidPeriod.Focus();
                    txtValidPeriod.Select();
                    return;
                }

            }
            int _validPeriod = 0;
            int.TryParse(txtValidPeriod.Text, out _validPeriod);
            if (_tmpValue < 1)
            {
                MessageBox.Show("Voucher valid period cannot be empty, negative or zero !", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValidPeriod.Focus();
                return;
            }

            //DialogResult _result = MessageBox.Show("Voucher parameters has not defined. Do you want to exit ?", "System Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (_result == System.Windows.Forms.DialogResult.Yes)
           // {
                pnlVoucherParameters.Visible = false;
            //}
            //else
            //{
            //    cmbParameterType.Focus();
            //    cmbParameterType.Select();
            //    return;
            //}            
        }

        private void ClearVoucherParameters()
        {
            pnlVoucherParameters.Visible = false;
            cmbParameterType.SelectedIndex = -1;
            rbdDiscValue.Checked = true;
            rbdDiscountRate.Checked = false;
            lblDiscType.Text = "Discount Value";
            txtVoucherDiscount.Text = string.Empty;
            txtVoucherValue.Text = string.Empty;
        }
       
        private void DisplayVoucherParameterPnl()
        {            
            rbdDiscValue.Checked = false;
            rbdDiscountRate.Checked = false;
            pnlDiscount.Visible = false;
            pnlDiscType.Visible = false;
            pnlVoucherValue.Visible = false;
            txtVoucherDiscount.Text = string.Empty;
            txtVouValue.Text = string.Empty;
            pnlVoucherValue.Visible = false;
            pnlDiscType.Visible = false;
            pnlDiscount.Visible = false;
            txtVoucherDiscount.Text = string.Empty;
            txtVoucherValue.Text = string.Empty;
            pnlValidPeriod.Visible = false;
            txtValidPeriod.Text = string.Empty;
            pnlVoucherParameters.Visible = true;
            cmbParameterType.Focus();
            cmbParameterType.Select();
            cmbParameterType.SelectedIndex = -1;
        }

        private List<MasterItemSimilar> UpdateItemList(List<MasterItemSimilar> _items)
        {
            List<MasterItemSimilar> _updatedItemList = new List<MasterItemSimilar>();
            try
            {
                if (_items != null && _items.Count > 0)
                {
                    MasterItem _itemMaster = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _items.FirstOrDefault().Misi_sim_itm_cd);
                    if (_itemMaster != null && _itemMaster.Mi_itm_tp == "G")//check whether item is a GIFT VOUCHER
                    {
                        decimal tmpVoucherValue = 0;
                        if (cmbParameterType.Text == "DISCOUNT")
                        {
                            decimal.TryParse(txtVoucherDiscount.Text, out tmpVoucherValue);
                        }
                        else if (cmbParameterType.Text == "VALUE")
                        {
                            decimal.TryParse(txtVouValue.Text, out tmpVoucherValue);
                        }
                        else if (cmbParameterType.Text == "CURRENT PRICE")
                        {
                            decimal.TryParse(txtVouValue.Text, out tmpVoucherValue);
                        }

                        int _tmpVouValidPeriod = 0;
                        int.TryParse(txtValidPeriod.Text, out _tmpVouValidPeriod);

                        _items.ForEach(x =>
                        {
                            x.Misi_Itm_Tp = _itemMaster.Mi_itm_tp;
                            x.Misi_Vou_Tp = cmbParameterType.Text;
                            if (cmbParameterType.Text=="CURRENT PRICE")
                            {
                                x.Misi_Vou_Tp = "VALUE";
                            }
                            if (cmbParameterType.Text == "DISCOUNT")
                            {
                                if (rbdDiscValue.Checked ==true)
                                {
                                    x.Misi_Vou_Disc_Tp = "D";
                                }
                                else
                                { x.Misi_Vou_Disc_Tp = "R"; }
                            }
                            else if (cmbParameterType.Text == "CURRENT PRICE")
                            {
                                x.Misi_Vou_Disc_Tp = "V";
                            }
                            else
                            {
                                x.Misi_Vou_Disc_Tp = string.Empty;
                            }

                           // x.Misi_Vou_Disc_Tp = (cmbParameterType.Text == "DISCOUNT" && rbdDiscValue.Checked) ? "D" : (cmbParameterType.Text == "DISCOUNT" && rbdDiscountRate.Checked) ? "R" : string.Empty;
                            x.Misi_Vou_Value = tmpVoucherValue;
                            x.Misi_Vou_Val_Period = _tmpVouValidPeriod;
                            x.Misi_price_book = txt_p_book.Text.ToString().Trim();
                            x.Misi_price_leval = txt_p_level.Text.ToString().Trim();
                        });
                        _updatedItemList = _items;
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
            return _updatedItemList;
        }

        private void txtVoucherDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtVouValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtValidPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        //Akila 2018/02/08 - Restrict price upload for back dates
        private bool IsRestrictBackDatePriceUpload(DateTime _fromDate, DateTime _toDate)
        {
            bool _isRestrict = false;
            try
            {
                if (_fromDate.Date < DateTime.Today.Date)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16101))
                    {                        
                        MessageBox.Show(string.Format("Back date price upload option has been restricted for user - {0}(User Id - {1}).{2}Required permission - {3}", BaseCls.GlbUserName, BaseCls.GlbUserID, Environment.NewLine, "16101"), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }

                if (_toDate.Date < DateTime.Today.Date)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16101))
                    {
                        MessageBox.Show(string.Format("Back date price upload option has been restricted for user - {0}(User Id - {1}).{2}Required permission - {3}", BaseCls.GlbUserName, BaseCls.GlbUserID, Environment.NewLine, "16101"), "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                _isRestrict = true;
                throw;
            }
            return _isRestrict;
        }

    

        private void brn_serch_pbook_Click_1(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_p_book;
                _CommonSearch.ShowDialog();
                txt_p_book.Select();
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

        private void brn_serch_plevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_p_book.Text))
                {
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_p_book.Clear();
                    txt_p_book.Focus();
                    return;
                }
                if (chkAll.Checked)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllPBLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllPBLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPBLvl;
                    _CommonSearch.ShowDialog();
                    txtPBLvl.Select();
                }
                else
                {
                    _Stype = "PBDEF4";
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                    DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txt_p_level;
                    _CommonSearch.ShowDialog();
                    txt_p_level.Select();
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

        private void txt_p_book_Leave(object sender, EventArgs e)
        {
           

            if (!string.IsNullOrEmpty(txtLvlPB.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, txtLvlPB.Text);
                if (_dt.Rows.Count > 0)
                {
                    txt_p_book.Text = _dt.Rows[0]["sapb_desc"].ToString();

                }
                else
                {
                    MessageBox.Show("Invalid price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_p_book.Text = "";
                    txt_p_book.Text = "";
                    txt_p_book.Focus();
                }
            }
        }

        private void txt_p_level_Leave(object sender, EventArgs e)
        {
            if (! string.IsNullOrEmpty(txt_p_level.Text))
            {
                PriceBookLevelRef _tbl = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txt_p_book.Text.Trim(), txt_p_level.Text.Trim());
                if (_tbl.Sapl_pb_lvl_desc != null)
                {
                    txt_p_level.Text = _tbl.Sapl_erp_ref;

                }
                else
                {
                    MessageBox.Show("Please enter valid price level ", "Price level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_p_level.Clear();
                    txt_p_level.Focus();
                } 
            }
           
        }

        private void btn_get_price_Click(object sender, EventArgs e)
        {
            string txtUnitPrice = "";
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CS", txt_p_book.Text, txt_p_level.Text, "", txtMainItem.Text, Convert.ToDecimal(1), DateTime.Now);

            if (_priceDetailRef.Count <= 0)
            { 
             MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
             return;
            }
            if (_priceDetailRef != null && _priceDetailRef.Count > 0)
            {
                var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                if (_isSuspend > 0)
                {
                    MessageBox.Show("Price has been suspended. Please contact IT dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (_priceDetailRef.Count == 1)
            {
                var _one = from _itm in _priceDetailRef
                           select _itm;
                int _priceType = 0;
                foreach (var _single in _one)
                {
                    _priceType = _single.Sapd_price_type;
                    PriceTypeRef _promotion = TakePromotion(_priceType);
                    decimal UnitPrice = FigureRoundUp(TaxCalculation(txtMainItem.Text.Trim(), "GOD", Convert.ToDecimal(1), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(0), Convert.ToDecimal(0), false), true);
                    txtUnitPrice = FormatToCurrency(Convert.ToString(UnitPrice));
                }
            }
            Decimal amount = CHNLSVC.Inventory.Get_def_price_from_pc(txt_p_book.Text, txt_p_level.Text, txtMainItem.Text.ToString(), DateTime.Now);
           
                       
            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtMainItem.Text.Trim(), "GOD", Convert.ToDecimal(1), _priceBookLevelRef, Convert.ToDecimal(amount), Convert.ToDecimal(0), Convert.ToDecimal(0), true), true);
            txtVouValue.Text = FormatToCurrency(Convert.ToString(_vatPortion));
            #region CALCULATION
           
            string  txtTaxAmt  = FormatToCurrency(Convert.ToString(_vatPortion));

            decimal _totalAmount = Convert.ToDecimal(1) * Convert.ToDecimal(txtUnitPrice);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    bool _isVATInvoice = false;
                   

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        {
                            decimal _tmpUnitPrice = (_totalAmount + _vatPortion - _disAmt);
                            decimal _tmpVat = RecalculateTax(_tmpUnitPrice, _vatPortion, txtItem.Text.Trim(), "GOD", true);
                             txtTaxAmt = Convert.ToString(FigureRoundUp(_tmpVat, true));
                          
                        }
                    }

                   // txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt) - _disAmt, true);
                }

                // txtVouValue.Text = FormatToCurrency(Convert.ToString(_totalAmount));
                txtVouValue.Text = Convert.ToString(_totalAmount);

            #endregion


        }
        private decimal RecalculateTax(decimal _unitAmount, decimal _taxAmt, string _item, string _itmStatus, bool _isTaxfaction)
        {
            decimal _tax = 0;

            try
            {
                bool _isTaxExempted = false;
                bool _isCurrentDayTransaction = false;


                if (!_isTaxExempted)
                {
                    List<MasterItemTax> _taxDetails = new List<MasterItemTax>();
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                    if (_isCurrentDayTransaction)
                    {
                        if (_isTaxfaction == false)
                        {
                            if (_isStrucBaseTax == true)
                            {
                                _taxDetails = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _itmStatus, null, null, _mstItem.Mi_anal1);
                            }
                            else
                                _taxDetails = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _itmStatus);
                        }
                        else
                        {
                            if (_isStrucBaseTax == true)
                            {
                                _taxDetails = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxDetails = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT");
                        }
                    }
                    else
                    {
                        if (_isTaxfaction == false)
                        {
                            _taxDetails = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _itmStatus, DateTime.Now);
                        }
                        else
                        {
                            _taxDetails = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", DateTime.Now);
                        }

                        if (_taxDetails == null || _taxDetails.Count < 1)
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false)
                            {
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _itmStatus, DateTime.Now);
                            }
                            else
                            {
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", DateTime.Now);
                            }

                            if (_taxsEffDt != null && _taxsEffDt.Count > 0)
                            {
                                foreach (LogMasterItemTax logTax in _taxsEffDt)
                                {
                                    MasterItemTax _masterTax = new MasterItemTax();
                                    _masterTax.Mict_act = logTax.Lict_act;
                                    _masterTax.Mict_com = logTax.Lict_com;
                                    _masterTax.Mict_effct_dt = logTax.Lict_effect_dt;
                                    _masterTax.Mict_itm_cd = logTax.Lict_itm_cd;
                                    _masterTax.Mict_stus = logTax.Lict_stus;
                                    _masterTax.Mict_tax_cd = logTax.Lict_tax_cd;
                                    _masterTax.Mict_tax_rate = logTax.Lict_tax_rate;
                                    _masterTax.Mict_taxrate_cd = logTax.Lict_taxrate_cd;
                                    _taxDetails.Add(_masterTax);
                                }
                            }
                        }
                    }

                    if (_taxDetails != null && _taxDetails.Count > 0)
                    {
                        //calculate unit price without vat
                        var _vat = _taxDetails.Where(x => x.Mict_tax_cd == "VAT").SingleOrDefault();
                        if (_vat != null)
                        {
                            _tax += (_unitAmount * _vat.Mict_tax_rate) / (100 + _vat.Mict_tax_rate);//price with nbt
                        }

                        var _nbt = _taxDetails.Where(x => x.Mict_tax_cd == "NBT").SingleOrDefault();
                        if (_nbt != null)
                        {
                            _tax += ((_unitAmount - _tax) * _nbt.Mict_tax_rate) / (100 + _nbt.Mict_tax_rate);//price with nbt
                        }
                    }
                    else { _tax = _unitAmount; }
                }
                else { _tax = _unitAmount; }

                return _tax;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            decimal _returnValValue = 0;
            DateTime _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txt_p_book.Text, txt_p_level.Text);
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (DateTime.Now.Date == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                        {
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                        }
                        else
                        {
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                        }
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                                if (_isTaxfaction == false)
                                    if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                        _returnValValue = _pbUnitPrice;
                                    else
                                        _returnValValue += _pbUnitPrice * _one.Mict_tax_rate;
                              
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                        _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;

                                    }
                                    else
                                        _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;
                              
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                            _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, DateTime.Now.Date);
                        else
                            _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", DateTime.Now.Date);

                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                    if (_isTaxfaction == false)
                                        if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                            _returnValValue = _pbUnitPrice;
                                        else
                                            _returnValValue += _pbUnitPrice * _one.Mict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                            _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;

                                        }
                                        else
                                            _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false)
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, DateTime.Now.Date);
                            else
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", DateTime.Now.Date);

                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                    if (_isTaxfaction == false)
                                        if (_isStrucBaseTax == true)    //kapila 9/2/2017
                                            _returnValValue = _pbUnitPrice;
                                        else
                                            _returnValValue += _pbUnitPrice * _one.Lict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                            _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Lict_tax_rate / 100) * _qty;

                                        }
                                        else
                                            _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Lict_tax_rate / 100) * _qty;
                            }
                        }
                    }
                }
                else
                {
                    _returnValValue = _pbUnitPrice;
                    if (_isTaxfaction) _returnValValue = 0;
                }


            return _returnValValue;
        }
        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            //else return RoundUpForPlace(value, 2);
            else return Math.Round(value, 2);
        }
        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;
            }
            return _list;
        }
    }
}
