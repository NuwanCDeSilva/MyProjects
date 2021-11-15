using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class RequestApprovalDetailLog
    {
        #region Private Members
        private string _grad_anal1;
        private string _grad_anal2;
        private string _grad_anal3;
        private string _grad_anal4;
        private string _grad_anal5;
        private DateTime _grad_date_param;
        private Boolean _grad_is_rt1;
        private Boolean _grad_is_rt2;
        private int _grad_line;
        private string _grad_ref;
        private string _grad_req_param;
        private decimal _grad_val1;
        private decimal _grad_val2;
        private decimal _grad_val3;
        private decimal _grad_val4;
        private decimal _grad_val5;
        private int _grad_lvl;
        
        private string _grad_anal6;
        private string _grad_anal7;
        private string _grad_anal8;
        private string _grad_anal9;
        private string _grad_anal10;
        private string _grad_anal11;
        private string _grad_anal12;
        private string _grad_anal13;
        private string _grad_anal14;
        private string _grad_anal15;
        private decimal _grad_anal16;
        private decimal _grad_anal17;
        private decimal _grad_anal18;
        #endregion

        public string Grad_anal1 { get { return _grad_anal1; } set { _grad_anal1 = value; } }
        public string Grad_anal2 { get { return _grad_anal2; } set { _grad_anal2 = value; } }
        public string Grad_anal3 { get { return _grad_anal3; } set { _grad_anal3 = value; } }
        public string Grad_anal4 { get { return _grad_anal4; } set { _grad_anal4 = value; } }
        public string Grad_anal5 { get { return _grad_anal5; } set { _grad_anal5 = value; } }
        public DateTime Grad_date_param { get { return _grad_date_param; } set { _grad_date_param = value; } }
        public Boolean Grad_is_rt1 { get { return _grad_is_rt1; } set { _grad_is_rt1 = value; } }
        public Boolean Grad_is_rt2 { get { return _grad_is_rt2; } set { _grad_is_rt2 = value; } }
        public int Grad_line { get { return _grad_line; } set { _grad_line = value; } }
        public string Grad_ref { get { return _grad_ref; } set { _grad_ref = value; } }
        public string Grad_req_param { get { return _grad_req_param; } set { _grad_req_param = value; } }
        public decimal Grad_val1 { get { return _grad_val1; } set { _grad_val1 = value; } }
        public decimal Grad_val2 { get { return _grad_val2; } set { _grad_val2 = value; } }
        public decimal Grad_val3 { get { return _grad_val3; } set { _grad_val3 = value; } }
        public decimal Grad_val4 { get { return _grad_val4; } set { _grad_val4 = value; } }
        public decimal Grad_val5 { get { return _grad_val5; } set { _grad_val5 = value; } }

        public int Grad_lvl { get { return _grad_lvl; } set { _grad_lvl = value; } }

        public string Grad_anal15 { get { return _grad_anal15; } set { _grad_anal15 = value; } }

        public string Grad_anal6 { get { return _grad_anal6; } set { _grad_anal6 = value; } }
        public string Grad_anal7 { get { return _grad_anal7; } set { _grad_anal7 = value; } }
        public string Grad_anal8 { get { return _grad_anal8; } set { _grad_anal8 = value; } }
        public string Grad_anal9 { get { return _grad_anal9; } set { _grad_anal9 = value; } }
        public string Grad_anal10 { get { return _grad_anal10; } set { _grad_anal10 = value; } }
        public string Grad_anal11 { get { return _grad_anal11; } set { _grad_anal11 = value; } }
        public string Grad_anal12 { get { return _grad_anal12; } set { _grad_anal12 = value; } }
        public string Grad_anal13 { get { return _grad_anal13; } set { _grad_anal13 = value; } }
        public string Grad_anal14 { get { return _grad_anal14; } set { _grad_anal14 = value; } }
      
        public decimal Grad_anal16 { get { return _grad_anal16; } set { _grad_anal16 = value; } } // Nadeeka 24-08-2015
        public decimal Grad_anal17 { get { return _grad_anal17; } set { _grad_anal17 = value; } } // Nadeeka 24-08-2015
        public decimal Grad_anal18 { get { return _grad_anal18; } set { _grad_anal18 = value; } } // Nadeeka 24-08-2015

        public static RequestApprovalDetailLog Converter(DataRow row)
        {
            return new RequestApprovalDetailLog
            {
                Grad_anal1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                Grad_anal2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                Grad_anal3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                Grad_anal4 = row["GRAD_ANAL4"] == DBNull.Value ? string.Empty : row["GRAD_ANAL4"].ToString(),
                Grad_anal5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                Grad_date_param = row["GRAD_DATE_PARAM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAD_DATE_PARAM"]),
                Grad_is_rt1 = row["GRAD_IS_RT1"] == DBNull.Value ? false : Convert.ToBoolean(row["GRAD_IS_RT1"]),
                Grad_is_rt2 = row["GRAD_IS_RT2"] == DBNull.Value ? false : Convert.ToBoolean(row["GRAD_IS_RT2"]),
                Grad_line = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAD_LINE"]),
                Grad_ref = row["GRAD_REF"] == DBNull.Value ? string.Empty : row["GRAD_REF"].ToString(),
                Grad_req_param = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                Grad_val1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL1"]),
                Grad_val2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"]),
                Grad_val3 = row["GRAD_VAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL3"]),
                Grad_val4 = row["GRAD_VAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL4"]),
                Grad_val5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"]),
                Grad_lvl = row["GRAD_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAD_LVL"]),
                Grad_anal15 = row["GRAD_ANAL15"] == DBNull.Value ? string.Empty : row["GRAD_ANAL15"].ToString(),
                Grad_anal6 = row["GRAD_ANAL6"] == DBNull.Value ? string.Empty : row["GRAD_ANAL6"].ToString(),
                Grad_anal7 = row["GRAD_ANAL7"] == DBNull.Value ? string.Empty : row["GRAD_ANAL7"].ToString(),
                Grad_anal8 = row["GRAD_ANAL8"] == DBNull.Value ? string.Empty : row["GRAD_ANAL8"].ToString(),
                Grad_anal9 = row["GRAD_ANAL9"] == DBNull.Value ? string.Empty : row["GRAD_ANAL9"].ToString(),
                Grad_anal10 = row["GRAD_ANAL10"] == DBNull.Value ? string.Empty : row["GRAD_ANAL10"].ToString(),
                Grad_anal11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString(),
                Grad_anal12 = row["GRAD_ANAL12"] == DBNull.Value ? string.Empty : row["GRAD_ANAL12"].ToString(),
                Grad_anal13 = row["GRAD_ANAL13"] == DBNull.Value ? string.Empty : row["GRAD_ANAL13"].ToString(),
                Grad_anal14 = row["GRAD_ANAL14"] == DBNull.Value ? string.Empty : row["GRAD_ANAL14"].ToString(),
                Grad_anal16 = row["GRAD_ANAL16"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL16"]),
                Grad_anal17 = row["GRAD_ANAL17"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL17"]),
                Grad_anal18 = row["GRAD_ANAL18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL18"])

            };
        }
    }
}
