using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using FF.BusinessObjects;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace FF.WindowsERPClient.Reports.Audit
{
    class clsAuditRep
    {
        public FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Bal_Coll _PhyStkColl = new FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Bal_Coll();
        public FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification _PhyStkVerify = new FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification();
        public FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification_AST _PhyStkVerifycmstk = new FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification_AST();
        public FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification_ByRef _PhyStkbyRef = new FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification_ByRef();
        public FF.WindowsERPClient.Reports.Audit.Audit_Manager_Explanation _PhyMgrExpl = new FF.WindowsERPClient.Reports.Audit.Audit_Manager_Explanation();
        public FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification_DefItems _PhyStkDef = new FF.WindowsERPClient.Reports.Audit.Audit_Phy_Stock_Verification_DefItems();
        public FF.WindowsERPClient.Reports.Audit.Audit_Mismatch_Serials _SerMismatch = new FF.WindowsERPClient.Reports.Audit.Audit_Mismatch_Serials();
        public FF.WindowsERPClient.Reports.Audit.Audit_Used_as_FixedAsset _FixAsst = new FF.WindowsERPClient.Reports.Audit.Audit_Used_as_FixedAsset();
        public FF.WindowsERPClient.Reports.Audit.Audit_Ageing_Items _AgeItm = new FF.WindowsERPClient.Reports.Audit.Audit_Ageing_Items();
        public FF.WindowsERPClient.Reports.Audit.Audit_FIFO_not_Followed _FIFO = new FF.WindowsERPClient.Reports.Audit.Audit_FIFO_not_Followed();
        public FF.WindowsERPClient.Reports.Audit.Audit_Unconfirmed_AOD _GIT = new FF.WindowsERPClient.Reports.Audit.Audit_Unconfirmed_AOD();
        public FF.WindowsERPClient.Reports.Audit.Audit_Fixed_Asset_Bal _FixBal = new FF.WindowsERPClient.Reports.Audit.Audit_Fixed_Asset_Bal();
        public FF.WindowsERPClient.Reports.Audit.Audit_Stock_Varience_Note _StkVarNote = new FF.WindowsERPClient.Reports.Audit.Audit_Stock_Varience_Note();

        public FF.WindowsERPClient.Reports.Audit.RevertedItems _revertedItm = new FF.WindowsERPClient.Reports.Audit.RevertedItems();
        public FF.WindowsERPClient.Reports.Audit.PendingDelivery _pendingDel = new FF.WindowsERPClient.Reports.Audit.PendingDelivery();
        public FF.WindowsERPClient.Reports.Audit.MultipleAccounts _multiAcc = new FF.WindowsERPClient.Reports.Audit.MultipleAccounts();
        public FF.WindowsERPClient.Reports.Audit.Arrears _Age_Debt_ArrearsReport = new FF.WindowsERPClient.Reports.Audit.Arrears();
        public FF.WindowsERPClient.Reports.Audit.AuditDeliveredSalesReport _delSalesAud = new FF.WindowsERPClient.Reports.Audit.AuditDeliveredSalesReport();
        public FF.WindowsERPClient.Reports.Audit.User_Prev_Menu _UPermMnu = new FF.WindowsERPClient.Reports.Audit.User_Prev_Menu();
        public FF.WindowsERPClient.Reports.Audit.User_Role_Prev _URoleMnu = new FF.WindowsERPClient.Reports.Audit.User_Role_Prev();
        public FF.WindowsERPClient.Reports.Audit.User_Spec_Perm _USpcPerm = new FF.WindowsERPClient.Reports.Audit.User_Spec_Perm();
        public FF.WindowsERPClient.Reports.Audit.User_list _UList = new FF.WindowsERPClient.Reports.Audit.User_list();
        public FF.WindowsERPClient.Reports.Audit.Audit_Stock_Verification_Exec_Sum _executiveSummary = new FF.WindowsERPClient.Reports.Audit.Audit_Stock_Verification_Exec_Sum();
        public FF.WindowsERPClient.Reports.Audit.Audit_Stock_Verification_stk_sign _stksign = new FF.WindowsERPClient.Reports.Audit.Audit_Stock_Verification_stk_sign();

        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        Base bsObj;
        public clsAuditRep()
        {
            bsObj = new Base();

        }

        public void SpecialPrivilegesReport()
        {// Sanjeewa 19/06/2014
            DataTable param = new DataTable();
            DataRow dr;

            DataTable UserPrev = new DataTable();
            DataTable UserAsgnRole = new DataTable();            
            DataTable MnuAsgnRole = new DataTable();
            DataTable SpecialPerm = new DataTable();

            UserPrev = bsObj.CHNLSVC.Security.Get_SystemUsers(BaseCls.GlbReportUser, BaseCls.GlbReportDepartment);            
            MnuAsgnRole = bsObj.CHNLSVC.Security.Get_SystemMenuAssgnRole();
            SpecialPerm = bsObj.CHNLSVC.Security.Get_SystemSpecialPerm();

            if (UserPrev.Rows.Count > 0)
            {
                foreach (DataRow drow in UserPrev.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemRoleAssgnUser(drow["se_usr_id"].ToString());
                    UserAsgnRole.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("department", typeof(string));
            param.Columns.Add("roleid", typeof(string));
            param.Columns.Add("userId", typeof(string));
            param.Columns.Add("compay", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["department"] = BaseCls.GlbReportDepartment == "" ? "ALL" : BaseCls.GlbReportDepartment;
            dr["roleid"] = BaseCls.GlbReportRole == "" ? "ALL" : BaseCls.GlbReportRole;
            dr["userId"] = BaseCls.GlbReportUser == "" ? "ALL" : BaseCls.GlbReportUser;
            dr["compay"] = BaseCls.GlbReportCompCode == "" ? "ALL" : BaseCls.GlbReportCompCode;

            param.Rows.Add(dr);

            _USpcPerm.Database.Tables["param"].SetDataSource(param);

            _USpcPerm.Database.Tables["USER_PREV"].SetDataSource(UserPrev);
            _USpcPerm.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
            _USpcPerm.Database.Tables["SPEC_PERM"].SetDataSource(SpecialPerm);
            _USpcPerm.Database.Tables["MENU_ASGN_ROLE"].SetDataSource(MnuAsgnRole);

        }

        public void RoleMenuPrivilegesReport()
        {// Sanjeewa 19/06/2014
            DataTable param = new DataTable();
            DataRow dr;

            DataTable UserPrev = new DataTable();
            DataTable UserAsgnRole = new DataTable();
            DataTable RoleDef = new DataTable();
            DataTable MenuDef = new DataTable();
            DataTable MnuAsgnRole = new DataTable();

            UserPrev = bsObj.CHNLSVC.Security.Get_SystemUsers(BaseCls.GlbReportUser, BaseCls.GlbReportDepartment);            
            RoleDef = bsObj.CHNLSVC.Security.Get_SystemRole();
            MenuDef = bsObj.CHNLSVC.Security.Get_SystemMenu();
            MnuAsgnRole = bsObj.CHNLSVC.Security.Get_SystemMenuAssgnRole();

            if (UserPrev.Rows.Count > 0)
            {
                foreach (DataRow drow in UserPrev.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemRoleAssgnUser(drow["se_usr_id"].ToString());
                    UserAsgnRole.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("department", typeof(string));
            param.Columns.Add("roleid", typeof(string));
            param.Columns.Add("userId", typeof(string));
            param.Columns.Add("compay", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["department"] = BaseCls.GlbReportDepartment == "" ? "ALL" : BaseCls.GlbReportDepartment;
            dr["roleid"] = BaseCls.GlbReportRole == "" ? "ALL" : BaseCls.GlbReportRole;
            dr["userId"] = BaseCls.GlbReportUser == "" ? "ALL" : BaseCls.GlbReportUser;
            dr["compay"] = BaseCls.GlbReportCompCode == "" ? "ALL" : BaseCls.GlbReportCompCode;

            param.Rows.Add(dr);

            _URoleMnu.Database.Tables["param"].SetDataSource(param);

            _URoleMnu.Database.Tables["USER_PREV"].SetDataSource(UserPrev);
            _URoleMnu.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
            _URoleMnu.Database.Tables["ROLE_DEF"].SetDataSource(RoleDef);

            foreach (object repOp in _URoleMnu.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    ReportDocument subRepDoc = _URoleMnu.Subreports[_cs.SubreportName];
                    if (_cs.SubreportName == "MenuPermission")
                    {
                        subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MENU_ASGN_ROLE"].SetDataSource(MnuAsgnRole);
                        subRepDoc.Database.Tables["MENU_DEF"].SetDataSource(MenuDef);                        
                    }

                }
            }

        }

        public void UserMenuPrivilegesReport()
        {// Sanjeewa 18/06/2014
            DataTable param = new DataTable();
            DataRow dr;

            DataTable UserPrev = new DataTable();
            DataTable UserAsgnRole = new DataTable();
            DataTable UserLoc = new DataTable();
            DataTable UserPc = new DataTable();
            DataTable SpecialPerm = new DataTable();
            DataTable RoleDef = new DataTable();
            DataTable MenuDef = new DataTable();
            DataTable MnuAsgnRole = new DataTable();

            UserPrev = bsObj.CHNLSVC.Security.Get_SystemUsers(BaseCls.GlbReportUser, BaseCls.GlbReportDepartment );
            SpecialPerm = bsObj.CHNLSVC.Security.Get_SystemSpecialPerm();
            RoleDef = bsObj.CHNLSVC.Security.Get_SystemRole();
            MenuDef = bsObj.CHNLSVC.Security.Get_SystemMenu();
            MnuAsgnRole = bsObj.CHNLSVC.Security.Get_SystemMenuAssgnRole();

            if (UserPrev.Rows.Count > 0)
            {
                foreach (DataRow drow in UserPrev.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemRoleAssgnUser(drow["se_usr_id"].ToString());
                    UserAsgnRole.Merge(TMP_DataTable);

                    TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemUserLoc(drow["se_usr_id"].ToString(), BaseCls.GlbReportCompCode, "LOC");
                    UserLoc.Merge(TMP_DataTable);

                    TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemUserLoc(drow["se_usr_id"].ToString(), BaseCls.GlbReportCompCode, "PC");
                    UserPc.Merge(TMP_DataTable);

                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("department", typeof(string));
            param.Columns.Add("roleid", typeof(string));
            param.Columns.Add("userId", typeof(string));
            param.Columns.Add("compay", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["department"] = BaseCls.GlbReportDepartment == "" ? "ALL" : BaseCls.GlbReportDepartment;
            dr["roleid"] = BaseCls.GlbReportRole == "" ? "ALL" : BaseCls.GlbReportRole;
            dr["userId"] = BaseCls.GlbReportUser == "" ? "ALL" : BaseCls.GlbReportUser;
            dr["compay"] = BaseCls.GlbReportCompCode == "" ? "ALL" : BaseCls.GlbReportCompCode;

            param.Rows.Add(dr);

            _UPermMnu.Database.Tables["param"].SetDataSource(param);

            _UPermMnu.Database.Tables["USER_PREV"].SetDataSource(UserPrev);
            _UPermMnu.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);

            foreach (object repOp in _UPermMnu.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    ReportDocument subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
                    if (_cs.SubreportName == "MenuPermission")
                    {
                        subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MENU_ASGN_ROLE"].SetDataSource(MnuAsgnRole);
                        subRepDoc.Database.Tables["MENU_DEF"].SetDataSource(MenuDef);
                        subRepDoc.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
                    }
                    if (_cs.SubreportName == "UserLocation")
                    {
                        subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["USER_LOC"].SetDataSource(UserLoc);
                    }
                    if (_cs.SubreportName == "UserProfitcenter")
                    {
                        subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["USER_PC"].SetDataSource(UserPc);
                    }
                    if (_cs.SubreportName == "SpecialPermission")
                    {
                        subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SPEC_PERM"].SetDataSource(SpecialPerm);
                        subRepDoc.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
                    }

                }
            }        
            
        }

        public void UserListReport()
        {// Wimal 02/03/2017
            DataTable param = new DataTable();
            DataRow dr;

            //DataTable UserPrev = new DataTable();
            //DataTable UserAsgnRole = new DataTable();
            //DataTable UserLoc = new DataTable();
            //DataTable UserPc = new DataTable();
            //DataTable SpecialPerm = new DataTable();
            //DataTable RoleDef = new DataTable();
            //DataTable MenuDef = new DataTable();
            //DataTable MnuAsgnRole = new DataTable();
            DataTable UserList = new DataTable();

            //UserPrev = bsObj.CHNLSVC.Security.Get_SystemUsers(BaseCls.GlbReportUser, BaseCls.GlbReportDepartment);
            //SpecialPerm = bsObj.CHNLSVC.Security.Get_SystemSpecialPerm();
            //RoleDef = bsObj.CHNLSVC.Security.Get_SystemRole();
            //MenuDef = bsObj.CHNLSVC.Security.Get_SystemMenu();
            //MnuAsgnRole = bsObj.CHNLSVC.Security.Get_SystemMenuAssgnRole();
            UserList = bsObj.CHNLSVC.MsgPortal.UserMonitor("", BaseCls.GlbReportDepartment, BaseCls.GlbReportUser);

            //if (UserPrev.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in UserPrev.Rows)
            //    {
            //        DataTable TMP_DataTable = new DataTable();
            //        TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemRoleAssgnUser(drow["se_usr_id"].ToString());
            //        UserAsgnRole.Merge(TMP_DataTable);

            //        TMP_DataTable = new DataTable();
            //        TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemUserLoc(drow["se_usr_id"].ToString(), BaseCls.GlbReportCompCode, "LOC");
            //        UserLoc.Merge(TMP_DataTable);

            //        TMP_DataTable = new DataTable();
            //        TMP_DataTable = bsObj.CHNLSVC.Security.Get_SystemUserLoc(drow["se_usr_id"].ToString(), BaseCls.GlbReportCompCode, "PC");
            //        UserPc.Merge(TMP_DataTable);

            //    }
            //}

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("department", typeof(string));
            param.Columns.Add("roleid", typeof(string));
            param.Columns.Add("userId", typeof(string));
            param.Columns.Add("compay", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["department"] = BaseCls.GlbReportDepartment == "" ? "ALL" : BaseCls.GlbReportDepartment;
            dr["roleid"] = BaseCls.GlbReportRole == "" ? "ALL" : BaseCls.GlbReportRole;
            dr["userId"] = BaseCls.GlbReportUser == "" ? "ALL" : BaseCls.GlbReportUser;
            dr["compay"] = BaseCls.GlbReportCompCode == "" ? "ALL" : BaseCls.GlbReportCompCode;

            param.Rows.Add(dr);

            _UList.Database.Tables["param"].SetDataSource(param);

            //_UPermMnu.Database.Tables["USER_PREV"].SetDataSource(UserPrev);
            //_UPermMnu.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
            _UList.Database.Tables["AUD_USERLIST"].SetDataSource(UserList);
            

            //foreach (object repOp in _UPermMnu.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        ReportDocument subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
            //        if (_cs.SubreportName == "MenuPermission")
            //        {
            //            subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["MENU_ASGN_ROLE"].SetDataSource(MnuAsgnRole);
            //            subRepDoc.Database.Tables["MENU_DEF"].SetDataSource(MenuDef);
            //            subRepDoc.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
            //        }
            //        if (_cs.SubreportName == "UserLocation")
            //        {
            //            subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["USER_LOC"].SetDataSource(UserLoc);
            //        }
            //        if (_cs.SubreportName == "UserProfitcenter")
            //        {
            //            subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["USER_PC"].SetDataSource(UserPc);
            //        }
            //        if (_cs.SubreportName == "SpecialPermission")
            //        {
            //            subRepDoc = _UPermMnu.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["SPEC_PERM"].SetDataSource(SpecialPerm);
            //            subRepDoc.Database.Tables["ROLE_ASGN_USER"].SetDataSource(UserAsgnRole);
            //        }

            //    }
            //}

        }

        public void DeliveredSalesReportAudit()
        {// Nadeeka 08-05-14
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesDetailsAuditReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, BaseCls.GlbReportToPage);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_CUST", typeof(string));
            param.Columns.Add("PARA_EXEC", typeof(string));
            param.Columns.Add("PARA_DOCTYPE", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_STKTYPE", typeof(string));
            param.Columns.Add("PARA_INVNO", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));

            param.Columns.Add("GRP_PCENTER", typeof(int));
            param.Columns.Add("GRP_CUST", typeof(int));
            param.Columns.Add("GRP_EXEC", typeof(int));
            param.Columns.Add("GRP_DOCTYPE", typeof(int));
            param.Columns.Add("GRP_ITCODE", typeof(int));
            param.Columns.Add("GRP_BRAND", typeof(int));
            param.Columns.Add("GRP_MODEL", typeof(int));
            param.Columns.Add("GRP_CAT1", typeof(int));
            param.Columns.Add("GRP_CAT2", typeof(int));
            param.Columns.Add("GRP_CAT3", typeof(int));
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_DLOC", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;

            dr["GRP_PCENTER"] = BaseCls.GlbReportGroupProfit;
            dr["GRP_DOCTYPE"] = BaseCls.GlbReportGroupDocType;
            dr["GRP_CUST"] = BaseCls.GlbReportGroupCustomerCode;
            dr["GRP_EXEC"] = BaseCls.GlbReportGroupExecCode;
            dr["GRP_ITCODE"] = BaseCls.GlbReportGroupItemCode;
            dr["GRP_BRAND"] = BaseCls.GlbReportGroupBrand;
            dr["GRP_MODEL"] = BaseCls.GlbReportGroupModel;
            dr["GRP_CAT1"] = BaseCls.GlbReportGroupItemCat1;
            dr["GRP_CAT2"] = BaseCls.GlbReportGroupItemCat2;
            dr["GRP_CAT3"] = BaseCls.GlbReportGroupItemCat3;
            dr["GRP_STKTYPE"] = BaseCls.GlbReportGroupItemStatus;
            dr["GRP_INVNO"] = BaseCls.GlbReportGroupInvoiceNo;
            dr["GRP_LAST_GROUP"] = BaseCls.GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = BaseCls.GlbReportGroupLastGroupCat;
            dr["GRP_DLOC"] = BaseCls.GlbReportGroupDOLoc;

            param.Rows.Add(dr);


          
                _delSalesAud.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
                _delSalesAud.Database.Tables["REP_PARA"].SetDataSource(param);
           

        }
        public void AgeAnalysisOfDebtorsArrearsPrint()
        {
            int i = 0;
            DataTable mst_PC = new DataTable();
            DataTable glob_debt_arr = new DataTable();
            DataTable param = new DataTable();
            DataTable Glb_Debt_Arr_Sum = new DataTable();
            DataTable Glb_Debt_Arr_Chnl = new DataTable();
            DataRow dr;
            Boolean isItem = false;
            mst_PC.Clear();
            param.Clear();
            glob_debt_arr.Clear();
            Glb_Debt_Arr_Sum.Clear();
            Glb_Debt_Arr_Chnl.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("vGroupBy", typeof(string));

            Glb_Debt_Arr_Sum.Columns.Add("ARR_COM", typeof(string));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_PC", typeof(string));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_RVT_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_ACT_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_TOT_ARR_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_TOT_ARR_VAL", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_CLOSE_BAL", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_POVR_ACC", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_POVR_VAL", typeof(Decimal));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_USER", typeof(string));
            Glb_Debt_Arr_Sum.Columns.Add("ARR_LOC_DESC", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["vGroupBy"] = BaseCls.GlbReportGroupBy;
            param.Rows.Add(dr);

            mst_PC = bsObj.CHNLSVC.Sales.GetTempPCLoc(BaseCls.GlbUserID);
            foreach (DataRow row in mst_PC.Rows)
            {
                dr = mst_PC.NewRow();
                dr["MPC_CD"] = row["MPC_CD"].ToString();
                dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                dr["MPC_COM"] = row["MPC_COM"].ToString();
                i = i + 1;
            }
            if (BaseCls.GlbReportItemCode == null && BaseCls.GlbReportItemCat1 == "" && BaseCls.GlbReportItemCat2 == null && BaseCls.GlbReportItemCat3 == null && BaseCls.GlbReportModel == null && BaseCls.GlbReportBrand == null)
            {
                isItem = false;
            }
            else
            {
                isItem = true;
            }
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            DataTable GLOB_DataTable1 = new DataTable();
            DataTable GLOB_DataTable2 = new DataTable();

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    DataTable _jobs = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Win(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportScheme, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, isItem, BaseCls.GlbReportDoc, out _jobs);
                    GLOB_DataTable.Merge(TMP_DataTable);

                    TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Financial.Process_AgeOfDebtors_Arrears_Sum(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID,  BaseCls.GlbReportScheme, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, isItem);
                    GLOB_DataTable1.Merge(TMP_DataTable);

                }
            }

            _Age_Debt_ArrearsReport.Database.Tables["SP_PROC_AGE_ANLS_OF_DEBT_ARR"].SetDataSource(GLOB_DataTable);
            _Age_Debt_ArrearsReport.Database.Tables["param"].SetDataSource(param);
            _Age_Debt_ArrearsReport.Database.Tables["Debt_Arr_Sum"].SetDataSource(GLOB_DataTable1);

        }

        public void MultipleAccountsRep()
        {//
            DataTable param = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable glb_hp_multiple_acc = new DataTable();

            DataRow dr;

            // DataTable glb_hp_multiple_acc = bsObj.CHNLSVC.Sales.ProcessHPMultipleAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportComp, BaseCls.GlbReportScheme, BaseCls.GlbReportCusId, BaseCls.GlbReportCusAccBal, drow["tpl_pc"].ToString());
            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            //        DataTable tmp_hp_multiple_acc = new DataTable();

            //        tmp_hp_multiple_acc = bsObj.CHNLSVC.Sales.ProcessHPMultipleAccounts(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportComp, BaseCls.GlbReportScheme, BaseCls.GlbReportCusId, BaseCls.GlbReportCusAccBal, drow["tpl_pc"].ToString());

            //        glb_hp_multiple_acc.Merge(tmp_hp_multiple_acc);

            //    }
            //}

            glb_hp_multiple_acc = BaseCls.GlbReportDataTable;

            DataTable mst_com = default(DataTable);


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);
            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbReportComp, string.Empty);


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("scheme", typeof(string));
            param.Columns.Add("cusid", typeof(string));
            param.Columns.Add("cusBal", typeof(Double));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["scheme"] = BaseCls.GlbReportScheme == "" ? "ALL" : BaseCls.GlbReportScheme;
            dr["cusid"] = BaseCls.GlbReportCusId == "" ? "ALL" : BaseCls.GlbReportCusId;
            dr["cusBal"] = BaseCls.GlbReportCusAccBal;
            param.Rows.Add(dr);

            glb_hp_multiple_acc = glb_hp_multiple_acc.DefaultView.ToTable(true);

            _multiAcc.Database.Tables["MULIPLE_ACC"].SetDataSource(glb_hp_multiple_acc);
            _multiAcc.Database.Tables["mst_com"].SetDataSource(mst_com);
            _multiAcc.Database.Tables["param"].SetDataSource(param);
            _multiAcc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);


            //foreach (object repOp in _multiAcc.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "sum")
            //        {
            //            ReportDocument subRepDoc = _multiAcc.Subreports[_cs.SubreportName];

            //            subRepDoc.Database.Tables["glb_hp_multiple_acc"].SetDataSource(glb_hp_multiple_acc);
            //            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            //        }

            //    }
            //}

        }

        public void PendingDeliveryReport()
        {// kapila 5/12/2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable _dtTmp = new DataTable();

            if (BaseCls.GlbReportParaLine1 == 1) // get from processed data - kapila 13/5/2017
            {
                GLOB_DataTable=new DataTable();
                _dtTmp = bsObj.CHNLSVC.MsgPortal.GetForwardSalesDetailAudit_Pro(BaseCls.GlbReportProfit);
                var query = _dtTmp.AsEnumerable().Where(x => x.Field<string>("company") == BaseCls.GlbUserComCode.ToString()).ToList();
                if (query.Count>0)
                GLOB_DataTable = query.CopyToDataTable();
                else
                    GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetForwardSalesDetailAudit_Pro("XX");
            }
            else
            {
                tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                if (tmp_user_pc.Rows.Count > 0)
                {
                    foreach (DataRow drow in tmp_user_pc.Rows)
                    {
                        DataTable TMP_DataTable = new DataTable();
                        TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetForwardSalesDetailAudit(BaseCls.GlbReportAsAtDate, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportExeType, BaseCls.GlbReportDiscRate, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), "N");
                        GLOB_DataTable.Merge(TMP_DataTable);
                    }
                }
            }
            //DataTable FORWARD_SALES_REP = bsObj.CHNLSVC.Sales.GetForwardSalesDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportExeType, BaseCls.GlbReportDiscRate);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docsubtype", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("age", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doctype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["docsubtype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["age"] = BaseCls.GlbReportExeType + ' ' + BaseCls.GlbReportDiscRate;

            param.Rows.Add(dr);

            _pendingDel.Database.Tables["PROC_FWD_SALES"].SetDataSource(GLOB_DataTable);
            _pendingDel.Database.Tables["param"].SetDataSource(param);


        }

        public void RevertedItemsReport()
        {// kapila
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable _rvtItems=new DataTable();
            DataRow dr;

           // DataTable _rvtItems = bsObj.CHNLSVC.Sales.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_rvtItems = new DataTable();

                    tmp_rvtItems = bsObj.CHNLSVC.Sales.GetRevertItemDetailsAudit(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID,BaseCls.GlbReportJobNo);

                    _rvtItems.Merge(tmp_rvtItems);

                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);


            _revertedItm.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(_rvtItems);
            _revertedItm.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkBalCollectionReport()
        {// Sanjeewa 25-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportType, BaseCls.GlbReportStrStatus,"");

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));            
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));            

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            
            param.Rows.Add(dr);

            _PhyStkColl.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _PhyStkColl.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _PhyStkColl.Database.Tables["AUD_SVR_SERIAL"].SetDataSource(AUD_SVR_SERIAL);
            _PhyStkColl.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkVarienceNoteReport()
        {// Sanjeewa 06-11-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));            
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));            

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            
            param.Rows.Add(dr);

            _StkVarNote.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _StkVarNote.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _StkVarNote.Database.Tables["AUD_SVR_SERIAL"].SetDataSource(AUD_SVR_SERIAL);
            _StkVarNote.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkVerificationReport()
        {// Sanjeewa 26-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;

            param.Rows.Add(dr);

            _PhyStkVerify.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _PhyStkVerify.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _PhyStkVerify.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            _PhyStkVerify.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkVerificationCommStkReport()
        {// Sanjeewa 12-02-2016
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);
            DataTable SUPP_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode,"");

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;

            param.Rows.Add(dr);

            _PhyStkVerifycmstk.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _PhyStkVerifycmstk.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _PhyStkVerifycmstk.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            _PhyStkVerifycmstk.Database.Tables["SUPP_ITEM"].SetDataSource(SUPP_ITEM);
            _PhyStkVerifycmstk.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkVerificationByRefReport()
        {// Sanjeewa 28-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;

            param.Rows.Add(dr);

            _PhyStkbyRef.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _PhyStkbyRef.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _PhyStkbyRef.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            _PhyStkbyRef.Database.Tables["param"].SetDataSource(param);
        }

        public void ManagerExplanationReport()
        {// Sanjeewa 28-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;

            param.Rows.Add(dr);

            _PhyMgrExpl.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _PhyMgrExpl.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _PhyMgrExpl.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            _PhyMgrExpl.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkVerificationDefReport()
        {// Sanjeewa 28-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);
            
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;

            param.Rows.Add(dr);

            _PhyStkDef.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _PhyStkDef.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _PhyStkDef.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);            
            _PhyStkDef.Database.Tables["param"].SetDataSource(param);
        }

        public void UsedFixedAssetReport()
        {// Sanjeewa 29-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);
            DataTable AUD_SVR_RMK_VAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionRemarkDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, 7);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));
            param.Columns.Add("companyname", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;
            dr["companyname"] = BaseCls.GlbReportComp;

            param.Rows.Add(dr);

            _FixAsst.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _FixAsst.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _FixAsst.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            _FixAsst.Database.Tables["AUD_SVR_RMK_VAL"].SetDataSource(AUD_SVR_RMK_VAL);
            _FixAsst.Database.Tables["param"].SetDataSource(param);
        }

        public void SerialMismatchReport()
        {// Sanjeewa 28-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;

            param.Rows.Add(dr);

            _SerMismatch.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _SerMismatch.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _SerMismatch.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            _SerMismatch.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkBalAgeingReport()
        {// Sanjeewa 29-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable AUD_SVR_SERIAL = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionSerialAgeDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;

            param.Rows.Add(dr);

            _AgeItm.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _AgeItm.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            _AgeItm.Database.Tables["AUD_SVR_SERIAL"].SetDataSource(AUD_SVR_SERIAL);
            _AgeItm.Database.Tables["param"].SetDataSource(param);
        }

        public void PhyStkBalFIFOReport()
        {// Sanjeewa 29-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable AUD_SVR_MAIN = bsObj.CHNLSVC.MsgPortal.PhyStkBalCollectionHeaderDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            DataTable GLB_AUDIT_FIFO = bsObj.CHNLSVC.MsgPortal.PhyStkBalFIFODetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);            

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;

            param.Rows.Add(dr);

            _FIFO.Database.Tables["AUD_SVR_MAIN"].SetDataSource(AUD_SVR_MAIN);
            _FIFO.Database.Tables["GLB_AUDIT_FIFO"].SetDataSource(GLB_AUDIT_FIFO);
            _FIFO.Database.Tables["param"].SetDataSource(param);
        }

        public void UnconfirmedAODReport()
        {// Sanjeewa 30-10-2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable GLB_AUDIT_UNCONF_AOD=new DataTable();
            if(BaseCls.GlbReportParaLine1==1)   //get processed data
                GLB_AUDIT_UNCONF_AOD = bsObj.CHNLSVC.MsgPortal.UnconfirmedAODDet_Pro(BaseCls.GlbReportProfit);
            else
             GLB_AUDIT_UNCONF_AOD = bsObj.CHNLSVC.MsgPortal.UnconfirmedAODDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, BaseCls.GlbReportAsAtDate);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            //dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy hh:mm:ss tt") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy hh:mm:ss tt");
            dr["period"] = " As at " + BaseCls.GlbReportAsAtDate;

            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;

            param.Rows.Add(dr);

            _GIT.Database.Tables["GLB_AUDIT_UNCONF_AOD"].SetDataSource(GLB_AUDIT_UNCONF_AOD);
            _GIT.Database.Tables["param"].SetDataSource(param);
        }

        public void FixedAssetBalReport()
        {// Sanjeewa 30-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable FIXED_ASST_BAL = bsObj.CHNLSVC.Inventory.FixedAssetBalDetails(BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("reftype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["reftype"] = BaseCls.GlbReportStrStatus;

            param.Rows.Add(dr);

            _FixBal.Database.Tables["FIXED_ASST_BAL"].SetDataSource(FIXED_ASST_BAL);
            _FixBal.Database.Tables["param"].SetDataSource(param);
        }
               

        public Boolean RemoveCols()
        {
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = bsObj.CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "REMCOLAUD", DateTime.Now.Date);
            if (_SystemPara.Hsy_cd != null)
            {
                if (_SystemPara.Hsy_val == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void ExecutiveSummaryReport()
        {// Sanjeewa 17-03-2017
            DataTable param = new DataTable();
            DataRow dr;

            Dictionary<string, DataTable> dict = new Dictionary<string, DataTable>();
            DateTime jobdate=new DateTime();
            string pc_cd = "";
            int i = 0;
            DataTable note01 = new DataTable(); DataTable note02 = new DataTable(); DataTable note03 = new DataTable(); DataTable note04 = new DataTable(); DataTable note05 = new DataTable();
            DataTable note06 = new DataTable(); DataTable note07 = new DataTable(); DataTable note08 = new DataTable(); DataTable note09 = new DataTable(); DataTable note10 = new DataTable();
            DataTable note11 = new DataTable(); DataTable note12 = new DataTable(); DataTable note13 = new DataTable(); DataTable note14 = new DataTable(); DataTable note15 = new DataTable();
            DataTable xnote01 = new DataTable(); DataTable xnote02 = new DataTable(); DataTable xnote03 = new DataTable(); DataTable xnote04 = new DataTable(); DataTable xnote05 = new DataTable();
            DataTable xnote06 = new DataTable(); DataTable xnote07 = new DataTable();
            DataTable _diffserial = new DataTable();
            DataTable pendingdelivery = new DataTable();
            DataTable unconfaod = new DataTable();
            DataTable reverted = new DataTable();
            DataTable fifo = new DataTable();
            DataTable Rcc = new DataTable();
            DataTable serialmismatch = new DataTable();
            DataTable nonmoving = new DataTable();
            DataTable fixasset = new DataTable();
            DataTable _jobMembers = new DataTable();
            DataTable _PendAgree = new DataTable();
            DataTable agedebtorsum = new DataTable();

            DataTable AUD_REP_MAIN = bsObj.CHNLSVC.MsgPortal.GetAuditExecSummary1(BaseCls.GlbReportDoc1, BaseCls.GlbReportJobNo, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out dict, out _diffserial);

            foreach (KeyValuePair<string, DataTable> pair in dict)
            {
                if (pair.Key.ToString() == "note01")
                {
                    note01 = pair.Value;
                }
                if (pair.Key.ToString() == "note02")
                {
                    note02 = pair.Value;
                }
                if (pair.Key.ToString() == "note03")
                {
                    note03 = pair.Value;
                }
                if (pair.Key.ToString() == "note04")
                {
                    note04 = pair.Value;
                }
                if (pair.Key.ToString() == "note05")
                {
                    note05 = pair.Value;
                }
                if (pair.Key.ToString() == "note06")
                {
                    note06 = pair.Value;
                }
                if (pair.Key.ToString() == "note07")
                {
                    note07 = pair.Value;
                }
                if (pair.Key.ToString() == "note08")
                {
                    note08 = pair.Value;
                }
                if (pair.Key.ToString() == "note09")
                {
                    note09 = pair.Value;
                }
                if (pair.Key.ToString() == "note10")
                {
                    note10 = pair.Value;
                }
                if (pair.Key.ToString() == "note11")
                {
                    note11 = pair.Value;
                }
                if (pair.Key.ToString() == "note12")
                {
                    note12 = pair.Value;
                }
                if (pair.Key.ToString() == "note13")
                {
                    note13 = pair.Value;
                }
                if (pair.Key.ToString() == "note14")
                {
                    note14 = pair.Value;
                }
                if (pair.Key.ToString() == "note15")
                {
                    note15 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote01")
                {
                    xnote01 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote02")
                {
                    xnote02 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote03")
                {
                    xnote03 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote04")
                {
                    xnote04 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote05")
                {
                    xnote05 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote06")
                {
                    xnote06 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote07")
                {
                    xnote07 = pair.Value;
                }
                if (pair.Key.ToString() == "pendingdelivery")
                {
                    pendingdelivery = pair.Value;
                }
                if (pair.Key.ToString() == "unconfaod")
                {
                    unconfaod = pair.Value;
                }
                if (pair.Key.ToString() == "reverted")
                {
                    reverted = pair.Value;
                }
                if (pair.Key.ToString() == "fifo")
                {
                    fifo = pair.Value;
                }
                if (pair.Key.ToString() == "Rcc")
                {
                    Rcc = pair.Value;
                }
                if (pair.Key.ToString() == "serialmismatch")
                {
                    serialmismatch = pair.Value;
                }
                if (pair.Key.ToString() == "nonmoving")
                {
                    nonmoving = pair.Value;
                }
                if (pair.Key.ToString() == "fixasset")
                {
                    fixasset = pair.Value;
                }
                if (pair.Key.ToString() == "members")
                {
                    _jobMembers = pair.Value;
                }
                if (pair.Key.ToString() == "agedebtorsum")
                {
                    agedebtorsum = pair.Value;
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("mainjobno", typeof(string));
            param.Columns.Add("removecol", typeof(int));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["mainjobno"] = BaseCls.GlbReportDoc1;
            dr["removecol"] = RemoveCols();

            param.Rows.Add(dr);

            DataTable dt2;
            var filteredDataRows1 = _diffserial.Select("audjs_rpt_type='X NOTE 04'");
            dt2 = new DataTable();
            if (filteredDataRows1.Length != 0)
                dt2 = filteredDataRows1.CopyToDataTable();
            else
                dt2 = _diffserial.Clone();
            foreach (DataRow _drow in AUD_REP_MAIN.Rows)
            {
                _drow["XNote4"] = dt2.Rows.Count;
                jobdate = Convert.ToDateTime(_drow["JobDate"]).Date;
                pc_cd = _drow["pc_code"].ToString();
            }

            filteredDataRows1 = _diffserial.Select("audjs_rpt_type='X NOTE 03'");
            dt2 = new DataTable();
            if (filteredDataRows1.Length != 0)
                dt2 = filteredDataRows1.CopyToDataTable();
            else
                dt2 = _diffserial.Clone();
            foreach (DataRow _drow in AUD_REP_MAIN.Rows)
            {
                _drow["XNote3"] = dt2.Rows.Count;
            }

            DateTime _enddate = jobdate.AddDays(-30);

            _PendAgree = bsObj.CHNLSVC.MsgPortal.GetAgreementStatementDetailsAudit(_enddate, BaseCls.GlbUserComCode, pc_cd, BaseCls.GlbUserID);

            foreach (DataRow _drow in _PendAgree.Rows)
            {
                i = i + 1;
                if (i == 1)
                {
                    foreach (DataRow _drow1 in AUD_REP_MAIN.Rows)
                    {
                        _drow1["dtlmismatch"] = _drow["dtlmismatch"];
                        _drow1["acc_close"] = _drow["acc_close"];
                        _drow1["sr_return"] = _drow["sr_return"];
                    }
                }
            }

            _executiveSummary.Database.Tables["AUD_REP_MAIN"].SetDataSource(AUD_REP_MAIN);
            _executiveSummary.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _executiveSummary.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    ReportDocument subRepDoc = _executiveSummary.Subreports[_cs.SubreportName];

                    if (_cs.SubreportName == "pend_agree")
                    {
                        subRepDoc = _executiveSummary.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AGREE_PENDING"].SetDataSource(_PendAgree);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "unconfaod")
                    {
                        subRepDoc = _executiveSummary.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GLB_AUDIT_UNCONF_AOD"].SetDataSource(unconfaod);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "agedebt")
                    {
                        subRepDoc = _executiveSummary.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["Debt_Arr_Sum"].SetDataSource(agedebtorsum);
                        subRepDoc.Dispose();
                    }
                }
            }

        }

        public void StockSignatureReport()
        {// Sanjeewa 18-03-2017
            DataTable param = new DataTable();
            DataRow dr;

            Dictionary<string, DataTable> dict = new Dictionary<string, DataTable>();
            DataTable note01 = new DataTable(); DataTable note02 = new DataTable(); DataTable note03 = new DataTable(); DataTable note04 = new DataTable(); DataTable note05 = new DataTable();
            DataTable note06 = new DataTable(); DataTable note07 = new DataTable(); DataTable note08 = new DataTable(); DataTable note09 = new DataTable(); DataTable note10 = new DataTable();
            DataTable note11 = new DataTable(); DataTable note12 = new DataTable(); DataTable note13 = new DataTable(); DataTable note14 = new DataTable(); DataTable note15 = new DataTable();
            DataTable xnote01 = new DataTable(); DataTable xnote02 = new DataTable(); DataTable xnote03 = new DataTable(); DataTable xnote04 = new DataTable(); DataTable xnote05 = new DataTable();
            DataTable xnote06 = new DataTable(); DataTable xnote07 = new DataTable();
            DataTable _diffserial = new DataTable();
            DataTable pendingdelivery = new DataTable();
            DataTable unconfaod = new DataTable();
            DataTable reverted = new DataTable();
            DataTable fifo = new DataTable();
            DataTable Rcc = new DataTable();
            DataTable serialmismatch = new DataTable();
            DataTable nonmoving = new DataTable();
            DataTable fixasset = new DataTable();
            DataTable _jobMembers = new DataTable();

            DataTable AUD_REP_MAIN = bsObj.CHNLSVC.MsgPortal.GetAuditExecSummary1(BaseCls.GlbReportDoc1, BaseCls.GlbReportJobNo, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out dict, out _diffserial);

            foreach (KeyValuePair<string, DataTable> pair in dict)
            {
                if (pair.Key.ToString() == "note01")
                {
                    note01 = pair.Value;
                }
                if (pair.Key.ToString() == "note02")
                {
                    note02 = pair.Value;
                }
                if (pair.Key.ToString() == "note03")
                {
                    note03 = pair.Value;
                }
                if (pair.Key.ToString() == "note04")
                {
                    note04 = pair.Value;
                }
                if (pair.Key.ToString() == "note05")
                {
                    note05 = pair.Value;
                }
                if (pair.Key.ToString() == "note06")
                {
                    note06 = pair.Value;
                }
                if (pair.Key.ToString() == "note07")
                {
                    note07 = pair.Value;
                }
                if (pair.Key.ToString() == "note08")
                {
                    note08 = pair.Value;
                }
                if (pair.Key.ToString() == "note09")
                {
                    note09 = pair.Value;
                }
                if (pair.Key.ToString() == "note10")
                {
                    note10 = pair.Value;
                }
                if (pair.Key.ToString() == "note11")
                {
                    note11 = pair.Value;
                }
                if (pair.Key.ToString() == "note12")
                {
                    note12 = pair.Value;
                }
                if (pair.Key.ToString() == "note13")
                {
                    note13 = pair.Value;
                }
                if (pair.Key.ToString() == "note14")
                {
                    note14 = pair.Value;
                }
                if (pair.Key.ToString() == "note15")
                {
                    note15 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote01")
                {
                    xnote01 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote02")
                {
                    xnote02 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote03")
                {
                    xnote03 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote04")
                {
                    xnote04 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote05")
                {
                    xnote05 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote06")
                {
                    xnote06 = pair.Value;
                }
                if (pair.Key.ToString() == "xnote07")
                {
                    xnote07 = pair.Value;
                }
                if (pair.Key.ToString() == "pendingdelivery")
                {
                    pendingdelivery = pair.Value;
                }
                if (pair.Key.ToString() == "unconfaod")
                {
                    unconfaod = pair.Value;
                }
                if (pair.Key.ToString() == "reverted")
                {
                    reverted = pair.Value;
                }
                if (pair.Key.ToString() == "fifo")
                {
                    fifo = pair.Value;
                }
                if (pair.Key.ToString() == "Rcc")
                {
                    Rcc = pair.Value;
                }
                if (pair.Key.ToString() == "serialmismatch")
                {
                    serialmismatch = pair.Value;
                }
                if (pair.Key.ToString() == "nonmoving")
                {
                    nonmoving = pair.Value;
                }
                if (pair.Key.ToString() == "fixasset")
                {
                    fixasset = pair.Value;
                }
                if (pair.Key.ToString() == "members")
                {
                    _jobMembers = pair.Value;
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));
            param.Columns.Add("mainjobno", typeof(string));
            param.Columns.Add("removecol", typeof(int));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;
            dr["mainjobno"] = BaseCls.GlbReportDoc1;
            dr["removecol"] = RemoveCols();

            param.Rows.Add(dr);

            DataTable dt2;
            var filteredDataRows1 = _diffserial.Select("audjs_rpt_type='X NOTE 04'");
            dt2 = new DataTable();
            if (filteredDataRows1.Length != 0)
                dt2 = filteredDataRows1.CopyToDataTable();
            else
                dt2 = _diffserial.Clone();
            foreach (DataRow _drow in AUD_REP_MAIN.Rows)
            {
                _drow["XNote4"] = dt2.Rows.Count;
            }

            filteredDataRows1 = _diffserial.Select("audjs_rpt_type='X NOTE 03'");
            dt2 = new DataTable();
            if (filteredDataRows1.Length != 0)
                dt2 = filteredDataRows1.CopyToDataTable();
            else
                dt2 = _diffserial.Clone();
            foreach (DataRow _drow in AUD_REP_MAIN.Rows)
            {
                _drow["XNote3"] = dt2.Rows.Count;
            }

            _stksign.Database.Tables["AUD_REP_MAIN"].SetDataSource(AUD_REP_MAIN);
            _stksign.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _stksign.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    ReportDocument subRepDoc = _stksign.Subreports[_cs.SubreportName];
                    DataTable dt1;

                    if (_cs.SubreportName == "Note01_ex")
                    {                        
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 01'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note01);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note02")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 02'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note02);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note03")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 03'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note03);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note04")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 04'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note04);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note07")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 07'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note07);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note05-06")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 05' or audjs_rpt_type='NOTE 06'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        note05.Merge(note06);
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note05);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note08")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 08'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note08);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "XNote02")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 02'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote02);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "XNote05")
                    {                        
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 05'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote05);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note09")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 09'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note09);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note10")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 10'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note10);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note11")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 11'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note11);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note13")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 13'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note13);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note14")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 14'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note14);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note15")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 15'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note15);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Note12")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='NOTE 12'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(note12);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "pendingdelivery")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["FORWARD_SALES"].SetDataSource(pendingdelivery);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "unconfaod")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GLB_AUDIT_UNCONF_AOD"].SetDataSource(unconfaod);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "xnote01")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 01'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote01);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "reverted")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(reverted);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "fifo")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GLB_AUDIT_FIFO"].SetDataSource(fifo);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Rcc")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_RCC"].SetDataSource(Rcc);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "serialmismatch")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 03'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote03);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "xnote04")
                    {      
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 04'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote04);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "nonmoving")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_SVR_SERIAL"].SetDataSource(nonmoving);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "XNote06")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 06'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote06);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "XNote07")
                    {
                        var filteredDataRows = _diffserial.Select("audjs_rpt_type='X NOTE 07'");
                        dt1 = new DataTable();
                        if (filteredDataRows.Length != 0)
                            dt1 = filteredDataRows.CopyToDataTable();
                        else
                            dt1 = _diffserial.Clone();
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_SER"].SetDataSource(dt1);
                        subRepDoc.Database.Tables["AUD_REP_RMK"].SetDataSource(xnote07);
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "fixasset")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["FIX_ASST"].SetDataSource(fixasset);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "members")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_MEMBERS"].SetDataSource(_jobMembers);                        
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "Members2")
                    {
                        subRepDoc = _stksign.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["AUD_REP_MEMBERS"].SetDataSource(_jobMembers);
                        subRepDoc.Dispose();
                    }
                }
            }
        }

        public void StockSummaryReport()
        {// Lakshika 2016/10/20
            DataTable param = new DataTable();
            DataRow dr;

            DataTable dtStockVal = bsObj.CHNLSVC.Sales.GetStockSummaryReportDetail(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            //DataTable AUD_SVR_ITEM = bsObj.CHNLSVC.Sales.PhyStkBalCollectionItemDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo);
            //DataTable GLB_AUDIT_STOCK_SERIAL = bsObj.CHNLSVC.Sales.PhyStkBalCollectionSerialMatchDetails(BaseCls.GlbUserID, BaseCls.GlbReportJobNo, BaseCls.GlbReportDocType, BaseCls.GlbReportStrStatus, BaseCls.GlbReportDocMismatch);
            //DataTable SUPP_ITEM = bsObj.CHNLSVC.Sales.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            //param.Columns.Add("heading_1", typeof(string));
            //param.Columns.Add("period", typeof(string));
            //param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("jobno", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            //dr["heading_1"] = BaseCls.GlbReportHeading;
            //dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            //dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["jobno"] = BaseCls.GlbReportJobNo;

            param.Rows.Add(dr);

            //_executiveSummary.Database.Tables["STOCKVALUES"].SetDataSource(dtStockVal);
            //_PhyStkVerifycmstk.Database.Tables["AUD_SVR_ITEM"].SetDataSource(AUD_SVR_ITEM);
            //_PhyStkVerifycmstk.Database.Tables["GLB_AUDIT_STOCK_SERIAL"].SetDataSource(GLB_AUDIT_STOCK_SERIAL);
            //_PhyStkVerifycmstk.Database.Tables["SUPP_ITEM"].SetDataSource(SUPP_ITEM);
            //_executiveSummary.Database.Tables["param"].SetDataSource(param);
        }
    }
}
