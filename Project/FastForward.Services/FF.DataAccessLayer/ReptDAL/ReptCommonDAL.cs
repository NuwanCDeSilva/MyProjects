using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Oracle.DataAccess.Client;
using FF.BusinessObjects;
using FF.BusinessObjects.ReptObj;
using FF.BusinessObjects.General;

namespace FF.DataAccessLayer
{
    /// <summary>
    /// This is a Data Access Layer Report db class for common functionalty.
    /// Created By : Chamal De Silva.
    /// Created On : 21/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    public class ReptCommonDAL : ReptDAL
    {
        //kapila
        public DataTable getTempPOItems(string _com, string _PO)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_PO", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _PO;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _result = QueryDataTable("tbl", "sp_get_temp_po_itms", CommandType.StoredProcedure, false, param);
            return _result;
        }
        //Chamal 
        #region Save picked/Scan Serial List

        public Int16 SavePickedItemSerials(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[59];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
            (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
            (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
            (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;
            (param[38] = new OracleParameter("p_ser_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_remarks;
            (param[39] = new OracleParameter("p_resqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_resqty;
            (param[40] = new OracleParameter("p_ageloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc;
            (param[41] = new OracleParameter("p_ageloc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc_dt.Date;
            (param[42] = new OracleParameter("p_isownmrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_isownmrn;

            (param[43] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_no;//Chamal 20-Jan-2015
            (param[44] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_line;//Chamal 20-Jan-2015
            (param[45] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_no;//Chamal 20-Jan-2015
            (param[46] = new OracleParameter("p_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_line;//Chamal 20-Jan-2015

            (param[47] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_no;//kapila 4/7/2015
            (param[48] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exp_dt.Date;
            (param[49] = new OracleParameter("p_manufac_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_manufac_dt.Date;   //kapila 20/7/2015

            (param[50] = new OracleParameter("p_tus_is_pgs", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_is_pgs;//Rukshan 1/8/2015
            (param[51] = new OracleParameter("p_tus_pgs_count", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_count;//Rukshan 1/8/2015
            (param[52] = new OracleParameter("p_tus_pg_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_prefix;   //Rukshan 1/8/2015
            (param[53] = new OracleParameter("p_tus_st_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_st_pg;   //Rukshan 1/8/2015
            (param[54] = new OracleParameter("p_tus_ed_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ed_pg;   //Rukshan 1/8/2015
            (param[55] = new OracleParameter("p_tus_new_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_itm_cd;   //Sahan 14/09/2015

            (param[56] = new OracleParameter("P_tus_bin_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin_to;   //Tharaka 2015-11-05
            (param[57] = new OracleParameter("P_TUS_NEW_ITM_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_base_new_ser;   //Tharaka 2015-11-05
            param[58] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_picksernewnew", CommandType.StoredProcedure, param);
            //return (Int16)this.UpdateRecords("sp_picksernewnew_test", CommandType.StoredProcedure, param);
        }
        //Lakshan 08 Dec 2016 Mac save
        public Int16 SavePickedItemSerialsMac(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[60];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
            (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
            (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
            (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;
            (param[38] = new OracleParameter("p_ser_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_remarks;
            (param[39] = new OracleParameter("p_resqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_resqty;
            (param[40] = new OracleParameter("p_ageloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc;
            (param[41] = new OracleParameter("p_ageloc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc_dt.Date;
            (param[42] = new OracleParameter("p_isownmrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_isownmrn;

            (param[43] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_no;//Chamal 20-Jan-2015
            (param[44] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_line;//Chamal 20-Jan-2015
            (param[45] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_no;//Chamal 20-Jan-2015
            (param[46] = new OracleParameter("p_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_line;//Chamal 20-Jan-2015

            (param[47] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_no;//kapila 4/7/2015
            (param[48] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exp_dt.Date;
            (param[49] = new OracleParameter("p_manufac_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_manufac_dt.Date;   //kapila 20/7/2015

            (param[50] = new OracleParameter("p_tus_is_pgs", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_is_pgs;//Rukshan 1/8/2015
            (param[51] = new OracleParameter("p_tus_pgs_count", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_count;//Rukshan 1/8/2015
            (param[52] = new OracleParameter("p_tus_pg_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_prefix;   //Rukshan 1/8/2015
            (param[53] = new OracleParameter("p_tus_st_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_st_pg;   //Rukshan 1/8/2015
            (param[54] = new OracleParameter("p_tus_ed_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ed_pg;   //Rukshan 1/8/2015
            (param[55] = new OracleParameter("p_tus_new_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_itm_cd;   //Sahan 14/09/2015

            (param[56] = new OracleParameter("P_tus_bin_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin_to;   //Tharaka 2015-11-05
            (param[57] = new OracleParameter("P_Tus_pkg_uom_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pkg_uom_tp;   //Tharaka 2015-11-05
            (param[58] = new OracleParameter("P_Tus_pkg_uom_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pkg_uom_qty;   //Tharaka 2015-11-05

            param[59] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("SP_PICKSERNEWNEW_MAC", CommandType.StoredProcedure, param);
        }


        public Int16 SavePickedSerialsDecimalItems(ReptPickSerials _scanserNew)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[34];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            param[33] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            //Open the connection and call the stored procedure.

            rows_affected = (Int16)this.UpdateRecords("sp_upate_pickser_decimal_item", CommandType.StoredProcedure, param);

            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}

            return rows_affected;
        }

        #endregion

        //Written By Prabhath on 29/03/2012
        //Modify by Rukshan on 01/oct/2015
        #region Save picked/Scan Sub Serial List
        public Int16 SavePickedSubItemSerials(List<ReptPickSerialsSub> _reptPickSerialsSub)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            foreach (ReptPickSerialsSub _details in _reptPickSerialsSub)
            {
                OracleParameter[] param = new OracleParameter[13];
                (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_itm_cd;
                (param[1] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_itm_stus;
                (param[2] = new OracleParameter("p_m_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_m_itm_cd;
                (param[3] = new OracleParameter("p_m_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_m_ser;
                (param[4] = new OracleParameter("p_mfc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_mfc;
                (param[5] = new OracleParameter("p_serial", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_sub_ser;
                (param[6] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_tp;
                (param[7] = new OracleParameter("p_userseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _details.Tpss_usrseq_no;
                (param[8] = new OracleParameter("p_warano", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_warr_no;
                (param[9] = new OracleParameter("p_waraperiod", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _details.Tpss_warr_period;
                (param[10] = new OracleParameter("p_wararemarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _details.Tpss_warr_rem;
                (param[11] = new OracleParameter("p_serialid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _details.Tpss_warr_rem;
                param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //Open the connection and call the stored procedure.
                rows_affected = (Int16)this.UpdateRecords("sp_picksubser", CommandType.StoredProcedure, param);
            }


            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}

            return rows_affected;
        }
        #endregion

        //Chamal 
        #region Save picked/Scan Item List

        public Int16 SavePickedItems(ReptPickItems _scanitemNew)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_usrseq_no;
            (param[1] = new OracleParameter("p_req_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_req_itm_cd;
            (param[2] = new OracleParameter("p_req_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_req_itm_stus;
            (param[3] = new OracleParameter("p_req_itm_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_req_itm_qty;
            (param[4] = new OracleParameter("p_pic_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_pic_itm_cd;
            (param[5] = new OracleParameter("p_pic_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_pic_itm_stus;
            (param[6] = new OracleParameter("p_pic_itm_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_pic_itm_qty;
            (param[7] = new OracleParameter("p_sup", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_sup;
            (param[8] = new OracleParameter("p_batch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_batch;
            (param[9] = new OracleParameter("p_grn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_grn;
            (param[10] = new OracleParameter("p_grn_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_grn_dt;
            (param[11] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_exp_dt;

            param[12] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            //Open the connection and call the stored procedure.

            rows_affected = (Int16)this.UpdateRecords("sp_pickitm_new", CommandType.StoredProcedure, param);
            
            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}
            //finally
            //{
            //    ConnectionClose();
            //}

            return rows_affected;
        }
        #endregion

        //Chamal 
        #region Save picked/Scan Item Header

        public Int16 SavePickedHeader(ReptPickHeader _scanheaderNew)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.

            //Added p_tuh_wh_com,p_tuh_wh_loc,p_tuh_load_bay On 9th Nov 2015 (Sahan)

            OracleParameter[] param = new OracleParameter[22];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usrseq_no;
            (param[1] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usr_id;
            (param[2] = new OracleParameter("p_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usr_com;
            (param[3] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_session_id;
            (param[4] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            (param[5] = new OracleParameter("p_doc_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_doc_tp;
            (param[6] = new OracleParameter("p_direction", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_direct == true ? 1 : 0;
            (param[7] = new OracleParameter("p_ischeck_itemstatus", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_ischek_itmstus == true ? 1 : 0;
            (param[8] = new OracleParameter("p_ischeck_similaritem", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_ischek_simitm == true ? 1 : 0;
            (param[9] = new OracleParameter("p_ischeck_reqqty", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_ischek_reqqty;
            (param[10] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_doc_no;
            (param[11] = new OracleParameter("p_rec_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_rec_com;
            (param[12] = new OracleParameter("p_rec_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_rec_loc;
            (param[13] = new OracleParameter("p_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_isdirect;
            (param[14] = new OracleParameter("p_pro_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_pro_user;
            (param[15] = new OracleParameter("p_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usr_loc;
            (param[16] = new OracleParameter("p_dir_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_dir_type;
            (param[17] = new OracleParameter("p_tuh_wh_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_wh_com;
            (param[18] = new OracleParameter("p_tuh_wh_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_wh_loc;
            (param[19] = new OracleParameter("p_tuh_load_bay", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_load_bay;
            (param[20] = new OracleParameter("p_tuh_is_take_res", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_is_take_res == true ? 1 : 0;

            param[21] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            //Open the connection and call the stored procedure.

            rows_affected = (Int16)this.UpdateRecords("SP_PICKHDR", CommandType.StoredProcedure, param);
            
            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}
            //finally
            //{
            //    ConnectionClose();
            //}

            return rows_affected;
        }
        #endregion


        #region Delete temp pick objects
        //Chamal 29/05/2012
        public Int16 DeleteTempPickObjs(Int32 _seqno)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _seqno;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            //Open the connection and call the stored procedure.

            rows_affected = (Int16)this.UpdateRecords("sp_deletepickobjs", CommandType.StoredProcedure, param);

            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}
            //finally
            //{
            //    ConnectionClose();
            //}

            return rows_affected;
        }

        #endregion

        /// <summary>
        /// Get the scan item details
        /// </summary>
        /// <param name="_company">Company code</param>
        /// <param name="_location">Location code</param>
        /// <param name="_user">User</param>
        /// <param name="_seqno">Sequence no</param>
        /// <param name="_doctype">Document type</param>
        /// <returns>return the inventory serial scan object</returns>
        /// Wriitebn by P.Wijetunge on 22/03/2012
        public DataTable GetAllScanSerials(string _company, string _location, string _user, Int32 _userseqno, string _doctype)
        {
            // List<ReptPickSerials> _scanList = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("p_seqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userseqno;
            (param[4] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSerials", CommandType.StoredProcedure, false, param);
            return _tblScan;
        }

        /// <summary>
        /// Get the scan item details
        /// </summary>
        /// <param name="_company">Company code</param>
        /// <param name="_location">Location code</param>
        /// <param name="_user">User</param>
        /// <param name="_seqno">Sequence no</param>
        /// <param name="_doctype">Document type</param>
        /// <returns>return the inventory serial scan object</returns>
        /// Wriitebn by P.Wijetunge on 22/03/2012
        public List<ReptPickSerials> GetAllScanSerialsList(string _company, string _location, string _user, Int32 _userseqno, string _doctype)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("p_seqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userseqno;
            (param[4] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSerials", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_doctype == "UPLD")
            {
                if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConverterForMac);

            }
            else
            {
                if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            }
                //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }
        public List<ReptPickSerials> GetAllScanSerialsListNew(string _company, string _location, string _user, Int32 _userseqno, string _doctype)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("p_seqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userseqno;
            (param[4] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSerialsNew", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_doctype == "UPLD")
            {
                if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConverterForMac);

            }
            else
            {
                if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            }
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }
        //Written By Prabhath on 29/03/2012
        public DataTable GetAllScanSubSerials(Int32 _userseqno, string _doctype)
        {
            // List<ReptPickSerials> _scanList = null;
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_userseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userseqno;
            (param[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSubSerials", CommandType.StoredProcedure, false, param);
            return _tblScan;
        }

        //Written By Prabhath on 29/03/2012
        public List<ReptPickSerialsSub> GetAllScanSubSerialsList(Int32 _userseqno, string _doctype)
        {
            List<ReptPickSerialsSub> _scanList = null;
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_userseqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userseqno;
            (param[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSubSerials", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerialsSub>(_tblScan, ReptPickSerialsSub.ConvertTotal);
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;

        }

        //TODO: HW
        //Written By Prabhath on 29/03/2012
        public DataTable GetAllScanRequestItems(Int32 _userSeqNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_reqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeqNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllRequestItemDetails", CommandType.StoredProcedure, false, param);
            return _tblScan;
        }
        //TODO: HW
        public List<ReptPickItems> GetAllScanRequestItemsList(Int32 _userSeqNo)
        {
            List<ReptPickItems> _list = new List<ReptPickItems>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_reqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeqNo;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllRequestItemDetails", CommandType.StoredProcedure, false, param);
            {
                if (_tblScan.Rows.Count > 0) _list = DataTableExtensions.ToGenericList<ReptPickItems>(_tblScan, ReptPickItems.Converter);
            }
            return _list;
        }

        //Written By Prabhath on 29/03/2012
        public ReptPickHeader GetAllScanSerialParameters(string _company, string _user, Int32 _userseqno, string _doctype)
        {   //p_com In NVARCHAR2,  p_user In NVARCHAR2, p_userseqno In NUMBER, p_doctype In NVARCHAR2
            ReptPickHeader _reptPickHeader = new ReptPickHeader();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[2] = new OracleParameter("p_userseqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userseqno;
            (param[3] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblScanParam", "sp_getpickserhdr", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _reptPickHeader = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.ConvertTotal)[0];
            }
            return _reptPickHeader;

        }


        //written by shani 27/4/2012

        public Boolean Update_TEMP_PICK_SER(string compny, string location, Int32 userseq_no, Int32 ser_id, string newstatus, string newremarks)
        {
            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = compny;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = userseq_no;
            (param[3] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ser_id;
            (param[4] = new OracleParameter("p_newstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newstatus;
            (param[5] = new OracleParameter("p_newremark", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newremarks;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = UpdateRecords("SP_Update_TEMP_PICK_SER", CommandType.StoredProcedure, param);
            //ConnectionClose();
            //return effects >= 1 ? true : false;

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            //finally
            //{
            //    ConnectionClose();
            //}
            return effects >= 1 ? true : false;
        }
        //written by Shani on 2-5-2012
        public List<ReptPickSerials> Get_TEMP_PICK_SER(string _company, string _location, string _user, Int32 _userseqno, Int32 ser_id, string serial_1, string itemCD, string binCD)
        {//( p_user In NVARCHAR2,p_com in NVARCHAR2, p_loc in NVARCHAR2,p_usrseq_no in NUMBER,p_ser_id in NUMBER,p_ser_1 in VARCHAR2,p_itemcode in VARCHAR2,p_bin in VARCHAR2,c_data OUT sys_refcursor)
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;

            (param[3] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userseqno;
            (param[4] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ser_id;
            (param[5] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_1;
            (param[6] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCD;
            (param[7] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = binCD;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            //DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSerials", CommandType.StoredProcedure, false, param);
            DataTable _tblScan = QueryDataTable("tblScan", "SP_get_TEMP_PICK_SER", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }

        //written by darshana on 10-08-2012
        public List<ReptPickSerials> Get_TEMP_PICK_SER_BY_BASEDOC(string _company, string _docNo)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_basedoc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _tblScan = QueryDataTable("tblScan", "sp_get_pick_ser_by_basedoc", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }


        public List<ReptPickSerials> Get_TEMP_PICK_SER_BY_BASEDOC2(string _company, string _docNo)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_basedoc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _tblScan = QueryDataTable("tblScan", "sp_get_pick_ser_by_basedoc2", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }

        // (p_com in NVARCHAR2, p_loc in NVARCHAR2,p_usrseq_no in NUMBER,p_ser_id in NUMBER,p_itemcode in VARCHAR2,p_bin in VARCHAR2,o_effect OUT NUMBER)
        public Boolean Del_temp_pick_serdummy(string compny, string location, Int32 userSeqNo, Int32 ser_id, string itemCd, string bin)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = compny;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = userSeqNo;
            (param[3] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ser_id;
            (param[4] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[5] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bin;
            param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = UpdateRecords("sp_del_temp_pick_serdummy", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects >= 1 ? true : false;
        }

        //Chamal 15/03/2013
        public Boolean Del_temp_pick_itm(Int32 _seqNo, string _itemCode, string _itemStatus, Int32 _itemLine, Int32 _type)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqNo;
            (param[1] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[2] = new OracleParameter("p_itemstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemStatus;
            (param[3] = new OracleParameter("p_lineno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemLine.ToString();
            (param[4] = new OracleParameter("p_deltype", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _type;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = UpdateRecords("sp_del_temp_pick_itm", CommandType.StoredProcedure, param);
            ConnectionClose();
            return effects >= 1 ? true : false;
        }

        //written by Shani on 26/4/2012
        //p_com in NVARCHAR2, p_loc in NVARCHAR2,p_usrseq_no in NUMBER,p_ser_id in NUMBER,o_effect OUT NUMBER)
        public Boolean Del_temp_pick_ser(string compny, string location, Int32 userSeqNo, Int32 ser_id,string _itm)
        {
            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = compny;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[2] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = userSeqNo;
            (param[3] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = ser_id;
            (param[4] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = UpdateRecords("SP_Del_TEMP_PICK_SER", CommandType.StoredProcedure, param);
            //ConnectionClose();
            //return effects >= 1 ? true : false;

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            //finally
            //{
            //    ConnectionClose();
            //}
            return effects >= 1 ? true : false;
        }

        //written by shani on 4-5-2012
        public Boolean Update_temp_pick_serdummy(string user, string compny, string location, Int32 userSeqNo, string serial_1, string itemCd, string bin, Decimal newQty)//serial_1 is "_"
        {
            //update the qty of dummy

            //(p_user in NVARCHAR2, p_com in NVARCHAR2, p_loc in NVARCHAR2,p_usrseq_no in NUMBER,p_ser_1 in NVARCHAR2,p_itemcode in VARCHAR2,p_bin in VARCHAR2,p_new_qty in NUMBER,o_effect OUT NUMBER)
            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = user;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = compny;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
            (param[3] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = userSeqNo;
            (param[4] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = serial_1;
            (param[5] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itemCd;
            (param[6] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bin;
            (param[7] = new OracleParameter("p_new_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = newQty;
            param[8] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            ConnectionOpen();
            effects = UpdateRecords("SP_Update_TEMP_PICK_SERDUMMY", CommandType.StoredProcedure, param);
            //ConnectionClose();
            //return effects >= 1 ? true : false;

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            //finally
            //{
            //    ConnectionClose();
            //}
            return effects >= 1 ? true : false;
        }
        //written by shani on 9-5-2012
        public Int32 GET_SEQNUM_FOR_INVOICE(string doc_type, string company, string invoiceNO, int direction_)
        {
           // ConnectionOpen();
            DataTable dt = new DataTable();

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc_type;
            (param[1] = new OracleParameter("company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("invoiceNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceNO;
            (param[3] = new OracleParameter("direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction_;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            dt = QueryDataTable("tblScan", "SP_GET_SEQNUM_FOR_INVOICE", CommandType.StoredProcedure, false, param);//this returns only one value
            //ConnectionClose();

            // List<string> seqNumList = new List<string>();

            // seqNumList.Add("");
            Int32 st = -1;
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                // string st = r[0].ToString();
                st = Convert.ToInt32(r[0]);
                // seqNumList.Add(st);
            }
            return st;
            //  return seqNumList;
        }

        //written by Chamal on 13-03-2013
        public Int32 Get_Scan_SeqNo(string _company, string _location, string _doctype, string _user, int _direction, string _docNo)
        {
            ConnectionOpen();
            DataTable dt = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            (param[3] = new OracleParameter("p_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direction;
            (param[4] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            dt = QueryDataTable("tblScan", "SP_GET_SCAN_SEQ_NO", CommandType.StoredProcedure, false, param);//this returns only one value
            ConnectionClose();

            Int32 st = -1;
            foreach (DataRow r in dt.Rows)
            {
                st = Convert.ToInt32(r[0]);
            }
            return st;
        }

        //Written By Shani on 07/04/2012
        public List<string> GetAll_Adj_SeqNums_for_user(string usrID, string doc_type, int direction_, string company_)
        {
            ConnectionOpen();
            DataTable dt = new DataTable();

            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = usrID;
            (param[1] = new OracleParameter("doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc_type;
            (param[2] = new OracleParameter("direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction_;
            (param[3] = new OracleParameter("company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company_;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            dt = QueryDataTable("tblScan", "SP_ADJ_GET_ALL_SEQ_numbs", CommandType.StoredProcedure, false, param);
            ConnectionClose();

            List<string> seqNumList = new List<string>();
            //Prabhath- added empty row by Prabhath on 18/04/2012
            seqNumList.Add("");
            List<Int32> seqNos = new List<Int32>(); ;
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                Int32 st = Convert.ToInt32(r[0]);
                seqNos.Add(st);
            }
            seqNos.Sort();
            foreach (Int32 seno in seqNos)
            {
                // Get the value of the wanted column and cast it to string
                string st = seno.ToString();
                seqNumList.Add(st);
            }

            //foreach (DataRow r in dt.Rows)
            //{
            //    // Get the value of the wanted column and cast it to string
            //    string st = r[0].ToString();
            //    seqNumList.Add(st);
            //}
            return seqNumList;


        }
        //Written By Shani on 07/04/2012
        //public int Generate_new_seq_num(string usrID, string doc_type, int direction_, string company_)
        //{
        //    ConnectionOpen();
        //    OracleParameter[] param = new OracleParameter[4];

        //    (param[0] = new OracleParameter("usrid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = usrID;
        //    (param[1] = new OracleParameter("doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc_type;
        //    (param[2] = new OracleParameter("direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction_;
        //    (param[3] = new OracleParameter("company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company_;
        //    //  param[4] = new OracleParameter("seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
        //    OracleParameter outParameter = new OracleParameter("seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
        //    //param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
        //    // ReturnSP_SingleValue(String _storedProcedure, CommandType _commTypes,OracleParameter _outPara, params OracleParameter[] _parameters)
        //    int seq = ReturnSP_SingleValue("SP_SEQ_NUM_GENERATOR", CommandType.StoredProcedure, outParameter, param);
        //    ConnectionClose();
        //    return seq;


        //}

        //Written By Shani on 09/04/2012
        public Int32 SaveSeq_to_TempPickHDR(ReptPickHeader Rph)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[21];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Rph.Tuh_usrseq_no;
            (param[1] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_usr_id;
            (param[2] = new OracleParameter("p_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_usr_com;
            (param[3] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_session_id;
            (param[4] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Rph.Tuh_cre_dt;
            (param[5] = new OracleParameter("p_doc_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_doc_tp;

            //direction should be 1 or 2. Not true or false
            (param[6] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Rph.Tuh_direct;
            (param[7] = new OracleParameter("p_ischeck_itemstatus", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            (param[8] = new OracleParameter("p_ischeck_similaritem", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            (param[9] = new OracleParameter("p_ischeck_reqqty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            (param[10] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_doc_no;

            (param[11] = new OracleParameter("p_rec_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_rec_com;
            (param[12] = new OracleParameter("p_rec_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_rec_loc;
            (param[13] = new OracleParameter("p_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Rph.Tuh_isdirect;
            (param[14] = new OracleParameter("p_pro_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_pro_user;
            (param[15] = new OracleParameter("p_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_usr_loc;
            (param[16] = new OracleParameter("p_dir_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_dir_type;

            //Sahan 12/Nov/2015
            (param[17] = new OracleParameter("p_tuh_wh_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_wh_com;
            (param[18] = new OracleParameter("p_tuh_wh_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_wh_loc;
            (param[19] = new OracleParameter("p_tuh_load_bay", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = Rph.Tuh_load_bay;

            param[20] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            //Open the connection and call the stored procedure.
            //ConnectionOpen();
            rows_affected = (Int16)this.UpdateRecords("sp_pickhdr", CommandType.StoredProcedure, param);

            //}
            //catch (Exception e)
            //{
            //    rows_affected = -1;
            //    throw new Exception(e.Message);
            //}
            //finally
            //{
            //    ConnectionClose();
            //}

            return rows_affected;
        }

        //Code By Miginda on 08/05/2012
        public Int32 GetRequestUserSeqNo(string _companyCode, string _docNo)
        {
            //ConnectionOpen();
            OracleParameter[] param = new OracleParameter[2];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            OracleParameter outParameter = new OracleParameter("usr_seq_number", OracleDbType.Int32, null, ParameterDirection.Output);
            int _userSeqNo = ReturnSP_SingleValue("sp_request_usr_seqno", CommandType.StoredProcedure, outParameter, param);
            //ConnectionClose();
            return _userSeqNo;
        }

        //Code By Miginda on 08/05/2012
        public Int32 IsExistInTempPickSerial(string _companyCode, string _userSeqNo, string _itemCode, string _serialNo1)
        {
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_usr_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userSeqNo;
            (param[2] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[3] = new OracleParameter("p_serialNo1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialNo1;

            OracleParameter outParameter = new OracleParameter("row_count", OracleDbType.Int32, null, ParameterDirection.Output);
            int _row_count = ReturnSP_SingleValue("sp_isexist_in_temp_pick_ser", CommandType.StoredProcedure, outParameter, param);
            ConnectionClose();
            return _row_count;
        }


        //Prabhath

        public Int32 DeleteTempPickSerialByItem(string _company, string _location, Int32 _userSeqNo, string _item, string _status)
        {
            //sp_delete_temppickser_by_Item
            //p_com in NVARCHAR2, p_loc in NVARCHAR2,p_usrseq_no in NUMBER,p_item in NUMBER,p_status in NVARCHAR2,o_effect OUT NUMBER
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeqNo;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("sp_delete_temppickser_by_Item", CommandType.StoredProcedure, param);
        }

        //By Chamal 03-10-2014
        public Int32 DeleteTempPickSerialBySeq(int _userSeqNo)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeqNo;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (int)this.UpdateRecords("SP_DEL_TEMPPICKSER", CommandType.StoredProcedure, param);
        }

        //kapila 31/5/2012
        #region save picked manual doc details
        //public Int16 SavePickedManualDocDet(TempPickManualDocDet _manualDocDet)
        //{
        //    Int16 rows_affected = 0;

        //    OracleParameter[] param = new OracleParameter[12];
        //    (param[0] = new OracleParameter("p_mdd_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_REF;
        //    (param[1] = new OracleParameter("p_mdd_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_LINE;
        //    (param[2] = new OracleParameter("p_mdd_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_ITM_CD;
        //    (param[3] = new OracleParameter("p_mdd_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_PREFIX;
        //    (param[4] = new OracleParameter("p_mdd_bk_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_BK_NO;
        //    (param[5] = new OracleParameter("p_mdd_bk_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_BK_TP;
        //    (param[6] = new OracleParameter("p_mdd_first", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_FIRST;
        //    (param[7] = new OracleParameter("p_mdd_last", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_LAST;
        //    (param[8] = new OracleParameter("p_mdd_cnt", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_CNT;
        //    (param[9] = new OracleParameter("p_mdd_issue_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_ISSUE_BY;
        //    (param[10] = new OracleParameter("p_mdd_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _manualDocDet.MDD_USER;
        //    param[11] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);

        //    rows_affected = (Int16)this.UpdateRecords("sp_pick_man_doc_det", CommandType.StoredProcedure, param);

        //    return rows_affected;
        //}
        #endregion

        //Written By Prabhath on 7/06/2012
        public List<ReptPickItems> GetAllPickItem(string _company, Int32 _userSeqNo, string _docType, string _baseDoc, string _reqitem, string _reqstatus)
        {
            List<ReptPickItems> _list = new List<ReptPickItems>();
            //sp_getallscanitem  (p_com In NVARCHAR2,  p_seqno In NUMBER, p_doctype In NVARCHAR2,p_reqno in NVARCHAR2,p_reqitem in NVARCHAR2,p_reqstatus in NVARCHAR2,c_data OUT SYS_REFCURSOR)

            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeqNo;
            (param[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docType;
            (param[3] = new OracleParameter("p_reqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _baseDoc;
            (param[4] = new OracleParameter("p_reqitem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqitem;
            (param[5] = new OracleParameter("p_reqstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqstatus;
            param[6] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dtResults", "sp_getallscanitem", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickItems>(_dtResults, ReptPickItems.Converter);
            }
            return _list;
        }

        public Int32 UpdateBaseDocumentDetail(Int32 _userSeqNo, Int32 _baseDocLine, string _baseDoc, string _reqitem, string _reqstatus)
        {
            //SP_Update_basedocdetail(p_userseqno in NUMBER,p_lineno in NUMBER,p_basedocno in NVARCHAR2,p_item in NVARCHAR2,p_status in NVARCHAR2,o_effect OUT NUMBER)
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_userseqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userSeqNo;
            (param[1] = new OracleParameter("p_lineno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _baseDocLine;
            (param[2] = new OracleParameter("p_basedocno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _baseDoc;
            (param[3] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqitem;
            (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqstatus;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("sp_update_basedocdetail", CommandType.StoredProcedure, param);

        }

        public List<ReptPickItems> IsPickQtyExceedRequestQty(Int32 _userSeq, string _reqItem, string _reqStatus)
        {
            List<ReptPickItems> _list = new List<ReptPickItems>();

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeq;
            (param[1] = new OracleParameter("p_reqitem", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqItem;
            (param[2] = new OracleParameter("p_reqstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _reqStatus;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dtResults", "sp_checkpickandrequestqty", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickItems>(_dtResults, ReptPickItems.Converter);
            }
            return _list;
        }

        #region Vehicle Approval
        //SHANI 
        public List<ReptPickSerials> getserialByInvoiceNum(string docNo, string com)
        {
            List<ReptPickSerials> _list = new List<ReptPickSerials>();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_docNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dtResults", "sp_getByInvoiceNumToApprove", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickSerials>(_dtResults, ReptPickSerials.ConvertTotal);
            }
            return _list;

        }

        public Int32 Update_VehicleApproveStatus(string p_com, Int32 p_usrseq_no, string p_engineNo, string p_chasseNo, Int32 isApprove)
        {
            //  p_com in NVARCHAR2,p_usrseq_no in NUMBER,p_engineNo in NVARCHAR2,p_chasseNo in NVARCHAR2,p_isapp in NUMBER,o_effect OUT NUMBER

            Int32 effects = 0;

            //try
            //{
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_com;
            (param[1] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = p_usrseq_no;
            (param[2] = new OracleParameter("p_engineNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_engineNo;
            (param[3] = new OracleParameter("p_chasseNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = p_chasseNo;
            (param[4] = new OracleParameter("isApprove", OracleDbType.Int32, null, ParameterDirection.Input)).Value = isApprove;

            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            // ConnectionOpen();
            effects = UpdateRecords("SP_UpdateVehicleApproveStatus", CommandType.StoredProcedure, param);
            //ConnectionClose();
            //return effects >= 1 ? true : false;

            //}
            //catch (Exception)
            //{
            //    return -1;
            //}
            //finally
            //{
            //    //ConnectionClose();
            //}
            return effects;
        }
        #endregion

        //Written By Prabhath on 01/09/2012
        public Int32 UpdateAdvanceReceiptNofromInvoice(string _receiptno, string _invoiceseqno, string _invoiceno)
        {
            //SP_UPDATE_RECEIPTNO2INVOICE(p_receiptno in NVARCHAR2,p_invoiceseqno in NVARCHAR2,p_invocieno in NVARCHAR2,o_effect out NUMBER)
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_receiptno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _receiptno;
            (param[1] = new OracleParameter("p_invoiceseqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceseqno;
            (param[2] = new OracleParameter("p_invoiceno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceno;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("SP_UPDATE_RECEIPTNO2INVOICE", CommandType.StoredProcedure, param);
        }

        public Int32 UpdateTemCoverNote(string _invNo, string _com)
        {
            int _effect = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_base_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invNo;
            (param[2] = new OracleParameter("p_status", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return _effect = UpdateRecords("sp_updatecvnotestus", CommandType.StoredProcedure, param);
        }

        public List<ReptPickSerials> GetInvoiceAdvanceReceiptSerial(string _company, string _invoiceno)
        {
            //sp_getinvoicereceiptserial(p_com in NVARCHAR2, p_invoiceno in NVARCHAR2,c_data out sys_refcursor)
            List<ReptPickSerials> _list = new List<ReptPickSerials>();

            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_invoiceno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _invoiceno;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("dtResults", "sp_getinvoicereceiptserial", CommandType.StoredProcedure, false, param);
            if (_dtResults.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickSerials>(_dtResults, ReptPickSerials.ConvertTotal);
            }
            return _list;
        }

        public DataTable GetTemSerStatus(string _com, string _loc, string _item, int _seq)
        {
            DataTable _list = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _list = QueryDataTable("tbl", "sp_getitemstatus", CommandType.StoredProcedure, false, param);
            return _list;
        }

        public DataTable GetTemSerByCodeStatus(string _com, string _loc, string _item, string _stus, int _seq)
        {
            DataTable _list = null;
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _stus;
            (param[4] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _list = QueryDataTable("tbl", "sp_getitem_code_status", CommandType.StoredProcedure, false, param);
            return _list;
        }

        public int UpdateUnitCost(string _com, string _loc, string _item, string _stus, string _ser, decimal _cost, int _seq)
        {
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[3] = new OracleParameter("p_ser1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser;
            (param[4] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            (param[5] = new OracleParameter("p_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _cost;
            param[6] = new OracleParameter("effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updateunitcost", CommandType.StoredProcedure, param);
        }

        public Int32 DeleteTempPickItembyItem(Int32 _userseqno, string _item, string _status)
        {
            // sp_deletepickitembyitem(p_userseq in NUMBER,p_item in NVARCHAR2,p_status in NVARCHAR2,o_effect out NUMBER)
            int _effect = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_userseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userseqno;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return _effect = UpdateRecords("sp_deletepickitembyitem", CommandType.StoredProcedure, param);
        }

        public Int32 UpdateTempPickItem(Int32 _userseqno, string _item, string _status, decimal _qty)
        {
            //sp_updatepickitem(p_userseq in NUMBER,p_item in NVARCHAR2,p_status in NVARCHAR2,p_qty in NUMBER ,o_effect out NUMBER)
            int _effect = 0;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_userseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userseqno;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[2] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _status;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return _effect = UpdateRecords("sp_updatepickitem", CommandType.StoredProcedure, param);
        }

        public Int32 IsExistWarrantyInTempPickSerial(string _companyCode, string _warranty)
        {
            //sp_isexist_warra (p_com In NVARCHAR2,  p_warranty In NVARCHAR2,  row_count OUT NUMBER)
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_warranty", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _warranty;
            OracleParameter outParameter = new OracleParameter("row_count", OracleDbType.Int32, null, ParameterDirection.Output);
            int _row_count = ReturnSP_SingleValue("sp_isexist_warra", CommandType.StoredProcedure, outParameter, param);
            ConnectionClose();
            return _row_count;
        }

        public int UpdateCashConvertionDocNo(string oldDoc, string newDoc)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_pre_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oldDoc;
            (param[1] = new OracleParameter("p_new_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newDoc;
            param[2] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_update_docno", CommandType.StoredProcedure, param);
        }

        public Int32 DeleteResSerial(string _inv, string _com, string _itm, string _engine, string _chassis)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inv;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[3] = new OracleParameter("p_engine", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _engine;
            (param[4] = new OracleParameter("p_chassis", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _chassis;

            param[5] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_delete_ser_det", CommandType.StoredProcedure, param);
        }

        public Int32 DeleteResHdr(string _com, string _inv)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _inv;

            param[2] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_delete_ser_hdr", CommandType.StoredProcedure, param);
        }

        public Int32 DeleteTempValBal( string _user)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            
            param[1] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_delete_temp_val_bal", CommandType.StoredProcedure, param);
        }
        public Int32 DeleteTempValBalOut(string _user)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            param[1] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_delete_temp_val_bal_out", CommandType.StoredProcedure, param);
        }
        public Int32 Deletetemp_bmt_inv_bal_insu(string _user)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;

            param[1] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_del_temp_bmt_inv_bal_insu", CommandType.StoredProcedure, param);
        }

        //Written By Chamal on 19/03/2013
        public List<string> Get_User_Seq_Batch(string _user, string _docType, int _direction_, string _company, string _location)
        {
            ConnectionOpen();
            DataTable dt = new DataTable();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docType;
            (param[2] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direction_;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[4] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            dt = QueryDataTable("tblScan", "sp_get_user_seq_batch", CommandType.StoredProcedure, false, param);
            ConnectionClose();

            List<string> seqNumList = new List<string>();
            //Prabhath- added empty row by Prabhath on 18/04/2012
            seqNumList.Add("");
            List<Int32> seqNos = new List<Int32>(); ;
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                Int32 st = Convert.ToInt32(r[0]);
                seqNos.Add(st);
            }
            seqNos.Sort();
            foreach (Int32 seno in seqNos)
            {
                // Get the value of the wanted column and cast it to string
                string st = seno.ToString();
                seqNumList.Add(st);
            }

            return seqNumList;
        }

        public DataTable GetUnReadMessages(string _receiver, string _company, string _location, string _profitcenter)
        {
            // sp_getunreadmessages(p_receiver in NVARCHAR2, p_company in NVARCHAR2,p_loc in NVARCHAR2,p_pc in NVARCHAR2, c_data out sys_refcursor )
            DataTable _list = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_receiver", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _receiver;
            (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[3] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _list = QueryDataTable("tbl", "sp_getunreadmessages", CommandType.StoredProcedure, false, param);

            return _list;

        }

        public Int32 UpdateViewedMessage(string _document)
        {
            // sp_updateviewedmessage(p_doc in NVARCHAR2, o_effect out NUMBER)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updateviewedmessage", CommandType.StoredProcedure, param);

        }

        public DataTable GetAvailableToken(DateTime _date, string _company, string _profitcenter, int _token)
        {
            //sp_getavailabletoken(p_date in date,p_com in NVARCHAR2,p_pc in NVARCHAR2,p_token in NUMBER,c_data out sys_refcursor)
            DataTable _list = null;
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date.Date;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[2] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _profitcenter;
            (param[3] = new OracleParameter("p_token", OracleDbType.Int64, null, ParameterDirection.Input)).Value = _token;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _list = QueryDataTable("tblToken", "sp_getavailabletoken", CommandType.StoredProcedure, false, param);
            return _list;
        }

        public int StartTimeModule(string _modName, string _funcName, DateTime _stTime, string _loc, string _com, string _user, DateTime _funcDate)
        {
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_mod_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _modName;
            (param[1] = new OracleParameter("p_funct_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _funcName;
            (param[2] = new OracleParameter("p_st_time", OracleDbType.Date, null, ParameterDirection.Input)).Value = _stTime;
            (param[3] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            (param[4] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[5] = new OracleParameter("p_user_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[6] = new OracleParameter("p_mod_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _funcDate.Date;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_savescm2modtimecap", CommandType.StoredProcedure, param);
        }

        public int EndTimeModule(int _seqNo, DateTime _edTime, TimeSpan _diffTime)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqNo;
            (param[1] = new OracleParameter("p_ed_time", OracleDbType.Date, null, ParameterDirection.Input)).Value = _edTime;
            (param[2] = new OracleParameter("p_diff_time", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _diffTime.ToString();
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updatescm2modtimecap", CommandType.StoredProcedure, param);
        }

        public Int32 Delete_TEMP_PC_LOC(string userid, string _com, string _pc, string loc)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;

            (param[0] = new OracleParameter("p_tpl_user_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
            (param[1] = new OracleParameter("p_tpl_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_tpl_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[3] = new OracleParameter("p_tpl_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_DELETE_temp_pc_loc", CommandType.StoredProcedure, param);
            return effects;
        }

        public Int32 Save_TEMP_PC_LOC(string username, string _com, string pc, string loc)
        {
            //p_tpl_user_name, p_tpl_pc, p_tpl_loc

            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_tpl_user_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = username;
            (param[1] = new OracleParameter("p_tpl_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[2] = new OracleParameter("p_tpl_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = loc;
            (param[3] = new OracleParameter("p_tpl_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_save_temp_pc_loc", CommandType.StoredProcedure, param);
            return effects;

        }

        public DataTable GetPOSAccDetFromRepDB(string _com, string _cus, string _cusTp)
        {
            DataTable _list = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_cus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cus;
            (param[2] = new OracleParameter("p_cusTp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _cusTp;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _list = QueryDataTable("tblToken", "sp_getposaccfromrepdb", CommandType.StoredProcedure, false, param);
            return _list;
        }

        public DataTable GetPickHeaderByDocument(string _company, string _document,string loc=null)
        {
            //sp_getheaderfromdoc(p_document in NVARCHAR2,p_com in NVARCHAR2,c_data out sys_refcursor )
            DataTable _db = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_document", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _db = QueryDataTable("tbl_header", "sp_getheaderfromdoc", CommandType.StoredProcedure, false, param);
            return _db;
        }

        //Randima 15-Sep-2016
        public DataTable GetPickSerByDocument(string _company, string _document)
        {
            //sp_getheaderfromdoc(p_document in NVARCHAR2,p_com in NVARCHAR2,c_data out sys_refcursor )
            DataTable _db = null;
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_document", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _db = QueryDataTable("tbl_header", "sp_getheaderfromdocWithSer", CommandType.StoredProcedure, false, param);
            return _db;
        }

        public DataTable GetDirectUnFinishedDocument(string _company, string _location, string _documenttype)
        {//sp_getdirectdocument(p_com in NVARCHAR2,p_loc in NVARCHAR2,p_doctype in NVARCHAR2,c_data out sys_refcursor )
            DataTable _db = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _documenttype;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _db = QueryDataTable("tbl_header", "sp_getdirectdocument", CommandType.StoredProcedure, false, param);
            return _db;
        }

        public DataTable GetScanDocInfor(string _company, string _location, string _document, int _serid)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[3] = new OracleParameter("p_serid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serid;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            return QueryDataTable("tbl", "SP_GET_SCAN_INFOR", CommandType.StoredProcedure, false, param);
        }

        public int Remove_Scan_Serials(int _scanSeq, int _serID)
        {
            //Chamal 13-Sep-2013
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanSeq;
            (param[1] = new OracleParameter("p_serid", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _serID;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("SP_REMOVE_SCAN_SER", CommandType.StoredProcedure, param);
        }

        public int Remove_Scan_Header(int _scanSeq)
        {
            //Chamal 13-Sep-2013
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanSeq;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("SP_REMOVE_SCAN_HDR", CommandType.StoredProcedure, param);
        }

        public DataTable GetTempUserPcRptDB(string _com, string _user)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable tblDet = QueryDataTable("tblPCEmp", "sp_gettmpuserpc", CommandType.StoredProcedure, false, param);
            return tblDet;
        }
        public DataTable GetTempUserPcRptDB_AllCom(string _user)
        {//Sanjeewa 2015-12-23
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable tblDet = QueryDataTable("tblPCEmp", "sp_gettmpuserpc_allcom", CommandType.StoredProcedure, false, param);
            return tblDet;
        }

        //kapila
        public DataTable Get_dt_inc_calc_dt(string _circular, string _ref)
        {
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_circ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _circular;
            (param[1] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tblPc", "sp_get_gnt_inc_calc_dt", CommandType.StoredProcedure, false, param);

            return _tbl;
        }
        public DataTable Get_gnt_inc_calc(string _circular, string _ref)
        {
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_circ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _circular;
            (param[1] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tblPc", "sp_get_gnt_inc_calc", CommandType.StoredProcedure, false, param);

            return _tbl;
        }
        public DataTable Get_dt_inc_calc_inc(string _circular, string _ref)
        {
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_circ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _circular;
            (param[1] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tblPc", "sp_get_dt_inc_calc_inc", CommandType.StoredProcedure, false, param);

            return _tbl;
        }
        public DataTable Get_dt_inc_calc_inv(string _circular, string _ref)
        {
            OracleParameter[] param = new OracleParameter[3];

            (param[0] = new OracleParameter("p_circ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _circular;
            (param[1] = new OracleParameter("p_ref", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ref;
            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tbl = QueryDataTable("tblPc", "sp_get_dt_inc_calc_inv", CommandType.StoredProcedure, false, param);

            return _tbl;
        }
        public Int16 Is_Prod_Bonus_Finish(string _userID)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userID;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dtResults = QueryDataTable("tblAssLoc", "sp_chk_pbonus_finish", CommandType.StoredProcedure, false, param);

            Int16 _isFinish = 0;
            if (_dtResults.Rows.Count > 0)
            {
                _isFinish = 1;
            }

            return _isFinish;
        }

        public DataTable GetProcessUser(int _seqno, string _document, string _company)
        {
            //sp_getprocessuser(p_seqno in NVARCHAR2,p_doc in NVARCHAR2,p_com in NVARCHAR2,c_data out sys_refcursor) 
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqno;
            (param[1] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _tbl = QueryDataTable("tblPc", "sp_getprocessuser", CommandType.StoredProcedure, false, param);
            return _tbl;
        }

        public Int32 UpdateProcessUser(string _user, int _seqno, string _document, string _company)
        {
            //sp_updatepickuser(p_user in NVARCHAR2,p_seqno in NUMBER,p_doc in NVARCHAR2,p_com in NVARCHAR2,o_effect out NUMBER) 
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[1] = new OracleParameter("p_seqno", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seqno;
            (param[2] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _document;
            (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updatepickuser", CommandType.StoredProcedure, param);
        }

        public Int32 DeleteTempPickSerialsBySeq(int _seq)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _seq;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_del_pick_ser_by_seq", CommandType.StoredProcedure, param);
        }

        public Int32 UpdateTokenStus(string _com, string _pc, int _token, DateTime _date)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pc;
            (param[2] = new OracleParameter("p_token", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _token;
            (param[3] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = _date;

            param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_update_token_stus", CommandType.StoredProcedure, param);
        }


        public Int32 SaveAccountAcknoledge(DataRow _dr)
        {
            OracleParameter[] param = new OracleParameter[21];
            (param[0] = new OracleParameter("p_ack_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[0].ToString();
            (param[1] = new OracleParameter("p_ack_accno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[1].ToString();
            (param[2] = new OracleParameter("p_ack_create_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = Convert.ToDateTime(_dr[2]).Date;
            (param[3] = new OracleParameter("p_ack_cust_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[3].ToString();

            (param[4] = new OracleParameter("p_ack_cust_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[4].ToString();
            (param[5] = new OracleParameter("p_ack_cust_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[5].ToString();
            (param[6] = new OracleParameter("p_ack_cust_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[6].ToString();

            (param[7] = new OracleParameter("p_ack_guar1_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[7].ToString();
            (param[8] = new OracleParameter("p_ack_guar1_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[8].ToString();
            (param[9] = new OracleParameter("p_ack_guar1_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[9].ToString();

            (param[10] = new OracleParameter("p_ack_guar2_name", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[10].ToString();
            (param[11] = new OracleParameter("p_ack_guar2_add1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[11].ToString();
            (param[12] = new OracleParameter("p_ack_guar2_add2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[12].ToString();

            (param[13] = new OracleParameter("p_ack_item_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[13].ToString();
            (param[14] = new OracleParameter("p_ack_hire_val", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _dr[14].ToString();
            (param[15] = new OracleParameter("p_ack_diriya_val", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _dr[15].ToString();

            (param[16] = new OracleParameter("p_ack_loc_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[16].ToString();
            (param[17] = new OracleParameter("p_ack_print_add_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[17].ToString();
            (param[18] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[18].ToString();
            (param[19] = new OracleParameter("p_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _dr[19].ToString();


            param[20] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_save_temp_cust_ack", CommandType.StoredProcedure, param);
        }

        //public DataTable GetRec_Age_Analysis_New(string _orderBy,string _com, string _user, string _groupintr)
        //{ 
        //   // sp_getarresreceivable(p_order in NVARCHAR2,p_user in NVARCHAR2,c_data out sys_refcursor)
        //    OracleParameter[] param = new OracleParameter[5];
        //    (param[0] = new OracleParameter("p_order", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _orderBy;
        //    (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
        //    (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
        //    (param[3] = new OracleParameter("p_groupintr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _groupintr;
        //    param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

        //    DataTable _tbl = QueryDataTable("tblPc", "sp_getarresreceivable1", CommandType.StoredProcedure, false, param);

        //    return _tbl;
        //}

        //public Int32 DeleteRecAgeAnal(string _user, string _com)
        //{//sp_deleteGLB_HP_REC_AGE1(p_user in NVARCHAR2 ,o_effect out number)
        //    OracleParameter[] param = new OracleParameter[3];
        //    (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
        //    (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
        //    param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
        //    return UpdateRecords("sp_deleteGLB_HP_REC_AGE1", CommandType.StoredProcedure, param);
        //}

        public Int32 DeleteAccountAcknoledge(string _user)
        {//sp_deleteGLB_HP_REC_AGE1(p_user in NVARCHAR2 ,o_effect out number)
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_delete_cus_acknoledge", CommandType.StoredProcedure, param);
        }

        //CHAMAL 23-May-2014
        public int UpdateScanSerialDocDate(string _com, string _user, string _docNo, DateTime _docDate, bool _isUpdateGRNDate)
        {

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _com;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[2] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            (param[3] = new OracleParameter("p_docdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = _docDate.Date;
            if (_isUpdateGRNDate == true)
            { (param[4] = new OracleParameter("p_isupdategrndate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 1; }
            else
            { (param[4] = new OracleParameter("p_isupdategrndate", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0; }
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updatedocdate", CommandType.StoredProcedure, param);
        }

        // Tharaka 11/07/2014
        public Int32 DeleteTempPromoVoucher(string CreateUser)
        {
            Int32 effects = 0;

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_spd_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = CreateUser;
            param[1] = new OracleParameter("c_data", OracleDbType.Int32, null, ParameterDirection.Output);

            effects = (Int32)UpdateRecords("sp_dlt_tempbyuser", CommandType.StoredProcedure, param);

            return effects;
        }
        //Darshana 17-02-2015
        public int UpdateDocNoAndTp(string oldDoc, string newDoc, string docTp)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_pre_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = oldDoc;
            (param[1] = new OracleParameter("p_new_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = newDoc;
            (param[2] = new OracleParameter("p_docTp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docTp;
            param[3] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updatedocnoandtp", CommandType.StoredProcedure, param);
        }
        //Nadeeka 11-11-2015
        public int UpdateitemAllocation(string _item, Decimal _qty, Int32 _dir)
        {
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            (param[1] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            (param[2] = new OracleParameter("p_dir", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _dir;
            param[3] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_itemAllocation", CommandType.StoredProcedure, param);
        }

        //Rukshan 01/Sep/2015
        public DataTable GetSubSerials(string _ICode, int _USeq, string _mainItemSeri)
        {
            // List<ReptPickSerials> _scanList = null;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ICode;
            (param[1] = new OracleParameter("p_tpss_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _USeq;
            (param[2] = new OracleParameter("p_tpss_m_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _mainItemSeri;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetSubSerials", CommandType.StoredProcedure, false, param);
            return _tblScan;
        }



        //Rukshan 05/Sep/2015

        public Int32 DeleteTempPickSubSerialByItem(Int32 _userSeqNo, string _item)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _userSeqNo;
            (param[1] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[2] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("sp_del_temp_picksubser_by_Item", CommandType.StoredProcedure, param);
        }
        //Rukshan 05/Sep/2015
        public Boolean Del_temp_pick_Subser(Int32 userSeqNo, string ser_id, string _item)
        {
            Int32 effects = 0;
            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = userSeqNo;
            (param[1] = new OracleParameter("p_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = ser_id;
            (param[2] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = UpdateRecords("SP_Del_TEMP_PICK_SUBSER", CommandType.StoredProcedure, param);
            return effects >= 1 ? true : false;
        }

        public DataTable GetItemAllocationDet(string _item)
        {

            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_item", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _item;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_ItemAllocationDet", CommandType.StoredProcedure, false, param);
            return _tblScan;
        }

        public Int16 UpdatePickedItemSerials(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            param[4] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("SP_PICKSERUPDATE", CommandType.StoredProcedure, param);
        }

        //Tharaka 2016-01-05
        public Int32 DeletePickSerByItemAndBaseItemLine(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[2] = new OracleParameter("P_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[3] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("SP_DELETE_SER_BY_ITM_STUS_LINE", CommandType.StoredProcedure, param);
        }

        //Tharaka 2016-01-05
        public Int32 DeletePickSerByItemAndItemLine(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[2] = new OracleParameter("P_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[3] = new OracleParameter("P_LINE", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("SP_DELETE_PICK_SER_BY_ITM_LINE", CommandType.StoredProcedure, param);
        }

        //Tharaka 2016-01-05
        public Int32 UPDATE_QTY_ITM_STUS_NEWSTUS(decimal Qty, Int32 Seq, string item, string stus, string stusNew)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Qty;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[2] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[3] = new OracleParameter("P_STUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stus;
            (param[4] = new OracleParameter("P_NEWSTUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stusNew;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("SP_UPDATE_QTY_ITM_STUS_NEWSTUS", CommandType.StoredProcedure, param);
        }

        //Tharaka 2016-01-08
        public Int32 UPDATE_PICK_QTY(decimal Qty, Int32 Seq, string item, string stus, Int32 IsUpdatePick)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("P_QTY", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Qty;
            (param[1] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Seq;
            (param[2] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = item;
            (param[3] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = stus;
            (param[4] = new OracleParameter("P_UPDATEPIC", OracleDbType.Int32, null, ParameterDirection.Input)).Value = IsUpdatePick;
            param[5] = new OracleParameter("O_EFFECT", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int32)this.UpdateRecords("SP_UPDATE_PICK_QTY", CommandType.StoredProcedure, param);
        }


        public Int16 UpdatePicked_Hd_doc(ReptPickHeader _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tuh_usrseq_no;
            (param[1] = new OracleParameter("P_DOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tuh_doc_no;
            (param[2] = new OracleParameter("P_WCOM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tuh_wh_com;
            (param[3] = new OracleParameter("P_WLOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tuh_wh_loc;
            (param[4] = new OracleParameter("P_LOADBAY", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tuh_load_bay;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("SP_PICKSERUPDATE_DOC", CommandType.StoredProcedure, param);
        }

        //Lakshan 24 Mar 2016
        public List<ReptPickHeader> GetReptPickHeaders(ReptPickHeader _obj)
        {   //p_com In NVARCHAR2,  p_user In NVARCHAR2, p_userseqno In NUMBER, p_doctype In NVARCHAR2
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_tuh_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_usrseq_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblScanParam", "sp_get_temp_pick_hdr", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.ConverterNew);
            }
            return _list;
        }

        //Lakshan 18 Apr 2016
        public List<ReptPickSerialsSub> GET_TEMP_PICK_SER_SUB(ReptPickSerialsSub _obj)
        {
            List<ReptPickSerialsSub> _list = new List<ReptPickSerialsSub>();
            OracleParameter[] param = new OracleParameter[5];
            (param[0] = new OracleParameter("p_tpss_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tpss_usrseq_no;
            (param[1] = new OracleParameter("p_tpss_m_itm_cd", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tpss_itm_cd;
            (param[2] = new OracleParameter("p_tpss_m_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tpss_m_ser;
            (param[3] = new OracleParameter("P_tpss_sub_ser", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tpss_sub_ser;
            param[4] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _dt = QueryDataTable("tblScanParam", "sp_get_temp_pick_ser_sub", CommandType.StoredProcedure, false, param);
            if (_dt.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickSerialsSub>(_dt, ReptPickSerialsSub.ConvertTotal);
            }
            return _list;
        }


        public Int16 UPDATEPICKSERIAL_BASEITM(ReptPickSerials _scanserNew)
        {
            Int16 rows_affected = 0;
            //try
            //{
            //Create parameters and assign values.
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("P_SEQ", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("P_BASE_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_itm_cd;
            (param[2] = new OracleParameter("P_ITM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[3] = new OracleParameter("P_BASE_ITM_LINE", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("P_UCOST", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[5] = new OracleParameter("P_UPRICE", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;  
            param[6] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            //Open the connection and call the stored procedure.

            rows_affected = (Int16)this.UpdateRecords("SP_UPDATE_BASEITEM", CommandType.StoredProcedure, param);

            return rows_affected;
        }
        //Written By Rukshan on 06/06/2016
        public List<ReptPickHeader> GetAllScanHdr(string _company, string _user, string _doctype,int _direc,string _location=null)
        {   //p_com In NVARCHAR2,  p_user In NVARCHAR2, p_userseqno In NUMBER, p_doctype In NVARCHAR2
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            (param[3] = new OracleParameter("p_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _direc;
            (param[4] = new OracleParameter("p_tuh_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblScanParam", "sp_getallhddr", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.ConverterNew);
            }
            return _list;

        }
        //Lakshan 2016 Aug 15
        public List<ReptPickHeader> GetAllScanHdrWithDateRange(ReptPickHeader _obj,Int32 _isDtRang, DateTime _dtFrom, DateTime _dtTo)
        {   //p_com In NVARCHAR2,  p_user In NVARCHAR2, p_userseqno In NUMBER, p_doctype In NVARCHAR2
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[9];
            (param[0] = new OracleParameter("p_tuh_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_id;
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_com;
            (param[2] = new OracleParameter("p_tuh_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_tp;
            (param[3] = new OracleParameter("p_tuh_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_isdirect;
            (param[4] = new OracleParameter("p_tuh_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_loc;
            (param[5] = new OracleParameter("p_is_dt_rang", OracleDbType.Int32, null, ParameterDirection.Input)).Value =  _isDtRang;
            (param[6] = new OracleParameter("p_dt_from", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtFrom;
            (param[7] = new OracleParameter("p_dt_to", OracleDbType.Date, null, ParameterDirection.Input)).Value = _dtTo;
            param[8] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _itemTable = QueryDataTable("tblScanParam", "sp_get_temp_pick_hdr_dt", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.Converter3);
            }
            return _list;
        }
        //Lakshan 2016 Aug 29
        public List<ReptPickHeader> GetReportTempPickHdr(ReptPickHeader _obj)
        {  
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_tuh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_no;
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_com;
            (param[2] = new OracleParameter("p_tuh_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_tp;
            (param[3] = new OracleParameter("p_tuh_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_isdirect;
            (param[4] = new OracleParameter("p_TUH_USRSEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_usrseq_no;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("tblScanParam", "SP_GET_TMP_PICK_HDR", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.ConverterNew);
            }
            return _list;
        }
        //Lakshan 2016 Aug 29
        public Int32 UpdateTempAodRec(Temp_aod_rec _obj)
        {
            OracleParameter[] param = new OracleParameter[8];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_madmin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.madmin;
            (param[1] = new OracleParameter("p_mloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.mloc;
            (param[2] = new OracleParameter("p_oadmin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.oadmin;
            (param[3] = new OracleParameter("p_channel", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.channel;
            (param[4] = new OracleParameter("p_out_cost", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.out_cost;
            (param[5] = new OracleParameter("p_in_cost", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.in_cost;
            (param[6] = new OracleParameter("p_user_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.user_id;
            param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_save_temp_aod_rec", CommandType.StoredProcedure, param);
            return effects;
        } 
        //Lakshan 2016 Sep 19
        public List<ReptPickSerials> GET_TEMP_PICK_SER_DATA(ReptPickSerials _repPickSer)
        {
            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_tus_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _repPickSer.Tus_usrseq_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("temp_pick_ser", "sp_get_temp_pick_ser_data", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickSerials>(_itemTable, ReptPickSerials.ConverterNew);
            }
            return _list;
        }
        //Lakshan 2016 Sep 20
        public List<ReptPickItems> GET_TEMP_PICK_ITM_DATA(ReptPickItems _repTemItm)
        {
            List<ReptPickItems> _list = new List<ReptPickItems>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_Tus_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _repTemItm.Tui_usrseq_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("temp_pick_ser", "sp_get_temp_pick_itm_data", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickItems>(_itemTable, ReptPickItems.Converter);
            }
            return _list;
        }

        //Lakshan 20 Sep 2016
        public List<ReptPickHeader> GET_TEMP_PICK_HDR_DATA(ReptPickHeader _obj)
        {
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_tuh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_no;
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_com;
            (param[2] = new OracleParameter("p_tuh_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_tp;
            (param[3] = new OracleParameter("p_tuh_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_direct;
            (param[4] = new OracleParameter("p_tuh_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_isdirect;
            (param[5] = new OracleParameter("p_TUH_USRSEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_usrseq_no;
            (param[6] = new OracleParameter("p_tuh_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_loc;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            if (string.IsNullOrEmpty(_obj.Tuh_doc_no) && string.IsNullOrEmpty(_obj.Tuh_usr_com) && _obj.Tuh_usrseq_no==0)
            {
                return _list;
            }
            else
            {
                _itemTable = QueryDataTable("tblScanParam", "sp_get_tmp_pick_hdr_data", CommandType.StoredProcedure, false, param);
                if (_itemTable.Rows.Count > 0)
                {
                    _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.Converter2);
                }
            }
            return _list;
        }
        //Lakshan 2016 Sep 22
        public Int32 UpdateTempPickSerSerVerification(ReptPickSerials _obj)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_tus_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tus_base_doc_no;
            (param[1] = new OracleParameter("p_tus_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tus_itm_cd;
            (param[2] = new OracleParameter("p_tus_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tus_itm_stus;
            (param[3] = new OracleParameter("p_tus_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tus_ser_1;
            (param[4] = new OracleParameter("p_tus_ser_ver", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tus_ser_ver;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_update_temp_pick_ser_ver", CommandType.StoredProcedure, param);
            return effects;
        }
        //Lakshan 2016 Sep 22
        public Int32 UpdateTempPickSerIdInvalid(string temp_aod_in_no, Int32 temp_aod_in_ser_id, string temp_itm_cd, string temp_aod_out_no, string temp_aod_out_ser_id)
        {
            OracleParameter[] param = new OracleParameter[6];
            Int16 effects = 0;
            (param[0] = new OracleParameter("p_temp_aod_in_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = temp_aod_in_no;
            (param[1] = new OracleParameter("p_temp_aod_in_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = temp_aod_in_ser_id;
            (param[2] = new OracleParameter("p_temp_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = temp_itm_cd;
            (param[3] = new OracleParameter("p_temp_aod_out_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = temp_aod_out_no;
            (param[4] = new OracleParameter("p_temp_aod_out_ser_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value =temp_aod_out_ser_id;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int16)UpdateRecords("sp_temp_aod_ser_id_mis_match", CommandType.StoredProcedure, param);
            return effects;
        }
        //written by RUKSHAN on 2016-10-23
        public Int32 GET_SEQNUM_FOR_INVOICE_LOC(string doc_type, string company, string invoiceNO, int direction_, string _loc)
        {
            ConnectionOpen();
            DataTable dt = new DataTable();

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doc_type;
            (param[1] = new OracleParameter("company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
            (param[2] = new OracleParameter("invoiceNo", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceNO;
            (param[3] = new OracleParameter("direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction_;
            (param[4] = new OracleParameter("LOC", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            dt = QueryDataTable("tblScan", "SP_GET_SEQNUM_FOR_LOC", CommandType.StoredProcedure, false, param);//this returns only one value
            ConnectionClose();

            // List<string> seqNumList = new List<string>();

            // seqNumList.Add("");
            Int32 st = -1;
            foreach (DataRow r in dt.Rows)
            {
                // Get the value of the wanted column and cast it to string
                // string st = r[0].ToString();
                st = Convert.ToInt32(r[0]);
                // seqNumList.Add(st);
            }
            return st;
            //  return seqNumList;
        }

        //written by RUKSHAN on 2016-10-23
        public List<ReptPickSerials> Get_TEMP_PICK_SER_BY_loc(string _company, string _docNo,string _loc)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[4];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_basedoc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _docNo;
            (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _loc;
            param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);


            DataTable _tblScan = QueryDataTable("tblScan", "sp_get_pick_ser_by_LOC", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }
        //Lakshan 20 Sep 2016
        public List<ReptPickHeader> GET_TEMP_PICK_HDR_DATA_WITH_COMPLETE_DATE(ReptPickHeader _obj)
        {
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_tuh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_no;
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_com;
            (param[2] = new OracleParameter("p_tuh_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_tp;
            (param[3] = new OracleParameter("p_tuh_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_direct;
            (param[4] = new OracleParameter("p_tuh_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_isdirect;
            (param[5] = new OracleParameter("p_TUH_USRSEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_usrseq_no;
            (param[6] = new OracleParameter("p_tuh_usr_loc", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_loc;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            if (string.IsNullOrEmpty(_obj.Tuh_doc_no) && string.IsNullOrEmpty(_obj.Tuh_usr_com) && _obj.Tuh_usrseq_no == 0)
            {
                return _list;
            }
            else
            {
                _itemTable = QueryDataTable("tblScanParam", "sp_get_tmp_pick_hdr_data", CommandType.StoredProcedure, false, param);
                if (_itemTable.Rows.Count > 0)
                {
                    _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.Converter3);
                }
            }
            return _list;
        }
        public List<ReptPickSerials> GetAllScanSerialsListForAodOut(string _company, string _location, string _user, Int32 _userseqno, string _doctype)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("p_seqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userseqno;
            (param[4] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_GetAllScanSerials", CommandType.StoredProcedure, false, param);
            //try
            //{
            if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConverterForMac);
            //}
            //catch (Exception e)
            //{
            //    throw e; //TODO: Throw the exception to general           
            //}

            return _scanList;
        }


        //RUKSHAN 
        public List<ReptPickHeader> GetReportPickHdrDetails(ReptPickHeader _obj)
        {
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_com;
            (param[1] = new OracleParameter("p_usr", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_id;
            (param[2] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_tp;
            (param[3] = new OracleParameter("p_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_isdirect;
            (param[4] = new OracleParameter("p_usrloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_loc;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("tblScanParam", "SP_GETTEMPHDR", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.Converter3);
            }
            return _list;
        }
        #region nuwan for aging process 2017.01.26

        public int getNearestBalanceSeqNo(string comp, DateTime rundate)
        {
            try
            {
                Int32 seq = -1;

                OracleParameter[] param = new OracleParameter[3];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = comp;
                (param[2] = new OracleParameter("p_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = rundate.Date;
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_BALANCEDTSEQ", CommandType.StoredProcedure, false, param);
                if (_dtResults.Rows.Count > 0)
                {
                    seq = (_dtResults.Rows[0]["PRD_SEQ_NO"] != DBNull.Value) ? Convert.ToInt32(_dtResults.Rows[0]["PRD_SEQ_NO"].ToString()) : -1;
                }
                return seq;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable  getTempGetAgeSlot(string userID)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_userID", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userID;                
                DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TMPAGESLOT", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateAllOutDocuments(DateTime runStrtDt, DateTime runEndDt, string company, string location, int direction, string userid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[7];
                (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction;
                (param[4] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runStrtDt.Date;
                (param[5] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runEndDt.Date;
                param[6] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_ALLOUTDOCUMENT", CommandType.StoredProcedure, param);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateAllInDocuments(DateTime runStrtDt, DateTime runEndDt, string company, string location, int direction, int seq, string userid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction;
                (param[4] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runStrtDt.Date;
                (param[5] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runEndDt.Date;
                (param[6] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_ALLINDOCUMENT", CommandType.StoredProcedure, param);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateAllInDocuments_Insu(DateTime runStrtDt, DateTime runEndDt, string company, string location, int direction, int seq, string userid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[8];
                (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction;
                (param[4] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runStrtDt.Date;
                (param[5] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runEndDt.Date;
                (param[6] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                param[7] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_ALLINDOCUMENT_INSU", CommandType.StoredProcedure, param);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getLocationOutTempData(string docloc, string userid, string com)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docloc;
                (param[3] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                return QueryDataTable("tbl", "SP_GET_TEMPOUTDOCUDATA", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getLocationInTempData(string _basedocno, string _baseitemcode, string _baseitemstatus, int _baseitemline, int _basebatchline, decimal unitcost, string combination,string _user)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[9];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _basedocno;
                (param[2] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _baseitemcode;
                (param[3] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _baseitemstatus;
                (param[4] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _baseitemline;
                (param[5] = new OracleParameter("p_btchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _basebatchline;
                (param[6] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = unitcost;
                (param[7] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = combination;
                (param[8] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
                return QueryDataTable("tbl", "SP_GET_TEMPINDOCUDATA", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateTempINOutBalanceQty(decimal _outqty1, int _inseq, int _outseq)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];

                (param[0] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _outqty1;
                (param[1] = new OracleParameter("p_inseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _inseq;
                (param[2] = new OracleParameter("p_outseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _outseq;
                param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //Query Data base.         
                Int32 result = UpdateRecords("SP_UPDATE_INOUTBALQTY", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateNotFoundStus(int _outseq)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[2];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _outseq;
                param[1] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //Query Data base.         
                Int32 result = UpdateRecords("SP_UPDATE_TMPDOCNOTFND", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getLocationInTempDataOutPath(string docloc, string _baseitemcode, string _baseitemstatus, decimal unitcost, string combination,string _user)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[7];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docloc;
                (param[2] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _baseitemcode;
                (param[3] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _baseitemstatus;
                (param[4] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = combination;
                (param[5] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = unitcost;
                (param[6] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
                return QueryDataTable("tbl", "SP_GET_TEMPINDOCUNCOM", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getTemporyInBalanceDocument(string com, string userid, string location)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[2] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[3] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                return QueryDataTable("tbl", "SP_GET_TEMPINDOCUBALANCE", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int addToBalanceTableStock(DataRow row, Int32 seq)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[12];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["COM"].ToString();
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["LOC"].ToString();
                (param[3] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMCODE"].ToString();
                (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMSTATUS"].ToString();
                (param[5] = new OracleParameter("p_balqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["BAL_QTY"].ToString());
                (param[6] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["UNITCOST"].ToString());
                (param[7] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["DOCNO"].ToString();
                (param[8] = new OracleParameter("p_docdt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = (row["DOCDATE"] != DBNull.Value) ? Convert.ToDateTime(row["DOCDATE"].ToString()).ToString("d") : DateTime.MinValue.ToString("d");
                (param[9] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value =(row["ITEMLINE"]!=DBNull.Value)? row["ITEMLINE"].ToString():"0";
                (param[10] = new OracleParameter("p_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = (row["BATCHLINE"]!=DBNull.Value)?row["BATCHLINE"].ToString():"0";
                //(param[11] = new OracleParameter("p_blogseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                param[11] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_BALANCETABLESTOCK", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int addToBalanceTableStock_Insu(DataRow row, Int32 seq,string _user)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[13];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["COM"].ToString();
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["LOC"].ToString();
                (param[3] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMCODE"].ToString();
                (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMSTATUS"].ToString();
                (param[5] = new OracleParameter("p_balqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["BAL_QTY"].ToString());
                (param[6] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["UNITCOST"].ToString());
                (param[7] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["DOCNO"].ToString();
                (param[8] = new OracleParameter("p_docdt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = (row["DOCDATE"] != DBNull.Value) ? Convert.ToDateTime(row["DOCDATE"].ToString()).ToString("d") : DateTime.MinValue.ToString("d");
                (param[9] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = (row["ITEMLINE"] != DBNull.Value) ? row["ITEMLINE"].ToString() : "0";
                (param[10] = new OracleParameter("p_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = (row["BATCHLINE"] != DBNull.Value) ? row["BATCHLINE"].ToString() : "0";
                //(param[11] = new OracleParameter("p_blogseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                (param[11] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
                param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_BALTABLESTOCK_INSU", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateAgingDetails(int seq, DateTime monthEnd, string location)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_date", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = monthEnd.ToString("d");
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                param[3] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_BALANCETABLEAGING", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         public int updateAgingDetails_Insu(int seq, DateTime monthEnd, string location,string _user)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_date", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = monthEnd.ToString("d");
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_BALTABLEAGING_INSU", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public int updateAgingDetails_AgeM(int seq, DateTime monthEnd, string location, string _user)
         {
             //Wimal 05/Oct/2018
             try
             {
                 OracleParameter[] param = new OracleParameter[5];

                 (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                 (param[1] = new OracleParameter("p_date", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = monthEnd.ToString("d");
                 (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                 (param[3] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
                 param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                 Int32 result = UpdateRecords("SP_UPDATE_BALTABLEAGING_AGEM", CommandType.StoredProcedure, param);

                 return result;
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }


        
        public DataTable getTemporyOutLocations(string com, string location, string userid)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                return QueryDataTable("tbl", "SP_GET_TEMPOUTDOCUDATALOC", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateAgingCompanyDetails(BMT_INV_BAL_COM bal)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[23];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = bal.BMI_SEQ_NO;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_COM_CD;
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_LOC_CD;
                (param[3] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ITM_CD;
                (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ITM_STUS;
                (param[5] = new OracleParameter("p_ageqty1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY1;
                (param[6] = new OracleParameter("p_agecost1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST1;
                (param[7] = new OracleParameter("p_ageqty2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY2;
                (param[8] = new OracleParameter("p_agecost2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST2;
                (param[9] = new OracleParameter("p_ageqty3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY3;
                (param[10] = new OracleParameter("p_agecost3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST3;
                (param[11] = new OracleParameter("p_ageqty4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY4;
                (param[12] = new OracleParameter("p_agecost4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST4;
                (param[13] = new OracleParameter("p_ageqty5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY5;
                (param[14] = new OracleParameter("p_agecost5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST5;
                (param[15] = new OracleParameter("p_ageqty6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY6;
                (param[16] = new OracleParameter("p_agecost6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST6;
                (param[17] = new OracleParameter("p_ageqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY_T;
                (param[18] = new OracleParameter("p_agecost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST_T;
                (param[19] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_DOC_NO;
                (param[20] = new OracleParameter("p_docdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = bal.BMI_DOC_DT;
                (param[21] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = bal.BMI_ITEM_LINE;
                param[22] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_COMPANYAGEDET", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateSerAgingCompanyDetails(BMT_INV_BAL_COM bal)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[26];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = bal.BMI_SEQ_NO;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_COM_CD;
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_LOC_CD;
                (param[3] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ITM_CD;
                (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ITM_STUS;
                (param[5] = new OracleParameter("p_ageqty1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY1;
                (param[6] = new OracleParameter("p_agecost1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST1;
                (param[7] = new OracleParameter("p_ageqty2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY2;
                (param[8] = new OracleParameter("p_agecost2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST2;
                (param[9] = new OracleParameter("p_ageqty3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY3;
                (param[10] = new OracleParameter("p_agecost3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST3;
                (param[11] = new OracleParameter("p_ageqty4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY4;
                (param[12] = new OracleParameter("p_agecost4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST4;
                (param[13] = new OracleParameter("p_ageqty5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY5;
                (param[14] = new OracleParameter("p_agecost5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST5;
                (param[15] = new OracleParameter("p_ageqty6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY6;
                (param[16] = new OracleParameter("p_agecost6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST6;
                (param[17] = new OracleParameter("p_ageqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY_T;
                (param[18] = new OracleParameter("p_agecost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST_T;
                (param[19] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_DOC_NO;
                (param[20] = new OracleParameter("p_docdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = bal.BMI_DOC_DT;
                (param[21] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = bal.BMI_ITEM_LINE;
                (param[22] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_CHNL_CD;
                (param[23] = new OracleParameter("p_adminteam", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ADMIN_TEAM;
                (param[24] = new OracleParameter("p_group", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_GROUP;
                param[25] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_COMBALANCEDET", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getInrSerBalanceData(string docNo, int itmLine, int batchLine, string itmcd)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docNo;
                (param[2] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = itmLine;
                (param[3] = new OracleParameter("p_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = batchLine;
                (param[4] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itmcd;
                return QueryDataTable("tbl", "SP_GET_BMVBALANCESERIAL", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getLocaionDetails(string company, string location)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_company", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                return QueryDataTable("tbl", "SP_GET_BMVLOCDETAILS", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getBalanaceItemData(int seq, string location, string com)
        {
            try
            {

                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                return QueryDataTable("tbl", "SP_GET_BMVBALANCEDETAILS", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getAllOutDocuments(DateTime runStrtDt, DateTime runEndDt, string company, string location, Int32 direction)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[6];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction;
                (param[4] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runStrtDt.Date;
                (param[5] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runEndDt.Date;

                return QueryDataTable("tbl", "SP_GET_ALLOUTDOCUMENT", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int addTemporyOutDocument(DataTable getOutDocs, string userid)
        {
            try
            {
                Int32 effect = -1;
                Int32 i = 0;
                foreach (DataRow row in getOutDocs.Rows)
                {
                    OracleParameter[] param = new OracleParameter[14];
                    (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                    (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["COM"].ToString();
                    (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["LOC"].ToString();
                    (param[3] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMCODE"].ToString();
                    (param[4] = new OracleParameter("p_itemstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMSTATUS"].ToString();
                    (param[5] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["QTY"].ToString());
                    (param[6] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["UNITCOST"].ToString());
                    (param[7] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["DOCNO"].ToString();
                    (param[8] = new OracleParameter("p_docdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = (row["DOCDATE"] != DBNull.Value) ? Convert.ToDateTime(row["DOCDATE"].ToString()) : DateTime.MinValue;
                    (param[9] = new OracleParameter("p_itemline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(row["ITEMLINE"].ToString());
                    (param[10] = new OracleParameter("p_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(row["BATCHLINE"].ToString());
                    (param[11] = new OracleParameter("p_balqty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["BAL_QTY"].ToString());
                    (param[12] = new OracleParameter("p_outdocno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["OUTDOCNO"].ToString();
                    param[13] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                    effect = UpdateRecords("SP_SAVE_TEMPOUTDATA", CommandType.StoredProcedure, param);
                    i++;
                }
                return effect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getAllInDocuments(DateTime runStrtDt, DateTime runEndDt, string company, string location, int direction, int seq)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[7];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_direction", OracleDbType.Int32, null, ParameterDirection.Input)).Value = direction;
                (param[4] = new OracleParameter("p_frmdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runStrtDt.Date;
                (param[5] = new OracleParameter("p_todt", OracleDbType.Date, null, ParameterDirection.Input)).Value = runEndDt.Date;
                (param[6] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                return QueryDataTable("tbl", "SP_GET_ALLINDOCUMENT", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int addTemporyInDocument(DataTable getInDocs, string userid)
        {
            try
            {
                Int32 effect = -1;
                Int32 i = 0;
                foreach (DataRow row in getInDocs.Rows)
                {
                    OracleParameter[] param = new OracleParameter[13];
                    (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                    (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["COM"].ToString();
                    (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["LOC"].ToString();
                    (param[3] = new OracleParameter("p_itemcode", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMCODE"].ToString();
                    (param[4] = new OracleParameter("p_itemstatus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMSTATUS"].ToString();
                    (param[5] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["QTY"].ToString());
                    (param[6] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["UNITCOST"].ToString());
                    (param[7] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["DOCNO"].ToString();
                    (param[8] = new OracleParameter("p_docdate", OracleDbType.Date, null, ParameterDirection.Input)).Value = (row["DOCDATE"] != DBNull.Value) ? Convert.ToDateTime(row["DOCDATE"].ToString()) : DateTime.MinValue;
                    (param[9] = new OracleParameter("p_itemline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToInt32(row["ITEMLINE"].ToString());
                    (param[10] = new OracleParameter("p_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = (row["BATCHLINE"] != DBNull.Value) ? Convert.ToInt32(row["BATCHLINE"].ToString()) : 0;
                    (param[11] = new OracleParameter("p_balqty", OracleDbType.Int32, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["BAL_QTY"].ToString());
                    param[12] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                    effect = UpdateRecords("SP_SAVE_TEMPINDATA", CommandType.StoredProcedure, param);
                    i++;
                }
                return effect;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DataTable getCompanyLocations(string company, string location,string adminteam)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[2] = new OracleParameter("p_adtm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = adminteam;
                param[3] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                return QueryDataTable("tbl", "SP_GET_COMPANY_LOCATIONS", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //RUKSHAN 2017 FEB 02
        public List<ReptPickItems> GET_TEMP_PICK_ITM_GROUP(string _seq)
        {
            List<ReptPickItems> _list = new List<ReptPickItems>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_Tus_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _seq;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("temp_pick_ser", "sp_get_TEMPITEMGROUP", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickItems>(_itemTable, ReptPickItems.Converter);
            }
            return _list;
        }

        public DataTable getBalanaceItemDataGIT(int seq, string location, string company)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                return QueryDataTable("tbl", "SP_GET_BMVBALANCEDETAILSGIT", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getSerDataByItm(string docno, string itmcd)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[3];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_doc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
                (param[2] = new OracleParameter("p_itm", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = itmcd;
                return QueryDataTable("tbl", "SP_GET_INTSERBYITMDOC", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateSerAgingCompanyDetailsOnlyCom(BMT_INV_BAL_COM bal)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[26];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = bal.BMI_SEQ_NO;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_COM_CD;
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_LOC_CD;
                (param[3] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ITM_CD;
                (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ITM_STUS;
                (param[5] = new OracleParameter("p_ageqty1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY1;
                (param[6] = new OracleParameter("p_agecost1", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST1;
                (param[7] = new OracleParameter("p_ageqty2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY2;
                (param[8] = new OracleParameter("p_agecost2", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST2;
                (param[9] = new OracleParameter("p_ageqty3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY3;
                (param[10] = new OracleParameter("p_agecost3", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST3;
                (param[11] = new OracleParameter("p_ageqty4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY4;
                (param[12] = new OracleParameter("p_agecost4", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST4;
                (param[13] = new OracleParameter("p_ageqty5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY5;
                (param[14] = new OracleParameter("p_agecost5", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST5;
                (param[15] = new OracleParameter("p_ageqty6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY6;
                (param[16] = new OracleParameter("p_agecost6", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST6;
                (param[17] = new OracleParameter("p_ageqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_QTY_T;
                (param[18] = new OracleParameter("p_agecost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = bal.BMI_AGE_COST_T;
                (param[19] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_DOC_NO;
                (param[20] = new OracleParameter("p_docdt", OracleDbType.Date, null, ParameterDirection.Input)).Value = bal.BMI_DOC_DT;
                (param[21] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = bal.BMI_ITEM_LINE;
                (param[22] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_CHNL_CD;
                (param[23] = new OracleParameter("p_adminteam", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_ADMIN_TEAM;
                (param[24] = new OracleParameter("p_group", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = bal.BMI_GROUP;

                param[25] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_COMBALANCEDETINSERT", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //by Akila 2017/09/12
        public Int16 UpdateScanItemDetails(ReptPickItems _pickItems)
        {
            Int16 effects = 0;
            OracleParameter[] param = new OracleParameter[7];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _pickItems.Tui_usrseq_no;
            (param[1] = new OracleParameter("p_req_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pickItems.Tui_req_itm_cd;
            (param[2] = new OracleParameter("p_req_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pickItems.Tui_req_itm_stus;
            (param[3] = new OracleParameter("p_pic_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _pickItems.Tui_pic_itm_cd;
            (param[4] = new OracleParameter("p_pic_itm_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _pickItems.Tui_pic_itm_qty;
            (param[5] = new OracleParameter("p_req_itm_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _pickItems.Tui_req_itm_qty;

            param[6] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);

            effects = (Int16)this.UpdateRecords("SP_UPDATE_SCNITMDETAILS", CommandType.StoredProcedure, param);

            return effects;
        }
        //Add by Lakshan 20Aug2017
        public DataTable SearchTempPickItemData(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;
            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }
            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);
            string _itm = null;
            string _model = null;
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "ITEM CODE":
                        _itm = _searchText;
                        break;
                    case "MODEL":
                        _model = _searchText;
                        break;
                    default:
                        break;
                }
            }

            _itm = string.IsNullOrEmpty(_itm) ? "" : _itm.ToUpper();
            _model = string.IsNullOrEmpty(_model) ? "" : _model.ToUpper()+"%";
            
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_tuh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0].ToString();
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1].ToString();
            (param[2] = new OracleParameter("p_tuh_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[2].ToString();
            (param[3] = new OracleParameter("p_tui_req_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[4] = new OracleParameter("p_mi_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _model;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("int_req", "sp_search_temp_pick_itm_data", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        //Add by lakshan 12Febg2018
        public Int16 SavePickedHeaderPartial(ReptPickHeader _scanheaderNew)
        {
            Int16 rows_affected = 0;
            OracleParameter[] param = new OracleParameter[22];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usrseq_no;
            (param[1] = new OracleParameter("p_usr_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usr_id;
            (param[2] = new OracleParameter("p_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usr_com;
            (param[3] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_session_id;
            (param[4] = new OracleParameter("p_cre_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = DateTime.Now;
            (param[5] = new OracleParameter("p_doc_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_doc_tp;
            (param[6] = new OracleParameter("p_direction", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_direct == true ? 1 : 0;
            (param[7] = new OracleParameter("p_ischeck_itemstatus", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_ischek_itmstus == true ? 1 : 0;
            (param[8] = new OracleParameter("p_ischeck_similaritem", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_ischek_simitm == true ? 1 : 0;
            (param[9] = new OracleParameter("p_ischeck_reqqty", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_ischek_reqqty;
            (param[10] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_doc_no;
            (param[11] = new OracleParameter("p_rec_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_rec_com;
            (param[12] = new OracleParameter("p_rec_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_rec_loc;
            (param[13] = new OracleParameter("p_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_isdirect;
            (param[14] = new OracleParameter("p_pro_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_pro_user;
            (param[15] = new OracleParameter("p_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_usr_loc;
            (param[16] = new OracleParameter("p_dir_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_dir_type;
            (param[17] = new OracleParameter("p_tuh_wh_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_wh_com;
            (param[18] = new OracleParameter("p_tuh_wh_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_wh_loc;
            (param[19] = new OracleParameter("p_tuh_load_bay", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_load_bay;
            (param[20] = new OracleParameter("p_tuh_is_take_res", OracleDbType.Int16, null, ParameterDirection.Input)).Value = _scanheaderNew.Tuh_is_take_res == true ? 1 : 0;
            param[21] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            rows_affected = (Int16)this.UpdateRecords("sp_temp_pick_hdr_partial", CommandType.StoredProcedure, param);
            return rows_affected;
        }

        public Int16 SavePickedItemsPartial(ReptPickItems _scanitemNew)
        {
            Int16 rows_affected = 0, _res = 0;
            OracleParameter[] param = new OracleParameter[13];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_usrseq_no;
            (param[1] = new OracleParameter("p_req_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_req_itm_cd;
            (param[2] = new OracleParameter("p_req_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_req_itm_stus;
            (param[3] = new OracleParameter("p_req_itm_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_req_itm_qty;
            (param[4] = new OracleParameter("p_pic_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_pic_itm_cd;
            (param[5] = new OracleParameter("p_pic_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_pic_itm_stus;
            (param[6] = new OracleParameter("p_pic_itm_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_pic_itm_qty;
            (param[7] = new OracleParameter("p_sup", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_sup;
            (param[8] = new OracleParameter("p_batch", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_batch;
            (param[9] = new OracleParameter("p_grn", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_grn;
            (param[10] = new OracleParameter("p_grn_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_grn_dt;
            (param[11] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanitemNew.Tui_exp_dt;
            param[12] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            rows_affected = (Int16)this.UpdateRecords("sp_temp_pick_itm_partial_save", CommandType.StoredProcedure, param);
            return rows_affected;
        }
        //Add by lakshan 14Feb2018
        public List<ReptPickSerials> GetAllScanSerialsListPartial(string _company, string _location, string _user, Int32 _userseqno, string _doctype)
        {
            List<ReptPickSerials> _scanList = null;

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _company;
            (param[1] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _location;
            (param[2] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            (param[3] = new OracleParameter("p_seqno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userseqno;
            (param[4] = new OracleParameter("p_doctype", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _doctype;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

            DataTable _tblScan = QueryDataTable("tblScan", "sp_getallscanserials_partial", CommandType.StoredProcedure, false, param);
            if (_doctype == "UPLD")
            {
                if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConverterForMac);
            }
            else
            {
                if (_tblScan.Rows.Count > 0) _scanList = DataTableExtensions.ToGenericList<ReptPickSerials>(_tblScan, ReptPickSerials.ConvertTotal);
            }
            return _scanList;
        }
        //Add by lakshan 14Feb2018
        public DataTable SearchTempPickItemDataPartial(string _initialSearchParams, string _searchCatergory, string _searchText)
        {
            DataTable _dtResults = null;
            if (_initialSearchParams.Contains(":"))
            {
                string[] arr = _initialSearchParams.Split(new string[] { ":" }, StringSplitOptions.None);
                _initialSearchParams = arr[1];
            }
            string[] seperator = new string[] { "|" };
            string[] searchParams = _initialSearchParams.Split(seperator, StringSplitOptions.None);
            string _itm = null;
            string _model = null;
            if (!string.IsNullOrEmpty(_searchCatergory))
            {
                switch (_searchCatergory.ToUpper())
                {
                    case "ITEM CODE":
                        _itm = _searchText;
                        break;
                    case "MODEL":
                        _model = _searchText;
                        break;
                    default:
                        break;
                }
            }

            _itm = string.IsNullOrEmpty(_itm) ? "" : _itm.ToUpper();
            _model = string.IsNullOrEmpty(_model) ? "" : _model.ToUpper() + "%";

            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_tuh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[0].ToString();
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[1].ToString();
            (param[2] = new OracleParameter("p_tuh_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = searchParams[2].ToString();
            (param[3] = new OracleParameter("p_tui_req_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itm;
            (param[4] = new OracleParameter("p_mi_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _model;
            param[5] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            _dtResults = QueryDataTable("int_req", "sp_ser_temp_pick_itm_partial", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        //Add by lakshan 14Feb2018
        public Int32 IsExistInTempPickSerialPartial(string _companyCode, string _userSeqNo, string _itemCode, string _serialNo1)
        {
            ConnectionOpen();
            OracleParameter[] param = new OracleParameter[4];

            (param[0] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _companyCode;
            (param[1] = new OracleParameter("p_usr_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _userSeqNo;
            (param[2] = new OracleParameter("p_item_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _itemCode;
            (param[3] = new OracleParameter("p_serialNo1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _serialNo1;

            OracleParameter outParameter = new OracleParameter("row_count", OracleDbType.Int32, null, ParameterDirection.Output);
            int _row_count = ReturnSP_SingleValue("sp_isexist_in_tmp_pick_ser_par", CommandType.StoredProcedure, outParameter, param);
            ConnectionClose();
            return _row_count;
        }
        //Add by lakshan 14Feb2018
        public Int16 SavePickedItemSerialsPartial(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[58];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
            (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
            (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
            (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;
            (param[38] = new OracleParameter("p_ser_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_remarks;
            (param[39] = new OracleParameter("p_resqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_resqty;
            (param[40] = new OracleParameter("p_ageloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc;
            (param[41] = new OracleParameter("p_ageloc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc_dt.Date;
            (param[42] = new OracleParameter("p_isownmrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_isownmrn;

            (param[43] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_no;//Chamal 20-Jan-2015
            (param[44] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_line;//Chamal 20-Jan-2015
            (param[45] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_no;//Chamal 20-Jan-2015
            (param[46] = new OracleParameter("p_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_line;//Chamal 20-Jan-2015

            (param[47] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_no;//kapila 4/7/2015
            (param[48] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exp_dt.Date;
            (param[49] = new OracleParameter("p_manufac_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_manufac_dt.Date;   //kapila 20/7/2015

            (param[50] = new OracleParameter("p_tus_is_pgs", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_is_pgs;//Rukshan 1/8/2015
            (param[51] = new OracleParameter("p_tus_pgs_count", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_count;//Rukshan 1/8/2015
            (param[52] = new OracleParameter("p_tus_pg_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_prefix;   //Rukshan 1/8/2015
            (param[53] = new OracleParameter("p_tus_st_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_st_pg;   //Rukshan 1/8/2015
            (param[54] = new OracleParameter("p_tus_ed_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ed_pg;   //Rukshan 1/8/2015
            (param[55] = new OracleParameter("p_tus_new_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_itm_cd;   //Sahan 14/09/2015

            (param[56] = new OracleParameter("P_tus_bin_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin_to;   //Tharaka 2015-11-05

            param[57] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_picksernewnew_partial", CommandType.StoredProcedure, param);
        }
        //Add by lakshan 14Feb2018
        public Int16 SavePickedItemSerialsMacPartial(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[60];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
            (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
            (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
            (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;
            (param[38] = new OracleParameter("p_ser_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_remarks;
            (param[39] = new OracleParameter("p_resqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_resqty;
            (param[40] = new OracleParameter("p_ageloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc;
            (param[41] = new OracleParameter("p_ageloc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc_dt.Date;
            (param[42] = new OracleParameter("p_isownmrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_isownmrn;

            (param[43] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_no;//Chamal 20-Jan-2015
            (param[44] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_line;//Chamal 20-Jan-2015
            (param[45] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_no;//Chamal 20-Jan-2015
            (param[46] = new OracleParameter("p_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_line;//Chamal 20-Jan-2015

            (param[47] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_no;//kapila 4/7/2015
            (param[48] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exp_dt.Date;
            (param[49] = new OracleParameter("p_manufac_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_manufac_dt.Date;   //kapila 20/7/2015

            (param[50] = new OracleParameter("p_tus_is_pgs", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_is_pgs;//Rukshan 1/8/2015
            (param[51] = new OracleParameter("p_tus_pgs_count", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_count;//Rukshan 1/8/2015
            (param[52] = new OracleParameter("p_tus_pg_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_prefix;   //Rukshan 1/8/2015
            (param[53] = new OracleParameter("p_tus_st_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_st_pg;   //Rukshan 1/8/2015
            (param[54] = new OracleParameter("p_tus_ed_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ed_pg;   //Rukshan 1/8/2015
            (param[55] = new OracleParameter("p_tus_new_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_itm_cd;   //Sahan 14/09/2015

            (param[56] = new OracleParameter("P_tus_bin_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin_to;   //Tharaka 2015-11-05
            (param[57] = new OracleParameter("P_Tus_pkg_uom_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pkg_uom_tp;   //Tharaka 2015-11-05
            (param[58] = new OracleParameter("P_Tus_pkg_uom_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pkg_uom_qty;   //Tharaka 2015-11-05

            param[59] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_picksernewnew_mac_par", CommandType.StoredProcedure, param);
        }
        //Add by lakshan 14Feb2018
        public List<ReptPickHeader> GET_TEMP_PICK_HDR_DATA_PARTIAL(ReptPickHeader _obj)
        {
            List<ReptPickHeader> _list = new List<ReptPickHeader>();
            OracleParameter[] param = new OracleParameter[8];
            (param[0] = new OracleParameter("p_tuh_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_no;
            (param[1] = new OracleParameter("p_tuh_usr_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_com;
            (param[2] = new OracleParameter("p_tuh_doc_tp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_doc_tp;
            (param[3] = new OracleParameter("p_tuh_direct", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_direct;
            (param[4] = new OracleParameter("p_tuh_isdirect", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_isdirect;
            (param[5] = new OracleParameter("p_TUH_USRSEQ_NO", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _obj.Tuh_usrseq_no;
            (param[6] = new OracleParameter("p_tuh_usr_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _obj.Tuh_usr_loc;
            param[7] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = new DataTable();
            if (string.IsNullOrEmpty(_obj.Tuh_doc_no) && string.IsNullOrEmpty(_obj.Tuh_usr_com) && _obj.Tuh_usrseq_no == 0)
            {
                return _list;
            }
            else
            {
                _itemTable = QueryDataTable("tblScanParam", "sp_get_tmp_pick_hdr_partial", CommandType.StoredProcedure, false, param);
                if (_itemTable.Rows.Count > 0)
                {
                    _list = DataTableExtensions.ToGenericList<ReptPickHeader>(_itemTable, ReptPickHeader.Converter2);
                }
            }
            return _list;
        }
        //Add by lakshan 14Feb2018
        public List<ReptPickSerials> GET_TEMP_PICK_SER_DATA_PARTIAL(ReptPickSerials _repPickSer)
        {
            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_tus_usrseq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _repPickSer.Tus_usrseq_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("temp_pick_ser", "sp_get_temp_pick_ser_partial", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickSerials>(_itemTable, ReptPickSerials.ConverterNew);
            }
            return _list;
        }
        //Add by lakshan 14Feb2018
        public List<ReptPickItems> GET_TEMP_PICK_ITM_DATA_PARTIAL(ReptPickItems _repTemItm)
        {
            List<ReptPickItems> _list = new List<ReptPickItems>();
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_Tus_seq_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _repTemItm.Tui_usrseq_no;
            param[1] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _itemTable = QueryDataTable("temp_pick_ser", "sp_get_temp_pick_itm_partial", CommandType.StoredProcedure, false, param);
            if (_itemTable.Rows.Count > 0)
            {
                _list = DataTableExtensions.ToGenericList<ReptPickItems>(_itemTable, ReptPickItems.Converter);
            }
            return _list;
        }
        //Add by lakshan 14Feb2018
        public Int32 DocumentFinishPartially(string docno, string doctyp, Int32 status,string _user)
        {
            OracleParameter[] param = new OracleParameter[5];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            (param[1] = new OracleParameter("p_doctyp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doctyp;
            (param[2] = new OracleParameter("p_status", OracleDbType.Int32, null, ParameterDirection.Input)).Value = status;
            (param[3] = new OracleParameter("p_user", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _user;
            param[4] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_finished_doc_partialy", CommandType.StoredProcedure, param);
            return effects;
        }
        //Add by lakshan 14Feb2018
        public Int32 SaveDocumentHdrFromPartially(string docno, string doctyp)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            (param[1] = new OracleParameter("p_doctyp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doctyp;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_save_partial_doc_to_hdr", CommandType.StoredProcedure, param);
            return effects;
        }
        //Add by lakshan 14Feb2018
        public Int32 DELETE_TEMP_PICK_HDR_PARTIAL(string docno, string doctyp)
        {
            OracleParameter[] param = new OracleParameter[3];
            Int32 effects = 0;
            (param[0] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = docno;
            (param[1] = new OracleParameter("p_doctyp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = doctyp;
            param[2] = new OracleParameter("O_EFFECT", OracleDbType.Int32, null, ParameterDirection.Output);
            effects = (Int32)UpdateRecords("sp_del_temp_pick_hdr_partial", CommandType.StoredProcedure, param);
            return effects;
        }
        //Add by lakshan 14Feb2018
        public Int16 UpdatePickedItemSerialsPartial(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[6];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("P_ITEM", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[2] = new OracleParameter("P_STATUS", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[3] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[4] = new OracleParameter("p_tus_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            param[5] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_pickserupdate_partial", CommandType.StoredProcedure, param);
        }

        public DataTable getDetailsForDocGenarate(string pc, string type)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
            (param[1] = new OracleParameter("p_type", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = type;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_SUNSCMDATANEW", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }

        public int updateTempAndAnalDetails(string invoiceno, decimal cost, string pc, string com)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[4];
                (param[0] = new OracleParameter("p_pc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = pc;
                (param[1] = new OracleParameter("p_inv", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = invoiceno;
                (param[2] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                (param[3] = new OracleParameter("p_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = cost;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
                return (Int16)this.UpdateRecords("SP_UPDATE_ANALANDSTUS", CommandType.StoredProcedure, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GET_TEMP_PICK_SER_BY_SER(string _ser_1, string _ser_2)
        {
            OracleParameter[] param = new OracleParameter[3];
            (param[0] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser_1;
            (param[1] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _ser_2;

            param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
            DataTable _dtResults = QueryDataTable("tbl", "SP_GET_TEMP_PICK_SER_BY_SER", CommandType.StoredProcedure, false, param);
            return _dtResults;
        }
        public DataTable getBalanaceItemDataGIT(int seq, string location, string company, DateTime lstDt)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];
                param[0] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);
                (param[1] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[4] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = lstDt;
                return QueryDataTable("tbl", "SP_GET_BMVBALANCEDETAILSGIT", CommandType.StoredProcedure, false, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int addToBalanceTableStock(DataRow row, Int32 seq, string chnl, string adminteam, string wedret)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[15];
                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["COM"].ToString();
                (param[2] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["LOC"].ToString();
                (param[3] = new OracleParameter("p_itmcd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMCODE"].ToString();
                (param[4] = new OracleParameter("p_status", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["ITEMSTATUS"].ToString();
                (param[5] = new OracleParameter("p_balqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["BAL_QTY"].ToString());
                (param[6] = new OracleParameter("p_unitcost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = Convert.ToDecimal(row["UNITCOST"].ToString());
                (param[7] = new OracleParameter("p_docno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = row["DOCNO"].ToString();
                (param[8] = new OracleParameter("p_docdt", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = (row["DOCDATE"] != DBNull.Value) ? Convert.ToDateTime(row["DOCDATE"].ToString()).ToString("d") : DateTime.MinValue.ToString("d");
                (param[9] = new OracleParameter("p_itmline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = (row["ITEMLINE"] != DBNull.Value) ? row["ITEMLINE"].ToString() : "0";
                (param[10] = new OracleParameter("p_batchline", OracleDbType.Int32, null, ParameterDirection.Input)).Value = (row["BATCHLINE"] != DBNull.Value) ? row["BATCHLINE"].ToString() : "0";
                //(param[11] = new OracleParameter("p_blogseq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = 0;
                (param[11] = new OracleParameter("p_chnl", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = chnl;
                (param[12] = new OracleParameter("p_adminteam", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = adminteam;
                (param[13] = new OracleParameter("p_wedret", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = wedret;

                param[14] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                Int32 result = UpdateRecords("SP_UPDATE_BALANCETABLESTOCK", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int updateAgingDetails(int seq, DateTime monthEnd, string location, string com)
        {
            try
            {
                OracleParameter[] param = new OracleParameter[5];

                (param[0] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[1] = new OracleParameter("p_date", OracleDbType.Date, null, ParameterDirection.Input)).Value = monthEnd.Date;
                (param[2] = new OracleParameter("p_location", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[3] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = com;
                param[4] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);

                Int32 result = UpdateRecords("SP_UPDATE_BALANCETABLEAGING", CommandType.StoredProcedure, param);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getLocationDetails(string location, string company)
        {
            try
            {

                OracleParameter[] param = new OracleParameter[3];
                (param[0] = new OracleParameter("p_loc_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                (param[1] = new OracleParameter("p_com_code", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                param[2] = new OracleParameter("c_data", OracleDbType.RefCursor, null, ParameterDirection.Output);

                DataTable _dtResults = QueryDataTable("tblloc", "SP_GET_LOC_ADTEAMCHNL", CommandType.StoredProcedure, false, param);
                return _dtResults;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int deleteTempTable(string userid, string company, Int32 seq, string typ, string location)
        {
            try
            {
                if (company == "ABL" || company == "LRP")
                {
                    seq = seq + 2;
                }
                else
                {
                    seq = seq + 1;
                }
                OracleParameter[] param = new OracleParameter[6];
                (param[0] = new OracleParameter("p_userid", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = userid;
                (param[1] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = company;
                (param[2] = new OracleParameter("p_seq", OracleDbType.Int32, null, ParameterDirection.Input)).Value = seq;
                (param[3] = new OracleParameter("p_typ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = typ;
                (param[4] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = location;
                param[5] = new OracleParameter("o_effect", OracleDbType.Int32, null, ParameterDirection.Output);
                //Query Data base.         
                Int32 result = UpdateRecords("SP_DELETE_TMPDOCNOTFND", CommandType.StoredProcedure, param);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Int16 SavePickedItemSerialsnew(ReptPickSerials _scanserNew)
        {
            OracleParameter[] param = new OracleParameter[59];
            (param[0] = new OracleParameter("p_usrseq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_usrseq_no;
            (param[1] = new OracleParameter("p_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_no;
            (param[2] = new OracleParameter("p_seq_no", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_seq_no;
            (param[3] = new OracleParameter("p_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_line;
            (param[4] = new OracleParameter("p_batch_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_line;
            (param[5] = new OracleParameter("p_ser_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_line;
            (param[6] = new OracleParameter("p_doc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_doc_dt;
            (param[7] = new OracleParameter("p_com", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_com;
            (param[8] = new OracleParameter("p_loc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_loc;
            (param[9] = new OracleParameter("p_bin", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin;
            (param[10] = new OracleParameter("p_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_cd;
            (param[11] = new OracleParameter("p_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_stus;
            (param[12] = new OracleParameter("p_unit_cost", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_cost;
            (param[13] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_qty;
            (param[14] = new OracleParameter("p_ser_id", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_id;
            (param[15] = new OracleParameter("p_ser_1", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_1;
            (param[16] = new OracleParameter("p_ser_2", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_2;
            (param[17] = new OracleParameter("p_ser_3", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_3;
            (param[18] = new OracleParameter("p_ser_4", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ser_4;
            (param[19] = new OracleParameter("p_warr_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_no;
            (param[20] = new OracleParameter("p_warr_period", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_warr_period;
            (param[21] = new OracleParameter("p_orig_grncom ", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grncom;
            (param[22] = new OracleParameter("p_orig_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grnno;
            (param[23] = new OracleParameter("p_orig_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_grndt;
            (param[24] = new OracleParameter("p_orig_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_orig_supp;
            (param[25] = new OracleParameter("p_exist_grncom", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grncom;
            (param[26] = new OracleParameter("p_exist_grnno", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grnno;
            (param[27] = new OracleParameter("p_exist_grndt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_grndt;
            (param[28] = new OracleParameter("p_exist_supp", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exist_supp;
            (param[29] = new OracleParameter("p_cre_by", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_cre_by;
            (param[30] = new OracleParameter("p_session_id", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_session_id;
            (param[31] = new OracleParameter("p_unit_price", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_unit_price;
            (param[32] = new OracleParameter("p_new_itm_stus", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_status;
            (param[33] = new OracleParameter("p_base_doc_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_doc_no;
            (param[34] = new OracleParameter("p_base_itm_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_base_itm_line;
            (param[35] = new OracleParameter("p_itm_desc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_desc;
            (param[36] = new OracleParameter("p_itm_model", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_model;
            (param[37] = new OracleParameter("p_itm_brand", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_brand;
            (param[38] = new OracleParameter("p_ser_remarks", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_remarks;
            (param[39] = new OracleParameter("p_resqty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _scanserNew.Tus_resqty;
            (param[40] = new OracleParameter("p_ageloc", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc;
            (param[41] = new OracleParameter("p_ageloc_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ageloc_dt.Date;
            (param[42] = new OracleParameter("p_isownmrn", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_isownmrn;

            (param[43] = new OracleParameter("p_job_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_no;//Chamal 20-Jan-2015
            (param[44] = new OracleParameter("p_job_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_job_line;//Chamal 20-Jan-2015
            (param[45] = new OracleParameter("p_res_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_no;//Chamal 20-Jan-2015
            (param[46] = new OracleParameter("p_res_line", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_res_line;//Chamal 20-Jan-2015

            (param[47] = new OracleParameter("p_batch_no", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_batch_no;//kapila 4/7/2015
            (param[48] = new OracleParameter("p_exp_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_exp_dt.Date;
            (param[49] = new OracleParameter("p_manufac_dt", OracleDbType.Date, null, ParameterDirection.Input)).Value = _scanserNew.Tus_manufac_dt.Date;   //kapila 20/7/2015

            (param[50] = new OracleParameter("p_tus_is_pgs", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_is_pgs;//Rukshan 1/8/2015
            (param[51] = new OracleParameter("p_tus_pgs_count", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_count;//Rukshan 1/8/2015
            (param[52] = new OracleParameter("p_tus_pg_prefix", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_pgs_prefix;   //Rukshan 1/8/2015
            (param[53] = new OracleParameter("p_tus_st_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_st_pg;   //Rukshan 1/8/2015
            (param[54] = new OracleParameter("p_tus_ed_pg", OracleDbType.Int32, null, ParameterDirection.Input)).Value = _scanserNew.Tus_ed_pg;   //Rukshan 1/8/2015
            (param[55] = new OracleParameter("p_tus_new_itm_cd", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_new_itm_cd;   //Sahan 14/09/2015

            (param[56] = new OracleParameter("P_tus_bin_to", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_bin_to;   //Tharaka 2015-11-05
            (param[57] = new OracleParameter("P_TUS_NEW_ITM_SER", OracleDbType.NVarchar2, null, ParameterDirection.Input)).Value = _scanserNew.Tus_itm_base_new_ser;   //Tharaka 2015-11-05
            param[58] = new OracleParameter("o_effect", OracleDbType.Int16, null, ParameterDirection.Output);
            return (Int16)this.UpdateRecords("sp_picksernewnewsave", CommandType.StoredProcedure, param);
            //return (Int16)this.UpdateRecords("sp_picksernewnew_test", CommandType.StoredProcedure, param);
        }
        public int UpdateInvitation(Decimal _qty)
        {
            OracleParameter[] param = new OracleParameter[2];
            (param[0] = new OracleParameter("p_qty", OracleDbType.Decimal, null, ParameterDirection.Input)).Value = _qty;
            param[1] = new OracleParameter("oeffect", OracleDbType.Int32, null, ParameterDirection.Output);
            return UpdateRecords("sp_updateInvt", CommandType.StoredProcedure, param);
        }
    }
}
