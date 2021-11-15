using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterGiftVoucherCategoryDetail
    {
        //Created by Prabhath on 20/11/2013
        //Mapped Table : MST_GV_CATDET

        #region Private Members
        private string _gvctd_itm;
        private Int32 _gvctd_seq;
        private Boolean _gvctd_stus;
        private Boolean _gvctd_tp;
        private string _mi_longdesc;
        #endregion

        public string Gvctd_itm { get { return _gvctd_itm; } set { _gvctd_itm = value; } }
        public Int32 Gvctd_seq { get { return _gvctd_seq; } set { _gvctd_seq = value; } }
        public Boolean Gvctd_stus { get { return _gvctd_stus; } set { _gvctd_stus = value; } }
        public Boolean Gvctd_tp { get { return _gvctd_tp; } set { _gvctd_tp = value; } }
        public string Mi_longdesc { get { return _mi_longdesc; } set { _mi_longdesc = value; } }

        public static MasterGiftVoucherCategoryDetail Converter(DataRow row)
        {
            return new MasterGiftVoucherCategoryDetail
            {
                Gvctd_itm = row["GVCTD_ITM"] == DBNull.Value ? string.Empty : row["GVCTD_ITM"].ToString(),
                Gvctd_seq = row["GVCTD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVCTD_SEQ"]),
                Gvctd_stus = row["GVCTD_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["GVCTD_STUS"]),
                Gvctd_tp = row["GVCTD_TP"] == DBNull.Value ? false : Convert.ToBoolean(row["GVCTD_TP"])

            };
        }
    }
}

