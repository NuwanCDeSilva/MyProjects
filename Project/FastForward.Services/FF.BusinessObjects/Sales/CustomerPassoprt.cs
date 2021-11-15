using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class CustomerPassoprt
    {
        #region Private Members
        private string _sdcd_cre_by;
        private DateTime _sdcd_cre_dt;
        private string _sdcd_flight;
        private string _sdcd_invc_no;
        private int _sdcd_line;
        private string _sdcd_ref;
        #endregion

        public string Sdcd_cre_by
        {
            get { return _sdcd_cre_by; }
            set { _sdcd_cre_by = value; }
        }
        public DateTime Sdcd_cre_dt
        {
            get { return _sdcd_cre_dt; }
            set { _sdcd_cre_dt = value; }
        }
        public string Sdcd_flight
        {
            get { return _sdcd_flight; }
            set { _sdcd_flight = value; }
        }
        public string Sdcd_invc_no
        {
            get { return _sdcd_invc_no; }
            set { _sdcd_invc_no = value; }
        }
        public int Sdcd_line
        {
            get { return _sdcd_line; }
            set { _sdcd_line = value; }
        }
        public string Sdcd_ref
        {
            get { return _sdcd_ref; }
            set { _sdcd_ref = value; }
        }

        public static CustomerPassoprt Converter(DataRow row)
        {
            return new CustomerPassoprt
            {
                Sdcd_cre_by = row["SDCD_CRE_BY"] == DBNull.Value ? string.Empty : row["SDCD_CRE_BY"].ToString(),
                Sdcd_cre_dt = row["SDCD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SDCD_CRE_DT"]),
                Sdcd_flight = row["SDCD_FLIGHT"] == DBNull.Value ? string.Empty : row["SDCD_FLIGHT"].ToString(),
                Sdcd_invc_no = row["SDCD_INVC_NO"] == DBNull.Value ? string.Empty : row["SDCD_INVC_NO"].ToString(),
                Sdcd_line = row["SDCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["SDCD_LINE"]),
                Sdcd_ref = row["SDCD_REF"] == DBNull.Value ? string.Empty : row["SDCD_REF"].ToString()

            };
        }

    }
}
