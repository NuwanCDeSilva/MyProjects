using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RemAdjTypes
    {
        #region Private Members
        private string _adt_cd;
        private string _adt_desc;
        private Int32 _adt_need_dt;
        private Int32 _adt_need_pay;
        private Int32 _adt_need_ref;
        private Int32 _adt_upd_rem;
        #endregion

        #region Public Property Definition
        public string Adt_cd
        {
            get { return _adt_cd; }
            set { _adt_cd = value; }
        }
        public string Adt_desc
        {
            get { return _adt_desc; }
            set { _adt_desc = value; }
        }
        public Int32 Adt_need_dt
        {
            get { return _adt_need_dt; }
            set { _adt_need_dt = value; }
        }
        public Int32 Adt_need_pay
        {
            get { return _adt_need_pay; }
            set { _adt_need_pay = value; }
        }
        public Int32 Adt_need_ref
        {
            get { return _adt_need_ref; }
            set { _adt_need_ref = value; }
        }
        public Int32 Adt_upd_rem
        {
            get { return _adt_upd_rem; }
            set { _adt_upd_rem = value; }
        }
        #endregion

        #region Converters
        public static RemAdjTypes Converter(DataRow row)
        {
            return new RemAdjTypes
            {
                Adt_cd = row["ADT_CD"] == DBNull.Value ? string.Empty : row["ADT_CD"].ToString(),
                Adt_desc = row["ADT_DESC"] == DBNull.Value ? string.Empty : row["ADT_DESC"].ToString(),
                Adt_need_dt = row["ADT_NEED_DT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_NEED_DT"]),
                Adt_need_pay = row["ADT_NEED_PAY"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_NEED_PAY"]),
                Adt_need_ref = row["ADT_NEED_REF"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_NEED_REF"]),
                Adt_upd_rem = row["ADT_UPD_REM"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADT_UPD_REM"])

            };
        }
        #endregion
    }
}

