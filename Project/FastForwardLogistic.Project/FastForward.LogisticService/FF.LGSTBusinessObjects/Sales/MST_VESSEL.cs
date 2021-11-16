using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FF.BusinessObjects.Sales
{
    public class MST_VESSEL
    {
        public String VM_VESSAL_CD { get; set; }
        public String VM_VESSAL_NAME { get; set; }
        public String VM_COUNTRY_CD { get; set; }
        public String VM_MOS_CD { get; set; }
        public String VM_RMK { get; set; }
        public Int32 VM_ACT { get; set; }
        public String VM_CRE_BY { get; set; }
        public DateTime VM_CRE_DT { get; set; }
        public String VM_MOD_BY { get; set; }
        public DateTime VM_MOD_DT { get; set; }
        public Int32 VM_CRE_SESSION_ID { get; set; }
        public Int32 VM_MOD_SESSION_ID { get; set; }
        public Int32 MCE_SEQ { get; set; }
        public String MCE_CD { get; set; }
        public String MCE_DESC { get; set; }
        public Int32 MCE_ACT { get; set; }
        public String MCE_CRE_BY { get; set; }
        public DateTime MCE_CRE_DT { get; set; }
        public String MCE_MOD_BY { get; set; }
        public DateTime MCE_MOD_DT { get; set; }
        public Int32 MCE_IGNORE { get; set; }
        public String MCE_ACC_CD { get; set; }
        public String MCE_COST_REV_STS { get; set; }

        public String PA_PRT_CD { get; set; }
        public String PA_PRT_NAME { get; set; }
        public String PA_CUNTRY_CD { get; set; }
        public String PA_MOS_CD { get; set; }
        public String PA_RMK { get; set; }
        public Int32 PA_ACT { get; set; }
        public String PA_CRE_BY { get; set; }
        public DateTime PA_CRE_DT { get; set; }
        public String PA_MOD_BY { get; set; }
        public DateTime PA_MOD_DT { get; set; }        

        public static MST_VESSEL Converter(DataRow row)
        {
            return new MST_VESSEL
            {
                VM_VESSAL_CD = row["VM_VESSAL_CD"] == DBNull.Value ? string.Empty : row["VM_VESSAL_CD"].ToString(),
                VM_VESSAL_NAME = row["VM_VESSAL_NAME"] == DBNull.Value ? string.Empty : row["VM_VESSAL_NAME"].ToString(),
                VM_COUNTRY_CD = row["VM_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["VM_COUNTRY_CD"].ToString(),
                VM_MOS_CD = row["VM_MOS_CD"] == DBNull.Value ? string.Empty : row["VM_MOS_CD"].ToString(),
                VM_RMK = row["VM_RMK"] == DBNull.Value ? string.Empty : row["VM_RMK"].ToString(),
                VM_ACT = row["VM_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["VM_ACT"].ToString()),
                VM_CRE_BY = row["VM_CRE_BY"] == DBNull.Value ? string.Empty : row["VM_CRE_BY"].ToString(),
                VM_CRE_DT = row["VM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["VM_CRE_DT"].ToString()),
                VM_MOD_BY = row["VM_MOD_BY"] == DBNull.Value ? string.Empty : row["VM_MOD_BY"].ToString(),
                VM_MOD_DT = row["VM_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["VM_MOD_DT"].ToString()),
                VM_CRE_SESSION_ID = row["VM_CRE_SESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["VM_CRE_SESSION_ID"].ToString()),
                VM_MOD_SESSION_ID = row["VM_MOD_SESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["VM_MOD_SESSION_ID"].ToString())
            };
        }

        public static MST_VESSEL Converter1(DataRow row)
        {
            return new MST_VESSEL
            {
                MCE_SEQ = row["MCE_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_SEQ"].ToString()),
                MCE_CD = row["MCE_CD"] == DBNull.Value ? string.Empty : row["MCE_CD"].ToString(),
                MCE_DESC = row["MCE_DESC"] == DBNull.Value ? string.Empty : row["MCE_DESC"].ToString(),
                MCE_ACT = row["MCE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_ACT"].ToString()),
                MCE_CRE_BY = row["MCE_CRE_BY"] == DBNull.Value ? string.Empty : row["MCE_CRE_BY"].ToString(),
                MCE_CRE_DT = row["MCE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCE_CRE_DT"].ToString()),
                MCE_MOD_BY = row["MCE_MOD_BY"] == DBNull.Value ? string.Empty : row["MCE_MOD_BY"].ToString(),
                MCE_MOD_DT = row["MCE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCE_MOD_DT"].ToString()),
                MCE_IGNORE = row["MCE_IGNORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_IGNORE"].ToString()),
                MCE_ACC_CD = row["MCE_ACC_CD"] == DBNull.Value ? string.Empty : row["MCE_ACC_CD"].ToString(),
                MCE_COST_REV_STS = row["MCE_COST_REV_STS"] == DBNull.Value ? string.Empty : row["MCE_COST_REV_STS"].ToString()
            };
        }

        public static MST_VESSEL Converter2(DataRow row)
        {
            return new MST_VESSEL
            {
                PA_PRT_CD = row["PA_PRT_CD"] == DBNull.Value ? string.Empty : row["PA_PRT_CD"].ToString(),
                PA_PRT_NAME = row["PA_PRT_NAME"] == DBNull.Value ? string.Empty : row["PA_PRT_NAME"].ToString(),
                PA_CUNTRY_CD = row["PA_CUNTRY_CD"] == DBNull.Value ? string.Empty : row["PA_CUNTRY_CD"].ToString(),
                PA_MOS_CD = row["PA_MOS_CD"] == DBNull.Value ? string.Empty : row["PA_MOS_CD"].ToString(),
                PA_RMK = row["PA_RMK"] == DBNull.Value ? string.Empty : row["PA_RMK"].ToString(),
                PA_ACT = row["PA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PA_ACT"].ToString()),
                PA_CRE_BY = row["PA_CRE_BY"] == DBNull.Value ? string.Empty : row["PA_CRE_BY"].ToString(),
                PA_CRE_DT = row["PA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PA_CRE_DT"].ToString()),
                PA_MOD_BY = row["PA_MOD_BY"] == DBNull.Value ? string.Empty : row["PA_MOD_BY"].ToString(),
                PA_MOD_DT = row["PA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PA_MOD_DT"].ToString()),
            };
        }
    }
}
