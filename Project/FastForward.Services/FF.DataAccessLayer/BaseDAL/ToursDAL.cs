using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;

namespace FF.DataAccessLayer
{
    public class ToursDAL : BaseDAL
    {
        //Sahan
        public DataTable SP_TOURS_GET_OVERLAP_DATES(string P_MFD_VEH_NO, string P_MFD_DRI, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_seq_no)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_MFD_VEH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFD_VEH_NO;
            (param[1] = new OracleParameter("P_MFD_DRI", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFD_DRI;
            (param[2] = new OracleParameter("p_mfd_frm_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_frm_dt;
            (param[3] = new OracleParameter("p_mfd_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_to_dt;
            (param[4] = new OracleParameter("p_mfd_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_seq_no;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_DRIVER", "SP_TOURS_GET_OVERLAP_DATES", CommandType.StoredProcedure, false, param);

        }
        public DataTable sp_tours_get_Selected_alloc(string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_mfd_veh_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_veh_no;
            (param[1] = new OracleParameter("p_mfd_dri", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_dri;
            (param[2] = new OracleParameter("p_mfd_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mfd_act;
            (param[3] = new OracleParameter("p_mfd_frm_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_frm_dt;
            (param[4] = new OracleParameter("p_mfd_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_to_dt;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_DRIVER", "sp_tours_get_Selected_alloc", CommandType.StoredProcedure, false, param);
        }

        //Sahan
        public DataTable SP_TOURS_GET_ALLOCATIONS(string P_MFD_VEH_NO, string p_MFD_DRI)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_MFD_VEH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFD_VEH_NO;
            (param[1] = new OracleParameter("p_MFD_DRI", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_MFD_DRI;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_DRIVER", "SP_TOURS_GET_ALLOCATIONS", CommandType.StoredProcedure, false, param);
        }

        //Sahan
        public DataTable SP_TOURS_GET_DRIVER_COM(string P_MPE_EPF)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_MPE_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MPE_EPF;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("mst_pcemp", "SP_TOURS_GET_DRIVER_COM", CommandType.StoredProcedure, false, param);
        }

        //Sahan
        public Int32 sp_tours_update_driver_alloc(string p_mfd_seq, string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_cre_by, DateTime p_mfd_cre_dt, string p_mfd_mod_by, DateTime p_mfd_mod_dt, string p_mfd_com, string p_mfd_pc)
        {
            OracleParameter[] param = new OracleParameter[13];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_mfd_seq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_seq;
            (param[1] = new OracleParameter("p_mfd_veh_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_veh_no;
            (param[2] = new OracleParameter("p_mfd_dri", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_dri;
            (param[3] = new OracleParameter("p_mfd_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mfd_act;
            (param[4] = new OracleParameter("p_mfd_frm_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_frm_dt;
            (param[5] = new OracleParameter("p_mfd_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_to_dt;
            (param[6] = new OracleParameter("p_mfd_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_cre_by;
            (param[7] = new OracleParameter("p_mfd_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_cre_dt;
            (param[8] = new OracleParameter("p_mfd_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_mod_by;
            (param[9] = new OracleParameter("p_mfd_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_mod_dt;
            (param[10] = new OracleParameter("p_mfd_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_com;
            (param[11] = new OracleParameter("p_mfd_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfd_pc;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int32)UpdateRecords("sp_tours_update_driver_alloc", CommandType.StoredProcedure, param);
            return effects;
        }

        //Sahan
        public DataTable SP_TOURS_GET_DRIVER(string P_MFA_PC)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_MFA_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFA_PC;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("mst_pcemp", "SP_TOURS_GET_DRIVER", CommandType.StoredProcedure, false, param);
        }

        //Sahan
        public DataTable SP_TOURS_GET_VEHICLE(string P_MFA_PC)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_MFA_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFA_PC;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_ALLOC", "SP_TOURS_GET_VEHICLE", CommandType.StoredProcedure, false, param);
        }

        //Sahan
        public DataTable sp_tours_get_fleet_alloc2(string p_mfa_regno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_mfa_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_regno;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_ALLOC", "sp_tours_get_fleet_alloc2", CommandType.StoredProcedure, false, param);
        }

        //Sahan
        public DataTable sp_tours_get_fleet_alloc(string p_mfa_regno, string p_mfa_pc)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_mfa_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_regno;
            (param[1] = new OracleParameter("p_mfa_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_pc;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_ALLOC", "sp_tours_get_fleet_alloc", CommandType.StoredProcedure, false, param);
        }

        //Sahan

        public Int32 sp_tours_update_fleet_alloc(string p_mfa_regno, string p_mfa_pc, Int32 p_mfa_act, string p_mfa_cre_by, DateTime p_mfa_cre_dt, string p_mfa_mod_by, DateTime p_mfa_mod_dt)
        {
            OracleParameter[] param = new OracleParameter[8];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_mfa_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_regno;
            (param[1] = new OracleParameter("p_mfa_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_pc;
            (param[2] = new OracleParameter("p_mfa_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mfa_act;
            (param[3] = new OracleParameter("p_mfa_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_cre_by;
            (param[4] = new OracleParameter("p_mfa_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfa_cre_dt;
            (param[5] = new OracleParameter("p_mfa_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mfa_mod_by;
            (param[6] = new OracleParameter("p_mfa_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfa_mod_dt;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int32)UpdateRecords("sp_tours_update_fleet_alloc", CommandType.StoredProcedure, param);
            return effects;
        }
        //Sahan
        public DataTable Get_Fleet(string p_mstf_regno)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_mstf_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_regno;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("mst_fleet", "sp_tours_Get_Fleet", CommandType.StoredProcedure, false, param);
        }
        public Boolean ConnectionOpen()
        {
            Boolean _isOpen = false;
            if (oConnection.State != ConnectionState.Open)
            {
                oConnection.Open();
            }

            if (oConnection.State == ConnectionState.Open)
                _isOpen = true;
            else _isOpen = false;


            _isTr = false;
            return _isOpen;
        }

        //Sahan
        public Int32 sp_tours_update_fleet(string p_mstf_regno, DateTime p_mstf_dt, string p_mstf_brd, string p_mstf_model, string p_mstf_veh_tp, string p_mstf_sipp_cd,
            Int32 p_mstf_st_meter, string p_mstf_own, string p_mstf_own_nm, Int32 p_mstf_own_cont, Int32 p_mstf_lst_sermet, string p_mstf_tou_regno, Int32 p_mstf_is_lease,
            DateTime p_mstf_insu_exp, DateTime p_mstf_reg_exp, string p_mstf_fual_tp, Int32 p_mstf_act, string p_mstf_cre_by, DateTime p_mstf_cre_dt, string p_mstf_mod_by,
            DateTime p_mstf_mod_dt, string p_mstf_engin_cap, Int32 p_mstf_noof_seat, string p_mst_email, string p_mst_inscom, string p_mst_comment, decimal cost,
            string reason, string ownadd1, string ownadd2, DateTime p_mstf_from_dt, DateTime p_mstf_to_dt, string p_nic, decimal mileage, decimal fullday, decimal halfday, decimal air, decimal corramount, decimal deposit)
        {
            OracleParameter[] param = new OracleParameter[40];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_mstf_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_regno;
            (param[1] = new OracleParameter("p_mstf_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_dt;
            (param[2] = new OracleParameter("p_mstf_brd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_brd;
            (param[3] = new OracleParameter("p_mstf_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_model;
            (param[4] = new OracleParameter("p_mstf_veh_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_veh_tp;
            (param[5] = new OracleParameter("p_mstf_sipp_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_sipp_cd;
            (param[6] = new OracleParameter("p_mstf_st_meter", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mstf_st_meter;
            (param[7] = new OracleParameter("p_mstf_own", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_own;
            (param[8] = new OracleParameter("p_mstf_own_nm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_own_nm;
            (param[9] = new OracleParameter("p_mstf_own_cont", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mstf_own_cont;
            (param[10] = new OracleParameter("p_mstf_lst_sermet", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mstf_lst_sermet;
            (param[11] = new OracleParameter("p_mstf_tou_regno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_tou_regno;
            (param[12] = new OracleParameter("p_mstf_is_lease", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mstf_is_lease;
            (param[13] = new OracleParameter("p_mstf_insu_exp", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_insu_exp;
            (param[14] = new OracleParameter("p_mstf_reg_exp", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_reg_exp;
            (param[15] = new OracleParameter("p_mstf_fual_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_fual_tp;
            (param[16] = new OracleParameter("p_mstf_act", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mstf_act;
            (param[17] = new OracleParameter("p_mstf_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_cre_by;
            (param[18] = new OracleParameter("p_mstf_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_cre_dt;
            (param[19] = new OracleParameter("p_mstf_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_mod_by;
            (param[20] = new OracleParameter("p_mstf_mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_mod_dt;
            (param[21] = new OracleParameter("p_mstf_engin_cap", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mstf_engin_cap;
            (param[22] = new OracleParameter("p_mstf_noof_seat", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_mstf_noof_seat;
            (param[23] = new OracleParameter("p_mst_email", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mst_email;
            (param[24] = new OracleParameter("p_mst_inscom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mst_inscom;
            (param[25] = new OracleParameter("p_mst_comment", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_mst_comment;
            if (cost == null)
            {
                cost = 0;
            }
            (param[26] = new OracleParameter("p_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = cost;
            (param[27] = new OracleParameter("p_reason", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = reason;
            (param[28] = new OracleParameter("p_own_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ownadd1;
            (param[29] = new OracleParameter("p_own_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ownadd2;
            (param[30] = new OracleParameter("p_mstf_from_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_from_dt;
            (param[31] = new OracleParameter("p_mstf_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mstf_to_dt;
            (param[32] = new OracleParameter("p_own_nic", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_nic;
            (param[33] = new OracleParameter("p_mileage", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = mileage;
            (param[34] = new OracleParameter("p_full", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = fullday;
            (param[35] = new OracleParameter("p_half", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = halfday;
            (param[36] = new OracleParameter("p_air", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = air;
            (param[37] = new OracleParameter("p_amount", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = corramount;
            (param[38] = new OracleParameter("p_deposit", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = deposit;
            param[39] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int32)UpdateRecords("sp_tours_update_fleet", CommandType.StoredProcedure, param);
            return effects;
        }

        //Sahan
        public DataTable Get_Vehicle_Type()
        {
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("mst_veh_tp", "SP_TOURS_Get_Vehicle_Type", CommandType.StoredProcedure, false, param);
        }

        //Rukshan
        public DataTable InvoiceDeatilsForPrint(string _recNo, string _Code)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recNo;
            (param[1] = new OracleParameter("p_C", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _Code;
            param[2] = new OracleParameter("I_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "GETINVOICE_R", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }
        //Rukshan
        public List<invoiceCenter> InvoiceDeatilsForPrintList(string _recNo)
        {
            List<invoiceCenter> _list = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recNo;
            param[1] = new OracleParameter("I_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults;
            _dtResults = QueryDataTable("tbl", "GETINVOICE_R", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<invoiceCenter>(_dtResults, invoiceCenter.ConvertTotal);
            }

            return _list;
        }

        //Tharaka 2015-03-09
        public List<MST_ENQTP> GET_ENQUIRY_TYPE(string Com)
        {
            List<MST_ENQTP> result = new List<MST_ENQTP>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GETENQTYPES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ENQTP>(_dtResults, MST_ENQTP.Converter);
            }
            return result;
        }

        // Nadeeka 06-04-2015
        public DataTable GET_ENQUIRY_STATUS(string Com)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GETENQSTATUS", CommandType.StoredProcedure, false, _para);

            return _dtResults;
        }

        //Tharaka 2015-03-09
        public List<MST_FACBY> GET_FACILITY_BY(string Com, string type)
        {
            List<MST_FACBY> result = new List<MST_FACBY>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_FACTYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_FACILITY_BY", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_FACBY>(_dtResults, MST_FACBY.Converter);
            }
            return result;
        }

        //Tharaka 2015-03-09
        public Int32 Save_GEN_CUST_ENQ(GEN_CUST_ENQ lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[58];
            (param[0] = new OracleParameter("P_GCE_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCE_SEQ;
            (param[1] = new OracleParameter("P_GCE_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ_ID;
            (param[2] = new OracleParameter("P_GCE_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_REF;
            (param[3] = new OracleParameter("P_GCE_ENQ_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ_TP;
            (param[4] = new OracleParameter("P_GCE_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_COM;
            (param[5] = new OracleParameter("P_GCE_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_PC;
            (param[6] = new OracleParameter("P_GCE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCE_DT;
            (param[7] = new OracleParameter("P_GCE_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CUS_CD;
            (param[8] = new OracleParameter("P_GCE_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_NAME;
            (param[9] = new OracleParameter("P_GCE_ADD1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ADD1;
            (param[10] = new OracleParameter("P_GCE_ADD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ADD2;
            (param[11] = new OracleParameter("P_GCE_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_MOB;
            (param[12] = new OracleParameter("P_GCE_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_EMAIL;
            (param[13] = new OracleParameter("P_GCE_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_NIC;
            (param[14] = new OracleParameter("P_GCE_EXPECT_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCE_EXPECT_DT;
            (param[15] = new OracleParameter("P_GCE_SER_LVL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_SER_LVL;
            (param[16] = new OracleParameter("P_GCE_ENQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ;
            (param[17] = new OracleParameter("P_GCE_ENQ_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ_COM;
            (param[18] = new OracleParameter("P_GCE_ENQ_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ_PC;
            (param[19] = new OracleParameter("P_GCE_ENQ_PC_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ_PC_DESC;
            (param[20] = new OracleParameter("P_GCE_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCE_STUS;
            (param[21] = new OracleParameter("P_GCE_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CRE_BY;
            (param[22] = new OracleParameter("P_GCE_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCE_CRE_DT;
            (param[23] = new OracleParameter("P_GCE_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_MOD_BY;
            (param[24] = new OracleParameter("P_GCE_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCE_MOD_DT;

            (param[25] = new OracleParameter("GCE_FRM_TN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_FRM_TN;
            (param[26] = new OracleParameter("GCE_TO_TN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_TO_TN;
            (param[27] = new OracleParameter("GCE_FRM_ADD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_FRM_ADD;
            (param[28] = new OracleParameter("GCE_TO_ADD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_TO_ADD;
            (param[29] = new OracleParameter("GCE_NO_PASS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCE_NO_PASS;
            (param[30] = new OracleParameter("GCE_VEH_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_VEH_TP;
            (param[31] = new OracleParameter("GCE_RET_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCE_RET_DT;

            (param[32] = new OracleParameter("P_GCE_FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_FLEET;
            (param[33] = new OracleParameter("P_GCE_DRIVER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_DRIVER;
            (param[34] = new OracleParameter("P_GCE_GUESS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_GUESS;

            (param[35] = new OracleParameter("P_gce_mainreqid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.Gce_mainreqid;
            (param[36] = new OracleParameter("P_gce_bill_cuscd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.Gce_bill_cuscd;
            (param[37] = new OracleParameter("P_gce_bill_cusname", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.Gce_bill_cusname;

            (param[38] = new OracleParameter("P_GCE_CONT_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CONT_PER;
            (param[39] = new OracleParameter("P_GCE_CONT_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CONT_MOB;
            (param[40] = new OracleParameter("P_GCE_REQ_NO_VEH", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCE_REQ_NO_VEH;
            (param[41] = new OracleParameter("p_GCE_ENQ_SUB_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_ENQ_SUB_TP;
            (param[42] = new OracleParameter("p_GCE_CONT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CONT_CD;
            (param[43] = new OracleParameter("p_GCE_CONT_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CONT_EMAIL;
            (param[44] = new OracleParameter("p_GCE_EX_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_EX_CD;
            (param[45] = new OracleParameter("P_FRM_COUNTRY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_FRM_CONTRY;
            (param[46] = new OracleParameter("P_DEST_COUNTRY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_DEST_CONTRY;
            (param[47] = new OracleParameter("P_GCE_FLY_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_FLY_NO;
            (param[48] = new OracleParameter("P_GCE_FLY_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCE_FLY_DATE;
            (param[49] = new OracleParameter("P_GCE_CUS_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CUS_TYPE;
            (param[50] = new OracleParameter("P_RENTAL_AGENT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_RENTAL_AGENT;
            (param[51] = new OracleParameter("P_CITY_OF_ISSUE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_CITY_OF_ISSUE;
            (param[52] = new OracleParameter("P_PP_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_PP_NO;
            (param[53] = new OracleParameter("P_DL_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_DL_TYPE;


            //nuwan 
            (param[54] = new OracleParameter("P_GCE_DEPOSIT_CHG", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.GCE_DEPOSIT_CHG;
            (param[55] = new OracleParameter("P_GCE_LBLTY_CHG", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.GCE_LBLTY_CHG;
            (param[56] = new OracleParameter("P_GCE_TR_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCE_TR_CODE;
            param[57] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_GEN_CUST_ENQRY", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-09
        public GEN_CUST_ENQ GET_CUST_ENQRY(string Com, string PC, string ENQID)
        {
            GEN_CUST_ENQ result = new GEN_CUST_ENQ();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ENQID;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_CUST_ENQRY", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-03-10
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_CUST(string Com, string CustomerCode)
        {
            List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CustomerCode;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_ENQRY_BY_CUST", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterDetail);
            }
            return result;
        }

        //Tharaka 2015-03-11
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_PC_STATUS(string Com, string PC, String Status)
        {
            List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_ENQRY_BY_PC_STATUS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterDetail);
            }
            return result;
        }
        //subodana
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_PC_STATUS_NEW(string Com, string PC, String Status)
        {
            List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_ENQRY_BY_PC_NEW", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterDetail);
            }
            return result;
        }
        //Tharaka 2015-03-11
        public Int32 SAVE_GEN_ENQLOG(GEN_ENQLOG lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_GEL_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GEL_SEQ;
            (param[1] = new OracleParameter("P_GEL_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GEL_ENQ_ID;
            (param[2] = new OracleParameter("P_GEL_USR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GEL_USR;
            (param[3] = new OracleParameter("P_GEL_STAGE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GEL_STAGE;
            (param[4] = new OracleParameter("P_GEL_LOGWHEN", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GEL_LOGWHEN;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_GEN_ENQLOG", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-11
        public List<MST_TIMEEXPECT> GET_EXPETTIME(string Com, string PC)
        {
            List<MST_TIMEEXPECT> result = new List<MST_TIMEEXPECT>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_EXPETTIME", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_TIMEEXPECT>(_dtResults, MST_TIMEEXPECT.Converter);
            }
            return result;
        }

        //Tharaka 2015-03-11
        public List<MST_COST_CATE> GET_COST_CATE(string Com, string PC)
        {
            List<MST_COST_CATE> result = new List<MST_COST_CATE>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_COST_CATE", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_COST_CATE>(_dtResults, MST_COST_CATE.Converter);
            }
            return result;
        }

        //Tharaka 2015-03-16
        public Int32 Save_QUO_COST_HDR(QUO_COST_HDR lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[34];
            (param[0] = new OracleParameter("P_QCH_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_SEQ;
            (param[1] = new OracleParameter("P_QCH_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_COM;
            (param[2] = new OracleParameter("P_QCH_SBU", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_SBU;
            (param[3] = new OracleParameter("P_QCH_COST_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_COST_NO;
            (param[4] = new OracleParameter("P_QCH_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_DT;
            (param[5] = new OracleParameter("P_QCH_OTH_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_OTH_DOC;
            (param[6] = new OracleParameter("P_QCH_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_REF;
            (param[7] = new OracleParameter("P_QCH_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_CUS_CD;
            (param[8] = new OracleParameter("P_QCH_CUS_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_CUS_NAME;
            (param[9] = new OracleParameter("P_QCH_CUS_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_CUS_MOB;
            (param[10] = new OracleParameter("P_QCH_TOT_PAX", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_PAX;
            (param[11] = new OracleParameter("P_QCH_TOT_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_COST;
            (param[12] = new OracleParameter("P_QCH_TOT_COST_LOCAL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_COST_LOCAL;
            (param[13] = new OracleParameter("P_QCH_MARKUP", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_MARKUP;
            (param[14] = new OracleParameter("P_QCH_TOT_VALUE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_VALUE;
            (param[15] = new OracleParameter("P_QCH_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_ACT;
            (param[16] = new OracleParameter("P_QCH_SEND_CUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_SEND_CUS;
            (param[17] = new OracleParameter("P_QCH_CUS_SEND_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_CUS_SEND_DT;
            (param[18] = new OracleParameter("P_QCH_CUS_APP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_CUS_APP;
            (param[19] = new OracleParameter("P_QCH_CUS_APP_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_CUS_APP_DT;
            (param[20] = new OracleParameter("P_QCH_ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_ANAL1;
            (param[21] = new OracleParameter("P_QCH_ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_ANAL2;
            (param[22] = new OracleParameter("P_QCH_ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_ANAL3;
            (param[23] = new OracleParameter("P_QCH_ANAL4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_ANAL4;
            (param[24] = new OracleParameter("P_QCH_ANAL5", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_ANAL5;
            (param[25] = new OracleParameter("P_QCH_ANAL6", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_ANAL6;
            (param[26] = new OracleParameter("P_QCH_ANAL7", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_ANAL7;
            (param[27] = new OracleParameter("P_QCH_ANAL8", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_ANAL8;
            (param[28] = new OracleParameter("P_QCH_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_CRE_BY;
            (param[29] = new OracleParameter("P_QCH_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_CRE_DT;
            (param[30] = new OracleParameter("P_QCH_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_MOD_BY;
            (param[31] = new OracleParameter("P_QCH_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_MOD_DT;
            (param[32] = new OracleParameter("P_QCH_MARKUP_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_MARKUP_AMT;
            param[33] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_QUOCOSTHDR", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-17
        public Int32 SP_GETCOSTSEQ()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_GETCOSTSEQ", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-17
        public Int32 UPDATE_COST_DET_STATUS(Int32 status, Int32 seqNum, string costNumber, string user)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = status;
            (param[1] = new OracleParameter("P_SEQNUM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seqNum;
            (param[2] = new OracleParameter("P_COSTNUMBET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = costNumber;
            (param[3] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_UPDATE_COST_DET_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-17
        public Int32 SAVE_COST_DET(QUO_COST_DET oItem)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[30];
            (param[0] = new OracleParameter("P_QCD_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_SEQ;
            (param[1] = new OracleParameter("P_QCD_COST_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_COST_NO;
            (param[2] = new OracleParameter("P_QCD_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_CAT;
            (param[3] = new OracleParameter("P_QCD_SUB_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_SUB_CATE;
            (param[4] = new OracleParameter("P_QCD_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_DESC;
            (param[5] = new OracleParameter("P_QCD_CURR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_CURR;
            (param[6] = new OracleParameter("P_QCD_EX_RATE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_EX_RATE;
            (param[7] = new OracleParameter("P_QCD_QTY", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_QTY;
            (param[8] = new OracleParameter("P_QCD_UNIT_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_UNIT_COST;
            (param[9] = new OracleParameter("P_QCD_TAX", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_TAX;
            (param[10] = new OracleParameter("P_QCD_TOT_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_TOT_COST;
            (param[11] = new OracleParameter("P_QCD_TOT_LOCAL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_TOT_LOCAL;
            (param[12] = new OracleParameter("P_QCD_MARKUP", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_MARKUP;
            (param[13] = new OracleParameter("P_QCD_AF_MARKUP", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_AF_MARKUP;
            (param[14] = new OracleParameter("P_QCD_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_RMK;
            (param[15] = new OracleParameter("P_QCD_ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL1;
            (param[16] = new OracleParameter("P_QCD_ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL2;
            (param[17] = new OracleParameter("P_QCD_ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL3;
            (param[18] = new OracleParameter("P_QCD_ANAL4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL4;
            (param[19] = new OracleParameter("P_QCD_ANAL5", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL5;
            (param[20] = new OracleParameter("P_QCD_ANAL6", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL6;
            (param[21] = new OracleParameter("P_QCD_ANAL7", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL7;
            (param[22] = new OracleParameter("P_QCD_ANAL8", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.QCD_ANAL8;
            (param[23] = new OracleParameter("P_QCD_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_CRE_BY;
            (param[24] = new OracleParameter("P_QCD_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.QCD_CRE_DT;
            (param[25] = new OracleParameter("P_QCD_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.QCD_MOD_BY;
            (param[26] = new OracleParameter("P_QCD_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.QCD_MOD_DT;
            (param[27] = new OracleParameter("P_QCD_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.QCD_STATUS;
            (param[28] = new OracleParameter("P_QCD_MARKUP_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.QCD_MARKUP_AMT;
            param[29] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_COST_DET", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-17
        public QUO_COST_HDR GetCostSheetHeaderByEnquiryID(string Com, string PC, string enquiryID, string stages)
        {
            QUO_COST_HDR result = new QUO_COST_HDR();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("P_ENQRYID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enquiryID;
            (_para[3] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stages;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_QUO_COST_HDR_BY_EQ", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<QUO_COST_HDR>(_dtResults, QUO_COST_HDR.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-03-17
        public List<QUO_COST_DET> GetCostSheetDetailBySeq(Int32 seqNumber)
        {
            List<QUO_COST_DET> result = new List<QUO_COST_DET>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = seqNumber;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_QUO_DET_BY_SEQ", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<QUO_COST_DET>(_dtResults, QUO_COST_DET.Converter);
            }
            return result;
        }

        //Tharaka 2015-03-23
        public Int32 GETINVOSEQ()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_GETINVOSEQ", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-23
        public Int32 GETRECIPTSEQ()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_GETRECIPTSEQ", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveInvoiceHeader(InvoiceHeader _invoiceHeader)
        {
            OracleParameter[] param = new OracleParameter[71];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_1;
            (param[1] = new OracleParameter("p_anal_10", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_10;
            (param[2] = new OracleParameter("p_anal_11", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_11;
            (param[3] = new OracleParameter("p_anal_12", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_12;
            (param[4] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_2;
            (param[5] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_3;
            (param[6] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_4;
            (param[7] = new OracleParameter("p_anal_5", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_5;
            (param[8] = new OracleParameter("p_anal_6", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_6;
            (param[9] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_7;
            (param[10] = new OracleParameter("p_anal_8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_8;
            (param[11] = new OracleParameter("p_anal_9", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_9;
            (param[12] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_com;
            (param[13] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cre_by;
            (param[14] = new OracleParameter("p_cre_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cre_when;
            (param[15] = new OracleParameter("p_currency", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_currency;
            (param[16] = new OracleParameter("p_cus_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_add1;
            (param[17] = new OracleParameter("p_cus_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_add2;
            (param[18] = new OracleParameter("p_cus_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_cd;
            (param[19] = new OracleParameter("p_cus_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_cus_name;
            (param[20] = new OracleParameter("p_d_cust_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_add1;
            (param[21] = new OracleParameter("p_d_cust_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_add2;
            (param[22] = new OracleParameter("p_d_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_cd;
            (param[23] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_dt;
            (param[24] = new OracleParameter("p_epf_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_epf_rt;
            (param[25] = new OracleParameter("p_esd_rt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_esd_rt;
            (param[26] = new OracleParameter("p_ex_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_ex_rt;
            (param[27] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_no;
            (param[28] = new OracleParameter("p_inv_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_sub_tp;
            (param[29] = new OracleParameter("p_inv_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_tp;
            (param[30] = new OracleParameter("p_is_acc_upload", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_acc_upload;
            (param[31] = new OracleParameter("p_man_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_man_cd;
            (param[32] = new OracleParameter("p_man_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_man_ref;
            (param[33] = new OracleParameter("p_manual", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_manual;
            (param[34] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_mod_by;
            (param[35] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_mod_when;
            (param[36] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_pc;
            (param[37] = new OracleParameter("p_pdi_req", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_pdi_req;
            (param[38] = new OracleParameter("p_ref_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_ref_doc;
            (param[39] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_remarks;
            (param[40] = new OracleParameter("p_sales_chn_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_chn_cd;
            (param[41] = new OracleParameter("p_sales_chn_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_chn_man;
            (param[42] = new OracleParameter("p_sales_ex_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_ex_cd;
            (param[43] = new OracleParameter("p_sales_region_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_region_cd;
            (param[44] = new OracleParameter("p_sales_region_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_region_man;
            (param[45] = new OracleParameter("p_sales_sbu_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_sbu_cd;
            (param[46] = new OracleParameter("p_sales_sbu_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_sbu_man;
            (param[47] = new OracleParameter("p_sales_str_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_str_cd;
            (param[48] = new OracleParameter("p_sales_zone_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_zone_cd;
            (param[49] = new OracleParameter("p_sales_zone_man", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_sales_zone_man;
            (param[50] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_seq_no;
            (param[51] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_session_id;
            (param[52] = new OracleParameter("p_structure_seq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_structure_seq;
            (param[53] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_stus;
            (param[54] = new OracleParameter("p_town_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_town_cd;
            (param[55] = new OracleParameter("p_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_tp;
            (param[56] = new OracleParameter("p_wht_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_wht_rt;
            (param[57] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_direct ? 1 : 0;
            (param[58] = new OracleParameter("p_tax_inv", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_tax_inv;
            (param[59] = new OracleParameter("p_grup_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_grup_cd;
            (param[60] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_acc_no;
            (param[61] = new OracleParameter("p_tax_exempted", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_tax_exempted;
            (param[62] = new OracleParameter("p_is_svat", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_svat;
            (param[63] = new OracleParameter("p_fin_chrg", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_fin_chrg;
            (param[64] = new OracleParameter("p_del_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_del_loc;
            (param[65] = new OracleParameter("p_grn_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_grn_com;
            (param[66] = new OracleParameter("p_grn_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_grn_loc;
            (param[67] = new OracleParameter("p_is_grn", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_grn;
            (param[68] = new OracleParameter("p_d_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_d_cust_name;//Edit by Chamal 27/05/2013
            (param[69] = new OracleParameter("p_is_dayend", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_is_dayend;//Edit by Prabhath on  20/06/2013
            param[70] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_SAVE_TBSSATHDR", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveInvoiceItem(InvoiceItem _invoiceItem)
        {
            OracleParameter[] param = new OracleParameter[45];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_alt_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_alt_itm_cd;
            (param[1] = new OracleParameter("p_alt_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_alt_itm_desc;
            (param[2] = new OracleParameter("p_comm_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_comm_amt;
            (param[3] = new OracleParameter("p_disc_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_disc_amt;
            (param[4] = new OracleParameter("p_disc_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_disc_rt;
            (param[5] = new OracleParameter("p_do_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_do_qty;
            (param[6] = new OracleParameter("p_fws_ignore_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_fws_ignore_qty;
            (param[7] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_inv_no;
            (param[8] = new OracleParameter("p_is_promo", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_is_promo;
            (param[9] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_cd;
            (param[10] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_line;
            (param[11] = new OracleParameter("p_itm_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_seq;
            (param[12] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_stus;
            (param[13] = new OracleParameter("p_itm_tax_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_tax_amt;
            (param[14] = new OracleParameter("p_itm_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_itm_tp;
            (param[15] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_job_line;
            (param[16] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_job_no;
            (param[17] = new OracleParameter("p_merge_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_merge_itm;
            (param[18] = new OracleParameter("p_outlet_dept", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_outlet_dept;
            (param[19] = new OracleParameter("p_pb_lvl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_pb_lvl;
            (param[20] = new OracleParameter("p_pb_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_pb_price;
            (param[21] = new OracleParameter("p_pbook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_pbook;
            (param[22] = new OracleParameter("p_print_stus", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_print_stus;
            (param[23] = new OracleParameter("p_promo_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_promo_cd;
            (param[24] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_qty;
            (param[25] = new OracleParameter("p_res_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_res_line_no;
            (param[26] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_res_no;
            (param[27] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_seq;
            (param[28] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_seq_no;
            (param[29] = new OracleParameter("p_srn_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_srn_qty;
            (param[30] = new OracleParameter("p_tot_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_tot_amt;
            (param[31] = new OracleParameter("p_unit_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = (_invoiceItem.Sad_unit_rt * _invoiceItem.Sad_qty);
            (param[32] = new OracleParameter("p_unit_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_unit_rt;
            (param[33] = new OracleParameter("p_uom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_uom;
            (param[34] = new OracleParameter("p_warr_based", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_warr_based;
            (param[35] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_warr_period;
            (param[36] = new OracleParameter("p_warr_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_warr_remarks;
            (param[37] = new OracleParameter("p_sad_isapp", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_isapp;
            (param[38] = new OracleParameter("p_sad_iscovernote", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_iscovernote;
            (param[39] = new OracleParameter("p_dis_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_dis_seq;//Added by Prabhath on 20/sp_save_sathdr06/2013
            (param[40] = new OracleParameter("p_dis_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_dis_line;//Added by Prabhath on 20/06/2013
            (param[41] = new OracleParameter("p_dis_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.Sad_dis_type;//Added by Prabhath on 20/06/2013

            (param[42] = new OracleParameter("P_SII_CURR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItem.SII_CURR;
            (param[43] = new OracleParameter("P_SII_EX_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItem.SII_EX_RT;

            param[44] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_SAVE_TBSSATITM", CommandType.StoredProcedure, param);
            return effects;
        }

        public int UpdateTBSPrice(string _item, string _book, string _level, string _customer, string _promotioncode, int _pbseqno, int _lineno)
        {
            // sp_updatepricepicked(  p_item in NVARCHAR2, p_book in NVARCHAR2, p_level in NVARCHAR2, p_customer in NVARCHAR2,  p_promotioncd in NVARCHAR2, p_pbseq in NUMBER, p_pblineno in NUMBER, o_effect out NUMBER)
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[1] = new OracleParameter("p_book", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[2] = new OracleParameter("p_level", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _level;
            (param[3] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[4] = new OracleParameter("p_promotioncd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _promotioncode;
            (param[5] = new OracleParameter("p_pbseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _pbseqno;
            (param[6] = new OracleParameter("p_pblineno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _lineno;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            int effects = (Int16)UpdateRecords("SP_UPDATE_TBSPRICEPICKED", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveTBSSalesItemTax(InvoiceItemTax _invoiceItemTax)
        {
            OracleParameter[] param = new OracleParameter[10];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_inv_no;
            (param[1] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_cd;
            (param[2] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_line;
            (param[3] = new OracleParameter("p_itm_tax_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_tax_amt;
            (param[4] = new OracleParameter("p_itm_tax_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_tax_rt;
            (param[5] = new OracleParameter("p_itm_tax_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_itm_tax_tp;
            (param[6] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_job_line;
            (param[7] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_job_no;
            (param[8] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceItemTax.Satx_seq_no;
            param[9] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_SAVE_TBSSTITMTAX", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveTBSReceiptHeader(RecieptHeader _recieptHeader)
        {
            OracleParameter[] param = new OracleParameter[52];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_acc_no;
            (param[1] = new OracleParameter("p_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_act;
            (param[2] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_1;
            (param[3] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_2;
            (param[4] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_3;
            (param[5] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_4;
            (param[6] = new OracleParameter("p_anal_5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_5;
            (param[7] = new OracleParameter("p_anal_6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_6;
            (param[8] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_7;
            (param[9] = new OracleParameter("p_anal_8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_8;
            (param[10] = new OracleParameter("p_anal_9", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_anal_9;
            (param[11] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_com_cd;
            (param[12] = new OracleParameter("p_comm_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_comm_amt;
            (param[13] = new OracleParameter("p_create_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_create_by;
            (param[14] = new OracleParameter("p_create_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_create_when;
            (param[15] = new OracleParameter("p_currency_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_currency_cd;
            (param[16] = new OracleParameter("p_debtor_add_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_add_1;
            (param[17] = new OracleParameter("p_debtor_add_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_add_2;
            (param[18] = new OracleParameter("p_debtor_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_cd;
            (param[19] = new OracleParameter("p_debtor_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_debtor_name;
            (param[20] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct;
            (param[21] = new OracleParameter("p_direct_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct_deposit_bank_cd;
            (param[22] = new OracleParameter("p_direct_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_direct_deposit_branch;
            (param[23] = new OracleParameter("p_epf_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_epf_rate;
            (param[24] = new OracleParameter("p_esd_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_esd_rate;
            (param[25] = new OracleParameter("p_is_mgr_iss", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_mgr_iss;
            (param[26] = new OracleParameter("p_is_oth_shop", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_oth_shop;
            (param[27] = new OracleParameter("p_is_used", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_used;
            (param[28] = new OracleParameter("p_manual_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_manual_ref_no;
            (param[29] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mob_no;
            (param[30] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_by;
            (param[31] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_when;
            (param[32] = new OracleParameter("p_nic_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_nic_no;
            (param[33] = new OracleParameter("p_oth_sr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_oth_sr;
            (param[34] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_prefix;
            (param[35] = new OracleParameter("p_profit_center_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_profit_center_cd;
            (param[36] = new OracleParameter("p_receipt_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_date;
            (param[37] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_no;
            (param[38] = new OracleParameter("p_receipt_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_receipt_type;
            (param[39] = new OracleParameter("p_ref_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_ref_doc;
            (param[40] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_remarks;
            (param[41] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_seq_no;
            (param[42] = new OracleParameter("p_ser_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_ser_job_no;
            (param[43] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_session_id;
            (param[44] = new OracleParameter("p_tel_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_tel_no;
            (param[45] = new OracleParameter("p_tot_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_tot_settle_amt;
            (param[46] = new OracleParameter("p_uploaded_to_finance", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_uploaded_to_finance;
            (param[47] = new OracleParameter("p_used_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_used_amt;
            (param[48] = new OracleParameter("p_wht_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_wht_rate;
            (param[49] = new OracleParameter("p_is_dayend", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_dayend;//Added by Prabhath on 20/06/2013
            (param[50] = new OracleParameter("p_sir_valid_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.SAR_VALID_TO;
            param[51] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_SAVE_TBSSATRECEIPT", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 SaveTBSReceiptItem(RecieptItem _recieptItem)
        {
            OracleParameter[] param = new OracleParameter[25];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_1;
            (param[1] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_2;
            (param[2] = new OracleParameter("p_anal_3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_3;
            (param[3] = new OracleParameter("p_anal_4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_4;
            (param[4] = new OracleParameter("p_anal_5", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_anal_5;
            (param[5] = new OracleParameter("p_cc_expiry_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_expiry_dt;
            (param[6] = new OracleParameter("p_cc_is_promo", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_is_promo;
            (param[7] = new OracleParameter("p_cc_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_period;
            (param[8] = new OracleParameter("p_cc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_cc_tp;
            (param[9] = new OracleParameter("p_chq_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_chq_bank_cd;
            (param[10] = new OracleParameter("p_chq_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_chq_branch;
            (param[11] = new OracleParameter("p_credit_card_bank", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_credit_card_bank;
            (param[12] = new OracleParameter("p_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_deposit_bank_cd;
            (param[13] = new OracleParameter("p_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_deposit_branch;
            (param[14] = new OracleParameter("p_gv_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sard_gv_issue_dt;
            (param[15] = new OracleParameter("p_gv_issue_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_gv_issue_loc;
            (param[16] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_inv_no;
            (param[17] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_line_no;
            (param[18] = new OracleParameter("p_pay_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_pay_tp;
            (param[19] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_receipt_no;
            (param[20] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_ref_no;
            (param[21] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sard_seq_no;
            (param[22] = new OracleParameter("p_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sard_settle_amt;
            (param[23] = new OracleParameter("p_sim_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sard_sim_ser;
            param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_SAVE_TBSSATRECEIPTITM", CommandType.StoredProcedure, param);
            return effects;
        }

        public int UpdateTBSCreditNoteBalance(string com, string pc, string invoice, decimal amo)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoice;
            (param[3] = new OracleParameter("p_amo", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = amo;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_UPDATE_TBSCREDITNOTE", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int16? CheckTBSSalesNo(string _fnName, string _param, string _paramVal)
        {
            OracleParameter[] param = new OracleParameter[1];
            (param[0] = new OracleParameter(_param, OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _paramVal;
            OracleParameter _outParam = new OracleParameter("p_out", OracleDbType.Int32, null, ParameterDirection.Output);
            Int16 _effect = Convert.ToInt16(ReturnSP_SingleValue(_fnName, CommandType.StoredProcedure, _outParam, param));
            return _effect;
        }

        //Tharaka 2015-03-26
        public Int32 UpdateEnquiryStage(Int32 Stage, String user, String enquiryID, String com, String PC)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;

            (param[0] = new OracleParameter("P_STAUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Stage;
            (param[1] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[2] = new OracleParameter("P_ENQRYID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enquiryID;
            (param[3] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[4] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_UPDATE_ENQRY_STAGE", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-26
        public InvoiceHeader GetInvoiceHeader(string Com, string PC, string InvoiceNum)
        {
            InvoiceHeader result = new InvoiceHeader();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvoiceNum;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_INVOICE_HEADER", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceHeader>(_dtResults, InvoiceHeader.ConverterTours)[0];
            }
            return result;
        }

        //Tharaka 2015-03-26
        public List<InvoiceItem> GetInvoiceDetail(Int32 Seq)
        {
            List<InvoiceItem> result = new List<InvoiceItem>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_INVOICE_DETAILS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceItem>(_dtResults, InvoiceItem.ConverterTours);
            }
            return result;
        }

        //Tharaka 2015-03-26
        public RecieptHeader GetRecieptHeader(Int32 Seq)
        {
            RecieptHeader result = new RecieptHeader();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_RECEIPT_HDR", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<RecieptHeader>(_dtResults, RecieptHeader.ConverterTours)[0];
            }
            return result;
        }

        //Tharaka 2015-03-27
        public List<RecieptItem> GetRecieptItems(Int32 Seq)
        {
            List<RecieptItem> result = new List<RecieptItem>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_RECIPT_ITMS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<RecieptItem>(_dtResults, RecieptItem.ConverterTours);
            }
            return result;
        }

        //Tharaka 2015-03-31
        public int UPDATE_COST_HDR_STATUS(Int32 Stage, Int32 Seq, String com, String pc)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Stage;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_TOUR_UPDATE_COST_HDR_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-16
        public Int32 UPDATE_QUO_COST_HDR(QUO_COST_HDR lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("P_QCH_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_SEQ;
            (param[1] = new OracleParameter("P_QCH_SBU", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_SBU;
            (param[2] = new OracleParameter("P_QCH_COST_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_COST_NO;
            (param[3] = new OracleParameter("P_QCH_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_REF;
            (param[4] = new OracleParameter("P_QCH_TOT_PAX", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_PAX;
            (param[5] = new OracleParameter("P_QCH_TOT_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_COST;
            (param[6] = new OracleParameter("P_QCH_TOT_COST_LOCAL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_COST_LOCAL;
            (param[7] = new OracleParameter("P_QCH_MARKUP", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_MARKUP;
            (param[8] = new OracleParameter("P_QCH_TOT_VALUE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.QCH_TOT_VALUE;
            (param[9] = new OracleParameter("P_QCH_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.QCH_ACT;
            (param[0] = new OracleParameter("P_QCH_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.QCH_MOD_BY;
            (param[1] = new OracleParameter("P_QCH_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.QCH_MOD_DT;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_UPDATE_QUO_COST_HDR", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-03-30
        public SR_AIR_CHARGE GetChargeDetailsByCode(string com, String Cate, string Code, string pc)
        {
            SR_AIR_CHARGE result = new SR_AIR_CHARGE();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            (_para[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (_para[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_SR_AIR_CHARHES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SR_AIR_CHARGE>(_dtResults, SR_AIR_CHARGE.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-04-01
        public SR_TRANS_CHA GetTransferChargeDetailsByCode(string com, String Cate, string Code, string pc)
        {
            SR_TRANS_CHA result = new SR_TRANS_CHA();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            (_para[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (_para[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_TRANS_CHARGES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SR_TRANS_CHA>(_dtResults, SR_TRANS_CHA.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-04-02
        public SR_SER_MISS GetMiscellaneousChargeDetailsByCode(string com, String Cate, string Code, string PC)
        {
            SR_SER_MISS result = new SR_SER_MISS();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            (_para[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (_para[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_SER_MISS_BY_CODE", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SR_SER_MISS>(_dtResults, SR_SER_MISS.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-04-06
        public List<PriceDefinitionRef> GetToursPriceDefByBookAndLevel(string _company, string _book, string _level, string _invoiceType, string _profitCenter)
        {
            List<PriceDefinitionRef> _priceDefinitionRef = new List<PriceDefinitionRef>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pbook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[2] = new OracleParameter("p_plevel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _level;
            (param[3] = new OracleParameter("p_invtype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceType;
            (param[4] = new OracleParameter("p_profit", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitCenter;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblPriceDefinition = QueryDataTable("tblPriceDefinition", "SP_TOURS_GETPRICEDEF_BOOK", CommandType.StoredProcedure, false, param);

            if (_tblPriceDefinition.Rows.Count > 0)
            {
                _priceDefinitionRef = DataTableExtensions.ToGenericList<PriceDefinitionRef>(_tblPriceDefinition, PriceDefinitionRef.ConvertTotalTours);
            }

            return _priceDefinitionRef;
        }

        //Tharaka 2015-04-07
        public List<PriceDetailRef> GetToursPriceDetail(string _book, string _level, string _item, decimal _qty, DateTime _date, string _customer)
        {
            List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_pbook", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _book;
            (param[1] = new OracleParameter("p_plevel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _level;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            (param[4] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
            (param[5] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblPriceDetail = QueryDataTable("tblPriceDetail", "SP_GETTOURSPRICEDETAIL", CommandType.StoredProcedure, false, param);
            if (_tblPriceDetail.Rows.Count > 0)
            {
                _priceDetailRef = DataTableExtensions.ToGenericList<PriceDetailRef>(_tblPriceDetail, PriceDetailRef.ConvertWithPriceTypeTours);
            }

            return _priceDetailRef;
        }

        //Tharaka 2015-04-08
        public List<ComboBoxObject> GetServiceClasses(string com, String Cate)
        {
            List<ComboBoxObject> result = new List<ComboBoxObject>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_SERIVCE_CLASS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    ComboBoxObject oCom = new ComboBoxObject();
                    oCom.Value = _dtResults.Rows[i]["MSC_CD"].ToString();
                    oCom.Text = _dtResults.Rows[i]["MSC_DESC"].ToString();
                    result.Add(oCom);
                }
            }
            return result;
        }

        //Tharaka 2015-04-08
        public List<ComboBoxObject> GetServiceProviders(string com, String Cate)
        {
            List<ComboBoxObject> result = new List<ComboBoxObject>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_SERVICE_PROVIDER", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    ComboBoxObject oCom = new ComboBoxObject();
                    oCom.Value = _dtResults.Rows[i]["MSP_CD"].ToString();
                    oCom.Text = _dtResults.Rows[i]["MSP_DESC"].ToString();
                    result.Add(oCom);
                }
            }
            return result;
        }

        //Tharaka 2015-04-08
        public Int32 SaveAitChageCodes(SR_AIR_CHARGE oItem)
        {
            OracleParameter[] param = new OracleParameter[25];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SAC_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SAC_SEQ;
            (param[1] = new OracleParameter("P_SAC_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_COM;
            (param[2] = new OracleParameter("P_SAC_SCV_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_SCV_BY;
            (param[3] = new OracleParameter("P_SAC_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_CATE;
            (param[4] = new OracleParameter("P_SAC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_CD;
            (param[5] = new OracleParameter("P_SAC_CLS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_CLS;
            (param[6] = new OracleParameter("P_SAC_FRM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SAC_FRM_DT;
            (param[7] = new OracleParameter("P_SAC_TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SAC_TO_DT;
            (param[8] = new OracleParameter("P_SAC_TIC_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_TIC_DESC;
            (param[9] = new OracleParameter("P_SAC_ADD_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_ADD_DESC;
            (param[10] = new OracleParameter("P_SAC_FROM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_FROM;
            (param[11] = new OracleParameter("P_SAC_TO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_TO;
            (param[12] = new OracleParameter("P_SAC_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.SAC_RT;
            (param[13] = new OracleParameter("P_SAC_CUR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_CUR;
            (param[14] = new OracleParameter("P_SAC_IS_CHILD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SAC_IS_CHILD;
            (param[15] = new OracleParameter("P_SAC_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_TP;
            (param[16] = new OracleParameter("P_SAC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_CRE_BY;
            (param[17] = new OracleParameter("P_SAC_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SAC_CRE_DT;
            (param[18] = new OracleParameter("P_SAC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_MOD_BY;
            (param[19] = new OracleParameter("P_SAC_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SAC_MOD_DT;
            (param[20] = new OracleParameter("P_SAC_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SAC_ACT;
            (param[21] = new OracleParameter("P_SAC_TAX_RT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SAC_TAX_RT;
            (param[22] = new OracleParameter("P_SAC_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_PC;
            (param[23] = new OracleParameter("P_SAC_TCKT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SAC_TCKT_TP;
            param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_AIR_CHARGE", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-04-09
        public Int32 SaveTrasportChageCodes(SR_TRANS_CHA oItem)
        {
            OracleParameter[] param = new OracleParameter[26];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STC_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.STC_SEQ;
            (param[1] = new OracleParameter("P_STC_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_COM;
            (param[2] = new OracleParameter("P_STC_SER_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_SER_BY;
            (param[3] = new OracleParameter("P_STC_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_CATE;
            (param[4] = new OracleParameter("P_STC_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_CD;
            (param[5] = new OracleParameter("P_STC_FRM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.STC_FRM_DT;
            (param[6] = new OracleParameter("P_STC_TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.STC_TO_DT;
            (param[7] = new OracleParameter("P_STC_FRM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_FRM;
            (param[8] = new OracleParameter("P_STC_TO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_TO;
            (param[9] = new OracleParameter("P_STC_CLS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_CLS;
            (param[10] = new OracleParameter("P_STC_VEH_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_VEH_TP;
            (param[11] = new OracleParameter("P_STC_FRM_KM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.STC_FRM_KM;
            (param[12] = new OracleParameter("P_STC_TO_KM", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.STC_TO_KM;
            (param[13] = new OracleParameter("P_STC_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.STC_RT;
            (param[14] = new OracleParameter("P_STC_AD_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.STC_AD_RT;
            (param[15] = new OracleParameter("P_STC_RT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_RT_TP;
            (param[16] = new OracleParameter("P_STC_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_CRE_BY;
            (param[17] = new OracleParameter("P_STC_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.STC_CRE_DT;
            (param[18] = new OracleParameter("P_STC_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_MOD_BY;
            (param[19] = new OracleParameter("P_STC_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.STC_MOD_DT;
            (param[20] = new OracleParameter("P_STC_CURR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_CURR;
            (param[21] = new OracleParameter("P_STC_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_DESC;
            (param[22] = new OracleParameter("P_STC_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.STC_ACT;
            (param[23] = new OracleParameter("P_STC_TAX_RT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.STC_TAX_RT;
            (param[24] = new OracleParameter("P_STC_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.STC_PC;
            param[25] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_TRAS_CHA", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-04-09
        public Int32 SaveMiscellaneousChageCodes(SR_SER_MISS oItem)
        {
            OracleParameter[] param = new OracleParameter[21];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SSM_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SSM_SEQ;
            (param[1] = new OracleParameter("P_SSM_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_COM;
            (param[2] = new OracleParameter("P_SSM_SER_PRO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_SER_PRO;
            (param[3] = new OracleParameter("P_SSM_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_CATE;
            (param[4] = new OracleParameter("P_SSM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_CD;
            (param[5] = new OracleParameter("P_SSM_FRM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SSM_FRM_DT;
            (param[6] = new OracleParameter("P_SSM_TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SSM_TO_DT;
            (param[7] = new OracleParameter("P_SSM_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.SSM_RT;
            (param[8] = new OracleParameter("P_SSM_CUR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_CUR;
            (param[9] = new OracleParameter("P_SSM_RT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_RT_TP;
            (param[10] = new OracleParameter("P_SSM_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_CRE_BY;
            (param[11] = new OracleParameter("P_SSM_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SSM_CRE_DT;
            (param[12] = new OracleParameter("P_SSM_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_MOD_BY;
            (param[13] = new OracleParameter("P_SSM_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = oItem.SSM_MOD_DT;
            (param[14] = new OracleParameter("P_SSM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_DESC;
            (param[15] = new OracleParameter("P_SSM_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SSM_ACT;
            (param[16] = new OracleParameter("P_SSM_TAX_RT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oItem.SSM_TAX_RT;
            (param[17] = new OracleParameter("P_SSM_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_PC;

            (param[18] = new OracleParameter("P_SSM_PARENT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oItem.SSM_PARENT_CD;
            (param[19] = new OracleParameter("P_SSM_PERDAY_RTE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = oItem.SSM_PERDAY_RTE;
            
            param[20] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_SER_MISS", CommandType.StoredProcedure, param);
            return effects;
        }

        public DataTable Get_CostingFormat(string costNumber)
        {
            //List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();

            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("QUO_COST_HDR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = costNumber;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //DataTable _dtResults = QueryDataTable("tblJobs", "sp_costingformat", CommandType.StoredProcedure, false, _para);
            //if (_dtResults.Rows.Count > 0)
            //{
            //    result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterDetail);
            //}
            DataTable result = QueryDataTable("tblJobs", "sp_costingformat", CommandType.StoredProcedure, false, _para);

            return result;
        }

        //Tharaka 2015-05-27
        public RecieptHeaderTBS GetReceiptHeaderTBS(string _com, string _pc, string _doc)
        {
            RecieptHeaderTBS _ReceiptList = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblReceiptHeader", "SP_GETRECEIPTHEADERTBS", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                _ReceiptList = DataTableExtensions.ToGenericList<RecieptHeaderTBS>(_dtResults, RecieptHeaderTBS.ConvertTotal)[0];
            }
            return _ReceiptList;
        }

        //Tharaka 2015-05-28
        public List<ComboBoxObject> GET_TOUR_PACKAGE_TYPES()
        {
            List<ComboBoxObject> result = new List<ComboBoxObject>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_TOUR_PACKAGE_TYPES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    ComboBoxObject oCom = new ComboBoxObject();
                    oCom.Value = _dtResults.Rows[i]["PKT_CODE"].ToString();
                    oCom.Text = _dtResults.Rows[i]["PKT_DESC"].ToString();
                    result.Add(oCom);
                }
            }
            return result;
        }

        //Tharaka 2015-05-28
        public Int32 UPDATE_INVOICE_STATUS(string status, string user, string com, string pc, string invoice)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = status;
            (param[1] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[2] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[4] = new OracleParameter("P_INVOICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoice;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_UPDATE_INVOICE_STATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-01
        public List<Ref_Title> GET_REF_TITLE()
        {
            List<Ref_Title> result = new List<Ref_Title>();

            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("Ref_Title", "get_ref_title", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Ref_Title>(_dtResults, Ref_Title.Converter);
            }
            return result;
        }

        //Pemil 2015-06-03
        public List<Mst_empcate> Get_mst_empcate()
        {
            List<Mst_empcate> result = new List<Mst_empcate>();

            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_empcate", "sp_tour_get_mst_empcate", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<Mst_empcate>(_dtResults, Mst_empcate.Converter);
            }
            return result;
        }

        //Pemil 2015-06-03
        public List<MST_VEH_TP> Get_mst_veh_tp()
        {
            List<MST_VEH_TP> result = new List<MST_VEH_TP>();

            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("MST_VEH_TP", "sp_tour_get_mst_veh_tp", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_VEH_TP>(_dtResults, MST_VEH_TP.Converter);
            }
            return result;
        }

        //Pemil 2015-06-03
        public DataTable Get_mst_profit_center(string com)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("MST_VEH_TP", "sp_tour_get_mst_profit_center", CommandType.StoredProcedure, false, _para);

            return _dtResults;
        }

        //Pemil 2015-06-03
        public Int32 SaveEmployee(MST_EMPLOYEE_TBS mst_employee_tbs)
        {
            OracleParameter[] param = new OracleParameter[26];
            Int32 effects = 0;
            (param[0] = new OracleParameter("MEMP_TITLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TITLE;
            (param[1] = new OracleParameter("MEMP_FIRST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_FIRST_NAME;
            (param[2] = new OracleParameter("MEMP_LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LAST_NAME;
            (param[3] = new OracleParameter("MEMP_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_EPF;
            (param[4] = new OracleParameter("MEMP_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_NIC;
            (param[5] = new OracleParameter("MEMP_DOB", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOB;
            (param[6] = new OracleParameter("MEMP_DOJ", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOJ;
            (param[7] = new OracleParameter("MEMP_LIVING_ADD_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_1;
            (param[8] = new OracleParameter("MEMP_LIVING_ADD_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_2;
            (param[9] = new OracleParameter("MEMP_LIVING_ADD_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_3;
            (param[10] = new OracleParameter("MEMP_TEL_HOME_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TEL_HOME_NO;
            (param[11] = new OracleParameter("MEMP_MOBI_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_MOBI_NO;
            (param[12] = new OracleParameter("MEMP_CON_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER;
            (param[13] = new OracleParameter("MEMP_CON_PER_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER_MOB;
            (param[14] = new OracleParameter("MEMP_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ACT;
            (param[15] = new OracleParameter("MEMP_CAT_SUBCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_SUBCD;
            (param[16] = new OracleParameter("MEMP_CAT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_CD;
            (param[17] = new OracleParameter("MEMP_TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TOU_LIC;
            (param[18] = new OracleParameter("MEMP_LIC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_NO;
            (param[19] = new OracleParameter("MEMP_LIC_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_CAT;
            (param[20] = new OracleParameter("MEMP_LIC_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_EXDT;
            (param[21] = new OracleParameter("MEMP_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_COM_CD;
            (param[22] = new OracleParameter("MEMP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CD;
            (param[23] = new OracleParameter("MEMP_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_BY;
            (param[24] = new OracleParameter("MEMP_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_DT;
            param[25] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_saveemployee", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-04
        public Int32 UpdateEmployee(MST_EMPLOYEE_TBS mst_employee_tbs)
        {
            OracleParameter[] param = new OracleParameter[26];
            Int32 effects = 0;
            (param[0] = new OracleParameter("TITLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TITLE;
            (param[1] = new OracleParameter("FIRST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_FIRST_NAME;
            (param[2] = new OracleParameter("LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LAST_NAME;
            (param[3] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_EPF;
            (param[4] = new OracleParameter("NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_NIC;
            (param[5] = new OracleParameter("DOB", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOB;
            (param[6] = new OracleParameter("DOJ", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOJ;
            (param[7] = new OracleParameter("LIVING_ADD_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_1;
            (param[8] = new OracleParameter("LIVING_ADD_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_2;
            (param[9] = new OracleParameter("LIVING_ADD_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_3;
            (param[10] = new OracleParameter("TEL_HOME_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TEL_HOME_NO;
            (param[11] = new OracleParameter("MOBI_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_MOBI_NO;
            (param[12] = new OracleParameter("CON_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER;
            (param[13] = new OracleParameter("CON_PER_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER_MOB;
            (param[14] = new OracleParameter("ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ACT;
            (param[15] = new OracleParameter("CAT_SUBCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_SUBCD;
            (param[16] = new OracleParameter("CAT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_CD;
            (param[17] = new OracleParameter("TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TOU_LIC;
            (param[18] = new OracleParameter("LIC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_NO;
            (param[19] = new OracleParameter("LIC_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_CAT;
            (param[20] = new OracleParameter("LIC_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_EXDT;
            (param[21] = new OracleParameter("COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_COM_CD;
            (param[22] = new OracleParameter("CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CD;
            (param[23] = new OracleParameter("CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_BY;
            (param[24] = new OracleParameter("CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_DT;
            param[25] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_updateemployee", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-03
        public Int32 SavePcemp(MST_PCEMP mst_pcemp)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("MPE_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_EPF;
            (param[1] = new OracleParameter("MPE_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_COM;
            (param[2] = new OracleParameter("MPE_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_PC;
            (param[3] = new OracleParameter("MPE_ASSN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_ASSN_DT;
            (param[4] = new OracleParameter("MPE_REP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_REP_CD;
            (param[5] = new OracleParameter("MPE_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_ACT;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_savepcemp", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-03
        public Int32 UpdatePcemp(MST_PCEMP mst_pcemp)
        {
            OracleParameter[] param = new OracleParameter[7];
            Int32 effects = 0;
            (param[0] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_EPF;
            (param[1] = new OracleParameter("COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_COM;
            (param[2] = new OracleParameter("PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_PC;
            (param[3] = new OracleParameter("ASSN_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_ASSN_DT;
            (param[4] = new OracleParameter("REP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_REP_CD;
            (param[5] = new OracleParameter("ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_pcemp.MPE_ACT;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_updatepcemp ", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-04
        public MST_EMPLOYEE_TBS Get_mst_employee(string memp_epf)
        {
            MST_EMPLOYEE_TBS employee = new MST_EMPLOYEE_TBS();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = memp_epf;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "sp_tour_get_employee", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                employee = DataTableExtensions.ToGenericList<MST_EMPLOYEE_TBS>(_dtResults, MST_EMPLOYEE_TBS.Converter)[0];
            }
            return employee;
        }

        //Pemil 2015-06-04
        public List<MST_PCEMP> Get_mst_pcemp(string mpe_epf)
        {
            List<MST_PCEMP> employee = new List<MST_PCEMP>();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mpe_epf;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_pcemp", "sp_tour_get_pcemp", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                employee = DataTableExtensions.ToGenericList<MST_PCEMP>(_dtResults, MST_PCEMP.Converter);
            }
            return employee;
        }

        //Tharaka 2015-06-03
        public List<ComboBoxObject> GET_ALL_TOWN_FOR_COMBO()
        {
            List<ComboBoxObject> result = new List<ComboBoxObject>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_ALL_TOWN_FOR_COMBO", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    ComboBoxObject oCom = new ComboBoxObject();
                    oCom.Value = _dtResults.Rows[i]["MT_CD"].ToString();
                    oCom.Text = _dtResults.Rows[i]["MT_DESC"].ToString();
                    result.Add(oCom);
                }
            }
            return result;
        }

        //Tharaka 2015-06-03
        public List<ComboBoxObject> GET_ALL_VEHICLE_FOR_COMBO()
        {
            List<ComboBoxObject> result = new List<ComboBoxObject>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_ALL_VEHICLE_FOR_COMBO", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                for (int i = 0; i < _dtResults.Rows.Count; i++)
                {
                    ComboBoxObject oCom = new ComboBoxObject();
                    oCom.Value = _dtResults.Rows[i]["MVT_CD"].ToString();
                    oCom.Text = _dtResults.Rows[i]["MVT_DESC"].ToString();
                    result.Add(oCom);
                }
            }
            return result;
        }

        //Pemil 2015-06-05
        public DataTable Get_gen_cust_enq(string com, string PC, string enq_tp, string fleet, string driver, string cus_cd, DateTime? fromDate, DateTime? toDate)
        {
            OracleParameter[] _para = new OracleParameter[9];
            (_para[0] = new OracleParameter("COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("ENQ_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enq_tp;
            (_para[3] = new OracleParameter("FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet;
            (_para[4] = new OracleParameter("DRIVER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driver;
            (_para[5] = new OracleParameter("CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cus_cd;
            (_para[6] = new OracleParameter("EXPECT_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromDate;
            (_para[7] = new OracleParameter("RET_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDate;
            _para[8] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable result = QueryDataTable("tblJobs", "sp_tour_get_gen_cust_enq", CommandType.StoredProcedure, false, _para);
            return result;
        }

        //Tharaka 2015-06-06
        public MST_EMPLOYEE_TBS GetEmployeeByComPC(String com, String PC, String EPF)
        {
            MST_EMPLOYEE_TBS employee = new MST_EMPLOYEE_TBS();

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (param[2] = new OracleParameter("P_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EPF;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "SP_TOURS_GET_EMP_COM_PC_EPF", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                employee = DataTableExtensions.ToGenericList<MST_EMPLOYEE_TBS>(_dtResults, MST_EMPLOYEE_TBS.Converter)[0];
            }
            return employee;
        }

        //Tharaka 2015-06-08
        public Int32 UpdateTourReceipt(string _invoicno, string _receiptno, Int32 _invoiceseqno, Int32 _receiptseqno)
        {
            //CREATE OR REPLACE PROCEDURE sp_update_invoice_receipt_no (p_invoiceno in NVARCHAR2,p_recieptno in NVARCHAR2,p_invoiceseqno in NUMBER,p_recieptseqno in NUMBER,o_effect out NUMBER )
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_invoiceno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoicno;
            (param[1] = new OracleParameter("p_recieptno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _receiptno;
            (param[2] = new OracleParameter("p_invoiceseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceseqno;
            (param[3] = new OracleParameter("p_recieptseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _receiptseqno;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("SP_TOURS_UPDT_RECEIPT", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-08
        public Int32 Check_Employeeepf(string epf)
        {
            OracleParameter[] param = new OracleParameter[2];
            Int32 effects = 0;
            (param[0] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = epf;
            param[1] = new OracleParameter("C_OUT", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_tour_check_employeeepf", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-09
        public DataTable Get_triprequest(string enq_id)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enq_id;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable result = QueryDataTable("TripRequestSheet", "sp_tour_get_triprequest", CommandType.StoredProcedure, false, _para);
            return result;

        }

        //Pemil 2015-06-12
        public DataTable Get_tour_searchreceipttype(string com, Int32 is_refund)
        {
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("IS_REFUND", OracleDbType.Int32, null, ParameterDirection.Input)).Value = is_refund;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable result = QueryDataTable("mst_rec_tp", "sp_get_tour_searchreceipttype", CommandType.StoredProcedure, false, _para);
            return result;
        }

        //Tharaka 2015-06-12
        public Int32 GETTOURLOGSEQ()
        {
            OracleParameter[] param = new OracleParameter[1];
            Int32 effects = 0;

            param[0] = new OracleParameter("o_serialid", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_GETTOURLOGSEQ", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-12
        public Int32 SAVE_LOGSHEETHEADER(TR_LOGSHEET_HDR lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[29];
            (param[0] = new OracleParameter("P_TLH_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLH_SEQ;
            (param[1] = new OracleParameter("P_TLH_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_COM;
            (param[2] = new OracleParameter("P_TLH_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_PC;
            (param[3] = new OracleParameter("P_TLH_LOG_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_LOG_NO;
            (param[4] = new OracleParameter("P_TLH_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.TLH_DT;
            (param[5] = new OracleParameter("P_TLH_REQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_REQ_NO;
            (param[6] = new OracleParameter("P_TLH_ST_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.TLH_ST_DT;
            (param[7] = new OracleParameter("P_TLH_ED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.TLH_ED_DT;
            (param[8] = new OracleParameter("P_TLH_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_CUS_CD;
            (param[9] = new OracleParameter("P_TLH_DRI_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_DRI_CD;
            (param[10] = new OracleParameter("P_TLH_GUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_GUST;
            (param[11] = new OracleParameter("P_TLH_FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_FLEET;
            (param[12] = new OracleParameter("P_TLH_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_RMK;
            (param[13] = new OracleParameter("P_TLH_INV_MIL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLH_INV_MIL;
            (param[14] = new OracleParameter("P_TLH_DRI_MIL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLH_DRI_MIL;
            (param[15] = new OracleParameter("P_TLH_MET_IN", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLH_MET_IN;
            (param[16] = new OracleParameter("P_TLH_MET_OUT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLH_MET_OUT;
            (param[17] = new OracleParameter("P_TLH_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_CRE_BY;
            (param[18] = new OracleParameter("P_TLH_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.TLH_CRE_DT;
            (param[19] = new OracleParameter("P_TLH_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_MOD_BY;
            (param[20] = new OracleParameter("P_TLH_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.TLH_MOD_DT;
            (param[21] = new OracleParameter("P_TLH_ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_ANAL1;
            (param[22] = new OracleParameter("P_TLH_ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_ANAL2;
            (param[23] = new OracleParameter("P_TLH_ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLH_ANAL3;
            (param[24] = new OracleParameter("P_TLH_ANAL4", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLH_ANAL4;
            (param[25] = new OracleParameter("P_TLH_ANAL5", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLH_ANAL5;
            (param[26] = new OracleParameter("P_TLH_PAY_DRI", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLH_PAY_DRI;
            (param[27] = new OracleParameter("P_TLH_INV", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLH_INV;
            param[28] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_LOGSHEETHEADER", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-12
        public Int32 SAVE_LOGSHEETDET(TR_LOGSHEET_DET lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[16];
            (param[0] = new OracleParameter("P_TLD_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLD_SEQ;
            (param[1] = new OracleParameter("P_TLD_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLD_LINE;
            (param[2] = new OracleParameter("P_TLD_CHR_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLD_CHR_CD;
            (param[3] = new OracleParameter("P_TLD_CHR_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLD_CHR_DESC;
            (param[4] = new OracleParameter("P_TLD_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_QTY;
            (param[5] = new OracleParameter("P_TLD_RT_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.TLD_RT_TP;
            (param[6] = new OracleParameter("P_TLD_U_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_U_RT;
            (param[7] = new OracleParameter("P_TLD_U_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_U_AMT;
            (param[8] = new OracleParameter("P_TLD_TAX", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_TAX;
            (param[9] = new OracleParameter("P_TLD_DIS_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_DIS_RT;
            (param[10] = new OracleParameter("P_TLD_DIS_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_DIS_AMT;
            (param[11] = new OracleParameter("P_TLD_TOT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = lst.TLD_TOT;
            (param[12] = new OracleParameter("P_TLD_IS_CUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLD_IS_CUS;
            (param[13] = new OracleParameter("P_TLD_IS_DRI", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLD_IS_DRI;
            (param[14] = new OracleParameter("P_TLD_IS_ADD", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.TLD_IS_ADD;
            param[15] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_SAVE_LOGSHEETDET", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-13
        public TR_LOGSHEET_HDR GetLogSheetHeader(String com, String PC, String LOG)
        {
            TR_LOGSHEET_HDR result = new TR_LOGSHEET_HDR();

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (param[2] = new OracleParameter("P_LOG", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = LOG;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "SP_TOUR_GET_LOGSHEET_HDR", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<TR_LOGSHEET_HDR>(_dtResults, TR_LOGSHEET_HDR.Converter)[0];
            }
            return result;
        }

        //Tharaka 2015-06-13
        public List<TR_LOGSHEET_DET> GetLogSheetDetails(Int32 seqNum)
        {
            List<TR_LOGSHEET_DET> result = new List<TR_LOGSHEET_DET>();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seqNum;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "SP_TOUR_GET_LOGSHEETDET_BY_SEQ", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<TR_LOGSHEET_DET>(_dtResults, TR_LOGSHEET_DET.Converter);
            }
            return result;
        }

        //Pemil 2015-06-13
        public DataTable Get_tour_logsheet(string com, string pc, string dri_cd, DateTime from_dt, DateTime to_dt)
        {
            OracleParameter[] para = new OracleParameter[6];
            (para[0] = new OracleParameter("COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (para[1] = new OracleParameter("PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (para[2] = new OracleParameter("DRI_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dri_cd;
            (para[3] = new OracleParameter("FROM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = from_dt;
            (para[4] = new OracleParameter("TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = to_dt;
            para[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable result = QueryDataTable("TR_LOGSHEET_DET", "sp_tour_get_logsheet", CommandType.StoredProcedure, false, para);
            return result;
        }

        //Pemil 2015-06-16
        public Int32 SaveReceiptHeaderTBS(RecieptHeaderTBS _recieptHeader)
        {
            OracleParameter[] param = new OracleParameter[52];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_acc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_acc_no;
            (param[1] = new OracleParameter("p_act", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_act;
            (param[2] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_1;
            (param[3] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_2;
            (param[4] = new OracleParameter("p_anal_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_3;
            (param[5] = new OracleParameter("p_anal_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_4;
            (param[6] = new OracleParameter("p_anal_5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_5;
            (param[7] = new OracleParameter("p_anal_6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_6;
            (param[8] = new OracleParameter("p_anal_7", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_7;
            (param[9] = new OracleParameter("p_anal_8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_8;
            (param[10] = new OracleParameter("p_anal_9", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_anal_9;
            (param[11] = new OracleParameter("p_com_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_com_cd;
            (param[12] = new OracleParameter("p_comm_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_comm_amt;
            (param[13] = new OracleParameter("p_create_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_create_by;
            (param[14] = new OracleParameter("p_create_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_create_when;
            (param[15] = new OracleParameter("p_currency_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_currency_cd;
            (param[16] = new OracleParameter("p_debtor_add_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_debtor_add_1;
            (param[17] = new OracleParameter("p_debtor_add_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_debtor_add_2;
            (param[18] = new OracleParameter("p_debtor_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_debtor_cd;
            (param[19] = new OracleParameter("p_debtor_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_debtor_name;
            (param[20] = new OracleParameter("p_direct", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_direct;
            (param[21] = new OracleParameter("p_direct_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_direct_deposit_bank_cd;
            (param[22] = new OracleParameter("p_direct_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_direct_deposit_branch;
            (param[23] = new OracleParameter("p_epf_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_epf_rate;
            (param[24] = new OracleParameter("p_esd_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_esd_rate;
            (param[25] = new OracleParameter("p_is_mgr_iss", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_is_mgr_iss;
            (param[26] = new OracleParameter("p_is_oth_shop", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_is_oth_shop;
            (param[27] = new OracleParameter("p_is_used", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_is_used;
            (param[28] = new OracleParameter("p_manual_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_manual_ref_no;
            (param[29] = new OracleParameter("p_mob_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_mob_no;
            (param[30] = new OracleParameter("p_mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_mod_by;
            (param[31] = new OracleParameter("p_mod_when", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_mod_when;
            (param[32] = new OracleParameter("p_nic_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_nic_no;
            (param[33] = new OracleParameter("p_oth_sr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_oth_sr;
            (param[34] = new OracleParameter("p_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_prefix;
            (param[35] = new OracleParameter("p_profit_center_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_profit_center_cd;
            (param[36] = new OracleParameter("p_receipt_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_receipt_date;
            (param[37] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_receipt_no;
            (param[38] = new OracleParameter("p_receipt_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_receipt_type;
            (param[39] = new OracleParameter("p_ref_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_ref_doc;
            (param[40] = new OracleParameter("p_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_remarks;
            (param[41] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_seq_no;
            (param[42] = new OracleParameter("p_ser_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_ser_job_no;
            (param[43] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_session_id;
            (param[44] = new OracleParameter("p_tel_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_tel_no;
            (param[45] = new OracleParameter("p_tot_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_tot_settle_amt;
            (param[46] = new OracleParameter("p_uploaded_to_finance", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_uploaded_to_finance;
            (param[47] = new OracleParameter("p_used_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_used_amt;
            (param[48] = new OracleParameter("p_wht_rate", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_wht_rate;
            (param[49] = new OracleParameter("p_is_dayend", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_is_dayend;//Added by Prabhath on 20/06/2013
            (param[50] = new OracleParameter("p_sar_valid_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptHeader.Sir_VALID_TO;
            param[51] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_save_satreceipttbs", CommandType.StoredProcedure, param);
            return effects;

        }

        //Pemil 2015-06-16
        public Int32 SaveReceiptItemTBS(RecieptItemTBS _recieptItem)
        {
            OracleParameter[] param = new OracleParameter[25];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_anal_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_anal_1;
            (param[1] = new OracleParameter("p_anal_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_anal_2;
            (param[2] = new OracleParameter("p_anal_3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sird_anal_3;
            (param[3] = new OracleParameter("p_anal_4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sird_anal_4;
            (param[4] = new OracleParameter("p_anal_5", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sird_anal_5;
            (param[5] = new OracleParameter("p_cc_expiry_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sird_cc_expiry_dt;
            (param[6] = new OracleParameter("p_cc_is_promo", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _recieptItem.Sird_cc_is_promo;
            (param[7] = new OracleParameter("p_cc_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sird_cc_period;
            (param[8] = new OracleParameter("p_cc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_cc_tp;
            (param[9] = new OracleParameter("p_chq_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_chq_bank_cd;
            (param[10] = new OracleParameter("p_chq_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_chq_branch;
            (param[11] = new OracleParameter("p_credit_card_bank", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_credit_card_bank;
            (param[12] = new OracleParameter("p_deposit_bank_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_deposit_bank_cd;
            (param[13] = new OracleParameter("p_deposit_branch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_deposit_branch;
            (param[14] = new OracleParameter("p_gv_issue_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _recieptItem.Sird_gv_issue_dt;
            (param[15] = new OracleParameter("p_gv_issue_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_gv_issue_loc;
            (param[16] = new OracleParameter("p_inv_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_inv_no;
            (param[17] = new OracleParameter("p_line_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sird_line_no;
            (param[18] = new OracleParameter("p_pay_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_pay_tp;
            (param[19] = new OracleParameter("p_receipt_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_receipt_no;
            (param[20] = new OracleParameter("p_ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_ref_no;
            (param[21] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptItem.Sird_seq_no;
            (param[22] = new OracleParameter("p_settle_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _recieptItem.Sird_settle_amt;
            (param[23] = new OracleParameter("p_sim_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptItem.Sird_sim_ser;
            param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int16)UpdateRecords("sp_save_satreceiptitmtbs", CommandType.StoredProcedure, param);
            return effects;
        }

        //Pemil 2015-06-16
        public Int32 SaveSR_PAY_LOGTBS(SR_PAY_LOG sr_pay_log)
        {
            OracleParameter[] param = new OracleParameter[14];
            Int32 effects = 0;

            (param[0] = new OracleParameter("seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_SEQ;
            (param[1] = new OracleParameter("com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_COM;
            (param[2] = new OracleParameter("pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_PC;
            (param[3] = new OracleParameter("cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_CUS;
            (param[4] = new OracleParameter("dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_DT;
            (param[5] = new OracleParameter("ref_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_REF_NO;
            (param[6] = new OracleParameter("amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_AMT;
            (param[7] = new OracleParameter("act", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_ACT;
            (param[8] = new OracleParameter("cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_CRE_BY;
            (param[9] = new OracleParameter("cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_CRE_DT;
            (param[10] = new OracleParameter("mod_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_MOD_BY;
            (param[11] = new OracleParameter("mod_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_MOD_DT;
            (param[12] = new OracleParameter("rec_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = sr_pay_log.SPL_REC_NO;
            param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_save_sr_pay_log", CommandType.StoredProcedure, param);
            return effects;

        }

        //Pemil 2015-06-16
        public Int32 UPDATE_LOGSHEET_HDRTBS(string log_no, Int16 pay_dri)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;

            (param[0] = new OracleParameter("LOG_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = log_no;
            (param[1] = new OracleParameter("PAY_DRI", OracleDbType.Int16, null, ParameterDirection.Input)).Value = pay_dri;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_update_logsheet_hdr", CommandType.StoredProcedure, param);
            return effects;

        }

        //Pemil 2015-06-16
        public Int32 Cancel_paymentTBS(int seq, string log_no, string receipt_no)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;

            (param[0] = new OracleParameter("SEQ", OracleDbType.Int64, null, ParameterDirection.Input)).Value = seq;
            (param[1] = new OracleParameter("LOG_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = log_no;
            (param[2] = new OracleParameter("RECEIPT_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receipt_no;
            param[3] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_cancel_payment", CommandType.StoredProcedure, param);
            return effects;
        }

        //Sahan 15/Jun/2015
        public DataTable SP_TOURS_GET_ALL_OVERLAP_DATES(string P_MFD_VEH_NO, string P_MFD_DRI, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_MFD_VEH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFD_VEH_NO;
            (param[1] = new OracleParameter("P_MFD_DRI", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = P_MFD_DRI;
            (param[2] = new OracleParameter("p_mfd_frm_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_frm_dt;
            (param[3] = new OracleParameter("p_mfd_to_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = p_mfd_to_dt;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("MST_FLEET_DRIVER", "SP_TOURS_GET_ALL_OVERLAP_DATES", CommandType.StoredProcedure, false, param);

        }

        //Pemil 2015-06-17
        public DataTable Get_tour_searchDriverTBS(string com_cd, string ep, string cat_subcd2, string first_name2, string last_name, string nic, string tou_lic)
        {
            OracleParameter[] _para = new OracleParameter[8];
            (_para[0] = new OracleParameter("COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com_cd;
            (_para[1] = new OracleParameter("EP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ep;
            (_para[2] = new OracleParameter("CAT_SUBCD2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cat_subcd2;
            (_para[3] = new OracleParameter("FIRST_NAME2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = first_name2;
            (_para[4] = new OracleParameter("LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = last_name;
            (_para[5] = new OracleParameter("NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = nic;
            (_para[6] = new OracleParameter("TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = tou_lic;
            _para[7] = new OracleParameter("C_Data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable result = QueryDataTable("Driver", "TBS.SP_SearchDriverTBS", CommandType.StoredProcedure, false, _para);
            return result;
        }

        //Pemil 2015-06-18
        public DataTable Get_sr_pay_log(string rec_no)
        {
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("REC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = rec_no;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable result = QueryDataTable("TripRequestSheet", "sp_tour_get_sr_pay_log", CommandType.StoredProcedure, false, _para);
            return result;

        }

        //Tharaka 2015-06-24
        public List<TR_LOGSHEET_DET> GetLogDetailsCustInvoice(String custCode, String Com, DateTime From, DateTime TO, Int32 Status)
        {
            List<TR_LOGSHEET_DET> result = new List<TR_LOGSHEET_DET>();

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (param[1] = new OracleParameter("P_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = custCode;
            (param[2] = new OracleParameter("P_FROM", OracleDbType.Date, null, ParameterDirection.Input)).Value = From;
            (param[3] = new OracleParameter("P_TO", OracleDbType.Date, null, ParameterDirection.Input)).Value = TO;
            (param[4] = new OracleParameter("P_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Status;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "SP_GET_CUSTPAID_LOG_DET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<TR_LOGSHEET_DET>(_dtResults, TR_LOGSHEET_DET.Converter2);
            }
            return result;
        }

        //Tharaka 2015-06-26
        public Int32 UPDATE_LOG_HDR_INVOICE(int seq, int STATUS, string USER)
        {
            OracleParameter[] param = new OracleParameter[4];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_STATUS", OracleDbType.Int64, null, ParameterDirection.Input)).Value = STATUS;
            (param[1] = new OracleParameter("P_USER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = USER;
            (param[2] = new OracleParameter("P_SEQ", OracleDbType.Int64, null, ParameterDirection.Input)).Value = seq;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_TOUR_UPDATE_LOG_HDR_INVOICE", CommandType.StoredProcedure, param);
            return effects;
        }

        //Tharaka 2015-06-30
        public TR_LOGSHEET_HDR GET_LOG_HDR_BY_ENQRY(String enquiryID)
        {
            TR_LOGSHEET_HDR result = new TR_LOGSHEET_HDR();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_ENQUIRY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enquiryID;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "SP_TOUR_GET_LOG_HDR_BY_ENQRY", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<TR_LOGSHEET_HDR>(_dtResults, TR_LOGSHEET_HDR.Converter)[0];
            }
            return result;
        }
        //Rukshan 2016-1-28
        public List<QUO_COST_HDR> GET_COST_PRFITABILITY(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate)
        {
            List<QUO_COST_HDR> result = new List<QUO_COST_HDR>();

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _procenter;
            (param[2] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            (param[3] = new OracleParameter("P_COSTREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _costRef;
            (param[4] = new OracleParameter("P_CUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[5] = new OracleParameter("P_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[6] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[7] = new OracleParameter("P_TODATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            param[8] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "PKG_TOURS_SEARCH.SP_SEARCH_COST_PRFITABILITY", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<QUO_COST_HDR>(_dtResults, QUO_COST_HDR.Converter2);
            }
            return result;
        }

        //Rukshan 2016-1-28
        public List<QUO_COST_HDR> GET_COST_PRFITABILITY_DETAILS(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate)
        {
            List<QUO_COST_HDR> result = new List<QUO_COST_HDR>();

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _procenter;
            (param[2] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            (param[3] = new OracleParameter("P_COSTREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _costRef;
            (param[4] = new OracleParameter("P_CUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[5] = new OracleParameter("P_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[6] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[7] = new OracleParameter("P_TODATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            param[8] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "PKG_TOURS_SEARCH.SP_SEARCH_COST_PRFITA_DET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<QUO_COST_HDR>(_dtResults, QUO_COST_HDR.Converter3);
            }
            return result;
        }

        // Rukshan 29-01-2016
        public DataTable Get_COST_HDR_NO(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;

            //Spliit for Search control type.
            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }

            //Spliit for Search SP parameters.
            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);

            string _CSNO = null;
            string _REF = null;


            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "COST SHEET NO":
                        _CSNO = _searchText;
                        break;

                    case "REF":
                        _REF = _searchText;
                        break;



                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _CSNO = (_CSNO != null) ? (_CSNO.ToUpper() + "%") : null;
            _REF = (_REF != null) ? (_REF.ToUpper() + "%") : null;


            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0];
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1];
            (param[2] = new OracleParameter("P_REF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _REF;
            (param[3] = new OracleParameter("P_COST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _CSNO;

            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblCost", "PKG_TOURS_SEARCH.SP_SEARCH_COST_HDR", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        // Rukshan 29-01-2016
        public DataTable Get_SERVICE_CODE(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;

            //Spliit for Search control type.
            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }

            //Spliit for Search SP parameters.
            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);

            string _CODE = null;
            string _DES = null;


            //Set relavant parameters according to the,search catergory.
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "CODE":
                        _CODE = _searchText;
                        break;

                    case "DESCRIPTION":
                        _DES = _searchText;
                        break;



                    default:
                        break;
                }
            }

            //Modify parameter values for LIKE search.
            _CODE = (_CODE != null) ? (_CODE.ToUpper() + "%") : null;
            _DES = (_DES != null) ? (_DES.ToUpper() + "%") : null;


            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0];
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1];
            (param[2] = new OracleParameter("P_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _CODE;
            (param[3] = new OracleParameter("P_Des", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _DES;

            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //Query Data base.
            _dtResults = QueryDataTable("tblCost", "PKG_TOURS_SEARCH.SP_SEARCH_SERVICE_CODE", CommandType.StoredProcedure, false, param);

            return _dtResults;
        }

        //Rukshan 2016-01-30
        public List<SR_AIR_CHARGE> GetALLChargeDetailsByCode(string com, string Cate, string Code, DateTime date)
        {
            List<SR_AIR_CHARGE> result = new List<SR_AIR_CHARGE>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            (param[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (param[3] = new OracleParameter("P_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "pkg_tours_search.SP_TOUR_GETALL_SR_AIR_CHARHES", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SR_AIR_CHARGE>(_dtResults, SR_AIR_CHARGE.Converter2);
            }
            return result;
        }

        //Tharaka 2015-04-01
        public List<SR_TRANS_CHA> GetAllTransferChargeDetailsByCode(string com, String Cate, string Code, DateTime date)
        {
            List<SR_TRANS_CHA> result = new List<SR_TRANS_CHA>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            (param[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (param[3] = new OracleParameter("P_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "pkg_tours_search.SP_TOUR_GETALL_TRANS_CHARGES", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SR_TRANS_CHA>(_dtResults, SR_TRANS_CHA.Converter2);
            }
            return result;
        }

        //Tharaka 2015-04-02
        public List<SR_SER_MISS> GetALLMiscellaneousChargeDetailsByCode(string com, String Cate, string Code, DateTime date)
        {
            List<SR_SER_MISS> result = new List<SR_SER_MISS>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_CATE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cate;
            (param[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Code;
            (param[3] = new OracleParameter("P_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
            param[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "pkg_tours_search.SP_TOUR_GETALL_MISS_BY_CODE", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SR_SER_MISS>(_dtResults, SR_SER_MISS.Converter2);
            }
            return result;
        }
        /// <summary>
        /// get title list
        /// </summary>
        /// <returns>List<MST_TITLE></returns>
        public List<MST_TITLE> GetTitleList()
        {
            List<MST_TITLE> listTitle = new List<MST_TITLE>();
            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblTitle", "SP_GET_TITLELIST", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                listTitle = DataTableExtensions.ToGenericList<MST_TITLE>(_dtResults, MST_TITLE.Converter);
            }
            return listTitle;
        }

        /* Created on 09/Feb/2016 4:40:04 PM Lakshan*/
        public List<MST_ENQSUBTP> GET_ENQRY_SUB_TP(MST_ENQSUBTP obj)
        {
            List<MST_ENQSUBTP> result = new List<MST_ENQSUBTP>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("p_mest_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = obj.MEST_COM;
            (_para[1] = new OracleParameter("p_mest_tpcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = obj.MEST_TPCD;
            (_para[2] = new OracleParameter("p_mest_stpcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = obj.MEST_STPCD;
            (_para[3] = new OracleParameter("p_mest_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = obj.MEST_DESC;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "sp_get_enq_sub_tp", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ENQSUBTP>(_dtResults, MST_ENQSUBTP.Converter);
            }
            return result;
        }

        public MST_EMPLOYEE_NEW_TBS getMstEmployeeDetails(string empCode)
        {
            MST_EMPLOYEE_NEW_TBS employee = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = empCode;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "sp_tour_get_employee", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                employee = DataTableExtensions.ToGenericList<MST_EMPLOYEE_NEW_TBS>(_dtResults, MST_EMPLOYEE_NEW_TBS.Converter)[0];
            }
            return employee;
        }

        public Int32 SaveEmployeeData(MST_EMPLOYEE_NEW_TBS mst_employee_tbs)
        {
            OracleParameter[] param = new OracleParameter[31];
            Int32 effects = 0;
            (param[0] = new OracleParameter("MEMP_TITLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TITLE;
            (param[1] = new OracleParameter("MEMP_FIRST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_FIRST_NAME;
            (param[2] = new OracleParameter("MEMP_LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LAST_NAME;
            (param[3] = new OracleParameter("MEMP_EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_EPF;
            (param[4] = new OracleParameter("MEMP_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_NIC;
            (param[5] = new OracleParameter("MEMP_DOB", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOB;
            (param[6] = new OracleParameter("MEMP_DOJ", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOJ;
            (param[7] = new OracleParameter("MEMP_LIVING_ADD_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_1;
            (param[8] = new OracleParameter("MEMP_LIVING_ADD_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_2;
            (param[9] = new OracleParameter("MEMP_LIVING_ADD_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_3;
            (param[10] = new OracleParameter("MEMP_TEL_HOME_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TEL_HOME_NO;
            (param[11] = new OracleParameter("MEMP_MOBI_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_MOBI_NO;
            (param[12] = new OracleParameter("MEMP_CON_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER;
            (param[13] = new OracleParameter("MEMP_CON_PER_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER_MOB;
            (param[14] = new OracleParameter("MEMP_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ACT;
            (param[15] = new OracleParameter("MEMP_CAT_SUBCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_SUBCD;
            (param[16] = new OracleParameter("MEMP_CAT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_CD;
            (param[17] = new OracleParameter("MEMP_TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TOU_LIC;
            (param[18] = new OracleParameter("MEMP_LIC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_NO;
            (param[19] = new OracleParameter("MEMP_LIC_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_CAT;
            (param[20] = new OracleParameter("MEMP_LIC_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_EXDT;
            (param[21] = new OracleParameter("MEMP_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_COM_CD;
            (param[22] = new OracleParameter("MEMP_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CD;
            (param[23] = new OracleParameter("MEMP_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_BY;
            (param[24] = new OracleParameter("MEMP_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_DT;
            (param[25] = new OracleParameter("MEMP_EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_EMAIL;
            (param[26] = new OracleParameter("MEMP_ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ANAL1;
            (param[27] = new OracleParameter("ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ANAL2;
            (param[28] = new OracleParameter("ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ANAL3;
            (param[29] = new OracleParameter("MEMP_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_COST;
            param[30] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_saveemployee", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 UpdateEmployeeData(MST_EMPLOYEE_NEW_TBS mst_employee_tbs)
        {
            OracleParameter[] param = new OracleParameter[31];
            Int32 effects = 0;
            (param[0] = new OracleParameter("TITLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TITLE;
            (param[1] = new OracleParameter("FIRST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_FIRST_NAME;
            (param[2] = new OracleParameter("LAST_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LAST_NAME;
            (param[3] = new OracleParameter("EPF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_EPF;
            (param[4] = new OracleParameter("NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_NIC;
            (param[5] = new OracleParameter("DOB", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOB;
            (param[6] = new OracleParameter("DOJ", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_DOJ;
            (param[7] = new OracleParameter("LIVING_ADD_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_1;
            (param[8] = new OracleParameter("LIVING_ADD_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_2;
            (param[9] = new OracleParameter("LIVING_ADD_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIVING_ADD_3;
            (param[10] = new OracleParameter("TEL_HOME_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TEL_HOME_NO;
            (param[11] = new OracleParameter("MOBI_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_MOBI_NO;
            (param[12] = new OracleParameter("CON_PER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER;
            (param[13] = new OracleParameter("CON_PER_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CON_PER_MOB;
            (param[14] = new OracleParameter("ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ACT;
            (param[15] = new OracleParameter("CAT_SUBCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_SUBCD;
            (param[16] = new OracleParameter("CAT_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CAT_CD;
            (param[17] = new OracleParameter("TOU_LIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_TOU_LIC;
            (param[18] = new OracleParameter("LIC_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_NO;
            (param[19] = new OracleParameter("LIC_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_CAT;
            (param[20] = new OracleParameter("LIC_EXDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_LIC_EXDT;
            (param[21] = new OracleParameter("COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_COM_CD;
            (param[22] = new OracleParameter("CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CD;
            (param[23] = new OracleParameter("CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_BY;
            (param[24] = new OracleParameter("CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_CRE_DT;
            (param[25] = new OracleParameter("EMAIL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_EMAIL;
            (param[26] = new OracleParameter("ANAL1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ANAL1;
            (param[27] = new OracleParameter("ANAL2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ANAL2;
            (param[28] = new OracleParameter("ANAL3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_ANAL3;
            (param[29] = new OracleParameter("COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = mst_employee_tbs.MEMP_COST;
            param[30] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_tour_updateemployee", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<mst_fleet_driver> getAllocateVehicles(string driver)
        {
            List<mst_fleet_driver> allocation = new List<mst_fleet_driver>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_MFD_DRI", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driver;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("MST_FLEET_DRIVER", "SP_GET_ALLOCATIONSDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                allocation = DataTableExtensions.ToGenericList<mst_fleet_driver>(_dtResults, mst_fleet_driver.Converter);
            }
            return allocation;
        }
        public List<Mst_Fleet_driver_new> getAllocateVehiclesnew(string driver)
        {
            List<Mst_Fleet_driver_new> allocation = new List<Mst_Fleet_driver_new>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_MFD_DRI", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driver;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("MST_FLEET_DRIVER", "SP_GET_ALLOCATIONSDETAILS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                allocation = DataTableExtensions.ToGenericList<Mst_Fleet_driver_new>(_dtResults, Mst_Fleet_driver_new.Converter);
            }
            return allocation;
        }
        public List<mst_fleet_driver> getAllocateVehiclesByID(string regNo)
        {
            List<mst_fleet_driver> allocation = new List<mst_fleet_driver>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_MFD_VEH_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = regNo;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("MST_FLEET_DRIVER", "SP_GET_ALLOCDATA", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                allocation = DataTableExtensions.ToGenericList<mst_fleet_driver>(_dtResults, mst_fleet_driver.Converter);
            }
            return allocation;
        }

        public string getBankCode(string bankId)
        {
            string code = null;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_bank_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bankId;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblBnkCd", "SP_GET_BANKCDFRMID", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                code = _dtResults.Rows[0]["MBI_CD"].ToString();
            }
            return code;
        }

        public MST_FLEET_NEW getMstFleetDetails(string regNo)
        {
            MST_FLEET_NEW fleets = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("MSTF_REGNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = regNo;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_fleet", "sp_tours_Get_Fleet", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                fleets = DataTableExtensions.ToGenericList<MST_FLEET_NEW>(_dtResults, MST_FLEET_NEW.Converter)[0];
            }
            return fleets;
        }

        //Subodana 2016-02-16
        public List<mst_fleet_alloc> Get_mst_fleet_alloc(string regNo)
        {
            List<mst_fleet_alloc> alloc = new List<mst_fleet_alloc>();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("REGNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = regNo;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_fleet_alloc", "Get_mst_fleet_alloc", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                alloc = DataTableExtensions.ToGenericList<mst_fleet_alloc>(_dtResults, mst_fleet_alloc.Converter);
            }
            return alloc;
        }

        public string getAdvanceRefAmount(string cuscd, string company, string receiptno)
        {
            string amnt = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscd;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("p_recetype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "ADVAN";
            (param[3] = new OracleParameter("p_debcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "CASH";
            (param[4] = new OracleParameter("p_receiptno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = receiptno;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblBnkCd", "SP_GET_ADVANCEREFAMT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                amnt = _dtResults.Rows[0]["SAR_USED_AMT"].ToString();
            }
            return amnt;
        }

        public string getCreditRefAmount(string cuscd, string company, string refNo, string profcen)
        {
            string amnt = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_customer", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscd;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("p_profcen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profcen;
            (param[3] = new OracleParameter("p_refno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = refNo;
            (param[4] = new OracleParameter("p_waveoffval", OracleDbType.Int32, null, ParameterDirection.Input)).Value = vaweOffVal(company);
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblBnkCd", "SP_GET_CREDITNTEREFAMT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                amnt = _dtResults.Rows[0]["CREDIT_AMT"].ToString();
            }
            return amnt;
        }
        public int vaweOffVal(string company)
        {

            int vaweOff = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblProf", "SP_GET_WAVEOFFVAL", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["MC_WAVEOFF_VAL"].ToString() != "")
            {
                vaweOff = Convert.ToInt32(_dtResults.Rows[0]["MC_WAVEOFF_VAL"].ToString());
            }
            return vaweOff;
        }
        //Subodana 2016-02-24
        public Int32 UPDATE_DRIVER_ALLO_STATUS_TO_INACT(Int32 SEQ)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = SEQ;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int16)UpdateRecords("UPDATE_DRI_ALL_STATUS_TO_INACT", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects;
        }
        public Int32 UPDATE_DRIVER_ALLO_STATUS_TO_ACT(Int32 SEQ)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = SEQ;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int16)UpdateRecords("UPDATE_DRI_ALL_STATUS_TO_ACT", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects;
        }
        public Int32 UPDATE_ENQ_STATUS_WITH_REASON(string ENQ_ID, string ENQ)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("GCE_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ENQ_ID;
            (param[1] = new OracleParameter("GCE_ENQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ENQ;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int16)UpdateRecords("UPDATE_ENQ_STATUS_WITH_REASON", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects;
        }
        public Int32 SAVE_GEN_ENQSER(GEN_CUST_ENQSER lst)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[26];
            (param[0] = new OracleParameter("P_GCS_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCS_SEQ;
            (param[1] = new OracleParameter("P_GCS_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCS_LINE;
            (param[2] = new OracleParameter("P_GCS_MAIN_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_MAIN_ID;
            (param[3] = new OracleParameter("P_GCS_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_ENQ_ID;
            (param[4] = new OracleParameter("P_GCS_FAC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_FAC;
            (param[5] = new OracleParameter("P_GCS_SERVICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_SERVICE;
            (param[6] = new OracleParameter("P_GCS_UNITS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCS_UNITS;
            (param[7] = new OracleParameter("P_GCS_SER_PROVIDER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_SER_PROVIDER;
            (param[8] = new OracleParameter("P_GCS_SER_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_SER_COM;
            (param[9] = new OracleParameter("P_GCS_SER_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_SER_PC;
            (param[10] = new OracleParameter("P_GCS_PICK_FRM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_PICK_FRM;
            (param[11] = new OracleParameter("P_GCS_PICK_TN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_PICK_TN;
            (param[12] = new OracleParameter("P_GCS_EXP_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCS_EXP_DT;
            (param[13] = new OracleParameter("P_GCS_EXP_TIME", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCS_EXP_TIME;
            (param[14] = new OracleParameter("P_GCS_DROP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_DROP;
            (param[15] = new OracleParameter("P_GCS_DROP_TN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_DROP_TN;
            (param[16] = new OracleParameter("P_GCS_DROP_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCS_DROP_DT;
            (param[17] = new OracleParameter("P_GCS_DROP_TIME", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCS_DROP_TIME;
            (param[18] = new OracleParameter("P_GCS_VEH_TP", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_VEH_TP;
            (param[19] = new OracleParameter("P_GCS_COMMENT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_COMMENT;
            (param[20] = new OracleParameter("P_GCS_STATUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = lst.GCS_STATUS;
            (param[21] = new OracleParameter("P_GCS_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_CRE_BY;
            (param[22] = new OracleParameter("P_GCS_CRE_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCS_CRE_DT;
            (param[23] = new OracleParameter("P_GCS_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = lst.GCS_MOD_BY;
            (param[24] = new OracleParameter("P_GCS_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = lst.GCS_MOD_DT;
            param[25] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_gen_enqser", CommandType.StoredProcedure, param);
            return effects;
        }
        public List<SEARCH_MST_EMP> Get_mst_emp(string _company, string _profitcenter)
        {
            List<SEARCH_MST_EMP> emp = new List<SEARCH_MST_EMP>();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblPCEmp", "sp_getpcempolyee", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                emp = DataTableExtensions.ToGenericList<SEARCH_MST_EMP>(_dtResults, SEARCH_MST_EMP.Converter);
            }
            return emp;
        }


        public MST_EMPLOYEE_NEW_TBS getMstEmployeeDetailsByNic(string Nic)
        {
            MST_EMPLOYEE_NEW_TBS employee = null;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Nic;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_employee", "sp_tour_get_employee_nic", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                employee = DataTableExtensions.ToGenericList<MST_EMPLOYEE_NEW_TBS>(_dtResults, MST_EMPLOYEE_NEW_TBS.Converter)[0];
            }
            return employee;
        }

        //subodana 2016-03-19
        public List<GEN_CUST_ENQ> GET_ENQRY_BY_CUST_PEN_INV(string Com, string CustomerCode)
        {
            List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_CUST", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CustomerCode;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_ENQRY_BY_CUST_NEW", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterDetail);
            }
            return result;
        }

        public Int32 UPDATE_ENQ_STATUS(string cuscode, string enqid)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_GCE_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscode;
            (param[1] = new OracleParameter("P_GCE_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqid;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int16)UpdateRecords("UPDATE_ENQ_STATUS_NEW", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects;
        }

        //Sanjeewa 2016-03-21
        public DataTable Get_DailySalesReport(DateTime _fdate, DateTime _tdate, string _customer, string _itemcode, string _com, string _pc, string _user)
        {
            OracleParameter[] param = new OracleParameter[8];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[4] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemcode;
            (param[5] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[6] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[7] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_daily_sales", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Sanjeewa 2016-04-19
        public DataTable Get_DebtorStatementReport(DateTime _fdate, DateTime _tdate, string _customer, string _com, string _pc, string _user)
        {
            OracleParameter[] param = new OracleParameter[7];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[4] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[5] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[6] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_debt_settlement", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //subodana 2016-03-24
        public List<GEN_CUST_ENQSER> GetEnqSerData(string enqid)
        {
            List<GEN_CUST_ENQSER> result = new List<GEN_CUST_ENQSER>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_ENQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqid;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_CUS_ENQ_SER_NEW", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQSER>(_dtResults, GEN_CUST_ENQSER.Converter);
            }
            return result;
        }
        //Sanjeewa 2016-03-24
        public DataTable Get_DailySalesDetailReport(DateTime _fdate, DateTime _tdate, string _customer, string _itemcode, string _com, string _pc, string _user)
        {
            OracleParameter[] param = new OracleParameter[8];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[4] = new OracleParameter("in_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemcode;
            (param[5] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[6] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[7] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_daily_sales_dtl", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Sanjeewa 2016-03-25
        public DataTable Get_ReceiptDetailReport(DateTime _fdate, DateTime _tdate, string _customer, string _com, string _pc, string _user)
        {
            OracleParameter[] param = new OracleParameter[7];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            (param[3] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[4] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[5] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[6] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_receipt_dtl", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Sanjeewa 2016-04-01
        public DataTable Get_ATSInquiryReport(DateTime _fdate, DateTime _tdate, string _customer, string _inqid, Int16 _status, string _type, string _com, string _pc, string _user)
        {
            OracleParameter[] param = new OracleParameter[10];
            param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            (param[1] = new OracleParameter("fdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fdate;
            (param[2] = new OracleParameter("tdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _tdate;
            (param[3] = new OracleParameter("in_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[4] = new OracleParameter("in_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[5] = new OracleParameter("in_cust", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[6] = new OracleParameter("in_inqid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inqid;
            (param[7] = new OracleParameter("in_status", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _status;
            (param[8] = new OracleParameter("in_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _type;
            (param[9] = new OracleParameter("in_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            DataTable _dtResults = QueryDataTable("tbl", "sp_get_ats_inquiry", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //subodana
        public List<InvoiceHeader> GetInvoiceData(string enqid)
        {
            List<InvoiceHeader> result = new List<InvoiceHeader>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_ENQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqid;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_INV_DATA", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceHeader>(_dtResults, InvoiceHeader.ConverterTours);
            }
            return result;
        }
        //subodana
        public List<InvoiceItem> GetInvoiceDetailforInvNo(string InvNo)
        {
            List<InvoiceItem> result = new List<InvoiceItem>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "GET_INVOICE_DETAILS_NEW", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceItem>(_dtResults, InvoiceItem.ConverterTours);
            }
            return result;
        }
        //subodana
        public List<InvoiceHeader> GetInvoiceHDRData(string InvNo)
        {
            List<InvoiceHeader> result = new List<InvoiceHeader>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_INV", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = InvNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_INV_DATA_IN", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceHeader>(_dtResults, InvoiceHeader.ConverterTours);
            }
            return result;
        }
        public string GetServiceByCode(string company, string userDefPro, string chgCd)
        {
            string result = "";
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            (_para[2] = new OracleParameter("p_chargecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgCd;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "GET_SERVICE_BYCODEAIRTRVAL", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["sac_scv_by"].ToString() != "")
            {
                result = _dtResults.Rows[0]["sac_scv_by"].ToString();
            }
            return result;
        }

        public string GetServiceByCodeTRANS(string company, string userDefPro, string chgCd)
        {
            string result = "";
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            (_para[2] = new OracleParameter("p_chargecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgCd;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "GET_SERVICE_BYCODETRANS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["sac_scv_by"].ToString() != "")
            {
                result = _dtResults.Rows[0]["sac_scv_by"].ToString();
            }
            return result;
        }

        public string GetServiceByCodeMSCELNS(string company, string userDefPro, string chgCd)
        {
            string result = "";
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            (_para[2] = new OracleParameter("p_chargecode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgCd;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "GET_SERVICE_BYCODEMSCELNS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0 && _dtResults.Rows[0]["sac_scv_by"].ToString() != "")
            {
                result = _dtResults.Rows[0]["sac_scv_by"].ToString();
            }
            return result;
        }

        public int Save_costPurchaseOrderHead(MST_PR_HDR prHeader)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[25];
            (param[0] = new OracleParameter("p_ph_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = prHeader.PH_SEQ_NO;
            (param[1] = new OracleParameter("p_ph_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_TP;
            (param[2] = new OracleParameter("p_ph_sub_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_SUB_TP;
            (param[3] = new OracleParameter("p_ph_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_DOC_NO;
            (param[4] = new OracleParameter("p_ph_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_COM;
            (param[5] = new OracleParameter("p_ph_profit_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_PROFIT_CD;
            (param[6] = new OracleParameter("p_ph_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = prHeader.PH_DT;
            (param[7] = new OracleParameter("p_ph_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_REF;
            (param[8] = new OracleParameter("p_ph_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_JOB_NO;
            (param[9] = new OracleParameter("p_ph_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_SUPP;
            (param[10] = new OracleParameter("p_ph_cur_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_CUR_CD;
            (param[11] = new OracleParameter("p_ph_ex_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_EX_RT;
            (param[12] = new OracleParameter("p_ph_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_STUS;
            (param[13] = new OracleParameter("p_ph_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_REMARKS;
            (param[14] = new OracleParameter("p_ph_sub_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_SUB_TOT;
            (param[15] = new OracleParameter("p_ph_tax_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_TAX_TOT;
            (param[16] = new OracleParameter("p_ph_dis_rt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_DIS_RT;
            (param[17] = new OracleParameter("p_ph_dis_amt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_DIS_AMT;
            (param[18] = new OracleParameter("p_ph_oth_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_OTH_TOT;
            (param[19] = new OracleParameter("p_ph_tot", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = prHeader.PH_TOT;
            (param[20] = new OracleParameter("p_ph_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prHeader.PH_CRE_BY;
            (param[21] = new OracleParameter("p_ph_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = prHeader.PH_CRE_DT;
            (param[22] = new OracleParameter("p_ph_session_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = prHeader.PH_SESSION_ID;
            param[23] = new OracleParameter("O_SEQ", OracleDbType.Int32, null, ParameterDirection.Output);
            param[24] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("SP_SAVE_COSTPOHEAD", CommandType.StoredProcedure, param);
            if (effects > 0) effects = Convert.ToInt32(param[23].Value.ToString());
            return effects;
        }


        public List<MST_PR_HED_DET> getcostingforPurchaseOrder(string costNo, string com, string procen)
        {
            List<MST_PR_HED_DET> result = new List<MST_PR_HED_DET>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COSTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = costNo;
            (_para[1] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[2] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = procen;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_COSTINGDETBYNO", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_PR_HED_DET>(_dtResults, MST_PR_HED_DET.Converter);
            }
            return result;
        }

        public List<string> getcostingSerProPurchaseOrder(string costNo)
        {

            List<string> result = new List<string>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_COSTNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = costNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_COSTINGSERPROBYID", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                foreach (DataRow row in _dtResults.Rows)
                {
                    string val = row["SER_PRO"].ToString();
                    result.Add(val);
                }
            }
            return result;
        }
        public MasterAutoNumber GetAutoNumber(string _module, Int32? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year)
        {
            OracleParameter[] param = new OracleParameter[8];
            MasterAutoNumber _masterAutoNumber = new MasterAutoNumber();

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _module;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _startChar;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catType;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _catCode;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _modifyDate;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _year;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAuto", "sp_autonumber", CommandType.StoredProcedure, false, param);
            //   p_module IN NVARCHAR2,p_direction IN NUMBER,p_startchar IN NVARCHAR2,p_cattype in NVARCHAR2,p_catcode in NVARCHAR2,p_modifydate in DATE,p_year in NUMBER ,c_data out SYS_REFCURSOR
            if (_dtResults.Rows.Count > 0)
            {
                _masterAutoNumber = DataTableExtensions.ToGenericList<MasterAutoNumber>(_dtResults, MasterAutoNumber.ConvertTotal)[0];
            }
            else if (_dtResults.Rows.Count == 0)
            {
                //_masterAutoNumber.Aut_number = 999;//since the auto number not generated, it is hard corded.

                _masterAutoNumber.Aut_cate_cd = _catCode;
                _masterAutoNumber.Aut_cate_tp = _catType;
                _masterAutoNumber.Aut_direction = _direction;
                _masterAutoNumber.Aut_modify_dt = _modifyDate;
                _masterAutoNumber.Aut_moduleid = _module;
                _masterAutoNumber.Aut_number = 1;
                _masterAutoNumber.Aut_start_char = _startChar;
                _masterAutoNumber.Aut_year = _year;
            }

            //if (_masterAutoNumber == null)
            //{
            //    _masterAutoNumber.Aut_cate_cd = _catCode;
            //    _masterAutoNumber.Aut_cate_tp = _catType;
            //    _masterAutoNumber.Aut_direction = _direction;
            //    _masterAutoNumber.Aut_modify_dt = _modifyDate;
            //    _masterAutoNumber.Aut_moduleid = _module;
            //    _masterAutoNumber.Aut_number = 1;
            //    _masterAutoNumber.Aut_start_char = _startChar;
            //    _masterAutoNumber.Aut_year = _year;
            //}

            return _masterAutoNumber;
        }
        public Int16 UpdateAutoNumber(MasterAutoNumber _masterAutoNumber)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[8];

            (param[0] = new OracleParameter("p_module", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_moduleid;
            (param[1] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_direction;
            (param[2] = new OracleParameter("p_startchar", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_start_char;
            (param[3] = new OracleParameter("p_cattype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_tp;
            (param[4] = new OracleParameter("p_catcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_cate_cd;
            (param[5] = new OracleParameter("p_modifydate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_modify_dt;
            (param[6] = new OracleParameter("p_year", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _masterAutoNumber.Aut_year;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("sp_updateautonumber", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        public Int32 Save_costPurchaseOrderDet(MST_PR_DET prMainItems)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[15];

            (param[0] = new OracleParameter("p_pd_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_SEQ_NO;
            (param[1] = new OracleParameter("p_pd_line_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_LINE_NO;
            (param[2] = new OracleParameter("p_pd_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_ITM_CD;
            (param[3] = new OracleParameter("p_pd_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_ITM_STUS;
            (param[4] = new OracleParameter("p_pd_qty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_QTY;
            (param[5] = new OracleParameter("p_pd_unit_price", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_UNIT_PRICE;
            (param[6] = new OracleParameter("p_pd_dis_rt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_DIS_RT;
            (param[7] = new OracleParameter("p_pd_dis_amt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_DIS_AMT;
            (param[8] = new OracleParameter("p_pd_line_tax", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_LINE_TAX;
            (param[9] = new OracleParameter("p_pd_line_amt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_LINE_AMT;
            (param[10] = new OracleParameter("p_pd_pi_bal", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_PI_BAL;
            (param[11] = new OracleParameter("p_pd_lc_bal", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_LC_BAL;
            (param[12] = new OracleParameter("p_pd_si_bal", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_SI_BAL;
            (param[13] = new OracleParameter("p_pd_grn_bal", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prMainItems.PD_GRN_BAL;
            param[14] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effected = UpdateRecords("SP_SAVE_COSTPODET", CommandType.StoredProcedure, param);
            return (Int32)effected;
        }

        public int UpdatePOStatus(string enquiryID, string User, DateTime dateTime)
        {
            int effected = 0;
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_enqid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enquiryID;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = User;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = "C";
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effected = UpdateRecords("SP_UPDATE_COST_POSTATUS", CommandType.StoredProcedure, param);
            return (Int16)effected;
        }

        //SUBODANA
        public List<InvoiceHeader> GetAllSalesHRDdata(string com, string procen, DateTime startdate, DateTime enddate)
        {
            List<InvoiceHeader> result = new List<InvoiceHeader>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = procen;
            (_para[2] = new OracleParameter("P_ST_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = startdate;
            (_para[3] = new OracleParameter("P_END_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = enddate;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_SALESHDR", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceHeader>(_dtResults, InvoiceHeader.ConverterTours);
            }
            return result;
        }
        public List<RecieptHeader> GetAllRecieptHRDdata(string com, string procen, DateTime startdate, DateTime enddate)
        {
            List<RecieptHeader> result = new List<RecieptHeader>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[1] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = procen;
            (_para[2] = new OracleParameter("P_ST_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = startdate;
            (_para[3] = new OracleParameter("P_END_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = enddate;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_RECIEPTSHDR", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<RecieptHeader>(_dtResults, RecieptHeader.ConverterTours);
            }
            return result;
        }
        public List<MST_GNR_ACC> GetgrnalDetails(string com)
        {
            List<MST_GNR_ACC> result = new List<MST_GNR_ACC>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_JRNDET", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_GNR_ACC>(_dtResults, MST_GNR_ACC.Converter);
            }
            return result;
        }
        //subodana 2016-04-09
        public Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_SIH_INV_NO", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = invNo;
            (param[1] = new OracleParameter("P_SIH_ANAL_10", OracleDbType.Int32, null, ParameterDirection.Input)).Value = value;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int16)UpdateRecords("UPDATE_INV_HDRENGLOG", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects;
        }
        public Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_REC_NO", OracleDbType.Varchar2, null, ParameterDirection.Input)).Value = recNo;
            (param[1] = new OracleParameter("P_ANAL", OracleDbType.Int32, null, ParameterDirection.Input)).Value = value;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = (Int16)UpdateRecords("UPDATE_REC_HDRENGLOG", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects;
        }

        public Int32 saveInvoicePax(MST_ST_PAX_DET pax)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("P_SPD_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pax.SPD_SEQ;
            (param[1] = new OracleParameter("P_SPD_INV_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_INV_NO;
            (param[2] = new OracleParameter("P_SPD_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pax.SPD_LINE;
            (param[3] = new OracleParameter("P_SPD_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_ENQ_ID;
            (param[4] = new OracleParameter("P_SPD_CUS_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_CUS_CD;
            (param[5] = new OracleParameter("P_SPD_CUS_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_CUS_NAME;
            (param[6] = new OracleParameter("P_SPD_RMK", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_RMK;
            (param[7] = new OracleParameter("P_SPD_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = pax.SPD_AMT;
            (param[8] = new OracleParameter("P_SPD_STUS", OracleDbType.Int32, null, ParameterDirection.Input)).Value = pax.SPD_STUS;
            (param[9] = new OracleParameter("P_SPD_PP_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_PP_NO;
            (param[10] = new OracleParameter("P_SPD_MOB", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_MOB;
            (param[11] = new OracleParameter("P_SPD_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pax.SPD_NIC;
            param[12] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            //ConnectionOpen();
            effects = (Int32)UpdateRecords("SP_SAVE_INVOICEPAX", CommandType.StoredProcedure, param);
            //ConnectionClose();
            return effects;
        }



        public List<MST_ST_PAX_DET> GetInvoicePaxDet(string invNo)
        {
            List<MST_ST_PAX_DET> result = new List<MST_ST_PAX_DET>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_INV_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_INVOICEPAXDET", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_ST_PAX_DET>(_dtResults, MST_ST_PAX_DET.Converter);
            }
            return result;
        }
        //Nuwan
        public List<GEN_CUST_ENQ> SP_TOUR_GET_TRANSPORT_ENQRY(string Com, string PC, String Status, string type)
        {
            List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            (_para[2] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (_para[3] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_TRANSPORT_ENQRY", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterEnq);
            }
            return result;
        }

        public List<InvoiceHeader> getInvoiceHedDataByEnqId(string ENQID)
        {
            List<InvoiceHeader> result = new List<InvoiceHeader>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_enq", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ENQID;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_INVBYENQ", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceHeader>(_dtResults, InvoiceHeader.ConverterTours);
            }
            return result;
        }

        public InvoiceHeader getInvoiceHederData(string invNo, string com, string procen)
        {
            InvoiceHeader result = new InvoiceHeader();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_invno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invNo;
            (_para[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = procen;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_INVOICEDATA", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<InvoiceHeader>(_dtResults, InvoiceHeader.ConverterTours)[0];
            }
            return result;
        }
        public Int32 updateReceiptHeader(RecieptHeader _recieptHeader)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_act;
            (param[1] = new OracleParameter("P_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_mod_by;
            (param[2] = new OracleParameter("P_IS_DAYEND", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_is_dayend;
            (param[3] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_com_cd;
            (param[4] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_profit_center_cd;
            (param[5] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _recieptHeader.Sar_seq_no;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_RECEIPTHED", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<RecieptItem> getReceiptItemList(string invNo)
        {
            List<RecieptItem> rec = new List<RecieptItem>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invNo;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_RECPTITEMBYINV", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<RecieptItem>(_dtResults, RecieptItem.ConverterTours);
            }
            return rec;
        }

        public MST_CHKINOUT getEnqChkData(string enqId)
        {
            MST_CHKINOUT rec = new MST_CHKINOUT();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_GET_ENQCHKDETAILS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<MST_CHKINOUT>(_dtResults, MST_CHKINOUT.Converter)[0];
            }
            return rec;
        }

        public Int32 saveCheckoutDetails(MST_CHKINOUT check)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[12];
            (param[0] = new OracleParameter("P_CHK_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = check.CHK_ENQ_ID;
            (param[1] = new OracleParameter("P_CHK_OUT_KM", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = check.CHK_OUT_KM;
            (param[2] = new OracleParameter("P_CHK_OUT_DTE", OracleDbType.Date, null, ParameterDirection.Input)).Value = check.CHK_OUT_DTE;
            (param[3] = new OracleParameter("P_CHK_OUT_FUEL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = check.CHK_OUT_FUEL;
            (param[4] = new OracleParameter("P_CHK_IN_DTE", OracleDbType.Date, null, ParameterDirection.Input)).Value = check.CHK_IN_DTE;
            (param[5] = new OracleParameter("P_CHK_IN_KM", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = check.CHK_IN_KM;
            (param[6] = new OracleParameter("P_CHK_IN_FUEL", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = check.CHK_IN_FUEL;
            (param[7] = new OracleParameter("P_CHK_OTH_CHRG", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = check.CHK_OTH_CHRG;
            (param[8] = new OracleParameter("P_CHK_RMKS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = check.CHK_RMKS;
            (param[9] = new OracleParameter("P_CHK_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = check.CHK_CRE_BY;
            (param[10] = new OracleParameter("P_CHK_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = check.CHK_MOD_BY;
            param[11] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_CHECKINOUTDETAILS", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<MST_TEMP_MESSAGES> getTempSmsMessage(string company, string pc, string msgtyp)
        {
            List<MST_TEMP_MESSAGES> rec = new List<MST_TEMP_MESSAGES>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (_para[2] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = msgtyp;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TEMPMESSAGES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<MST_TEMP_MESSAGES>(_dtResults, MST_TEMP_MESSAGES.Converter);
            }
            return rec;
        }

        public List<GEN_CUST_ENQ> getAllEnquiryData(string enqId, string Com, string ProfitC, string Status)
        {
            List<GEN_CUST_ENQ> result = new List<GEN_CUST_ENQ>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Com;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ProfitC;
            (_para[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            (_para[3] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblJobs", "SP_TOUR_GET_ALL_ENQRY", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<GEN_CUST_ENQ>(_dtResults, GEN_CUST_ENQ.ConverterEnq);
            }
            return result;
        }

        public List<QUO_COST_HDR> GET_INVOICEDCOST_PRFITABILITY_DETAILS(string _com, string _procenter, string _req, string _costRef, string _customer, string _category, DateTime _fromdate, DateTime _todate)
        {
            List<QUO_COST_HDR> result = new List<QUO_COST_HDR>();

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _procenter;
            (param[2] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            (param[3] = new OracleParameter("P_COSTREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _costRef;
            (param[4] = new OracleParameter("P_CUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[5] = new OracleParameter("P_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[6] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[7] = new OracleParameter("P_TODATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            param[8] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "SP_SEARCH_INVCOST_PRFITA_DET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<QUO_COST_HDR>(_dtResults, QUO_COST_HDR.Converter3);
            }
            return result;
        }

        public List<QUO_COST_HDR> GET_INVOICEDCOST_PRFITABILITY(string _com, string _procenter, string _req, string _costRef, string _customer, string _category, DateTime _fromdate, DateTime _todate)
        {
            List<QUO_COST_HDR> result = new List<QUO_COST_HDR>();

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _procenter;
            (param[2] = new OracleParameter("P_REQ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _req;
            (param[3] = new OracleParameter("P_COSTREF", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _costRef;
            (param[4] = new OracleParameter("P_CUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _customer;
            (param[5] = new OracleParameter("P_CAT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _category;
            (param[6] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _fromdate;
            (param[7] = new OracleParameter("P_TODATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = _todate;
            param[8] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_Cost", "SP_SEARCH_INVCOST_PRFITABILITY", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<QUO_COST_HDR>(_dtResults, QUO_COST_HDR.Converter2);
            }
            return result;
        }

        public int updateOldTourInvoiceStatus(InvoiceHeader _invoiceHeader)
        {
            int effects = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_no;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_com;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_pc;
            (param[3] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_seq_no;
            (param[4] = new OracleParameter("P_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_mod_by;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (int)UpdateRecords("SP_UPDATE_OLDINVOICESTATUS", CommandType.StoredProcedure, param);
            return effects;
        }

        //subodana
        public int updateOldTourInvoiceanal8(InvoiceHeader _invoiceHeader)
        {
            int effects = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_inv_no;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_com;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_pc;
            (param[3] = new OracleParameter("P_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_mod_by;
            (param[4] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_stus;
            (param[5] = new OracleParameter("P_ANAL8", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _invoiceHeader.Sah_anal_8;
            param[6] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (int)UpdateRecords("SP_UPDATE_ANAL8", CommandType.StoredProcedure, param);
            return effects;
        }
        public List<MST_COUNTRY> getCountryDetails(string countryCd)
        {
            List<MST_COUNTRY> result = new List<MST_COUNTRY>();

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_CUTYCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = countryCd;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_country", "SP_GET_COUNTRYDET", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_COUNTRY>(_dtResults, MST_COUNTRY.Converter);
            }
            return result;
        }

        public List<MST_CUSTOMER_TYPE> getCustomerTypes()
        {
            List<MST_CUSTOMER_TYPE> result = new List<MST_CUSTOMER_TYPE>();

            OracleParameter[] param = new OracleParameter[1];
            param[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_country", "SP_GET_CUSTOMERTYPES", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<MST_CUSTOMER_TYPE>(_dtResults, MST_CUSTOMER_TYPE.Converter);
            }
            return result;
        }

        public List<RecieptItemTBS> getReceiptItemByinvNo(string invNo, string com, string PC)
        {
            List<RecieptItemTBS> result = new List<RecieptItemTBS>();

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("P_INVNO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invNo;
            (param[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PC;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("mst_DTL", "SP_GET_RECEIPTITEMSBYENQ", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<RecieptItemTBS>(_dtResults, RecieptItemTBS.ConverterTours);
            }
            return result;
        }

        public List<SearchEnqSEQ> GetseqDetails(string EnqId)
        {
            List<SearchEnqSEQ> result = new List<SearchEnqSEQ>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_EnqId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = EnqId;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("abc", "SP_GET_Enq_Seq", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                result = DataTableExtensions.ToGenericList<SearchEnqSEQ>(_dtResults, SearchEnqSEQ.Converter);
            }
            return result;
        }
        public List<RecieptHeader> GET_RECIEPT_BY_ENQ(string company, string pc, string ENQID)
        {
            List<RecieptHeader> rec = new List<RecieptHeader>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("P_PROCEN", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (_para[2] = new OracleParameter("P_ENQ_ID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ENQID;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_RECIEPTENQID", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<RecieptHeader>(_dtResults, RecieptHeader.ConverterTours);
            }
            return rec;
        }

        public List<ST_MENU> getUserMenu(string userId)
        {
            List<ST_MENU> rec = new List<ST_MENU>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_USERID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TOURSUSERMENU", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_MENU>(_dtResults, ST_MENU.Converter);
            }
            return rec;
        }

        public ST_MENU getAcccessPermission(string userId, int menuId)
        {
            ST_MENU rec = new ST_MENU();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("P_USERID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userId;
            (_para[1] = new OracleParameter("P_MENUID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = menuId;
            _para[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TOURSUSERACCPER", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_MENU>(_dtResults, ST_MENU.Converter)[0];
            }
            return rec;
        }

        public MST_PCADDPARA getPcAdditionalPara(string com, string pc, string code)
        {
            MST_PCADDPARA rec = new MST_PCADDPARA();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (_para[2] = new OracleParameter("P_CODE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PCPARA", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<MST_PCADDPARA>(_dtResults, MST_PCADDPARA.Converter)[0];
            }
            return rec;
        }

        public Int32 UpdateCostHeaderCustomerApprove(QUO_COST_HDR oHeader)
        {
            int effects = 0;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.QCH_SEQ;
            (param[1] = new OracleParameter("P_QCH_CUS_APP", OracleDbType.Int32, null, ParameterDirection.Input)).Value = oHeader.QCH_CUS_APP;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_COSTCUSAPPRV", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<RecieptHeaderTBS> getReceiptItems(string company, string pc, string type, string enqId)
        {
            List<RecieptHeaderTBS> rec = new List<RecieptHeaderTBS>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (_para[2] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
            (_para[3] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ADVANCERECEIPTPAY", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<RecieptHeaderTBS>(_dtResults, RecieptHeaderTBS.ConverterTours);
            }
            return rec;
        }

        public List<ST_SATIS_QUEST> getFeedBackQuestions(string channel, string company, string userDefPro)
        {
            List<ST_SATIS_QUEST> rec = new List<ST_SATIS_QUEST>();
            OracleParameter[] _para = new OracleParameter[4];
            (_para[0] = new OracleParameter("P_CHANL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (_para[1] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[2] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            _para[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_FEEDBACKQUES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_SATIS_QUEST>(_dtResults, ST_SATIS_QUEST.Converter);
            }
            return rec;
        }

        public List<ST_SATIS_VAL> getFeedBackQuestionsAns(int seq)
        {
            List<ST_SATIS_VAL> rec = new List<ST_SATIS_VAL>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
            _para[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_FEEDBACKQUESVAL", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_SATIS_VAL>(_dtResults, ST_SATIS_VAL.Converter);
            }
            return rec;
        }

        public int SaveCustomerFeedBac(ST_SATIS_RESULT data)
        {
            int effects = 0;
            OracleParameter[] param = new OracleParameter[9];
             (param[0] = new OracleParameter("P_SSVL_QSTSEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = data.SSVL_QSTSEQ;
            (param[1] = new OracleParameter("P_SSVL_VALSEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = data.SSVL_VALSEQ;
            (param[2] = new OracleParameter("P_SSVL_ANS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = data.SSVL_ANS;
            (param[3] = new OracleParameter("P_SSVL_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = data.SSVL_ENQID;
            (param[4] = new OracleParameter("P_SSVL_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = data.SSVL_CHNL;
            (param[5] = new OracleParameter("P_SSVL_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = data.SSVL_PC;
            (param[6] = new OracleParameter("P_SSVL_CRE_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = data.SSVL_CRE_BY;
            (param[7] = new OracleParameter("P_SSVL_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = data.SSVL_COM;
            param[8] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_CUSFEEDBACK", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<ST_SATIS_RESULT> getCustermerFeedData(string channel, string company, string userDefPro, string enqId)
        {
            List<ST_SATIS_RESULT> rec = new List<ST_SATIS_RESULT>();
            OracleParameter[] _para = new OracleParameter[5];
            (_para[0] = new OracleParameter("P_SSVL_CHNL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = channel;
            (_para[1] = new OracleParameter("P_SSVL_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            (_para[2] = new OracleParameter("P_SSVL_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[3] = new OracleParameter("P_SSVL_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
            _para[4] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_FEEDBACKDATA", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_SATIS_RESULT>(_dtResults, ST_SATIS_RESULT.Converter);
            }
            return rec;
        }
        // Nuwan 2016.05.25
        public DataTable GetReceipt(string _doc)
        {   

            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doc;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _tblData = QueryDataTable("tbl", "sp_tbsgetreceipt", CommandType.StoredProcedure, false, param);

            return _tblData;
        }

        public List<MST_FAC_LOC> getFacLocations(string company, string pc)
        {
            List<MST_FAC_LOC> rec = new List<MST_FAC_LOC>();
            OracleParameter[] _para = new OracleParameter[3];
            (_para[0] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (_para[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
           _para[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_FACLOCATIONS", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<MST_FAC_LOC>(_dtResults, MST_FAC_LOC.Converter);
            }
            return rec;
        }

        public Int32 saveEnquiryCharges(ST_ENQ_CHARGES enqChg)
        {
             Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[18];
            (param[0] = new OracleParameter("P_SCH_SEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = enqChg.SCH_SEQ_NO;
            (param[1] = new OracleParameter("P_SCH_ITM_SERVICE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ITM_SERVICE;
            (param[2] = new OracleParameter("P_SCH_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ITM_CD;
            (param[3] = new OracleParameter("P_SCH_ITM_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ITM_STUS;
            (param[4] = new OracleParameter("P_SCH_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_QTY;
            (param[5] = new OracleParameter("P_SCH_UNIT_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_UNIT_RT;
            (param[6] = new OracleParameter("P_SCH_UNIT_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_UNIT_AMT;
            (param[7] = new OracleParameter("P_SCH_DISC_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_DISC_RT;
            (param[8] = new OracleParameter("P_SCH_DISC_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_DISC_AMT;
            (param[9] = new OracleParameter("P_SCH_ITM_TAX_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_ITM_TAX_AMT;
            (param[10] = new OracleParameter("P_SCH_TOT_AMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_TOT_AMT;
            (param[11] = new OracleParameter("P_SCH_ENQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ENQ_NO;
            (param[12] = new OracleParameter("P_SCH_EX_RT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_EX_RT;
            (param[13] = new OracleParameter("P_SCH_CURR", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_CURR;
            (param[14] = new OracleParameter("P_SCH_INVOICED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = enqChg.SCH_INVOICED;
            (param[15] = new OracleParameter("P_SCH_INVOICED_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_INVOICED_NO;
            (param[16] = new OracleParameter("P_SCH_ALT_ITM_DESC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ALT_ITM_DESC;
            param[17] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_ENQUIRYCHARGES", CommandType.StoredProcedure, param);
            return effects;
        }
        public Int32 UpdateEnqChargesInvoiced(ST_ENQ_CHARGES enqChg)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_SCH_ITM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ITM_CD;
            (param[1] = new OracleParameter("P_SCH_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = enqChg.SCH_QTY;
            (param[2] = new OracleParameter("P_SCH_ENQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_ENQ_NO;
            (param[3] = new OracleParameter("P_SCH_INVOICED", OracleDbType.Int32, null, ParameterDirection.Input)).Value = enqChg.SCH_INVOICED;
            (param[4] = new OracleParameter("P_SCH_INVOICED_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqChg.SCH_INVOICED_NO;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_ENQUIRYCHARGES", CommandType.StoredProcedure, param);
            return effects;
        }
        public List<ST_ENQ_CHARGES> saleGetChargItemDetails(string ENQID)
        {
            List<ST_ENQ_CHARGES> rec = new List<ST_ENQ_CHARGES>();
            OracleParameter[] _para = new OracleParameter[2];
            (_para[0] = new OracleParameter("p_enqid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ENQID;
            _para[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_ENQCHARGES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_ENQ_CHARGES>(_dtResults, ST_ENQ_CHARGES.Converter);
            }
            return rec;
        }

        public List<ST_AIRTCKT_TYPS> getAirTicketTypes()
        {
            List<ST_AIRTCKT_TYPS> rec = new List<ST_AIRTCKT_TYPS>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_AIRTCKTTYP", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_AIRTCKT_TYPS>(_dtResults, ST_AIRTCKT_TYPS.Converter);
            }
            return rec;
        }

        public List<ST_VEHICLE_TP> getVehicleTypes()
        {
            List<ST_VEHICLE_TP> rec = new List<ST_VEHICLE_TP>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_VEHICLETYPES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_VEHICLE_TP>(_dtResults, ST_VEHICLE_TP.Converter);
            }
            return rec;
        }

        public List<ST_PKG_TP> getpKGTypes()
        {
            List<ST_PKG_TP> rec = new List<ST_PKG_TP>();
            OracleParameter[] _para = new OracleParameter[1];
            _para[0] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_PKGTYPES", CommandType.StoredProcedure, false, _para);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<ST_PKG_TP>(_dtResults, ST_PKG_TP.Converter);
            }
            return rec;
        }

        public List<RecieptHeaderTBS> getOtherPartyReceipts(string dateFrom, string dateTo, string OthCus, string Cus, string company, string pc)
        {
            List<RecieptHeaderTBS> rec = new List<RecieptHeaderTBS>();
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_FROMDT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dateFrom;
            (param[1] = new OracleParameter("P_TODT", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = dateTo;
            (param[2] = new OracleParameter("P_OTHCUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = OthCus;
            (param[3] = new OracleParameter("P_CUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Cus;
            (param[4] = new OracleParameter("P_COM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[6] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_OTHERPARTYRECPAYMENT", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                rec = DataTableExtensions.ToGenericList<RecieptHeaderTBS>(_dtResults, RecieptHeaderTBS.ConverterTours);
            }
            return rec;
        }

        public Int32 updateTBSReceiptHeaderDetails(RecieptHeaderTBS hedDetails)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int32 effects = 0;
            (param[0] = new OracleParameter("P_SIR_PROFIT_CENTER_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hedDetails.Sir_profit_center_cd;
            (param[1] = new OracleParameter("P_SIR_RECEIPT_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hedDetails.Sir_receipt_type;
            (param[2] = new OracleParameter("P_SIR_COM_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hedDetails.Sir_com_cd;
            (param[3] = new OracleParameter("P_SIR_OTH_PAIDAMT", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = hedDetails.payAmount;
            (param[4] = new OracleParameter("P_SIR_RECEIPT_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = hedDetails.Sir_receipt_no;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_UPDATE_OTHERPARTYRECEIPT", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 deleteEnquiryCharges(string enqId)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
            param[1] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_DELETE_TEMPENQCHARGES", CommandType.StoredProcedure, param);
            return effects;
        }

        public List<mst_fleet_driver> getAlowcatedFleetAndDriverDetails(string driver, string fleet, DateTime frmDt, DateTime toDt, string fletordri)
        {
            List<mst_fleet_driver> allocation = new List<mst_fleet_driver>();
            OracleParameter[] param = new OracleParameter[6];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_DRIVER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driver;
            (param[1] = new OracleParameter("P_FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet;
            (param[2] = new OracleParameter("P_FRMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt;
            (param[3] = new OracleParameter("P_TODT", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            (param[4] = new OracleParameter("P_FLETORDRI", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fletordri;
            param[5] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_CHECK_ALOWFLEETANDDRIVERS", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                allocation = DataTableExtensions.ToGenericList<mst_fleet_driver>(_dtResults, mst_fleet_driver.Converter);
            }
            return allocation;
        }

        public List<GEN_CUS_ENQ_DRIVER> getAlowcatedFleetAndDriverDetailsInEnquiry(string driver, DateTime frmDt, DateTime toDt)
        {
            List<GEN_CUS_ENQ_DRIVER> allocation = new List<GEN_CUS_ENQ_DRIVER>();
            OracleParameter[] param = new OracleParameter[4];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_DRIVER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driver;
            (param[1] = new OracleParameter("P_FRMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt;
            (param[2] = new OracleParameter("P_TODT", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_CHECK_ENQUIRYASSGNDRI", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                allocation = DataTableExtensions.ToGenericList<GEN_CUS_ENQ_DRIVER>(_dtResults, GEN_CUS_ENQ_DRIVER.Converter);
            }
            return allocation;
        }

        public Int32 Save_GEN_CUST_ENQ_DRIVER(GEN_CUS_ENQ_DRIVER driverList)
        {
            try
            {
                Int32 effects = 0;
                OracleParameter[] param = new OracleParameter[15];
                (param[0] = new OracleParameter("P_GCD_DRIVER_CD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driverList.GCD_DRIVER_CD;
                (param[1] = new OracleParameter("P_GCD_ENQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driverList.GCD_ENQ_NO;
                (param[2] = new OracleParameter("P_GCD_DRIVER_NAME", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driverList.GCD_DRIVER_NAME;
                (param[3] = new OracleParameter("P_GCD_FROM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = driverList.GCD_FROM_DT;
                (param[4] = new OracleParameter("P_GCD_TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = driverList.GCD_TO_DT;
                (param[5] = new OracleParameter("P_GCD_DRIVER_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = driverList.GCD_DRIVER_COST;
                (param[6] = new OracleParameter("P_GCD_AMENTMENT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = driverList.GCD_AMENTMENT;
                (param[7] = new OracleParameter("P_GCD_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = driverList.GCD_ACT;
                (param[8] = new OracleParameter("P_GCD_ACTL_DRIVER_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = driverList.GCD_ACTL_DRIVER_COST;
                (param[9] = new OracleParameter("P_GCD_ADDED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = driverList.GCD_ADDED_DT;
                (param[10] = new OracleParameter("P_GCD_ADDED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driverList.GCD_ADDED_BY;
                (param[11] = new OracleParameter("P_GCD_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = driverList.GCD_MOD_DT;
                (param[12] = new OracleParameter("P_GCD_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = driverList.GCD_MOD_BY;
                (param[13] = new OracleParameter("P_GCD_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = driverList.GCD_SEQ;
                param[14] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("SP_SAVE_ENQUIRYDRIVERS", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GEN_CUS_ENQ_DRIVER> getEnquiryDriverDetails(string enqId)
        {
            try
            {
                List<GEN_CUS_ENQ_DRIVER> allocation = new List<GEN_CUS_ENQ_DRIVER>();
                OracleParameter[] param = new OracleParameter[2];
                DataTable _dtResults = new DataTable();
                (param[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
                param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tbl", "SP_CHECK_ENQDRIVERS", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    allocation = DataTableExtensions.ToGenericList<GEN_CUS_ENQ_DRIVER>(_dtResults, GEN_CUS_ENQ_DRIVER.Converter);
                }
                return allocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GEN_CUS_ENQ_FLEET> getAlowcatedFleetDetailsInEnquiry(string fleet, DateTime frmDt, DateTime toDt)
        {
            List<GEN_CUS_ENQ_FLEET> allocation = new List<GEN_CUS_ENQ_FLEET>();
            OracleParameter[] param = new OracleParameter[4];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet;
            (param[1] = new OracleParameter("P_FRMDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = frmDt;
            (param[2] = new OracleParameter("P_TODT", OracleDbType.Date, null, ParameterDirection.Input)).Value = toDt;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_CHECK_ENQUIRYASSGNVEHI", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                allocation = DataTableExtensions.ToGenericList<GEN_CUS_ENQ_FLEET>(_dtResults, GEN_CUS_ENQ_FLEET.Converter);
            }
            return allocation;
        }

        public List<GEN_CUS_ENQ_FLEET> getEnquiryFleetDetails(string enqId)
        {
            try
            {
                List<GEN_CUS_ENQ_FLEET> allocation = new List<GEN_CUS_ENQ_FLEET>();
                OracleParameter[] param = new OracleParameter[2];
                DataTable _dtResults = new DataTable();
                (param[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqId;
                param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tbl", "SP_CHECK_ENQVEHICLES", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    allocation = DataTableExtensions.ToGenericList<GEN_CUS_ENQ_FLEET>(_dtResults, GEN_CUS_ENQ_FLEET.Converter);
                }
                return allocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save_GEN_CUST_ENQ_FLEET(GEN_CUS_ENQ_FLEET fleet)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[16];
            (param[0] = new OracleParameter("P_GCF_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = fleet.GCF_SEQ;
            (param[1] = new OracleParameter("P_GCF_FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet.GCF_FLEET;
            (param[2] = new OracleParameter("P_GCF_ENQ_NO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet.GCF_ENQ_NO;
            (param[3] = new OracleParameter("P_GCF_FROM_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = fleet.GCF_FROM_DT;
            (param[4] = new OracleParameter("P_GCF_TO_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = fleet.GCF_TO_DT;
            (param[5] = new OracleParameter("P_GCF_FLEET_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = fleet.GCF_FLEET_COST;
            (param[6] = new OracleParameter("P_GCF_AMENTMENT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = fleet.GCF_AMENTMENT;
            (param[7] = new OracleParameter("P_GCF_ACT", OracleDbType.Int32, null, ParameterDirection.Input)).Value = fleet.GCF_ACT;
            (param[8] = new OracleParameter("P_GCF_ACTL_FLEET_COST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = fleet.GCF_ACTL_FLEET_COST;
            (param[9] = new OracleParameter("P_GCF_ADDED_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = fleet.GCF_ADDED_DT;
            (param[10] = new OracleParameter("P_GCF_ADDED_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet.GCF_ADDED_BY;
            (param[11] = new OracleParameter("P_GCF_MOD_DT", OracleDbType.Date, null, ParameterDirection.Input)).Value = fleet.GCF_MOD_DT;
            (param[12] = new OracleParameter("P_GCF_MOD_BY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet.GCF_MOD_BY;
            (param[13] = new OracleParameter("P_GCF_MODEL", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet.GCF_MODEL;
            (param[14] = new OracleParameter("P_GCF_BRAND", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet.GCF_BRAND;
            param[15] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("SP_SAVE_ENQUIRYFLEETS", CommandType.StoredProcedure, param);
            return effects;
        }

        public DataTable getEnquiryHeaderData(string enqNo, string pc)
        {
            OracleParameter[] param = new OracleParameter[3];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqNo;
            (param[1] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_ENQDATA", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DataTable getEnquiryInvoiceItems(string enqNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_ENQID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqNo;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_ENQINVOICEDATA", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public DEPO_AMT_DATA getLiabilityDetails(string liability)
        {
            try
            {
                DEPO_AMT_DATA lib = new DEPO_AMT_DATA();
                OracleParameter[] param = new OracleParameter[2];
                DataTable _dtResults = new DataTable();
                (param[0] = new OracleParameter("p_libtamt", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(liability);
                param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tbl", "GET_DEPOSITAMTDATA", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    lib = DataTableExtensions.ToGenericList<DEPO_AMT_DATA>(_dtResults, DEPO_AMT_DATA.Converter)[0];
                }
                return lib;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DEPO_AMT_DATA getLiabilityDatabyChgCd(string chgCd)
        {
            DEPO_AMT_DATA lib = new DEPO_AMT_DATA();
            OracleParameter[] param = new OracleParameter[2];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("p_chgcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgCd;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "GET_DEPOSITAMTDATABYCD", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                lib = DataTableExtensions.ToGenericList<DEPO_AMT_DATA>(_dtResults, DEPO_AMT_DATA.Converter)[0];
            }
            return lib;
        }
        public MasterBusinessEntity GetBusinessCompanyDetail(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType)
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_busetycode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _businessCompanyCode;
            (param[2] = new OracleParameter("p_NIC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _nic;
            (param[3] = new OracleParameter("p_mobile", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mobile;
            (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _businessType;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tblBusinessCompany = QueryDataTable("tblBusinessCompany", "sp_GetCustomerDetail", CommandType.StoredProcedure, false, param);
            if (_tblBusinessCompany.Rows.Count > 0)
            {
                _masterBusinessCompany = DataTableExtensions.ToGenericList<MasterBusinessEntity>(_tblBusinessCompany, MasterBusinessEntity.Converternew)[0];
            }
            return _masterBusinessCompany;
        }

        public MasterBusinessEntity GetCustomerProfile(string CustCD, string nic, string DL, string PPNo, string brNo)
        {// p_CustCD in NVARCHAR2,p_nic in NVARCHAR2,p_dl in NVARCHAR2,p_passport in NVARCHAR2,c_data OUT sys_refcursor
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_CustCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CustCD;
            (param[1] = new OracleParameter("p_nic", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = nic;
            (param[2] = new OracleParameter("p_dl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = DL;
            (param[3] = new OracleParameter("p_passport", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = PPNo;
            (param[4] = new OracleParameter("p_brNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = brNo;

            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbData", "sp_getCustomerProfile", CommandType.StoredProcedure, false, param);

            MasterBusinessEntity bizEnt = new MasterBusinessEntity();
            if (_dtResults.Rows.Count > 0)
            {
                bizEnt = DataTableExtensions.ToGenericList<MasterBusinessEntity>(_dtResults, MasterBusinessEntity.Converternew)[0];
            }
            return bizEnt;
        }

        //subodana 2016-12-19
        public DataTable getAgreementParameter(Int32 agreno)
        {
            OracleParameter[] param = new OracleParameter[2];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("p_agreement_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = agreno;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "sp_get_agr_para", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //subodana 2017-01-06
        public Int32 Save_BusChargecd(List<Cus_chg_cds> chg, string userid)
        {
            try
            {
                Int32 effects = 0;
                foreach (var chgob in chg)
                {
                OracleParameter[] param = new OracleParameter[5];
                (param[0] = new OracleParameter("P_CUSCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgob.bcd_cus_cd;
                (param[1] = new OracleParameter("P_TYPE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgob.bcd_chg_type;
                (param[2] = new OracleParameter("P_CHGCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chgob.bcd_chg_cd;
                (param[3] = new OracleParameter("P_CREBY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
                effects = (Int32)UpdateRecords("TBS.SP_SAVE_BUSCHGCD", CommandType.StoredProcedure, param);
                }
               return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-01-06
        public Int32 Delete_BusChargecd(string code, string userid)
        {
            try
            {
                    Int32 effects = 0;
                    OracleParameter[] param = new OracleParameter[3];
                    (param[0] = new OracleParameter("P_CUSCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = code;
                    (param[1] = new OracleParameter("P_MODBY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                    param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
                    effects = (Int32)UpdateRecords("TBS.SP_DELETE_BUSCHGCD", CommandType.StoredProcedure, param);
                return effects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Cus_chg_cds> GETBUSCHARGECODES(string cuscd)
        {
            try
            {
                List<Cus_chg_cds> chgcds = new List<Cus_chg_cds>();
                OracleParameter[] param = new OracleParameter[2];
                DataTable _dtResults = new DataTable();
                (param[0] = new OracleParameter("P_CUSCD", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = cuscd;
                param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                _dtResults = QueryDataTable("tbl", "SP_GETCUSBUSCHG", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    chgcds = DataTableExtensions.ToGenericList<Cus_chg_cds>(_dtResults, Cus_chg_cds.Converter);
                }
                return chgcds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //2017-01-11 subodana
        public List<DriverAllocationHome> GET_TOURS_DRIALLOC(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal, String Status)
        {
            string search = "";
            if (searchFld == "reqDte" || searchFld == "expDte")
            {
                search = (!string.IsNullOrEmpty(searchVal)) ? Convert.ToDateTime(searchVal).ToString("yyyy/MM/dd") : DateTime.MinValue.ToString("yyyy/MM/dd");
            }
            else
            {
                search = (!string.IsNullOrEmpty(searchVal)) ? "%" + searchVal + "%" : "";
            }

            switch (searchFld)
            {
                case "Vehicle No":
                    searchFld = "mstf_regno";
                    break;
                case "Model":
                    searchFld = "mstf_model";
                    break;
                case "Category":
                    searchFld = "mstf_veh_tp";
                    break;
                case "System Id":
                    searchFld = "gce_enq_id";
                    break;
                default:
                    searchFld = "";
                    break;

            }
            List<DriverAllocationHome> enq = new List<DriverAllocationHome>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_pgenum", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeNum;
            (param[1] = new OracleParameter("p_pagesize", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pgeSize;
            (param[2] = new OracleParameter("p_field", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchFld;
            (param[3] = new OracleParameter("p_search", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = search;
            (param[4] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[5] = new OracleParameter("p_proCen", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userDefPro;
            (param[6] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Status;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tblenq", "SP_SEARCH_DRIVERALLOC", CommandType.StoredProcedure, false, param);

            if (_dtResults.Rows.Count > 0)
            {
                enq = DataTableExtensions.ToGenericList<DriverAllocationHome>(_dtResults, DriverAllocationHome.Converter);
            }
            return enq;
        }
        //subodana 2017-01-13
        public List<FleetAlert> FleetAlertdata(DateTime date, string type)
        {
            try
            {
                List<FleetAlert> fleets = new List<FleetAlert>();
                OracleParameter[] param = new OracleParameter[2];
                DataTable _dtResults = new DataTable();
                (param[0] = new OracleParameter("P_CUSCD", OracleDbType.Date, null, ParameterDirection.Input)).Value = date;
                param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
                if (type == "REG")
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_FleetAlertRegExp", CommandType.StoredProcedure, false, param);
                }
                else
                {
                    _dtResults = QueryDataTable("tbl", "SP_GET_FleetAlertInsExp", CommandType.StoredProcedure, false, param);
                }
                if (_dtResults.Rows.Count > 0)
                {
                    fleets = DataTableExtensions.ToGenericList<FleetAlert>(_dtResults, FleetAlert.Converter);
                }
                return fleets;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //subodana 2017-JAN-19
        public DataTable GETINVCHARGES(string INVNO)
        {
            OracleParameter[] param = new OracleParameter[2];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_INVOICENO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = INVNO;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "getcompanyInvoicedetails", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //subodana 2017-JAN-20
        public DataTable GETFLEETGES(string fleet, DateTime fdate, DateTime tdate)
        {
            OracleParameter[] param = new OracleParameter[4];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_FLEET", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet;
            (param[1] = new OracleParameter("P_FDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = fdate;
            (param[2] = new OracleParameter("P_TDATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = tdate;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "MonthlyFleetCharges", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable CHECK_DRIVER_ALLOC(string fleet, DateTime fdate, DateTime tdate)
        {
            OracleParameter[] param = new OracleParameter[4];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_VEHICLE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet;
            (param[1] = new OracleParameter("P_EXPDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = fdate;
            (param[2] = new OracleParameter("P_RETDT", OracleDbType.Date, null, ParameterDirection.Input)).Value = tdate;
            param[3] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_CHECK_DRIVER_ALLOC", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //subodana 2017-JAN-24
        public DataTable IsInvoiced1(string com, string enqid)
        {
            OracleParameter[] param = new OracleParameter[3];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_EnqId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqid;
            param[2] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_CheckisInvoiced1", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //subodana 2017-JAN-24
        public DataTable IsInvoiced2(string enqid)
        {
            OracleParameter[] param = new OracleParameter[2];
            DataTable _dtResults = new DataTable();
            (param[0] = new OracleParameter("P_EnqId", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = enqid;
            param[1] = new OracleParameter("C_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("tbl", "SP_GET_CheckisInvoiced2", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        // ISURU 2017/02/21
        public DataTable GET_PRINT_DATA(string id, string company)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("P_COMPANY ", OracleDbType.Char, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_INV_NO ", OracleDbType.Char, null, ParameterDirection.Input)).Value = id;
            param[2] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("dt", "SP_CREDITNOTE_PRINT", CommandType.StoredProcedure, false, param);
        }
        //ISURU 2017/02/22
        public DataTable GET_TRIPREQUEST_DATA(DateTime fromdate, DateTime todate, string company, string profcen)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COMPANY ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_FROM_DATE ", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
            (param[2] = new OracleParameter("P_TO_DATE ", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[3] = new OracleParameter("P_PC ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profcen;
            param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("dt", "SP_GET_TRIPREQUEST", CommandType.StoredProcedure, false, param);
        }
   
        //ISURU 2017/02/24

        public DataTable GET_LOGSHEET_DATA(DateTime fromdate, DateTime todate, string company, string profcen)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COMPANY ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[1] = new OracleParameter("P_FROM_DATE ", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
            (param[2] = new OracleParameter("P_TO_DATE ", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[3] = new OracleParameter("P_PC ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profcen;
            param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("dt", "SP_GET_LOGSHEET", CommandType.StoredProcedure, false, param);
        }

        // ISURU 2017/02/27

        public DataTable HOME_CONFIG_DATA(string user,string company, string profcen,string type)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_USER ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[1] = new OracleParameter("P_PC ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = profcen;
            (param[2] = new OracleParameter("P_COM ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
             (param[3] = new OracleParameter("P_TYPE ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;
             param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
             return QueryDataTable("dt", "SP_HOME_CONFIGER", CommandType.StoredProcedure, false, param);
        }

        // ISURU 2017/02/27
        public DataTable LEASED_CAR_DATA(DateTime fromdate, DateTime todate, string com, string fleet)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_COMPANY ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
            (param[1] = new OracleParameter("P_FROM_DATE ", OracleDbType.Date, null, ParameterDirection.Input)).Value = fromdate;
            (param[2] = new OracleParameter("P_TO_DATE ", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
            (param[3] = new OracleParameter("P_FLEET ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = fleet;
            param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("dt", "SP_GET_LEASEDCAR_PAYMENTS", CommandType.StoredProcedure, false, param);
        }
    
        //ISURU 2017-02-28
        public List<FleetAllocDaily> FleetAllocDailydata(string com, DateTime fdate, DateTime todate, string prc)
        {
            try
            {
                List<FleetAllocDaily> fleets = new List<FleetAllocDaily>();
                OracleParameter[] param = new OracleParameter[5];
                DataTable _dtResults = new DataTable();
                (param[0] = new OracleParameter("P_COMPANY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[1] = new OracleParameter("P_FROM_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = fdate;
                (param[2] = new OracleParameter("P_TO_DATE", OracleDbType.Date, null, ParameterDirection.Input)).Value = todate;
                (param[3] = new OracleParameter("P_PC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = prc;
                param[4] = new OracleParameter("O_DATA", OracleDbType.RefCursor, null, ParameterDirection.Output);

                _dtResults = QueryDataTable("tbl", "SP_GET_FleetAlloc_Dailydata", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    fleets = DataTableExtensions.ToGenericList<FleetAllocDaily>(_dtResults, FleetAllocDaily.Convertor);
                }
                return fleets;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}






