using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class EliteCommissionItem
    {
        #region Private Members
        private string _saec_brand;
        private string _saec_cat1;
        private string _saec_cat2;
        private string _saec_circular;
        private string _saec_itm;
        private string _saec_pro;
        private Int32 _saec_seq;
        private string _saec_serial;
        private bool _saec_direct;

        
        #endregion

        public string Saec_brand
        {
            get { return _saec_brand; }
            set { _saec_brand = value; }
        }
        public string Saec_cat1
        {
            get { return _saec_cat1; }
            set { _saec_cat1 = value; }
        }
        public string Saec_cat2
        {
            get { return _saec_cat2; }
            set { _saec_cat2 = value; }
        }
        public string Saec_circular
        {
            get { return _saec_circular; }
            set { _saec_circular = value; }
        }
        public string Saec_itm
        {
            get { return _saec_itm; }
            set { _saec_itm = value; }
        }
        public string Saec_pro
        {
            get { return _saec_pro; }
            set { _saec_pro = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }
        public string Saec_serial
        {
            get { return _saec_serial; }
            set { _saec_serial = value; }
        }
        public bool Saec_direct
        {
            get { return _saec_direct; }
            set { _saec_direct = value; }
        }


        public static EliteCommissionItem Converter(DataRow row)
        {
            return new EliteCommissionItem
            {
                Saec_brand = row["SAEC_BRAND"] == DBNull.Value ? string.Empty : row["SAEC_BRAND"].ToString(),
                Saec_cat1 = row["SAEC_CAT1"] == DBNull.Value ? string.Empty : row["SAEC_CAT1"].ToString(),
                Saec_cat2 = row["SAEC_CAT2"] == DBNull.Value ? string.Empty : row["SAEC_CAT2"].ToString(),
                Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULAR"].ToString(),
                Saec_itm = row["SAEC_ITM"] == DBNull.Value ? string.Empty : row["SAEC_ITM"].ToString(),
                Saec_pro = row["SAEC_PRO"] == DBNull.Value ? string.Empty : row["SAEC_PRO"].ToString(),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"]),
                Saec_serial = row["SAEC_SERIAL"] == DBNull.Value ? string.Empty : row["SAEC_SERIAL"].ToString(),
                Saec_direct = row["SAEC_DIRECT"] == DBNull.Value ? false :Convert.ToBoolean(row["SAEC_DIRECT"])
            };
        }

    }
}
