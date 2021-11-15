using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class gnr_acc_sun_ledger
    {

        public String ledg_comp { get; set; }
        public String ledg_pc { get; set; }
        public String ledg_sales_tp { get; set; }
        public String ledg_sub_tp { get; set; }
        public String ledg_acc_tp { get; set; }
        public String ledg_acc_cd { get; set; }
        public String ledg_desc { get; set; }


        public static gnr_acc_sun_ledger Converter(DataRow row)
        {
            return new gnr_acc_sun_ledger
            {

                ledg_comp = row["LEDG_COMP"] == DBNull.Value ? string.Empty : row["LEDG_COMP"].ToString(),
                ledg_pc = row["LEDG_PC"] == DBNull.Value ? string.Empty : row["LEDG_PC"].ToString(),
                ledg_sales_tp = row["LEDG_SALES_TP"] == DBNull.Value ? string.Empty : row["LEDG_SALES_TP"].ToString(),
                ledg_sub_tp = row["LEDG_SUB_TP"] == DBNull.Value ? string.Empty : row["LEDG_SUB_TP"].ToString(),
                ledg_acc_tp = row["LEDG_ACC_TP"] == DBNull.Value ? string.Empty : row["LEDG_ACC_TP"].ToString(),
                ledg_acc_cd = row["LEDG_ACC_CD"] == DBNull.Value ? string.Empty : row["LEDG_ACC_CD"].ToString(),
                ledg_desc = row["LEDG_DESC"] == DBNull.Value ? string.Empty : row["LEDG_DESC"].ToString(),

            };
        }
    }
}
