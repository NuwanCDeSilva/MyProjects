using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class HPAddSchemePara
    {
        #region Private Members
        private string _hap_anal1;
        private string _hap_anal2;
        private string _hap_anal3;
        private DateTime _hap_anal4;
        private DateTime _hap_anal5;
        private string _hap_cd;
        private string _hap_com;
        private string _hap_cre_by;
        private DateTime _hap_cre_when;
        private DateTime _hap_frm;
        private string _hap_mod_by;
        private DateTime _hap_mod_when;
        private string _hap_sch;
        private DateTime _hap_to;
        private string _hap_tp;
        private Int32 _hap_val1;
        private Int32 _hap_val2;
        private Int32 _hap_val3;
        private Boolean _hap_val4;
        private Int32 _hap_val5;
        private Int32 _hap_anal6;    //kapila 7/9/2015
        private Int32 _hap_anal7;    //kapila 24/9/2015
        private decimal _hap_anal8;    //kapila 24/9/2015
        private decimal _hap_anal9;    //kapila 24/9/2015
        private Int32 _hap_val6;    //Sanjeewa 2016-06-02
        #endregion

        public decimal Hap_anal8
        {
            get { return _hap_anal8; }
            set { _hap_anal8 = value; }
        }
        public decimal Hap_anal9
        {
            get { return _hap_anal9; }
            set { _hap_anal9 = value; }
        }
        public Int32 Hap_anal7
        {
            get { return _hap_anal7; }
            set { _hap_anal7 = value; }
        }
        public Int32 Hap_anal6
        {
            get { return _hap_anal6; }
            set { _hap_anal6 = value; }
        }
        public Boolean Hap_val4
        {
            get { return _hap_val4; }
            set { _hap_val4 = value; }
        }

        public Int32 Hap_val5
        {
            get { return _hap_val5; }
            set { _hap_val5 = value; }
        }

        public string Hap_anal1
        {
            get { return _hap_anal1; }
            set { _hap_anal1 = value; }
        }
        public string Hap_anal2
        {
            get { return _hap_anal2; }
            set { _hap_anal2 = value; }
        }
        public string Hap_anal3
        {
            get { return _hap_anal3; }
            set { _hap_anal3 = value; }
        }
        public DateTime Hap_anal4
        {
            get { return _hap_anal4; }
            set { _hap_anal4 = value; }
        }
        public DateTime Hap_anal5
        {
            get { return _hap_anal5; }
            set { _hap_anal5 = value; }
        }
        public string Hap_cd
        {
            get { return _hap_cd; }
            set { _hap_cd = value; }
        }
        public string Hap_com
        {
            get { return _hap_com; }
            set { _hap_com = value; }
        }
        public string Hap_cre_by
        {
            get { return _hap_cre_by; }
            set { _hap_cre_by = value; }
        }
        public DateTime Hap_cre_when
        {
            get { return _hap_cre_when; }
            set { _hap_cre_when = value; }
        }
        public DateTime Hap_frm
        {
            get { return _hap_frm; }
            set { _hap_frm = value; }
        }
        public string Hap_mod_by
        {
            get { return _hap_mod_by; }
            set { _hap_mod_by = value; }
        }
        public DateTime Hap_mod_when
        {
            get { return _hap_mod_when; }
            set { _hap_mod_when = value; }
        }
        public string Hap_sch
        {
            get { return _hap_sch; }
            set { _hap_sch = value; }
        }
        public DateTime Hap_to
        {
            get { return _hap_to; }
            set { _hap_to = value; }
        }
        public string Hap_tp
        {
            get { return _hap_tp; }
            set { _hap_tp = value; }
        }
        public Int32 Hap_val1
        {
            get { return _hap_val1; }
            set { _hap_val1 = value; }
        }
        public Int32 Hap_val2
        {
            get { return _hap_val2; }
            set { _hap_val2 = value; }
        }
        public Int32 Hap_val3
        {
            get { return _hap_val3; }
            set { _hap_val3 = value; }
        }
        public Int32 Hap_val6
        {
            get { return _hap_val6; }
            set { _hap_val6 = value; }
        }

        public static HPAddSchemePara Converter(DataRow row)
        {
            return new HPAddSchemePara
            {
                Hap_anal1 = row["HAP_ANAL1"] == DBNull.Value ? string.Empty : row["HAP_ANAL1"].ToString(),
                Hap_anal2 = row["HAP_ANAL2"] == DBNull.Value ? string.Empty : row["HAP_ANAL2"].ToString(),
                Hap_anal3 = row["HAP_ANAL3"] == DBNull.Value ? string.Empty : row["HAP_ANAL3"].ToString(),
                Hap_anal4 = row["HAP_ANAL4"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAP_ANAL4"]),
                Hap_anal5 = row["HAP_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAP_ANAL5"]),
                Hap_cd = row["HAP_CD"] == DBNull.Value ? string.Empty : row["HAP_CD"].ToString(),
                Hap_com = row["HAP_COM"] == DBNull.Value ? string.Empty : row["HAP_COM"].ToString(),
                Hap_cre_by = row["HAP_CRE_BY"] == DBNull.Value ? string.Empty : row["HAP_CRE_BY"].ToString(),
                Hap_cre_when = row["HAP_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAP_CRE_WHEN"]),
                Hap_frm = row["HAP_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAP_FRM"]),
                Hap_mod_by = row["HAP_MOD_BY"] == DBNull.Value ? string.Empty : row["HAP_MOD_BY"].ToString(),
                Hap_mod_when = row["HAP_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAP_MOD_WHEN"]),
                Hap_sch = row["HAP_SCH"] == DBNull.Value ? string.Empty : row["HAP_SCH"].ToString(),
                Hap_to = row["HAP_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HAP_TO"]),
                Hap_tp = row["HAP_TP"] == DBNull.Value ? string.Empty : row["HAP_TP"].ToString(),
                Hap_val1 = row["HAP_VAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAP_VAL1"]),
                Hap_val2 = row["HAP_VAL2"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAP_VAL2"]),
                Hap_val3 = row["HAP_VAL3"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAP_VAL3"]),
                Hap_val4 = row["HAP_VAL4"] == DBNull.Value ? false : Convert.ToBoolean(row["HAP_VAL4"]),
                Hap_val5 = row["HAP_VAL5"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAP_VAL5"]),
                Hap_anal6 = row["Hap_anal6"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hap_anal6"]),
                Hap_anal7 = row["Hap_anal7"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hap_anal7"]),
                Hap_anal8 = row["Hap_anal8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Hap_anal8"]),
                Hap_anal9 = row["Hap_anal9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Hap_anal9"]),
                Hap_val6 = row["HAP_VAL6"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAP_VAL6"])

            };
        }

    }
}
