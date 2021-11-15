using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class HP_ACC_COMP
    {
        public string bhs_com { get; set; }
        public string bhs_pc { get; set; }
        public string bhs_pc_desc { get; set; }
        public string bhs_pc_chnl { get; set; }
        public string bhs_close_bal_pre { get; set; }
        public string bhs_close_bal_fr { get; set; }
        public string bhs_close_bal_pd { get; set; }
        public string bhs_no_actacc_pre { get; set; }
        public string bhs_no_actacc_fr { get; set; }
        public string bhs_no_actacc_pd { get; set; }
        public string bhs_no_arracc_pre { get; set; }
        public string bhs_no_arracc_fr { get; set; }
        public string bhs_no_arracc_pd { get; set; }
        public string bhs_arrears_pre { get; set; }
        public string bhs_arrears_fr { get; set; }
        public string bhs_arrears_pd { get; set; }
        public string bhs_rev_acc { get; set; }
        public string bhs_pc_schnl { get; set; }
        public string bhs_pc_area { get; set; }
        public string bhs_pc_region { get; set; }
        public string bhs_pc_zone { get; set; }
        public static HP_ACC_COMP Converter(DataRow row)
        {
            return new HP_ACC_COMP
            {
                bhs_com = row["bhs_com"] == DBNull.Value ? string.Empty : row["bhs_com"].ToString(),
                bhs_pc = row["bhs_pc"] == DBNull.Value ? string.Empty : row["bhs_pc"].ToString(),
                bhs_pc_desc = row["bhs_pc_desc"] == DBNull.Value ? string.Empty : row["bhs_pc_desc"].ToString(),
                bhs_pc_chnl = row["bhs_pc_chnl"] == DBNull.Value ? string.Empty : row["bhs_pc_chnl"].ToString(),
                bhs_close_bal_pre = row["bhs_close_bal_pre"] == DBNull.Value ? string.Empty : row["bhs_close_bal_pre"].ToString(),
                bhs_close_bal_fr = row["bhs_close_bal_fr"] == DBNull.Value ? string.Empty : row["bhs_close_bal_fr"].ToString(),
                bhs_close_bal_pd = row["bhs_close_bal_pd"] == DBNull.Value ? string.Empty : row["bhs_close_bal_pd"].ToString(),
                bhs_no_actacc_pre = row["bhs_no_actacc_pre"] == DBNull.Value ? string.Empty : row["bhs_no_actacc_pre"].ToString(),
                bhs_no_actacc_fr = row["bhs_no_actacc_fr"] == DBNull.Value ? string.Empty : row["bhs_no_actacc_fr"].ToString(),
                bhs_no_actacc_pd = row["bhs_no_actacc_pd"] == DBNull.Value ? string.Empty : row["bhs_no_actacc_pd"].ToString(),
                bhs_no_arracc_pre = row["bhs_no_arracc_pre"] == DBNull.Value ? string.Empty : row["bhs_no_arracc_pre"].ToString(),
                bhs_no_arracc_fr = row["bhs_no_arracc_fr"] == DBNull.Value ? string.Empty : row["bhs_no_arracc_fr"].ToString(),
                bhs_no_arracc_pd = row["bhs_no_arracc_pd"] == DBNull.Value ? string.Empty : row["bhs_no_arracc_pd"].ToString(),
                bhs_arrears_pre = row["bhs_arrears_pre"] == DBNull.Value ? string.Empty : row["bhs_arrears_pre"].ToString(),
                bhs_arrears_fr = row["bhs_arrears_fr"] == DBNull.Value ? string.Empty : row["bhs_arrears_fr"].ToString(),
                bhs_arrears_pd = row["bhs_arrears_pd"] == DBNull.Value ? string.Empty : row["bhs_arrears_pd"].ToString()
            };
        }

        public static HP_ACC_COMP ConverterSub(DataRow row)
        {
            return new HP_ACC_COMP
            {
                bhs_com = row["bhs_com"] == DBNull.Value ? string.Empty : row["bhs_com"].ToString(),
                bhs_pc = row["bhs_pc"] == DBNull.Value ? string.Empty : row["bhs_pc"].ToString(),
                bhs_pc_desc = row["bhs_pc_desc"] == DBNull.Value ? string.Empty : row["bhs_pc_desc"].ToString(),
                bhs_pc_chnl = row["bhs_pc_chnl"] == DBNull.Value ? string.Empty : row["bhs_pc_chnl"].ToString(),
                bhs_close_bal_pre = row["bhs_close_bal_pre"] == DBNull.Value ? string.Empty : row["bhs_close_bal_pre"].ToString(),
                bhs_close_bal_fr = row["bhs_close_bal_fr"] == DBNull.Value ? string.Empty : row["bhs_close_bal_fr"].ToString(),
                bhs_close_bal_pd = row["bhs_close_bal_pd"] == DBNull.Value ? string.Empty : row["bhs_close_bal_pd"].ToString(),
                bhs_no_actacc_pre = row["bhs_no_actacc_pre"] == DBNull.Value ? string.Empty : row["bhs_no_actacc_pre"].ToString(),
                bhs_no_actacc_fr = row["bhs_no_actacc_fr"] == DBNull.Value ? string.Empty : row["bhs_no_actacc_fr"].ToString(),
                bhs_no_actacc_pd = row["bhs_no_actacc_pd"] == DBNull.Value ? string.Empty : row["bhs_no_actacc_pd"].ToString(),
                bhs_no_arracc_pre = row["bhs_no_arracc_pre"] == DBNull.Value ? string.Empty : row["bhs_no_arracc_pre"].ToString(),
                bhs_no_arracc_fr = row["bhs_no_arracc_fr"] == DBNull.Value ? string.Empty : row["bhs_no_arracc_fr"].ToString(),
                bhs_no_arracc_pd = row["bhs_no_arracc_pd"] == DBNull.Value ? string.Empty : row["bhs_no_arracc_pd"].ToString(),
                bhs_arrears_pre = row["bhs_arrears_pre"] == DBNull.Value ? string.Empty : row["bhs_arrears_pre"].ToString(),
                bhs_arrears_fr = row["bhs_arrears_fr"] == DBNull.Value ? string.Empty : row["bhs_arrears_fr"].ToString(),
                bhs_arrears_pd = row["bhs_arrears_pd"] == DBNull.Value ? string.Empty : row["bhs_arrears_pd"].ToString(),
                bhs_rev_acc = row["bhs_rev_acc"] == DBNull.Value ? string.Empty : row["bhs_rev_acc"].ToString(),
                bhs_pc_schnl = row["bhs_pc_schnl"] == DBNull.Value ? string.Empty : row["bhs_pc_schnl"].ToString(),
                bhs_pc_area = row["bhs_pc_area"] == DBNull.Value ? string.Empty : row["bhs_pc_area"].ToString(),
                bhs_pc_region = row["bhs_pc_region"] == DBNull.Value ? string.Empty : row["bhs_pc_region"].ToString(),
                bhs_pc_zone = row["bhs_pc_zone"] == DBNull.Value ? string.Empty : row["bhs_pc_zone"].ToString()
            };
        }
       
    }
}
