using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PromotComSch
    {
        #region Private Members
        private Int32 _hpcs_line;
        private string _hpcs_sch_cd;
        private Int32 _hpcs_seq;
        private decimal _hpcs_com_amt;
        private decimal _hpcs_com_rt;
        private Int32 _hpcs_from_qty;
        private Int32 _hpcs_to_qty;
        #endregion

        public Int32 Hpcs_to_qty
        {
            get { return _hpcs_to_qty; }
            set { _hpcs_to_qty = value; }
        }
        public decimal Hpcs_com_amt
        {
            get { return _hpcs_com_amt; }
            set { _hpcs_com_amt = value; }
        }
        public decimal Hpcs_com_rt
        {
            get { return _hpcs_com_rt; }
            set { _hpcs_com_rt = value; }
        }
        public Int32 Hpcs_from_qty
        {
            get { return _hpcs_from_qty; }
            set { _hpcs_from_qty = value; }
        }
        public Int32 Hpcs_line
        {
            get { return _hpcs_line; }
            set { _hpcs_line = value; }
        }
        public string Hpcs_sch_cd
        {
            get { return _hpcs_sch_cd; }
            set { _hpcs_sch_cd = value; }
        }
        public Int32 Hpcs_seq
        {
            get { return _hpcs_seq; }
            set { _hpcs_seq = value; }
        }
        public static PromotComSch Converter(DataRow row)
        {
            return new PromotComSch
            {

                Hpcs_line = row["HPCS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCS_LINE"]),
                Hpcs_sch_cd = row["HPCS_SCH_CD"] == DBNull.Value ? string.Empty : row["HPCS_SCH_CD"].ToString(),
                Hpcs_seq = row["HPCS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCS_SEQ"]),
                Hpcs_com_amt = row["HPCS_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCS_COMM_AMT"]),
                Hpcs_com_rt = row["HPCS_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCS_COMM_RT"]),
                Hpcs_from_qty = row["HPCS_FRM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCS_FRM_QTY"]),
                Hpcs_to_qty = row["HPCS_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCS_TO_QTY"])
            };
        }

    }
}
