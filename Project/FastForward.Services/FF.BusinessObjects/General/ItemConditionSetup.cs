using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class ItemConditionSetup
    {
        public Int32 irsc_ser_id { get; set; }

        public int irsc_line { get; set; }

        public String irsc_com { get; set; }

        public String irsc_loc { get; set; }

        public String irsc_cat { get; set; }

        public String irsc_catText { get; set; }

        public String irsc_tp { get; set; }

        public String irsc_rmk { get; set; }

        public String irsc_cre_by { get; set; }

        public DateTime irsc_cre_dt { get; set; }

        public String irsc_stus { get; set; }

        public String rcc_desc { get; set; }

        public String ItemSearial { get; set; }

        public String StatusText { get; set; }

        public String ItemCode { get; set; }

        public String irsc_add_loc { get; set; }

        public String irsc_cnl_by { get; set; }

        public DateTime p_irsc_cnl_dt { get; set; }
        public decimal irsc_cha { get; set; }
        public String tmpRemark { get; set; }
        public String tmpCondescription{ get; set; }
        //Added by Dulaj 2018?mar/15
        public String irsc_doc { get; set; }
        public String irsc_othloc { get; set; }

        public static ItemConditionSetup Converter(DataRow row)
        {
            return new ItemConditionSetup
            {
                irsc_ser_id = row["irsc_ser_id"] == DBNull.Value ? 0 : Convert.ToInt32(row["irsc_ser_id"].ToString()),
                irsc_line = row["irsc_line"] == DBNull.Value ? 0 : Convert.ToInt16(row["irsc_line"].ToString()),
                irsc_com = row["irsc_com"] == DBNull.Value ? string.Empty : row["irsc_com"].ToString(),
                irsc_loc = row["irsc_loc"] == DBNull.Value ? string.Empty : row["irsc_loc"].ToString(),
                irsc_cat = row["irsc_cat"] == DBNull.Value ? string.Empty : row["irsc_cat"].ToString(),
                irsc_tp = row["irsc_tp"] == DBNull.Value ? string.Empty : row["irsc_tp"].ToString(),
                irsc_rmk = row["irsc_rmk"] == DBNull.Value ? string.Empty : row["irsc_rmk"].ToString(),
                irsc_cre_by = row["irsc_cre_by"] == DBNull.Value ? string.Empty : row["irsc_cre_by"].ToString(),
                irsc_cre_dt = row["irsc_cre_dt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["irsc_cre_dt"].ToString()),
                irsc_stus = row["irsc_stus"] == DBNull.Value ? string.Empty : row["irsc_stus"].ToString(),
                rcc_desc = row["rcc_desc"] == DBNull.Value ? string.Empty : row["rcc_desc"].ToString(),
                irsc_catText = row["rcc_desc"] == DBNull.Value ? string.Empty : row["rcc_desc"].ToString(),
                ItemSearial = row["ins_ser_1"] == DBNull.Value ? string.Empty : row["ins_ser_1"].ToString(),
                ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : row["ItemCode"].ToString(),
                //Added Sahan 20 Nov 2015
                irsc_add_loc = row["irsc_add_loc"] == DBNull.Value ? string.Empty : row["irsc_add_loc"].ToString(),
                irsc_cnl_by = row["irsc_cnl_by"] == DBNull.Value ? string.Empty : row["irsc_cnl_by"].ToString(),
                p_irsc_cnl_dt = row["p_irsc_cnl_dt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["p_irsc_cnl_dt"].ToString()),
                irsc_doc = row["irsc_doc"] == DBNull.Value ? string.Empty : row["irsc_doc"].ToString(),
                irsc_othloc = row["irsc_othloc"] == DBNull.Value ? string.Empty : row["irsc_othloc"].ToString()

            };
        }
    }
}