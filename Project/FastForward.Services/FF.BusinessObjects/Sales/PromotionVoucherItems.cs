using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//Tharaka 2014-07-14

namespace FF.BusinessObjects
{
    [Serializable]

    public class PromotionVoucherItems
    {
        #region Private Members

        private Int32 spi_seq;
        private Int32 spi_itm_seq;
        private string spi_itm;
        private string spi_itm_stus;
        private Int32 spi_act;
        private string spi_cre_by;
        private DateTime spi_cre_dt;
        private string spi_mod_by;
        private DateTime spi_mod_dt;
        private string spi_brand;
        private string spi_cat1;
        private string spi_cat2;

        #endregion

        public Int32 Spi_seq
        {
            get { return spi_seq; }
            set { spi_seq = value; }
        }

        public Int32 Spi_itm_seq
        {
            get { return spi_itm_seq; }
            set { spi_itm_seq = value; }
        }

        public string Spi_itm
        {
            get { return spi_itm; }
            set { spi_itm = value; }
        }

        public string Spi_itm_stus
        {
            get { return spi_itm_stus; }
            set { spi_itm_stus = value; }
        }

        public Int32 Spi_act
        {
            get { return spi_act; }
            set { spi_act = value; }
        }

        public string Spi_cre_by
        {
            get { return spi_cre_by; }
            set { spi_cre_by = value; }
        }

        public DateTime Spi_cre_dt
        {
            get { return spi_cre_dt; }
            set { spi_cre_dt = value; }
        }

        public string Spi_mod_by
        {
            get { return spi_mod_by; }
            set { spi_mod_by = value; }
        }

        public DateTime Spi_mod_dt
        {
            get { return spi_mod_dt; }
            set { spi_mod_dt = value; }
        }

        public string Spi_brand
        {
            get { return spi_brand; }
            set { spi_brand = value; }
        }
        public string Spi_cat1
        {
            get { return spi_cat1; }
            set { spi_cat1 = value; }
        }

        public string Spi_cat2
        {
            get { return spi_cat2; }
            set { spi_cat2 = value; }
        }

        public static PromotionVoucherItems Converter(DataRow row)
        {
            return new PromotionVoucherItems
            {
                Spi_seq = row["Spi_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["Spi_seq"].ToString()),
                Spi_itm_seq = row["Spi_itm_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["Spi_itm_seq"].ToString()),
                Spi_itm = row["Spi_itm"] == DBNull.Value ? string.Empty : row["Spi_itm"].ToString(),
                Spi_itm_stus = row["Spi_itm_stus"] == DBNull.Value ? string.Empty : row["Spi_itm_stus"].ToString(),
                Spi_act = row["Spi_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["Spi_act"].ToString()),
                Spi_cre_by = row["Spi_cre_by"] == DBNull.Value ? string.Empty : row["Spi_cre_by"].ToString(),
                //Spi_cre_dt = row["Spi_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Spi_cre_dt"]),
                Spi_mod_by = row["Spi_mod_by"] == DBNull.Value ? string.Empty : row["Spi_mod_by"].ToString(),
                Spi_brand = row["spi_brand"] == DBNull.Value ? string.Empty : row["spi_brand"].ToString(),
                Spi_cat1 = row["spi_cat1"] == DBNull.Value ? string.Empty : row["spi_cat1"].ToString(),
                Spi_cat2 = row["Spi_cat2"] == DBNull.Value ? string.Empty : row["Spi_cat2"].ToString()
                //Spi_mod_dt = row["Spi_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Spi_mod_dt"])
            };
        }
    }
}
