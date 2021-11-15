using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchPC
    {
        #region Private Members
        private string _incl_area;
        private string _incl_chnl;
        private string _incl_circ;
        private Int32 _incl_line;
        private string _incl_pc;
        private string _incl_ref;
        private string _incl_reg;
        private string _incl_schnl;
        private string _incl_zone;
        #endregion

        public string Incl_area
        {
            get { return _incl_area; }
            set { _incl_area = value; }
        }
        public string Incl_chnl
        {
            get { return _incl_chnl; }
            set { _incl_chnl = value; }
        }
        public string Incl_circ
        {
            get { return _incl_circ; }
            set { _incl_circ = value; }
        }
        public Int32 Incl_line
        {
            get { return _incl_line; }
            set { _incl_line = value; }
        }
        public string Incl_pc
        {
            get { return _incl_pc; }
            set { _incl_pc = value; }
        }
        public string Incl_ref
        {
            get { return _incl_ref; }
            set { _incl_ref = value; }
        }
        public string Incl_reg
        {
            get { return _incl_reg; }
            set { _incl_reg = value; }
        }
        public string Incl_schnl
        {
            get { return _incl_schnl; }
            set { _incl_schnl = value; }
        }
        public string Incl_zone
        {
            get { return _incl_zone; }
            set { _incl_zone = value; }
        }

        public static IncentiveSchPC Converter(DataRow row)
        {
            return new IncentiveSchPC
            {
                Incl_area = row["INCL_AREA"] == DBNull.Value ? string.Empty : row["INCL_AREA"].ToString(),
                Incl_chnl = row["INCL_CHNL"] == DBNull.Value ? string.Empty : row["INCL_CHNL"].ToString(),
                Incl_circ = row["INCL_CIRC"] == DBNull.Value ? string.Empty : row["INCL_CIRC"].ToString(),
                Incl_line = row["INCL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCL_LINE"]),
                Incl_pc = row["INCL_PC"] == DBNull.Value ? string.Empty : row["INCL_PC"].ToString(),
                Incl_ref = row["INCL_REF"] == DBNull.Value ? string.Empty : row["INCL_REF"].ToString(),
                Incl_reg = row["INCL_REG"] == DBNull.Value ? string.Empty : row["INCL_REG"].ToString(),
                Incl_schnl = row["INCL_SCHNL"] == DBNull.Value ? string.Empty : row["INCL_SCHNL"].ToString(),
                Incl_zone = row["INCL_ZONE"] == DBNull.Value ? string.Empty : row["INCL_ZONE"].ToString()
            };
        }

    }
}
