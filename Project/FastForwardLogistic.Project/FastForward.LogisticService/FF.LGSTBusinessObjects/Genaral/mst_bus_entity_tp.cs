using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class mst_bus_entity_tp
    {
        public string met_trans_tp { get; set; }  
        public string met_trans_sbtp {get; set;}
        public string met_desc {get; set;}
        public Int32 met_act {get; set;}
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static mst_bus_entity_tp Converter(DataRow row)
        {
            return new mst_bus_entity_tp
              {
                  met_trans_tp = row["met_trans_tp"] == DBNull.Value ? string.Empty : row["met_trans_tp"].ToString(),
                  //met_trans_sbtp = row["met_trans_sbtp"] == DBNull.Value ? string.Empty : row["met_trans_sbtp"].ToString(),
                  met_desc = row["met_desc"] == DBNull.Value ? string.Empty : row["met_desc"].ToString(),
                  //met_act = row["met_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["met_act"].ToString()),
                  RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                  R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
              };
        }


    }
}
