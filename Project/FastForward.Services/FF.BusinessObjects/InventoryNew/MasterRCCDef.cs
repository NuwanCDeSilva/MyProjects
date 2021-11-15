using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterRCCDef
    {
        #region Private Members
        private Int32 _ird_act;
        private Int32 _ird_cd;
        private string _ird_desc;
        private string _ird_tp;
        #endregion

        public Int32 Ird_act { get; set; }
        public Int32 Ird_cd { get; set; }
        public string Ird_desc { get; set; }
        public string Ird_tp { get; set; }

        public static MasterRCCDef Converter(DataRow row)
        {
            return new MasterRCCDef
            {
                Ird_act = row["IRD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_ACT"]),
                Ird_cd = row["IRD_CD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRD_CD"]),
                Ird_desc = row["IRD_DESC"] == DBNull.Value ? string.Empty : row["IRD_DESC"].ToString(),
                Ird_tp = row["IRD_TP"] == DBNull.Value ? string.Empty : row["IRD_TP"].ToString()

            };
        }
    }
}
