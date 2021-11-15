﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- MILKYWAY  | User :- Chamald On 07-Feb-2016 08:34:20
    //===========================================================================================================

    public class CusdecCommon
    {
        public String Rcsc_cnty { get; set; }
        public String Rcsc_acc_no { get; set; }
        public String Rcsc_licence_no { get; set; }
        public String Rcsc_wh_no { get; set; }
        public String Rcsc_auth_by { get; set; }
        public String Rcsc_submit_by { get; set; }
        public String Rcsc_id_no { get; set; }
        public Decimal Rcsc_comchg_0{ get; set; }
        public Decimal Rcsc_comchg_1{ get; set; }
        public Decimal Rcsc_comchg_up{ get; set; }
        public static CusdecCommon Converter(DataRow row)
        {
            return new CusdecCommon
            {
                Rcsc_cnty = row["RCSC_CNTY"] == DBNull.Value ? string.Empty : row["RCSC_CNTY"].ToString(),
                Rcsc_acc_no = row["RCSC_ACC_NO"] == DBNull.Value ? string.Empty : row["RCSC_ACC_NO"].ToString(),
                Rcsc_licence_no = row["RCSC_LICENCE_NO"] == DBNull.Value ? string.Empty : row["RCSC_LICENCE_NO"].ToString(),
                Rcsc_wh_no = row["RCSC_WH_NO"] == DBNull.Value ? string.Empty : row["RCSC_WH_NO"].ToString(),
                Rcsc_auth_by = row["RCSC_AUTH_BY"] == DBNull.Value ? string.Empty : row["RCSC_AUTH_BY"].ToString(),
                Rcsc_submit_by = row["RCSC_SUBMIT_BY"] == DBNull.Value ? string.Empty : row["RCSC_SUBMIT_BY"].ToString(),
                Rcsc_id_no = row["RCSC_ID_NO"] == DBNull.Value ? string.Empty : row["RCSC_ID_NO"].ToString(),
                Rcsc_comchg_0 = row["RCSC_COMCHG_0"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCSC_COMCHG_0"]),
                Rcsc_comchg_1 = row["RCSC_COMCHG_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCSC_COMCHG_1"]),
                Rcsc_comchg_up = row["RCSC_COMCHG_UP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCSC_COMCHG_UP"])
            };
        }
    } 
}