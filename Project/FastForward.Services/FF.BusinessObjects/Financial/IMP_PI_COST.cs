﻿using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD12  | User :- pemil On 24-Sep-2015 04:04:30
    //===========================================================================================================

    public class IMP_PI_COST
    {
        public Int32 IPC_SEQ_NO { get; set; }
        public String IPC_PI_NO { get; set; }
        public Int32 IPC_LINE { get; set; }
        public String IPC_ELE_CAT { get; set; }
        public String IPC_ELE_TP { get; set; }
        public String IPC_ELE_CD { get; set; }
        public Decimal IPC_AMT { get; set; }
        public Decimal IPC_AMT_DEAL { get; set; }
        public Decimal IPC_EX_RT { get; set; }
        public Int32 IPC_ACT { get; set; }
        public String IPC_ANAL_1 { get; set; }
        public String IPC_ANAL_2 { get; set; }
        public String IPC_ANAL_3 { get; set; }
        public String IPC_ANAL_4 { get; set; }
        public String IPC_ANAL_5 { get; set; }
        public String IPC_CRE_BY { get; set; }
        public DateTime IPC_CRE_DT { get; set; }
        public String IPC_SESSION_ID { get; set; }
        public static IMP_PI_COST Converter(DataRow row)
        {
            return new IMP_PI_COST
            {
                IPC_SEQ_NO = row["IPC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPC_SEQ_NO"].ToString()),
                IPC_PI_NO = row["IPC_PI_NO"] == DBNull.Value ? string.Empty : row["IPC_PI_NO"].ToString(),
                IPC_LINE = row["IPC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPC_LINE"].ToString()),
                IPC_ELE_CAT = row["IPC_ELE_CAT"] == DBNull.Value ? string.Empty : row["IPC_ELE_CAT"].ToString(),
                IPC_ELE_TP = row["IPC_ELE_TP"] == DBNull.Value ? string.Empty : row["IPC_ELE_TP"].ToString(),
                IPC_ELE_CD = row["IPC_ELE_CD"] == DBNull.Value ? string.Empty : row["IPC_ELE_CD"].ToString(),
                IPC_AMT = row["IPC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPC_AMT"].ToString()),
                IPC_AMT_DEAL = row["IPC_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPC_AMT_DEAL"].ToString()),
                IPC_EX_RT = row["IPC_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPC_EX_RT"].ToString()),
                IPC_ACT = row["IPC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPC_ACT"].ToString()),
                IPC_ANAL_1 = row["IPC_ANAL_1"] == DBNull.Value ? string.Empty : row["IPC_ANAL_1"].ToString(),
                IPC_ANAL_2 = row["IPC_ANAL_2"] == DBNull.Value ? string.Empty : row["IPC_ANAL_2"].ToString(),
                IPC_ANAL_3 = row["IPC_ANAL_3"] == DBNull.Value ? string.Empty : row["IPC_ANAL_3"].ToString(),
                IPC_ANAL_4 = row["IPC_ANAL_4"] == DBNull.Value ? string.Empty : row["IPC_ANAL_4"].ToString(),
                IPC_ANAL_5 = row["IPC_ANAL_5"] == DBNull.Value ? string.Empty : row["IPC_ANAL_5"].ToString(),
                IPC_CRE_BY = row["IPC_CRE_BY"] == DBNull.Value ? string.Empty : row["IPC_CRE_BY"].ToString(),
                IPC_CRE_DT = row["IPC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPC_CRE_DT"].ToString()),
                IPC_SESSION_ID = row["IPC_SESSION_ID"] == DBNull.Value ? string.Empty : row["IPC_SESSION_ID"].ToString()
            };
        }
    }
}
