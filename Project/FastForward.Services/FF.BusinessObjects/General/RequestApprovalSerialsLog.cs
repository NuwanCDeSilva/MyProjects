using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RequestApprovalSerialsLog
    {
        #region Private Members
        private string _gras_anal1;
        private decimal _gras_anal10;
        private string _gras_anal2;
        private string _gras_anal3;
        private string _gras_anal4;
        private string _gras_anal5;
        private decimal _gras_anal6;
        private decimal _gras_anal7;
        private decimal _gras_anal8;
        private decimal _gras_anal9;
        private int _gras_line;
        private string _gras_ref;
        private int _gras_lvl;
        #endregion

        public string Gras_anal1
        {
            get { return _gras_anal1; }
            set { _gras_anal1 = value; }
        }
        public decimal Gras_anal10
        {
            get { return _gras_anal10; }
            set { _gras_anal10 = value; }
        }
        public string Gras_anal2
        {
            get { return _gras_anal2; }
            set { _gras_anal2 = value; }
        }
        public string Gras_anal3
        {
            get { return _gras_anal3; }
            set { _gras_anal3 = value; }
        }
        public string Gras_anal4
        {
            get { return _gras_anal4; }
            set { _gras_anal4 = value; }
        }
        public string Gras_anal5
        {
            get { return _gras_anal5; }
            set { _gras_anal5 = value; }
        }
        public decimal Gras_anal6
        {
            get { return _gras_anal6; }
            set { _gras_anal6 = value; }
        }
        public decimal Gras_anal7
        {
            get { return _gras_anal7; }
            set { _gras_anal7 = value; }
        }
        public decimal Gras_anal8
        {
            get { return _gras_anal8; }
            set { _gras_anal8 = value; }
        }
        public decimal Gras_anal9
        {
            get { return _gras_anal9; }
            set { _gras_anal9 = value; }
        }
        public int Gras_line
        {
            get { return _gras_line; }
            set { _gras_line = value; }
        }
        public string Gras_ref
        {
            get { return _gras_ref; }
            set { _gras_ref = value; }
        }

        public int Gras_lvl
        {
            get { return _gras_lvl; }
            set { _gras_lvl = value; }
        }

        public static RequestApprovalSerialsLog Converter(DataRow row)
        {
            return new RequestApprovalSerialsLog
            {
                Gras_anal1 = row["GRAS_ANAL1"] == DBNull.Value ? string.Empty : row["GRAS_ANAL1"].ToString(),
                Gras_anal10 = row["GRAS_ANAL10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL10"]),
                Gras_anal2 = row["GRAS_ANAL2"] == DBNull.Value ? string.Empty : row["GRAS_ANAL2"].ToString(),
                Gras_anal3 = row["GRAS_ANAL3"] == DBNull.Value ? string.Empty : row["GRAS_ANAL3"].ToString(),
                Gras_anal4 = row["GRAS_ANAL4"] == DBNull.Value ? string.Empty : row["GRAS_ANAL4"].ToString(),
                Gras_anal5 = row["GRAS_ANAL5"] == DBNull.Value ? string.Empty : row["GRAS_ANAL5"].ToString(),
                Gras_anal6 = row["GRAS_ANAL6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL6"]),
                Gras_anal7 = row["GRAS_ANAL7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL7"]),
                Gras_anal8 = row["GRAS_ANAL8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL8"]),
                Gras_anal9 = row["GRAS_ANAL9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL9"]),
                Gras_line = row["GRAS_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_LINE"]),
                Gras_ref = row["GRAS_REF"] == DBNull.Value ? string.Empty : row["GRAS_REF"].ToString(),
                Gras_lvl = row["GRAS_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_LVL"])
            };
        }

    }
}
