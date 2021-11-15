using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterGiftVoucherCategory
    {
        //Created by Prabhath on 20/11/2013
        //Mapped Table : MST_GV_CAT

        #region Private Members
        private string _gvct_cat;
        private string _gvct_com;
        private string _gvct_cr_by;
        private DateTime _gvct_cr_dt;
        private string _gvct_desc;
        private string _gvct_md_by;
        private DateTime _gvct_md_dt;
        private Int32 _gvct_seq;
        private string _gvct_session;
        private Boolean _gvct_stus;
        private Boolean _gvct_verb;
        #endregion

        public string Gvct_cat { get { return _gvct_cat; } set { _gvct_cat = value; } }
        public string Gvct_com { get { return _gvct_com; } set { _gvct_com = value; } }
        public string Gvct_cr_by { get { return _gvct_cr_by; } set { _gvct_cr_by = value; } }
        public DateTime Gvct_cr_dt { get { return _gvct_cr_dt; } set { _gvct_cr_dt = value; } }
        public string Gvct_desc { get { return _gvct_desc; } set { _gvct_desc = value; } }
        public string Gvct_md_by { get { return _gvct_md_by; } set { _gvct_md_by = value; } }
        public DateTime Gvct_md_dt { get { return _gvct_md_dt; } set { _gvct_md_dt = value; } }
        public Int32 Gvct_seq { get { return _gvct_seq; } set { _gvct_seq = value; } }
        public string Gvct_session { get { return _gvct_session; } set { _gvct_session = value; } }
        public Boolean Gvct_stus { get { return _gvct_stus; } set { _gvct_stus = value; } }
        public Boolean Gvct_verb { get { return _gvct_verb; } set { _gvct_verb = value; } }

        public static MasterGiftVoucherCategory Converter(DataRow row)
        {
            return new MasterGiftVoucherCategory
            {
                Gvct_cat = row["GVCT_CAT"] == DBNull.Value ? string.Empty : row["GVCT_CAT"].ToString(),
                Gvct_com = row["GVCT_COM"] == DBNull.Value ? string.Empty : row["GVCT_COM"].ToString(),
                Gvct_cr_by = row["GVCT_CR_BY"] == DBNull.Value ? string.Empty : row["GVCT_CR_BY"].ToString(),
                Gvct_cr_dt = row["GVCT_CR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVCT_CR_DT"]),
                Gvct_desc = row["GVCT_DESC"] == DBNull.Value ? string.Empty : row["GVCT_DESC"].ToString(),
                Gvct_md_by = row["GVCT_MD_BY"] == DBNull.Value ? string.Empty : row["GVCT_MD_BY"].ToString(),
                Gvct_md_dt = row["GVCT_MD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVCT_MD_DT"]),
                Gvct_seq = row["GVCT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVCT_SEQ"]),
                Gvct_session = row["GVCT_SESSION"] == DBNull.Value ? string.Empty : row["GVCT_SESSION"].ToString(),
                Gvct_stus = row["GVCT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["GVCT_STUS"]),
                Gvct_verb = row["GVCT_VERB"] == DBNull.Value ? false : Convert.ToBoolean(row["GVCT_VERB"])

            };
        }
    }
}

