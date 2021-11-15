using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 25-07-2013
    //table = HPT_ACCOUNTS_AGREEMENT
    [Serializable]
    public class glb_hp_rec_age
    {
        public string COM_CODE { get; set; }
        public string COM_DESC { get; set; }
        public string POWERED_BY { get; set; }
        public string PC_CODE { get; set; }
        public string PC_DESC { get; set; }
        public string ACC_NO { get; set; }
        public Int32 YEAR { get; set; }
        public Int32 MONTH { get; set; }
        public Decimal ARR_0 { get; set; }
        public Decimal ARR_1 { get; set; }
        public Decimal ARR_2 { get; set; }
        public Decimal ARR_3 { get; set; }
        public Decimal ARR_4 { get; set; }
        public Decimal ARR_5 { get; set; }
        public Decimal ARR_6 { get; set; }
        public Decimal ARR_7 { get; set; }
        public Decimal ARR_8 { get; set; }
        public Decimal ARR_9 { get; set; }
        public Decimal ARR_10 { get; set; }
        public Decimal ARR_11 { get; set; }
        public Decimal ARR_12 { get; set; }
        public Decimal ARR_13 { get; set; }
        public Decimal ARR_14 { get; set; }
        public Decimal ARR_15 { get; set; }
        public Decimal ARR_16 { get; set; }
        public Decimal ARR_17 { get; set; }
        public Decimal ARR_18 { get; set; }
        public Decimal ARR_19 { get; set; }
        public Decimal ARR_20 { get; set; }
        public Decimal ARR_21 { get; set; }
        public Decimal ARR_22 { get; set; }
        public Decimal ARR_23 { get; set; }
        public Decimal ARR_24 { get; set; }
        public Decimal ARR_N { get; set; }

        public Decimal TOTAL { get; set; }
        public string USER_ID { get; set; }
        public Decimal INTR_RATE { get; set; }
        public Decimal HP_ARREARS { get; set; }
        public Decimal HP_TERM { get; set; }
        public Decimal DUE_1 { get; set; }

        public Decimal DUE_2 { get; set; }
        public Decimal DUE_3 { get; set; }
        public Decimal DUE_4 { get; set; }
        public Decimal DUE_5 { get; set; }
        public Decimal DUE_6 { get; set; }
        public Decimal DUE_N { get; set; }



                public Decimal ARR_0_RCV_CAP { get; set; }
         public Decimal ARR_0_RCV_INTR { get; set; }
         public Decimal ARR_1_RCV_CAP { get; set; }
         public Decimal ARR_1_RCV_INTR{ get; set; }
         public Decimal ARR_1_DUE_CAP{ get; set; }
         public Decimal ARR_1_DUE_INTR{ get; set; }
         public Decimal ARR_2_RCV_CAP{ get; set; }
         public Decimal ARR_2_RCV_INTR{ get; set; }
        public Decimal ARR_2_DUE_CAP{ get; set; }
         public Decimal ARR_2_DUE_INTR{ get; set; }
         public Decimal ARR_3_RCV_CAP{ get; set; }
         public Decimal ARR_3_RCV_INTR{ get; set; }
         public Decimal ARR_3_DUE_CAP{ get; set; }
         public Decimal ARR_3_DUE_INTR{ get; set; }
         public Decimal ARR_4_RCV_CAP{ get; set; }
         public Decimal ARR_4_RCV_INTR{ get; set; }
         public Decimal ARR_4_DUE_CAP{ get; set; }
         public Decimal ARR_4_DUE_INTR{ get; set; }
         public Decimal ARR_5_RCV_CAP{ get; set; }
         public Decimal ARR_5_RCV_INTR{ get; set; }
         public Decimal ARR_5_DUE_CAP{ get; set; }
         public Decimal ARR_5_DUE_INTR{ get; set; }
         public Decimal ARR_6_RCV_CAP{ get; set; }
         public Decimal ARR_6_RCV_INTR{ get; set; }
         public Decimal ARR_6_DUE_CAP{ get; set; }
         public Decimal ARR_6_DUE_INTR{ get; set; }
         public Decimal ARR_7_CAP{ get; set; }
         public Decimal ARR_7_INTR{ get; set; }
         public Decimal ARR_8_CAP{ get; set; }
        public Decimal ARR_8_INTR{ get; set; }
         public Decimal ARR_9_CAP{ get; set; }
         public Decimal ARR_9_INTR{ get; set; }
         public Decimal ARR_10_CAP{ get; set; }
         public Decimal ARR_10_INTR{ get; set; }
         public Decimal ARR_11_CAP{ get; set; }
         public Decimal ARR_11_INTR{ get; set; }
         public Decimal ARR_12_CAP{ get; set; }
         public Decimal ARR_12_INTR{ get; set; }
         public Decimal ARR_13_CAP{ get; set; }
         public Decimal ARR_13_INTR{ get; set; }
         public Decimal ARR_14_CAP{ get; set; }
         public Decimal ARR_14_INTR{ get; set; }
         public Decimal ARR_15_CAP{ get; set; }
         public Decimal ARR_15_INTR{ get; set; }
         public Decimal ARR_16_CAP{ get; set; }
         public Decimal ARR_16_INTR{ get; set; }
         public Decimal ARR_17_CAP{ get; set; }
         public Decimal ARR_17_INTR{ get; set; }
         public Decimal ARR_18_CAP{ get; set; }
         public Decimal ARR_18_INTR{ get; set; }
         public Decimal ARR_19_CAP{ get; set; }
         public Decimal ARR_19_INTR{ get; set; }
         public Decimal ARR_20_CAP{ get; set; }
         public Decimal ARR_20_INTR{ get; set; }
         public Decimal ARR_21_CAP{ get; set; }
         public Decimal ARR_21_INTR{ get; set; }
         public Decimal ARR_22_CAP{ get; set; }
         public Decimal ARR_22_INTR{ get; set; }
         public Decimal ARR_23_CAP{ get; set; }
         public Decimal ARR_23_INTR{ get; set; }
         public Decimal ARR_24_CAP{ get; set; }
         public Decimal ARR_24_INTR{ get; set; }
         public Decimal ARR_N_RCV_CAP{ get; set; }
         public Decimal ARR_N_RCV_INTR{ get; set; }
         public Decimal ARR_N_DUE_CAP{ get; set; }
         public Decimal ARR_N_DUE_INTR{ get; set; }
         public Decimal TOTAL_CAP{ get; set; }
         public Decimal TOTAL_INTR{ get; set; }
         public Decimal ARREARS_CAP{ get; set; }
         public Decimal ARREARS_INTR { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static glb_hp_rec_age Converter(DataRow row)
        {
            return new glb_hp_rec_age
            {

                COM_CODE = row["COM_CODE"] == DBNull.Value ? string.Empty : row["COM_CODE"].ToString(),
                COM_DESC = row["COM_DESC"] == DBNull.Value ? string.Empty : row["COM_DESC"].ToString(),
                POWERED_BY = row["POWERED_BY"] == DBNull.Value ? string.Empty : row["POWERED_BY"].ToString(),
                PC_CODE = row["PC_CODE"] == DBNull.Value ? string.Empty : row["PC_CODE"].ToString(),
                PC_DESC = row["PC_DESC"] == DBNull.Value ? string.Empty : row["PC_DESC"].ToString(),
                ACC_NO = row["ACC_NO"] == DBNull.Value ? string.Empty : row["ACC_NO"].ToString(),
                YEAR = row["YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["YEAR"]),
                MONTH = row["YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MONTH"]),
                ARR_0 = row["ARR_0"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_0"]),
                ARR_1 = row["ARR_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_1"]),
                ARR_2 = row["ARR_2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_2"]),
                ARR_3= row["ARR_3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_3"]),
                ARR_4 = row["ARR_4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_4"]),
                ARR_5 = row["ARR_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_5"]),
                ARR_6 = row["ARR_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_6"]),
                ARR_7 = row["ARR_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_7"]),
                ARR_8 = row["ARR_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_8"]),
                ARR_9 = row["ARR_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_9"]),
                ARR_10 = row["ARR_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_10"]),
                ARR_11 = row["ARR_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_11"]),
                ARR_12 = row["ARR_12"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_12"]),
                ARR_13 = row["ARR_13"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_13"]),
                ARR_14 = row["ARR_14"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_14"]),
                ARR_15 = row["ARR_15"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_15"]),
                ARR_16 = row["ARR_16"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_16"]),
                ARR_17 = row["ARR_17"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_17"]),
                ARR_18 = row["ARR_18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_18"]),
                ARR_19 = row["ARR_19"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_19"]),
                ARR_20 = row["ARR_20"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_20"]),
                ARR_21 = row["ARR_21"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_21"]),
                ARR_22 = row["ARR_22"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_22"]),
                ARR_23 = row["ARR_23"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_23"]),
                ARR_24 = row["ARR_24"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_24"]),
                ARR_N = row["ARR_N"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ARR_N"]),
                TOTAL = row["TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL"]),
                USER_ID = row["USER_ID"] == DBNull.Value ? string.Empty : row["USER_ID"].ToString(),
                INTR_RATE = row["intr_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["intr_rate"]),
                HP_ARREARS = row["HP_ARREARS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HP_ARREARS"]),
                HP_TERM = row["HP_TERM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HP_TERM"]),
                DUE_1 = row["DUE_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_1"]),
                DUE_2 = row["DUE_2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_2"]),
                DUE_3 = row["DUE_3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_3"]),
                DUE_4 = row["DUE_4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_4"]),
                DUE_5 = row["DUE_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_5"]),
                DUE_6 = row["DUE_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_6"]),
                DUE_N = row["DUE_N"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DUE_N"]),


            };
        }

    }
}
