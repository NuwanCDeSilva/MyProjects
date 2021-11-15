using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class DisposalItem
    {
        public Int32 Idd_seq { get; set; }
        public Int32 Idd_line { get; set; }
        public String Idd_job_no { get; set; }
        public String Idd_itm_cd { get; set; }
        public Int32 Idd_ser_id { get; set; }
        public String Idd_ser_1 { get; set; }
        public String Idd_ser_2 { get; set; }
        public String Idd_stus { get; set; }
        public String Idd_cur_loc { get; set; }
        public String Idd_disp_loc { get; set; }
        public Int32 Idd_act { get; set; }
        public String Idd_cre_by { get; set; }
        public DateTime Idd_cre_dt { get; set; }
        public String Idd_mod_by { get; set; }
        public DateTime Idd_mod_dt { get; set; }
        public String Idd_unit_cost { get; set; }
        public String Idd_itm_model { get; set; }
        public String Idd_itm_brand { get; set; }
        public String Idd_stus_desc { get; set; }
        public Decimal Idd_qty { get; set; }
        public Decimal Idd_bqty { get; set; }
        public String Idd_bin_cd { get; set; }
        public Int32 Idd_base_Seq { get; set; }
        public Int32 Idd_scan_stus { get; set; }
        public decimal TMP_idd_res_qty { get; set; }
        public decimal TMP_idd_res_bal { get; set; }
        public Decimal Idd_scan_qty { get; set; }

        public static DisposalItem Converter(DataRow row)
        {
            return new DisposalItem
            {
                Idd_act = row["IDD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_ACT"].ToString()),
                Idd_seq = row["IDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_SEQ"].ToString()),
                Idd_line = row["IDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_LINE"].ToString()),
                Idd_job_no = row["IDD_JOB_NO"] == DBNull.Value ? string.Empty : row["IDD_JOB_NO"].ToString(),
                Idd_itm_cd = row["IDD_ITM_CD"] == DBNull.Value ? string.Empty : row["IDD_ITM_CD"].ToString(),
                Idd_ser_id = row["IDD_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_SER_ID"].ToString()),
                Idd_ser_1 = row["IDD_SER_1"] == DBNull.Value ? string.Empty : row["IDD_SER_1"].ToString(),
                Idd_ser_2 = row["IDD_SER_2"] == DBNull.Value ? string.Empty : row["IDD_SER_2"].ToString(),
                Idd_stus = row["IDD_STUS"] == DBNull.Value ? string.Empty : row["IDD_STUS"].ToString(),
                Idd_cur_loc = row["IDD_CUR_LOC"] == DBNull.Value ? string.Empty : row["IDD_CUR_LOC"].ToString(),
                Idd_disp_loc = row["IDD_DISP_LOC"] == DBNull.Value ? string.Empty : row["IDD_DISP_LOC"].ToString(),
                Idd_cre_by = row["IDD_CRE_BY"] == DBNull.Value ? string.Empty : row["IDD_CRE_BY"].ToString(),
                Idd_cre_dt = row["IDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDD_CRE_DT"].ToString()),
                Idd_mod_by = row["IDD_MOD_BY"] == DBNull.Value ? string.Empty : row["IDD_MOD_BY"].ToString(),
                Idd_mod_dt = row["IDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDD_MOD_DT"].ToString()),
                Idd_unit_cost = row["INS_UNIT_COST"] == DBNull.Value ? string.Empty : row["INS_UNIT_COST"].ToString(),
                Idd_itm_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Idd_itm_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Idd_stus_desc = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
                Idd_qty = row["IDD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IDD_QTY"].ToString()),
                Idd_bin_cd = row["IDD_BIN_CD"] == DBNull.Value ? string.Empty : row["IDD_BIN_CD"].ToString(),
                Idd_base_Seq = row["IDD_BASE_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_BASE_SEQ"].ToString()),
            };
        }
        public static DisposalItem Converter3(DataRow row)
        {
            return new DisposalItem
            {
                Idd_act = row["IDD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_ACT"].ToString()),
                Idd_seq = row["IDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_SEQ"].ToString()),
                Idd_line = row["IDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_LINE"].ToString()),
                Idd_job_no = row["IDD_JOB_NO"] == DBNull.Value ? string.Empty : row["IDD_JOB_NO"].ToString(),
                Idd_itm_cd = row["IDD_ITM_CD"] == DBNull.Value ? string.Empty : row["IDD_ITM_CD"].ToString(),
                Idd_ser_id = row["IDD_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_SER_ID"].ToString()),
                Idd_ser_1 = row["IDD_SER_1"] == DBNull.Value ? string.Empty : row["IDD_SER_1"].ToString(),
                Idd_ser_2 = row["IDD_SER_2"] == DBNull.Value ? string.Empty : row["IDD_SER_2"].ToString(),
                Idd_stus = row["IDD_STUS"] == DBNull.Value ? string.Empty : row["IDD_STUS"].ToString(),
                Idd_cur_loc = row["IDD_CUR_LOC"] == DBNull.Value ? string.Empty : row["IDD_CUR_LOC"].ToString(),
                Idd_disp_loc = row["IDD_DISP_LOC"] == DBNull.Value ? string.Empty : row["IDD_DISP_LOC"].ToString(),
                Idd_cre_by = row["IDD_CRE_BY"] == DBNull.Value ? string.Empty : row["IDD_CRE_BY"].ToString(),
                Idd_cre_dt = row["IDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDD_CRE_DT"].ToString()),
                Idd_mod_by = row["IDD_MOD_BY"] == DBNull.Value ? string.Empty : row["IDD_MOD_BY"].ToString(),
                Idd_mod_dt = row["IDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDD_MOD_DT"].ToString()),
                Idd_unit_cost = row["Idd_unit_cost"] == DBNull.Value ? string.Empty : row["Idd_unit_cost"].ToString(),
                Idd_itm_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Idd_itm_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Idd_stus_desc = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
                Idd_qty = row["IDD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IDD_QTY"].ToString()),
                Idd_bqty = row["IDD_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IDD_BQTY"].ToString()),
                Idd_bin_cd = row["IDD_BIN_CD"] == DBNull.Value ? string.Empty : row["IDD_BIN_CD"].ToString(),
                Idd_base_Seq = row["IDD_BASE_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_BASE_SEQ"].ToString()),
            };
        }
        public static DisposalItem Converter2(DataRow row)
        {
            return new DisposalItem
            {
                Idd_act = row["IDD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_ACT"].ToString()),
                Idd_seq = row["IDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_SEQ"].ToString()),
                Idd_line = row["IDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_LINE"].ToString()),
                Idd_job_no = row["IDD_JOB_NO"] == DBNull.Value ? string.Empty : row["IDD_JOB_NO"].ToString(),
                Idd_itm_cd = row["IDD_ITM_CD"] == DBNull.Value ? string.Empty : row["IDD_ITM_CD"].ToString(),
                Idd_ser_id = row["IDD_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_SER_ID"].ToString()),
                Idd_ser_1 = row["IDD_SER_1"] == DBNull.Value ? string.Empty : row["IDD_SER_1"].ToString(),
                Idd_ser_2 = row["IDD_SER_2"] == DBNull.Value ? string.Empty : row["IDD_SER_2"].ToString(),
                Idd_stus = row["IDD_STUS"] == DBNull.Value ? string.Empty : row["IDD_STUS"].ToString(),
                Idd_cur_loc = row["IDD_CUR_LOC"] == DBNull.Value ? string.Empty : row["IDD_CUR_LOC"].ToString(),
                Idd_disp_loc = row["IDD_DISP_LOC"] == DBNull.Value ? string.Empty : row["IDD_DISP_LOC"].ToString(),
                Idd_cre_by = row["IDD_CRE_BY"] == DBNull.Value ? string.Empty : row["IDD_CRE_BY"].ToString(),
                Idd_cre_dt = row["IDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDD_CRE_DT"].ToString()),
                Idd_mod_by = row["IDD_MOD_BY"] == DBNull.Value ? string.Empty : row["IDD_MOD_BY"].ToString(),
                Idd_mod_dt = row["IDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDD_MOD_DT"].ToString()),
                Idd_unit_cost = row["Idd_unit_cost"] == DBNull.Value ? string.Empty : row["Idd_unit_cost"].ToString(),
                Idd_itm_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Idd_itm_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Idd_stus_desc = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
                Idd_qty = row["IDD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IDD_QTY"].ToString()),
                Idd_bin_cd = row["IDD_BIN_CD"] == DBNull.Value ? string.Empty : row["IDD_BIN_CD"].ToString(),
                Idd_base_Seq = row["IDD_BASE_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDD_BASE_SEQ"].ToString()),
            };
        }
    }
}
