using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects.General
{
   public class ITM_PLU
    {
        public String Mi_cd { get; set; }
        public String mi_longdesc { get; set; }
        public String mi_model { get; set; }
        public String mi_brand { get; set; }
        public String mbii_plu_cd { get; set; }
        public String mi_is_ser1 { get; set; }
        public String mi_warr { get; set; }

        

           public static ITM_PLU Converter(DataRow row)
        {
            return new ITM_PLU
            {
                Mi_cd = row["Mi_cd"] == DBNull.Value ? string.Empty : row["Mi_cd"].ToString(),
                mi_longdesc = row["mi_longdesc"] == DBNull.Value ? string.Empty : row["mi_longdesc"].ToString(),
                mi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),
                mi_brand = row["mi_brand"] == DBNull.Value ? string.Empty : row["mi_brand"].ToString(),
                mbii_plu_cd = row["mbii_plu_cd"] == DBNull.Value ? string.Empty : row["mbii_plu_cd"].ToString(),
                mi_is_ser1 = row["mi_is_ser1"] == DBNull.Value ? string.Empty : row["mi_is_ser1"].ToString(),
                mi_warr = row["mi_warr"] == DBNull.Value ? string.Empty : row["mi_warr"].ToString(),
            };
        }
    }
}
