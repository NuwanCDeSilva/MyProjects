using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
   public class trn_req_serdet
    {
       public string rs_seq_no { get; set; }
       public Int32 rs_line_no { get; set; }
       public string rs_ser_tp { get; set; }
       public string rs_pc { get; set; }
       public string rs_rmk { get; set; }
       public string rs_cre_by { get; set; }
       public DateTime rs_cre_dt { get; set; }
       public string rs_mod_by { get; set; }
       public DateTime rs_mod_dt { get; set; }
       public string rs_cus_cd { get; set; }
       public string rs_mser_tp { get; set; }

       public static trn_req_serdet Converter(DataRow row)
       {
           return new trn_req_serdet
           {
               rs_seq_no = row["rs_seq_no"] == DBNull.Value ? string.Empty : row["rs_seq_no"].ToString(),
               rs_line_no = row["rs_line_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["rs_line_no"].ToString()),
               rs_ser_tp = row["rs_ser_tp"] == DBNull.Value ? string.Empty : row["rs_ser_tp"].ToString(),
               rs_pc = row["rs_pc"] == DBNull.Value ? string.Empty : row["rs_pc"].ToString(),
               rs_rmk = row["rs_rmk"] == DBNull.Value ? string.Empty : row["rs_rmk"].ToString(),
               rs_cre_by = row["rs_cre_by"] == DBNull.Value ? string.Empty : row["rs_cre_by"].ToString(),
               rs_cre_dt = row["rs_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rs_cre_dt"].ToString()),
               rs_mod_by = row["rs_mod_by"] == DBNull.Value ? string.Empty : row["rs_mod_by"].ToString(),
               rs_mod_dt = row["rs_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rs_mod_dt"].ToString()),
               rs_cus_cd = row["rs_cus_cd"] == DBNull.Value ? string.Empty : row["rs_cus_cd"].ToString(),
               rs_mser_tp = row["rs_mser_tp"] == DBNull.Value ? string.Empty : row["rs_mser_tp"].ToString(),
           };
       }

    }
}
