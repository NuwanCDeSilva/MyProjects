using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class scv_agr_payshed
    {
        #region Private Members
        private string _sap_agr_no;
        private decimal _sap_amt;
        private decimal _sap_bal_amt;
        private DateTime _sap_due_dt;
        private Int32 _sap_seq;
        private int _sap_term;
        private Boolean _sap_sel;
        #endregion

        #region Public Property Definition
        public string Sap_agr_no { get { return _sap_agr_no; } set { _sap_agr_no = value; } }
        public decimal Sap_amt { get { return _sap_amt; } set { _sap_amt = value; } }
        public decimal Sap_bal_amt { get { return _sap_bal_amt; } set { _sap_bal_amt = value; } }
        public DateTime Sap_due_dt { get { return _sap_due_dt; } set { _sap_due_dt = value; } }
        public Int32 Sap_seq { get { return _sap_seq; } set { _sap_seq = value; } }
        public int Sap_term { get { return _sap_term; } set { _sap_term = value; } }

        public Boolean Sap_sel { get { return _sap_sel; } set { _sap_sel = value; } }
        #endregion

        #region Converters
        public static scv_agr_payshed Converter(DataRow row)
        {
            return new scv_agr_payshed
            {

                Sap_agr_no = row["SAP_AGR_NO"] == DBNull.Value ? string.Empty : row["SAP_AGR_NO"].ToString(),
                Sap_amt = row["SAP_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAP_AMT"]),
                Sap_bal_amt = row["SAP_BAL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAP_BAL_AMT"]),
                Sap_due_dt = row["SAP_DUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAP_DUE_DT"]),
                Sap_seq = row["SAP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAP_SEQ"]),
                Sap_term = row["SAP_TERM"] == DBNull.Value ? 0 : Convert.ToInt16(row["SAP_TERM"])

            };
        }

        #endregion
    }
}

