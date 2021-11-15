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
using FF.WindowsERPClient.Reports.Inventory;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;

//Written By Nadeeka on 26/12/2012
namespace FF.WindowsERPClient.Reports.Inventory
{
    public partial class Inv_Rep : Base
    {
        private string _userCompany = "";
        private string companyAllList = ""; // Added by Chathura on 09-nov-2017
        private string docSubTypesList = ""; // Added by Chathura on 09-nov-2017
        public bool CheckPermission = true; //by akila 2017/05/12
        public Inv_Rep()
        {
            try
            {
                _userCompany = BaseCls.GlbUserComCode;
                InitializeComponent();
                InitializeEnv();
                GetCompanyDet(null, null);
                GetPCDet(null, null);
                setFormControls(0);
                BindAdminTeam();
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

        private void BindAdminTeam()
        {
            //DataTable dt = new DataTable();
            //string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdminTeam);
            //dt = CHNLSVC.CommonSearch.GetAdminTeamByCompany(para, null, null);
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in dt.Rows)
            //    {
            //        lstAdmin.Items.Add(drow["mso_cd"].ToString());
            //    }
            //}

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

        private void BindAllCompanies() // Added by Chathura on 09-nov-2017
        {
            DataTable dt = new DataTable();
            dt = CHNLSVC.General.GetAllCompanies();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    lstMulCom.Items.Add(drow["mc_cd"].ToString());
                }
            }

        }


