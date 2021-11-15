using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
     [Serializable]
    public class mst_commodel
    {
        public string Mcm_com { get; set; }
        public string Mcm_com_desc { get; set; }
        public string Mcm_model { get; set; }
        public bool Mcm_isfoc { get; set; }
        public string Mcm_isfoc_status { get; set; }
        public bool Mcm_act { get; set; }
        public string Mcm_act_status { get; set; }
        public bool Mcm_hpqty_chk { get; set; }
        public string Mcm_restric_inv_tp { get; set; }

        public static mst_commodel Converter(DataRow row)
        {
            return new mst_commodel
            {
                Mcm_com = row["Mcm_com"] == DBNull.Value ? string.Empty : row["Mcm_com"].ToString(),
                Mcm_com_desc = row["mc_desc"] == DBNull.Value ? string.Empty : row["mc_desc"].ToString(),
                Mcm_model = row["Mcm_model"] == DBNull.Value ? string.Empty : row["Mcm_model"].ToString(),
                Mcm_isfoc = row["Mcm_isfoc"] == DBNull.Value ? false : Convert.ToBoolean(row["Mcm_isfoc"]),
                Mcm_hpqty_chk = row["Mcm_hpqty_chk"] == DBNull.Value ? false : Convert.ToBoolean(row["Mcm_hpqty_chk"]),
                Mcm_act = row["Mcm_act"] == DBNull.Value ? false : Convert.ToBoolean(row["Mcm_act"]),
                Mcm_restric_inv_tp = row["Mcm_restric_inv_tp"] == DBNull.Value ? string.Empty : row["Mcm_restric_inv_tp"].ToString(),

                Mcm_act_status = Convert.ToInt32(row["Mcm_act"]) == 0 ? "NO" : "YES",
                Mcm_isfoc_status = Convert.ToInt32(row["Mcm_isfoc"]) == 0 ? "NO" : "YES"

            };
        }
    }
}
