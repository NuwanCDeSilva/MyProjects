using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

 
namespace FF.BusinessObjects
{
    public class HPPrmotorCommDef
    {
        #region Private Members
        private string _hpcm_circ;
        private string _hpcm_com;
        private decimal _hpcm_comm_amt;
        private decimal _hpcm_comm_rt;
        private string _hpcm_cre_by;
        private DateTime _hpcm_cre_dt;
        private DateTime _hpcm_from_dt;
        private string _hpcm_itm_cd;
        private string _hpcm_mod_by;
        private DateTime _hpcm_mod_dt;
        private string _hpcm_sch_cd;
        private Int32 _hpcm_seq;
        private DateTime _hpcm_to_dt;

        private string mi_shortdesc;
        #endregion

        public string Hpcm_circ
        {
            get { return _hpcm_circ; }
            set { _hpcm_circ = value; }
        }
        public string Hpcm_com
        {
            get { return _hpcm_com; }
            set { _hpcm_com = value; }
        }
        public decimal Hpcm_comm_amt
        {
            get { return _hpcm_comm_amt; }
            set { _hpcm_comm_amt = value; }
        }
        public decimal Hpcm_comm_rt
        {
            get { return _hpcm_comm_rt; }
            set { _hpcm_comm_rt = value; }
        }
        public string Hpcm_cre_by
        {
            get { return _hpcm_cre_by; }
            set { _hpcm_cre_by = value; }
        }
        public DateTime Hpcm_cre_dt
        {
            get { return _hpcm_cre_dt; }
            set { _hpcm_cre_dt = value; }
        }
        public DateTime Hpcm_from_dt
        {
            get { return _hpcm_from_dt; }
            set { _hpcm_from_dt = value; }
        }
        public string Hpcm_itm_cd
        {
            get { return _hpcm_itm_cd; }
            set { _hpcm_itm_cd = value; }
        }
        public string Hpcm_mod_by
        {
            get { return _hpcm_mod_by; }
            set { _hpcm_mod_by = value; }
        }
        public DateTime Hpcm_mod_dt
        {
            get { return _hpcm_mod_dt; }
            set { _hpcm_mod_dt = value; }
        }
        public string Hpcm_sch_cd
        {
            get { return _hpcm_sch_cd; }
            set { _hpcm_sch_cd = value; }
        }
        public Int32 Hpcm_seq
        {
            get { return _hpcm_seq; }
            set { _hpcm_seq = value; }
        }
        public DateTime Hpcm_to_dt
        {
            get { return _hpcm_to_dt; }
            set { _hpcm_to_dt = value; }
        }

        public static HPPrmotorCommDef Converter(DataRow row)
        {
            return new HPPrmotorCommDef
            {
                Hpcm_circ = row["HPCM_CIRC"] == DBNull.Value ? string.Empty : row["HPCM_CIRC"].ToString(),
                Hpcm_com = row["HPCM_COM"] == DBNull.Value ? string.Empty : row["HPCM_COM"].ToString(),
                Hpcm_comm_amt = row["HPCM_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCM_COMM_AMT"]),
                Hpcm_comm_rt = row["HPCM_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HPCM_COMM_RT"]),
                Hpcm_cre_by = row["HPCM_CRE_BY"] == DBNull.Value ? string.Empty : row["HPCM_CRE_BY"].ToString(),
                Hpcm_cre_dt = row["HPCM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCM_CRE_DT"]),
                Hpcm_from_dt = row["HPCM_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCM_FROM_DT"]),
                Hpcm_itm_cd = row["HPCM_ITM_CD"] == DBNull.Value ? string.Empty : row["HPCM_ITM_CD"].ToString(),
                Hpcm_mod_by = row["HPCM_MOD_BY"] == DBNull.Value ? string.Empty : row["HPCM_MOD_BY"].ToString(),
                Hpcm_mod_dt = row["HPCM_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCM_MOD_DT"]),
                Hpcm_sch_cd = row["HPCM_SCH_CD"] == DBNull.Value ? string.Empty : row["HPCM_SCH_CD"].ToString(),
                Hpcm_seq = row["HPCM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPCM_SEQ"]),
                Hpcm_to_dt = row["HPCM_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPCM_TO_DT"])

            };
        }
    }
}
