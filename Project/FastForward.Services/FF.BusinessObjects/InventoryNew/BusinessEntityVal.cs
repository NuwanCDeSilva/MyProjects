using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
     [Serializable]
    public class BusinessEntityVal
    {
        public string rbv_cate { get; set; }
        public string rbv_tp { get; set; }
        public string rbv_val { get; set; }
        public string rbv_com { get; set; }
        public string model { get; set; }
        public int active { get; set; }


        public static BusinessEntityVal Converter(DataRow row)
        {
            return new BusinessEntityVal
            {
                rbv_tp = row["mmcl_tp"] == DBNull.Value ? string.Empty : row["mmcl_tp"].ToString(),
                rbv_val = row["mmcl_val"] == DBNull.Value ? string.Empty : row["mmcl_val"].ToString(),
                rbv_com = row["mmcl_com"] == DBNull.Value ? string.Empty : row["mmcl_com"].ToString(),
                active = row["mbei_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbei_act"].ToString()),
                model = row["mmcl_model"] == DBNull.Value ? string.Empty : row["mmcl_model"].ToString(),
               
            };
        }
    }
}