        private void setFormControls(Int32 _index)
        {
            chk_Doc_Tp.Checked = true;
            chk_Doc_Sub_Tp.Checked = true;
            chk_Doc.Checked = true;
            chk_Dir.Checked = true;
            chk_Entry_Tp.Checked = true;
            chkRecType.Checked = true;

            pnlAsAtDate.Enabled = false;
            pnlDateRange.Enabled = true;
            pnlLoc.Enabled = true;
            pnl_Direc.Enabled = false;
            pnl_DocNo.Enabled = false;
            pnl_DocSubType.Enabled = false;
            pnl_DocType.Enabled = false;
            pnl_Entry_Tp.Enabled = false;
            pnl_Rec_Tp.Enabled = false;
            pnl_Item.Enabled = false;
            pnl_DocStatus.Enabled = false;
            pnl_Location.Enabled = false;
            pnl_RepType.Enabled = false;
            chkStatusWise.Enabled = false;
            pnl_git.Enabled = false;
            chk_withRCC.Enabled = false;
            chk_withRCC.Visible = false;
            chk_withRCC.Checked = false;
            pnlSum.Enabled = false;
            pnl_withsupp.Visible = false;
            pnl_withsupp.Enabled = false;
            pnl_supplier.Visible = false;
            pnl_supplier.Enabled = false;
            pnl_PB.Enabled = false;
            pnl_PB.Visible = false;
            pnl_PBLevel.Enabled = false;
            pnl_PBLevel.Visible = false;
            pnlAgeCat.Enabled = false;
            chkCost.Enabled = false;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            pnlHO.Enabled = false;
            pnlHO.Visible = false;
            pnlSum.Visible = false;
            pnlSum.Enabled = false;
            pnlExces.Enabled = false;
            pnl_RepType.Visible = false;
            pnlFOC.Visible = false;
            chk_exportexcel.Enabled = false;
            chk_all_com.Visible = false;
            chk_all_com.Enabled = false;
            optB.Text = "Both";
            optS.Text = "Short";
            optE.Text = "Excess";
            chk_allserial.Visible = false;
            chkWOStus.Enabled = false;
            chkWODet.Enabled = false;
            chkJob.Visible = true;
            chk_allserial.Visible = false;

            pnlLifeSpan.Enabled = false;
            pnlModelIntroduce.Enabled = false;
            pnlLifeSpan.Visible = false;
            pnlModelIntroduce.Visible = false;
            pnl_itmcond.Visible = false;
            pnl_itmcond.Enabled = false;
            pnlDisConItems.Enabled = false;
            pnl_color.Visible = false;
            pnl_color.Enabled = false;
            pnl_size.Visible = false;
            pnl_size.Enabled = false;
            chkroot.Visible = false;
            chkroot.Enabled = false;
            pnlMutiparasel.Visible = false;
            pnlAgeSlot.Enabled = false;

            lstPC.Enabled = true; // Added by Chathura on 09-nov-2017
            btnAddItem.Enabled = true; // Added by Chathura on 09-nov-2017
            pnlMulCom.Visible = false; // Added by Chathura on 09-nov-2017
            pnlSubDocs.Visible = false; // Added by Chathura on 09-nov-2017
            chkmulticompany.Visible = false; // Added by Chathura on 09-nov-2017
            lblAddDocSub.Visible = false; // Added by Chathura on 09-nov-2017
            pnl_ageing.Visible = false;
            chkAllComp.Enabled = false;
            chkAllComp.Checked = false;
            optitmwise.Enabled = false;
            pnlDateRange_2.Visible = false;
            pnlDateRange_2.Enabled = false;
            switch (_index)
            {
                case 1:
                case 2:
                    pnlDateRange.Enabled = true;
                    pnl_DocSubType.Enabled = true;
                    pnl_Direc.Enabled = true;
                    pnl_Item.Enabled = true;
                    pnl_DocType.Enabled = true;
                    pnlSum.Enabled = true;
                    pnlSum.Visible = true;
                    chkCost.Enabled = true;
                    pnlHO.Enabled = true;
                    pnlHO.Visible = true;
                    pnl_DocNo.Enabled = true;
                    pnl_OthLoc.Enabled = true;
                    pnl_OthLoc.Visible = true;
                    pnl_Location.Visible = false;
                    break;
                case 3:
                    {
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        //pnlSum.Enabled = true;
                        // pnlSum.Visible = true;
                        pnlHO.Enabled = true;
                        pnlHO.Visible = true;
                        pnl_DocNo.Enabled = true;
                        pnl_OthLoc.Enabled = true;
                        pnl_OthLoc.Visible = true;
                        pnl_Location.Visible = false;
                        chkCost.Enabled = true;
                        break;
                    }
                case 4:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        chkStatusWise.Enabled = true;
                        pnl_withsupp.Visible = true;
                        pnl_withsupp.Enabled = true;
                        pnl_color.Visible = true;
                        pnl_color.Enabled = true;
                        pnl_size.Visible = true;
                        pnl_size.Enabled = true;
                        break;
                    }
                case 5:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        chkStatusWise.Enabled = true;
                        pnl_withsupp.Visible = true;
                        pnl_withsupp.Enabled = true;
                        pnl_color.Visible = true;
                        pnl_color.Enabled = true;
                        pnl_size.Visible = true;
                        pnl_size.Enabled = true;
                        break;
                    }
                case 6:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        chkStatusWise.Checked = true;
                        //pnl_color.Visible = true;
                        //pnl_color.Enabled = true;
                        //pnl_size.Visible = true;
                        //pnl_size.Enabled = true;
                        break;
                    }
                case 7:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_git.Enabled = true;
                        pnl_withsupp.Visible = true;
                        pnl_withsupp.Enabled = true;
                        pnlLifeSpan.Visible = true;
                        pnlModelIntroduce.Visible = true;
                        pnlLifeSpan.Enabled = true;
                        pnlModelIntroduce.Enabled = true;
                        pnlLifeSpan.Enabled = true;
                        pnlModelIntroduce.Enabled = true;
                        pnlLifeSpan.Visible = true;
                        pnlModelIntroduce.Visible = true;
                        pnlGIT.Enabled = true;
                        chkWOStus.Enabled = true;
                        chkWODet.Enabled = false;
                        pnlDisConItems.Enabled = true;
                        chkWODet.Checked = false;
                        break;
                    }
                case 8:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_git.Enabled = true;
                        pnl_withsupp.Visible = true;
                        pnl_withsupp.Enabled = true;
                        pnlLifeSpan.Enabled = true;
                        pnlModelIntroduce.Enabled = true;
                        pnlLifeSpan.Visible = true;
                        pnlModelIntroduce.Visible = true;
                        pnlDisConItems.Enabled = true;
                        break;
                    }
                case 9:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        chk_withRCC.Enabled = true;
                        chk_withRCC.Visible = true;
                        pnlLifeSpan.Enabled = true;
                        pnlModelIntroduce.Enabled = true;
                        pnlLifeSpan.Visible = true;
                        pnlModelIntroduce.Visible = true;
                        pnlGIT.Enabled = true;
                        chkWOStus.Enabled = false;
                        chkWODet.Enabled = true;
                        pnlDisConItems.Enabled = true;
                        break;
                    }

                case 10:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        DateTime _date = CHNLSVC.Security.GetServerDateTime().Date;
                        txtAsAtDate.Value = _date;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnlHO.Enabled = true;
                        pnlHO.Visible = true;
                        break;
                    }
                case 12:
                    {

                        pnlDateRange.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnlSum.Enabled = true;
                        pnlHO.Enabled = true;
                        pnlSum.Visible = true;
                        pnlHO.Visible = true;
                        pnl_OthLoc.Visible = true;
                        break;
                    }
                case 11:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnl_DocStatus.Enabled = true;
                        pnl_RepType.Visible = true;
                        pnl_RepType.Enabled = true;
                        pnlFOC.Visible = false;
                        pnl_Item.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        chk_exportexcel.Enabled = true;
                        break;
                    }
                case 13:
                    {
                        pnl_Location.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 14:
                    {
                        pnl_Location.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 15:
                    {
                        pnl_Location.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 16:
                    {
                        pnlDateRange.Enabled = false;
                        pnl_Location.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnlAge.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        txtNoOfDays.Enabled = true;
                        txtAsAtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                        chkCost.Enabled = true;
                        break;
                    }
                case 17:
                    {
                        pnlDateRange.Enabled = false;
                        pnl_Location.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        break;
                    }
                case 18:
                    {
                        pnl_DocType.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 19:
                    {
                        pnl_DocType.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 20:
                    {
                        pnl_DocType.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 21:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnl_DocStatus.Enabled = true;
                        break;
                    }
                case 23:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnl_DocNo.Enabled = true;
                        break;
                    }
                case 24:
                    {
                        break;
                    }

                case 25:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnl_DocStatus.Enabled = true;
                        //pnl_Item.Enabled = true;
                        break;
                    }
                case 26:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }

                case 27:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 28:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 29:
                    {
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 30:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnlPO.Enabled = true;
                        pnlPO.Visible = true;
                        pnlExces.Visible = false;
                        opBoth.Text = "Both";
                        optLocal.Text = "Local";
                        optImport.Text = "Import";
                        break;
                    }
                case 31:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 32:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;
                        //pnl_Itm_Stus.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 33:
                    {
                        pnl_DocType.Enabled = true;
                        break;
                    }
                case 34:
                    {
                        pnl_Item.Enabled = true;
                        pnl_supplier.Enabled = true;
                        pnl_supplier.Visible = true;
                        break;
                    }
                case 567:
                    {
                        pnlDateRange.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_DocType.Enabled = true;
                        break;
                    }
                case 35:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 36:
                    {
                        pnlDateRange.Enabled = false;
                        pnl_supplier.Enabled = true;
                        pnl_supplier.Visible = true;
                        pnl_Item.Enabled = true;
                        chkComAge.Enabled = true;
                        pnlAgeCat.Enabled = true;
                        chkCost.Enabled = true;
                        chk_all_com.Visible = true;
                        chk_all_com.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;                      
                        break;
                    }
                case 37:
                    {
                        pnl_PB.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PBLevel.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 38:
                    {
                        pnl_Item.Enabled = true;
                        pnl_PB.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PBLevel.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 39:
                    {
                        pnl_PB.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PBLevel.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnlDateRange.Enabled = true;
                        break;
                    }

                case 40:
                    {
                        pnlLoc.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }

                case 41:
                    {
                        pnl_Item.Enabled = true;
                        pnlAgeCat.Enabled = true;
                        chkCost.Enabled = true;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        chkComAge.Enabled = true;
                        optitmwise.Enabled = true;
                        //Wimal @ 09/09/2018 - get price to age report
                        pnl_PB.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PBLevel.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        break;
                    }
                case 42:
                    {
                        //pnlAgeCat.Enabled = true;
                        chkCost.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        chkCost.Enabled = true;
                        pnlAge.Enabled = true;
                        txtNoOfDays.Enabled = true;
                        txttoage.Enabled = true;
                        chkComAge.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        chk_exportexcel.Enabled = true;
                        chkroot.Enabled = true;
                        chkroot.Visible = true;
                        break;
                    }
                case 43:
                    {
                        pnlFOC.Visible = true;
                        pnl_RepType.Visible = false;
                        pnl_Item.Enabled = true;
                        pnlFOC.Enabled = true;
                        break;
                    }
                case 44:
                    {
                        pnlPO.Enabled = true;
                        pnlPO.Visible = true;
                        pnlExces.Visible = false;
                        opBoth.Text = "Both";
                        optLocal.Text = "Qty";
                        optImport.Text = "Value";
                        pnl_Item.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        chk_exportexcel.Enabled = true;
                        chkWOStus.Enabled = true;
                        break;
                    }
                case 46:
                    {
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 47:
                    {
                        pnlExces.Enabled = true;
                        pnlExces.Visible = true;
                        pnlPO.Visible = false;
                        pnl_Item.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 48:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_OthLoc.Enabled = true;
                        pnl_OthLoc.Visible = true;
                        pnl_Location.Visible = false;
                        pnl_Item.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        chk_exportexcel.Enabled = true;
                        chk_all_com.Visible = true;
                        chk_all_com.Enabled = true;
                        chkCost.Visible = true;
                        chkCost.Enabled = true;

                        break;
                    }
                case 49:
                    {
                        pnl_Item.Enabled = true;
                        btn_Srch_Cat1.Enabled = true;
                        txtIcat1.Enabled = true;
                        pnlExces.Visible = true;
                        pnlExces.Enabled = true;
                        pnlDateRange.Enabled = false;
                        optB.Text = "Item";
                        optS.Text = "Component";
                        optE.Text = "Both";
                        optB.Checked = true;
                        break;
                    }
                case 50:
                    {
                        pnlExces.Visible = true;
                        pnlExces.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 51:
                    {
                        pnl_Item.Enabled = true;
                        pnl_OthLoc.Visible = true;
                        pnl_OthLoc.Enabled = true;
                        break;
                    }
                case 53:
                    {
                        pnl_Item.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Itm_Stus.Enabled = true;
                        pnl_itmcond.Visible = true;
                        pnl_itmcond.Enabled = true;
                        pnl_git.Visible = true;
                        pnl_git.Enabled = true;
                        chk_allserial.Visible = true;
                        chk_allserial.Enabled = true;
                        chkJob.Visible = false;
                        break;
                    }
                case 54:
                    {
                        pnlPO.Enabled = true;
                        pnlPO.Visible = true;
                        pnlExces.Visible = false;
                        opBoth.Text = "Both";
                        optLocal.Text = "Qty";
                        optImport.Text = "Value";
                        pnl_Item.Enabled = true;
                        pnl_Itm_Stus.Enabled = true;
                        chk_exportexcel.Enabled = true;
                        chkWOStus.Checked = true;
                        break;
                    }
                case 55: // CMCM
                    {

                        pnlDateRange.Enabled = true;
                        pnl_DocSubType.Enabled = true;
                        pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = false;
                        pnl_DocType.Enabled = true;
                        pnlSum.Enabled = true;
                        pnlHO.Enabled = true;
                        pnlSum.Visible = true;
                        pnlHO.Visible = true;
                        pnl_OthLoc.Visible = true;
                        chkmulticompany.Visible = true;
                        lblAddDocSub.Visible = true;

                        break;
                    }
                case 57: // CMCM
                    {
                        pnl_Itm_Stus.Enabled = true;
                        chkAllComp.Enabled = true;
                        chk_Itm_Stus.Enabled = false;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        pnl_root.Enabled = false;
                        pnl_ageing.Visible = true;
                        pnl_ageing.Enabled = true;
                        //chkAllComp.Checked = true;
                        loadageing();
                        break;
                    }
                case 58: // CMCM
                    {
                        pnl_Itm_Stus.Enabled = true;
                        chkAllComp.Enabled = true;
                        chk_Itm_Stus.Enabled = false;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        pnl_root.Enabled = false;
                        pnl_ageing.Visible = false;
                        pnl_ageing.Enabled = false;
                        chkAllComp.Checked = false;
                        //loadageing();
                        break;
                    }
                case 59: // CMCM
                    {
                        pnl_Itm_Stus.Enabled = true;
                        chkAllComp.Enabled = true;
                        chk_Itm_Stus.Enabled = false;
                       
                        pnl_root.Enabled = false;
                        pnl_ageing.Visible = false;
                        pnl_ageing.Enabled = false;
                        chkAllComp.Checked = false;
                        pnlDateRange.Enabled = true;
                        txtFromDate.Enabled = true;
                        txtToDate.Enabled = true;
                        //loadageing();
                        break;
                    }

                case 60:
                    {
                        pnl_DocNo.Enabled = true;
                        txtDocNo.Enabled = true;
                        txt_root1.Enabled = false;
                        pnl_root.Enabled = false;
                        btn_Srch_Doc.Enabled = false;
                        chk_Doc.Enabled = false;

                        break;
                    }

                case 61: // Abstract sales inv bal
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_OthLoc.Enabled = true;                      
                        break;
                    }

                case 62: // 
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_OthLoc.Visible = true;
                        break;
                    }

                //case 63: // 
                //    {
                //        pnlAsAtDate.Enabled = false;
                //        pnlDateRange.Enabled = false;
                //        pnl_root.Enabled = false;
                //        break;
                //    }
                                

                case 63: // 
                    {
                        pnlAsAtDate.Enabled = false;
                        pnl_root.Enabled = false;
                        pnl_Item.Enabled = true;
                        break;
            }
                case 64: // 
                    {
                        pnl_PB.Enabled = true;
                        pnl_PB.Visible = true;
                        pnl_PBLevel.Enabled = true;
                        pnl_PBLevel.Visible = true;
                        pnlDateRange.Enabled = false;
                        pnl_root.Enabled = false;
                        break;
        }
                case 65: // 
                    {
                        pnlMutiparasel.Visible = true;
                        pnl_Item.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        //pnlAge.Enabled = true;
                        pnlAgeSlot.Enabled = true;
                        pnlDateRange.Enabled = false;
                        chkComAge.Enabled = true;
                        pnlAgeCat.Enabled = true;
                        chkComAge.Checked = true;
                        break;
                    }

                case 66:
                    {
                        pnlDateRange.Enabled = true;
                        //pnl_DocSubType.Enabled = true;
                        //pnl_Direc.Enabled = true;
                        pnl_Item.Enabled = true;
                        //pnl_DocType.Enabled = true;
                        pnlSum.Enabled = true;
                        pnlSum.Visible = true;
                        //chkCost.Enabled = true;
                        //pnlHO.Enabled = true;
                        //pnlHO.Visible = true;
                        //pnl_DocNo.Enabled = true;
                        //pnl_OthLoc.Enabled = true;
                        //pnl_OthLoc.Visible = true;
                        pnl_root.Enabled = false;                                    
                        pnl_Location.Visible = false;
                        optNor.Enabled = false;
                        optList.Enabled = false;  
                        chk_ICat1.Enabled = false;  
                            chk_ICat2.Enabled = false;  
                            chk_ICat3.Enabled = false;  
                            chk_Brand.Enabled = false;  
                            chk_Model.Enabled = false;  
                            btnCat1.Enabled = false;  
                            btnItem.Enabled = false;  
                            btnBrand.Enabled = false;
                            btnModel.Enabled = false;
                            chk_Item.Checked = false;
                        break; 
                    }

                case 67: // 
                    {
                        pnlMutiparasel.Visible = false;
                        pnl_Item.Enabled = true;
                        pnlAgeSlot.Enabled = false;
                        pnlDateRange.Enabled = true ;
                        chkComAge.Enabled = false;
                        pnlAgeCat.Enabled = false;
                        chkComAge.Checked = false;
                        pnlDateRange_2.Visible = true;
                        pnlDateRange_2.Enabled = true;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        txtFromDate_2.Enabled = false;
                        txtToDate_2.Enabled = false;
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

            MasterLocation _masterLoc = null;
            _masterLoc = CHNLSVC.General.GetAllLocationByLocCode(txtComp.Text, txtPC.Text, 1);
            if (_masterLoc != null)
            {
                txtPCDesn.Text = _masterLoc.Ml_loc_desc;
            }
            else
            {
                txtPCDesn.Text = "";
            }
        }

        private void InitializeEnv()
        {
            txtComp.Text = BaseCls.GlbUserComCode;
            txtPC.Text = BaseCls.GlbUserDefLoca;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");

            cmbYear_2.Items.Add("2012");
            cmbYear_2.Items.Add("2013");
            cmbYear_2.Items.Add("2014");
            cmbYear_2.Items.Add("2015");
            cmbYear_2.Items.Add("2016");
            cmbYear_2.Items.Add("2017");
            cmbYear_2.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            cmbYear.SelectedIndex = _Year % 2013 + 1;
            cmbYear_2.SelectedIndex = _Year % 2013 + 1;

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

            cmbMonth_2.Items.Add("January");
            cmbMonth_2.Items.Add("February");
            cmbMonth_2.Items.Add("March");
            cmbMonth_2.Items.Add("April");
            cmbMonth_2.Items.Add("May");
            cmbMonth_2.Items.Add("June");
            cmbMonth_2.Items.Add("July");
            cmbMonth_2.Items.Add("August");
            cmbMonth_2.Items.Add("September");
            cmbMonth_2.Items.Add("October");
            cmbMonth_2.Items.Add("November");
            cmbMonth_2.Items.Add("December");
            cmbMonth_2.SelectedIndex = DateTime.Now.Month - 1;

            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtFromDate_2.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtToDate_2.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
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
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {

                if (Item.Checked == true)
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


                    if (_isPCFound == false)
                    {
                        BaseCls.GlbReportProfit = txtPC.Text;
                        Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
                    }
                }
            }
            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
            //BaseCls.GlbReportProfit = "";

            //Boolean _isPCFound = false;
            //Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

            //foreach (ListViewItem Item in lstPC.Items)
            //{
            //    string pc = Item.Text;

            //    if (Item.Checked == true)
            //    {
            //        Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, pc, null);

            //        _isPCFound = true;
            //        if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
            //        {
            //            BaseCls.GlbReportProfit = pc;
            //        }
            //        else
            //        {
            //            //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
            //            BaseCls.GlbReportProfit = "All Locations Based on User Rights";
            //        }
            //    }
            //}

            //List<string> _listLocs = new List<string>();

            //if (_isPCFound == false)
            //{
            //    BaseCls.GlbReportProfit = txtPC.Text;
            //    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            //    _listLocs.Add(txtPC.Text);
            //}
            //else
            //{
            //    _listLocs = lstPC.Items.Cast<ListViewItem>().Where(item => item.Checked).Select(item => item.Text).ToList();
            //}
            //if (_listLocs.Count > 0) CHNLSVC.Security.Add_User_Selected_Loc_Pc_DR(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null, _listLocs);

        }



        private void btnAddItem_Click(object sender, EventArgs e)
        {

            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            Boolean _isChk = false;
            string _adminTeam = "";

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

                            //DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                            }

                            com = txtComp.Text;

                            //dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                            }
                        }
                        else
                        {

                            DataTable dtn = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            foreach (DataRow drow in dtn.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPI"))
                //Add by Chamal 30-Aug-2013
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10045))
                {
                    if (chkroot.Checked == true) // add by tharanga 2017/08/29
                    {

                        DataTable route = CHNLSVC.Sales.getrootDesc(BaseCls.GlbUserComCode, txtPC.Text);
                        foreach (DataRow row2 in route.Rows)
                        {
                            lstPC.Items.Add(row2["frs_loc_cd"].ToString());
                        }
                    }
                    else
                    {
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
                            DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                            }
                            com = txtComp.Text;

                            //dt = CHNLSVC.Sales.GetPC_from_Hierachy_with_Opteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                            dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                            }

                        }
                        else
                        {

                            DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);

                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["ML_COM_CD"].ToString());
                    }
                }
            }




            //string com = txtComp.Text;
            //string chanel = txtChanel.Text;
            //string subChanel = txtSChanel.Text;
            //string area = txtArea.Text;
            //string region = txtRegion.Text;
            //string zone = txtZone.Text;
            //string pc = txtPC.Text;

            //Boolean _isChk = false;
            //string _adminTeam = "";

            //lstPC.Clear();
            //string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

            //foreach (ListViewItem Item in lstAdmin.Items)
            //{
            //    if (Item.Checked == true)
            //    {
            //        _adminTeam = Item.Text;
            //        _isChk = true;
            //        break;
            //    }
            //}
            //if (_isChk == true)
            //{
            //    DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
            //    foreach (DataRow drow in dt.Rows)
            //    {
            //        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
            //    }
            //}
            //else
            //{
            //    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPI"))
            //    //Add by Chamal 30-Aug-2013
            //    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10045))
            //    {
            //        if (chkroot.Checked == true) // add by tharanga 2017/08/29
            //        {

            //            DataTable route = CHNLSVC.Sales.getrootDesc(BaseCls.GlbUserComCode, txtPC.Text);
            //            foreach (DataRow row2 in route.Rows)
            //            {
            //                lstPC.Items.Add(row2["frs_loc_cd"].ToString());
            //            }
            //        }
            //        else
            //        {
            //            DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);

            //            foreach (DataRow drow in dt.Rows)
            //            {
            //                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
            //            }
            //        }
            //    }
            //    else
            //    {
            //        DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
            //        foreach (DataRow drow in dt.Rows)
            //        {
            //            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
            //        }
            //    }
            //}

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
            try
            {
                if (opt1.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7001"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7001))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7001 )");
                        opt1.Checked = false;
                        return;
                    }
                    setFormControls(1);
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
            if (month > 0)
            {
                int numberOfDays = DateTime.DaysInMonth(year, month);
                DateTime lastDay = new DateTime(year, month, numberOfDays);

                txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
                txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            }

        }

        private void Sales_Rep_Load(object sender, EventArgs e)
        {

        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Item.Enabled = true;
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
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(txtDocType.Text + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.AdminTeam:
                    {
                        paramsText.Append(_userCompany);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append(txtDocType.Text.ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        //paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        //break;
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InventoryDirection:
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "CT003" + seperator + "0" + seperator + "" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Route_cd:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break; ;
                    }
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
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
                DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRecType;
                _CommonSearch.ShowDialog();
                txtRecType.Select();
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
        }

        //made public by akila 2017/05/12
        public void btnDisplay_Click(object sender, EventArgs e)
        {
            btnClear.Focus();
            //check whether current session is expired
            CheckSessionIsExpired();

            //kapila 4/7/2014
            if (CheckServerDateTime() == false) return;

            //check this user has permission for this PC
            if (txtPC.Text != string.Empty)
            {
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "REPI"))
                //Add by Chamal 30-Aug-2013

                //Add by akila - 2017/05/17 ignore the permission cheking option in stockverification screen
                if (CheckPermission) // default value is true
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10045))
                    {
                        Int16 is_Access = CHNLSVC.Security.Check_User_Loc(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                        if (is_Access != 1)
                        {
                            //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10045)", "Inventory Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                ////Commented by akila 2017/05/17
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10045))
                //{
                //    Int16 is_Access = CHNLSVC.Security.Check_User_Loc(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                //    if (is_Access != 1)
                //    {
                //        //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10045)", "Inventory Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }
                //}
            }

            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;

            // btnDisplay.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            BaseCls.GlbReportName = string.Empty;
            GlbReportName = string.Empty;

            if (opt1.Checked == true)     //Inventory Statement
            {  // Nadeeka
                //update temporary table
                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;
                Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 7001);
                if (_rptPrd != 0)
                {
                    if ((finish - start).Days > _rptPrd)
                    {
                        MessageBox.Show("you cannot generate this report for this date period.", "Inventory Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                }
               // update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Statement";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "InventoryStatements.rpt";
                BaseCls.GlbReportName = "InventoryStatements.rpt";
                _view.Show();
                _view = null;

            }

            if (opt2.Checked == true)
            {
                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;
                Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 7003);
                if (_rptPrd != 0)
                {
                    if ((finish - start).Days > _rptPrd)
                    {
                        MessageBox.Show("you cannot generate this report for this date period.", "Inventory Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                }// Nadeeka
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Audit Trial With Serial";
                BaseCls.GlbReportIsCostPrmission = 0;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                BaseCls.GlbReportType = optsum.Checked ? "SUM" : "DTL";
                if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";
                BaseCls.GlbReportWithCost = 0;
                BaseCls.GlbReportDoc = txtDocNo.Text;
                BaseCls.GlbReportOtherLoc = txt_othloc.Text;

                ReportViewerInventory _view = new ReportViewerInventory();

                if (chkHO.Checked == false)
                {
                    if (BaseCls.GlbReportType == "DTL")
                    {
                        BaseCls.GlbReportGroupLastGroupCat = null;
                        //Akila 2017/12/06
                        if (lstGroup.Items.Count > 0)
                        {
                            foreach (ListViewItem Item in lstGroup.Items)
                            {
                                if (Item.Text.ToUpper() == "CAT1")
                                {
                                    BaseCls.GlbReportGroupLastGroupCat = "CAT1";
                                    break;
                                }
                            }
                        }

                        if (BaseCls.GlbReportGroupLastGroupCat == "CAT1")
                        {
                            _view.GlbReportName = "InventoryMovementAuditTrials_GroupByCate.rpt";
                            BaseCls.GlbReportName = "InventoryMovementAuditTrials_GroupByCate.rpt";
                        }
                        else
                        {
                            _view.GlbReportName = "InventoryMovementAuditTrials.rpt";
                            BaseCls.GlbReportName = "InventoryMovementAuditTrials.rpt";
                        }
                        //_view.GlbReportName = "InventoryMovementAuditTrials.rpt";
                        //BaseCls.GlbReportName = "InventoryMovementAuditTrials.rpt";
                    }
                    else
                    {
                        _view.GlbReportName = "InventoryMovementAuditTrials_sum.rpt";
                        BaseCls.GlbReportName = "InventoryMovementAuditTrials_sum.rpt";
                    }
                }
                else
                {
                    BaseCls.GlbReportParaLine1 = chkAppStus.Checked ? 1 : 0;
                    if (optNor.Checked == true)
                    {
                        BaseCls.GlbReportHeading = "Movement Audit Trial Report (Items)";
                        if (chkCost.Checked == true)
                        {
                            BaseCls.GlbReportWithCost = Convert.ToInt16(1);
                            BaseCls.GlbReportName = "Movement_audit_trial_cost.rpt";
                        }
                        else
                        {
                            BaseCls.GlbReportWithCost = Convert.ToInt16(0);
                            BaseCls.GlbReportName = "Movement_audit_trial.rpt";
                        }
                    }

                    else if (optsum.Checked == true)
                    {
                        if (chkCost.Checked == true)
                            BaseCls.GlbReportWithCost = Convert.ToInt16(1);
                        else
                            BaseCls.GlbReportWithCost = Convert.ToInt16(0);

                        BaseCls.GlbReportHeading = "Movement Audit Trial Summary Report";
                        BaseCls.GlbReportName = "Movement_audit_trial_summary.rpt";
                    }
                    else if (optdtl.Checked == true)
                    {
                        if (chkCost.Checked == true)
                            BaseCls.GlbReportWithCost = Convert.ToInt16(1);
                        else
                            BaseCls.GlbReportWithCost = Convert.ToInt16(0);

                        BaseCls.GlbReportHeading = "Movement Audit Trial Detail Report";
                        BaseCls.GlbReportName = "Movement_audit_trial_det.rpt";
                    }
                    else if (optList.Checked == true)
                    {
                        BaseCls.GlbReportHeading = "Movement Audit Trial Listing Report";
                        BaseCls.GlbReportName = "Movement_audit_trial_sum.rpt";
                    }
                    BaseCls.GlbReportIsSummary = Convert.ToInt16(optsum.Checked);

                }
                _view.Show();
                _view = null;

            }

            if (opt3.Checked == true)
            {   // Nadeeka
                //update temporary table

                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;
                Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 7012);
                if (_rptPrd != 0)
                {
                    if ((finish - start).Days > _rptPrd)
                    {
                        MessageBox.Show("you cannot generate this report for this date period.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                }
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Audit Trial With Serial";
                BaseCls.GlbReportHeading = "Movement Audit Trial with Serial Report";
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;
                if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                BaseCls.GlbReportDoc = txtDocNo.Text;
                BaseCls.GlbReportOtherLoc = txt_othloc.Text;
                ReportViewerInventory _view = new ReportViewerInventory();


                if (chkHO.Checked == false)
                {
                    _view.GlbReportName = "InventoryMovementAuditTrialWithSerials.rpt";
                    BaseCls.GlbReportName = "InventoryMovementAuditTrialWithSerials.rpt";
                }
                else
                {
                    BaseCls.GlbReportParaLine1 = chkAppStus.Checked ? 1 : 0;
                    BaseCls.GlbReportWithCost = 0;
                    if (chkCost.Checked == true)
                    {
                        BaseCls.GlbReportWithCost = 1;
                        _view.GlbReportName = "Movement_audit_trial_ser_cost.rpt";
                        BaseCls.GlbReportName = "Movement_audit_trial_ser_cost.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportWithCost = 0;
                        _view.GlbReportName = "Movement_audit_trial_ser.rpt";
                        BaseCls.GlbReportName = "Movement_audit_trial_ser.rpt";
                    }

                }
                _view.Show();
                _view = null;

            }

            if (opt12.Checked == true)     //Inventory Statement
            {  // Nadeeka
                //update temporary table
                //update temporary table

                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;
                Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 7002);
                if (_rptPrd != 0)
                {
                    if ((finish - start).Days > _rptPrd)
                    {
                        MessageBox.Show("you cannot generate this report for this date period.", "Inventory Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                }
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Audit Trial With Serial";
                BaseCls.GlbReportIsCostPrmission = 1;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;
                BaseCls.GlbReportType = optsum.Checked ? "SUM" : "DTL";
                if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";
                BaseCls.GlbReportOtherLoc = txt_othloc.Text;
                ReportViewerInventory _view = new ReportViewerInventory();

                if (chkHO.Checked == false)
                {
                    if (BaseCls.GlbReportType == "DTL")
                    {
                        _view.GlbReportName = "InventoryMovementAuditTrials.rpt";
                        BaseCls.GlbReportName = "InventoryMovementAuditTrials.rpt";

                        //if (BaseCls.GlbReportCompCode=="ARL")
                        //{
                        //    _view.GlbReportName = "InventoryMovementAuditTrials_ARL.rpt";
                        //    BaseCls.GlbReportName = "InventoryMovementAuditTrials_ARL.rpt";                        
                        //}
                    }
                    else
                    {
                        _view.GlbReportName = "InventoryMovementAuditTrials_sum.rpt";
                        BaseCls.GlbReportName = "InventoryMovementAuditTrials_sum.rpt";
                    }
                }
                else
                {
                    BaseCls.GlbReportWithCost = 1;
                    _view.GlbReportName = "Movement_audit_trial_ser_cost.rpt";
                    BaseCls.GlbReportName = "Movement_audit_trial_ser_cost.rpt";

                    if (BaseCls.GlbReportCompCode == "ARL")
                    {
                        _view.GlbReportName = "InventoryMovementAuditTrials_ARL.rpt";
                        BaseCls.GlbReportName = "InventoryMovementAuditTrials_ARL.rpt";
                    }
                }
                _view.Show();
                _view = null;

            }

            // hasith 28/01/2016
            if (opt567.Checked == true)
            {
                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;
                Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 16045);
                if (_rptPrd != 0)
                {
                    if ((finish - start).Days > _rptPrd)
                    {
                        MessageBox.Show("you cannot generate this report for this date period.", "Inventory Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                }
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Movement Summary";
                BaseCls.GlbReportIsCostPrmission = 0;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "MovementSummaryReport.rpt";
                BaseCls.GlbReportName = "MovementSummaryReport.rpt";
                _view.Show();
                _view = null;

            }


            //if (opt6.Checked == true)    //Inventory Balance as at
            //{
            //    //update temporary table
            //    update_PC_List();

            //    if (opt4.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE"; }
            //    // if (opt5.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH COST)"; }
            //    if (opt6.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH SERIAL)"; }


            //    BaseCls.GlbReportChannel = txtChanel.Text;
            //    BaseCls.GlbReportBrand = txtBrand.Text;
            //    BaseCls.GlbReportModel = txtModel.Text;
            //    BaseCls.GlbReportItemCode = txtItemCode.Text;
            //    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
            //    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
            //    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
            //    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
            //    if (opt4.Checked == true) { BaseCls.GlbReportWithCost = 0; }
            //    if (opt5.Checked == true) { BaseCls.GlbReportWithCost = 1; }
            //    if (opt6.Checked == true) { BaseCls.GlbReportWithCost = 0; }
            //    if (opt4.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
            //    if (opt5.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
            //    if (opt6.Checked == true) { BaseCls.GlbReportWithSerial = 1; }
            //    BaseCls.GlbReportType = "ASAT";

            //    ReportViewerInventory _view = new ReportViewerInventory();
            //    BaseCls.GlbReportName = "Stock_Balance.rpt";
            //    _view.GlbReportName = "Stock_Balance.rpt";
            //    _view.Show();
            //    _view = null;

            //}
            if (opt4.Checked == true || opt5.Checked == true)    //Inventory Balance as at
            {
                if (BaseCls.GlbReportAsAtDate == DateTime.Today.Date)
                {
                    MessageBox.Show("If you are running this report as at today, Please use the Current Inventory balance Reports.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    return;
                }

                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportComp = txtComp.Text;
                BaseCls.GlbReportCompCode = txtComp.Text;
                if (opt4.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE"; }
                if (opt5.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH COST)"; }
                if (opt6.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH SERIAL)"; }

                BaseCls.GlbReportWithStatus = 0;
                BaseCls.GlbReportChannel = txtChanel.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDoc = txt_color.Text;
                BaseCls.GlbReportDoc1 = txt_size.Text;
                if (chkStatusWise.Checked == true) { BaseCls.GlbReportWithStatus = 1; }
                if (opt4.Checked == true) { BaseCls.GlbReportWithCost = 0; }
                if (opt5.Checked == true) { BaseCls.GlbReportWithCost = 1; }
                if (opt6.Checked == true) { BaseCls.GlbReportWithCost = 0; }
                if (opt4.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
                if (opt5.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
                if (opt6.Checked == true) { BaseCls.GlbReportWithSerial = 1; }
                if (opt6.Checked == true) { BaseCls.GlbReportWithStatus = 1; }
                BaseCls.GlbReportType = "ASAT";
                BaseCls.GlbReportDocType = opt_supp.Checked == true ? "SUP" : "NOR";

                ReportViewerInventory _view = new ReportViewerInventory();
                if (BaseCls.GlbReportDocType == "NOR")
                {
                    BaseCls.GlbReportName = "Stock_BalanceCost.rpt";
                    _view.GlbReportName = "Stock_BalanceCost.rpt";
                }
                else
                {
                    BaseCls.GlbReportName = "Stock_BalanceCost_AST.rpt";
                    _view.GlbReportName = "Stock_BalanceCost_AST.rpt";
                }
                _view.Show();
                _view = null;

            }

            if (opt6.Checked == true)    //Inventory Balance with serial as at
            {
                if (BaseCls.GlbReportAsAtDate == DateTime.Today.Date)
                {
                    MessageBox.Show("If you are running this report as at today, Please use the Current Inventory balance Reports.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    return;
                }
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportCompCode = txtComp.Text;
                //if (opt4.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE"; }
                //if (opt5.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH COST)"; }
                if (opt6.Checked == true) { BaseCls.GlbReportHeading = "INVENTORY BALANCE AS AT DATE (WITH SERIAL)"; }

                BaseCls.GlbReportWithStatus = 0;
                BaseCls.GlbReportChannel = txtChanel.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDoc = txt_color.Text;
                BaseCls.GlbReportDoc1 = txt_size.Text;
                if (chkStatusWise.Checked == true) { BaseCls.GlbReportWithStatus = 1; }
                if (opt4.Checked == true) { BaseCls.GlbReportWithCost = 0; }
                if (opt5.Checked == true) { BaseCls.GlbReportWithCost = 1; }
                if (opt6.Checked == true) { BaseCls.GlbReportWithCost = 0; }
                if (opt4.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
                if (opt5.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
                if (opt6.Checked == true) { BaseCls.GlbReportWithSerial = 1; }
                if (opt6.Checked == true) { BaseCls.GlbReportWithStatus = 1; }
                BaseCls.GlbReportType = "ASAT";

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "Stock_BalanceSerialAsat.rpt";
                _view.GlbReportName = "Stock_BalanceSerialAsat.rpt";
                _view.Show();
                _view = null;

            }

            if (opt7.Checked == true || opt8.Checked == true || opt9.Checked == true)    //Inventory Balance current
            {
                #region clear
                BaseCls.GlbReportWithCost = 0;
                BaseCls.GlbReportWithSerial = 0;
                BaseCls.GlbReportWithStatus = 0;
                BaseCls.GlbReportWithDetail = 0;

                #endregion
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                if (opt7.Checked == true) { BaseCls.GlbReportHeading = "CURRENT INVENTORY BALANCE"; }
                if (opt8.Checked == true) { BaseCls.GlbReportHeading = "CURRENT INVENTORY BALANCE (WITH COST)"; }
                if (opt9.Checked == true) { BaseCls.GlbReportHeading = "CURRENT INVENTORY BALANCE (WITH SERIAL)"; }

                BaseCls.GlbReportAsAtDate = Convert.ToDateTime("01/01/1900");
                BaseCls.GlbReportChannel = txtChanel.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportParaLine1 = chk_withgit.Checked == true ? 1 : 0;
                BaseCls.GlbReportJobStatus = chkJob.Checked == true ? "Y" : "N";
                BaseCls.GlbReportDiscontinueItems = System.Convert.ToInt16(chkDisconItems.Checked == true ? 1 : 0);

                if (opt7.Checked == true) { BaseCls.GlbReportWithCost = 0; }
                if (opt8.Checked == true) { BaseCls.GlbReportWithCost = 1; BaseCls.GlbReportWithDetail = 1; }
                if (opt9.Checked == true) { BaseCls.GlbReportWithCost = 0; }
                if (opt7.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
                if (opt8.Checked == true) { BaseCls.GlbReportWithSerial = 0; }
                if (opt9.Checked == true) { BaseCls.GlbReportWithSerial = 1; }
                BaseCls.GlbReportWithRCC = chk_withRCC.Checked == true ? "Y" : "N";
                BaseCls.GlbReportType = "CURR";
                BaseCls.GlbReportDocType = opt_supp.Checked == true ? "SUP" : "NOR";
                BaseCls.GlbReportWithStatus = 0;

                if (chkWOStus.Checked == false) { BaseCls.GlbReportWithStatus = 1; }
                if (chkWODet.Checked == false) { BaseCls.GlbReportWithDetail = 1; }

                if (opt9.Checked == true && chkWODet.Checked == true)
                    BaseCls.GlbReportWithStatus = 0;

                ReportViewerInventory _view = new ReportViewerInventory();
                if (BaseCls.GlbReportDocType == "NOR")
                {
                    BaseCls.GlbReportName = "Stock_Balance.rpt";
                    _view.GlbReportName = "Stock_Balance.rpt";

                    if (opt7.Checked == true)
                        if (chkWOStus.Checked == true)
                        {
                            BaseCls.GlbReportName = "Stock_Balance_WO_Stus.rpt";
                            _view.GlbReportName = "Stock_Balance_WO_Stus.rpt";
                        }
                    if (opt9.Checked == true)
                        if (chkWODet.Checked == true)
                        {
                            BaseCls.GlbReportName = "Stock_Balance_WO_Det.rpt";
                            _view.GlbReportName = "Stock_Balance_WO_Det.rpt";
                        }


                }
                else
                {
                    BaseCls.GlbReportName = "Stock_Balance_AST.rpt";
                    _view.GlbReportName = "Stock_Balance_AST.rpt";
                }
                _view.Show();
                _view = null;

                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
            }

            if (opt10.Checked == true)
            {   // Nadeeka
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Approval Process Status For Damage Goods";


                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "DamageGoodsApproval.rpt";
                BaseCls.GlbReportName = "DamageGoodsApproval.rpt";
                _view.Show();
                _view = null;

            }

            if (opt11.Checked == true)    //GRAN / DIN current
            {
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "GRAN / DIN REPORT";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportWithCost = Convert.ToInt16((CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075)) ? 1 : 0);
                BaseCls.GlbReportDocType = "";
                if (cmbDocStatus.Text == "")
                    BaseCls.GlbStatus = "X";
                else
                    BaseCls.GlbStatus = cmbDocStatus.Text.ToString().Substring(0, 1);
                if (cmbReportType.Text == "")
                    BaseCls.GlbReportType = "ALL";
                else
                    BaseCls.GlbReportType = cmbReportType.Text.ToString();

                BaseCls.GlbReportRoot = txt_root1.Text.Trim();

                if (chk_exportexcel.Checked == true)
                {
                    string _error;

                    string _filePath = CHNLSVC.MsgPortal.GetSGRANDetails_Execl_Details(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbStatus, BaseCls.GlbReportType, out _error, BaseCls.GlbUserComCode, BaseCls.GlbReportRoot);

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
                    ReportViewerInventory _view = new ReportViewerInventory();
                    BaseCls.GlbReportName = "GRAN_Details_Report.rpt";
                    _view.GlbReportName = "GRAN_Details_Report.rpt";
                    _view.Show();
                    _view = null;
                }

            }
            if (opt13.Checked == true)
            {   // Nadeeka
                //update temporary table
               // update_PC_List();
                update_PC_List_RPTDBnew();
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
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "FastMovingItems.rpt";
                BaseCls.GlbReportName = "FastMovingItems.rpt";
                _view.Show();
                _view = null;

            }
            if (opt14.Checked == true)
            {   // Nadeeka
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
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

                BaseCls.GlbReportIsFast = 0;
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "FastMovingItems.rpt";
                BaseCls.GlbReportName = "FastMovingItems.rpt";
                _view.Show();
                _view = null;

            }
            if (opt15.Checked == true)
            {   // Nadeeka
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Non Moving Items";

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

                BaseCls.GlbReportIsFast = 0;
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "NonMovingItems.rpt";
                BaseCls.GlbReportName = "NonMovingItems.rpt";
                _view.Show();
                _view = null;

            }

            if (opt16.Checked == true)
            {   // Nadeeka
                //update temporary table
             //   update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Stock Balance With Serial Age";
                if (txtNoOfDays.Text == "")
                {
                    txtNoOfDays.Text = Convert.ToString(0);

                }
                BaseCls.GlbReportWithCost = 0;
                if (chkCost.Checked == true)
                    BaseCls.GlbReportWithCost = 1;
                if (chkComAge.Checked == true)
                    BaseCls.GlbReportIsFast = 1;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                BaseCls.GlbReportnoofDays = Convert.ToInt16(txtNoOfDays.Text);
                // BaseCls.GlbReportWithCost = Convert.ToInt16((CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075)) ? 1 : 0);
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "StockBalanceWithSerialAge.rpt";
                BaseCls.GlbReportName = "StockBalanceWithSerialAge.rpt";
                _view.Show();
                _view = null;

            }

            if (opt17.Checked == true)    //FIFO not updated
            {
                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;
                Int32 _rptPrd = CHNLSVC.Financial.GetReportAllowPeriod(txtComp.Text, 7017);
                if (_rptPrd != 0)
                {
                    if ((finish - start).Days > _rptPrd)
                    {
                        MessageBox.Show("you cannot generate this report for this date period.", "Inventory Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }
                }
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "FIFO NOT FOLLOWED REPORT";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "FIFO_Not_Followed_Report.rpt";
                _view.GlbReportName = "FIFO_Not_Followed_Report.rpt";
                _view.Show();
                _view = null;

            }

            if (opt18.Checked == true)     //Inventory Movement Listing (With Item Status)
            {  // Sanjeewa
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Movement Listing (With Item Status)";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "InventoryStatementsTr.rpt";
                BaseCls.GlbReportName = "InventoryStatementsTr.rpt";
                _view.Show();
                _view = null;

            }

            if (opt19.Checked == true)     //Inventory Movement Listing
            {  // Sanjeewa
                //update temporary table
               // update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Movement Listing";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "InventoryStatementsTr2.rpt";
                BaseCls.GlbReportName = "InventoryStatementsTr2.rpt";
                _view.Show();
                _view = null;

            }

            if (opt20.Checked == true)     //Inventory movement listing - Item wise
            {  // Sanjeewa
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Inventory Movement Listing (Item wise)";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "InventoryStatementsTr3.rpt";
                BaseCls.GlbReportName = "InventoryStatementsTr3.rpt";
                _view.Show();
                _view = null;
            }

            if (opt21.Checked == true)    //Inter Transfer Details
            {
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "INTER TRANSFER DETAILS REPORT";


                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = "";
                if (cmbDocStatus.Text == "")
                    BaseCls.GlbStatus = "X";
                else
                    BaseCls.GlbStatus = cmbDocStatus.Text.ToString().Substring(0, 1);

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "InterTransfer_Details_Report.rpt";
                _view.GlbReportName = "InterTransfer_Details_Report.rpt";
                _view.Show();
                _view = null;

            }

            if (opt22.Checked == true)   //serial ageig 
            {
                //update temporary table '//added by Wimal @ 21/08/2013
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "seria Ageing";

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "SerialAge.rpt";
                BaseCls.GlbUserComCode = txtComp.Text;
                BaseCls.GlbReportProfit = txtPC.Text;
                BaseCls.GlbReportAsAtDate = txtAsAtDate.Value.Date;
                BaseCls.GlbReportHeading = "SERIAL WISE COMPANY/LOCATION AGEING REPORT";
                _view.GlbReportName = "SerialAge.rpt";
                _view.Show();
                _view = null;
            }

            if (opt23.Checked == true)   //Customer Deliveries - Other Profitcenters 
            {   //Sanjeewa 2013-10-14
                //update temporary table 
               // update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportCustomerCode = "";
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDoc = txtDocNo.Text;

                BaseCls.GlbReportHeading = "CUSTOMER DELIVERIERS - OTHER PROFITCENTERS";

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "OtherShop_DO_Report.rpt";
                _view.GlbReportName = "OtherShop_DO_Report.rpt";
                _view.Show();
                _view = null;
            }

            if (opt24.Checked == true)   //Reserved Serials to Customers 
            {   //Sanjeewa 2013-12-11
                //update temporary table 
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "RESERVED SERIALS TO CUSTOMERS";

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "Reserved_Serial_Report.rpt";
                _view.GlbReportName = "Reserved_Serial_Report.rpt";
                _view.Show();
                _view = null;
            }

            if (opt25.Checked == true)    //Fixed Asset Transfer Details
            {
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "FIXED ASSET TRANSFER DETAILS";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                //BaseCls.GlbReportWithCost = Convert.ToInt16((CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075)) ? 1 : 0);
                BaseCls.GlbReportDocType = "";
                if (cmbDocStatus.Text == "")
                    BaseCls.GlbStatus = "X";
                else
                    BaseCls.GlbStatus = cmbDocStatus.Text.ToString().Substring(0, 1);

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "FAT_Dtl_Report.rpt";
                _view.GlbReportName = "FAT_Dtl_Report.rpt";
                _view.Show();
                _view = null;

            }
            if (opt26.Checked == true)   //BOC customer reserve
            {
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                clsSalesRep objSales = new clsSalesRep();
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportProfit = txtPC.Text;

                BaseCls.GlbReportHeading = "BOC Customer Reservation";

                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                BaseCls.GlbReportName = "BOCCusReserveList.rpt";
                objSales.BOCReserveList();

                MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                string _path = _MasterComp.Mc_anal6;
                objSales._bocReserveList.ExportToDisk(ExportFormatType.Excel, _path + "BOCReserveList" + BaseCls.GlbUserID + ".xls");

                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                string workbookPath = _path + "BOCReserveList" + BaseCls.GlbUserID + ".xls";
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
            }

            if (opt27.Checked == true)   //Duty Free Inventory Balance with Price 
            {   //Sanjeewa 2014-10-10
                //update temporary table 
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "DUTY FREE INVENTORY BALANCE WITH PRICE";

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "df_InvBal_Report.rpt";
                _view.GlbReportName = "df_InvBal_Report.rpt";
                _view.Show();
                _view = null;
            }
            if (opt28.Checked == true)
            {

                DateTime start = BaseCls.GlbReportFromDate;
                DateTime finish = BaseCls.GlbReportToDate;

                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                GlbReportName = "BOC Reserved Serials";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;
                if (BaseCls.GlbReportDirection == "IN") BaseCls.GlbReportDirection = "1";
                if (BaseCls.GlbReportDirection == "OUT") BaseCls.GlbReportDirection = "0";
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "BOCReservedSerials.rpt";
                BaseCls.GlbReportName = "BOCReservedSerials.rpt";
                _view.Show();
                _view = null;

            }
            if (opt29.Checked == true)     //re-order report (kapila)
            {
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                GlbReportName = "Items Availability Re-Order";
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                if (MessageBox.Show("Do you want the report with stores and other channel balances.\n This may take several minutes. Are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    BaseCls.GlbReportIsFast = 1;
                else
                    BaseCls.GlbReportIsFast = 0;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "Reorder_Items.rpt";
                BaseCls.GlbReportName = "Reorder_Items.rpt";
                _view.Show();
                _view = null;

            }
            if (opt30.Checked == true)     //PO Summary - Nadeeka
            {
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                GlbReportName = "POSummary";
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                if (opBoth.Checked)
                {
                    BaseCls.GlbReportDocSubType = null;
                }
                if (optLocal.Checked)
                {
                    BaseCls.GlbReportDocSubType = "L";
                }
                if (optImport.Checked)
                {
                    BaseCls.GlbReportDocSubType = "I";
                }

                BaseCls.GlbReportDirection = txtDirec.Text;


                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "POSummary.rpt";
                BaseCls.GlbReportName = "POSummary.rpt";
                _view.Show();
                _view = null;
            }

            if (opt31.Checked == true)   //Insured Stock Value 
            {   //Sanjeewa 2015-07-13
                //update temporary table 
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "INSURED STOCK VALUE";

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "Insu_Stock_Report.rpt";
                _view.GlbReportName = "Insu_Stock_Report.rpt";
                _view.Show();
                _view = null;
            }

            if (opt32.Checked == true)
            {   // Sanjeewa 2016-02-04
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Goods In Transit";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemCat4 = "";
                BaseCls.GlbReportItemCat4 = "";

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.Get_GIT_Details(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbUserID, out _error);
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

            if (opt33.Checked == true) //Temporary Saved Documents
            {//Sanjeewa 2015-10-30
                //update temporary table 
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "TEMPORARY SAVED DOCUMENTS";
                BaseCls.GlbReportDocType = txtDocType.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "temp_saved_doc_report.rpt";
                _view.GlbReportName = "temp_saved_doc_report.rpt";
                _view.Show();
                _view = null;
            }

            if (opt34.Checked == true) //Current Inventory Balance with Selling Price
            {//Sanjeewa 2016-01-20
                //update temporary table 
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "Current Inventory Balance with Selling Price";
                BaseCls.GlbReportSupplier = "";
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemCat4 = "";
                BaseCls.GlbReportItemCat5 = "";
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "Current_balance_with_price.rpt";
                _view.GlbReportName = "Current_balance_with_price.rpt";
                _view.Show();
                _view = null;
            }
            if (opt35.Checked == true) //temporary issue items
            {//kapila
                //update temporary table 
                //update_PC_List();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportHeading = "Temporary Issue Items Report";
                BaseCls.GlbReportProfit = txtPC.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportName = "TemporaryIssueItems.rpt";
                _view.GlbReportName = "TemporaryIssueItems.rpt";
                _view.Show();
                _view = null;
            }

            if (opt36.Checked == true)
            {   // Sanjeewa 2016-03-30
                //update temporary table

                // update_PC_List_RPTDB();

                update_PC_List_RPTDBnew();

                GlbReportName = "Current Age Report";

                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemCat4 = "";
                BaseCls.GlbReportItemCat5 = "";
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportSupplier = txt_supplier.Text;
                BaseCls.GlbReportHeading = "Current Age Analysis Report " + (chkComAge.Checked == true ? "(Company Age)" : "(Location Age)");

                BaseCls.GlbReportIsFast = 0;    //company age
                ReportViewerInventory _view = new ReportViewerInventory();
                BaseCls.GlbReportWithCost = 0;
                BaseCls.GlbReportRole = "N";
                if (chkCost.Checked == true)
                    BaseCls.GlbReportWithCost = 1;

                if (chkComAge.Checked == true)
                {
                    if (chk_exportexcel.Checked == true)
                    {
                        List<string> locList = new List<string>();
                        if (lstPC.Items.Count == 0)
                        {
                            locList.Add(txtPC.Text);
                        }
                        else
                        {
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                //string pc = Item.Text;

                                if (Item.Checked == true)
                                {
                                    List<string> tmpList = new List<string>();
                                    tmpList = Item.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                                    string pc = null;
                                    string com = txtComp.Text;
                                    if ((tmpList != null) && (tmpList.Count > 0))
                                    {
                                        pc = tmpList[0];
                                        com = tmpList[1];

                                        locList.Add(pc);
                                    }
                                }


                                //string pc = Item.Text;

                                //if (Item.Checked == true)
                                //{
                                //    locList.Add(pc);
                                //}
                            }
                        }
                        BaseCls.GlbReportIsFast = 1;
                        string _err = "";
                        string _filePath = CHNLSVC.MsgPortal.getCurrentComAgeDetails(BaseCls.GlbUserComCode, locList, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportBrandMgr, BaseCls.GlbUserID);

                        if (!string.IsNullOrEmpty(_err))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_err);
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

                    }

                    else
                    {
                        BaseCls.GlbReportType = "CURCOM";
                        BaseCls.GlbReportRole = chk_all_com.Checked ? "Y" : "N";
                        if (optloc.Checked == true)
                        {
                            _view.GlbReportName = "LocwiseItemAge.rpt";
                            BaseCls.GlbReportName = "LocwiseItemAge.rpt";
                        }
                        else if (optcat.Checked == true)
                        {
                            _view.GlbReportName = "CatwiseItemAge.rpt";
                            BaseCls.GlbReportName = "CatwiseItemAge.rpt";
                        }
                        else if (optscat.Checked == true)
                        {
                            _view.GlbReportName = "CatScatwiseItemAge.rpt";
                            BaseCls.GlbReportName = "CatScatwiseItemAge.rpt";
                        }
                        else if (optitmbrnd.Checked == true)
                        {
                            _view.GlbReportName = "ItmBrndwiseItemAge.rpt";
                            BaseCls.GlbReportName = "ItmBrndwiseItemAge.rpt";
                        }
                        else if (optcatItem.Checked == true)
                        {
                            _view.GlbReportName = "CatItmwiseItemAge.rpt";
                            BaseCls.GlbReportName = "CatItmwiseItemAge.rpt";
                        }
                        else if (optItm.Checked == true)
                        {
                            _view.GlbReportName = "ItmwiseItemAge.rpt";
                            BaseCls.GlbReportName = "ItmwiseItemAge.rpt";
                        }
                        else if (optLocItm.Checked == true)
                        {
                            _view.GlbReportName = "LocItemStusAge.rpt";
                            BaseCls.GlbReportName = "LocItemStusAge.rpt";
                        }
                        _view.Show();
                        _view = null;

                    }

                }
                else
                {
                    //BaseCls.GlbReportType = "CURCOM";
                    BaseCls.GlbReportType = "CUR";
                    BaseCls.GlbReportRole = chk_all_com.Checked ? "Y" : "N";
                    if (optloc.Checked == true)
                    {
                        _view.GlbReportName = "LocwiseItemAge.rpt";
                        BaseCls.GlbReportName = "LocwiseItemAge.rpt";
                    }
                    else if (optcat.Checked == true)
                    {
                        _view.GlbReportName = "CatwiseItemAge.rpt";
                        BaseCls.GlbReportName = "CatwiseItemAge.rpt";
                    }
                    else if (optscat.Checked == true)
                    {
                        _view.GlbReportName = "CatScatwiseItemAge.rpt";
                        BaseCls.GlbReportName = "CatScatwiseItemAge.rpt";
                    }
                    else if (optitmbrnd.Checked == true)
                    {
                        _view.GlbReportName = "ItmBrndwiseItemAge.rpt";
                        BaseCls.GlbReportName = "ItmBrndwiseItemAge.rpt";
                    }
                    else if (optcatItem.Checked == true)
                    {
                        _view.GlbReportName = "CatItmwiseItemAge.rpt";
                        BaseCls.GlbReportName = "CatItmwiseItemAge.rpt";
                    }
                    else if (optItm.Checked == true)
                    {
                        _view.GlbReportName = "ItmwiseItemAge.rpt";
                        BaseCls.GlbReportName = "ItmwiseItemAge.rpt";
                    }
                    else if (optLocItm.Checked == true)
                    {
                        _view.GlbReportName = "LocItemStusAge.rpt";
                        BaseCls.GlbReportName = "LocItemStusAge.rpt";
                    }
                    else
                    {
                        _view.GlbReportName = "curr_age_report.rpt";
                        BaseCls.GlbReportName = "curr_age_report.rpt";
                    }
                    _view.Show();
                    _view = null;
                }
                //}remove below path - 13/Sep/2018

                ///remove below path - 13/Sep/2018
                //else
                //{
                //    _view.GlbReportName = "curr_age_report.rpt";
                //    BaseCls.GlbReportName = "curr_age_report.rpt";
                //    _view.Show();
                //    _view = null;
                //}

            }

            if (opt37.Checked == true)
            {   // Sanjeewa 2016-06-07
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Local Purchase Cost Details";

                BaseCls.GlbReportPriceBook = txtPB.Text;
                BaseCls.GlbReportPBLevel = txtPBLevel.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.LOPOCostDetailsReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date,
                        BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, out _error);

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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt38.Checked == true)
            {   // Sanjeewa 2016-06-22
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();

                GlbReportName = "Item Canibalised Cost Details Report";

                BaseCls.GlbReportPriceBook = txtPB.Text;
                BaseCls.GlbReportPBLevel = txtPBLevel.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemStatus = txtItemStatus.Text;

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.GetCanibaliseStockReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, "", BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel,
                        BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                        BaseCls.GlbUserID, BaseCls.GlbReportItemStatus, BaseCls.GlbUserComCode, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, out _error);

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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt39.Checked == true)
            {   // kapila 23/6/2016
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();

                GlbReportName = "Consignment Details";

                BaseCls.GlbReportPriceBook = txtPB.Text;
                BaseCls.GlbReportPBLevel = txtPBLevel.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.ConsignDetailsReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date,
                        BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, out _error);

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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt40.Checked == true)     // PSI
            {  // kapila
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();

                GlbReportName = "PSI";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "PSI.rpt";
                BaseCls.GlbReportName = "PSI.rpt";
                _view.Show();
                _view = null;
            }

            if (opt41.Checked == true)     // ITEM AGE ANALYSIS
            {  // kapila
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();

                GlbReportName = "Item Age Analysis";

                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;
                BaseCls.GlbReportType = "";
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtToDate.Value);
                BaseCls.GlbReportPriceBook = txtPB.Text.Trim();
                BaseCls.GlbReportPBLevel = txtPBLevel.Text.Trim();
                //BaseCls.GL
                // BaseCls.GlbReportSupplier = txtSupplier.Text;
                // BaseCls.GlbReportPromotor = txtBrandMan.Text;
                BaseCls.GlbReportHeading = "Age Analysis Report" + (chkComAge.Checked == true ? "(Company Age)" : "(Location Age)");

                if (chkCost.Checked == true)
                    BaseCls.GlbReportWithCost = 1;

                //if (chkComAge.Checked == true)
                BaseCls.GlbReportIsFast = Convert.ToInt16(chkComAge.Checked == true ? 1 : 0);

                ReportViewerInventory _view = new ReportViewerInventory();
                if (optloc.Checked == true)
                {
                    _view.GlbReportName = "LocwiseItemAge.rpt";
                    BaseCls.GlbReportName = "LocwiseItemAge.rpt";
                }
                else if (optcat.Checked == true)
                {
                    _view.GlbReportName = "CatwiseItemAge.rpt";
                    BaseCls.GlbReportName = "CatwiseItemAge.rpt";
                }
                else if (optscat.Checked == true)
                {
                    _view.GlbReportName = "CatScatwiseItemAge.rpt";
                    BaseCls.GlbReportName = "CatScatwiseItemAge.rpt";
                }
                else if (optitmbrnd.Checked == true)
                {
                    _view.GlbReportName = "ItmBrndwiseItemAge.rpt";
                    BaseCls.GlbReportName = "ItmBrndwiseItemAge.rpt";
                }
                else if (optcatItem.Checked == true)
                {
                    _view.GlbReportName = "CatItmwiseItemAge.rpt";
                    BaseCls.GlbReportName = "CatItmwiseItemAge.rpt";
                }
                else if (optItm.Checked == true)
                {
                    _view.GlbReportName = "ItmwiseItemAge.rpt";
                    BaseCls.GlbReportName = "ItmwiseItemAge.rpt";
                }
                else if (optLocItm.Checked == true)
                {
                    _view.GlbReportName = "LocItemStusAge.rpt";
                    BaseCls.GlbReportName = "LocItemStusAge.rpt";
                }
                else if (optitmwise.Checked==true)
                {
                      _view.GlbReportName = "LocItemStusAgenew.rpt";
                    BaseCls.GlbReportName = "LocItemStusAgenew.rpt";
                }
                _view.Show();
                _view = null;
            }
            if (opt42.Checked == true)     // ITEM AGE ANALYSIS with serial
            {  // kapila
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();

                if (chk_exportexcel.Checked == true && chkComAge.Checked == true)    //kapila 19/5/2017
                {
                    if (chkCost.Checked == true)
                    {
                        List<string> locList = new List<string>();
                        if (lstPC.Items.Count == 0)
                        {
                            locList.Add(txtPC.Text);
                        }
                        else
                        {


                            foreach (ListViewItem Item in lstPC.Items)
                            {

                                if (Item.Checked == true)
                                {
                                    List<string> tmpList = new List<string>();
                                    tmpList = Item.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                                    string pc = null;
                                    string com = txtComp.Text;
                                    if ((tmpList != null) && (tmpList.Count > 0))
                                    {
                                        pc = tmpList[0];
                                        com = tmpList[1];

                                        locList.Add(pc);
                                    }
                                }
                            }


                            //foreach (ListViewItem Item in lstPC.Items)
                            //{
                            //    string pc = Item.Text;

                            //    if (Item.Checked == true)
                            //    {
                            //        locList.Add(pc);
                            //    }
                            //}


                        }
                        BaseCls.GlbReportIsFast = 1;
                        string _err = "";
                        string _filePath = CHNLSVC.MsgPortal.getCurrentComAgeDetails_Serials(BaseCls.GlbUserComCode, locList, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportBrandMgr, BaseCls.GlbUserID);

                        if (!string.IsNullOrEmpty(_err))
                        {
                            btnDisplay.Enabled = true;
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_err);
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

                    }
                }

                else
                {
                    clsInventoryRep objInv = new clsInventoryRep();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                    BaseCls.GlbReportWithCost = 0;
                    if (chkroot.Checked == true)
                    {
                        BaseCls.GlbReportDocType = "Route";
                    }
                    else
                    {
                        BaseCls.GlbReportDocType = "Location";
                    }

                    if (chkCost.Checked == true)
                        BaseCls.GlbReportWithCost = Convert.ToInt16(1);
                    if (string.IsNullOrEmpty(txtNoOfDays.Text))
                        BaseCls.GlbReportFromPage = 0;
                    else
                        BaseCls.GlbReportFromPage = Convert.ToInt32(txtNoOfDays.Text);
                    if (string.IsNullOrEmpty(txttoage.Text))
                        BaseCls.GlbReportToPage = 0;
                    else
                        BaseCls.GlbReportToPage = Convert.ToInt32(txttoage.Text);

                    BaseCls.GlbReportIsFast = 0;
                    if (chkComAge.Checked == true)
                        BaseCls.GlbReportIsFast = 1;

                    BaseCls.GlbReportHeading = "Item Age Analysis with Serial" + (chkComAge.Checked == true ? "(Company Age)" : "(Location Age)");

                    ReportViewerInventory _view = new ReportViewerInventory();
                    _view.GlbReportName = "StockBalanceWithSerialAge.rpt";
                    BaseCls.GlbReportName = "StockBalanceWithSerialAge.rpt";
                    _view.Show();
                    _view = null;

                    //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //BaseCls.GlbReportName = "StockBalanceWithSerialAge.rpt";
                    ////  objSales.BOCReserveList();

                    //MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    //string _path = _MasterComp.Mc_anal6;
                    //objInv._bocReserveList.ExportToDisk(ExportFormatType.Excel, _path + "StockBalanceWithSerialAge" + BaseCls.GlbUserID + ".xls");

                    //Excel.Application excelApp = new Excel.Application();
                    //excelApp.Visible = true;
                    //string workbookPath = _path + "StockBalanceWithSerialAge" + BaseCls.GlbUserID + ".xls";
                    //Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                    //        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    //        true, false, 0, true, false, false);
                }
            }

            if (opt43.Checked == true)     // GP report
            {

               // update_PC_List();
                update_PC_List_RPTDBnew();
                string _error = "";
                string _cat = "INV";

                if (optWithFOC.Checked == true)
                    BaseCls.GlbReportIsFreeIssue = 0;
                else if (optWithoutFOC.Checked == true)
                    BaseCls.GlbReportIsFreeIssue = 1;
                else
                    BaseCls.GlbReportIsFreeIssue = 2;

                foreach (ListViewItem Item in lstGroup.Items)
                {
                    _cat = Item.Text;
                    goto a;
                }
            a:

                string _filePath = CHNLSVC.MsgPortal.GetItemWiseGpExcel(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCusId, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode,
                    BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbUserComCode,
                   BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFreeIssue, BaseCls.GlbReportItmClasif, BaseCls.GlbReportBrandMgr,
                    _cat, true/*withReversal */, 0, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, 0, out _error); // (withReversal flag true) 0 - REV, 1- SALE, 2 - ALL (Lakshika 2016/10/20)*/

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
            if (opt44.Checked == true)     // valuation report
            {
                //update_PC_List();
                update_PC_List_RPTDBnew();
                string _error = "";
                string _cat = "ITM";
                string _type = "BOTH";

                _type = optLocal.Checked ? "QTY" : optImport.Checked ? "VAL" : "BOTH";
                Int32 withsts = chkWOStus.Checked ? 0 : 1; // add by tharanga 2017/10/13

                foreach (ListViewItem Item in lstGroup.Items)
                {
                    _cat = Item.Text;
                    goto a;
                }
            a:
                if (chk_exportexcel.Checked == true)
                {
                    string _filePath = "";
                    //string _filePath = CHNLSVC.MsgPortal.getValuationDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItmClasif,
                    //           BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    //           BaseCls.GlbReportItemStatus, _cat, _type, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    if (BaseCls.GlbUserComCode == "ARL")
                    {
                        _filePath = CHNLSVC.MsgPortal.getValuationDetails_ARL(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItmClasif,
                                  BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                                  BaseCls.GlbReportItemStatus, _cat, _type, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    }
                    else
                    {
                        if (chkIns.Checked == true)
                            _filePath = CHNLSVC.MsgPortal.getValuationDetails_Insu(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItmClasif,
                                                    BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                                                    BaseCls.GlbReportItemStatus, _cat, _type, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error, withsts);
                        else
                            _filePath = CHNLSVC.MsgPortal.getValuationDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItmClasif,
                                                BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                                                BaseCls.GlbReportItemStatus, _cat, _type, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
                    }

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
                    BaseCls.GlbReportDocType = "BOTH";
                    BaseCls.GlbReportDocSubType = "ITM";
                    clsInventoryRep objInv = new clsInventoryRep();
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    BaseCls.GlbReportName = "Valuation_Dtl.rpt";
                    string _err = string.Empty;
                    objInv.getValuationDetails_Insu(withsts, out _err);
                    if (!string.IsNullOrEmpty(_err))
                    {
                        MessageBox.Show("Records not found given criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;
                    objInv._valdtl.ExportToDisk(ExportFormatType.Excel, _path + "ItmWiseValuIns" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "ItmWiseValuIns" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);
                    //ReportViewerInventory _view = new ReportViewerInventory();
                    //_view.GlbReportName = "rpt_GLB_Git_Document.rpt";
                    //BaseCls.GlbReportName = "rpt_GLB_Git_Document.rpt";
                    //_view.Show();
                    //_view = null;
                }
            }
            if (opt45.Checked == true)     // aod reconcile report
            {

            }
            if (opt46.Checked == true)     // approval dispatch status report
            {
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                string _err = "";

                string _filePath = CHNLSVC.MsgPortal.getDispatchAppDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate,
                    BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _err);

                if (!string.IsNullOrEmpty(_err))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_err);
                    return;
                }
                if (string.IsNullOrEmpty(_filePath))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Report Generated. Please Download the Excel File from <Download Excel> link.");
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }
            if (opt47.Checked == true)
            {
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                if (optE.Checked == true)
                    BaseCls.GlbReportDocType = "Excess";
                else if (optS.Checked == true)
                    BaseCls.GlbReportDocType = "Short";
                else
                    BaseCls.GlbReportDocType = "Both";

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.ItemBufferStatusReport_Excel(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, "", BaseCls.GlbReportDocType, out _error);

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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt48.Checked == true)
            {   // Sanjeewa 2016-10-13 - GIT As at Report
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Goods In Transit";
                BaseCls.GlbReportHeading = "Goods In Transit";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemCat4 = "";
                BaseCls.GlbReportItemCat5 = "";
                BaseCls.GlbReportOtherLoc = txt_othloc.Text;
                BaseCls.GlbReportDoc = chk_all_com.Checked == true ? "Y" : "N";

                if (chkCost.Checked == true) { BaseCls.GlbReportWithCost = 1; }
                if (chkCost.Checked == false) { BaseCls.GlbReportWithCost = 0; }

                if (chk_exportexcel.Checked == true)
                {
                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.getGITReport_Asat(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbUserID, BaseCls.GlbReportOtherLoc, BaseCls.GlbReportDoc, out _error);
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

                    MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ReportViewerInventory _view = new ReportViewerInventory();
                    _view.GlbReportName = "rpt_GLB_Git_Document.rpt";
                    BaseCls.GlbReportName = "rpt_GLB_Git_Document.rpt";
                    _view.Show();
                    _view = null;
                }
            }
            if (opt49.Checked == true)     // Item List
            {

                string _error = "";
                string _cat = optB.Checked ? "ITEM" : optS.Checked ? "COMPONENT" : "ALL";
                if (txtIcat1.Text != "" && txtIcat1.Text != null)
                {
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text.Trim().ToString();
                }
                string _filePath = CHNLSVC.MsgPortal.getItemListDetails(_cat, BaseCls.GlbReportItemCat1, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt50.Checked == true)
            {   // Sanjeewa 2016-11-18 - Excess Stock Report
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Excess Short Stock Report";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemCat4 = "";
                BaseCls.GlbReportItemCat5 = "";
                BaseCls.GlbReportDoc = optE.Checked ? "EX" : optS.Checked ? "SH" : "BT";

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "excess_stock_report.rpt";
                BaseCls.GlbReportName = "excess_stock_report.rpt";
                _view.Show();
                _view = null;

            }

            if (opt51.Checked == true)     // Showroom Request Report
            {
                //update_PC_List_RPTDB();
                update_PC_List_RPTDBnew();
                string _err = "";

                BaseCls.GlbReportOtherLoc = txt_othloc.Text;

                string _filePath = CHNLSVC.MsgPortal.SRMRNStatusReport(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate,
                        BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                        BaseCls.GlbUserComCode, BaseCls.GlbReportOtherLoc, BaseCls.GlbUserID, out _err);

                if (!string.IsNullOrEmpty(_err))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_err);
                    return;
                }
                if (string.IsNullOrEmpty(_filePath))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }

            if (opt52.Checked == true)     // Sales and Inventory Summary Report
            {
                string _err = "";

                string _filePath = CHNLSVC.MsgPortal.getBondBalanceDetails1(BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _err);

                if (!string.IsNullOrEmpty(_err))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_err);
                    return;
                }
                if (string.IsNullOrEmpty(_filePath))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }

            if (opt53.Checked == true) //Item Condition Details
            {   // Sanjeewa 2017-02-17
                //update temporary table
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Item Condition Details";

                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportItemCat4 = "";
                BaseCls.GlbReportItemCat5 = "";
                BaseCls.GlbReportDoc1 = txtitmcond.Text;
                BaseCls.GlbReportItemStatus = txtItemStatus.Text;
                string _itemcond = chk_allserial.Checked == true ? "N" : "Y";
                string _allroute = "Y";
                string _route = "";

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.GetItemConditionDetail(BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportItemStatus, BaseCls.GlbUserComCode, BaseCls.GlbReportDoc1, BaseCls.GlbUserID, _itemcond, _allroute, _route, out _error);

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

            if (opt55.Checked == true) // Added by Chathura on 09-nov-2017
            {
                if (string.IsNullOrEmpty(txtDocType.Text))
                {
                    MessageBox.Show("Select the Doc Type. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkmulticompany.Checked == true)
                {
                    BaseCls.GlbReportCompCode = companyAllList;
                    BaseCls.GlbReportMulticompStatus = 1;

                }
                else
                {
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportMulticompStatus = 0;
                    //update_PC_List();
                    update_PC_List_RPTDBnew();
                }

                BaseCls.GlbReportDocType = txtDocType.Text;

                if (lstSubDocs.Items.Count == 0)
                {
                    BaseCls.GlbReportDocSubType = txtDocSubType.Text;
                }
                else
                {
                    BaseCls.GlbReportDocSubType = docSubTypesList;
                }
                if (string.IsNullOrEmpty(BaseCls.GlbReportDocSubType))
                {
                    MessageBox.Show("Select the sub Doc Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                TimeSpan _dateTimeDiffarance = BaseCls.GlbReportToDate.Date.Subtract(BaseCls.GlbReportFromDate.Date);

                if (Convert.ToInt32(_dateTimeDiffarance.TotalDays) >= 365)
                {
                    MessageBox.Show("Selected month range grater than one year", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!string.IsNullOrEmpty(txtDirec.Text))
                {
                    BaseCls.GlbReportDirection = txtDirec.Text.Trim().ToString();

                }


                //lblDays.Text += _dateTimeDiffarance.TotalDays.ToString("N");
                //lblMonths.Text += (((_dateTimeDiffarance.TotalDays) / 365) * 12).ToString("N");
                //lblYears.Text += ((_dateTimeDiffarance.TotalDays) / 365).ToString("N");
                //if (monthsApart > 0)
                //{
                //    MessageBox.Show("Selected month range grater than one year", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return; 
                //}

                processMovementSubtypeReport();

            }

            if (opt56.Checked == true)     // Adjusment Detail Report
            {  //Wimal-27/03/2018            
                //update_PC_List();
                update_PC_List_RPTDBnew();

                GlbReportName = "Stock Adjustment  Details";

                BaseCls.GlbReportHeading = GlbReportName;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportComp = txtCompDesc.Text;
                BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportDocType = txtDocType.Text;
                BaseCls.GlbReportDirection = txtDirec.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "AdjusmentCostWithCatType.rpt";
                BaseCls.GlbReportName = "AdjusmentCostWithCatType.rpt";
                _view.Show();
                _view = null;
            }

            if (opt57.Checked == true)//ADD BY THARANGA 2018/05/21
            {
                if (cmbRemTp.Text == "")
                {

                    MessageBox.Show("Cannot process. select the age ragnge ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                update_PC_List_RPTDBnew();

                BaseCls.GlbReportType = "";
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtToDate.Value);
                BaseCls.GlbReportItemType = cmbRemTp.SelectedValue.ToString();
                BaseCls.GlbReportDocType = cmbRemTp.Text.ToString();
                // BaseCls.GlbReportSupplier = txtSupplier.Text;
                // BaseCls.GlbReportPromotor = txtBrandMan.Text;
                BaseCls.GlbReportHeading = "Summarized Ageing Report for Provisioning";
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "summarized_age_report.rpt";
                BaseCls.GlbReportName = "summarized_age_report.rpt";
                _view.Show();
                _view = null;



            }
            if (opt58.Checked == true)//ADD BY THARANGA 2018/05/21
            {
                //if (cmbRemTp.Text == "")
                //{

                //    MessageBox.Show("Cannot process. select the age ragnge ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                update_PC_List_RPTDBnew();

                BaseCls.GlbReportType = "";
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtToDate.Value);
                //BaseCls.GlbReportItemType = cmbRemTp.SelectedValue.ToString();
                //BaseCls.GlbReportDocType = cmbRemTp.Text.ToString();
                // BaseCls.GlbReportSupplier = txtSupplier.Text;
                // BaseCls.GlbReportPromotor = txtBrandMan.Text;
                BaseCls.GlbReportHeading = "Status Wise Ageing Report";
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "Status_wise_ageing_report.rpt";
                BaseCls.GlbReportName = "Status_wise_ageing_report.rpt";
                _view.Show();
                _view = null;


                
            }
            if (opt59.Checked == true)//ADD BY THARANGA 2018/05/21
            {
              
                update_PC_List_RPTDBnew();

                BaseCls.GlbReportType = "";
                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtToDate.Value);
               
                BaseCls.GlbReportDocType = cmbRemTp.Text.ToString();
                BaseCls.GlbReportDoc = cmbMonth.Text.ToString();
                // BaseCls.GlbReportSupplier = txtSupplier.Text;
                // BaseCls.GlbReportPromotor = txtBrandMan.Text;
                BaseCls.GlbReportHeading = "Disposal Summary Report";
                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "Disposal_summary.rpt";
                BaseCls.GlbReportName = "Disposal_summary.rpt";
                _view.Show();
                _view = null;



            }

            if (opt60.Checked == true) // tharindu 2018-06-02
            {
                //update_PC_List();
                update_PC_List_RPTDBnew();
                GlbReportName = "Charge_Sheet";

                BaseCls.GlbReportDoc = txtDocNo.Text;

                ReportViewerInventory _view = new ReportViewerInventory();
                _view.GlbReportName = "Charge_Sheet.rpt";
                BaseCls.GlbReportName = "Charge_Sheet.rpt";
                _view.Show();
                _view = null;

            }


            if (opt61.Checked == true)     // Sales and Inventory bal Locationwise
            {
                string _err = "";
                DataTable dt = new DataTable();

                update_PC_List_RPTDBnew();
                dt = CHNLSVC.MsgPortal.GetLocationCount(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                if(dt.Rows.Count > 0)
                {
                    int val = int.Parse(dt.Rows[0][0].ToString());
                    if (val > 24)
                    {
                        MessageBox.Show("You're not be able to select more than 25 locations", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    
                }
                  


                string _filePath = CHNLSVC.MsgPortal.GetSalesWithInv_Bal_loc_wise(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                        BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode,
                        BaseCls.GlbReportProfit, BaseCls.GlbUserDefLoca, txtAsAtDate.Text, BaseCls.GlbUserID, "", "", BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail, "", out _err);

                if (!string.IsNullOrEmpty(_err))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_err);
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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

              if (opt62.Checked == true)     // Wimal 28/08/2018 Abstract Sales Stock Bal report
            {
                string _err = "";
                DataTable dt = new DataTable();

                update_PC_List_RPTDBnew();
                dt = CHNLSVC.MsgPortal.GetLocationCount(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                if(dt.Rows.Count == 0)
                {                    
                        MessageBox.Show("Select W/H location", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                if (!(BaseCls.GlbReportFromDate.Year == BaseCls.GlbReportToDate.Year && BaseCls.GlbReportFromDate.Month == BaseCls.GlbReportToDate.Month))
                {
                    MessageBox.Show("Date Range should be in same month", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                string _filePath = CHNLSVC.MsgPortal.Get_ABT_sales_W_StkBal(BaseCls.GlbUserComCode, txt_othloc.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportAsAtDate,
                    BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode,"",
                    BaseCls.GlbUserID, out _err);

                //string _filePath = CHNLSVC.MsgPortal.GetSalesWithInv_Bal_loc_wise(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                //     BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode,
                //     BaseCls.GlbReportProfit, BaseCls.GlbUserDefLoca, txtAsAtDate.Text, BaseCls.GlbUserID, "", "", BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail, "", out _err);

                if (!string.IsNullOrEmpty(_err))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_err);
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

                MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

              if (opt63.Checked == true)     // Inventory Roll forward 
              {
                  string _err = "";
                  DataTable dt = new DataTable();

                  BaseCls.GlbReportHeading = GlbReportName;
                  BaseCls.GlbReportCompCode = txtComp.Text;
                  BaseCls.GlbReportComp = txtCompDesc.Text;
                  BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                  BaseCls.GlbReportBrand = txtBrand.Text;
                  BaseCls.GlbReportModel = txtModel.Text;
                  BaseCls.GlbReportItemCode = txtItemCode.Text;
                  BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                  BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                  BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                

                  update_PC_List_RPTDBnew();
                  dt = CHNLSVC.MsgPortal.GetLocationCount(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                  if (dt.Rows.Count == 0)
                  {
                      MessageBox.Show("Select W/H location", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }

                  string _filePath = CHNLSVC.MsgPortal.get_InvRoll_Fwd(BaseCls.GlbUserComCode, txt_othloc.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportAsAtDate,
                      BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, "",
                      BaseCls.GlbUserID, out _err);

                  if (!string.IsNullOrEmpty(_err))
                  {
                      btnDisplay.Enabled = true;
                      Cursor.Current = Cursors.Default;
                      MessageBox.Show(_err);
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

                  MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }

              if (opt64.Checked == true)     // . Selling price of inventory items 
              {
                  string _err = "";
                  DataTable dt = new DataTable();

                  update_PC_List_RPTDBnew();
                  dt = CHNLSVC.MsgPortal.GetLocationCount(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                  if (dt.Rows.Count == 0)
                  {
                      MessageBox.Show("Select location", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }

                  if (txtPB.Text == "" || txtPBLevel.Text == "")
                  {
                      MessageBox.Show("pls select price book and level", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }

                 PriceBookLevelRef pb = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPB.Text.Trim(), txtPBLevel.Text.Trim());
                 if (pb.Sapl_is_serialized)
                  {
                      MessageBox.Show("Serlized Price Book Level not allowed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }

                  BaseCls.GlbReportPriceBook = txtPB.Text.Trim();
                  BaseCls.GlbReportPBLevel = txtPBLevel.Text.Trim();

                  string _filePath = CHNLSVC.MsgPortal.get_Inv_WSelPrice(BaseCls.GlbUserComCode, txtCompDesc.Text, txt_othloc.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportAsAtDate,
                      BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, "",
                      BaseCls.GlbUserID, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, out _err);

                  //string _filePath = CHNLSVC.MsgPortal.GetSalesWithInv_Bal_loc_wise(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                  //     BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode,
                  //     BaseCls.GlbReportProfit, BaseCls.GlbUserDefLoca, txtAsAtDate.Text, BaseCls.GlbUserID, "", "", BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail, "", out _err);

                  if (!string.IsNullOrEmpty(_err))
                  {
                      btnDisplay.Enabled = true;
                      Cursor.Current = Cursors.Default;
                      MessageBox.Show(_err);
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

                  MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }      

              if (opt65.Checked == true)     // Wimal 02/Oct/2018 Age Monitoring // Modified Sanjeewa 2018-10-10
              {
                  string _err = "";

                  update_PC_List_RPTDBnew();
                  
                  if (chk_useagecat.Checked)
                  {
                      if (txtageSlt1.Text == "" || txtageSlt2.Text == "" || txtageSlt3.Text == "" || txtageSlt4.Text == "" || txtageSlt5.Text == "")
                      {
                          MessageBox.Show("Enter age categories", "Missing Age Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          return;
                      }
                  }

                  DataRow dr;
                  DataTable _dtAgeSlot = new DataTable();
                  _dtAgeSlot.TableName = "AgeSlot";

                  _dtAgeSlot.Columns.Add("usenewcat", typeof(int));
                  _dtAgeSlot.Columns.Add("rags_slot_l1", typeof(int));
                  _dtAgeSlot.Columns.Add("rags_slot_l2", typeof(int));
                  _dtAgeSlot.Columns.Add("rags_slot_l3", typeof(int));
                  _dtAgeSlot.Columns.Add("rags_slot_l4", typeof(int));
                  _dtAgeSlot.Columns.Add("rags_slot_l5", typeof(int));
                  _dtAgeSlot.Columns.Add("rags_slot_g1", typeof(int));

                  dr = _dtAgeSlot.NewRow();
                  dr["usenewcat"] = chk_useagecat.Checked ? 1 : 0;
                  dr["rags_slot_l1"] = txtageSlt1.Text == "" ? 0 : Convert.ToInt16(txtageSlt1.Text);
                  dr["rags_slot_l2"] = txtageSlt2.Text == "" ? 0 : Convert.ToInt16(txtageSlt2.Text);
                  dr["rags_slot_l3"] = txtageSlt3.Text == "" ? 0 : Convert.ToInt16(txtageSlt3.Text);
                  dr["rags_slot_l4"] = txtageSlt4.Text == "" ? 0 : Convert.ToInt16(txtageSlt4.Text);
                  dr["rags_slot_l5"] = txtageSlt5.Text == "" ? 0 : Convert.ToInt16(txtageSlt5.Text);
                  dr["rags_slot_g1"] = txtageSlt5.Text == "" ? 0 : Convert.ToInt16(txtageSlt5.Text);
                  _dtAgeSlot.Rows.Add(dr);
                  
                  string _filePath = CHNLSVC.MsgPortal.Get_AgeMonitoringDtl(BaseCls.GlbUserComCode, BaseCls.GlbReportAsAtDate, DateTime.Now, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, _dtAgeSlot, BaseCls.GlbUserID, chkComAge.Checked, true, out  _err);

                  if (!string.IsNullOrEmpty(_err))
                  {
                      btnDisplay.Enabled = true;
                      Cursor.Current = Cursors.Default;
                      MessageBox.Show(_err);
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

                  MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }


              if (opt66.Checked == true)     // . Bin Card Report
              {
                  string _err = "";
                  DataTable dt = new DataTable();
               
                  update_PC_List_RPTDBnew();
                  dt = CHNLSVC.MsgPortal.GetLocationCount(BaseCls.GlbUserComCode, BaseCls.GlbUserID);
                  if (dt.Rows.Count > 0)
                  {
                      int val = int.Parse(dt.Rows[0][0].ToString());
                      if (val > 1)
                      {
                          MessageBox.Show("Not allowed to select more than 1 locations", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          return;
                      }

                  }

                  if (txtItemCode.Text=="")
                  {
                      MessageBox.Show("At least 1 Item must select ", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }


                  if (optdtl.Checked)
                  {
                      MasterItem itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text.Trim());
                      if (itm.Mi_is_ser1 != 1)
                      {
                          MessageBox.Show("Detail report allowed only for serialized items ", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          return;                      
                      }
                  }
                  //if (txtPB.Text == "" || txtPBLevel.Text == "")
                  //{
                  //    MessageBox.Show("pls select price book and level", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  //    return;
                  //}

                  //PriceBookLevelRef pb = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPB.Text.Trim(), txtPBLevel.Text.Trim());
                  //if (pb.Sapl_is_serialized)
                  //{
                  //    MessageBox.Show("Serlized Price Book Level not allowed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  //    return;
                  //}

                  //BaseCls.GlbReportPriceBook = txtPB.Text.Trim();
                  //BaseCls.GlbReportPBLevel = txtPBLevel.Text.Trim();

                  BaseCls.GlbReportCompCode = txtComp.Text;
                  BaseCls.GlbReportComp = txtCompDesc.Text;
                  BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                  BaseCls.GlbReportBrand = txtBrand.Text;
                  BaseCls.GlbReportModel = txtModel.Text;
                  BaseCls.GlbReportItemCode = txtItemCode.Text;
                  BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                  BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                  BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                  BaseCls.GlbReportDocType = txtDocType.Text;
                  BaseCls.GlbReportDirection = txtDirec.Text;

                  //string _filePath = CHNLSVC.MsgPortal.get_BinCardReport(BaseCls.GlbUserComCode, txtCompDesc.Text, txt_othloc.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, 
                  //    BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, "",
                  //    BaseCls.GlbUserID, optdtl.Checked ? "SERIAL" : "NSERIAL", out _err);


                  string _filePath = CHNLSVC.MsgPortal.get_BinCardReport(BaseCls.GlbUserComCode, txtCompDesc.Text, txt_othloc.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate,
                     string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, BaseCls.GlbReportItemCode, string.Empty,
                      BaseCls.GlbUserID, optdtl.Checked ? "SERIAL" : "NSERIAL", out _err);


               

                  if (!string.IsNullOrEmpty(_err))
                  {
                      btnDisplay.Enabled = true;
                      Cursor.Current = Cursors.Default;
                      MessageBox.Show(_err);
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

                  MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }

              if (opt67.Checked == true)
              {   
                  
                  // Wimal 05/Nov/2018
                  if (cmbYear.Text == cmbYear_2.Text && cmbMonth.Text == cmbMonth_2.Text)
                  {
                      btnDisplay.Enabled = true;
                      Cursor.Current = Cursors.Default;
                      MessageBox.Show("Same Year and month Can`t be use", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }

                  update_PC_List_RPTDBnew();
                  
                  GlbReportName = "Age Summery Report";
                  BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                  BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                  BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtToDate_2.Value).Date;
                  BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                  BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                  BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                  BaseCls.GlbReportItemCat4 = "";
                  BaseCls.GlbReportItemCat5 = "";
                  BaseCls.GlbReportItemCode = txtItemCode.Text;
                  BaseCls.GlbReportBrand = txtBrand.Text;
                  BaseCls.GlbReportModel = txtModel.Text;
                  BaseCls.GlbReportSupplier = txt_supplier.Text;
                  //BaseCls.GlbReportHeading = "Age Summery Report " + (chkComAge.Checked == true ? "(Company Age)" : "(Location Age)");
                  BaseCls.GlbReportHeading = "Age Summery Report " +  "(Company Age)" ;

                  BaseCls.GlbReportIsFast = 0;    //company age
                  ReportViewerInventory _view = new ReportViewerInventory();
                  BaseCls.GlbReportWithCost = 0;
                  BaseCls.GlbReportRole = "N";
                  if (chkCost.Checked == true)
                      BaseCls.GlbReportWithCost = 1;

                  _view.GlbReportName = "AgeSummery.rpt";
                  BaseCls.GlbReportName = "AgeSummery.rpt";
                      _view.Show();
                      _view = null;
                  
                  //}remove below path - 13/Sep/2018

                  ///remove below path - 13/Sep/2018
                  //else
                  //{
                  //    _view.GlbReportName = "curr_age_report.rpt";
                  //    BaseCls.GlbReportName = "curr_age_report.rpt";
                  //    _view.Show();
                  //    _view = null;
                  //}

              }

            btnDisplay.Enabled = true;
        }



        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt2.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7002")) 
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7002))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7002 )");
                        opt2.Checked = false;
                        return;
                    }
                    setFormControls(2);
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

        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt3.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7003"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7003))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7003)");
                        opt3.Checked = false;
                        return;
                    }
                    setFormControls(3);
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

        private void opt4_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (opt4.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7004"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7004))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7004 )");
                        opt4.Checked = false;
                        return;
                    }
                    setFormControls(4);
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

        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt5.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7005"))
                    //Edit by Chamal 22/03/2013

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7005))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7005 )");
                        opt5.Checked = false;
                        return;
                    }
                    setFormControls(5);
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

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt6.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7006"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7006))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7006 )");
                        opt6.Checked = false;
                        return;
                    }
                    setFormControls(6);
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

        private void opt7_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (opt7.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7007"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7007))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7007 )");
                        opt7.Checked = false;
                        return;
                    }
                    setFormControls(7);
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

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt8.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7008"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7008))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7008 )");
                        opt8.Checked = false;
                        return;
                    }
                    setFormControls(8);
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

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt9.Checked == true)
                {
                    if (CheckPermission) // by akila 2017/0/12
                    {
                        //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7009"))
                        //Edit by Chamal 22/03/2013
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7009))
                        {
                            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7009 )");
                            opt9.Checked = false;
                            return;
                        }
                    }

                    setFormControls(9);
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

        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt10.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7010"))
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7010))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7010 )");
                        opt10.Checked = false;
                        return;
                    }
                    setFormControls(10);
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

        private void txtIcat3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat3_Click(null, null);
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Itm_Click(null, null);
            }
        }

        private void btn_Srch_Brnd_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
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
            //Code by Chamal 03/05/2013
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
            DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocType;
            _CommonSearch.txtSearchbyword.Text = txtDocType.Text;
            _CommonSearch.ShowDialog();
            txtDocType.Focus();
        }

        private void btn_Srch_DocSubTp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocType.Text))
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocSubType;
                _CommonSearch.ShowDialog();
                txtDocSubType.Focus();
            }
            else
            {
                MessageBox.Show("Please select the document type!", "Document Sub Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDocType.Focus();
            }
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

        private void txtItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Itm_Stus_Click(null, null);
            }
        }

        private void opt11_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt11.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7011))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7011 )");
                        opt11.Checked = false;
                        return;
                    }
                    setFormControls(11);
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

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
        }

        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        }

        private void txtAsAtDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
        }

        private void txtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtAsAtDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void btn_Srch_Dir_Click(object sender, EventArgs e)
        {
            try
            {
                //Code by Chamal 03/05/2013
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InventoryDirection);
                DataTable _result = CHNLSVC.CommonSearch.GetInventoryDirections(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDirec;
                _CommonSearch.txtSearchbyword.Text = txtDirec.Text;
                _CommonSearch.ShowDialog();
                txtDirec.Focus();
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

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt12.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7002")) 
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7012))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7012 )");
                        opt12.Checked = false;
                        return;
                    }
                    setFormControls(12);
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

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt13.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7013))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7013 )");
                        opt13.Checked = false;
                        return;
                    }
                    setFormControls(13);
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

        private void opt14_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt14.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7014))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7014 )");
                        opt14.Checked = false;
                        return;
                    }
                    setFormControls(14);
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

        private void opt15_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt15.Checked == true)
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7015))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7015 )");
                        opt15.Checked = false;
                        return;
                    }
                    setFormControls(15);
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

        private void opt16_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt16.Checked == true)
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7016))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7016 )");
                        opt16.Checked = false;
                        return;
                    }
                    setFormControls(16);
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
        private void opt17_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt17.Checked == true)
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7017))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7017 )");
                        opt17.Checked = false;
                        return;
                    }
                    //setFormControls(17);
                    //Edit by Chamal 17-02-2014
                    setFormControls(25);
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

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNoOfDays_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
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

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (txtComp.Text == "")
                {
                    MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                DataTable _result = new DataTable();
                if (chkroot.Checked == true)// add by tharanga 2017/08/29
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Route_cd);
                    _result = CHNLSVC.CommonSearch.Get_route_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();

                load_PCDesc();

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

        private void load_PCDesc()
        {
            DataTable LocDes = new DataTable();
            txtPCDesn.Text = "";
            if (chkroot.Checked == true) // add by tharanga 2017/08/29
            {

                LocDes = CHNLSVC.Sales.getrootDesc(BaseCls.GlbUserComCode, txtPC.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtPCDesn.Text = row2["frh_desc"].ToString();
                }
            }
            else
            {
                LocDes = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", txtPC.Text);
                foreach (DataRow row2 in LocDes.Rows)
                {
                    txtPCDesn.Text = row2["descp"].ToString();
                }
            }

        }

        private void txtIcat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat1_Click(null, null);
        }

        private void txtIcat2_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat2_Click(null, null);
        }

        private void txtIcat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat3_Click(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Click(null, null);
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Brnd_Click(null, null);
        }

        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Model_Click(null, null);
        }

        private void txtItemStatus_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Stus_Click(null, null);
        }

        private void txtDocType_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Doc_Tp_Click(null, null);
        }

        private void txtDocType_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Doc_Tp_Click(null, null);
            }
        }

        private void txtDocSubType_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_DocSubTp_Click(null, null);
        }

        private void txtDocSubType_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_DocSubTp_Click(null, null);
            }
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

        private void opt18_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt18.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7018))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7018 )");
                        opt18.Checked = false;
                        return;
                    }
                    setFormControls(18);
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

        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt19.Checked == true)
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7019))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7019 )");
                        opt19.Checked = false;
                        return;
                    }
                    setFormControls(19);
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

        private void opt20_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt20.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7020))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7020 )");
                        opt20.Checked = false;
                        return;
                    }
                    setFormControls(20);
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

        private void update_PC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);

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

        private void opt21_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt21.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7021))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7021 )");
                        opt21.Checked = false;
                        return;
                    }
                    setFormControls(21);
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

        private void opt21_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void opt22_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt22.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7022))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7022 )");
                        opt22.Checked = false;
                        return;
                    }
                    setFormControls(22);
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

        private void opt23_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt23.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7023))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7023 )");
                        opt22.Checked = false;
                        return;
                    }
                    setFormControls(23);
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

        private void opt24_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (opt24.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7024))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7024 )");
                        opt22.Checked = false;
                        return;
                    }
                    setFormControls(24);
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

        private void opt25_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt25.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7025))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7025 )");
                        opt22.Checked = false;
                        return;
                    }
                    setFormControls(25);
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

        private void txtPC_Leave(object sender, EventArgs e)
        {
            load_PCDesc();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void opt26_CheckedChanged(object sender, EventArgs e)
        {
            if (opt26.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6026))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6026)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(26);
            }
        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {
            if (opt27.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7027))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7027)");
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
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7027))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7027)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(28);
            }
        }

        private void opt29_CheckedChanged(object sender, EventArgs e)
        {
            if (opt29.Checked == true)
            {
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7029))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7029)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(29);
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

        private void opt34_CheckedChanged(object sender, EventArgs e)
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
        //hasith 28/01/2016
        private void opt567_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt2.Checked == true)
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16045))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16045 )");
                        opt2.Checked = false;
                        return;
                    }
                    setFormControls(567);
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

        private void opt35_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(35);
        }

        private void opt36_CheckedChanged(object sender, EventArgs e)
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

        private void opt37_CheckedChanged(object sender, EventArgs e)
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

        private void opt38_CheckedChanged(object sender, EventArgs e)
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

        private void opt39_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(39);
        }

        private void btn_close_admin_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstAdmin.Items)
            {
                Item.Checked = false;
            }
            pnlAdmin.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lstAdmin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            foreach (ListViewItem lstItem in lstAdmin.Items)
            {
                if (lstItem.Text != e.Item.Text)
                {
                    // lstItem.Checked = false;
                }
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
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

            //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10140))
            //{
            //    MessageBox.Show("Sorry, You have no permission to view admin team!\n( Advice: Required permission code :10140 )");
            //    return;
            //}
            //pnlAdmin.Visible = true;
        }

        private void opt40_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(40);
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void opt41_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(41);
        }

        private void chkCost_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10141))
            {
                MessageBox.Show("Sorry, You have no permission to view with cost!\n( Advice: Required permission code :10141 )");
                chkCost.Checked = false;
                return;
            }
        }

        private void opt42_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(42);
        }

        private void chkComAge_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10142))
            {
                MessageBox.Show("Sorry, You have no permission!\n( Advice: Required permission code :10142 )");
                chkComAge.Checked = false;
                return;
            }
        }

        private void opt43_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt43.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11500))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11500 )");
                        opt43.Checked = false;
                        return;
                    }
                    setFormControls(43);
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

        private void opt44_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt44.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11505))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11505 )");
                        opt44.Checked = false;
                        return;
                    }
                    setFormControls(44);
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

        private void opt45_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt45.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 12542))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :12542 )");
                        opt45.Checked = false;
                        return;
                    }
                    setFormControls(45);
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

        private void opt46_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt46.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 12541))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :12541)");
                        opt46.Checked = false;
                        return;
                    }
                    setFormControls(46);
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

        private void lbl1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT1")
                    return;
            }
            lstGroup.Items.Add(("CAT1").ToString());
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

        private void lbl7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "PC")
                    return;
            }
            lstGroup.Items.Add(("PC").ToString());
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

        private void opt47_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt47.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 12531))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :12531)");
                        opt47.Checked = false;
                        return;
                    }
                    setFormControls(47);
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

        private void opt48_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt48.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7048))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7048)");
                        opt47.Checked = false;
                        return;
                    }
                    setFormControls(48);
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

        private void chk_othloc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_othloc.Checked == true)
            {
                txt_othloc.Text = "";
                txt_othloc.Enabled = false;
                btn_othloc.Enabled = false;
            }
            else
            {
                txt_othloc.Enabled = true;
                btn_othloc.Enabled = true;
            }
        }

        private void btn_othloc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_othloc;
                _CommonSearch.ShowDialog();
                txt_othloc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Srch_Doc_Click(object sender, EventArgs e)
        {

        }

        private void opt49_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt49.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7049))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7049)");
                        opt49.Checked = false;
                        return;
                    }
                    setFormControls(49);
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

        private void opt50_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt50.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 12543))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :12543)");
                        opt50.Checked = false;
                        return;
                    }
                    setFormControls(50);
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

        private void opt51_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt51.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 12544))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :12544)");
                        opt51.Checked = false;
                        return;
                    }
                    setFormControls(51);
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

        private void opt52_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt52.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11504))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11504)");
                        opt52.Checked = false;
                        return;
                    }
                    setFormControls(52);
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

        private void opt53_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt53.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7053))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7053)");
                        opt53.Checked = false;
                        return;
                    }
                    setFormControls(53);
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

        private void chkitmcond_CheckedChanged(object sender, EventArgs e)
        {
            if (chkitmcond.Checked == true)
            {
                txtitmcond.Text = "";
                txtitmcond.Enabled = false;
                btnitmcond.Enabled = false;
            }
            else
            {
                txtitmcond.Enabled = true;
                btnitmcond.Enabled = true;
            }
        }

        private void btnitmcond_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable _result = CHNLSVC.CommonSearch.SER_REF_COND_TP_NEW(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtitmcond;
            _CommonSearch.ShowDialog();
            txtitmcond.Focus();
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

        private void chkroot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkroot.Checked == true)
            {
                label5.Text = "Route";
                txtPC.Text = "";
                txtPCDesn.Text = "";
                lstPC.Clear();
            }
            else
            {
                label5.Text = "Location";
                txtPC.Text = "";
                txtPCDesn.Text = "";
                lstPC.Clear();

            }
        }

        private void opt55_CheckedChanged(object sender, EventArgs e) // Added by Chathura on 09-nov-2017
        {
            try
            {
                if (opt55.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7002")) 
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7012))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7012 )");
                        opt55.Checked = false;
                        return;
                    }
                    BindAllCompanies();
                    setFormControls(55);
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

        private void chkmulticompany_CheckedChanged(object sender, EventArgs e) // Added by Chathura on 09-nov-2017
        {
            if (chkmulticompany.Checked == true)
            {
                pnlMulCom.Visible = true;
                lstPC.Enabled = false;
                btnAddItem.Enabled = false;
            }
            else
            {
                foreach (ListViewItem Item in lstMulCom.Items)
                {
                    Item.Checked = false;
                }
                pnlMulCom.Visible = false;
                lstPC.Enabled = true;
                btnAddItem.Enabled = true;
            }
        }

        private void btnCloseMulCom_Click(object sender, EventArgs e) // Added by Chathura on 09-nov-2017
        {
            //foreach (ListViewItem Item in lstMulCom.Items)
            //{
            //    Item.Checked = false;
            //}
            chkmulticompany.Checked = false;
            pnlMulCom.Visible = false;
        }

        private void lstMulCom_ItemChecked(object sender, ItemCheckedEventArgs e) // Added by Chathura on 09-nov-2017
        {
            //BaseCls.GlbReportCompCode = "";
            companyAllList = "";
            int countForList = 0;
            foreach (ListViewItem lstItem in lstMulCom.Items)
            {
                if (lstItem.Checked == true)
                {
                    if (countForList == 0)
                    {
                        companyAllList = lstItem.Text;
                    }
                    else
                    {
                        companyAllList = companyAllList + "," + lstItem.Text;
                    }
                    countForList++;
                }
            }
        }

        private void lblAddDocSub_Click(object sender, EventArgs e) // Added by Chathura on 09-nov-2017
        {
            bool hasRecord = false;
            foreach (ListViewItem lstItem in lstSubDocs.Items)
            {
                if (lstItem.Text == txtDocSubType.Text)
                {
                    hasRecord = true;
                    break;
                }
            }

            if (txtDocSubType.Text == "")
            {

            }
            else if (hasRecord == true)
            {
                MessageBox.Show("Sub Doc Type " + txtDocSubType.Text + " Alredy added to the list");

                return;
            }
            else
            {
                ListViewItem listAddedItem = lstSubDocs.Items.Add(txtDocSubType.Text.ToString());
                listAddedItem.Checked = true;
                pnlSubDocs.Visible = true;

                docSubTypesList = "";
                int countForList = 0;
                foreach (ListViewItem lstItem in lstSubDocs.Items)
                {
                    if (lstItem.Checked == true)
                    {
                        if (countForList == 0)
                        {
                            docSubTypesList = lstItem.Text;
                        }
                        else
                        {
                            docSubTypesList = docSubTypesList + "," + lstItem.Text;
                        }
                        countForList++;
                    }
                }
            }
        }

        private void lstSubDocs_ItemChecked(object sender, ItemCheckedEventArgs e) // Added by Chathura on 09-nov-2017
        {
            docSubTypesList = "";
            int countForList = 0;
            foreach (ListViewItem lstItem in lstSubDocs.Items)
            {
                if (lstItem.Checked == true)
                {
                    if (countForList == 0)
                    {
                        docSubTypesList = lstItem.Text;
                    }
                    else
                    {
                        docSubTypesList = docSubTypesList + "," + lstItem.Text;
                    }
                    countForList++;
                }
            }
        }

        private void btnCloseSubDoc_Click(object sender, EventArgs e) // Added by Chathura on 09-nov-2017
        {
            //foreach (ListViewItem Item in lstSubDocs.Items)
            //{
            //    Item.Checked = false;
            //}
            pnlSubDocs.Visible = false;
        }

        public void processMovementSubtypeReport() // Added by Chathura on 09-nov-2017
        {
            DataTable tblReportData = CHNLSVC.Sales.processMovementSubtypeReport(
                BaseCls.GlbReportCompCode,
                BaseCls.GlbReportMulticompStatus,
                BaseCls.GlbReportDocType,
                BaseCls.GlbReportDocSubType,
                BaseCls.GlbReportFromDate,
                BaseCls.GlbReportToDate,
                BaseCls.GlbUserID);
            DataSet dtSet;
            String _detection = "0";

            if (tblReportData.Rows.Count <= 0)
            {
                MessageBox.Show("Record Not Found");
                return;
            }





            if (!String.IsNullOrEmpty(BaseCls.GlbReportDirection))
            {
                if (BaseCls.GlbReportDirection == "IN")
                {
                    _detection = "1";
                }
                else if (BaseCls.GlbReportDirection == "OUT")
                {
                    _detection = "0";
                }
                if (tblReportData.Rows.Count > 1)
                {
                    IEnumerable<DataRow> results = (from MyRows in tblReportData.AsEnumerable()
                                                    where
                                                     MyRows.Field<String>("ITH_DIRECT") == _detection

                                                    select MyRows);
                    tblReportData = results.CopyToDataTable();
                }
            }
            DataTable dTable = tblReportData;
            if (dTable.Rows.Count <= 0)
            {
                MessageBox.Show("Record Not Found");
                return;
            }
            #region add table heading
            DataTable param = new DataTable("dt");
            DataRow dr;


            param.Clear();
            param.Columns.Add("User", typeof(string));
            param.Columns.Add("Report Name", typeof(string));
            param.Columns.Add("Period", typeof(string));
            param.Columns.Add("Company", typeof(string));
            param.Columns.Add("Profitcenter", typeof(string));
            param.Columns.Add("Doc type", typeof(string));
            param.Columns.Add("Sub Doc Type", typeof(string));
            param.Columns.Add(".", typeof(string));
            //param.Columns.Add("model", typeof(string));
            //param.Columns.Add("cat1", typeof(string));
            //param.Columns.Add("cat2", typeof(string));
            //param.Columns.Add("cat3", typeof(string));
            //param.Columns.Add("cat4", typeof(string));
            //param.Columns.Add("cat5", typeof(string));

            dr = param.NewRow();
            dr["User"] = "User :" + BaseCls.GlbUserID + " On " + DateTime.Now;
            dr["Report Name"] = "Movement Subtype Wise Summary";
            dr["Period"] = "From " + Convert.ToString(BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy")) + " To " + Convert.ToString(BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy"));
            dr["Company"] = BaseCls.GlbReportCompCode;
            dr["Profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["Doc type"] = BaseCls.GlbReportDocType;
            dr["Sub Doc Type"] = BaseCls.GlbReportDocSubType;
            dr["."] = " ";
            //dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            //dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            //dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            //dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            //dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            //dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            //dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4;
            //dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5;

            param.Rows.Add(dr);
            #endregion
            #region create temp table
            System.Data.DataTable custTable = new DataTable("Details");
            DataColumn dtColumn;
            DataRow myDataRow;
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "MAIN CATEGORY DESCRIPTION";
            dtColumn.Caption = "MAIN CATEGORY DESCRIPTION";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Jan qty";
            dtColumn.Caption = "Jan qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Jan val";
            dtColumn.Caption = "Jan val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Feb qty";
            dtColumn.Caption = "Feb qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Feb val";
            dtColumn.Caption = "Feb val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Mar qty";
            dtColumn.Caption = "Mar qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Mar val";
            dtColumn.Caption = "Mar val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Apr qty";
            dtColumn.Caption = "Aprl qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Apr val";
            dtColumn.Caption = "Aprl val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "May qty";
            dtColumn.Caption = "May qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "May val";
            dtColumn.Caption = "May val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Jun qty";
            dtColumn.Caption = "Jun qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Jun val";
            dtColumn.Caption = "Jun val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Jul qty";
            dtColumn.Caption = "Jul qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Jul val";
            dtColumn.Caption = "Jul val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Aug qty";
            dtColumn.Caption = "Aug qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Aug val";
            dtColumn.Caption = "Aug val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Sep qty";
            dtColumn.Caption = "Sep qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Sep val";
            dtColumn.Caption = "Sep val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Oct qty";
            dtColumn.Caption = "Oct qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Oct val";
            dtColumn.Caption = "Oct val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Nov qty";
            dtColumn.Caption = "Nov qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Nov val";
            dtColumn.Caption = "Nov val";
            custTable.Columns.Add(dtColumn);
            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Dec qty";
            dtColumn.Caption = "Dec qty";
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "Dec val";
            dtColumn.Caption = "Dec val";
            custTable.Columns.Add(dtColumn);

            // Add Address column to the table.
            //  custTable.Columns.Add(dtColumn);
            dtSet = new DataSet("Details");
            dtSet.Tables.Add(custTable);
            #endregion
            //var groupedData = from b in dTable.AsEnumerable()
            //                  group b by b.Field<string>("itb_itm_cd") into g
            //                  let count = g.Count()
            //                  select new
            //                  {
            //                      itmcd = g.Key,
            //                      Count = count,
            //                      //desc = g.AsEnumerable().Select(a => a.Field<string>("MI_SHORTDESC").ToString())  
            //                  };
            var groupedData = from b in dTable.AsEnumerable()
                              group b by b.Field<string>("RIC1_DESC") into g
                              let count = g.Count()
                              select new
                              {
                                  itmcd = g.Key,
                                  Count = count,
                                  //desc = g.AsEnumerable().Select(a => a.Field<string>("MI_SHORTDESC").ToString())  
                              };


            foreach (var itm in groupedData)
            {
                DataTable temp1 = new DataTable();
                myDataRow = custTable.NewRow();

                //var results = dTable.Rows.Cast<DataRow>()
                //    .FirstOrDefault(x => x.Field<string>("itb_itm_cd") == itm.itmcd);
                //var description = dTable.AsEnumerable()
                //  .Where(r => r.Field<string>("itb_itm_cd") == itm.itmcd);
                //.Sum(r => r.Field<Decimal>("QTY"));
                //var sumqty = dTable.AsEnumerable()
                // .Where(r => r.Field<string>("MONTH_") == "01" && r.Field<string>("itb_itm_cd") == itm.itmcd)
                //  .Sum(r => r.Field<Int32>("QTY"));
                //var sumcost = dTable.AsEnumerable()
                // .Where(r => r.Field<string>("MONTH_") == "01" && r.Field<string>("itb_itm_cd") == itm.itmcd)
                //  .Sum(r => r.Field<Decimal>("COST"));

                // var sumqty = dTable.AsEnumerable()
                //.Where(r => r.Field<string>("MONTH_") == "01" && r.Field<string>("MI_SHORTDESC") == itm.itmcd)
                // .Sum(r => r.Field<Int32>("QTY"));
                #region jan
                var jan_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "01" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var jan_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "01" && r.Field<string>("RIC1_DESC") == itm.itmcd)
               .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion

                #region feb
                var feb_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "02" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var feb_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "02" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region mar
                var mar_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "03" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var mar_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "03" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region APR
                var apr_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "04" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var apr_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "04" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region may
                var may_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "05" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var may_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "05" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region jun
                var jun_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "06" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var jun_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "06" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region jul
                var jul_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "07" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var jul_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "07" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region aug
                var aug_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "08" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var aug_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "08" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region sep
                var sep_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "09" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var sep_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "09" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region oct
                var oct_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "10" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var oct_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "10" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region nov
                var nov_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "11" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var nov_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "11" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion
                #region des
                var des_val = dTable.AsEnumerable()
                 .Where(r => r.Field<string>("MONTH_") == "12" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                  .Sum(r => r.Field<Decimal>("COST"));

                var des_qty = dTable.AsEnumerable()
               .Where(r => r.Field<string>("MONTH_") == "12" && r.Field<string>("RIC1_DESC") == itm.itmcd)
                .Sum(r => Convert.ToDecimal(r.Field<Int64>("QTY")));
                #endregion

                #region add datatable
                myDataRow = custTable.NewRow();
                myDataRow["MAIN CATEGORY DESCRIPTION"] = itm.itmcd;
                myDataRow["Jan qty"] = jan_qty.ToString();
                myDataRow["Jan val"] = jan_val.ToString();
                myDataRow["Feb qty"] = feb_qty.ToString();
                myDataRow["Feb val"] = feb_val.ToString();
                myDataRow["Mar qty"] = mar_qty.ToString();
                myDataRow["Mar val"] = mar_val.ToString();
                myDataRow["Apr qty"] = apr_qty.ToString();
                myDataRow["Apr val"] = apr_val.ToString();
                myDataRow["May qty"] = may_qty.ToString();
                myDataRow["May val"] = may_val.ToString();
                myDataRow["Jun qty"] = jun_qty.ToString();
                myDataRow["Jun val"] = jun_val.ToString();
                myDataRow["Jul qty"] = jul_qty.ToString();
                myDataRow["Jul val"] = jul_val.ToString();
                myDataRow["Aug qty"] = aug_qty.ToString();
                myDataRow["Aug val"] = aug_val.ToString();
                myDataRow["Sep qty"] = sep_qty.ToString();
                myDataRow["Sep val"] = sep_val.ToString();
                myDataRow["Oct qty"] = oct_qty.ToString();
                myDataRow["Oct val"] = oct_val.ToString();
                myDataRow["Nov qty"] = nov_qty.ToString();
                myDataRow["Nov val"] = nov_val.ToString();
                myDataRow["Dec qty"] = des_qty.ToString();
                myDataRow["Dec val"] = des_val.ToString();



                custTable.Rows.Add(myDataRow);


                #endregion
            }
            string path = "";
            DataTable _tp = new DataTable("TEMP");
            _tp = GenerateTransposedTable(param);
            path = CHNLSVC.MsgPortal.ExportExcel2007WithHDR(BaseCls.GlbUserComCode, BaseCls.GlbUserID, _tp, custTable, out path, "Y");

            // Excel.Application.Workbooks.Open(@"C:\Test\YourWorkbook.xlsx");

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            string _path = _MasterComp.Mc_anal6;
            // objSales._bocReserveList.ExportToDisk(ExportFormatType.Excel, _path + "Movement Subtype Wise Summary" + BaseCls.GlbUserID + ".xls");

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            string workbookPath = path; // +"Movement Subtype Wise Summary" + BaseCls.GlbUserID + ".xls";
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);





            #region commnet by tharanga
            //string repType = string.Empty;

            //Excel.Application excelApp = new Excel.Application();
            //Excel.Workbook workbook = (Excel.Workbook)excelApp.Workbooks.Add(Missing.Value);
            //Excel.Worksheet worksheet;

            //string _repPath = "";
            //MasterCompany _masterComp = null;
            //_masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            //if (_masterComp != null)
            //{
            //    _repPath = _masterComp.Mc_anal16;

            //}

            //repType = "MovementSubtypeWiseSummary";

            //workbook.SaveAs(_repPath + repType + BaseCls.GlbUserID + ".xls", Excel.XlPlatform.xlWindows, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //workbook.Close(true, Missing.Value, Missing.Value);
            //workbook = excelApp.Workbooks.Open(_repPath + repType + BaseCls.GlbUserID + ".xls", 0, false, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            //// Get first Worksheet
            //worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);

            //// ---Excel code-----

            //int detailsStartingPointX = 1;
            //int detailsStartingPointY = 5;
            //int lastColumnPoint = 0;
            //int lastRowPoint = 0;
            //long totQty = 0;
            //decimal totCost = 0;

            //((Excel.Range)worksheet.Cells[1, "B"]).Value2 = "Period From " + Convert.ToDateTime(BaseCls.GlbReportFromDate) + "To " + Convert.ToDateTime(BaseCls.GlbReportToDate).Date;

            //((Excel.Range)worksheet.Cells[1, "A"]).Value2 = "Movement Subtype Wise Summary"; 

            //((Excel.Range)worksheet.Cells[1, "C"]).Value2 = "User :" + BaseCls.GlbUserID + " On " + DateTime.Now;

            //// Find and create headings
            //var dateMonth = (from row in tblReportData.AsEnumerable()
            //                 group row by new { yr = row.Field<Int16>("YEAR_"), mn = int.Parse(row.Field<string>("MONTH_")) } into grpRepData
            //                 orderby grpRepData.Key.yr, grpRepData.Key.mn
            //                 select new {
            //                     year = grpRepData.Key.yr,
            //                     month = grpRepData.Key.mn,
            //                     heading = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(grpRepData.Key.mn) + "/" + grpRepData.Key.yr,
            //                     headingNum = grpRepData.Key.mn + "/" + grpRepData.Key.yr
            //                 }).ToList();

            //var descriptions = (from rowd in tblReportData.AsEnumerable()
            //                    group rowd by new { desc = rowd.Field<string>("MI_SHORTDESC")} into grpDescData
            //                    orderby grpDescData.Key.desc
            //                    select new
            //                    {
            //                       description = grpDescData.Key.desc
            //                    }).ToList();

            //var repData = (from rowrd in tblReportData.AsEnumerable()
            //               select new
            //               {
            //                   des = rowrd.Field<string>("MI_SHORTDESC"),
            //                   yrmnth = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(int.Parse(rowrd.Field<string>("MONTH_"))) + "/" + rowrd.Field<Int16>("YEAR_"),
            //                   qty = rowrd.Field<Int64>("QTY"),
            //                   cost = rowrd.Field<System.Decimal>("COST")
            //               }).ToList();

            //for (int i = 0, j=0; i < dateMonth.Count; i++,j=j+2 )
            //{
            //    lastColumnPoint = j;
            //    ((Excel.Range)worksheet.Cells[detailsStartingPointY, j + (detailsStartingPointX+1)]).Value2 = dateMonth[i].heading.ToUpper();
            //    ((Excel.Range)worksheet.Cells[detailsStartingPointY+1, j + (detailsStartingPointX + 1)]).Value2 = "Qty".ToUpper();
            //    ((Excel.Range)worksheet.Cells[detailsStartingPointY + 1, j + 1 + (detailsStartingPointX + 1)]).Value2 = "Value".ToUpper();
            //    //((Excel.Range)worksheet.Cells[detailsStartingPointY, j + (detailsStartingPointX + 1)]).Merge();
            //    //worksheet.Range[(Excel.Range)worksheet.Cells[detailsStartingPointY, j + (detailsStartingPointX + 1)], (Excel.Range)worksheet.Cells[detailsStartingPointY, j + 1 + (detailsStartingPointX + 1)]].Merge();


            //}


            //((Excel.Range)worksheet.Cells[detailsStartingPointY + 1, detailsStartingPointX]).Value2 = "Main Category Description".ToUpper();

            //for (int x = 0; x < descriptions.Count; x++)
            //{
            //    ((Excel.Range)worksheet.Cells[detailsStartingPointY + 2 + x, detailsStartingPointX]).Value2 = descriptions[x].description.ToUpper();
            //    lastRowPoint = detailsStartingPointY + 2 + x;

            //    //Excel.Range cellRange2 = (Excel.Range)worksheet.get_Range((Excel.Range)worksheet.Cells[detailsStartingPointY, detailsStartingPointX], (Excel.Range)worksheet.Cells[detailsStartingPointY, lastColumnPoint]);
            //    //cellRange2.Font.Bold = false;
            //    //cellRange2.RowHeight = 25.5;
            //    //cellRange2.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            //    //cellRange2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //    //cellRange2.Borders.Weight = 1d;


            //}

            //((Excel.Range)worksheet.Cells[lastRowPoint + 1, detailsStartingPointX]).Value2 = "Total : ".ToUpper();

            //for (int i = 0, j = 0; i < dateMonth.Count; i++, j = j + 2)
            //{
            //    for (int x = 0; x < descriptions.Count; x++)
            //    {
            //        for (int z = 0; z < repData.Count; z++)
            //        {
            //            if (descriptions[x].description == repData[z].des && dateMonth[i].heading == repData[z].yrmnth)
            //            {
            //                ((Excel.Range)worksheet.Cells[detailsStartingPointY + 1 + (z + 1), j + (detailsStartingPointX + 1)]).Value2 = repData[z].qty;
            //                ((Excel.Range)worksheet.Cells[detailsStartingPointY + 1 + (z + 1), j + 1 + (detailsStartingPointX + 1)]).Value2 = repData[z].cost;
            //                totQty = totQty + repData[z].qty;
            //                totCost = totCost + repData[z].cost;
            //            }
            //        }
            //    }

            //    ((Excel.Range)worksheet.Cells[lastRowPoint + 1, j + (detailsStartingPointX + 1)]).Value2 = totQty.ToString("0");
            //    ((Excel.Range)worksheet.Cells[lastRowPoint + 1, j + j + 1 + (detailsStartingPointX + 1)]).Value2 = totCost.ToString("0.00");

            //}

            ////Excel.Range cellRange3 = (Excel.Range)worksheet.get_Range((Excel.Range)worksheet.Cells[detailsStartingPointY, detailsStartingPointX], (Excel.Range)worksheet.Cells[detailsStartingPointY, lastColumnPoint]);
            ////cellRange3.Font.Bold = true;
            ////cellRange3.RowHeight = 25.5;
            ////cellRange3.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            ////cellRange3.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ////cellRange3.Borders.Weight = 1d;

            ////Excel.Range cellRange4 = (Excel.Range)worksheet.get_Range((Excel.Range)worksheet.Cells[lastRowPoint + 1, detailsStartingPointX], (Excel.Range)worksheet.Cells[lastRowPoint + 1, lastColumnPoint]);
            ////cellRange4.Font.Bold = true;
            ////cellRange4.RowHeight = 25.5;
            ////cellRange4.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlHairline, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            ////cellRange4.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ////cellRange4.Borders.Weight = 1d;


            ////worksheet.Columns.AutoFit();
            //Excel.Range autoFitCols1 = (Excel.Range)worksheet.get_Range((Excel.Range)worksheet.Cells[detailsStartingPointY, detailsStartingPointX], (Excel.Range)worksheet.Cells[lastRowPoint + 1, lastColumnPoint]);
            //autoFitCols1.Columns.AutoFit();
            //worksheet.Rows.AutoFit();

            //// --/Excel code-----

            //workbook.Save();

            //workbook.Close(0, 0, 0);


            //excelApp.Quit();
            //Marshal.ReleaseComObject(worksheet);
            //Marshal.ReleaseComObject(workbook);
            //Marshal.ReleaseComObject(excelApp);

            //Excel.Application excelApp1 = new Excel.Application();
            //excelApp1.Visible = true;

            //string workbookPath = _repPath + repType + BaseCls.GlbUserID + ".xls";
            //Excel.Workbook excelWorkbook = excelApp1.Workbooks.Open(workbookPath,
            //        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
            //        true, false, 0, true, false, false);
            #endregion

        }
        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable("DT");

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }


        private void label50_Click(object sender, EventArgs e)
        {

        }

        //Tharindu 2017-11-13
        private void txt_root_KeyDown(object sender, KeyEventArgs e)
        {

        }


        //  Tharindu 2017-11-13

        private void txt_root1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnroot_Click(null, null);
            }
        }
        private void btnroot_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementTypes);
            DataTable _result = CHNLSVC.CommonSearch.Get_RootTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_root1;
            _CommonSearch.txtSearchbyword.Text = txtDocType.Text;
            _CommonSearch.ShowDialog();
            txtDocType.Focus();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbMonth_SelectedIndexChanged(null, null);
        }

        private void opt56_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt56.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "7002")) 
                    //Edit by Chamal 22/03/2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16104))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16104 )");
                        opt55.Checked = false;
                        return;
                    }
                    BindAllCompanies();
                    setFormControls(56);
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

        private void opt57_CheckedChanged(object sender, EventArgs e)
        {
            if (opt57.Checked == true)
            {

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7054))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7054 )");
                    opt57.Checked = false;
                    return;
                }
                BindAllCompanies();
                setFormControls(57);
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
        private void loadageing()
        {

            DataTable dt = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);
            List<string> age = new List<string>();
           // cmbRemTp.Items.Clear();
            DataTable table = new DataTable();
            table.Columns.Add("slot", typeof(int));
            table.Columns.Add("age", typeof(string));


            foreach (DataRow item in dt.Rows)
            {
                table.Rows.Add(1, item["rags_slot_l1"].ToString());
                table.Rows.Add(2, item["rags_slot_l2"].ToString());
                table.Rows.Add(3, item["rags_slot_l3"].ToString());
                table.Rows.Add(4, item["rags_slot_l4"].ToString());
                table.Rows.Add(5, item["rags_slot_l5"].ToString());

                //age.Add(item["rags_slot_l1"].ToString());
                //age.Add(item["rags_slot_l2"].ToString());
                //age.Add(item["rags_slot_l3"].ToString());
                //age.Add(item["rags_slot_l4"].ToString());
                //age.Add(item["rags_slot_l5"].ToString());

            }

            if (table.Rows.Count > 0)
            {
                cmbRemTp.DataSource = table;
                cmbRemTp.DisplayMember = "age";
                cmbRemTp.ValueMember = "slot";
            }
            else
            {
                cmbRemTp.DataSource = null;
            }

            //foreach (string name in age)
            //{
            //    cmbRemTp.Items.Add(name);
            //}

        }

        private void btn_agepnl_Click(object sender, EventArgs e)
        {
            pnl_ageing.Visible = false;
        }

        private void cmbRemTp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void update_PC_List_RPTDBnew()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {

                if (Item.Checked == true)
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


                    if (_isPCFound == false)
                    {
                        BaseCls.GlbReportProfit = txtPC.Text;
                        Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
                    }
                }
            }
            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        private void opt58_CheckedChanged(object sender, EventArgs e)
        {
            if (opt58.Checked == true)
            {

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7055))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7055 )");
                    opt58.Checked = false;
                    return;
                }
                BindAllCompanies();
                setFormControls(58);
            }
        }

        private void opt59_CheckedChanged(object sender, EventArgs e)
        {
            if (opt59.Checked == true)
            {

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7056))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7056 )");
                    opt59.Checked = false;
                    return;
                }
                BindAllCompanies();
                setFormControls(59);
            }
        }



        public void add_loc_list(object sender, EventArgs e)
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
                        DataTable dt = CHNLSVC.MsgPortal.GetLoc_from_Hierachy_withOpteam(com, chanel, subChanel, area, region, zone, pc, _adminTeam);
                        foreach (DataRow drow in dt.Rows)
                        {
                            if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                        }
                    }
                }
            }
        }

        private void opt60_CheckedChanged(object sender, EventArgs e)
        {
            if (opt60.Checked == true)
            {

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 7057))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :7057 )");
                    opt60.Checked = false;
                    return;
                }

                setFormControls(60);
            }
        }

        private void opt61_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt61.Checked == true)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11504))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11504)");
                    //    opt52.Checked = false;
                    //    return;
                    //}
                    setFormControls(61);
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

        private void opt62_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt62.Checked == true)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11504))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11504)");
                    //    opt52.Checked = false;
                    //    return;
                    //}
                    setFormControls(62);
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

        //private void opt63_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (opt63.Checked == true)
        //        {
        //            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16119))
        //            {
        //                MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16120 )");
        //                opt63.Checked = false;
        //                return;
        //            }
        //            setFormControls(63);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        CHNLSVC.CloseChannel();
        //        MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseAllChannels();
        //    }
        //}

        //private void opt64_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (opt64.Checked == true)
        //        {
        //            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16119))
        //            {
        //                MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16119 )");
        //                opt64.Checked = false;
        //                return;
        //            }
        //            setFormControls(64);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        CHNLSVC.CloseChannel();
        //        MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseAllChannels();
        //    }
        //}

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

        private void btnModel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtModel.Text))
            {
                for (int i = 0; i < lstModel.Items.Count; i++)
                {
                    if (lstModel.Items[i].Text == txtModel.Text)
                        return;
                }
                lstModel.Items.Add((txtModel.Text).ToString());
                txtModel.Text = "";
            }
        }
     
        private void opt65_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt65.Checked == true)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11504))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11504)");
                    //    opt52.Checked = false;
                    //    return;
                    //}
                    setFormControls(65);
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

        private void opt64_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt64.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16119))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16119 )");
                        opt64.Checked = false;
                        return;
                    }
                    setFormControls(64);
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

        private void opt63_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt63.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16119))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16120 )");
                        opt63.Checked = false;
                        return;
                    }
                    setFormControls(63);
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

        private void opt66_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt66.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16127))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16127 )");
                        opt66.Checked = false;
                        return;
                    }
                    setFormControls(66);
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

        private void opt67_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt67.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16131))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16131 )");
                        opt67.Checked = false;
                        return;
                    }
                    setFormControls(67);
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

        private void cmbMonth_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear_2.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            int month = cmbMonth_2.SelectedIndex + 1;
            int year = Convert.ToInt32(cmbYear_2.Text);
            if (month > 0)
            {
                int numberOfDays = DateTime.DaysInMonth(year, month);
                DateTime lastDay = new DateTime(year, month, numberOfDays);

                txtToDate_2.Text = lastDay.ToString("dd/MMM/yyyy");

                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear_2.Text), month, 1);
                txtFromDate_2.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            }
        }

        private void cmbYear_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth_2_SelectedIndexChanged(null, null);
        }
    }
}


