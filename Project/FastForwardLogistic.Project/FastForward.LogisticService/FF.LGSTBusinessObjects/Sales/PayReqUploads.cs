using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
  public  class PayReqUploads
    {
        public string tprh_req_no { get; set; }
        public string tprh_manual_ref { get; set; }
        public DateTime tprh_req_dt { get; set; }
        public string tprh_remarks { get; set; }
        public decimal tprh_tot_amt { get; set; }
        public string tpsh_pc_cd { get; set; }
        public string Customer { get; set; }
        public string tprd_element_cd { get; set; }
        public decimal tprd_element_amt { get; set; }
        public string tprd_element_desc { get; set; }
        public static PayReqUploads Converter(DataRow row)
        {
            return new PayReqUploads
            {

                tprh_req_no = row["tprh_req_no"] == DBNull.Value ? string.Empty : row["tprh_req_no"].ToString(),
                tprh_manual_ref = row["tprh_manual_ref"] == DBNull.Value ? string.Empty : row["tprh_manual_ref"].ToString(),
                tprh_req_dt = row["tprh_req_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["tprh_req_dt"].ToString()),
                tprh_remarks = row["tprh_remarks"] == DBNull.Value ? string.Empty : row["tprh_remarks"].ToString(),
                tprh_tot_amt = row["tprh_tot_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["tprh_tot_amt"]),
                tpsh_pc_cd = row["tpsh_pc_cd"] == DBNull.Value ? string.Empty : row["tpsh_pc_cd"].ToString(),
                Customer = row["Customer"] == DBNull.Value ? string.Empty : row["Customer"].ToString(),
                tprd_element_cd = row["tprd_element_cd"] == DBNull.Value ? string.Empty : row["tprd_element_cd"].ToString(),
                tprd_element_amt = row["tprd_element_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["tprd_element_amt"]),
                tprd_element_desc = row["tprd_element_desc"] == DBNull.Value ? string.Empty : row["tprd_element_desc"].ToString(),
            };
        }
    }
}
