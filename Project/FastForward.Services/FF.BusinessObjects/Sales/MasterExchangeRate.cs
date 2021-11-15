using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
  public class MasterExchangeRate
    {
        #region Private Members
        private Boolean _mer_act;
        private decimal _mer_bnkbuy_rt;
        private decimal _mer_bnksel_rt;
        private DateTime _mer_buyvad_from;
        private DateTime _mer_buyvad_to;
        private string _mer_com;
        private string _mer_pc; //kapila 10/12/2013
        private string _mer_cre_by;
        private DateTime _mer_cre_dt;
        private string _mer_cur;
        private decimal _mer_cussel_rt;
        private Int32 _mer_id;
        private string _mer_mod_by;
        private DateTime _mer_mod_dt;
        private string _mer_session_id;
        private DateTime _mer_vad_from;
        private DateTime _mer_vad_to;
        private string _mer_to_cur;
        private string _mer_to_cur_desc;
        private string _mer_from_cur_desc;
        private string _mer_com_desc;
         
        #endregion

        public Boolean Mer_act
        {
            get { return _mer_act; }
            set { _mer_act = value; }
        }
        public decimal Mer_bnkbuy_rt
        {
            get { return _mer_bnkbuy_rt; }
            set { _mer_bnkbuy_rt = value; }
        }
        public decimal Mer_bnksel_rt
        {
            get { return _mer_bnksel_rt; }
            set { _mer_bnksel_rt = value; }
        }
        public DateTime Mer_buyvad_from
        {
            get { return _mer_buyvad_from; }
            set { _mer_buyvad_from = value; }
        }
        public DateTime Mer_buyvad_to
        {
            get { return _mer_buyvad_to; }
            set { _mer_buyvad_to = value; }
        }
        public string Mer_com
        {
            get { return _mer_com; }
            set { _mer_com = value; }
        }
        public string Mer_pc
        {
            get { return _mer_pc; }
            set { _mer_pc = value; }
        }
        public string Mer_cre_by
        {
            get { return _mer_cre_by; }
            set { _mer_cre_by = value; }
        }
        public DateTime Mer_cre_dt
        {
            get { return _mer_cre_dt; }
            set { _mer_cre_dt = value; }
        }
        public string Mer_cur
        {
            get { return _mer_cur; }
            set { _mer_cur = value; }
        }
        public decimal Mer_cussel_rt
        {
            get { return _mer_cussel_rt; }
            set { _mer_cussel_rt = value; }
        }
        public Int32 Mer_id
        {
            get { return _mer_id; }
            set { _mer_id = value; }
        }
        public string Mer_mod_by
        {
            get { return _mer_mod_by; }
            set { _mer_mod_by = value; }
        }
        public DateTime Mer_mod_dt
        {
            get { return _mer_mod_dt; }
            set { _mer_mod_dt = value; }
        }
        public string Mer_session_id
        {
            get { return _mer_session_id; }
            set { _mer_session_id = value; }
        }
        public DateTime Mer_vad_from
        {
            get { return _mer_vad_from; }
            set { _mer_vad_from = value; }
        }
        public DateTime Mer_vad_to
        {
            get { return _mer_vad_to; }
            set { _mer_vad_to = value; }
        }
        public string Mer_to_cur
        {
            get { return _mer_to_cur; }
            set { _mer_to_cur = value; }
        }

         /* objects add lakshan 2016/02/05 */

        public string Mer_to_cur_desc
        {
            get { return _mer_to_cur_desc; }
            set { _mer_to_cur_desc = value; }
        }

        public string Mer_from_cur_desc
        {
            get { return _mer_from_cur_desc; }
            set { _mer_from_cur_desc = value; }
        }
        public string Mer_com_desc
        {
            get { return _mer_com_desc; }
            set { _mer_com_desc = value; }
        }
        public int isDateRange { get; set; }
        public static MasterExchangeRate Converter(DataRow row)
        {
            return new MasterExchangeRate
            {
                Mer_act = row["MER_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MER_ACT"]),
                Mer_bnkbuy_rt = row["MER_BNKBUY_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MER_BNKBUY_RT"]),
                Mer_bnksel_rt = row["MER_BNKSEL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MER_BNKSEL_RT"]),
                Mer_buyvad_from = row["MER_BUYVAD_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MER_BUYVAD_FROM"]),
                Mer_buyvad_to = row["MER_BUYVAD_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MER_BUYVAD_TO"]),
                Mer_com = row["MER_COM"] == DBNull.Value ? string.Empty : row["MER_COM"].ToString(),
                Mer_cre_by = row["MER_CRE_BY"] == DBNull.Value ? string.Empty : row["MER_CRE_BY"].ToString(),
                Mer_cre_dt = row["MER_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MER_CRE_DT"]),
                Mer_cur = row["MER_CUR"] == DBNull.Value ? string.Empty : row["MER_CUR"].ToString(),
                Mer_cussel_rt = row["MER_CUSSEL_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MER_CUSSEL_RT"]),
                Mer_id = row["MER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["MER_ID"]),
                Mer_mod_by = row["MER_MOD_BY"] == DBNull.Value ? string.Empty : row["MER_MOD_BY"].ToString(),
                Mer_mod_dt = row["MER_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MER_MOD_DT"]),
                Mer_session_id = row["MER_SESSION_ID"] == DBNull.Value ? string.Empty : row["MER_SESSION_ID"].ToString(),
                Mer_vad_from = row["MER_VAD_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MER_VAD_FROM"]),
                Mer_vad_to = row["MER_VAD_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MER_VAD_TO"]),
                Mer_to_cur = row["MER_TO_CUR"] == DBNull.Value ? string.Empty : row["MER_TO_CUR"].ToString(),
                Mer_pc = row["MER_PC"] == DBNull.Value ? string.Empty : row["MER_PC"].ToString()
            };
        }


    }
}
