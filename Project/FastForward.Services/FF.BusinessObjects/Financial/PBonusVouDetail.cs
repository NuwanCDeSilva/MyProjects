using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PBonusVouDetail
    {
        #region Private Members
        private string _pbpd_circ;
        private string _pbpd_sch;
        private decimal _pbpd_value;
        private string _pbpd_vou_ref;
        #endregion

        #region Public Property Definition
        public string Pbpd_circ
        {
            get { return _pbpd_circ; }
            set { _pbpd_circ = value; }
        }
        public string Pbpd_sch
        {
            get { return _pbpd_sch; }
            set { _pbpd_sch = value; }
        }

        public decimal Pbpd_value
        {
            get { return _pbpd_value; }
            set { _pbpd_value = value; }
        }
        public string Pbpd_vou_ref
        {
            get { return _pbpd_vou_ref; }
            set { _pbpd_vou_ref = value; }
        }
        #endregion

        #region Converters
        public static PBonusVouDetail Converter(DataRow row)
        {
            return new PBonusVouDetail
            {
                Pbpd_circ = row["PBPD_CIRC"] == DBNull.Value ? string.Empty : row["PBPD_CIRC"].ToString(),
                Pbpd_sch = row["PBPD_SCH"] == DBNull.Value ? string.Empty : row["PBPD_SCH"].ToString(),
                Pbpd_value = row["PBPD_VALUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPD_VALUE"]),
                Pbpd_vou_ref = row["PBPD_VOU_REF"] == DBNull.Value ? string.Empty : row["PBPD_VOU_REF"].ToString()

            };
        }
        #endregion
    }
}

