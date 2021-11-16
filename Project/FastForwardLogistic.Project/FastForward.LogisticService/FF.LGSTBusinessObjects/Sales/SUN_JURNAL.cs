using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class SUN_JURNAL
    {
        public String ledg_sales_tp { get; set; }
        public String ledg_sub_tp { get; set; }
        public string ledg_acc_tp { get; set; }
        public String ledg_acc_cd { get; set; }
        public string ledg_jnl_tp { get; set; }
        public String ledg_desc { get; set; }
        public string ledg_is_dealer { get; set; }
        public string ledg_pc { get; set; }
        public string ledg_jnl_desc { get; set; }

        public static SUN_JURNAL Converter(DataRow row)
        {
            return new SUN_JURNAL
            {

                ledg_sales_tp = row["ledg_sales_tp"] == DBNull.Value ? string.Empty : row["ledg_sales_tp"].ToString(),
                ledg_sub_tp = row["ledg_sub_tp"] == DBNull.Value ? string.Empty : row["ledg_sub_tp"].ToString(),
                ledg_acc_tp = row["ledg_acc_tp"] == DBNull.Value ? string.Empty : row["ledg_acc_tp"].ToString(),
                ledg_acc_cd = row["ledg_acc_cd"] == DBNull.Value ? string.Empty : row["ledg_acc_cd"].ToString(),
                ledg_jnl_tp = row["ledg_jnl_tp"] == DBNull.Value ? string.Empty : row["ledg_jnl_tp"].ToString(),
                ledg_desc = row["ledg_desc"] == DBNull.Value ? string.Empty : row["ledg_desc"].ToString(),
                ledg_is_dealer = row["ledg_is_dealer"] == DBNull.Value ? string.Empty : row["ledg_is_dealer"].ToString(),
                ledg_pc = row["ledg_pc"] == DBNull.Value ? string.Empty : row["ledg_pc"].ToString(),
                ledg_jnl_desc = row["ledg_jnl_desc"] == DBNull.Value ? string.Empty : row["ledg_jnl_desc"].ToString(),


            };
        }
    }
}
