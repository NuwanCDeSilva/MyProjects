using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RemSection
    {
        #region Private Members
        private string _rss_cd;
        private string _rss_desc;
        private Int32 _rss_ord;
        private Int32 _rss_count;
        //private string _rss_user_id;
        //private string _rss_saul_tp;
        //private string _ree_saul_tp_cd;
        //private string _rss_saul_prem_cd;
        //private Int32 _rss_saul_max_app_limit;
        //private Int32 _rss_saul_val_limit;
        private string _rss_sart_main_tp;
        private string _rss_sart_desc;

        #endregion

        #region Public Property Definition
        public string Rss_cd
        {
            get { return _rss_cd; }
            set { _rss_cd = value; }
        }
        public string Rss_desc
        {
            get { return _rss_desc; }
            set { _rss_desc = value; }
        }
        public Int32 Rss_ord
        {
            get { return _rss_ord; }
            set { _rss_ord = value; }
        }
        public Int32 Rss_count
        {
            get { return _rss_count; }
            set { _rss_count = value; }
        }
        public string rss_sart_main_tp
        {
            get { return _rss_sart_main_tp; }
            set { _rss_sart_main_tp = value; }
        }
        public string rss_sart_desc
        {
            get { return _rss_sart_desc; }
            set { _rss_sart_desc = value; }
        }
        #endregion

        #region Converters
        public static RemSection Converter(DataRow row)
        {
            return new RemSection
            {
                Rss_cd = row["RSS_CD"] == DBNull.Value ? string.Empty : row["RSS_CD"].ToString(),
                Rss_desc = row["RSS_DESC"] == DBNull.Value ? string.Empty : row["RSS_DESC"].ToString(),
                Rss_ord = row["RSS_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RSS_ORD"])

            };
        }
        public static RemSection ConverterAuthorize(DataRow row)
        {
            return new RemSection
            {
                rss_sart_main_tp = row["Req_MainType"] == DBNull.Value ? string.Empty : row["Req_MainType"].ToString(),
                rss_sart_desc = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString(),
                Rss_cd = row["Sart_CD"] == DBNull.Value ? string.Empty : row["Sart_CD"].ToString(),
                Rss_count = row["Trn_Count"] == DBNull.Value ? 0 : Convert.ToInt32(row["Trn_Count"])

            };
        }
        public static RemSection Converter_Sart_TP(DataRow row)
        {
            return new RemSection
            {

                rss_sart_main_tp = row["cur_sart"] == DBNull.Value ? string.Empty : row["cur_sart"].ToString(),
                Rss_count = row["Trn_Count"] == DBNull.Value ? 0 : Convert.ToInt32(row["Trn_Count"])

            };
        }
        #endregion
    }
}

