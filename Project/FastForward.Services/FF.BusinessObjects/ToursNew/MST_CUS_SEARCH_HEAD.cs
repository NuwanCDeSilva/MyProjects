using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
    public class MST_CUS_SEARCH_HEAD
    {
        public string Mbe_cd { get; set; }
        public string Mbe_name { get; set; }
        public string Mbe_nic { get; set; }
        public string Mbe_mob { get; set; }
        public string Mbe_br_no { get; set; }
        public string MBE_ADD1 { get; set; }
        public string MBE_ADD2 { get; set; }
        public string MBE_TAX_NO { get; set; }
        public string Mbe_pp_no { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MST_CUS_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CUS_SEARCH_HEAD
            {
                Mbe_cd = row["Mbe_cd"] == DBNull.Value ? string.Empty : row["Mbe_cd"].ToString(),
                Mbe_name = row["Mbe_name"] == DBNull.Value ? string.Empty : row["Mbe_name"].ToString(),
                Mbe_nic = row["Mbe_nic"] == DBNull.Value ? string.Empty : row["Mbe_nic"].ToString(),
                Mbe_mob = row["Mbe_mob"] == DBNull.Value ? string.Empty : row["Mbe_mob"].ToString(),
                Mbe_br_no = row["Mbe_br_no"] == DBNull.Value ? string.Empty : row["Mbe_br_no"].ToString(),
                MBE_ADD1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                MBE_ADD2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                Mbe_pp_no = row["MBE_PP_NO"] == DBNull.Value ? string.Empty : row["MBE_PP_NO"].ToString(),
                MBE_TAX_NO = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
    public class MST_CUS_SELECT
    {
        public string Mbe_cd { get; set; }
        public string Mbe_name { get; set; }
    }
}
