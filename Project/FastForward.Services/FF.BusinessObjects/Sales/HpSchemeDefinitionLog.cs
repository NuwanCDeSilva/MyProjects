using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpSchemeDefinitionLog
    {
        #region Private Members
        private string _hscl_anal1;
        private string _hscl_anal2;
        private string _hscl_anal3;
        private decimal _hscl_anal4;
        private decimal _hscl_anal5;
        private string _hscl_cir;
        private decimal _hscl_dis;
        private decimal _hscl_dp_comm;
        private DateTime _hscl_dt;
        private decimal _hscl_inst_comm;
        private Boolean _hscl_restrict;
        private string _hscl_rmk;
        private string _hscl_usr;
        private DateTime _hscl_valid_from;
        private DateTime _hscl_valid_to;
        private string _hscl_session;
        #endregion

        public string Hscl_session
        {
            get { return _hscl_session; }
            set { _hscl_session = value; }
        }

        public string Hscl_anal1
        {
            get { return _hscl_anal1; }
            set { _hscl_anal1 = value; }
        }
        public string Hscl_anal2
        {
            get { return _hscl_anal2; }
            set { _hscl_anal2 = value; }
        }
        public string Hscl_anal3
        {
            get { return _hscl_anal3; }
            set { _hscl_anal3 = value; }
        }
        public decimal Hscl_anal4
        {
            get { return _hscl_anal4; }
            set { _hscl_anal4 = value; }
        }
        public decimal Hscl_anal5
        {
            get { return _hscl_anal5; }
            set { _hscl_anal5 = value; }
        }
        public string Hscl_cir
        {
            get { return _hscl_cir; }
            set { _hscl_cir = value; }
        }
        public decimal Hscl_dis
        {
            get { return _hscl_dis; }
            set { _hscl_dis = value; }
        }
        public decimal Hscl_dp_comm
        {
            get { return _hscl_dp_comm; }
            set { _hscl_dp_comm = value; }
        }
        public DateTime Hscl_dt
        {
            get { return _hscl_dt; }
            set { _hscl_dt = value; }
        }
        public decimal Hscl_inst_comm
        {
            get { return _hscl_inst_comm; }
            set { _hscl_inst_comm = value; }
        }
        public Boolean Hscl_restrict
        {
            get { return _hscl_restrict; }
            set { _hscl_restrict = value; }
        }
        public string Hscl_rmk
        {
            get { return _hscl_rmk; }
            set { _hscl_rmk = value; }
        }
        public string Hscl_usr
        {
            get { return _hscl_usr; }
            set { _hscl_usr = value; }
        }
        public DateTime Hscl_valid_from
        {
            get { return _hscl_valid_from; }
            set { _hscl_valid_from = value; }
        }
        public DateTime Hscl_valid_to
        {
            get { return _hscl_valid_to; }
            set { _hscl_valid_to = value; }
        }

        public static HpSchemeDefinitionLog Converter(DataRow row)
        {
            return new HpSchemeDefinitionLog
            {
                Hscl_anal1 = row["HSCL_ANAL1"] == DBNull.Value ? string.Empty : row["HSCL_ANAL1"].ToString(),
                Hscl_anal2 = row["HSCL_ANAL2"] == DBNull.Value ? string.Empty : row["HSCL_ANAL2"].ToString(),
                Hscl_anal3 = row["HSCL_ANAL3"] == DBNull.Value ? string.Empty : row["HSCL_ANAL3"].ToString(),
                Hscl_anal4 = row["HSCL_ANAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSCL_ANAL4"]),
                Hscl_anal5 = row["HSCL_ANAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSCL_ANAL5"]),
                Hscl_cir = row["HSCL_CIR"] == DBNull.Value ? string.Empty : row["HSCL_CIR"].ToString(),
                Hscl_dis = row["HSCL_DIS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSCL_DIS"]),
                Hscl_dp_comm = row["HSCL_DP_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSCL_DP_COMM"]),
                Hscl_dt = row["HSCL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSCL_DT"]),
                Hscl_inst_comm = row["HSCL_INST_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSCL_INST_COMM"]),
                Hscl_restrict = row["HSCL_RESTRICT"] == DBNull.Value ? false : Convert.ToBoolean(row["HSCL_RESTRICT"]),
                Hscl_rmk = row["HSCL_RMK"] == DBNull.Value ? string.Empty : row["HSCL_RMK"].ToString(),
                Hscl_usr = row["HSCL_USR"] == DBNull.Value ? string.Empty : row["HSCL_USR"].ToString(),
                Hscl_valid_from = row["HSCL_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSCL_VALID_FROM"]),
                Hscl_valid_to = row["HSCL_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSCL_VALID_TO"]),
                Hscl_session = row["HSCL_SESSION"] == DBNull.Value ? string.Empty : row["HSCL_SESSION"].ToString()
            };
        }



    }
}
