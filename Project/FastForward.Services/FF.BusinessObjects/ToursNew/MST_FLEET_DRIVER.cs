using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class mst_fleet_driver
    {
        public Int32 MFD_SEQ { get; set; }
        public String MFD_VEH_NO { get; set; }
        public String MFD_DRI { get; set; }
        public Int32 MFD_ACT { get; set; }
        public DateTime MFD_FRM_DT { get; set; }
        public DateTime MFD_TO_DT { get; set; }
        public String MFD_CRE_BY { get; set; }
        public DateTime MFD_CRE_DT { get; set; }
        public String MFD_MOD_BY { get; set; }
        public DateTime MFD_MOD_DT { get; set; }
        public String MFD_COM { get; set; }
        public String MFD_PC { get; set; }
        public static mst_fleet_driver Converter(DataRow row)
        {
            return new mst_fleet_driver
            {
                MFD_SEQ = row["MFD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFD_SEQ"].ToString()),
                MFD_VEH_NO = row["MFD_VEH_NO"] == DBNull.Value ? string.Empty : row["MFD_VEH_NO"].ToString(),
                MFD_DRI = row["MFD_DRI"] == DBNull.Value ? string.Empty : row["MFD_DRI"].ToString(),
                MFD_ACT = row["MFD_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFD_ACT"].ToString()),
                MFD_FRM_DT = row["MFD_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFD_FRM_DT"].ToString()),
                MFD_TO_DT = row["MFD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFD_TO_DT"].ToString()),
                MFD_CRE_BY = row["MFD_CRE_BY"] == DBNull.Value ? string.Empty : row["MFD_CRE_BY"].ToString(),
                MFD_CRE_DT = row["MFD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFD_CRE_DT"].ToString()),
                MFD_MOD_BY = row["MFD_MOD_BY"] == DBNull.Value ? string.Empty : row["MFD_MOD_BY"].ToString(),
                MFD_MOD_DT = row["MFD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFD_MOD_DT"].ToString()),
                MFD_COM = row["MFD_COM"] == DBNull.Value ? string.Empty : row["MFD_COM"].ToString(),
                MFD_PC = row["MFD_PC"] == DBNull.Value ? string.Empty : row["MFD_PC"].ToString()
            };
        }
    }
}
