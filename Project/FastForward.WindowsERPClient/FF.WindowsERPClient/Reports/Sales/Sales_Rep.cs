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
using FF.WindowsERPClient.Reports.Sales;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;


//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Sales
{
    public partial class Sales_Rep : Base
    {
        clsSalesRep objSales = new clsSalesRep();
        private string _userCompany = "";
        private Boolean _isMultiAdminTeam = false;

        public Sales_Rep()
        {
            InitializeComponent();
            InitializeEnv();
            GetCompanyDet(null, null);
            GetPCDet(null, null);
            setFormControls(0);
            _userCompany = BaseCls.GlbUserComCode;
            BindAdminTeam();
        }

        private void setFormControls(Int32 _index)
        {
            //Updated by akila 2018/015
            lstGroup.Clear();
            pnlSpecial.Visible = false;

            pnlNationality.Visible = false;
            pnlProvince.Visible = false;
            pnlDistrict.Visible = false;
            PnlCity.Visible = false;

            pnlDebt.Enabled = false;
            pnlOuts.Enabled = false;
            pnlAsAtDate.Enabled = false;
            pnlDateRange.Enabled = true;
            pnl_Direc.Enabled = false;
            pnl_DocNo.Enabled = false;
            pnl_DocSubType.Enabled = false;
            pnl_DocType.Enabled = false;
            pnl_Entry_Tp.Enabled = false;
            pnl_Rec_Tp.Enabled = false;
            pnl_Item.Enabled = false;
            pnlCust.Enabled = false;
            pnlDelivery.Enabled = false;
            pnlCust.Enabled = false;
            pnlExec.Enabled = false;
            pnl_Itm_Stus.Enabled = false;
            pnlPrefix.Enabled = false;
            pnlPayType.Enabled = false;
            pnl_PB.Visible = false;
            pnl_PB.Enabled = false;
            pnl_PBLevel.Visible = false;
            pnl_PBLevel.Enabled = false;
            pnlDiscRate.Visible = false;
            pnlDiscRate.Enabled = false;
            label41.Text = "Discount Rate";
            pnlStus.Enabled = false;
            pnlPO.Visible = false;
            pnlPO.Enabled = false;
            pnlSup.Visible = false;
            pnlSup.Enabled = false;
            pnlCustType.Enabled = false;
            pnlLoyTp.Enabled = false;
            pnl_approved.Enabled = false;
            pnl_approved.Visible = false;
            pnl_FOC.Visible = false;
            pnl_FOC.Enabled = false;
            pnl_jobno.Visible = false;
            pnl_jobno.Enabled = false;
            pnl19.Visible = false;
            panel1.Enabled = true;
            chkAsAtDate.Text = "Run as at Date";
            pnl_Location.Enabled = false;
            pnl_Location.Visible = false;

            lbl1.Enabled = false;
            lbl2.Enabled = false;
            lbl3.Enabled = false;
            lbl4.Enabled = false;
            lbl5.Enabled = false;
            lbl6.Enabled = false;
            lbl7.Enabled = false;
            lbl8.Enabled = false;
            lbl9.Enabled = false;
            lbl10.Enabled = false;
            lbl11.Enabled = false;
            lbl13.Enabled = false;
            lbl14.Enabled = false;
            lbl16.Enabled = false;

            lstCat1.Enabled = false;
            lstCat2.Enabled = false;
            lstCat3.Enabled = false;
            lstItem.Enabled = false;
            lstBrand.Enabled = false;
            btnCat1.Enabled = false;
            btnCat2.Enabled = false;
            btnCat3.Enabled = false;
            btnItem.Enabled = false;
            btnBrand.Enabled = false;
            label18.Text = "Document No";
            label16.Text = "Direction";
            label40.Text = "Pay Type";
            optDeliver.Text = "With Delivered Sales";
            optForward.Text = "With Forward Sales";
            chk_Ord.Text = "Order by Sales Value";
            chkAsAtDate.Visible = false;
            txtAsAtDate.Enabled = true;
            cmbExeType.Visible = false;
            comboBoxDocType.Visible = false;
            pnl_Export.Visible = false;
            pnl_Export.Enabled = false;
            chk_Export.Visible = false;
            chk_Ord.Visible = false;
            pnl_promotor.Visible = false;
            txtPO.Visible = true;
            btn_Srch_PO.Visible = true;
            label10.Text = "PO #";
            label41.Text = "Discount Rate";
            chkWithComm.Checked = false;
            chkWithComm.Enabled = false;
            optOutsAll.Visible = true;
            optOuts.Visible = true;
            chkWithComm.Text = "With Commission";
            chkAsAtDate.Text = "Run as at Date";
            pnl_month_year.Enabled = false;
            pnl_month_year.Visible = false;
            txtDocType.Text = "";
            pnl_supplier.Visible = false;
            pnl_supplier.Enabled = false;
            pnl_sum.Visible = false;
            pnl_sum.Enabled = false;
            txtToDate.Enabled = true;
            pnl_color.Visible = false;
            pnl_color.Enabled = false;
            pnl_size.Visible = false;
            pnl_size.Enabled = false;
            pnl_intercompany.Visible = false;
            pnlDateRange2.Visible = false;

            chkRepModel.Enabled = false;
            // pnlFOC.Visible = false;
            pnlRegReport.Visible = false;
            chkAllComp.Enabled = false;
            _isMultiAdminTeam = false;

            comboBoxPayModes.DataSource = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            comboBoxPayModes.DisplayMember = "SAPT_DESC";
            comboBoxPayModes.ValueMember = "SAPT_CD";
            comboBoxPayModes.SelectedIndex = -1;
            cmbExeType.SelectedIndex = -1;
            comboBoxDocType.SelectedIndex = -1;
            pnlDateRange.Visible = true;
            chkAllComp.Visible = false;
            if (BaseCls.GlbUserComCode == "ABL")
            {
                chkAllComp.Text = "LRP";
                chkAllComp.Visible = true;
            }
            if (BaseCls.GlbUserComCode == "SGL")
            {
                chkAllComp.Text = "SGD";
                chkAllComp.Visible = true;
            }

            chkDolar.Enabled = false;

            pnl_isdateandtime.Visible = false;
            pnldatewithtime.Visible = false;
            pnlDateRange.Visible = true;
            pnl_SaleQty.Visible = false;
            txtSQtyTo.Clear();
            txtSQtyFrom.Clear();

            switch (_index)
            {

                case 24:
                    {
                        //Updated by akila 2018/015
                        pnlSpecial.Visible = true;
                        pnlNationality.Visible = true;
                        pnlProvince.Visible = true;
                        pnlDistrict.Visible = true;
                        PnlCity.Visible = true;

                        //pnlItem.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_promotor.Enabled = true;
                        pnl_promotor.Visible = true;
                        pnl_FOC.Visible = true;
                        opt_FOC_No.Checked = true;
                        pnl_FOC.Enabled = true;
                        pnl_jobno.Visible = true;
                        pnl_jobno.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        chk_Export.Enabled = true;
                        chk_Ord.Visible = true;

                        lbl1.Enabled = true;
                        lbl2.Enabled = true;
                        lbl3.Enabled = true;
                        lbl4.Enabled = true;
                        lbl5.Enabled = true;
                        lbl6.Enabled = true;
                        lbl7.Enabled = true;
                        lbl8.Enabled = true;
                        lbl9.Enabled = true;
                        lbl10.Enabled = true;
                        lbl11.Enabled = true;
                        lbl13.Enabled = true;
                        lbl14.Enabled = true;
                        lbl16.Enabled = true;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        pnl_color.Visible = true;
                        pnl_color.Enabled = true;
                        pnl_size.Visible = true;
                        pnl_size.Enabled = true;
                        break;
                    }
                case 25:
                    {
                        //Updated by akila 2018/015
                        pnlSpecial.Visible = true;
                        pnlNationality.Visible = true;
                        pnlProvince.Visible = true;
                        pnlDistrict.Visible = true;
                        PnlCity.Visible = true;

                        //pnlItem.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_promotor.Enabled = true;
                        pnl_promotor.Visible = true;
                        pnl_FOC.Visible = true;
                        opt_FOC_No.Checked = true;
                        pnl_FOC.Enabled = true;
                        pnl_jobno.Visible = true;
                        pnl_jobno.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        chk_Export.Enabled = true;
                        chk_Ord.Visible = true;

                        lbl1.Enabled = true;
                        lbl2.Enabled = true;
                        lbl3.Enabled = true;
                        lbl4.Enabled = true;
                        lbl5.Enabled = true;
                        lbl6.Enabled = true;
                        lbl7.Enabled = true;
                        lbl8.Enabled = true;
                        lbl9.Enabled = true;
                        lbl10.Enabled = true;
                        lbl11.Enabled = true;
                        lbl13.Enabled = true;
                        lbl16.Enabled = true;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        pnl_color.Visible = true;
                        pnl_color.Enabled = true;
                        pnl_size.Visible = true;
                        pnl_size.Enabled = true;
                        chkDolar.Enabled = true;
                        break;
                    }
                case 74:
                    {
                        //pnlItem.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        //pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_promotor.Enabled = true;
                        pnl_promotor.Visible = true;
                        pnl_FOC.Visible = true;
                        opt_FOC_No.Checked = true;
                        pnl_FOC.Enabled = true;

                        lbl1.Enabled = true;
                        lbl2.Enabled = true;
                        lbl3.Enabled = true;
                        lbl4.Enabled = true;
                        lbl5.Enabled = true;
                        lbl6.Enabled = true;
                        lbl7.Enabled = true;
                        lbl8.Enabled = true;
                        lbl9.Enabled = true;
                        lbl10.Enabled = true;
                        lbl11.Enabled = true;
                        //lbl13.Enabled = true;
                        lbl14.Enabled = true;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        //chkAsAtDate.Text = "Month Wise";
                        //chkAsAtDate.Enabled = true;
                        //chkAsAtDate.Visible = true;
                        //pnlAsAtDate.Enabled = true;
                        //txtAsAtDate.Enabled = false;
                        pnl_month_year.Enabled = true;
                        pnl_month_year.Visible = true;
                        pnl_sum.Visible = false;
                        pnlOuts.Visible = false;
                        pnlDiscRate.Visible = false;
                        break;
                    }
                case 75:
                    {
                        //pnlItem.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        //pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_promotor.Enabled = true;
                        pnl_promotor.Visible = true;
                        pnl_FOC.Visible = true;
                        opt_FOC_No.Checked = true;
                        pnl_FOC.Enabled = true;

                        lbl1.Enabled = true;
                        lbl2.Enabled = true;
                        lbl3.Enabled = true;
                        lbl4.Enabled = true;
                        lbl5.Enabled = true;
                        lbl6.Enabled = true;
                        lbl7.Enabled = true;
                        lbl8.Enabled = true;
                        lbl9.Enabled = true;
                        lbl10.Enabled = true;
                        lbl11.Enabled = true;
                        //lbl13.Enabled = true;
                        lbl14.Enabled = true;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        pnl_month_year.Enabled = true;
                        pnl_month_year.Visible = true;
                        pnl_sum.Visible = false;
                        pnlOuts.Visible = false;
                        pnlDiscRate.Visible = false;

                        //chkAsAtDate.Text = "Month Wise";
                        //chkAsAtDate.Enabled = true;
                        //chkAsAtDate.Visible = true;
                        //pnlAsAtDate.Enabled = true;
                        //txtAsAtDate.Enabled = false;
                        break;
                    }
                case 77:
                    {
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_promotor.Enabled = true;
                        pnl_promotor.Visible = true;
                        pnl_FOC.Visible = true;
                        opt_FOC_No.Checked = true;
                        pnl_FOC.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;

                        //lbl1.Enabled = true;
                        //lbl2.Enabled = true;
                        //lbl3.Enabled = true;
                        //lbl4.Enabled = true;
                        //lbl5.Enabled = true;
                        //lbl6.Enabled = true;
                        //lbl7.Enabled = true;
                        //lbl8.Enabled = true;
                        //lbl9.Enabled = true;
                        //lbl10.Enabled = true;
                        //lbl11.Enabled = true;
                        //lbl13.Enabled = true;
                        //lbl14.Enabled = true;

                        //lstCat1.Enabled = true;
                        //lstCat2.Enabled = true;
                        //lstCat3.Enabled = true;
                        //lstItem.Enabled = true;
                        //lstBrand.Enabled = true;

                        //btnCat1.Enabled = true;
                        //btnCat2.Enabled = true;
                        //btnCat3.Enabled = true;
                        //btnItem.Enabled = true;
                        //btnBrand.Enabled = true;
                        break;
                    }
                case 30:
                    {
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        break;
                    }
                case 31:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlDiscRate.Visible = true;
                        pnlDiscRate.Enabled = true;
                        pnl_sum.Visible = false;
                        pnlOuts.Visible = false;
                        pnl_month_year.Visible = false;
                        label41.Text = "Age";
                        pnlDelivery.Enabled = true;
                        pnlCust.Visible = true;
                        pnlCust.Enabled = true;
                        optDeliver.Text = "Normal";
                        optForward.Text = "Export to Excel Format";
                        chk_Ord.Text = "With Cost";
                        BaseCls.GlbReportWithCost = Convert.ToInt16((CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075)) ? 1 : 0);
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = false;
                        if (BaseCls.GlbReportWithCost == 1)
                        {
                            chk_Ord.Enabled = true;
                            chk_Ord.Visible = true;
                        }
                        else
                        {
                            chk_Ord.Enabled = false;
                            chk_Ord.Visible = false;
                        }

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        break;
                    }
                case 32:
                    {
                        break;
                    }
                case 1:
                    {
                        break; // Added by Udesh 26-Oct-2018
                    }
                case 2:
                case 3:
                    {
                        pnl_DocType.Enabled = true;
                        break;
                    }
                case 13:
                    {
                        pnl_Rec_Tp.Enabled = true;
                        pnlPrefix.Enabled = true;
                        pnlStus.Visible = false;
                        pnlPayType.Enabled = true;
                        pnl_isdateandtime.Visible = true;
                        pnl_isdateandtime.Enabled = true;
                        rdiisdate.Checked = true;
                        break;
                    }
                case 16:
                    {
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Ord.Visible = true;
                        break;
                    }
                case 19:
                    {
                        pnlDebt.Enabled = true;
                        pnlOuts.Enabled = false;
                        pnlCust.Enabled = true;
                        pnl19.Visible = true;
                        pnlRegReport.Visible = false;
                        pnl_FOC.Visible = false;
                        pnlDelivery.Visible = false;

                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;

                        break;
                    }

                case 17:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = true;
                        txtToDate.Enabled = false;
                        chkAsAtDate.Visible = true;
                        chkAsAtDate.Enabled = true;
                        chkAsAtDate.Text = "Use From Date";
                        pnlDebt.Enabled = true;
                        pnlOuts.Enabled = true;
                        pnlCust.Enabled = true;
                        break;
                    }
                case 33:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;

                        pnlDelivery.Enabled = true;
                        break;
                    }
                case 34:
                    {
                        pnlDateRange.Enabled = true;
                        pnlDiscRate.Visible = true;
                        pnlDiscRate.Enabled = true;
                        pnl_sum.Visible = false;
                        pnlOuts.Visible = false;
                        pnl_month_year.Visible = false;
                        txtDiscRate.Enabled = true;
                        cmbExeType.Visible = true;
                        cmbExeType.Enabled = true;
                        pnlPayType.Enabled = true;
                        pnlDelivery.Enabled = true;
                        pnl_approved.Enabled = true;
                        pnl_approved.Visible = true;
                        label40.Text = "Exec. Type";
                        cmbExeType.SelectedIndex = -1;
                        break;
                    }
                case 36:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 37:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 38:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 39:
                    {
                        txtPC.Text = string.Empty; //updated by akila 2017/09/08
                        pnlDateRange.Enabled = true;
                        label18.Text = "Circular No";
                        pnl_DocNo.Enabled = true;
                        label16.Text = "Promo Code";
                        pnl_Direc.Enabled = true;
                        break;
                    }

                case 40:
                    {
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 41:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 42:
                    {
                        pnlDateRange.Enabled = true;
                        label18.Text = "Circular No";
                        pnl_DocNo.Enabled = true;
                        label16.Text = "Promo Code";
                        pnl_Direc.Enabled = true;
                        chkAsAtDate.Visible = true;
                        txtAsAtDate.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        label40.Text = "Price Type";
                        pnlPayType.Enabled = true;
                        pnlCust.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PB.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnl_PBLevel.Enabled = true;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;

                        comboBoxPayModes.DataSource = CHNLSVC.Sales.GetAllRepPriceType();
                        comboBoxPayModes.DisplayMember = "SARPT_DESC";
                        comboBoxPayModes.ValueMember = "sarpt_cd";
                        comboBoxPayModes.SelectedIndex = -1;
                        break;
                    }
                case 46:
                    {
                        pnl_Item.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;

                        break;
                    }
                case 22:
                    {
                        comboBoxDocType.Visible = true;
                        pnl_Entry_Tp.Enabled = true;
                        comboBoxDocType.Enabled = true;
                        comboBoxDocType.SelectedIndex = -1;
                        break;
                    }
                case 50:
                    {
                        comboBoxDocType.Visible = true;
                        pnl_Entry_Tp.Enabled = true;
                        comboBoxDocType.Enabled = true;
                        comboBoxDocType.SelectedIndex = -1;
                        break;
                    }
                case 47:
                    {
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnlPO.Visible = true;
                        pnlPO.Enabled = true;
                        pnlSup.Visible = true;
                        pnlSup.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = false;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        break;
                    }
                case 48:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlStus.Visible = true;
                        pnlStus.Enabled = true;
                        pnlDiscRate.Visible = false;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 49:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 51:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlPayType.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        break;
                    }
                case 52:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 53:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }

                case 54:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }

                case 55:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }

                case 56:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }


                case 57:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 62:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlPayType.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        break;
                    }
                case 63:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnlDebt.Enabled = true;
                        optPC.Checked = true;
                        pnlOuts.Enabled = true;
                        optOuts.Checked = true;
                        chkWithComm.Enabled = true;
                        pnlCust.Enabled = true;
                        break;
                    }
                case 64:
                    {
                        pnlDateRange.Enabled = true;
                        pnlPayType.Enabled = true;
                        pnlExec.Enabled = true;
                        break;
                    }
                case 65:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;

                        break;
                    }
                case 66:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 67:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlCustType.Enabled = true;
                        pnlLoyTp.Enabled = true;
                        break;
                    }
                case 69:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        label18.Text = "Circular No";
                        pnl_DocNo.Enabled = true;
                        chk_Doc.Checked = false;
                        pnlPO.Enabled = true;
                        pnlPO.Visible = true;
                        txtPO.Visible = false;
                        btn_Srch_PO.Visible = false;
                        label10.Text = "Circular Like (%)";
                        break;
                    }
                case 70:
                    {
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnlDiscRate.Enabled = true;
                        pnlDiscRate.Visible = true;
                        pnl_sum.Visible = false;
                        pnlOuts.Visible = false;
                        pnl_month_year.Visible = false;
                        label41.Text = "Accumilated %";

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        break;
                    }
                case 71:
                    {
                        pnlDateRange.Enabled = true;
                        label18.Text = "Circular No";
                        pnl_DocNo.Enabled = true;
                        label16.Text = "Promo Code";
                        pnl_Direc.Enabled = true;
                        chkAsAtDate.Visible = true;
                        txtAsAtDate.Enabled = false;
                        pnlAsAtDate.Enabled = true;
                        label40.Text = "Price Type";
                        pnlPayType.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PB.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnl_PBLevel.Enabled = true;
                        pnlOuts.Visible = true;
                        pnlOuts.Enabled = true;
                        optOutsAll.Visible = false;
                        optOuts.Visible = false;
                        chkWithComm.Text = "Filter Location";
                        chkWithComm.Checked = false;
                        chkWithComm.Enabled = true;
                        pnl_sum.Visible = false;
                        pnlDiscRate.Visible = false;
                        pnl_month_year.Visible = false;

                        comboBoxPayModes.DataSource = CHNLSVC.Sales.GetAllRepPriceType();
                        comboBoxPayModes.DisplayMember = "SARPT_DESC";
                        comboBoxPayModes.ValueMember = "sarpt_cd";
                        comboBoxPayModes.SelectedIndex = -1;
                        break;
                    }
                case 72:
                    {
                        pnlRegReport.Visible = true;
                        pnlDateRange.Enabled = true;
                        cmbYear.Enabled = false;
                        cmbMonth.Enabled = false;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        break;
                    }
                case 76:
                    {
                        pnlDateRange.Enabled = true;
                        txtDocType.Text = "HS";
                        //pnl_DocType.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        break;
                    }
                case 79:
                    {
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 80:
                    {

                        // pnl_promotor.Enabled = true;
                        //pnlAsAtDate.Enabled = false;
                        //pnlDateRange.Enabled = false;
                        //panel1.Enabled = false;
                        break;
                    }
                case 82:
                    {
                        break;
                    }
                case 83:
                    {
                        pnl_Item.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlCust.Visible = true;
                        chkAsAtDate.Visible = true;
                        txtAsAtDate.Enabled = false;
                        pnlAsAtDate.Enabled = true;
                        chkAsAtDate.Text = "With Cost";
                        break;
                    }
                case 84:
                    {
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        break;
                    }
                case 85:
                    {
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_promotor.Enabled = true;
                        pnl_promotor.Visible = true;
                        pnl_FOC.Visible = true;
                        opt_FOC_No.Checked = true;
                        pnl_FOC.Enabled = true;
                        pnl_supplier.Visible = true;
                        pnl_supplier.Enabled = true;
                        pnl_sum.Enabled = true;

                        pnl_sum.Visible = true;
                        pnlOuts.Visible = false;
                        pnlDiscRate.Visible = false;
                        pnl_month_year.Visible = false;
                        break;
                    }
                case 86:
                    {
                        pnlPO.Visible = true;
                        pnlPO.Enabled = true;
                        break;
                    }
                case 87:
                    {
                        break;
                    }
                case 88:
                    {
                        pnl_Item.Enabled = true;
                        pnlCat1.Enabled = true;
                        pnlCat2.Enabled = true;
                        pnlCat3.Enabled = true;
                        pnlItemList.Enabled = true;
                        pnlBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnlPO.Visible = true;
                        pnlPO.Enabled = true;
                        pnlSup.Visible = true;
                        pnlSup.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;

                        lstCat1.Enabled = true;
                        lstCat2.Enabled = true;
                        lstCat3.Enabled = true;
                        lstItem.Enabled = true;
                        lstBrand.Enabled = true;

                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        break;
                    }
                case 89:
                    {
                        break;
                    }
                case 90:
                    {
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 91:
                    {
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        pnl_Export.Enabled = true;
                        pnl_Export.Visible = true;
                        pnlDebt.Visible = false;
                        pnlDisc.Enabled = true;

                        break;
                    }

                case 92:
                    {
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        // pnl_FOC.Visible = true;
                        pnlCust.Enabled = true;
                        pnlExec.Enabled = true;
                        //  pnl_FOC.Enabled = true;
                        lbl4.Enabled = true;
                        lbl1.Enabled = true;
                        lbl11.Enabled = true;
                        chkAllComp.Enabled = true;
                        _isMultiAdminTeam = true;
                        break;
                    }
                case 93:
                    {
                        pnl_Location.Enabled = true;
                        pnl_Location.Visible = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 94:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 96:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 98:
                    {
                        pnlDateRange.Enabled = false;
                        pnlAsAtDate.Enabled = true;
                        break;
                    }
                case 99:
                    {
                        pnlDateRange.Enabled = true;
                        chkRepModel.Visible = true;
                        chkRepModel.Enabled = true;
                        pnl_intercompany.Visible = true;
                        pnl_intercompany.Enabled = true;
                        pnlDateRange2.Visible = true;
                        //pnl_FOC.Visible = true;
                        //pnl_FOC.Enabled = true;
                        chkAllComp.Enabled = true;
                        pnl_Item.Enabled = true;
                        _isMultiAdminTeam = true;
                        break;
                    }

                //added by tharindu 2017-11-08
                case 101:
                    {
                        // enable from & to date
                        label7.Enabled = true;
                        label8.Enabled = true;
                        txtFromDate.Enabled = true;
                        txtToDate.Enabled = true;

                        pnl_Item.Enabled = true;

                        // enable category 1
                        label29.Enabled = true;
                        txtIcat1.Enabled = true;
                        btn_Srch_Cat1.Enabled = true;
                        chk_ICat1.Enabled = true;

                        // enable category 2
                        label28.Enabled = true;
                        txtIcat2.Enabled = true;
                        btn_Srch_Cat2.Enabled = true;
                        chk_ICat2.Enabled = true;

                        // enable category 3
                        label27.Enabled = true;
                        txtIcat3.Enabled = true;
                        btn_Srch_Cat3.Enabled = true;
                        chk_ICat3.Enabled = true;

                        // enable category 4
                        label53.Enabled = true;
                        txtIcat4.Enabled = true;
                        btn_Srch_Cat4.Enabled = true;
                        chk_ICat4.Enabled = true;

                        // enable category 5
                        label55.Enabled = true;
                        txtIcat5.Enabled = true;
                        btn_Srch_Cat5.Enabled = true;
                        chk_ICat5.Enabled = true;

                        // enable model
                        label24.Enabled = true;
                        txtModel.Enabled = true;
                        btn_Srch_Model.Enabled = true;
                        chk_Model.Enabled = true;

                        // enable brand
                        label25.Enabled = true;
                        txtBrand.Enabled = true;
                        btn_Srch_Brnd.Enabled = true;
                        chk_Brand.Enabled = true;

                        //panel1.Enabled = true;
                        //pnlAsAtDate.Enabled = true;
                        //pnlDateRange.Enabled = false;

                        panel1.Visible = true;
                        pnlAsAtDate.Visible = true;


                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = true;

                        chkAsAtDate.Visible = true;
                        chkAsAtDate.Enabled = true;
                        //chkAsAtDate.Text = "Use From Date";
                        // pnlDebt.Enabled = true;
                        //pnlOuts.Enabled = true;

                        //if (chkAsAtDate.Checked)
                        //{
                        //    pnlDateRange.Enabled = false;
                        //}
                        //else
                        //{
                        //    pnlDateRange.Enabled = true;
                        //    pnlAsAtDate.Enabled = false;
                        //}

                        pnl_PB.Visible = true;
                        pnl_PB.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnl_PBLevel.Enabled = true;


                        break;
                    }

                //added by tharindu 2018-03-13
                case 102:
                    {
                        // enable from & to date
                        label7.Enabled = true;
                        label8.Enabled = true;
                        txtFromDate.Enabled = true;
                        txtToDate.Enabled = true;

                        break;
                    }
                case 103:
                    {
                        pnlDateRange.Enabled = true;
                        chkAllComp.Enabled = true;
                        _isMultiAdminTeam = true;
                        break;
                    }
                case 104:
                    {
                        pnlDateRange.Enabled = true;
                        chkAllComp.Enabled = true;
                        _isMultiAdminTeam = true;
                        break;
                    }

                case 105:
                    {

                        pnlDateRange.Enabled = true;
                        chkAllComp.Enabled = true;
                        _isMultiAdminTeam = true;
                        break;
                    }

                case 106:
                    {

                        pnlDateRange.Enabled = true;
                        chkAllComp.Enabled = true;
                        _isMultiAdminTeam = true;
                        pnl_SaleQty.Visible = true;
                        pnl_SaleQty.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
            }
        }

        protected void GetCompanyDet(object sender, EventArgs e)
        {

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
            if (_masterComp != null)
            {
                txtCompDesc.Text = _masterComp.Mc_desc;
                txtCompAddr.Text = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }
            else
            {
                txtCompDesc.Text = "";
                txtCompAddr.Text = "";
            }
        }

        protected void GetPCDet(object sender, EventArgs e)
        {

            MasterProfitCenter _masterPC = null;
            _masterPC = CHNLSVC.General.GetPCByPCCode(txtComp.Text, txtPC.Text);
            if (_masterPC != null)
            {
                txtPCDesn.Text = _masterPC.Mpc_desc;
            }
            else
            {
                txtPCDesn.Text = "";
            }
        }

        private void InitializeEnv()
        {

            txtComp.Text = BaseCls.GlbUserComCode;
            txtPC.Text = BaseCls.GlbUserDefProf;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");

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

            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtAsAtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;


        }

        private void update_PC_List()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                List<string> tmpList = new List<string>();
                tmpList = Item.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                string pc = null;
                string com = txtComp.Text;
                if ((tmpList != null) && (tmpList.Count > 0))
                {
                    pc = tmpList[0];
                    com = tmpList[1];
                }

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {

            btnNone.Focus();
            try
            {
                BaseCls.GlbReportCountry = string.Empty;
                BaseCls.GlbReportProvince = string.Empty;
                BaseCls.GlbReportDistrict = string.Empty;
                BaseCls.GlbReportCity = string.Empty;

                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;

                //check whether current session is expired
                CheckSessionIsExpired();

                //kapila 4/7/2014
                if (CheckServerDateTime() == false) return;

                //check this user has permission for this PC
                if (txtPC.Text != string.Empty)
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPC.Text);
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Profit Center.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "REPS"))
                    //Add by Chamal 30-Aug-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
                    {
                        Int16 is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                        if (is_Access != 1)
                        {
                            //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10044)", "Sales Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                BaseCls.GlbReportFromDate2 = Convert.ToDateTime(txtFromDate2.Value).Date;
                BaseCls.GlbReportToDate2 = Convert.ToDateTime(txtToDate2.Value).Date;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;

                btnDisplay.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                //if (opt26.Checked == true)     //price variation (kapila)
                //{
                //    //update temporary table
                //    update_PC_List();

                //    GlbReportName = "Price/Commission Variation";


                //    BaseCls.GlbReportCompCode = txtComp.Text;
                //    BaseCls.GlbReportComp = txtCompDesc.Text;
                //    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                //    ReportViewer _view = new ReportViewer();
                //    _view.GlbReportName = "variation.rpt";
                //    BaseCls.GlbReportName = "variation.rpt";
                //    _view.Show();
                //    _view = null;

                //}

                if (opt6.Checked == true)     //remitance detail (kapila)
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    GlbReportName = "Remitance Details";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "remitance_det.rpt";
                    BaseCls.GlbReportName = "remitance_det.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt7.Checked == true)     //SOS (kapila)
                {
                    //check whether date range is valid
                    Int32 _ok = CHNLSVC.Financial.IsValidWeekDataRange(Convert.ToInt32(cmbYear.Text), cmbMonth.SelectedIndex + 1, Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date, BaseCls.GlbUserComCode);
                    if (_ok == 0)
                    {
                        MessageBox.Show("Invalid Date Range !", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    GlbReportName = "Summary of Sales";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "SOS.rpt";
                    BaseCls.GlbReportName = "SOS.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt13.Checked == true)   //receipt listing
                {
                    string _recType = null;
                    string _prefix = null;
                    string _payType = null;
                    int isdateandtime = 0;
                    if (chkRecType.Checked == false && string.IsNullOrEmpty(txtRecType.Text))
                    {
                        MessageBox.Show("Select receipt type.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                    //update temporary table
                    update_PC_List_RPTDB();

                    GlbReportName = "Receipt Listing";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    if (chkRecType.Checked == false)
                    {
                        _recType = txtRecType.Text;
                    }
                    if (chkPrefix.Checked == false)
                    {
                        _prefix = txtPrefix.Text;
                    }
                    if (chk_Pay_Tp.Checked == false)
                    {
                        if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { MessageBox.Show("Please select the payment type"); return; }
                        _payType = comboBoxPayModes.SelectedValue.ToString();
                    }
                    if (rdiisdate.Checked == true)
                    {
                        isdateandtime = 0;
                        BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                        BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                        if (BaseCls.GlbReportFromDate > BaseCls.GlbReportToDate)
                        {
                            MessageBox.Show("Invalid Data Range.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    else
                    {
                        isdateandtime = 1;
                        BaseCls.GlbReportFromDate = Convert.ToDateTime(txtfrmdatewithtime.Text);
                        BaseCls.GlbReportToDate = Convert.ToDateTime(txttodatewithtime.Text);
                        if (BaseCls.GlbReportFromDate > BaseCls.GlbReportToDate)
                        {
                            MessageBox.Show("Invalid Data and time Range.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }
                    }

                    BaseCls.GlbReportnoofDays = isdateandtime;

                    BaseCls.GlbRecType = _recType;
                    BaseCls.GlbPrefix = _prefix;
                    BaseCls.GlbPayType = _payType;
                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "Receipt_List.rpt";
                    BaseCls.GlbReportName = "Receipt_List.rpt";
                    _view.Show();
                    _view = null;
                    BaseCls.GlbReportName = "Receipt Listing Summary";
                    GlbReportName = "Receipt Listing Summary";
                    ReportViewer _viewnew = new ReportViewer();
                    _viewnew.GlbReportName = "Receipt_List_summary.rpt";
                    BaseCls.GlbReportName = "Receipt_List_summary.rpt";
                    _viewnew.Show();
                    _viewnew = null;


                }

                if (opt5.Checked == true)       //remitance summary
                {
                    DateTime _fromDate;
                    Boolean _dayEndNotFound = false;

                    DateTime start = BaseCls.GlbReportFromDate;
                    DateTime finish = BaseCls.GlbReportToDate;

                    //kapila  30/11/2015
                    if (CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbReportToDate, BaseCls.GlbReportToDate) == false)
                        if (CHNLSVC.Financial.IsDayEndDone_win(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate) == true)
                        {
                            MessageBox.Show("You cannot generate this report for this date period.\nDay end is not done for the " + BaseCls.GlbReportToDate.ToShortDateString(), "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    //kapila 29/11/2013
                    Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(BaseCls.GlbUserComCode, 1);
                    if (_rptPrd != 0)
                    {
                        if ((finish - start).Days > _rptPrd)
                        {
                            MessageBox.Show("you cannot generate this report for this date period.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }
                    }

                    //check whether txns found after process day end for the same day -(13/3/2013)
                    if (CHNLSVC.Financial.IsTxnFoundAfterDayEnd(txtComp.Text, txtPC.Text, start, "") == true)
                    {
                        BaseCls.GlbStatus = "Transactions found after process last day end. Need to Re-Process the day end";
                    }
                    else
                    {
                        BaseCls.GlbStatus = "";
                    }

                    //check whether day end is done or not for the selected date period
                    for (DateTime x = start; x <= finish; x = x.AddDays(1))
                    {
                        if (CHNLSVC.Financial.IsDayEndDone(txtComp.Text, txtPC.Text, x, x) == false)
                        {
                            //_dayEndNotFound = true;      //4/5/2013
                            //BaseCls.GlbStatus = " ";     //4/5/2013   
                            //check whether invoices/receipts/remitance found
                            if (CHNLSVC.Financial.IsPrvDayTxnsFound(txtComp.Text, txtPC.Text, x) == true)
                            {
                                _dayEndNotFound = true;
                                BaseCls.GlbStatus = "Day end is not done for some date(s) between this date range";
                                break;
                            }
                            else
                            {
                                if (finish < Convert.ToDateTime(DateTime.Now).Date)         //24/7/2013
                                {
                                    Decimal _weekNo = 0;
                                    _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(x).Date, out _weekNo, BaseCls.GlbUserComCode);
                                    Int32 xx = CHNLSVC.Financial.UpdatePrvDayCIH(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, Convert.ToDateTime(x).Date, Convert.ToInt32(_weekNo));
                                }
                            }
                        }
                    }

                    //check whether HO finalized or not  13/12/2013
                    if (CHNLSVC.Financial.IsHOFinalized(txtComp.Text, txtPC.Text, finish) == 0)
                    {
                        BaseCls.GlbReportStrStatus = "Head Office Accounting Period not Finalized";
                    }
                    else
                    {
                        BaseCls.GlbReportStrStatus = "";
                    }

                    //13/11/2012
                    int D = CHNLSVC.Financial.GetFirstDayEndDate(txtComp.Text, txtPC.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, out _fromDate);

                    Decimal _CommWthdr = 0;
                    Decimal _CommWthdr_Final = 0;
                    int Z = CHNLSVC.Financial.GetRemSumDet(Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, "03", "012", txtPC.Text, BaseCls.GlbUserComCode, out  _CommWthdr, out _CommWthdr_Final);

                    Decimal _RemBanked = 0;
                    Decimal _RemBankedFinal = 0;

                    Decimal _totalSum = 0;
                    Decimal _totalSumFinal = 0;
                    Decimal _val = 0;
                    Decimal _valFinal = 0;

                    //check whether day ens done or not (if done get tot sum from gnt_rem_sum table)
                    if (String.IsNullOrEmpty(BaseCls.GlbStatus))
                    {
                        DataTable _tbl = CHNLSVC.Financial.GetRemSummaryReport_without_comm_withdraw(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, "03", out _totalSum, out _totalSumFinal, 1);
                    }
                    else
                    {
                        MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                        DataTable _tbl = CHNLSVC.Financial.get_Rem_Sum_Rep_View_dt_range(Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, "03", Convert.ToDecimal(_CommWthdr), mst_com.Mc_anal24);
                        for (int i = 0; i < _tbl.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(_tbl.Rows[i]["rem_val"].ToString()))
                            {
                                _val = _val + Convert.ToDecimal(_tbl.Rows[i]["rem_val"]);
                                _valFinal = _valFinal + Convert.ToDecimal(_tbl.Rows[i]["rem_val_fin"]);
                            }

                        }
                        _totalSum = _val;
                        _totalSumFinal = _valFinal;
                    }

                    Decimal _totalLess = 0;
                    Decimal _totalLessFinal = 0;
                    _val = 0;
                    _valFinal = 0;

                    //check whether day ens done or not (if done get tot sum from gnt_rem_sum table)
                    if (String.IsNullOrEmpty(BaseCls.GlbStatus))
                    {
                        DataTable _tbl1 = CHNLSVC.Financial.GetRemSummaryReport(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, "04", out _totalLess, out _totalLessFinal);
                    }
                    else
                    {
                        MasterCompany mst_com = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                        DataTable _tbl2 = CHNLSVC.Financial.get_Rem_Sum_Rep_View_dt_range(Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, txtPC.Text, BaseCls.GlbUserID, "04", Convert.ToDecimal(_CommWthdr), mst_com.Mc_anal24);
                        for (int i = 0; i < _tbl2.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(_tbl2.Rows[i]["rem_val"].ToString()))
                            {
                                _val = _val + Convert.ToDecimal(_tbl2.Rows[i]["rem_val"]);
                                _valFinal = _valFinal + Convert.ToDecimal(_tbl2.Rows[i]["rem_val_fin"]);
                            }

                        }
                        _totalLess = _val;
                        _totalLessFinal = _valFinal;
                    }

                    _RemBanked = _totalSum - _totalLess;
                    _RemBankedFinal = _totalSumFinal - _totalLessFinal;

                    BaseCls.GlbRemToBeBanked = _RemBanked;
                    BaseCls.GlbRemToBeBankedFinal = _RemBankedFinal;

                    Decimal _CIH = 0;
                    Decimal _CIH_Final = 0;
                    int X = CHNLSVC.Financial.GetRemSumDet(BaseCls.GlbReportToDate, BaseCls.GlbReportToDate, "06", "001", txtPC.Text, BaseCls.GlbUserComCode, out  _CIH, out _CIH_Final);
                    BaseCls.GlbCIH = _CIH;
                    BaseCls.GlbCIHFinal = _CIH_Final;

                    Decimal _totRemManual = 0;
                    Decimal _totRemManualFinal = 0;
                    DataTable _tblRem = CHNLSVC.Financial.GetRemSummaryReport(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, "05", out _totRemManual, out _totRemManualFinal);

                    //total remitance
                    Decimal _tmptotRem = 0;
                    Decimal _totRem = 0;
                    Decimal _totRemFinal = 0;
                    int Y = CHNLSVC.Financial.GetTotRemitance(BaseCls.GlbUserComCode, Convert.ToDateTime(_fromDate).Date, BaseCls.GlbReportToDate, txtPC.Text, out _tmptotRem);
                    _totRem = _tmptotRem + _totRemManual;
                    _totRemFinal = _tmptotRem + _totRemManualFinal;

                    BaseCls.GlbTotRemitance = _totRem;
                    BaseCls.GlbTotRemitanceFinal = _totRemFinal;


                    BaseCls.GlbCommWithdrawn = _CommWthdr;
                    BaseCls.GlbCommWithdrawnFinal = _CommWthdr_Final;

                    //BaseCls.GlbReportFromDate = Convert.ToDateTime(_fromDate);
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    ReportViewer _view = new ReportViewer();
                    if (_dayEndNotFound == true)
                    //if (Convert.ToDateTime(_fromDate).Date == Convert.ToDateTime(txtToDate.Text).Date)
                    //{
                    //if (CHNLSVC.Financial.IsDayEndDone(txtComp.Text, txtPC.Text, Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text)) == false)
                    {
                        _view.GlbReportName = "Remitance_Sum_view.rpt";     //   view report from transaction tables
                        BaseCls.GlbReportName = "Remitance_Sum_view.rpt";
                    }
                    else
                    {
                        _view.GlbReportName = "Remitance_Sum.rpt";          //   view from gnt_rem_sum table
                        BaseCls.GlbReportName = "Remitance_Sum.rpt";
                    }
                    //}
                    //else
                    //{
                    //    _view.GlbReportName = "Remitance_Sum.rpt";
                    //    BaseCls.GlbReportName = "Remitance_Sum.rpt";
                    //}

                    _view.Show();
                    _view = null;
                }

                if (opt1.Checked == true)   //cash sales summary
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportDocType = txtDocType.Text;
                    //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit;
                    BaseCls.GlbReportHeading = "Cash Sales Summary";
                    BaseCls.GlbReportType = "CASHSALE";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "SalesSummary1.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt2.Checked == true)   //Credit sales summary
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportDocType = txtDocType.Text;
                    //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit;
                    BaseCls.GlbReportHeading = "Credit Sales Summary";
                    BaseCls.GlbReportType = "CREDITSALE";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "SalesSummary1.rpt";
                    _view.Show();
                    _view = null;
                }


                if (opt3.Checked == true)   //Hire sales summary
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(BaseCls.GlbUserComCode, 6003);
                    if (_rptPrd != 0)
                    {
                        if ((BaseCls.GlbReportToDate - BaseCls.GlbReportFromDate).Days > _rptPrd)
                        {
                            MessageBox.Show("you cannot generate this report for this date period. Allowed period - " + _rptPrd + " Days.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }
                    }

                    BaseCls.GlbReportDocType = txtDocType.Text;
                    //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit;
                    BaseCls.GlbReportHeading = "Hire Sales Summary";
                    BaseCls.GlbReportType = "HIRESALE";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "HP_SummaryRep.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt16.Checked == true)   //Sales Figures
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Sales Figures";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportDocType = chk_Ord.Checked == true ? "Y" : "N";
                    if (chk_Ord.Checked == true)
                    {
                        BaseCls.GlbReportName = "Sales_Figures_OrderBy.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "Sales_Figures.rpt";
                    }
                    _view.Show();
                    _view = null;
                }
                if (opt17.Checked == true)  //Debtor sales and settlements
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDoc = chkAsAtDate.Checked ? "Y" : "N";
                    BaseCls.GlbReportCustomerCode = txtCust.Text;

                    ReportViewer _view = new ReportViewer();
                    if (optOutsAll.Checked == true)
                    {
                        BaseCls.GlbReportOutsOnly = 0;
                        if (optPC.Checked == true)
                        {
                            _view.GlbReportName = "DebtorSettlement_PC.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement_PC.rpt";
                        }
                        else
                        {
                            _view.GlbReportName = "DebtorSettlement.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement.rpt";
                        }

                    }
                    if (optOutsSett.Checked == true)
                    {
                        BaseCls.GlbReportOutsOnly = 2;
                        if (optPC.Checked == true)
                        {
                            _view.GlbReportName = "DebtorSettlement_PC.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement_PC_.rpt";
                        }
                        else
                        {
                            _view.GlbReportName = "DebtorSettlement.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement.rpt";
                        }

                    }
                    else
                    {
                        BaseCls.GlbReportOutsOnly = 1;
                        if (optPC.Checked == true)
                        {
                            _view.GlbReportName = "DebtorSettlement_Outs_PC.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement_Outs_PC.rpt";
                        }
                        else
                        {
                            _view.GlbReportName = "DebtorSettlement_Outs.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement_Outs.rpt";
                        }
                    }
                    _view.Show();
                    _view = null;

                }
                if (opt18.Checked == true)  //debtor sales and receipts
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;

                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "DebtorSalesReceipts.rpt";
                    BaseCls.GlbReportName = "DebtorSalesReceipts.rpt";
                    _view.Show();
                    _view = null;
                }


                if (opt19.Checked == true)  //age debtor outstanding
                {
                    string _recType = "";
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDoc = chk_Export.Checked ? "Y" : "N";

                    if (chk_Export.Checked)
                    {
                        string _error;
                        //update_PC_List();

                        #region ValuveAssigning
                        //BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                        //BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                        //BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                        //BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                        //BaseCls.GlbReportItemCat5 = txtIcat5.Text;
                        //BaseCls.GlbReportBrand = txtBrand.Text;
                        //BaseCls.GlbReportModel = txtModel.Text;
                        //BaseCls.GlbReportItemCode = txtItemCode.Text;
                        //BaseCls.GlbReportProfit = txtPC.Text;
                        #endregion

                        string _filePath = CHNLSVC.MsgPortal.GetAgeAnalysisDebotrsExecl(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "ALL", "0", out _error);


                        if (!string.IsNullOrEmpty(_error))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        if (chkReg.Checked == true)    //kapila 7/9/2015
                            BaseCls.GlbReportParaLine1 = 1;
                        else
                            BaseCls.GlbReportParaLine1 = 0;
                        if (chk_Cust.Checked == true)
                        {
                            _recType = "ALL";
                        }
                        else
                        {
                            _recType = txtCust.Text;
                        }

                        BaseCls.GlbRecType = _recType;

                        ReportViewer _view = new ReportViewer();
                        //kapila 31/8/2015
                        if (MessageBox.Show("Do you need to run the report with Credit Notes & Advance Receipts?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            if (MessageBox.Show("Do you need to run the report with DCN?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                if (MessageBox.Show("Do you need to run the report register/unregister-wise?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    if (optPC.Checked == true)
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_PC.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_PC.rpt";
                                    }
                                    else
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding.rpt";
                                    }
                                }
                                else
                                {
                                    if (optPC.Checked == true)
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_PC_new.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_PC_new.rpt";
                                    }
                                    else
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_new.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_new.rpt";
                                    }
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Do you need to run the report register/unregister-wise?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    if (optPC.Checked == true)
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_PC.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_PC.rpt";
                                    }
                                    else
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_dcn.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_dcn.rpt";
                                    }
                                }
                                else
                                {
                                    if (optPC.Checked == true)
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_PC_new.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_PC_new.rpt";
                                    }
                                    else
                                    {
                                        _view.GlbReportName = "Age_Debtor_Outstanding_new_dcn.rpt";
                                        BaseCls.GlbReportName = "Age_Debtor_Outstanding_new_dcn.rpt";
                                    }
                                }
                            }

                            _view.Show();
                            _view = null;
                            ReportViewer _view2 = new ReportViewer();
                            _view2.GlbReportName = "Age_Debtor_Outstanding_Veh_reg.rpt";
                            BaseCls.GlbReportName = "Age_Debtor_Outstanding_Veh_reg.rpt";
                            _view2.Show();
                            _view2 = null;
                        }
                        else
                        {
                            _view.GlbReportName = "Age_Debtor_Outstanding_adv.rpt";
                            BaseCls.GlbReportName = "Age_Debtor_Outstanding_adv.rpt";
                            _view.Show();
                            _view = null;
                        }
                    }

                }

                if (opt22.Checked == true)  //cancelled Documents
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;

                    BaseCls.GlbReportProfit = txtPC.Text;
                    if (comboBoxDocType.Text != null)
                    //  if (comboBoxDocType.SelectedValue != null)
                    {
                        BaseCls.GlbReportDocType = comboBoxDocType.Text;
                    }
                    else
                    {
                        BaseCls.GlbReportDocType = null;
                    }


                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "CancelledDocuments.rpt";
                    BaseCls.GlbReportName = "CancelledDocuments.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt24.Checked == true)  //Delivered Sales
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    //update temporary table
                    update_PC_List();

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                    BaseCls.GlbReportItemCat5 = txtIcat5.Text;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportIsFast = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportDoc1 = txt_color.Text;
                    BaseCls.GlbReportDoc2 = txt_size.Text;
                    BaseCls.GlbReportType = "DSALE";
                    BaseCls.GlbReportHeading = "DELIVERED SALES REPORT";
                    BaseCls.GlbReportCountry = txtNationality.Text;
                    BaseCls.GlbReportProvince = txtProvince.Text;
                    BaseCls.GlbReportDistrict = txtDistrict.Text;
                    BaseCls.GlbReportCity = txtCity.Text;

                    if (chk_Export.Checked == false)
                    {
                        int x = 0;
                        foreach (ListViewItem Item in lstGroup.Items)
                        {
                            x++;
                            if (Item.Text == "INV")
                            {
                                if (lstGroup.Items.Count > x)
                                {
                                    MessageBox.Show("Document Number group should be the last group.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnDisplay.Enabled = true;
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                            }
                        }

                        set_GroupOrder();

                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                        BaseCls.GlbReportName = "DeliveredSalesReport.rpt";

                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportType = "DSALEEX";
                        BaseCls.GlbReportAppBy = (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075)) ? "Y" : "N";
                        string _error;
                        string _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesDetailsExcel(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date,
                        Convert.ToDateTime(BaseCls.GlbReportToDate).Date,
                        BaseCls.GlbReportCustomerCode,
                        BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel,
                        BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportAppBy, BaseCls.GlbUserID,
                        BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc,
                        BaseCls.GlbUserComCode,
                        BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, 1, 2, out _error
                        , BaseCls.GlbReportCountry, BaseCls.GlbReportProvince, BaseCls.GlbReportDistrict, BaseCls.GlbReportCity); //updated by akila 2018/04/02

                        if (!string.IsNullOrEmpty(_error))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (opt25.Checked == true)  //Total Sales
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    //update temporary table
                    update_PC_List();

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                    BaseCls.GlbReportItemCat5 = txtIcat5.Text;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = "";
                    BaseCls.GlbReportPromotor = txt_promotor.Text;
                    BaseCls.GlbReportDoc1 = txt_color.Text;
                    BaseCls.GlbReportDoc2 = txt_size.Text;
                    BaseCls.GlbReportIsFast = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportCountry = txtNationality.Text;
                    BaseCls.GlbReportProvince = txtProvince.Text;
                    BaseCls.GlbReportDistrict = txtDistrict.Text;
                    BaseCls.GlbReportCity = txtCity.Text;

                    BaseCls.GlbReportType = "TSALE";
                    BaseCls.GlbReportHeading = "TOTAL SALES REPORT";

                    BaseCls.GlbReportParaLine1 = chkDolar.Checked == true ? 1 : 2;

                    int x = 0;
                    foreach (ListViewItem Item in lstGroup.Items)
                    {
                        x++;
                        if (Item.Text == "INV")
                        {
                            if (lstGroup.Items.Count > x)
                            {
                                MessageBox.Show("Document Number group should be the last group.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnDisplay.Enabled = true;
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                    }

                    set_GroupOrder();

                    if (chk_Export.Checked == true)
                    {
                        BaseCls.GlbReportType = "TSALEEX";
                        string _error;

                        string _filePath = CHNLSVC.MsgPortal.GetTotalSalesDetailsExecl(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date,
                            BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode,
                            BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode,
                            BaseCls.GlbReportBrand, BaseCls.GlbReportModel,
                            BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2,
                            BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit,
                            BaseCls.GlbUserID, BaseCls.GlbReportType,
                            BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc,
                            BaseCls.GlbUserDefLoca, BaseCls.GlbUserComCode,
                            BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast,
                            BaseCls.GlbReportParaLine1, 2,
                            BaseCls.GlbReportDoc1, BaseCls.GlbReportDoc2
                            , out _error
                            , BaseCls.GlbReportCountry, BaseCls.GlbReportProvince, BaseCls.GlbReportDistrict, BaseCls.GlbReportCity); //updated by akila 2018/04/02;


                        if (!string.IsNullOrEmpty(_error))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                        BaseCls.GlbReportName = "DeliveredSalesReport.rpt";

                        _view.Show();
                        _view = null;
                    }


                }

                if (opt27.Checked == true)   //Stamp Duty Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "STAMP DUTY REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Stamp_Duty_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt28.Checked == true)   //SVAT Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "SVAT Report";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "SVAT_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt30.Checked == true)  //Delivered Sales Detail
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportCustomerCode = "";
                    BaseCls.GlbReportExecCode = "";
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit;

                    BaseCls.GlbReportType = "DSALE";
                    BaseCls.GlbReportHeading = "Delivered Sales Detail Report";

                    BaseCls.GlbReportGroupProfit = 1;  // optProfit.Checked == true ? 1 : 0;
                    BaseCls.GlbReportGroupDocType = 0;
                    BaseCls.GlbReportGroupCustomerCode = 0;
                    BaseCls.GlbReportGroupExecCode = 0;
                    BaseCls.GlbReportGroupItemCode = 0;   // optProfit.Checked == true ? 0 : 1;
                    BaseCls.GlbReportGroupBrand = 0;
                    BaseCls.GlbReportGroupModel = 0;
                    BaseCls.GlbReportGroupItemCat1 = 0;
                    BaseCls.GlbReportGroupItemCat2 = 0;
                    BaseCls.GlbReportGroupItemCat3 = 0;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DeliveredSalesReport_withCust.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt31.Checked == true)   //Forward Sales Report
                {
                    //update temporary table
                    update_PC_List();

                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }


                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    if (txtDiscRate.Text == "")
                    {
                        BaseCls.GlbReportDiscRate = 0;
                    }
                    else
                    {
                        BaseCls.GlbReportDiscRate = Convert.ToDecimal(txtDiscRate.Text);
                    }
                    if (optAll.Checked)
                    {
                        BaseCls.GlbReportExeType = "All";
                    }
                    else if (optLs.Checked)
                    {
                        BaseCls.GlbReportExeType = "<";
                    }
                    else if (optEq.Checked)
                    {
                        BaseCls.GlbReportExeType = "=";
                    }
                    else if (optGt.Checked)
                    {
                        BaseCls.GlbReportExeType = ">";
                    }

                    if (optDeliver.Checked)
                    {
                        BaseCls.GlbReportType = "N";
                    }
                    else if (optForward.Checked)
                    {
                        BaseCls.GlbReportType = "EX";
                    }
                    if (chk_Ord.Checked == false)
                    {
                        BaseCls.GlbReportWithCost = 0;
                    }

                    BaseCls.GlbReportHeading = "PENDING SALES DELIVERY";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    if (BaseCls.GlbReportType == "N")
                    {
                        if (BaseCls.GlbReportWithCost == 1)
                            BaseCls.GlbReportName = "Forward_Sales_Report_cost.rpt";
                        else
                            BaseCls.GlbReportName = "Forward_Sales_Report1.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "Forward_Sales_Report2.rpt";
                    }
                    _view.Show();
                    _view = null;
                }

                if (opt32.Checked == true)   //POS Details Report
                {
                    //update temporary table
                    update_PC_List();
                    //update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "POS Detail Report";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "POS_Detail_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt33.Checked == true)   //Insurance REgister
                {
                    //update temporary table
                    update_PC_List();

                    //if (chkClims.Checked == true)
                    //{ BaseCls.GlbwithClaims = 1; }
                    //if (chkSettle.Checked == true)
                    //{ BaseCls.GlbwithSettle = 1; }


                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportHeading = "Insurance Register";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "HPInsuranceRegisterNew.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt34.Checked == true)   //Executive wise sales with invoice
                {
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    if (txtDiscRate.Text == "")
                    {
                        BaseCls.GlbReportDiscRate = 0;

                    }
                    else
                    {
                        BaseCls.GlbReportDiscRate = Convert.ToDecimal(txtDiscRate.Text);
                    }



                    BaseCls.GlbReportHeading = "EXECUTIVE WISE SALES INVOICE REPORT";

                    BaseCls.GlbReportDiscTp = 0;

                    if (optAll.Checked == true)
                    {
                        BaseCls.GlbReportDiscTp = 0;
                    }

                    if (optLs.Checked == true)
                    {
                        BaseCls.GlbReportDiscTp = 1;
                    }

                    if (optEq.Checked == true)
                    {
                        BaseCls.GlbReportDiscTp = 2;
                    }

                    if (optGt.Checked == true)
                    {
                        BaseCls.GlbReportDiscTp = 3;
                    }

                    if (optDeliver.Checked == true)
                    {
                        BaseCls.GlbReportIsDelivered = 1;
                    }
                    else
                    {
                        BaseCls.GlbReportIsDelivered = 0;
                    }

                    if (cmbExeType.Text != null)
                    {
                        BaseCls.GlbReportExeType = cmbExeType.Text;

                    }
                    if (cmbExeType.Text == "Communication Executive")
                    {
                        BaseCls.GlbReportAllowCommision = 1;
                    }
                    BaseCls.GlbReportAppBy = txt_AppBy.Text;
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Execitivewise_Sales_with_Invoices.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt35.Checked == true)   //Vehicle Insurance
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    //  update_PC_List();
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;


                    // BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportHeading = "Vehicle Insurance Arrears";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPInsuranceArrear.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt36.Checked == true)   //Elite Commission Detail Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "ELITE COMMISSION DETAIL REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Elite_Commission_Details.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt37.Checked == true)   //Not Registered Vehicle Invoices Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "PENDING REGISTRATION RECEIPTS";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Not_Reg_Vehicles_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt38.Checked == true)   //Warranty Replacement Credit Notes
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "WARRANTY REPLACEMENT CREDIT NOTES";
                    BaseCls.GlbReportCustomerCode = txtCust.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Warr_Rpl_CRNote.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt39.Checked == true)   //Sales Promotion Achievement Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "SALES PROMOTION ACHIEVEMENT REPORT";
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportDirection = txtDirec.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Sales_Promotion_Achievement_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt15.Checked == true)   //Dealse Commission
                {
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportHeading = " DEALER COMMISSION";


                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DealerCommision.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt40.Checked == true)   //Delivered Sales With Serial
                {
                    DateTime start = BaseCls.GlbReportFromDate;
                    DateTime finish = BaseCls.GlbReportToDate;
                    Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 6040);
                    if (_rptPrd != 0)
                    {
                        if ((finish - start).Days > _rptPrd)
                        {
                            MessageBox.Show("you cannot generate this report for this date period.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }
                    }
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportHeading = " DELIVERED SALES WITH SERIAL";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DeliveredSalesWithSerial.rpt";
                    _view.Show();
                    _view = null;

                    //Base bsObj;
                    //string _filePath = string.Empty;
                    //_filePath = CHNLSVC.Sales.DeliveredSalesWithSerial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, null);



                    //if (!string.IsNullOrEmpty(_error))
                    //{
                    //    MessageBox.Show(_error);
                    //    return;
                    //}

                    //if (string.IsNullOrEmpty(_filePath))
                    //{
                    //    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    //Process p = new Process();
                    //p.StartInfo = new ProcessStartInfo(_filePath);
                    //p.Start();
                    //objHp.ExportCustomerDetailReport();
                    //fldgOpenPath.ShowDialog();


                    //string sourcefileName = BaseCls.GlbUserID + ".xls";
                    //string targetfileName = ".xls";
                    //string sourcePath = @"\\192.168.1.222\scm2\Print";
                    //string targetPath = BaseCls.GlbReportFilePath;
                    //string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                    //string targetFile = targetPath + targetfileName;

                    //System.IO.File.Copy(sourceFile, targetFile);
                    //System.IO.File.Delete(sourceFile);

                    //  MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    //BaseCls.GlbReportName = "DeliveredSalesWithSerial.rpt";
                    //_view.Show();
                    //_view = null;
                }

                if (opt41.Checked == true)   //Elite Commission Summary Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "ELITE COMMISSION SUMMARY REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Elite_Commission_Summary.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt42.Checked == true)   //Price Details
                {
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportDirection = txtDirec.Text;
                    if (comboBoxPayModes.SelectedValue != null)
                    {
                        BaseCls.GlbPayType = comboBoxPayModes.SelectedValue.ToString();
                    }
                    else
                    {
                        BaseCls.GlbPayType = null;
                    }
                    BaseCls.GlbReportPriceBook = txtPB.Text;
                    BaseCls.GlbReportPBLevel = txtPBLevel.Text;
                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportType = chkAsAtDate.Checked == true ? "ASAT" : "PERIOD";
                    BaseCls.GlbReportHeading = "PRICE DETAILS REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Price_Details_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt43.Checked == true)   //Insurance Register- Nadeeka
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportHeading = "Insurance Register";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "HPInsurancePolicyReport.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt44.Checked == true)   //Insurance Register - Nadeeka
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportHeading = "Insurance Register";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "HPInsuranceSettlemetInscom.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt45.Checked == true)   //Insurance Register- Nadeeka
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportHeading = "Insurance Register";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "HPInsuranceClaimRegister.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt46.Checked == true)   //Insurance Register- Nadeeka
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportHeading = "Insurance Register";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "HPInsuranceDocumentRequired.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt47.Checked == true)   //Delivered Sales - with GRN details
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";
                    //update temporary table
                    update_PC_List();

                    //if (chk_Export.Checked == true)
                    //{
                    //    fldgOpenPath.ShowDialog();
                    //}

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportSupplier = txtSup.Text;
                    BaseCls.GlbReportPurchaseOrder = txtPO.Text;

                    BaseCls.GlbReportHeading = "DELIVERED SALES - WITH GRN DETAILS";

                    if (chk_Export.Checked == true)
                    {
                        string _error = string.Empty;
                        string val = "0";
                        string _filePath = objSales.DeliveredSalesGRNReport_Execl(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, val, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportSupplier, BaseCls.GlbReportPurchaseOrder, BaseCls.GlbUserComCode, "N/A", BaseCls.GlbReportIsExport);

                        if (!string.IsNullOrEmpty(_error))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        MessageBox.Show("Export Completed", "Success");

                    }
                    else
                    {
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportName = "Delivered_Sales_GRN.rpt";
                        _view.Show();
                        _view = null;
                    }


                }

                if (opt48.Checked == true)   //extended warranty - kapila
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportHeading = "Extended Warranty";
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    BaseCls.GlbReportStatus = 2;
                    if (cmb_Stus.Text == "Active")
                    {
                        BaseCls.GlbReportStatus = 1;
                    }
                    else if (cmb_Stus.Text == "Cancel")
                    {
                        BaseCls.GlbReportStatus = 0;
                    }

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "ExtendedWarranty.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt49.Checked == true)   //Total Revenue Report - Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "TOTAL REVENUE REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Total_Revenue_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt50.Checked == true)  //Manual Documents- Nadeeka
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;

                    BaseCls.GlbReportProfit = txtPC.Text;
                    if (comboBoxDocType.Text != null)
                    //  if (comboBoxDocType.SelectedValue != null)
                    {
                        BaseCls.GlbReportDocType = comboBoxDocType.Text;
                    }
                    else
                    {
                        BaseCls.GlbReportDocType = null;
                    }


                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "ManualDocument.rpt";
                    BaseCls.GlbReportName = "ManualDocument.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt51.Checked == true)   //Paymode wise transaction Report - Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "PAY MODE WISE TRANSACTIONS REPORT";

                    if (chk_Export.Checked == true)
                    {
                        fldgOpenPath.ShowDialog();
                    }

                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;
                    if (chk_Pay_Tp.Checked == false)
                    {
                        BaseCls.GlbPayType = comboBoxPayModes.SelectedValue.ToString();
                    }
                    else
                    {
                        BaseCls.GlbPayType = "";
                    }
                    BaseCls.GlbReportDocType = "NOTAMEND";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "PaymodewiseTr_Report.rpt";
                    _view.Show();
                    _view = null;

                    if (chk_Export.Checked == true)
                    {
                        string sourcefileName = BaseCls.GlbUserID + ".xls";
                        string targetfileName = ".xls";
                        string sourcePath = @"\\192.168.1.222\scm2\Print";
                        string targetPath = BaseCls.GlbReportFilePath;
                        string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                        string targetFile = targetPath + targetfileName;

                        System.IO.File.Copy(sourceFile, targetFile);
                        System.IO.File.Delete(sourceFile);
                    }
                }

                if (opt52.Checked == true)   //Duty Free Sales Statement
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE SALES STATEMENT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Sales_Statement.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt53.Checked == true)   //Duty Free Consolidated Statement of Sales
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE CONSOLIDATED STATEMENT OF SALES";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Consolidated_Sales.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt54.Checked == true)   //Duty Free Category wise Sales
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE CATEGORY WISE SALES";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Categorywise_Sales.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt55.Checked == true)   //Duty Free Currency wise Sales
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE CURRENCY WISE SALES";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Sales_Currencywise.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt89.Checked == true)   // credit note Details report by hasith 25/01/205
                {
                    //update temporary table
                    //update_PC_List();
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Credit Note Details Report";
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportProfit = txtPC.Text;


                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "CreditNoteDetails.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt56.Checked == true)   //Duty Free Sales Quantity and Value report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE SALES QUANTITY AND VALUE REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_SalesWithQty.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt57.Checked == true)   //Duty Free Quantity and Value report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE SALES QUANTITY REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Sales_Qty.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt58.Checked == true)   //DUTY FREE MONTHLY SALES REPORT
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE MONTHLY SALES REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_MothlySales.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt59.Checked == true)   //DUTY FREE WEEKLY SALES REPORT
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE WEEKLY SALES REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_WeeklySales.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt60.Checked == true)   //DUTY FREE CATEGORYSALES REPORT
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE ITEM CATEGORY SALES REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_ItemCategorywise_Sales.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt61.Checked == true)   //Duty Free Foreign Currency Transactions 2013-12-26 Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE FOREIGN CURRENCY TRANSACTIONS";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Sales_CurrencywiseTr.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt62.Checked == true)   //Paymode wise transaction Report (Amended) - Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "PAYMODE WISE TRANSACTIONS REPORT - AMENDED";

                    if (chk_Export.Checked == true)
                    {
                        fldgOpenPath.ShowDialog();
                    }

                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;
                    if (chk_Pay_Tp.Checked == false)
                    {
                        BaseCls.GlbPayType = comboBoxPayModes.SelectedValue.ToString();
                    }
                    else
                    {
                        BaseCls.GlbPayType = "";
                    }
                    BaseCls.GlbReportDocType = "AMEND";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "PaymodewiseTr_Report.rpt";
                    _view.Show();
                    _view = null;

                    if (chk_Export.Checked == true)
                    {
                        string sourcefileName = BaseCls.GlbUserID + ".xls";
                        string targetfileName = ".xls";
                        string sourcePath = @"\\192.168.1.222\scm2\Print";
                        string targetPath = BaseCls.GlbReportFilePath;
                        string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                        string targetFile = targetPath + targetfileName;

                        System.IO.File.Copy(sourceFile, targetFile);
                        System.IO.File.Delete(sourceFile);
                    }
                }
                if (opt50.Checked == true)
                {

                }

                if (opt65.Checked == true)   // Nadeeka
                {
                    //update temporary table
                    update_PC_List();
                    clsSalesRep objSales = new clsSalesRep();
                    BaseCls.GlbReportHeading = "OUT PUT TAX SCHEDULE";

                    BaseCls.GlbReportName = "SalesVatSchedule.rpt";


                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    objSales.GetSalesFiguresDetailsWithTax();
                    string _repPath = "";
                    MasterCompany _masterComp = null;
                    _masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

                    if (_masterComp != null)
                    {
                        _repPath = _masterComp.Mc_anal6;

                    }


                    objSales._salesTaxSch.ExportToDisk(ExportFormatType.Excel, _repPath + "SalesVatScheduleReport" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;




                    string workbookPath = _repPath + "SalesVatScheduleReport" + BaseCls.GlbUserID + ".xls";
                    //string workbookPath = @"\\192.168.1.222\SCM\Reports\HP_CashFlowForecastingReport.xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);
                }


                if (opt63.Checked == true)  //Debtor sales and settlements accounts format
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportCustomerCode = txtCust.Text;

                    ReportViewer _view = new ReportViewer();
                    if (optOutsAll.Checked == true)
                    {
                        if (chkWithComm.Checked == false)
                        {
                            MessageBox.Show("Sorry.This report is allow to generate outstanding details only", "Outstanding Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            optOuts.Checked = true;
                            return;
                        }

                        BaseCls.GlbReportOutsOnly = 0;  //kapila 18/6/2014
                        if (optPC.Checked == true)
                        {
                            _view.GlbReportName = "DebtorSettlement_Outs_PC_with_comm.rpt";
                            BaseCls.GlbReportName = "DebtorSettlement_Outs_PC_with_comm.rpt";
                        }
                        else
                        {
                            MessageBox.Show("Sorry.This report is allow to generate profit center wise only", "Outstanding Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            optPC.Checked = true;
                            return;
                        }

                    }
                    else
                    {
                        BaseCls.GlbReportOutsOnly = 1;
                        if (optPC.Checked == true)
                        {
                            if (chkWithComm.Checked == false)
                            {
                                _view.GlbReportName = "DebtorSettlement_Outs_PC_Meeting.rpt";
                                BaseCls.GlbReportName = "DebtorSettlement_Outs_PC_Meeting.rpt";
                            }
                            else
                            {//kapila 18/6/2014
                                _view.GlbReportName = "DebtorSettlement_Outs_PC_with_comm.rpt";
                                BaseCls.GlbReportName = "DebtorSettlement_Outs_PC_with_comm.rpt";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sorry.This report is allow to generate profit center wise only", "Outstanding Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            optPC.Checked = true;
                            return;
                            //_view.GlbReportName = "DebtorSettlement_Outs.rpt";
                            //BaseCls.GlbReportName = "DebtorSettlement_Outs.rpt";
                        }
                    }



                    _view.Show();
                    _view = null;

                }

                if (opt66.Checked == true)   //Sales Report - A-Entertainment 2014-03-10 Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "SALES REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "A_Sales_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt67.Checked == true)   //Loyalty Discounts Given to Customers 2014-03-20 Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "LOYALTY DISCOUNTS GIVEN TO CUSTOMERS";

                    BaseCls.GlbRccType = txtLoyTp.Text.Trim();
                    BaseCls.GlbReportCusId = txt_CustTp.Text.Trim();
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Loyality_Disc_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt68.Checked == true)   //Duty Free Model wise  2014-05-08 Nadeeka
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE MODEL WISE SALES REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_ModelwiseSales.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt69.Checked == true)   //Discount Promotions wise  2014-05-28 Sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "PROMOTIONWISE DISCOUNT REPORT";
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportDocType = chk_PO.Checked == true ? "Y" : "N";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.GetDiscountPromoDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc, BaseCls.GlbReportDocType, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt70.Checked == true)  //Customer Analysis (80% - 20%)
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    //update temporary table
                    update_PC_List();

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = "";
                    if (txtDiscRate.Text == "")
                    {
                        BaseCls.GlbReportGroupProfit = 0;
                    }
                    else
                    {
                        BaseCls.GlbReportGroupProfit = Convert.ToInt16(txtDiscRate.Text);
                    }
                    if (optAll.Checked)
                    {
                        BaseCls.GlbReportExeType = "All";
                    }
                    else if (optLs.Checked)
                    {
                        BaseCls.GlbReportExeType = "<";
                    }
                    else if (optEq.Checked)
                    {
                        BaseCls.GlbReportExeType = "=";
                    }
                    else if (optGt.Checked)
                    {
                        BaseCls.GlbReportExeType = ">";
                    }

                    BaseCls.GlbReportHeading = "CUSTOMER ANALYSIS (80% - 20%)";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                    BaseCls.GlbReportName = "DeliveredSalesReport80-20.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt71.Checked == true)   //Promotion Details
                {
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportDirection = txtDirec.Text;
                    if (comboBoxPayModes.SelectedValue != null)
                    {
                        BaseCls.GlbPayType = comboBoxPayModes.SelectedValue.ToString();
                    }
                    else
                    {
                        BaseCls.GlbPayType = null;
                    }
                    BaseCls.GlbReportPriceBook = txtPB.Text;
                    BaseCls.GlbReportPBLevel = txtPBLevel.Text;
                    BaseCls.GlbReportType = chkAsAtDate.Checked == true ? "Y" : "N";
                    BaseCls.GlbReportStrStatus = "A";
                    BaseCls.GlbReportDoc2 = chkWithComm.Checked == true ? "Y" : "N";
                    BaseCls.GlbReportHeading = "Promotion Details";
                    objSales.ActivePromotionReport();

                }

                if (radDailyColl.Checked)
                {
                    BaseCls.GlbReportHeading = "Daily Collection Report";

                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbPayType = Convert.ToString(comboBoxPayModes.SelectedValue);
                    BaseCls.GlbReportExecCode = txtExec.Text.Trim();

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view._lstView = lstPC;
                    BaseCls.GlbReportName = "AOACollection.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt72.Checked == true)
                {
                    //update_PC_List_RPTDB();
                    int check = 0;
                    clsSalesRep objSale = new clsSalesRep();
                    //if(txtPC.Text!=string.Empty)BaseCls.GlbReportProfit = txtPC.Text;

                    //foreach (ListViewItem Item in lstPC.Items)
                    //{
                    //    string pc = Item.Text;
                    //    if (Item.Checked == true)
                    //    {
                    //        check = 1;
                    //    }
                    //}
                    update_PC_List();
                    BaseCls.GlbReportDocType = "RMV";
                    if (rdbCRRec.Checked == true) BaseCls.GlbReportDocType = "CRR";
                    if (rdbNoplateRec.Checked == true) BaseCls.GlbReportDocType = "NO";
                    //if (check == 0)
                    //{
                    //    MessageBox.Show("Select at least one location");
                    //    return;
                    //}
                    BaseCls.GlbReportHeading = "REGISTRATION REPORT";

                    if (chk_Export.Checked == true)
                    {
                        objSales.RegistrationDetailsReport();
                    }
                    else
                    {
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportName = "RegistrationReport.rpt";
                        _view.Show();
                        _view = null;
                    }
                }
                if (opt73.Checked == true)   //Summary of weekly -- Nadeeka
                {
                    Int32 _ok = CHNLSVC.Financial.IsValidWeekDataRange(Convert.ToInt32(cmbYear.Text), cmbMonth.SelectedIndex + 1, Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date, BaseCls.GlbUserComCode);
                    if (_ok == 0)
                    {
                        MessageBox.Show("Invalid Date Range !", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbReportHeading = "Summary of Weekly";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "SummaryofWeekly.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt74.Checked == true)  //Comparison of Delivered Sales
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    //update temporary table
                    update_PC_List();

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportIsFast = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportCusId = opt_month.Checked == true ? "Y" : "N";

                    BaseCls.GlbReportType = "DSALE";
                    BaseCls.GlbReportHeading = "COMPARISON OF DELIVERED SALES";

                    int x = 0;
                    foreach (ListViewItem Item in lstGroup.Items)
                    {
                        x++;
                        if (Item.Text == "INV")
                        {
                            if (lstGroup.Items.Count > x)
                            {
                                MessageBox.Show("Document Number group should be the last group.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnDisplay.Enabled = true;
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                    }

                    set_GroupOrder();

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                    BaseCls.GlbReportName = "DeliveredSalesComparisonReport.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt75.Checked == true)  //Comparison of Total Sales
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    //update temporary table
                    update_PC_List();

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportIsFast = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportCusId = opt_month.Checked == true ? "Y" : "N";

                    BaseCls.GlbReportType = "TSALE";
                    BaseCls.GlbReportHeading = "COMPARISON OF TOTAL SALES";

                    int x = 0;
                    foreach (ListViewItem Item in lstGroup.Items)
                    {
                        x++;
                        if (Item.Text == "INV")
                        {
                            if (lstGroup.Items.Count > x)
                            {
                                MessageBox.Show("Document Number group should be the last group.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnDisplay.Enabled = true;
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                    }

                    set_GroupOrder();

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                    BaseCls.GlbReportName = "DeliveredSalesComparisonReport.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt76.Checked == true)  //HP Sales Information
                {
                    update_PC_List();
                    BaseCls.GlbReportDocType = txtDocType.Text.Trim();
                    BaseCls.GlbReportDocSubType = txtDocSubType.Text.Trim();

                    BaseCls.GlbReportHeading = "HP SALES INFORMATION";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.GetSalesInfoDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt77.Checked == true)  //Delviered Sales Insured
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    //update temporary table
                    update_PC_List();

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportIsFast = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;

                    BaseCls.GlbReportType = "DSALE";
                    BaseCls.GlbReportHeading = "SMART INSURED REPORT";

                    int x = 0;
                    foreach (ListViewItem Item in lstGroup.Items)
                    {
                        x++;
                        if (Item.Text == "INV")
                        {
                            if (lstGroup.Items.Count > x)
                            {
                                MessageBox.Show("Document Number group should be the last group.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnDisplay.Enabled = true;
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                        }
                    }

                    set_GroupOrder();

                    if (chk_Export.Checked == false)
                    {
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportName = "DeliveredSalesInsuranceReport.rpt";
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;
                        _filePath = CHNLSVC.MsgPortal.GetDeliveredSalesInsuDetails1(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbUserComCode, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, out _error);
                        if (!string.IsNullOrEmpty(_error))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_error);
                            return;
                        }

                        if (string.IsNullOrEmpty(_filePath))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Process p = new Process();
                        p.StartInfo = new ProcessStartInfo(_filePath);
                        p.Start();

                        MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (opt78.Checked == true)   // Quatation Details -Nadeeka
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportHeading = "Quotation Details";
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;



                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "QuoationDet.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt79.Checked == true)   // Sales Target Achievement - Sanjeewa 2015-05-26
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Sales Target Achivement Report";
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Sales_Target_Achievement_Del.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt80.Checked == true)   // sales promoter details Wimal @ 22/06/2015
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Sale promoter details Report";
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "SalesPromoterDetails.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt81.Checked == true)   //Insurance REgister
                {
                    //update temporary table

                    update_PC_List_RPTDB();

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportHeading = "Future Insurance Collected Premium";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "VehicleInsuranceCollection.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt82.Checked == true)  //Insurance Premium Payment Details
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "INSURANCE PREMIUM PAYMENT DETAILS";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.GetInsurancePremiumDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt83.Checked == true)
                {//Sanjeewa 2015-09-29 Pending Delivery Confirmations

                    update_PC_List();

                    GlbReportName = "Pending Delivery Confirmations";

                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportType = chkAsAtDate.Checked == true ? "ASAT" : "PERIOD";
                    BaseCls.GlbReportWithCost = Convert.ToInt16((CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075)) ? 1 : 0);

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "Del_Conf_Dtl_Report.rpt";
                    BaseCls.GlbReportName = "Del_Conf_Dtl_Report.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt84.Checked == true)
                {//Sanjeewa 2015-10-16 Pending Delivery Confirmations
                    string _compDesc = "";
                    string _compAddr = "";
                    string ExcessShortId = CHNLSVC.Financial.GetExcessShortID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, new DateTime(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), 1));
                    Decimal _curBal = 0;
                    Decimal _prvBal = 0;

                    if (ExcessShortId == "")
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("No Finalised Excess Short Remitances for the month.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //delete from sat_excs_bal table and insert
                    string _stus = "";
                    //if (lblStatus.Text == "PENDING")
                    //{
                    //    _stus = "NOT CONFIRMED";
                    //}

                    int X = CHNLSVC.Financial.PrintExcessShort(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ExcessShortId, new DateTime(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), 1), _stus, BaseCls.GlbUserID, out _prvBal, out _curBal);


                    //GlbReportName = "ExcessShort";
                    //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\ExcessShort.rpt";
                    //GlbReportMapPath = "~/Reports_Module/Sales_Rep/ExcessShort.rpt";

                    GlbReportName = "Excess Short";
                    BaseCls.GlbReportPrvBal = _prvBal;
                    BaseCls.GlbReportCurBal = _curBal;
                    BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                    BaseCls.GlbReportDoc = ExcessShortId;
                    BaseCls.GlbStatus = _stus;
                    BaseCls.GlbReportAsAtDate = new DateTime(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), 1);

                    MasterCompany _masterComp = null;
                    _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                    if (_masterComp != null)
                    {
                        BaseCls.GlbReportComp = _masterComp.Mc_desc;
                        BaseCls.GlbReportCompAddr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
                    }

                    ReportViewer _view = new ReportViewer();
                    _view.GlbReportName = "ExcessShort.rpt";
                    BaseCls.GlbReportName = "ExcessShort.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt85.Checked == true)  //Stock Sale
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                    BaseCls.GlbReportItemCat5 = txtIcat5.Text;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = "";
                    BaseCls.GlbReportPromotor = txt_promotor.Text;
                    BaseCls.GlbReportIsFast = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportSupplier = txt_supplier.Text;
                    BaseCls.GlbReportType = opt_sum.Checked == true ? "SUM" : "DTL";

                    BaseCls.GlbReportHeading = "SUPPLIER WISE STOCK SALES REPORT";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                    if (BaseCls.GlbReportType == "DTL")
                    {
                        BaseCls.GlbReportName = "Stock_Sale_Report.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "Stock_Sale_Report_sum.rpt";
                    }

                    _view.Show();
                    _view = null;
                }

                if (opt86.Checked == true)
                {//Sanjeewa 2016-02-10 PO Allocation

                    update_PC_List();

                    GlbReportName = "PO Allocation Details";

                    BaseCls.GlbReportPurchaseOrder = txtPO.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "PO_Allocation.rpt";
                    BaseCls.GlbReportName = "PO_Allocation.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt87.Checked == true)
                {//Sanjeewa 2016-02-19 Exchange Credit Note Details

                    update_PC_List_RPTDB();

                    GlbReportName = "Exchange Credit Note Details";

                    BaseCls.GlbReportPurchaseOrder = txtPO.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "exchange_crnote_report.rpt";
                    BaseCls.GlbReportName = "exchange_crnote_report.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt88.Checked == true)   //Delivered Sales - with GRN details & Cost
                {
                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";
                    //update temporary table
                    update_PC_List();

                    if (chk_Export.Checked == true)
                    {
                        fldgOpenPath.ShowDialog();
                    }

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportSupplier = txtSup.Text;
                    BaseCls.GlbReportPurchaseOrder = txtPO.Text;

                    BaseCls.GlbReportHeading = "DELIVERED SALES - WITH GRN DETAILS WITH COST";

                    if (chk_Export.Checked == true)
                    {
                        //objSales.DeliveredSalesGRNReport();
                        //string sourcefileName = BaseCls.GlbUserID + ".xls";
                        //string targetfileName = ".xls";
                        //string sourcePath = @"\\192.168.1.222\scm2\Print";
                        //string targetPath = BaseCls.GlbReportFilePath;
                        //string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                        //string targetFile = targetPath + targetfileName;

                        //System.IO.File.Copy(sourceFile, targetFile);
                        //System.IO.File.Delete(sourceFile);

                        //MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }

                if (opt90.Checked == true)  //Prce Definition Report - Sanjeewa 2016-05-30
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "PRICE DEFINITION REPORT";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.GetPriceDefinitionDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt91.Checked == true)  //discount report added by kapila done by lakshika in web
                {
                    update_PC_List();

                    int discountFrom = txtFromDisc.Text == "" ? 0 : Convert.ToInt32(txtFromDisc.Text);
                    int discountTo = txtToDisc.Text == "" ? 100 : Convert.ToInt32(txtToDisc.Text);

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportParaLine1 = discountFrom;
                    BaseCls.GlbReportParaLine2 = discountTo;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Discount_Report.rpt";
                    _view.Show();
                    _view = null;

                }

                if (opt92.Checked == true)  //GP report
                {
                    clsSalesRep objSales = new clsSalesRep();
                    //update temporary table
                    update_PC_List();
                    //kapila 16/6/2014

                    BaseCls.GlbReportHeading = "GP SUMMARY";

                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text).Date;
                    BaseCls.GlbReportDocType = txtDocType.Text.Trim();
                    BaseCls.GlbReportIsFreeIssue = 1;   //gros_amt<>0
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                    BaseCls.GlbReportItemCat5 = txtIcat5.Text;
                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    //BaseCls.GlbReportName = "GPSummary.rpt";
                    //_view.Show();
                    //_view = null;
                    #region commnet by thranga
                    //objSales.GPSummaryReport();

                    //MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    //string _path = _MasterComp.Mc_anal6;
                    //objSales._gpSumm.ExportToDisk(ExportFormatType.Excel, _path + "GPSumm" + BaseCls.GlbUserID + ".xls");

                    //Excel.Application excelApp = new Excel.Application();
                    //excelApp.Visible = true;
                    //string workbookPath = _path + "GPSumm" + BaseCls.GlbUserID + ".xls";
                    //Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                    //        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    //        true, false, 0, true, false, false);

                    #endregion

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "GP_Report.rpt";
                    _view.Show();
                    _view = null;


                    #region comment
                    //    Boolean _isSelAdmin = false;
                    //    string pc = "";
                    //    foreach (ListViewItem Item in lstAdmin.Items)
                    //    {
                    //         pc = Item.Text;

                    //        if (Item.Checked == true)
                    //        {
                    //            _isSelAdmin = true;
                    //            break;
                    //        }
                    //    }
                    //    if (_isSelAdmin && lstPC.Items.Count==0)     //kapila 20/12/2016
                    //    {
                    //        Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

                    //        foreach (ListViewItem Item in lstAdmin.Items)
                    //        {
                    //             pc = Item.Text;

                    //            if (Item.Checked == true)
                    //            {
                    //                Int32 effect = 0;
                    //                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam("ABL", null, null, null, null, null, null, pc);
                    //                foreach (DataRow drow in dt.Rows)
                    //                {
                    //                    effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, drow["COM"].ToString(), drow["PROFIT_CENTER"].ToString(), null);
                    //                }
                    //                 dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam("LRP", null, null, null, null, null, null, pc);
                    //                foreach (DataRow drow in dt.Rows)
                    //                {
                    //                    effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, drow["COM"].ToString(), drow["PROFIT_CENTER"].ToString(), null);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        update_PC_List();
                    //    }

                    //    string _error = "";
                    //    string _cat = "INV";
                    //    BaseCls.GlbReportIsFreeIssue = 0;

                    //    // if (opt_FOC_Yes.Checked == true)
                    //    BaseCls.GlbReportIsFreeIssue = 3;
                    //    // else if (opt_FOC_No.Checked == true)
                    //    //     BaseCls.GlbReportIsFreeIssue = 1;
                    //    // else
                    //    //     BaseCls.GlbReportIsFreeIssue = 2;

                    //    foreach (ListViewItem Item in lstGroup.Items)
                    //    {
                    //        _cat = Item.Text;
                    //        goto a;
                    //    }
                    //a:

                    //    string _filePath = CHNLSVC.MsgPortal.GetItemWiseGpExcel_new(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCusId, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode,
                    //        BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    //        BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbUserComCode,
                    //        BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFreeIssue, BaseCls.GlbReportItmClasif, BaseCls.GlbReportBrandMgr,
                    //        _cat, true/*withReversal */, 0, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, 0, out _error); // (withReversal flag true) 0 - REV, 1- SALE, 2 - ALL (Lakshika 2016/10/20)*/

                    //    if (!string.IsNullOrEmpty(_error))
                    //    {
                    //        btnDisplay.Enabled = true;
                    //        Cursor.Current = Cursors.Default;
                    //        MessageBox.Show(_error);
                    //        return;
                    //    }

                    //    if (string.IsNullOrEmpty(_filePath))
                    //    {
                    //        btnDisplay.Enabled = true;
                    //        Cursor.Current = Cursors.Default;
                    //        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }

                    //    Process p = new Process();
                    //    p.StartInfo = new ProcessStartInfo(_filePath);
                    //    p.Start();

                    //    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    #endregion
                }

                if (opt93.Checked == true)
                {   // Nadeeka - Moved from Inventory - Sanjeewa
                    //update temporary table
                    update_PC_List();

                    GlbReportName = "Fast Moving Items";

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportforAllCompany = 0;
                    if (chkAllLoc.Checked == true) { BaseCls.GlbReportforAllCompany = 1; }

                    BaseCls.GlbReportIsFast = 1;
                    FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory _view = new FF.WindowsERPClient.Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = "FastMovingItems.rpt";
                    BaseCls.GlbReportName = "FastMovingItems.rpt";
                    _view.Show();
                    _view = null;

                }
                if (opt94.Checked == true)   //Duty Free Currency wise Sales Detail
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "DUTY FREE CURRENCY WISE SALES";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "DF_Sales_Currencywise_dtl.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt95.Checked == true)   //Remitance_Sum_Recon
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "Remitance_Sum_Recon.rpt";
                    BaseCls.GlbReportName = "Remitance_Sum_Recon.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt96.Checked == true)  //Settlement Details - Sanjeewa 2017-02-20
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "SETTLEMENT DETAILS REPORT";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.Get_Receipt_Sett_Det(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt97.Checked == true)     //SOS reconcilation (kapila)
                {
                    string _error;
                    //check whether date range is valid
                    Int32 _ok = CHNLSVC.Financial.IsValidWeekDataRange(Convert.ToInt32(cmbYear.Text), cmbMonth.SelectedIndex + 1, Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date, BaseCls.GlbUserComCode);
                    if (_ok == 0)
                    {
                        MessageBox.Show("Invalid Date Range !", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }

                    update_PC_List_RPTDB();


                    string _filePath = CHNLSVC.MsgPortal.GetSOSReconcilation(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode,
                        BaseCls.GlbReportPromotor, BaseCls.GlbUserID, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                if (opt98.Checked == true)     //cash in hand summary (kapila)
                {
                    string _error;

                    update_PC_List_RPTDB();


                    string _filePath = CHNLSVC.MsgPortal.Get_CIH_Summary(BaseCls.GlbReportAsAtDate, BaseCls.GlbUserComCode,
                        BaseCls.GlbUserID, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                if (opt99.Checked == true)  //GP with Replaced Models - Sanjeewa 2017-02-20
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "GP WITH REPLACED MODELS REPORT";
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                    BaseCls.GlbReportItemCat5 = txtIcat5.Text;

                    if (chkRepModel.Checked)
                    {
                        BaseCls.GlbReportIsFast = 1;
                    }
                    else
                    {
                        BaseCls.GlbReportIsFast = 0;
                    }
                    BaseCls.GlbReportIsFreeIssue = Convert.ToInt16(opt_FOC_Yes.Checked == true ? 0 : opt_FOC_No.Checked == true ? 1 : opt_FOC.Checked == true ? 2 : 0);
                    BaseCls.GlbReportWithStatus = Convert.ToInt16(optintcom.Checked == true ? 0 : optintcomwithout.Checked == true ? 1 : optintcomwith.Checked == true ? 2 : 0);

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "GP_Detail_Repl.rpt";
                    BaseCls.GlbReportName = "GP_Detail_Repl.rpt";

                    _view.Show();
                    _view = null;

                    //string _filePath = string.Empty;
                    //string _error = string.Empty;
                    //_filePath = CHNLSVC.MsgPortal.GetItemWiseGpExcel(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode,
                    //        BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    //        BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportCompCode,
                    //        BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFreeIssue, BaseCls.GlbReportItmClasif, BaseCls.GlbReportBrandMgr, "ITM", 1,
                    //        BaseCls.GlbReportIsFast, BaseCls.GlbReportFromDate2, BaseCls.GlbReportToDate2, BaseCls.GlbReportWithStatus, out _error);

                    //if (!string.IsNullOrEmpty(_error))
                    //{
                    //    btnDisplay.Enabled = true;
                    //    Cursor.Current = Cursors.Default;
                    //    MessageBox.Show(_error);
                    //    return;
                    //}

                    //if (string.IsNullOrEmpty(_filePath))
                    //{
                    //    btnDisplay.Enabled = true;
                    //    Cursor.Current = Cursors.Default;
                    //    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    //Process p = new Process();
                    //p.StartInfo = new ProcessStartInfo(_filePath);
                    //p.Start();

                    //MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Tharanga 2017/06/24
                if (opt100.Checked == true)   //invoicesummary
                {
                    //update temporary tabletxtFromDate
                    update_PC_List();

                    BaseCls.GlbReportDocType = txtDocType.Text;
                    //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit;
                    BaseCls.GlbReportHeading = "Invoice Summary";
                    BaseCls.GlbReportType = "INV";

                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Invoice_summary.rpt";
                    _view.Show();
                    _view = null;
                }

                //added Tharindu 2017-11-08
                if (opt101.Checked == true)   // Sales With Current Inventory balance
                {
                    string _error;
                    string isasatchk = "N";
                    update_PC_List();

                    #region ValuveAssigning
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportItemCat4 = txtIcat4.Text;
                    BaseCls.GlbReportItemCat5 = txtIcat5.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    BaseCls.GlbReportWithCost = 0;
                    BaseCls.GlbReportWithSerial = 0;
                    BaseCls.GlbReportWithStatus = 0;
                    BaseCls.GlbReportWithDetail = 0;

                    BaseCls.GlbReportWithStatus = 0;

                    if (chkAsAtDate.Checked) { isasatchk = "Y"; }

                    BaseCls.GlbReportPriceBook = txtPB.Text;
                    BaseCls.GlbReportPBLevel = txtPBLevel.Text;

                    #endregion

                    //string _filePath = CHNLSVC.MsgPortal.GetSalesWithCurrentInv_Bal(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode,BaseCls.GlbUserComCode,
                    //    BaseCls.GlbUserID,BaseCls.GlbReportProfit,BaseCls.GlbUserDefLoca,out _error);

                    string _filePath = CHNLSVC.MsgPortal.GetSalesWithCurrentInv_Bal_new(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                        BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode,
                        BaseCls.GlbReportProfit, BaseCls.GlbUserDefLoca, txtAsAtDate.Text, BaseCls.GlbUserID, "", "", BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail, isasatchk, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Tharindu 2018/03/13
                if (opt102.Checked == true)
                {
                    //update temporary tabletxtFromDate
                    update_PC_List();

                    BaseCls.GlbReportDocType = txtDocType.Text;
                    //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit;
                    BaseCls.GlbReportHeading = "Invoice Summary";

                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Sales_Order_Sum_New.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt103.Checked == true)  //P & L Details - Sanjeewa 2017-02-20
                {
                    string _error;

                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "P & L Details Report";

                    string _filePath = CHNLSVC.MsgPortal.GetPLDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt104.Checked == true)  //Bill Collection Details - Wimal 11/09/2018
                {
                    if (Convert.ToDateTime(txtFromDate.Text).Date != Convert.ToDateTime(txtToDate.Text).Date)
                    {

                        MessageBox.Show("From Date and To Date Should be same", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }

                    int pcCount = 0;
                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        if (Item.Checked == true)
                        {
                            pcCount = pcCount + 1;
                        }
                    }

                    if (pcCount > 1)
                    {

                        MessageBox.Show("pls select one Profit center", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }

                    update_PC_List();

                    BaseCls.GlbReportHeading = "Bill Collection Detail";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Bill_Collection_detail.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt105.Checked == true)  //Bill Collection Summery - Wimal 11/09/2018
                {

                    update_PC_List();

                    BaseCls.GlbReportHeading = "Bill Collection Summery";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "Bill_Collection_summery.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt106.Checked == true)  //Slow moving Items Wimal - 29/Sep/2018
                {
                    string _error;

                    if (txtSQtyFrom.Text == "" || txtSQtyTo.Text == "")
                    {
                        MessageBox.Show("pls select From Qty And To Qty", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }

                    if (Convert.ToInt64(txtSQtyFrom.Text) > Convert.ToInt64(txtSQtyTo.Text))
                    {
                        MessageBox.Show("pls select valid From Qty And To Qty range ", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Cursor = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }



                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    BaseCls.GlbReportHeading = "Slow moving items Report";

                    string _filePath = CHNLSVC.MsgPortal.getSMIDetailsExcel(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, Convert.ToDecimal(txtSQtyFrom.Text), Convert.ToDecimal(txtSQtyTo.Text), out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnDisplay.Enabled = true;
                Cursor.Current = Cursors.Default;
            }


            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message + "\nTry again!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDisplay.Enabled = true;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            btnDisplay.Enabled = true;
            string com = "";
            //if (chkAllComp.Checked)
            //    com = "LRP";
            //else
            com = txtComp.Text;

            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            Boolean _isChk = false;
            string _adminTeam = "";

            //lstPC.SubItems.Add("Com");
            //if (chkAllComp.Checked == false)
            lstPC.Clear();

            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

            foreach (ListViewItem Item in lstAdmin.Items)
            {
                if (Item.Checked == true)
                {
                    _adminTeam = Item.Text;
                    _isChk = true;
                    break;
                }
            }
            if (_isChk == true)
            {
                foreach (ListViewItem Item in lstAdmin.Items)
                {
                    if (Item.Checked == true)
                    {
                        _adminTeam = Item.Text;
                        if (chkAllComp.Checked)
                        {
                            com = "";
                            if (BaseCls.GlbUserComCode == "ABL")
                            {
                                com = "LRP";
                            }
                            if (BaseCls.GlbUserComCode == "SGL")
                            {
                                com = "SGD";
                            }

                            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COM"].ToString());
                            }

                            com = txtComp.Text;

                            dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COM"].ToString());
                            }
                        }
                        else
                        {
                            com = txtComp.Text;

                            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COM"].ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());

                    }
                }
                else
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                    }
                }

                if (chkAllComp.Checked)
                {
                    com = "";
                    if (BaseCls.GlbUserComCode == "ABL")
                    {
                        com = "LRP";
                    }
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        com = "SGD";
                    }

                    if (CHNLSVC.Security.Is_OptionPerimitted(com, BaseCls.GlbUserID, 10044))
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                        }
                    }
                    else
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                        foreach (DataRow drow in dt.Rows)
                        {
                            if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                        }
                    }
                }
            }
        }

        private void txtChanel_LostFocus(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            btnDisplay.Enabled = true;
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

        #region option change events
        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL1"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6001))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6001)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }
        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL2"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6002))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6002)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(2);
            }
        }
        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            if (opt3.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL3"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6003))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6003)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(3);
            }
        }
        private void opt4_CheckedChanged(object sender, EventArgs e)
        {
            if (opt4.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL4"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6004))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6004)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(4);
            }
        }
        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            if (opt5.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL5"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6005))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6005)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(5);
            }
        }
        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            if (opt6.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL6"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6006))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6006)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(6);
            }
        }
        private void opt7_CheckedChanged(object sender, EventArgs e)
        {
            if (opt7.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL7"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6007))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6007)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(7);
            }
        }

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            if (opt9.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL9"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6009))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6009)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(9);
            }
        }
        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            if (opt10.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL10"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6010))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6010)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(10);
            }
        }
        //private void opt11_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt11.Checked == true)
        //    {
        //        //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL11"))
        //        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6011))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6011)");
        //            //opt1.Checked = false;
        //            RadioButton RDO = (RadioButton)sender;
        //            RDO.Checked = false;
        //            return;
        //        }
        //        setFormControls(11);
        //    }
        //}
        //private void opt12_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt12.Checked == true)
        //    {
        //        //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL12"))
        //        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6012))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6012)");
        //            //opt1.Checked = false;
        //            RadioButton RDO = (RadioButton)sender;
        //            RDO.Checked = false;
        //            return;
        //        }
        //        setFormControls(12);
        //    }
        //}


        private void opt15_CheckedChanged(object sender, EventArgs e)
        {
            if (opt15.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL15"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6015))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6015)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(15);
            }
        }
        private void opt16_CheckedChanged(object sender, EventArgs e)
        {
            if (opt16.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL16"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6016)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
        }
        private void opt17_CheckedChanged(object sender, EventArgs e)
        {
            if (opt17.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL17"))

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6017))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6017)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(17);
            }
        }
        private void opt18_CheckedChanged(object sender, EventArgs e)
        {
            if (opt18.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL18"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6018))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6018)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(18);
            }
        }
        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            if (opt19.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL19"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6019))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6019)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(19);
            }
        }
        private void opt20_CheckedChanged(object sender, EventArgs e)
        {
            if (opt20.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL20"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6020))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6020)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(20);
            }
        }
        //private void opt21_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt21.Checked == true) 
        //    {
        //        if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL21"))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :RSL21 )");
        //            opt21.Checked = false;
        //            return;
        //        }    
        //        setFormControls(21); 
        //    }
        //}
        private void opt22_CheckedChanged(object sender, EventArgs e)
        {
            if (opt22.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL22"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6022))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6022)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(22);
            }
        }

        private void opt25_CheckedChanged(object sender, EventArgs e)
        {
            if (opt25.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL10"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6025))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6025)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(25);
            }
        }

        private void opt26_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            int month = cmbMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(cmbYear.Text);

            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);

            txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
            txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
        }

        private void Sales_Rep_Load(object sender, EventArgs e)
        {

        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            if (opt24.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL10"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6024))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6024)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(24);
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                    {
                        paramsText.Append(_userCompany);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        paramsText.Append("HP" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + txtIcat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        paramsText.Append(txtDocType.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Prefix:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularByComp:
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

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromoByComp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "C" + seperator + "A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashComCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotor:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Nationality:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DistrictCode:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Supplier:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                //        break;
                //    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_DoubleClick(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                load_PCDesc();
            }
        }

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
        }

        private void txtRecType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(null, null);
            }
        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {
            if (opt27.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL10"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6027))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6027)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(27);
            }
        }

        private void opt28_CheckedChanged(object sender, EventArgs e)
        {
            if (opt28.Checked == true)
            {
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL10"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6028))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6028)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(28);
            }
        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            if (opt13.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6013))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6013)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(13);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
            DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRecType;
            _CommonSearch.ShowDialog();
            txtRecType.Select();
            chkRecType.Checked = false;
        }


        private void chkRecType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecType.Checked == true)
            {
                txtRecType.Text = "";
                txtRecType.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                txtRecType.Enabled = true;
                btnSearch.Enabled = true;
            }
        }

        private void chk_Entry_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Entry_Tp.Checked == true)
            {
                txtEntryTp.Text = "";
                txtEntryTp.Enabled = false;
                btn_Srch_Entry.Enabled = false;
            }
            else
            {
                txtEntryTp.Enabled = true;
                btn_Srch_Entry.Enabled = true;
            }
        }

        private void chk_Doc_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Doc_Tp.Checked == true)
            {
                txtDocType.Text = "";
                txtDocType.Enabled = false;
                btn_Srch_Doc_Tp.Enabled = false;
            }
            else
            {
                txtDocType.Enabled = true;
                btn_Srch_Doc_Tp.Enabled = true;
            }
        }

        private void chk_Doc_Sub_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Doc_Sub_Tp.Checked == true)
            {
                txtDocSubType.Text = "";
                txtDocSubType.Enabled = false;
                btn_Srch_DocSubTp.Enabled = false;
            }
            else
            {
                txtDocSubType.Enabled = true;
                btn_Srch_DocSubTp.Enabled = true;
            }
        }

        private void chk_Doc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Doc.Checked == true)
            {
                txtDocNo.Text = "";
                txtDocNo.Enabled = false;
                btn_Srch_Doc.Enabled = false;
            }
            else
            {
                txtDocNo.Enabled = true;
                btn_Srch_Doc.Enabled = true;
            }
        }

        private void chk_Dir_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Dir.Checked == true)
            {
                txtDirec.Text = "";
                txtDirec.Enabled = false;
                btn_Srch_Dir.Enabled = false;
            }
            else
            {
                txtDirec.Enabled = true;
                btn_Srch_Dir.Enabled = true;
            }
        }

        private void chk_ICat1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat1.Checked == true)
            {
                txtIcat1.Text = "";
                txtIcat1.Enabled = false;
                btn_Srch_Cat1.Enabled = false;
            }
            else
            {
                txtIcat1.Enabled = true;
                btn_Srch_Cat1.Enabled = true;
            }
        }

        private void chk_ICat2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat2.Checked == true)
            {
                txtIcat2.Text = "";
                txtIcat2.Enabled = false;
                btn_Srch_Cat2.Enabled = false;
            }
            else
            {
                txtIcat2.Enabled = true;
                btn_Srch_Cat2.Enabled = true;
            }
        }

        private void chk_ICat3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat3.Checked == true)
            {
                txtIcat3.Text = "";
                txtIcat3.Enabled = false;
                btn_Srch_Cat3.Enabled = false;
            }
            else
            {
                txtIcat3.Enabled = true;
                btn_Srch_Cat3.Enabled = true;
            }
        }

        private void chk_Item_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Item.Checked == true)
            {
                txtItemCode.Text = "";
                txtItemCode.Enabled = false;
                btn_Srch_Itm.Enabled = false;
            }
            else
            {
                txtItemCode.Enabled = true;
                btn_Srch_Itm.Enabled = true;
            }
        }

        private void chk_Brand_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Brand.Checked == true)
            {
                txtBrand.Text = "";
                txtBrand.Enabled = false;
                btn_Srch_Brnd.Enabled = false;
            }
            else
            {
                txtBrand.Enabled = true;
                btn_Srch_Brnd.Enabled = true;
            }
        }

        private void chk_Model_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Model.Checked == true)
            {
                txtModel.Text = "";
                txtModel.Enabled = false;
                btn_Srch_Model.Enabled = false;
            }
            else
            {
                txtModel.Enabled = true;
                btn_Srch_Model.Enabled = true;
            }
        }

        private void btn_Srch_Cat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat1;
            _CommonSearch.txtSearchbyword.Text = txtIcat1.Text;
            _CommonSearch.ShowDialog();
            txtIcat1.Focus();
        }

        private void btn_Srch_Cat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat2;
            _CommonSearch.txtSearchbyword.Text = txtIcat2.Text;
            _CommonSearch.ShowDialog();
            txtIcat2.Focus();
        }

        private void btn_Srch_Cat3_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat3;
            _CommonSearch.txtSearchbyword.Text = txtIcat3.Text;
            _CommonSearch.ShowDialog();
            txtIcat3.Focus();
        }

        private void txtIcat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat1_Click(null, null);
            }
        }

        private void txtIcat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat2_Click(null, null);
            }
        }


        private void btn_Srch_Itm_Click(object sender, EventArgs e)
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
            txtItemCode.Focus();
        }


        private void btn_Srch_Brnd_Click(object sender, EventArgs e)
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

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Brnd_Click(null, null);
            }
        }

        private void btn_Srch_Model_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtModel.Focus();
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Model_Click(null, null);
            }
        }

        private void btn_Srch_Doc_Tp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocType;
            _CommonSearch.txtSearchbyword.Text = txtDocType.Text;
            _CommonSearch.ShowDialog();
            txtDocType.Focus();
        }

        private void btn_Srch_DocSubTp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
            DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocSubType;
            _CommonSearch.ShowDialog();
            txtDocSubType.Focus();
        }

        private void txtDocType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Doc_Tp_Click(null, null);
            }
        }

        private void txtDocSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_DocSubTp_Click(null, null);
            }
        }

        private void opt30_CheckedChanged(object sender, EventArgs e)
        {
            if (opt30.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6030))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6030)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(30);
            }
        }

        private void opt31_CheckedChanged(object sender, EventArgs e)
        {
            if (opt31.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6031))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6031)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(31);
            }
        }

        private void opt32_CheckedChanged(object sender, EventArgs e)
        {
            if (opt32.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6032))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6032)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(32);
            }
        }


        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
        }

        private void txtAsAtDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
        }

        private void opt33_CheckedChanged(object sender, EventArgs e)
        {
            if (opt33.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6033))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6033)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(33);
            }
        }

        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtAsAtDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void opt34_CheckedChanged(object sender, EventArgs e)
        {
            if (opt34.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6034))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6034)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(34);
            }
        }

        private void btnCat1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat1.Text))
            {
                for (int i = 0; i < lstCat1.Items.Count; i++)
                {
                    if (lstCat1.Items[i].Text == txtIcat1.Text)
                        return;
                }
                lstCat1.Items.Add((txtIcat1.Text).ToString());
                txtIcat1.Text = "";
            }

        }

        private void lstCat1_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat1.Items.Count; i++)
            {
                if (lstCat1.Items[i].Selected)
                {
                    lstCat1.Items[i].Remove();
                    i--;
                }
            }
        }

        private void btnCat2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat2.Text))
            {
                for (int i = 0; i < lstCat2.Items.Count; i++)
                {
                    if (lstCat2.Items[i].Text == txtIcat2.Text)
                        return;
                }
                lstCat2.Items.Add((txtIcat2.Text).ToString());
                txtIcat2.Text = "";
            }
        }

        private void btnCat3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat3.Text))
            {
                for (int i = 0; i < lstCat3.Items.Count; i++)
                {
                    if (lstCat3.Items[i].Text == txtIcat3.Text)
                        return;
                }
                lstCat3.Items.Add((txtIcat3.Text).ToString());
                txtIcat3.Text = "";
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                for (int i = 0; i < lstItem.Items.Count; i++)
                {
                    if (lstItem.Items[i].Text == txtItemCode.Text)
                        return;
                }
                lstItem.Items.Add((txtItemCode.Text).ToString());
                txtItemCode.Text = "";
            }
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                for (int i = 0; i < lstBrand.Items.Count; i++)
                {
                    if (lstBrand.Items[i].Text == txtBrand.Text)
                        return;
                }
                lstBrand.Items.Add((txtBrand.Text).ToString());
                txtBrand.Text = "";
            }
        }


        private void lstCat1_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat1.Items.Count; i++)
            {
                if (lstCat1.Items[i].Selected)
                {
                    lstCat1.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstCat2_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat2.Items.Count; i++)
            {
                if (lstCat2.Items[i].Selected)
                {
                    lstCat2.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstCat3_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat3.Items.Count; i++)
            {
                if (lstCat3.Items[i].Selected)
                {
                    lstCat3.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstItem_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstItem.Items.Count; i++)
            {
                if (lstItem.Items[i].Selected)
                {
                    lstItem.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstBrand_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstBrand.Items.Count; i++)
            {
                if (lstBrand.Items[i].Selected)
                {
                    lstBrand.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstGroup_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Selected)
                {
                    lstGroup.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT1")
                    return;
            }
            lstGroup.Items.Add(("CAT1").ToString());
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT2")
                    return;
            }
            lstGroup.Items.Add(("CAT2").ToString());
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT3")
                    return;
            }
            lstGroup.Items.Add(("CAT3").ToString());
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "ITM")
                    return;
            }
            lstGroup.Items.Add(("ITM").ToString());
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "BRND")
                    return;
            }
            lstGroup.Items.Add(("BRND").ToString());
        }


        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Model_Click(null, null);
        }

        private void txtBrand_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Brnd_Click(null, null);
            }
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Brnd_Click(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Click(null, null);
        }

        private void txtItemCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Itm_Click(null, null);
            }
        }

        private void txtIcat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat3_Click(null, null);
        }

        private void txtIcat3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat3_Click(null, null);
            }
        }

        private void txtIcat2_DoubleClick(object sender, EventArgs e)
        {

            btn_Srch_Cat2_Click(null, null);

        }

        private void txtIcat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat1_Click(null, null);
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtAreaDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();


            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtRegDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtZoneDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();

            load_PCDesc();
        }

        private void set_GroupOrder()
        {
            int i = 1;
            int j = lstGroup.Items.Count;
            BaseCls.GlbReportGroupProfit = 0;
            BaseCls.GlbReportGroupDOLoc = 0;
            BaseCls.GlbReportGroupDocType = 0;
            BaseCls.GlbReportGroupCustomerCode = 0;
            BaseCls.GlbReportGroupExecCode = 0;
            BaseCls.GlbReportGroupItemCode = 0;
            BaseCls.GlbReportGroupBrand = 0;
            BaseCls.GlbReportGroupModel = 0;
            BaseCls.GlbReportGroupItemCat1 = 0;
            BaseCls.GlbReportGroupItemCat2 = 0;
            BaseCls.GlbReportGroupItemCat3 = 0;
            BaseCls.GlbReportGroupItemCat4 = 0;
            BaseCls.GlbReportGroupItemCat5 = 0;
            BaseCls.GlbReportGroupLastGroup = 0;
            BaseCls.GlbReportGroupInvoiceNo = 0;
            BaseCls.GlbReportGroupItemStatus = 0;
            BaseCls.GlbReportGroupPromotor = 0;
            BaseCls.GlbReportGroupJobNo = 0;
            BaseCls.GlbReportGroupColor = 0;
            BaseCls.GlbReportGroupSize = 0;
            BaseCls.GlbReportGroupLastGroupCat = "";
            BaseCls.GlbReportGroupByNationality = 0;
            BaseCls.GlbReportGroupByDistrict = 0;
            BaseCls.GlbReportGroupByProvince = 0;
            BaseCls.GlbReportGroupByCity = 0;

            foreach (ListViewItem Item in lstGroup.Items)
            {
                if (Item.Text == "PC")
                {
                    BaseCls.GlbReportGroupProfit = i;
                }
                if (Item.Text == "DLOC")
                {
                    BaseCls.GlbReportGroupDOLoc = i;
                }
                if (Item.Text == "DTP")
                {
                    BaseCls.GlbReportGroupDocType = i;
                }
                if (Item.Text == "CUST")
                {
                    BaseCls.GlbReportGroupCustomerCode = i;
                }
                if (Item.Text == "EXEC")
                {
                    BaseCls.GlbReportGroupExecCode = i;
                }
                if (Item.Text == "ITM")
                {
                    BaseCls.GlbReportGroupItemCode = i;
                }
                if (Item.Text == "BRND")
                {
                    BaseCls.GlbReportGroupBrand = i;
                }
                if (Item.Text == "MDL")
                {
                    BaseCls.GlbReportGroupModel = i;
                }
                if (Item.Text == "CAT1")
                {
                    BaseCls.GlbReportGroupItemCat1 = i;
                }
                if (Item.Text == "CAT2")
                {
                    BaseCls.GlbReportGroupItemCat2 = i;
                }
                if (Item.Text == "CAT3")
                {
                    BaseCls.GlbReportGroupItemCat3 = i;
                }
                if (Item.Text == "CAT4")
                {
                    BaseCls.GlbReportGroupItemCat4 = i;
                }
                if (Item.Text == "CAT5")
                {
                    BaseCls.GlbReportGroupItemCat5 = i;
                }
                if (Item.Text == "INV")
                {
                    BaseCls.GlbReportGroupInvoiceNo = i;
                }
                if (Item.Text == "STK")
                {
                    BaseCls.GlbReportGroupItemStatus = i;
                }
                if (Item.Text == "PRM")
                {
                    BaseCls.GlbReportGroupPromotor = i;
                }
                if (Item.Text == "JOB")
                {
                    BaseCls.GlbReportGroupJobNo = i;
                }
                if (Item.Text == "CLR")
                {
                    BaseCls.GlbReportGroupColor = i;
                }
                if (Item.Text == "SIZE")
                {
                    BaseCls.GlbReportGroupSize = i;
                }
                if (Item.Text == "NAT")
                {
                    BaseCls.GlbReportGroupByNationality = i;
                }
                if (Item.Text == "DIST")
                {
                    BaseCls.GlbReportGroupByDistrict = i;
                }
                if (Item.Text == "PROV")
                {
                    BaseCls.GlbReportGroupByProvince = i;
                }
                if (Item.Text == "CITY")
                {
                    BaseCls.GlbReportGroupByCity = i;
                }

                BaseCls.GlbReportGroupLastGroup = j;
                if (j == i)
                {
                    BaseCls.GlbReportGroupLastGroupCat = Item.Text;
                }
                i++;
            }
            if (j == 0)
            {
                BaseCls.GlbReportGroupProfit = 1;
                BaseCls.GlbReportGroupItemCode = 2;
                BaseCls.GlbReportGroupLastGroup = 2;
                BaseCls.GlbReportGroupLastGroupCat = "ITM";
            }
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "MDL")
                    return;
            }
            lstGroup.Items.Add(("MDL").ToString());
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "PC")
                    return;
            }
            lstGroup.Items.Add(("PC").ToString());
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DTP")
                    return;
            }
            lstGroup.Items.Add(("DTP").ToString());
        }

        private void lbl9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DSTP")
                    return;
            }
            lstGroup.Items.Add(("DSTP").ToString());
        }

        private void lbl10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CUST")
                    return;
            }
            lstGroup.Items.Add(("CUST").ToString());
        }

        private void chk_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Cust.Checked == true)
            {
                txtCust.Text = "";
                txtCust.Enabled = false;
                btn_Srch_Cust.Enabled = false;
            }
            else
            {
                txtCust.Enabled = true;
                btn_Srch_Cust.Enabled = true;
            }
        }

        private void btn_Srch_Cust_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCust;
            _CommonSearch.ShowDialog();
            txtCust.Select();
        }

        private void lbl11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "EXEC")
                    return;
            }
            lstGroup.Items.Add(("EXEC").ToString());
        }

        private void btn_Srch_Exec_Click(object sender, EventArgs e)
        {
            if (radDailyColl.Checked)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExec;
                _CommonSearch.ShowDialog();
                txtExec.Select();
            }
            else
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                if (_result == null || _result.Rows.Count <= 0)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                    _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExec;
                _CommonSearch.ShowDialog();
                txtExec.Select();
            }
        }

        private void lbl12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "STK")
                    return;
            }
            lstGroup.Items.Add(("STK").ToString());
        }

        private void btn_Srch_Itm_Stus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemStatus;
            _CommonSearch.ShowDialog();
            txtItemStatus.Focus();
        }

        private void lbl13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "INV")
                    return;
            }
            lstGroup.Items.Add(("INV").ToString());
        }

        private void chk_Exec_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Exec.Checked == true)
            {
                txtExec.Text = "";
                txtExec.Enabled = false;
                btn_Srch_Exec.Enabled = false;
            }
            else
            {
                txtExec.Enabled = true;
                btn_Srch_Exec.Enabled = true;
            }
        }

        private void opt35_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt36_CheckedChanged(object sender, EventArgs e)
        {
            if (opt36.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6036))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6036)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(36);
            }
        }

        private void opt37_CheckedChanged(object sender, EventArgs e)
        {
            if (opt37.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6037))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6037)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(37);
            }
        }

        private void opt38_CheckedChanged(object sender, EventArgs e)
        {
            if (opt38.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6038))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6038)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(38);
            }
        }

        private void chk_promotor_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_promotor.Checked == true)
            {
                txt_promotor.Text = "";
                txt_promotor.Enabled = false;
                btn_promotor.Enabled = false;
            }
            else
            {
                txt_promotor.Enabled = true;
                btn_promotor.Enabled = true;
            }
        }

        private void chk_Itm_Stus_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Itm_Stus.Checked == true)
            {
                txtItemStatus.Text = "";
                txtItemStatus.Enabled = false;
                btn_Srch_Itm_Stus.Enabled = false;
            }
            else
            {
                txtItemStatus.Enabled = true;
                btn_Srch_Itm_Stus.Enabled = true;
            }
        }

        private void btn_Srch_Prefix_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix);
            DataTable _result = CHNLSVC.CommonSearch.GetPrefixData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPrefix;
            _CommonSearch.ShowDialog();
            txtPrefix.Focus();
        }

        private void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrefix.Checked == true)
            {
                txtPrefix.Text = "";
                txtPrefix.Enabled = false;
                btn_Srch_Prefix.Enabled = false;
            }
            else
            {
                txtPrefix.Enabled = true;
                btn_Srch_Prefix.Enabled = true;
            }
        }

        private void txtPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Prefix_Click(null, null);
            }
        }

        private void txtPrefix_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Prefix_Click(null, null);
        }

        private void txtCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cust_Click(null, null);
            }
        }

        private void txtCust_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cust_Click(null, null);
        }

        private void opt39_CheckedChanged(object sender, EventArgs e)
        {
            if (opt39.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6039))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6039)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(39);
            }
        }

        private void opt40_CheckedChanged(object sender, EventArgs e)
        {
            if (opt40.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6040))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6040)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(40);
            }
        }

        private void opt41_CheckedChanged(object sender, EventArgs e)
        {
            if (opt41.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6041))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6041)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(41);
            }
        }

        private void chk_Pay_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Pay_Tp.Checked == true)
            {
                comboBoxPayModes.SelectedIndex = -1;
                comboBoxPayModes.Enabled = false;

            }
            else
            {
                comboBoxPayModes.Enabled = true;

            }
        }

        private void opt42_CheckedChanged(object sender, EventArgs e)
        {
            if (opt42.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6042))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6042)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(42);
            }
        }

        private void chkAsAtDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsAtDate.Text == "Run as at Date")
            {
                if (opt74.Checked == false)
                {
                    if (chkAsAtDate.Checked == false)
                    {
                        txtAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                    }
                    else
                    {
                        txtAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                    }
                }
            }
        }

        private void btn_Srch_Doc_Click(object sender, EventArgs e)
        {
            if (label18.Text == "Circular No")
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocNo;
                _CommonSearch.ShowDialog();
                txtDocNo.Select();
            }
        }

        private void btn_PB_Click(object sender, EventArgs e)
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
        }

        private void btn_PBLevel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPBLevel;
            _CommonSearch.ShowDialog();
            txtPBLevel.Select();
        }

        private void btn_Srch_Dir_Click(object sender, EventArgs e)
        {
            if (label16.Text == "Promo Code")
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromoByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromoByComp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDirec;
                _CommonSearch.ShowDialog();
                txtDirec.Select();
            }
        }

        private void chk_PB_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PB.Checked == true)
            {
                txtPB.Text = "";
                txtPB.Enabled = false;
                btn_PB.Enabled = false;
            }
            else
            {
                txtPB.Enabled = true;
                btn_PB.Enabled = true;
            }
        }

        private void chk_PBLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PBLevel.Checked == true)
            {
                txtPBLevel.Text = "";
                txtPBLevel.Enabled = false;
                btn_PBLevel.Enabled = false;
            }
            else
            {
                txtPBLevel.Enabled = true;
                btn_PBLevel.Enabled = true;
            }
        }

        private void lbl14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DLOC")
                    return;
            }
            lstGroup.Items.Add(("DLOC").ToString());
        }

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Doc_Click(null, null);
            }
        }

        private void txtDocNo_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Doc_Click(null, null);
        }

        private void txtDirec_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Dir_Click(null, null);
        }

        private void txtDirec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Dir_Click(null, null);
            }
        }



        private void update_PC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                List<string> tmpList = new List<string>();
                tmpList = Item.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                string pc = null;
                string com = txtComp.Text;
                if ((tmpList != null) && (tmpList.Count > 0))
                {
                    pc = tmpList[0];
                    com = tmpList[1];
                }


                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, com, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        private void opt43_CheckedChanged(object sender, EventArgs e)
        {
            if (opt43.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6043))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6043)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(43);
            }
        }

        private void opt44_CheckedChanged(object sender, EventArgs e)
        {
            if (opt44.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6044))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6044)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(44);
            }
        }

        private void opt45_CheckedChanged(object sender, EventArgs e)
        {
            if (opt45.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6045))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6045)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(45);
            }
        }

        private void opt46_CheckedChanged(object sender, EventArgs e)
        {
            if (opt46.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6046))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6046)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(46);
            }
        }

        private void optLs_CheckedChanged(object sender, EventArgs e)
        {
            if (optLs.Checked == true)
            {
                optEq.Checked = false;
                optGt.Checked = false;
                txtDiscRate.Enabled = true;
            }
        }

        private void optEq_CheckedChanged(object sender, EventArgs e)
        {
            if (optEq.Checked == true)
            {
                optLs.Checked = false;
                optGt.Checked = false;
                txtDiscRate.Enabled = true;
            }
        }

        private void optGt_CheckedChanged(object sender, EventArgs e)
        {
            if (optGt.Checked == true)
            {
                optLs.Checked = false;
                optEq.Checked = false;
                txtDiscRate.Enabled = true;
            }
        }

        private void optAll_CheckedChanged(object sender, EventArgs e)
        {

            if (optAll.Checked == true)
            {
                txtDiscRate.Text = "";
                txtDiscRate.Enabled = false;
            }
        }

        private void opt47_CheckedChanged(object sender, EventArgs e)
        {
            if (opt47.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6047))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6047)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(47);
            }
        }

        private void opt48_CheckedChanged(object sender, EventArgs e)
        {
            if (opt48.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6048))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6048)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(48);
            }
        }

        private void chk_Stus_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Stus.Checked == true)
            {
                cmb_Stus.SelectedIndex = -1;
                cmb_Stus.Enabled = false;

            }
            else
            {
                cmb_Stus.Enabled = true;

            }
        }

        private void chk_Sup_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Sup.Checked == true)
            {
                txtSup.Text = "";
                txtSup.Enabled = false;
                btn_Srch_Sup.Enabled = false;
            }
            else
            {
                txtSup.Enabled = true;
                btn_Srch_Sup.Enabled = true;
            }
        }

        private void btn_Srch_Sup_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSup;
            _CommonSearch.ShowDialog();
            txtSup.Select();
        }

        private void chk_PO_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PO.Checked == true)
            {
                txtPO.Text = "";
                txtPO.Enabled = false;
                btn_Srch_PO.Enabled = false;
            }
            else
            {
                txtPO.Enabled = true;
                btn_Srch_PO.Enabled = true;
            }
        }

        private void btn_Srch_PO_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
            DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPO;
            _CommonSearch.ShowDialog();
            txtPO.Select();
        }

        private void opt49_CheckedChanged(object sender, EventArgs e)
        {
            if (opt49.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6049))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6049)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(49);
            }
        }

        private void opt50_CheckedChanged(object sender, EventArgs e)
        {
            if (opt50.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6050))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6050)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(50);
            }
        }

        private void opt51_CheckedChanged(object sender, EventArgs e)
        {
            if (opt51.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6051))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6051)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(51);
            }
        }

        private void opt52_CheckedChanged(object sender, EventArgs e)
        {
            if (opt52.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6052))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6052)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(52);
            }
        }

        private void opt53_CheckedChanged(object sender, EventArgs e)
        {
            if (opt53.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6053))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6053)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(53);
            }
        }

        private void opt54_CheckedChanged(object sender, EventArgs e)
        {
            if (opt54.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6054))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6054)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(54);
            }
        }

        private void opt55_CheckedChanged(object sender, EventArgs e)
        {
            if (opt55.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6055))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6055)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(55);
            }
        }

        private void opt56_CheckedChanged(object sender, EventArgs e)
        {
            if (opt56.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6056))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6056)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(56);
            }
        }

        private void opt57_CheckedChanged(object sender, EventArgs e)
        {
            if (opt57.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6057))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6057)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(57);
            }
        }


        private void fldgOpenPath_FileOk(object sender, CancelEventArgs e)
        {
            BaseCls.GlbReportFilePath = fldgOpenPath.FileName;
        }

        private void opt60_CheckedChanged(object sender, EventArgs e)
        {
            if (opt60.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6060))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6060)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(60);
            }
        }

        private void opt58_CheckedChanged(object sender, EventArgs e)
        {
            if (opt58.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6058))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6058)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(58);
            }
        }

        private void opt59_CheckedChanged(object sender, EventArgs e)
        {
            if (opt59.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6059))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6059)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(59);
            }
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            load_PCDesc();
        }

        private void load_PCDesc()
        {
            txtPCDesn.Text = "";
            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtPC.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtPCDesn.Text = row2["descp"].ToString();
            }
        }

        private void opt61_CheckedChanged(object sender, EventArgs e)
        {
            if (opt61.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6061))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6061)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(61);
            }
        }

        private void opt62_CheckedChanged(object sender, EventArgs e)
        {
            if (opt62.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6062))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6062)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(62);
            }
        }

        private void opt63_CheckedChanged(object sender, EventArgs e)
        {
            if (opt63.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL17"))

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6063))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6063)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(63);
            }
        }

        private void radDailyColl_CheckedChanged(object sender, EventArgs e)
        {
            if (radDailyColl.Checked)
            {
                setFormControls(64);
            }

        }

        private void opt65_CheckedChanged(object sender, EventArgs e)
        {
            if (opt65.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6065))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6065)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(65);
            }
        }

        private void opt66_CheckedChanged(object sender, EventArgs e)
        {
            if (opt66.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6066))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6066)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(66);
            }
        }

        private void opt67_CheckedChanged(object sender, EventArgs e)
        {
            if (opt67.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6067))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6067)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(67);
            }
        }

        private void opt68_CheckedChanged(object sender, EventArgs e)
        {
            if (opt68.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6068))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6068)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(68);
            }
        }

        private void opt69_CheckedChanged(object sender, EventArgs e)
        {
            if (opt69.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6069))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6069)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(69);
            }
        }

        private void opt70_CheckedChanged(object sender, EventArgs e)
        {
            if (opt70.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6070))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6070)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(70);
            }

        }

        private void txt_promotor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_promotor_Click(null, null);
            }
        }

        private void btn_promotor_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
            DataTable _result = CHNLSVC.CommonSearch.SearchSalesPromotor(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_promotor;
            _CommonSearch.ShowDialog();
            txt_promotor.Select();
        }

        private void lbl15_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "PRM")
                    return;
            }
            lstGroup.Items.Add(("PRM").ToString());
        }

        private void chk_AppBy_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_AppBy.Checked == true)
            {
                txt_AppBy.Text = "";
                txt_AppBy.Enabled = false;
                btn_AppBy.Enabled = false;
            }
            else
            {
                txt_AppBy.Enabled = true;
                btn_AppBy.Enabled = true;
            }
        }

        private void opt71_CheckedChanged(object sender, EventArgs e)
        {
            if (opt71.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6071))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6071)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(71);
            }
        }

        private void opt72_CheckedChanged(object sender, EventArgs e)
        {
            if (opt72.Checked == false) pnlRegReport.Visible = false;
            if (opt72.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6072))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6072)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(72);
            }
        }

        private void opt73_CheckedChanged(object sender, EventArgs e)
        {
            if (opt73.Checked == true)
            {

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6073))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6073)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(73);
            }
        }

        private void opt74_CheckedChanged(object sender, EventArgs e)
        {
            if (opt74.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6074))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6074)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(74);
            }
        }

        private void opt75_CheckedChanged(object sender, EventArgs e)
        {
            if (opt75.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6075))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6075)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(75);
            }
        }

        private void opt76_CheckedChanged(object sender, EventArgs e)
        {
            if (opt76.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6076))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6076)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(76);
            }
        }

        private void opt77_CheckedChanged(object sender, EventArgs e)
        {
            if (opt77.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6077))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6077)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(77);
            }
        }

        private void opt78_CheckedChanged(object sender, EventArgs e)
        {
            if (opt78.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6078))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6078)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(78);
            }
        }

        private void opt79_CheckedChanged(object sender, EventArgs e)
        {
            if (opt79.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6079))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6079)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(79);
            }
        }


        private void lbl16_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "JOB")
                    return;
            }
            lstGroup.Items.Add(("JOB").ToString());
        }

        private void chkJobno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJobno.Checked == true)
            {
                txtJobno.Text = "";
                txtJobno.Enabled = false;
                btnJobno.Enabled = false;
            }
            else
            {
                txtJobno.Enabled = true;
                btnJobno.Enabled = true;
            }
        }

        private void opt80_CheckedChanged_1(object sender, EventArgs e)
        {
            if (opt80.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6080))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6080)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(80);
            }
        }

        private void opt81_CheckedChanged(object sender, EventArgs e)
        {
            if (opt81.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6081))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6081)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(81);
            }
        }

        private void lbl17_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT4")
                    return;
            }
            lstGroup.Items.Add(("CAT4").ToString());
        }

        private void lbl18_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT5")
                    return;
            }
            lstGroup.Items.Add(("CAT5").ToString());
        }


        private void chk_ICat4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat4.Checked == true)
            {
                txtIcat4.Text = "";
                txtIcat4.Enabled = false;
                btn_Srch_Cat4.Enabled = false;
            }
            else
            {
                txtIcat4.Enabled = true;
                btn_Srch_Cat4.Enabled = true;
            }
        }

        private void chk_ICat5_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat5.Checked == true)
            {
                txtIcat5.Text = "";
                txtIcat5.Enabled = false;
                btn_Srch_Cat5.Enabled = false;
            }
            else
            {
                txtIcat5.Enabled = true;
                btn_Srch_Cat5.Enabled = true;
            }
        }

        private void opt82_CheckedChanged(object sender, EventArgs e)
        {
            if (opt82.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6082))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6082)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(82);
            }
        }

        private void opt83_CheckedChanged(object sender, EventArgs e)
        {
            if (opt83.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6083))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6083)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(83);
            }
        }

        private void opt84_CheckedChanged(object sender, EventArgs e)
        {
            if (opt84.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6084))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6084)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(84);
            }
        }

        private void opt85_CheckedChanged(object sender, EventArgs e)
        {
            if (opt85.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6085))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6085)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(85);
            }
        }


        private void btn_supplier_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_supplier;
            _CommonSearch.ShowDialog();
            txt_supplier.Select();
        }

        private void chk_supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_supplier.Checked == true)
            {
                txt_supplier.Text = "";
                txt_supplier.Enabled = false;
                btn_supplier.Enabled = false;
            }
            else
            {
                txt_supplier.Enabled = true;
                btn_supplier.Enabled = true;
            }
        }
        //ADDED BY HASITH
        private void opt89_CheckedChanged(object sender, EventArgs e)
        {
            if (opt89.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16042))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16042)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(89);
            }
        }

        private void opt86_CheckedChanged(object sender, EventArgs e)
        {
            if (opt86.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6086))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6086)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(86);
            }
        }

        private void opt87_CheckedChanged(object sender, EventArgs e)
        {
            if (opt87.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6087))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6087)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(87);
            }
        }

        private void opt88_CheckedChanged(object sender, EventArgs e)
        {
            if (opt88.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6088))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6088)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(88);
            }
        }

        private void opt90_CheckedChanged(object sender, EventArgs e)
        {
            if (opt90.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6090))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6090)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(90);
            }
        }

        private void btn_close_admin_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstAdmin.Items)
            {
                Item.Checked = false;
            }
            pnlAdmin.Visible = false;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            //lstAdmin.Clear();
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10140))
            {
                MessageBox.Show("Sorry, You have no permission to view admin team!\n( Advice: Required permission code :10140 )");
                return;
            }
            if (chkAllComp.Checked)
            {
                _userCompany = "";
                if (BaseCls.GlbUserComCode == "ABL")
                {
                    _userCompany = "LRP";
                }
                if (BaseCls.GlbUserComCode == "SGL")
                {
                    _userCompany = "SGD";
                }
                BindAdminTeam();
            }
            pnlAdmin.Visible = true;
        }

        private void BindAdminTeam()
        {
            DataTable dt = new DataTable();
            string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
            dt = CHNLSVC.CommonSearch.GetAdminTeamByCompany(para, null, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    if (!lstAdmin.Items.Equals(drow["mso_cd"].ToString()))
                        lstAdmin.Items.Add(drow["mso_cd"].ToString());
                }
            }

        }

        private void lstAdmin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (_isMultiAdminTeam == false)
            {
                foreach (ListViewItem lstItem in lstAdmin.Items)
                {
                    if (lstItem.Text != e.Item.Text)
                    {
                        lstItem.Checked = false;
                    }
                }
            }
        }

        private void opt91_CheckedChanged(object sender, EventArgs e)
        {
            if (opt91.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6091))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6091)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(91);
            }
        }

        private void opt92_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt92.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11500))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11500 )");
                        opt92.Checked = false;
                        return;
                    }
                    setFormControls(92);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt93_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt93.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6093))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6093 )");
                        opt93.Checked = false;
                        return;
                    }
                    setFormControls(93);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkAllComp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllComp.Checked)
            {
                txtPC.Text = "";
                lstPC.Clear();
            }
        }

        private void opt94_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt94.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6094))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6094 )");
                        opt94.Checked = false;
                        return;
                    }
                    setFormControls(94);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt95_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt95.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6095))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6095 )");
                        opt95.Checked = false;
                        return;
                    }
                    setFormControls(95);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt96_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt96.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6096))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6096 )");
                        opt96.Checked = false;
                        return;
                    }
                    setFormControls(96);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt98_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(98);
        }

        private void chk_color_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_color.Checked == true)
            {
                txt_color.Text = "";
                txt_color.Enabled = false;
                btn_color.Enabled = false;
            }
            else
            {
                txt_color.Enabled = true;
                btn_color.Enabled = true;
            }
        }

        private void chk_size_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_size.Checked == true)
            {
                txt_size.Text = "";
                txt_size.Enabled = false;
                btn_size.Enabled = false;
            }
            else
            {
                txt_size.Enabled = true;
                btn_size.Enabled = true;
            }
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_color;
            _CommonSearch.txtSearchbyword.Text = txt_color.Text;
            _CommonSearch.ShowDialog();
            txt_color.Focus();
        }

        private void lbl19_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CLR")
                    return;
            }
            lstGroup.Items.Add(("CLR").ToString());
        }

        private void lbl20_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "SIZE")
                    return;
            }
            lstGroup.Items.Add(("SIZE").ToString());
        }

        private void opt99_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt99.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11518))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11518 )");
                        opt99.Checked = false;
                        return;
                    }
                    setFormControls(99);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void lstAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void opt100_CheckedChanged(object sender, EventArgs e)
        {
            if (opt100.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL10"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11519))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11519)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(10);
            }
        }

        //Tharindu 2017/11/08
        //Sales With Current Inventory balance Report
        private void opt101_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt101.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6101))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6101)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }

                    setFormControls(101);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Sales With Current Inventory balance Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt102_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt102.Checked == true)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6101))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6101)");
                    //    RadioButton RDO = (RadioButton)sender;
                    //    RDO.Checked = false;
                    //    return;
                    //}

                    setFormControls(102);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Sales Summary Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void lblGrpNationality_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "NAT")
                    return;
            }
            lstGroup.Items.Add(("NAT").ToString());
        }

        private void lblGrpProvince_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "PROV")
                    return;
            }
            lstGroup.Items.Add(("PROV").ToString());
        }

        private void lblGrpDistrict_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DIST")
                    return;
            }
            lstGroup.Items.Add(("DIST").ToString());
        }

        private void lblGrpCity_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CITY")
                    return;
            }
            lstGroup.Items.Add(("CITY").ToString());
        }

        private void btnSearchNationality_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsReturnFullRow = false;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Nationality);
                DataTable _result = CHNLSVC.CommonSearch.SearchNationality(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtNationality;
                _CommonSearch.ShowDialog();
                txtNationality.Focus();
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

        private void btn_size_Click(object sender, EventArgs e)
        {

        }

        private void btn_CustTp_Click(object sender, EventArgs e)
        {

        }

        private void btnJobno_Click(object sender, EventArgs e)
        {

        }

        private void txtNationality_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchNationality_Click(null, null);
            }
        }

        private void chkNationality_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNationality.Checked)
            {
                txtNationality.Text = string.Empty;
                txtNationality.Enabled = false;
                btnSearchNationality.Enabled = false;
            }
            else
            {
                txtNationality.Text = string.Empty;
                txtNationality.Enabled = true;
                btnSearchNationality.Enabled = true;
            }
        }

        private void chkProvince_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProvince.Checked)
            {
                txtProvince.Text = string.Empty;
                txtProvince.Enabled = false;
                btnSearchProvince.Enabled = false;
            }
            else
            {
                txtProvince.Text = string.Empty;
                txtProvince.Enabled = true;
                btnSearchProvince.Enabled = true;
            }
        }

        private void chkDistrict_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDistrict.Checked)
            {
                txtDistrict.Text = string.Empty;
                txtDistrict.Enabled = false;
                btnSearchDistrict.Enabled = false;
            }
            else
            {
                txtDistrict.Text = string.Empty;
                txtDistrict.Enabled = true;
                btnSearchDistrict.Enabled = true;
            }
        }

        private void chkCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCity.Checked)
            {
                txtCity.Text = string.Empty;
                txtCity.Enabled = false;
                btnSearchCity.Enabled = false;
            }
            else
            {
                txtCity.Text = string.Empty;
                txtCity.Enabled = true;
                btnSearchCity.Enabled = true;
            }
        }

        private void btnSearchCity_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCity;
                _CommonSearch.ShowDialog();
                txtCity.Focus();
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

        private void btnSearchProvince_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProvince;
                _CommonSearch.ShowDialog();
                txtProvince.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchDistrict_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DistrictCode);
                DataTable _result = CHNLSVC.CommonSearch.SearchDistrictDetails(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDistrict;
                _CommonSearch.ShowDialog();
                txtDistrict.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchDistrict_Click(null, null);
            }
        }

        private void txtProvince_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchProvince_Click(null, null);
            }
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchCity_Click(null, null);
            }
        }

        private void opt103_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt103.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11542))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11542 )");
                        opt99.Checked = false;
                        return;
                    }
                    setFormControls(103);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void rdiisdate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdiisdate.Checked == true)
            {

                pnldatewithtime.Visible = false;
                pnlDateRange.Visible = true;
            }
            else
            {
                pnldatewithtime.Visible = true;
                this.pnldatewithtime.Location = new System.Drawing.Point(590, 220);
                pnlDateRange.Visible = false;
            }
        }

        private void rdiisdateandtime_CheckedChanged(object sender, EventArgs e)
        {
            if (rdiisdateandtime.Checked == true)
            {
                this.pnldatewithtime.Location = new System.Drawing.Point(590, 220);
                pnldatewithtime.Visible = true;
                pnlDateRange.Visible = false;
            }
            else
            {
                pnldatewithtime.Visible = false;
                pnlDateRange.Visible = true;
            }
        }

        private void opt104_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt104.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16123))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11542 )");
                        opt104.Checked = false;
                        return;
                    }
                    setFormControls(104);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt105_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt105.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16122))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11542 )");
                        opt105.Checked = false;
                        return;
                    }
                    setFormControls(105);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt106_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt106.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16121))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11542 )");
                        opt99.Checked = false;
                        return;
                    }
                    setFormControls(106);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSQtyFrom_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSQtyFrom.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtSQtyFrom.Text = txtSQtyFrom.Text.Remove(txtSQtyFrom.Text.Length - 1);
            }
        }

        private void txtSQtyTo_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSQtyTo.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtSQtyTo.Text = txtSQtyTo.Text.Remove(txtSQtyTo.Text.Length - 1);
            }
        }

    }
}


