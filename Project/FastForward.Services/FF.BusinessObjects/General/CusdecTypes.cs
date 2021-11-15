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
        // Computer :- MILKYWAY  | User :- Chamald On 09-Jan-2016 06:48:45
        //===========================================================================================================

        public class CusdecTypes
        {
            public String Rcut_cnty { get; set; }
            public String Rcut_tp { get; set; }
            public String Rcut_desc { get; set; }
            public Int32 Rcut_act { get; set; }
            public Int32 Rcuit_is_sub { get; set; }
            public Int32 Rcuit_is_hs { get; set; }
            public Int32 Rcuit_duty_mp { get; set; }
            public Int32 Rcuit_grup_id { get; set; }
            public String Rcuit_auto_no_char { get; set; }
            public static CusdecTypes Converter(DataRow row)
            {
                return new CusdecTypes
                {
                    Rcut_cnty = row["RCUT_CNTY"] == DBNull.Value ? string.Empty : row["RCUT_CNTY"].ToString(),
                    Rcut_tp = row["RCUT_TP"] == DBNull.Value ? string.Empty : row["RCUT_TP"].ToString(),
                    Rcut_desc = row["RCUT_DESC"] == DBNull.Value ? string.Empty : row["RCUT_DESC"].ToString(),
                    Rcut_act = row["RCUT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCUT_ACT"].ToString()),
                    Rcuit_is_sub = row["RCUIT_IS_SUB"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCUIT_IS_SUB"].ToString()),
                    Rcuit_is_hs = row["RCUIT_IS_HS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCUIT_IS_HS"].ToString()),
                    Rcuit_duty_mp = row["RCUIT_DUTY_MP"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCUIT_DUTY_MP"].ToString()),
                    Rcuit_grup_id = row["RCUIT_GRUP_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCUIT_GRUP_ID"].ToString()),
                    Rcuit_auto_no_char = row["RCUIT_AUTO_NO_CHAR"] == DBNull.Value ? string.Empty : row["RCUIT_AUTO_NO_CHAR"].ToString()
                };
            }
        } 
}